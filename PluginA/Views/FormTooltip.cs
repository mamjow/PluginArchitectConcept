
using System.Drawing;
using System.Windows.Forms;

namespace pluginA
{
    public partial class FormTooltip : Form
    {
        public FormTooltip()
        {
            InitializeComponent();
        }
        public void UpdateLocation(Point screenLocation)
        {
            this.Location = new Point(screenLocation.X + 10, screenLocation.Y + 10);
        }

        public void SetText(string text)
        {
            label1.Text = text;
        }
    }
}
