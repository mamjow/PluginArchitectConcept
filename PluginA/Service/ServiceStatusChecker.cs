using pluginA.Db;
using System;
using System.Management;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pluginA.Service
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


		public async Task GetGoogle()
		{

			var he = await _httpClient.GetAsync("https://ifconfig.me/ip");
			if (he.IsSuccessStatusCode)
			{
				var content = await he.Content.ReadAsStringAsync();
				MessageBox.Show(content);
			}
			else if (he.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				MessageBox.Show("Not Found");
			}
			else if (he.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				MessageBox.Show("Unauthorized");
			}
			else
			{
				Console.WriteLine("Error: " + he.StatusCode);
			}

		}
		public async Task SomthingWithDB()
		{
			try
			{

				var isCreated = await _ds.Database.EnsureCreatedAsync();
				var isOK = await _ds.Database.CanConnectAsync();
				await _ds.Messages.AddAsync(new Db.Models.DxMessage()
				{
					Message = "Test",

				});
				await _ds.SaveChangesAsync();
				MessageBox.Show("DB Created: " + isCreated.ToString() + " CanConnect: " + isOK.ToString());

			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);

			}

		}
		public void CheckServiceStatus(string remoteMachine, string serviceName, string username = null, string password = null)
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
				var services = searcher.Get();
				if (services.Count == 0)
				{
					MessageBox.Show($"Service: {serviceName} not found");
					return;
				}

				foreach (ManagementObject service in services)
				{
					MessageBox.Show($"Service: {service["DisplayName"]} found \n Status: {service["Status"]} \n Start Mode: {service["StartMode"]}");
				}
			}
			catch (UnauthorizedAccessException uae)
			{
				MessageBox.Show("Access denied. Check your username/password or firewall settings.");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error connecting to remote machine: {ex.Message}");
			}
		}
	}
}
