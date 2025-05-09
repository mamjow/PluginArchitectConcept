using DxErpIntegration.Service;
using Microsoft.Extensions.DependencyInjection;
using Contract;
using System;
using System.Windows.Forms;

namespace DxErpIntegration
{
    public class ErpIntegrationAnalyzerPlugin : IPlugin
    {
        public float Version => 1.0f;

        public string Name => "Erp Framework Integration Analyzer";

        public Form Execute(IServiceProvider provider)
        {
            var form = provider.GetRequiredService<TestPanel>();
            form.Text = this.Name;
            return form;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<ServiceStatusChecker>();
            services.AddTransient<TestPanel>();
            services.AddHttpClient();
        }
    }
}
