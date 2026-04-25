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

        public P_POS_PRINT_PAY()
        {
            InitializeComponent();
            InitEvent();
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
