using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.Http;
using DxErpIntegration.Db;

namespace DxErpIntegration.Service
{
    public class ServiceStatusChecker
    {

        HttpClient _httpClient;
        DxAnalyzerContext _ds;
        public ServiceStatusChecker(HttpClient client, DxAnalyzerContext ds)
        {
            _ds = ds;
            _httpClient = client;
        }


        public async Task GetGoogle() {

            var he = await _httpClient.GetAsync("googl.com");
            if (he.IsSuccessStatusCode)
            {
                var content = await he.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Error: " + he.StatusCode);
            }

        }
        public async Task SomthingWithDB()
        {
            await _ds.Messages.AddAsync(new Db.Models.DxMessage()
            {
                Message = "Test",

            });
        }
        public  void CheckServiceStatus(string remoteMachine, string serviceName, string username = null, string password = null)
        {
 
            ConnectionOptions options = new ConnectionOptions();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                options.Username = username; 
                options.Password = password;
                options.EnablePrivileges = true; 
            }

 
            ManagementScope scope = new ManagementScope($"\\\\{remoteMachine}\\root\\cimv2", options);

            try
            {
                scope.Connect(); 

                ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_Service WHERE Name = '{serviceName}'");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
  
                foreach (ManagementObject service in searcher.Get())
                {
                    Console.WriteLine($"Service: {service["Name"]}");
                    Console.WriteLine($"Display Name: {service["DisplayName"]}");
                    Console.WriteLine($"State: {service["State"]}");    
                    Console.WriteLine($"Status: {service["Status"]}"); 
                    Console.WriteLine($"Start Mode: {service["StartMode"]}");
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine("Access denied. Check your username/password or firewall settings.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to remote machine: {ex.Message}");
            }
        }
    }
}
