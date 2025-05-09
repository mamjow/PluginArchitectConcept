using Microsoft.Extensions.DependencyInjection;

namespace Contract
{
    public interface IPlugin
    {
        public string Name { get; }

        public float Version { get;  }

        public void ConfigureServices(IServiceCollection services);
        public Form Execute(IServiceProvider provider);
    }
}
