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
    public partial class P_SORT : POSPopupBase
    {
        private string _resultStr = string.Empty;

        public P_SORT()
        {
            PopupTitle = "상품 위치 도움창";
            InitializeComponent();
            InitEvent();

            txtSeq.SelectAll();
        }

        private void InitEvent()
        {
            btnDone.Click += BtnDone_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            ResultQty = A.GetInt(txtSeq.DecimalValue);
            this.DialogResult = DialogResult.OK;
        }

        public int ResultQty { get; set; }

    }
}
