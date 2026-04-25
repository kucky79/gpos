using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HiNet.Framework.Common;

namespace HiNet.Framework.Win.Controls
{
	public partial class XHeaderPanel : XPanel, IXControl
	{
		public XHeaderPanel()
		{
			InitializeComponent();
		}

		#region IXControl Members

		void IXControl.OnSubSystemTypeChanged(SubSystemType subSysType)
		{
			this.BackColor = UIHelper.GetFormSettings(this).HeaderBackColor;			
		}

		#endregion
}
}
