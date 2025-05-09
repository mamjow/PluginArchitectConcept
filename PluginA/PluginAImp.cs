using pluginA.Service;
using Microsoft.Extensions.DependencyInjection;
using Contract;
using System;
using System.Windows.Forms;
using pluginA.Db;
using Microsoft.EntityFrameworkCore;

namespace pluginA
{
    public class PluginAImp : IPlugin
    {
        public float Version => 1.0f;
        public string Name => "Plugin A";
        public Form Execute(IServiceProvider provider)
        {
            try
            {
				var form = provider.GetRequiredService<TestPanel>();
				form.Text = this.Name;
				return form;
			}
			catch (Exception ex)
			{
				 MessageBox.Show(ex.Message);
				return new Form();
			}

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ServiceStatusChecker>();
			var dbConnection = "Server=localhost;Database=plugin;User=sa;Password=Development1!;TrustServerCertificate=True;";

			services.AddDbContext<DxAnalyzerContext>(o => o.UseSqlServer(dbConnection));
            services.AddTransient<TestPanel>();
            services.AddHttpClient();
        }
    }
}
