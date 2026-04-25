namespace Jarvis
{
	partial class UpdateProgForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateProgForm));
            this.progressBarMenu = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressBarTotal = new DevExpress.XtraEditors.ProgressBarControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label_Main = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarMenu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Appearance.Options.UseBackColor = true;
            this.pnlContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Size = new System.Drawing.Size(500, 200);
            this.pnlContainer.TabIndex = 4;
            // 
            // progressBarMenu
            // 
            this.progressBarMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMenu.Location = new System.Drawing.Point(12, 131);
            this.progressBarMenu.Name = "progressBarMenu";
            this.progressBarMenu.Properties.FlowAnimationEnabled = true;
            this.progressBarMenu.Size = new System.Drawing.Size(476, 25);
            this.progressBarMenu.TabIndex = 2;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarTotal.Location = new System.Drawing.Point(12, 164);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Properties.FlowAnimationEnabled = true;
            this.progressBarTotal.Size = new System.Drawing.Size(476, 25);
            this.progressBarTotal.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label_Main);
            this.panelControl1.Controls.Add(this.progressBarTotal);
            this.panelControl1.Controls.Add(this.progressBarMenu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(500, 200);
            this.panelControl1.TabIndex = 0;
            // 
            // label_Main
            // 
            this.label_Main.Location = new System.Drawing.Point(12, 59);
            this.label_Main.Name = "label_Main";
            this.label_Main.Size = new System.Drawing.Size(149, 14);
            this.label_Main.TabIndex = 1;
            this.label_Main.Text = "Update New Version.........";
            // 
            // UpdateProgForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateProgForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateProgForm";
            this.Shown += new System.EventHandler(this.UpdateProgForm_Shown);
            this.Controls.SetChildIndex(this.pnlContainer, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarMenu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
        private DevExpress.XtraEditors.ProgressBarControl progressBarMenu;
        private DevExpress.XtraEditors.ProgressBarControl progressBarTotal;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl label_Main;
    }
}