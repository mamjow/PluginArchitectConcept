using System.Reflection;
using System.Runtime.Loader;

namespace CoreApp
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly string _pluginPath;
        private readonly AssemblyDependencyResolver _resolver;

        public PluginLoadContext(string pluginPath, string pluginFile) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(pluginFile);
            _pluginPath = pluginPath;
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {



			string assemblyPath = Path.Combine(_pluginPath, $"{assemblyName.Name}.dll");
            if (File.Exists(assemblyPath))
            {
                return LoadFromAssemblyPath(assemblyPath);
            }


            return ResolveDependency(assemblyName);
        }

        //protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        //{
        //    string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        //    if (libraryPath != null)
        //    {
        //        return LoadUnmanagedDllFromPath(libraryPath);
        //    }

        //    return IntPtr.Zero;
        //}

        private Assembly ResolveDependency(AssemblyName assemblyName)
        {
            string dependencyPath = Path.Combine(_pluginPath, $"{assemblyName.Name}.dll");
            if (File.Exists(dependencyPath))
            {
                return LoadFromAssemblyPath(dependencyPath);
            }

            try
            {
                return Default.LoadFromAssemblyName(assemblyName);
            }
            catch
            {
                return null;
            }
        }

        private static bool IsSharedAssembly(AssemblyName assemblyName)
        {
            // Load the assembly to get its physical location
            var assembly = Assembly.Load(assemblyName);
            var location = assembly.Location;

            // Check if the assembly comes from the shared .NET framework folder
 

            var sharedPrefixes = new[]
             {
                "Microsoft.Extensions.DependencyInjection",  // Dependency Injection
                "Microsoft.NETCore.App",                    // .NET Core runtime
                "Microsoft.WindowsDesktop.App.WindowsForms", // Windows Forms
                "Microsoft.EntityFrameworkCore", // Windows Forms
             };

            var isDotnetShared =  location.Contains(@"\dotnet\shared\", StringComparison.OrdinalIgnoreCase);
            var isShared = sharedPrefixes.Any(prefix => assemblyName.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
            return isDotnetShared || isShared;
        }
    }
}
