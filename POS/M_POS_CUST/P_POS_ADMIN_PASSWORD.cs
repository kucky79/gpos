using Bifrost.Win;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class P_POS_ADMIN_PASSWORD : POSPopupBase
    {
        public P_POS_ADMIN_PASSWORD()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        private void InitEvent()
        {
            btnAdminCheck.Click += BtnAdminCheck_Click;
        }

        private void BtnAdminCheck_Click(object sender, EventArgs e)
        {
            if (txtAdminPassword.Text == "YOUR_ADMIN_PASSWORD")
            {
                OnOK();
            }
            else
            {
                ShowMessageBoxA("암호가 정확하지 않습니다.", Bifrost.Common.MessageType.Error);
                txtAdminPassword.Text = string.Empty;
            }
        }


        private void InitForm()
        {
            try
            {
                PopupTitle = "암호 입력";

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
            this.DialogResult = DialogResult.OK;
        }

    }
}
