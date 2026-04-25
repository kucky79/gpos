using NF.A2P.Helper;
using NF.Framework.Win;
using NF.Framework.Win.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_EXCEL_DOWNLOAD : PopupBase
    {
        string _excelFileName = "";
        string _fileName = "";

        public POPUP_EXCEL_DOWNLOAD(string excelFileName)
        {
            _excelFileName = excelFileName;
            InitializeComponent();
            InitEvent();
            InitForm();

            radioGroup_FgGroup.EditValue = "xlsx";
        }

        public POPUP_EXCEL_DOWNLOAD()
        {
            InitializeComponent();
            InitEvent();
            InitForm();

            radioGroup_FgGroup.EditValue = "xlsx";
        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "Excel Download";

                //폼 위치 조정
                CenterToParent();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void InitEvent()
        {
            aButton_Open.Click += AButton_Click;
            aButton_Save.Click += AButton_Click;
            aButton_Cancel.Click += AButton_Click;
        }

        private void AButton_Click(object sender, EventArgs e)
        {
            aButton btn = sender as aButton;
            string[] returnData = new string[2];

            try
            {
                switch (btn.Name)
                {
                    case "aButton_Open":
                        returnData[0] = "O";
                        returnData[1] = A.GetString(radioGroup_FgGroup.EditValue);
                        ReturnData.Add("ReturnData", returnData);
                        OnOK();
                        break;
                    case "aButton_Save":
                        returnData[0] = "S";
                        returnData[1] = A.GetString(radioGroup_FgGroup.EditValue);
                        ReturnData.Add("ReturnData", returnData);
                        OnOK();
                        break;
                    case "aButton_Cancel":
                        OnCancel();
                        break;
                }
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            this.DialogResult = DialogResult.OK;
        }

        protected override void OnCancel()
        {
            this.Close();
        }
    }
}
