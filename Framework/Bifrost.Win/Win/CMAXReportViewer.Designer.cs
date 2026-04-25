namespace CMAX.Framework.Win
{
	partial class CMAXReportViewer
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
			this.ReportViewerCtl = new Microsoft.Reporting.WinForms.ReportViewer();
			this.SuspendLayout();
			// 
			// ReportViewerCtl
			// 
			this.ReportViewerCtl.AccessibleName = "WinReportServiceViewer";
			this.ReportViewerCtl.AllowDrop = true;
			this.ReportViewerCtl.AutoScroll = true;
			this.ReportViewerCtl.AutoSize = true;
			this.ReportViewerCtl.BackColor = System.Drawing.Color.White;
			this.ReportViewerCtl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ReportViewerCtl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReportViewerCtl.LocalReport.ReportEmbeddedResource = null;
			this.ReportViewerCtl.LocalReport.ReportPath = null;
			this.ReportViewerCtl.Location = new System.Drawing.Point(0, 0);
			this.ReportViewerCtl.Name = "ReportViewerCtl";
			this.ReportViewerCtl.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
			this.ReportViewerCtl.PromptAreaCollapsed = false;
			this.ReportViewerCtl.ShowBackButton = true;
			this.ReportViewerCtl.ShowContextMenu = true;
			this.ReportViewerCtl.ShowCredentialPrompts = true;
			this.ReportViewerCtl.ShowDocumentMap = true;
			this.ReportViewerCtl.ShowDocumentMapButton = true;
			this.ReportViewerCtl.ShowExportButton = true;
			this.ReportViewerCtl.ShowPageNavigationControls = true;
			this.ReportViewerCtl.ShowParameterPrompts = true;
			this.ReportViewerCtl.ShowPrintButton = true;
			this.ReportViewerCtl.ShowProgress = true;
			this.ReportViewerCtl.ShowPromptAreaButton = true;
			this.ReportViewerCtl.ShowRefreshButton = true;
			this.ReportViewerCtl.ShowStopButton = true;
			this.ReportViewerCtl.ShowToolBar = true;
			this.ReportViewerCtl.ShowZoomButton = true;
			this.ReportViewerCtl.Size = new System.Drawing.Size(602, 483);
			this.ReportViewerCtl.TabIndex = 0;
			this.ReportViewerCtl.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
			this.ReportViewerCtl.ZoomPercent = 100;
			// 
			// CMAXReportViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(602, 483);
			this.Controls.Add(this.ReportViewerCtl);
			this.Name = "CMAXReportViewer";
			this.Text = "Report Viewer";
			this.Load += new System.EventHandler(this.CMAXReportViewer_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		
		public Microsoft.Reporting.WinForms.ReportViewer ReportViewerCtl;
	}
}