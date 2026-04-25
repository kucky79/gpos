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
    public partial class P_SEARCH_INITIAL : POSPopupBase
    {
        private string _resultStr = string.Empty;

        public ContentsMode ContentsType 
        {
            get; set;
        }

        public P_SEARCH_INITIAL()
        {
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            btn00.Click += Btn_Click;
            btn01.Click += Btn_Click;
            btn02.Click += Btn_Click;
            btn03.Click += Btn_Click;
            btn04.Click += Btn_Click;
            btn05.Click += Btn_Click;
            btn06.Click += Btn_Click;
            btn07.Click += Btn_Click;
            btn08.Click += Btn_Click;
            btn09.Click += Btn_Click;

            btnA.Click += Btn_Click;
            btnB.Click += Btn_Click;
            btnC.Click += Btn_Click;
            btnD.Click += Btn_Click;
            btnE.Click += Btn_Click;
            btnF.Click += Btn_Click;
            btnG.Click += Btn_Click;
            btnH.Click += Btn_Click;
            btnI.Click += Btn_Click;
            btnJ.Click += Btn_Click;
            btnK.Click += Btn_Click;
            btnL.Click += Btn_Click;
            btnM.Click += Btn_Click;
            btnN.Click += Btn_Click;
            btnO.Click += Btn_Click;
            btnP.Click += Btn_Click;
            btnQ.Click += Btn_Click;
            btnR.Click += Btn_Click;
            btnS.Click += Btn_Click;

            btnAll.Click += Btn_Click;
            btnETC.Click += Btn_Click;
            btnDone.Click += BtnDone_Click; ;

            this.Load += P_SEARCH_INITIAL_Load;
            this.Activated += P_SEARCH_INITIAL_Activated;
        }

        int InitialCnt = 1;
        int ClickCnt = 0;


        private void P_SEARCH_INITIAL_Activated(object sender, EventArgs e)
        {
            //프린터포트
            POSConfig configInitialCnt = POSConfigHelper.GetConfig("POS005");
            InitialCnt = A.GetInt(configInitialCnt.ConfigValue);
        }

        private void P_SEARCH_INITIAL_Load(object sender, EventArgs e)
        {
            if (ContentsType == ContentsMode.Customer)
                PopupTitle = "고객 검색";
            else if (ContentsType == ContentsMode.Item)
                PopupTitle = "상품 검색";
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public string ResultText { get; set; }


        private void Btn_Click(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.SimpleButton)sender).Name == nameof(btnETC))
            {
                ResultText = "EN";
                this.DialogResult = DialogResult.OK;
            }
            else if (((DevExpress.XtraEditors.SimpleButton)sender).Name == nameof(btnAll))
            {
                ResultText = "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ResultText += ((DevExpress.XtraEditors.SimpleButton)sender).Text;
                ClickCnt += 1;
            }

            lblInitial.Text = ResultText;

            if (ClickCnt == InitialCnt)
                this.DialogResult = DialogResult.OK;
        }
    }
}
