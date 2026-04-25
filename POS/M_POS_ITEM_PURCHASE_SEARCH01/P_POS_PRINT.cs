using Bifrost;
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

namespace POS
{
    public partial class P_POS_PRINT : POSPopupBase
    {
        private string PrintType;
        public string ResultPrint { get; set; } = string.Empty;

        public P_POS_PRINT()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            //프린터타입 가져오기
            Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
            PrintType = cfgPrint.ConfigValue;

            int btnCount = 0;


            switch (PrintType)
            {
                case "A": //둘다
                    btnCount = 4;
                    break;
                case "T": //감열지
                    btnCount = 2;
                    break;
                case "P": //일반프린터
                    btnCount = 2;
                    break;
                default:
                    break;
            }

            this.Size = new Size((btnCount+1) * 126 + 100, this.Size.Height);

            DevExpress.XtraEditors.SimpleButton[] _btn = new DevExpress.XtraEditors.SimpleButton[btnCount + 1];

            #region 좌측 컨트롤 패널
            flowLayoutPanelPrint.SuspendLayout();

            string[] printText = { "전제 상품\n(감열지)", "선택 상품\n(감열지)", "전제 상품\n(일반)", "선택 상품\n(일반)" };
            string[] printTag = { "AT", "ST", "AP", "SP" };

            for (int i = 0; i < btnCount; i++)
            {
                _btn[i] = new DevExpress.XtraEditors.SimpleButton();
                _btn[i].Name = "btnContents" + i.ToString();
                _btn[i].Size = new Size(120, 80);
                _btn[i].TabIndex = i;
                _btn[i].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                _btn[i].Appearance.Font = new Font("카이겐고딕 KR Regular", 14F);
                _btn[i].AllowFocus = false;
                _btn[i].Parent = flowLayoutPanelPrint;
                _btn[i].Text = PrintType == "P" ? printText[i + 2] : printText[i];
                _btn[i].Tag = PrintType == "P" ? printTag[i + 2] : printTag[i];

                _btn[i].Click += BtnContents_Click;
            }

            _btn[btnCount] = new DevExpress.XtraEditors.SimpleButton();
            _btn[btnCount].Name = "btnContents" + btnCount.ToString();
            _btn[btnCount].Size = new Size(120, 80);
            _btn[btnCount].TabIndex = btnCount;
            _btn[btnCount].Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            _btn[btnCount].Appearance.Font = new Font("카이겐고딕 KR Regular", 14F);
            _btn[btnCount].AllowFocus = false;
            _btn[btnCount].Parent = flowLayoutPanelPrint;
            _btn[btnCount].Text = "취소";
            _btn[btnCount].Tag = "N";
            _btn[btnCount].Click += BtnContents_Click;
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
