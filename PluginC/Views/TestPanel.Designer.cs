using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pluginC
{
    partial class TestPanel
    {

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestPanel));
            ToolTip = new ToolTip(components);
            errorProvider = new ErrorProvider(components);
            sucessProvider = new ErrorProvider(components);
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sucessProvider).BeginInit();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // sucessProvider
            // 
            sucessProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            sucessProvider.ContainerControl = this;
            sucessProvider.Icon = (System.Drawing.Icon)resources.GetObject("sucessProvider.Icon");
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(174, 90);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "do http";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(265, 90);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "do db stuff";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(359, 90);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(75, 23);
            button3.TabIndex = 2;
            button3.Text = "du shared stuff";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // TestPanel
            // 
            AllowDrop = true;
            ClientSize = new System.Drawing.Size(613, 661);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            HelpButton = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(575, 700);
            Name = "TestPanel";
            Padding = new Padding(5);
            Text = "Analyze 3DX Integration Framework";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)sucessProvider).EndInit();
            ResumeLayout(false);
        }
        private ToolTip ToolTip;
        private System.ComponentModel.IContainer components;
        private ErrorProvider errorProvider;
        private ErrorProvider sucessProvider;
        private Label label7;
        private Label label6;
        private Button button1;
        private Button button3;
        private Button button2;
    }
}
