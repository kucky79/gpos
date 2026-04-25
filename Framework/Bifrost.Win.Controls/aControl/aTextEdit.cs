using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
    public class aTextEdit : ButtonEdit
    {
        private bool m_modified = false;
        private bool isKeyProcessing = false;
        private const int FIXED_HEIGHT = 24;
        private static Image picBack = Images.txtClear;
        private SystemConfig _config = null;

        private Color _colorBorder = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));
        private Color _colorBack = Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

        static aTextEdit()
        {
            RepositoryItemaButtonEdit.RegisteraTextEdit();
        }

        public aTextEdit() : base()
        {
            RepositoryItemaButtonEdit.RegisteraTextEdit();
            InitEvent();

            if (this.Properties.Buttons.Count > 0)
            {
                this.Properties.Buttons[0].Visible = false;
            }
        }

        private void InitEvent()
        {
            this.GotFocus += ATextEdit_GotFocus;
            this.Leave += ATextEdit_Leave;
            this.ButtonClick += aTextEdit_ButtonClick;
        }

        [Category("AIMS"), DefaultValue(false)]
        public bool isUpper { get; set; }
        bool ShouldSerializeisUpper()
        {
            return isUpper != false;
        }
               

        private TextDesignType _designType;

        [Category("AIMS"), DefaultValue(TextDesignType.Default)]
        public TextDesignType DesignType
        {
            get { return _designType; }
            set { _designType = value; }
        }

        bool ShouldSerializeDesignType()
        {
            return DesignType != TextDesignType.Default;
        }

        [Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Modified
        {
            get { return this.m_modified; }
            set { this.m_modified = value; }
        }

        bool ShouldSerializeModified()
        {
            return Modified != false;
        }

        private void ATextEdit_Leave(object sender, EventArgs e)
        {
            if (this.Properties.Buttons.Count > 0)
            {
                this.Properties.Buttons[0].Visible = false;
            }
        }

        public override string EditorTypeName
        {
            get { return RepositoryItemaButtonEdit.aTextEditName; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemaButtonEdit Properties
        {
            get { return (RepositoryItemaButtonEdit)base.Properties; }
        }

        protected override void InitLayout()
        {
            this.Focus();
            base.InitLayout();

            //LookAndFeel.SkinName = "AIMS_SUB";
            //LookAndFeel.UseDefaultLookAndFeel = false;

            this.Properties.AutoHeight = false;

            if (this.Properties.Buttons.Count > 0)
            {
                this.Properties.Buttons[0].Visible = false;
            }

            //버튼이 없을경우 추가해줌
            if (this.Properties.Buttons.Count == 0)
            {
                var editorButton = new EditorButton { Kind = ButtonPredefines.Glyph };
                this.Properties.Buttons.Add(editorButton);

                //this.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                this.Properties.Buttons[0].Image = picBack;// global::Bifrost.Win.Controls.Properties.Resources.btn_calendar; //Images.btn_calendar;
                this.Properties.Buttons[0].Visible = false;
            }
        }


        private void ATextEdit_GotFocus(object sender, EventArgs e)
        {
            //20170627 디폴트 영문, 대문자 입력 일단 주석
            //this.ImeMode = ImeMode.Off;
            //if(isUpper)
            //    this.Properties.CharacterCasing = CharacterCasing.Upper;
            //else
            //    this.Properties.CharacterCasing = CharacterCasing.Normal;

            if (this.Properties.Buttons.Count > 0 && this.ReadOnly == false)
            {
                this.Properties.Buttons[0].Visible = true;
            }
        }
        

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.isKeyProcessing)
                this.Modified = true;
            base.OnTextChanged(e);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                this.m_modified = true;
                this.IsModified = true;

                if (value != null)
                {
                    value = isUpper == true ? value.ToString().ToUpper() : value.ToString();
                }

                base.Text = value;
            }
        }

   
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if (this.ReadOnly == true)
            //{
            //    this.Properties.Appearance.BackColor = _colorBack;
            //    this.Properties.Appearance.BorderColor = _colorBorder;
            //    this.Properties.Appearance.Options.UseBackColor = true;
            //    this.Properties.Appearance.Options.UseBorderColor = true;
            //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            //}
            //else
            //{
                //if (DesignType == TextDesignType.Default)
                //{
                //    this.Properties.Appearance.BackColor = System.Drawing.Color.White;
                //    this.Properties.Appearance.BorderColor = _colorBorder;
                //    this.Properties.Appearance.Options.UseBackColor = true;
                //    this.Properties.Appearance.Options.UseBorderColor = true;
                //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                //}
                //else if (DesignType == TextDesignType.ETC)
                //{
                //    this.Properties.Appearance.BorderColor = _colorBorder;
                //    this.Properties.Appearance.Options.UseBackColor = true;
                //    this.Properties.Appearance.Options.UseBorderColor = true;
                //    this.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                //}
            //}
            //Pen BorderColor = new Pen(Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(225))))));

            //RectangleF rec = e.Graphics.ClipBounds;
            //rec.Inflate(-1, -1);
            //e.Graphics.DrawRectangle(BorderColor, rec.Left, rec.Top, rec.Width, rec.Height);

            //this.BackColor = Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));

            //if (this.Size.Height != FIXED_HEIGHT)
            //    this.Size = new Size(this.Size.Width, FIXED_HEIGHT);
        }

        

        private void aTextEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.ReadOnly == false)
            {
                this.Text = string.Empty;
            }
        }

        public enum TextDesignType
        {
            Default = 0,
            ETC = 99
        }

    }
}
