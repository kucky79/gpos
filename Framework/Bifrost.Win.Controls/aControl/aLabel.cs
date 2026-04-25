using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;
using System;
using DevExpress.Utils.Text;

namespace Bifrost.Win.Controls
{
    public class aLabel : LabelControl
    {

        private int FIXED_HEIGHT = 24;

        public aLabel() : base()
        {
            AutoSizeMode = LabelAutoSizeMode.None;
            //this.Height = FIXED_HEIGHT;
            LabelType = LabelType.NONE;
        }

        LabelType _LabelType = LabelType.NONE;
        public LabelType LabelType
        {
            get { return _LabelType; }
            set
            {
                _LabelType = value;
                switch (value)
                {
                    //case LabelType.NONE:
                    //    this.Font = new System.Drawing.Font("Tahoma", 9F);
                    //    this.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                    //    this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4D4D46");
                    //    break;
                    //case LabelType.SUBTITLE:
                    //    this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
                    //    this.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                    //    this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0079DF");
                    //    break;
                    //case LabelType.ETC:
                    //    this.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                    //    break;
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

    public enum LabelType
    {
        NONE = 0,
        SUBTITLE = 1,
        ETC = 99
    }
}

