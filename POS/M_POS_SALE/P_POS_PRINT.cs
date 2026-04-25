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
        public string ResultPrint { get; set; } = string.Empty;

        public P_POS_PRINT()
        {
            InitializeComponent();
            InitEvent();
        }
        private void InitEvent()
        {
            btnAll.Click += Btn_Click;
            btnReceit.Click += Btn_Click;
            btnWorkSheet.Click += Btn_Click;
            btnNone.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            ResultPrint = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
