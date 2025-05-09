using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxErpIntegration.Service
{
    public class PluginSetting
    {
        public string ConnectionString { get;  set; }
        public string BaseUrl { get;  set; }
        public string AgentID { get;  set; }
        public string AgentPassword { get;  set; }
        public string SecurityContext { get;  set; }
    }
}
