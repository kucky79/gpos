using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
	public partial class PrgForm : Form
	{
		public PrgForm()
		{
			InitializeComponent();
		}

		public void UpdateProgress(int position, int maxValue)
		{
			this.progressBar1.Maximum = maxValue;
			this.progressBar1.Value = position;
			this.label1.Text = string.Format("Downloading...{0}%", position * 100 / maxValue);
			Application.DoEvents();
		}

		public void UpdateProgress(int position, int maxValue, string fileName)
		{
			this.progressBar1.Maximum = maxValue;
			this.progressBar1.Value = position;
			this.label1.Text = string.Format("Downloading {0}", fileName);
			Application.DoEvents();
		}

	}
}