using System.Reflection;
using System.Runtime.Loader;

namespace CoreApp
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly string? _pluginPath;
        private readonly AssemblyDependencyResolver _resolver;

        public PluginLoadContext( string pluginFile) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(pluginFile);
            _pluginPath = Path.GetDirectoryName(pluginFile);
        }

		/// <summary>
		/// Load assembely from the plugin path in isolation but including the loaded assembly in the current app domain
		/// </summary>
		/// <param name="assemblyName"></param>
		/// <returns></returns>
        protected override Assembly? Load(AssemblyName assemblyName)
        {

			// Check if the assembly is already loaded
			var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies()
				.FirstOrDefault(a => a.GetName().Name == assemblyName.Name);

			if (loadedAssembly != null)
			{
				return loadedAssembly;
			}

			// For other assemblies, load from the plugin folder or resolve dependencies
			string assemblyPath = Path.Combine(_pluginPath, $"{assemblyName.Name}.dll");
			if (File.Exists(assemblyPath))
			{
				return LoadFromAssemblyPath(assemblyPath);
			}

			// Resolve any additional dependencies
			return ResolveDependency(assemblyName);
		}

		private Assembly ResolveDependency(AssemblyName assemblyName)
		{
			try
			{
				
				return Default.LoadFromAssemblyName(assemblyName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading dependency {assemblyName.Name}: {ex.Message}");
				return null;
			}
		}
    }
}
