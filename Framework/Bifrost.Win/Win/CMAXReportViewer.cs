using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Security.Principal;
using System.Net;
//using ReportingServiceClient.ReportWebService;
using Microsoft.Reporting.WinForms;
using System.Collections;
using CMAX.Framework.Data;
using System.Drawing.Printing;

namespace CMAX.Framework.Win
{
	public partial class CMAXReportViewer : CMAX.Framework.Win.FormBase
	{
		public CMAXReportViewer()
		{
			InitializeComponent();
		}

		#region Public

		/// <summary>
		/// Set ReportViewer's Server Report Path
		/// </summary>
		public string ReportPath
		{
			set
			{
				this.ReportViewerCtl.ServerReport.ReportPath = value;
			}
		}

		private ArrayList _reportParameters = new ArrayList();

		public void AddReportParameter(string name, string value)
		{
			_reportParameters.Add(new ReportParameter(name, value));
		}

		public void AddReportParameter(string name, string value, bool visible)
		{
			_reportParameters.Add(new ReportParameter(name, value, visible));
		}

		public void AddReportParameter(string name, string[] values)
		{
			_reportParameters.Add(new ReportParameter(name, values));
		}

		public void AddReportParameter(string name, string[] values, bool visible)
		{
			_reportParameters.Add(new ReportParameter(name, values, visible));
		}

		#endregion				

		private void CMAXReportViewer_Load(object sender, EventArgs e)
		{
			this.ReportViewerCtl.ServerReport.ReportServerUrl = new Uri(MdiForm.Global.ReportServer);
			this.ReportViewerCtl.ServerReport.ReportServerCredentials = new ReportCredential(MdiForm.Global.RSUser, MdiForm.Global.RSUserPwd);
			if (_reportParameters.Count != 0)
			{
				this.ReportViewerCtl.ServerReport.SetParameters((ReportParameter[])_reportParameters.ToArray(typeof(ReportParameter)));
			}
			this.ReportViewerCtl.RefreshReport();
		}

	}
}