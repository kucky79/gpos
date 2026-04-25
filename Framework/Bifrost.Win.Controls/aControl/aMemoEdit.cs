using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
    public partial class aMemoEdit : MemoEdit
    {
        private bool m_modified = false;
        private bool isKeyProcessing = false;

        public aMemoEdit()
        {
            InitializeComponent();
            InitEvent();
        }
        private void InitEvent()
        {
        }

        public new bool Modified
        {
            get { return this.m_modified; }
            set { this.m_modified = value; }
        }

        private bool _isUpper = true;
        public bool isUpper
        {
            get { return _isUpper; }
            set { _isUpper = value; }
        }


        private ScrollBars _SetScrollbar = ScrollBars.Vertical;
        public ScrollBars SetScrollbar
        {
            get { return _SetScrollbar; }
            set
            {
                _SetScrollbar = value;
                this.Properties.ScrollBars = value;
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            if (isKeyProcessing)
                this.m_modified = true;

            base.OnTextChanged(e);
        }

        public override object EditValue
        {
            get { return base.EditValue; }
            set
            {
                this.IsModified = true;
                base.EditValue = value;
            }
        }

        public override string Text
        {
            get { return base.Text; }

            set
            {
                this.IsModified = true;
                base.Text = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            base.OnPaint(e);
            
        }

    }
}
