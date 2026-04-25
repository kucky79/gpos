using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Common;
using Bifrost;

namespace SAL
{
    public partial class POPUP_SMS : PopupBase
    {
        public POPUP_SMS()
        {
            try
            {
                InitializeComponent();
                InitForm();
                InitEvent();
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void InitForm()
        {
            PopupTitle = "SMS 도움창";
            DataTable dt = new DataTable();
            dt.Columns.Add("PH");
            aGrid1.Binding(dt);

        }

        private void InitEvent()
        {
            textEdit_rcv.KeyDown += textEdit_rcv_KeyDown;
            buttonEx_reset.Click += buttonEx_Click;
            buttonEx_send.Click += buttonEx_Click;
            buttonEx_rsv.Click += buttonEx_Click;
        }

        protected override void OnOK()
        {
            try
            {
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnCancel()
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void textEdit_rcv_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                gridView1.AddNewRow();
                gridView1.SetFocusedRowCellValue("PH", textEdit_rcv.Text);
                gridView1.UpdateCurrentRow();

                textEdit_rcv.Text = "";
                textEdit_rcv.Focus();
            }
        }

        private void buttonEx_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string qry = string.Empty;
            switch (button.Name)
            {
                case "buttonEx_reset":
                    gridView1.RefreshData();
                    break;
                case "buttonEx_send":
                    if (textBox_msg.Text.Length == 0)
                    {
                        ShowMessageBoxA("내용을 입력하세요", MessageType.Error);
                        return;
                    }
                    if (A.GetString(gridView1.GetRowCellValue(0, "PH")).Length == 0)
                    {
                        ShowMessageBoxA("번호를 입력하세요", MessageType.Error);
                        return;
                    }
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        qry = qry + "EXEC SP_SYS_SMS_SEND '" + Global.FirmCode + "', '" + Global.BizCode + "', '" +
                            A.GetString(gridView1.GetRowCellValue(i, "PH")) + "', '" + textBox_msg.Text + "', '" +
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' \r\n";
                    }
                    DBHelper.ExecuteNonQuery(qry);

                    break;
                case "buttonEx_rsv":

                    break;
            }
        }
    }
}
