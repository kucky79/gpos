namespace AppStarter
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
            this.lblUpdate = new System.Windows.Forms.Label();
            this.statusBarDetail = new System.Windows.Forms.ProgressBar();
            this.statusBarTotal = new System.Windows.Forms.ProgressBar();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(246)))));
            this.pnlContainer.Controls.Add(this.statusBarDetail);
            this.pnlContainer.Controls.Add(this.statusBarTotal);
            this.pnlContainer.Controls.Add(this.lblUpdate);
            this.pnlContainer.Size = new System.Drawing.Size(318, 92);
            // 
            // lblUpdate
            // 
            this.lblUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblUpdate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblUpdate.Location = new System.Drawing.Point(5, 5);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(308, 17);
            this.lblUpdate.TabIndex = 0;
            this.lblUpdate.Text = "Update New Version.........";
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusBarDetail
            // 
            this.statusBarDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarDetail.Location = new System.Drawing.Point(5, 51);
            this.statusBarDetail.Name = "statusBarDetail";
            this.statusBarDetail.Size = new System.Drawing.Size(308, 18);
            this.statusBarDetail.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.statusBarDetail.TabIndex = 1;
            // 
            // statusBarTotal
            // 
            this.statusBarTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarTotal.Location = new System.Drawing.Point(5, 69);
            this.statusBarTotal.Name = "statusBarTotal";
            this.statusBarTotal.Size = new System.Drawing.Size(308, 18);
            this.statusBarTotal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.statusBarTotal.TabIndex = 2;
            // 
            // UpdateProgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 102);
            this.ControlBox = false;
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
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblUpdate;
		private System.Windows.Forms.ProgressBar statusBarDetail;
        private System.Windows.Forms.ProgressBar statusBarTotal;
    }
}