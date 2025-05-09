using pluginA.Service;
using System;
using System.Windows.Forms;


namespace pluginA
{
	public partial class TestPanel : Form
	{
		FormTooltip dragTooltip;
		ServiceStatusChecker _serviceStatus;
		private BindingSource _feedbackBindingSource;

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
