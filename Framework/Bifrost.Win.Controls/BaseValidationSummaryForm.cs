using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NF.Framework.Win.Validation
{
	public partial class BaseValidationSummaryForm : Form
	{
		public BaseValidationSummaryForm()
		{
			InitializeComponent();
		}

		public virtual void SetError(string caption, string message)
		{
		}
		public virtual void LoadValidators(ValidatorCollection validators)
		{ 
		}
	}
}