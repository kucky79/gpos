#region Using directives

using System.Windows.Forms;

#endregion

namespace AppStarter
{
    public partial class DialogBase : Form
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
		
		///// <summary>
		///// Change SubSystemType
		///// </summary>
		//protected override void OnSubSystemTypeChanged()
		//{
  //          ///
  //          /// Apply colors to panels
  //          /// 
  //          this.BackColor = formSettings.FormBackColor;
  //          pnlContainer.BackColor = formSettings.WorkAreaBackColor;
  //          pnlContainer.BorderColor = formSettings.BorderColor;

  //          ///
  //          /// Call control IXControl.OnSubSysTypeChanged function
  //          /// 
  //          ApplyNewSubSystemType(this);
		//}

        /// <summary>
        /// Processing shortcut key
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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