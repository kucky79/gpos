#region Using directives

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;

using Bifrost.Common;
using Bifrost.Data;
using Bifrost.Win.Controls;
using Bifrost.Win.Util;

#endregion

namespace Bifrost.Win
{
	public partial class DialogBase : FormBase
	{
		#region Privates

		//private void ApplyNewSubSystemType(Control c)
		//{
		//	foreach (Control ctrl in c.Controls)
		//	{
		//		if (ctrl is IXControl)
		//		{
		//			((IXControl)ctrl).OnSubSystemTypeChanged(this.SubSystemType,false);
		//		}

		//		ApplyNewSubSystemType(ctrl);
		//	}
		//}
		
		/// <summary>
		/// Change SubSystemType
		/// </summary>
		protected override void OnSubSystemTypeChanged()
		{
            ///
            /// Apply colors to panels
            /// 
            this.BackColor = formSettings.FormBackColor;
            pnlContainer.BackColor = formSettings.WorkAreaBackColor;
            //pnlContainer.BorderColor = formSettings.BorderColor;

            ///
            /// Call control IXControl.OnSubSysTypeChanged function
            /// 
            //ApplyNewSubSystemType(this);
		}

        /// <summary>
        /// Processing shortcut key
        /// </summary>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        #endregion

        public DialogBase()
		{
			InitializeComponent();
		}
	}
}