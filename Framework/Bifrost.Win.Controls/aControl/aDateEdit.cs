using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Globalization;
using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;

namespace Bifrost.Win.Controls
{
    [ToolboxItem(true)]
    public class aDateEdit : DateEdit
    {

        private string _format = @"yyyy\-MM\-dd";
        private string _outFormat = @"yyyyMMdd";

        DateTime _today = DateTime.Now;
        DateTime _minDay = DateTime.MinValue;   
        private const int FIXED_HEIGHT = 24;
        private static Image picBack = Images.btn_calendar;
        private static Image picBack_disable = Images.btn_calendar_disable;
        private static Image picClear = Images.txtClear;

        static aDateEdit()
        {
            RepositoryItemaDateEdit.RegisteraDataEdit();
        }


        public aDateEdit()
        {
            this.DateTimeChanged += dateEdit_DateTimeChanged;
            this.EditValueChanged += dateEdit_EditValueChanged;
            this.Properties.ShowClear = true;

            this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.Properties.Mask.EditMask = _format;
            this.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.Text = string.Empty;
            this.EditValue = null;
            InitEvent();
        }

        private void InitEvent()
        {
            //this.GotFocus += ATextEdit_GotFocus;
            this.ButtonClick += aTextEdit_ButtonClick;
            this.Properties.Leave += Properties_Leave;
        }

        private void aTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index != 0)
            {
                if (this.ReadOnly == false)
                {
                    this.Text = string.Empty;
                }
            }
        }

        private void Properties_Leave(object sender, EventArgs e)
        {
            if (this.Properties.Buttons.Count > 1)
            {
                this.Properties.Buttons[1].Visible = false;
            }
        }

        private void ATextEdit_GotFocus(object sender, EventArgs e)
        {
            if (this.Properties.Buttons.Count > 1)
            {
                this.Properties.Buttons[1].Visible = true;
            }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public new RepositoryItemaDateEdit Properties
        //{
        //    get
        //    {
        //        return base.Properties as RepositoryItemaDateEdit;
        //    }
        //}

        //public override string EditorTypeName
        //{
        //    get
        //    {
        //        return RepositoryItemaDateEdit.CustomEditName;
        //    }
        //}

        protected override PopupBaseForm CreatePopupForm()
        {
            return new aDateEditPopupForm(this);
        }

        private void dateEdit_DateTimeChanged(object sender, System.EventArgs e)
        {
            this.DateTimeChanged -= dateEdit_DateTimeChanged;
            //this.EditValueChanged -= dateEdit_EditValueChanged;
            if (this.DateTime.ToString(_outFormat) != _minDay.ToString(_outFormat))
            {
                this.EditValue = this.DateTime.ToString(_format);
                this.Text = this.DateTime.ToString(_format);
            }
            else
            {
                this.Text = string.Empty;
                this.EditValue = null;
                this.Properties.NullDate = DateTime.MinValue;
                this.Properties.NullText = string.Empty;
            }
            this.DateTimeChanged += dateEdit_DateTimeChanged;
            //this.EditValueChanged += dateEdit_EditValueChanged;
        }
        private void dateEdit_EditValueChanged(object sender, System.EventArgs e)
        {
            //this.DateTimeChanged -= dateEdit_DateTimeChanged;
            this.EditValueChanged -= dateEdit_EditValueChanged;
            if (this.DateTime.ToString(_outFormat) != _minDay.ToString(_outFormat))
            {
                this.Text = this.DateTime.ToString(_format);
            }
            else
            {
                this.Text = string.Empty;
                this.Properties.NullDate = DateTime.MinValue;
                this.Properties.NullText = string.Empty;
            }
            //this.DateTimeChanged += dateEdit_DateTimeChanged;
            this.EditValueChanged += dateEdit_EditValueChanged;
        }

        protected override void InitLayout()
        {
            this.Properties.NullDate = DateTime.MinValue;
            this.Properties.NullText = string.Empty;
            base.InitLayout();
        }

        public string DateFormat
        {
            get { return _format; }
            set {
                _format = value;
                this.Properties.DisplayFormat.FormatString = _format;
                this.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.Properties.EditFormat.FormatString = _format;
                this.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                this.Properties.Mask.EditMask = _format;
            }
        }
         
        public override string Text
        {
            get
            {
                string strResult = string.Empty;
                if (base.Text != string.Empty)
                {
                    strResult = this.DateTime.ToString(_outFormat);
                }

                return strResult;
            }
            set
            {
                string resultString = string.Empty;
                if (value != string.Empty)
                {
                    if (value != "        ")
                    {
                        DateTime setText = new DateTime();
                        //yyyyMMdd일 경우는 
                        if (value.Length == 8)
                        {
                            setText = DateTime.ParseExact(value, _outFormat, null);
                        }
                        //그외에 구분자(-,/,.공백 등)은 아래 메서드 이용
                        else
                        {
                            DateTime.TryParse(value, null, DateTimeStyles.AssumeLocal, out setText);        
                        }
                        resultString = setText.ToString(_format);
                    }
                }
                else
                {
                    resultString = value;
                }
                base.Text = resultString;
                this.IsModified = true;
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            this.Properties.AutoHeight = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if (this.Properties.ReadOnly == true)
            //{
            //    this.Properties.Appearance.BackColor = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            //    this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            //    this.Properties.Appearance.Options.UseBackColor = true;
            //    this.Properties.Appearance.Options.UseBorderColor = true;
            //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

            //    if (Properties.Buttons.Count > 0)
            //    {
            //        this.Properties.Buttons[0].Image = picBack_disable;
            //        this.Properties.Buttons[0].Appearance.BackColor = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            //        this.Properties.Buttons[0].Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            //        this.Properties.Buttons[0].Appearance.Options.UseBackColor = true;
            //    }
            //}
            //else
            //{
            //    this.Properties.Appearance.BackColor = System.Drawing.Color.White;
            //    this.Properties.Appearance.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
            //    this.Properties.Appearance.Options.UseBackColor = true;
            //    this.Properties.Appearance.Options.UseBorderColor = true;
            //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //    if (Properties.Buttons.Count > 0)
            //    {
            //        this.Properties.Buttons[0].Image = picBack;
            //        this.Properties.Buttons[0].Appearance.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            //        this.Properties.Buttons[0].Appearance.BackColor2 = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            //        this.Properties.Buttons[0].Appearance.Options.UseBackColor = true;
            //    }
            //}
            //if (this.Size.Height != FIXED_HEIGHT)
            //    this.Size = new Size(this.Size.Width, FIXED_HEIGHT);
        }


    }

    public class aDateEditViewInfo : DateEditViewInfo
    {
        public aDateEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class aDateEditPainter : ButtonEditPainter
    {
        public aDateEditPainter()
        {
        }
    }

    public class aDateEditPopupForm : PopupDateEditForm
    {
        public aDateEditPopupForm(aDateEdit ownerEdit) : base(ownerEdit)
        {
        }
    }

}
