namespace WinViewer
{
    partial class RdpViewerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RdpViewerWindow));
            this.axRDPViewer1 = new AxRDPCOMAPILib.AxRDPViewer();
            ((System.ComponentModel.ISupportInitialize)(this.axRDPViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axRDPViewer1
            // 
            this.axRDPViewer1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.axRDPViewer1.Enabled = true;
            this.axRDPViewer1.Location = new System.Drawing.Point(-8, -30);
            this.axRDPViewer1.Name = "axRDPViewer1";
            this.axRDPViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axRDPViewer1.OcxState")));
            this.axRDPViewer1.Size = new System.Drawing.Size(1280, 1024);
            this.axRDPViewer1.TabIndex = 0;
            // 
            // RdpViewerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.axRDPViewer1);
            this.Name = "RdpViewerWindow";
            this.Text = "RdpViewerWindow";
            ((System.ComponentModel.ISupportInitialize)(this.axRDPViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxRDPCOMAPILib.AxRDPViewer axRDPViewer1;
    }
}