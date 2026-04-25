using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
    public partial class aNumericText : TextEdit
    {
        private const int FIXED_HEIGHT = 24;

        private bool m_modified = false;

        private decimal _DecimalValue = 0;
        private decimal _DecimalPoint = 0;

        [Browsable(true)]
        public event EventHandler DecimalValueChanged;

        public aNumericText()
        {
            InitializeComponent();
            Init();
        }

        public new bool Modified
        {
            get { return this.m_modified; }
            set { this.m_modified = value; }
        }

        bool ShouldSerializeModified()
        {
            return Modified != true;
        }

        protected SetNumericType _SetNumericType = SetNumericType.NONE;

        [Browsable(true), Description("올림, 반올림, 내림을 설정합니다.")]
        public SetNumericType SetNumericType
        {
            get
            {
                return this._SetNumericType;
            }
            set
            {

                this._SetNumericType = value;

                decimal TmpValue = this.DecimalValue;
                if (this._SetNumericType == SetNumericType.ROUNDUP)
                {
                    TmpValue = Math.Round(this.DecimalValue, 2, MidpointRounding.AwayFromZero);
                }
                else if (this._SetNumericType == SetNumericType.ROUNDDOWN)
                {

                }
                else if (this._SetNumericType == SetNumericType.CEIL)
                {

                }
                else if (this._SetNumericType == SetNumericType.FLOOR)
                {

                }

                this.DecimalValue = TmpValue;
            }
        }

        bool ShouldSerializeSetNumericType()
        {
            return SetNumericType != SetNumericType.NONE;
        }

        [Browsable(true)]
        public decimal DecimalValue
        {
            get
            {
                _DecimalValue = decimal.Parse(this.Text.ToString());
                return _DecimalValue;
            }
            set
            {
                this.EditValue = decimal.Parse(value.ToString());
                this._DecimalValue = decimal.Parse(value.ToString());
                this.DecimalValueChanged?.Invoke(this, null);
                this.Modified = true;
                this.IsModified = true;
                this.DoValidate();
            }
        }

        [Browsable(true)]
        public decimal DecimalPoint
        {
            get { return _DecimalPoint; }
            set { _DecimalPoint = value; }
        }

        public override object EditValue
        {
            get
            {
                return base.EditValue;
            }

            set
            {
                base.EditValue = value;
                this.DecimalValueChanged?.Invoke(this, null);
                this.IsModified = true;
                //this.DoValidate();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.SelectAll();
            base.OnGotFocus(e);
        }

        private void Init()
        {
            this.Properties.NullText = "0";
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            this.Properties.AutoHeight = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //this.Properties.AutoHeight = false;

            //this.RightToLeft = RightToLeft.No;

            //this.Properties.Appearance.Options.UseTextOptions = true;
            //this.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //this.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //this.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

            //string setDecimalPotint = "n" + _DecimalPoint.ToString();
            //this.Properties.Mask.EditMask = setDecimalPotint;
            //this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //this.Properties.Mask.UseMaskAsDisplayFormat = true;

            //if (this.ReadOnly == true)
            //{
            //    this.Properties.Appearance.BackColor = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            //    this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            //    this.Properties.Appearance.Options.UseBackColor = true;
            //    this.Properties.Appearance.Options.UseBorderColor = true;
            //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //}
            //else
            //{
            //    this.Properties.Appearance.BackColor = System.Drawing.Color.White;
            //    this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            //    this.Properties.Appearance.Options.UseBackColor = true;
            //    this.Properties.Appearance.Options.UseBorderColor = true;
            //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //}

            //if (this.Size.Height != FIXED_HEIGHT)
            //    this.Size = new Size(this.Size.Width, FIXED_HEIGHT);
        }
    }
}
