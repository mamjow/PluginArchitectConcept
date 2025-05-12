using pluginB.Service;
using System;
using System.Windows.Forms;


namespace pluginB
{
	public partial class TestPanel : Form
	{
		ServiceStatusChecker _serviceStatus;
		public TestPanel(ServiceStatusChecker sd)
		{
			InitializeComponent();
			_serviceStatus = sd;
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			await _serviceStatus.GetGoogle();
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			await _serviceStatus.SomthingWithDB();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			_serviceStatus.CheckServiceStatus("localhost", "MSSQLSERVER");
		}
	}
}
