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
    public partial class P_POS_PRINT_PAY : POSPopupBase
    {
        public string ResultPrint { get; set; } = string.Empty;
        private string PrintType;

        public string[] PrintText { get; set; }
        public string[] PrintTag { get; set; }


        public P_POS_PRINT_PAY()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        private void InitForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            //프린터타입 가져오기
            Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
            PrintType = cfgPrint.ConfigValue;

            int btnCount = PrintText.Length;

            this.Size = new Size((btnCount + 1) * 126 + 100, this.Size.Height);

            DevExpress.XtraEditors.SimpleButton[] _btn = new DevExpress.XtraEditors.SimpleButton[btnCount + 1];

            #region 좌측 컨트롤 패널
            flowLayoutPanelPrint.SuspendLayout();

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
                _btn[i].Text = PrintText[i];
                _btn[i].Tag = PrintTag[i];

                _btn[i].Click += Btn_Click;
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
            _btn[btnCount].Click += Btn_Click;
            flowLayoutPanelPrint.ResumeLayout();
            #endregion
        }


        private void InitEvent()
        {
            btnReceit.Click += Btn_Click;
            btnNone.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            ResultPrint = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
