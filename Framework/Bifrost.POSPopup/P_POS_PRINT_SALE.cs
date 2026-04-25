using Bifrost.Helper;
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

namespace Bifrost
{
    public partial class P_POS_PRINT_SALE : POSPopupBase
    {
        private string PrintType;
        public string ResultPrint { get; set; } = string.Empty;

        public P_POS_PRINT_SALE()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            //프린터타입 가져오기
            Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
            PrintType = cfgPrint.ConfigValue;

            DataTable dt = DBHelper.GetDataTable("SELECT TP_REPORT, NM_REPORT FROM POS_PRINT_CONFIGL WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND TP_PRINT = '" + PrintType + "' AND YN_USE = 'Y' ORDER BY NO_LINE");

            int btnCount = dt.Rows.Count;

            if (btnCount == 0) return;

            //478, 126
            
            if(PrintType == "A") //둘다
            {
                this.Size = new Size((btnCount - 2) * 126 + 100, (80 * 3) + 56);
            }    
            else if (PrintType == "P") // 일반 프린터
            {
                this.Size = new Size(478, (80 * 2) + 56);
            }

            else if (PrintType == "T") // 감열지
            {
                this.Size = new Size(478, (80 * 2) + 56);
            }



            DevExpress.XtraEditors.SimpleButton[] _btn = new DevExpress.XtraEditors.SimpleButton[btnCount];

            int btnWidth = 120;

            #region 좌측 컨트롤 패널
            flowLayoutPanelPrint.SuspendLayout();
            //콘텐츠 버튼 생성 (40개) 픽스
            for (int i = 0; i < btnCount; i++)
            {
                _btn[i] = new DevExpress.XtraEditors.SimpleButton();
                _btn[i].Name = "btnContents" + i.ToString();
                _btn[i].TabIndex = i;
                _btn[i].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                _btn[i].Appearance.Font = new Font("카이겐고딕 KR Regular", 14F);
                _btn[i].AllowFocus = false;
                _btn[i].Parent = flowLayoutPanelPrint;
                _btn[i].Text = A.GetString(dt.Rows[i]["NM_REPORT"]);
                _btn[i].Tag = A.GetString(dt.Rows[i]["TP_REPORT"]);


                //거래명세서, 미발행경우
                if (A.GetString(dt.Rows[i]["TP_REPORT"]) == "I")
                {
                    btnWidth = 372;
                    _btn[i].Appearance.BackColor = Color.FromArgb(199, 225, 239);
                }
                else if (A.GetString(dt.Rows[i]["TP_REPORT"]) == "N")
                {
                    btnWidth = 372;
                    
                }
                else
                {
                    
                    _btn[i].Appearance.BackColor = Color.FromArgb(170, 203, 239);

                    btnWidth = 120;
                }

                _btn[i].Size = new Size(btnWidth, 80);
                _btn[i].Click += BtnContents_Click;
            }
            flowLayoutPanelPrint.ResumeLayout();
            #endregion
        }

        private void BtnContents_Click(object sender, EventArgs e)
        {
            ResultPrint = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
