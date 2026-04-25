using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bifrost.Win
{
	public partial class UserControlBase : UserControl
	{
		public MDIBase MdiForm
		{
			get
			{
				try
				{
					Form form = this.FindForm();
					if (form is MDIBase)
					{
						return (MDIBase)form;
					}
                    else
					{
						return (MDIBase)Application.OpenForms[0];
					}
				}
				catch
				{
					return null;
				}
			}
		}

        public UserControlBase()
		{
			InitializeComponent();
        }

		//protected override void OnPaint(PaintEventArgs pe)
		//{
		//	// TODO: Add custom paint code here

		//	// Calling the base class OnPaint
		//	base.OnPaint(pe);
		//}
	}
}
