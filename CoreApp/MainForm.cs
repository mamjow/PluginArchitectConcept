using Microsoft.Extensions.DependencyInjection;
using Contract;
using System.Text.Json;

namespace CoreApp
{
    public partial class MainForm : Form
    {
        public record LoadedPlugin(IPlugin Instance, PluginMetadata Metadata);
        public MainForm()
        {
            InitializeComponent();
            string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            if (!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
            }

            var plugins = LoadPlugins(pluginPath);

            var pluginServicesCollection = new ServiceCollection();
            pluginServicesCollection.AddHttpClient();

            foreach (var pluginData in plugins)
            {
                var plugin = pluginData.Instance;
                plugin.ConfigureServices(pluginServicesCollection);
                var pluginServiceProvider = pluginServicesCollection.BuildServiceProvider();

                AddinToolStripMenuItem.DropDownItems.Add(plugin.Name).Click += (sender, e) =>
                {
                    var form = plugin.Execute(pluginServiceProvider);
                    form.MdiParent = this;
                    form.Show();
                };

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InstallNewAddinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var installAddin = new InstallPluginForm();
            //installAddin.MdiParent = this;
            //installAddin.Show();
        }

        private List<LoadedPlugin> LoadPlugins(string pluginsRoot)
        {
            Console.WriteLine($"IPlugin from assembly: {typeof(IPlugin).Assembly.FullName}");
            List<LoadedPlugin> plugins = [];

            foreach (var folder in Directory.GetDirectories(pluginsRoot))
            {
                var pluginJsonPath = Path.Combine(folder, "plugin.json");
                if (!File.Exists(pluginJsonPath))
                {
                    continue;
                }

                var metadata = JsonSerializer.Deserialize<PluginMetadata>(File.ReadAllText(pluginJsonPath), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var mainDll = Path.Combine(folder, $"{Path.GetFileName(folder)}.dll");

                if (!File.Exists(mainDll)) continue;

                var context = new PluginLoadContext( mainDll);
                var assembly = context.LoadFromAssemblyPath(mainDll);

                var pluginType = assembly.GetType(metadata!.MainClass);
                if (pluginType == null || !typeof(IPlugin).IsAssignableFrom(pluginType)) continue;

                var pluginInstance = (IPlugin?)Activator.CreateInstance(pluginType);
                if (pluginInstance != null)
                {
                    plugins.Add(new LoadedPlugin(pluginInstance, metadata));
                }
            }
            return plugins;
        }
    }
}
