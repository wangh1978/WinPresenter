/* All content in this sample is ”AS IS?with with no warranties, and confer no rights. 
 * Any code on this blog is subject to the terms specified at http://www.microsoft.com/info/cpyright.mspx. 
 */

namespace WinViewer
{
    partial class WinViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinViewer));
            this.pRdpViewer = new AxRDPCOMAPILib.AxRDPViewer();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ControlButton = new System.Windows.Forms.Button();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.CaptureWndButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pRdpViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // pRdpViewer
            // 
            this.pRdpViewer.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.pRdpViewer.AllowDrop = true;
            this.pRdpViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pRdpViewer.Enabled = true;
            this.pRdpViewer.Location = new System.Drawing.Point(0, 0);
            this.pRdpViewer.Name = "pRdpViewer";
            this.pRdpViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pRdpViewer.OcxState")));
            this.pRdpViewer.Size = new System.Drawing.Size(1278, 1006);
            this.pRdpViewer.TabIndex = 0;
            this.pRdpViewer.OnConnectionEstablished += new System.EventHandler(this.OnConnectionEstablished);
            this.pRdpViewer.OnConnectionFailed += new System.EventHandler(this.OnConnectionFailed);
            this.pRdpViewer.OnConnectionTerminated += new AxRDPCOMAPILib._IRDPSessionEvents_OnConnectionTerminatedEventHandler(this.OnConnectionTerminated);
            this.pRdpViewer.OnError += new AxRDPCOMAPILib._IRDPSessionEvents_OnErrorEventHandler(this.OnError);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(12, 11);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(49, 21);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Á¬½Ó";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(76, 11);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(53, 21);
            this.DisconnectButton.TabIndex = 2;
            this.DisconnectButton.Text = "¶Ï¿ª";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ControlButton
            // 
            this.ControlButton.Location = new System.Drawing.Point(12, 38);
            this.ControlButton.Name = "ControlButton";
            this.ControlButton.Size = new System.Drawing.Size(183, 21);
            this.ControlButton.TabIndex = 3;
            this.ControlButton.Text = "Control Desktop";
            this.ControlButton.UseVisualStyleBackColor = true;
            this.ControlButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(214, 13);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogTextBox.Size = new System.Drawing.Size(454, 46);
            this.LogTextBox.TabIndex = 4;
            // 
            // CaptureWndButton
            // 
            this.CaptureWndButton.Location = new System.Drawing.Point(135, 11);
            this.CaptureWndButton.Name = "CaptureWndButton";
            this.CaptureWndButton.Size = new System.Drawing.Size(53, 21);
            this.CaptureWndButton.TabIndex = 2;
            this.CaptureWndButton.Text = "½ØÍ¼";
            this.CaptureWndButton.UseVisualStyleBackColor = true;
            this.CaptureWndButton.Click += new System.EventHandler(this.CaptureWndButton_Click);
            // 
            // WinViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 1006);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.ControlButton);
            this.Controls.Add(this.CaptureWndButton);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.pRdpViewer);
            this.Name = "WinViewer";
            this.Text = "WinViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pRdpViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxRDPCOMAPILib.AxRDPViewer pRdpViewer;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button ControlButton;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Button CaptureWndButton;
    }
}

