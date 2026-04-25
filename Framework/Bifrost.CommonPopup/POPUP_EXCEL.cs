using NF.A2P.Helper;
using NF.Framework.Win;
using System;
using System.Windows.Forms;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_EXCEL : PopupBase
    {
        public POPUP_EXCEL()
        {
            InitializeComponent();
            InitForm();

            radioGroup_FgGroup.EditValue = "U";
        }
        private void InitForm()
        {
            try
            {
                PopupTitle = "Excel";

                //폼 위치 조정
                CenterToParent();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            ReturnData.Add("ReturnData", A.GetString(radioGroup_FgGroup.EditValue));
            this.DialogResult = DialogResult.OK;
        }

        protected override void OnCancel()
        {
            this.Close();
        }
    }
}
