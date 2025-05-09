using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pluginA
{
    public partial class FormTooltip
    {
        private Label label1;
        private void InitializeComponent()
        {
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(4, 4);
            label1.Name = "label1";
            label1.Size = new Size(184, 15);
            label1.TabIndex = 0;
            label1.Text = "You can drop it any moment now";
            // 
            // FormTooltip
            // 
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.LightYellow;
            ClientSize = new Size(193, 24);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormTooltip";
            Opacity = 0.9D;
            Padding = new Padding(4);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
