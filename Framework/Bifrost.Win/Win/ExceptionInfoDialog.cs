using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bifrost.Win
{
	public partial class ExceptionInfoDialog : Form
	{
		public void ShowException(Exception ex)
		{
			ShowException(ex, 2);
		}

		public void ShowException(Exception ex, int msgType)
		{
			if (msgType > 1)
				txtContents.Text = ex.ToString().Replace("\n", Environment.NewLine);
			else
				txtContents.Text = ex.Message.Replace("\n", Environment.NewLine);

		}

		public ExceptionInfoDialog()
		{
			InitializeComponent();
		}
	}
}