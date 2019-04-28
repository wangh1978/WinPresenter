namespace WinSharer
{
    partial class WinSharer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.textIp = new System.Windows.Forms.TextBox();
            this.SetButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(31, 50);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(210, 21);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start Presentation";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(273, 50);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(193, 21);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop Presentation";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(31, 106);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogTextBox.Size = new System.Drawing.Size(435, 141);
            this.LogTextBox.TabIndex = 3;
            // 
            // textIp
            // 
            this.textIp.Location = new System.Drawing.Point(95, 10);
            this.textIp.Name = "textIp";
            this.textIp.Size = new System.Drawing.Size(182, 21);
            this.textIp.TabIndex = 4;
            // 
            // SetButton
            // 
            this.SetButton.Location = new System.Drawing.Point(298, 10);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(75, 23);
            this.SetButton.TabIndex = 5;
            this.SetButton.Text = "…Ë÷√";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Window;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(31, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(58, 14);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "Host IP:";
            // 
            // WinSharer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 259);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.textIp);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Name = "WinSharer";
            this.Text = "WinSharer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.TextBox textIp;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.TextBox textBox2;
    }
}

