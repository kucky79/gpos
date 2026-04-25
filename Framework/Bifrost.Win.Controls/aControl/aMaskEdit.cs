using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraEditors;

namespace Bifrost.Win.Controls
{
    public partial class aMaskEdit : TextEdit
    {
        //private bool isKeyProcessing = false;
        //private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit fProperties;
        //private aDateEdit aDateEdit1;
        //private const int FIXED_HEIGHT = 24;

        public aMaskEdit() : base()
        {
            //InitializeComponent();
            InitEvent();
        }


        private void InitEvent()
        {
            this.GotFocus += ATextEdit_GotFocus;
        }

        protected override void InitLayout()
        {
            this.Focus();
            base.InitLayout();

            //this.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void ATextEdit_GotFocus(object sender, EventArgs e)
        {
            //this.Select(0, 0);
            //20170627 디폴트 영문, 대문자 입력 일단 주석
            //this.ImeMode = ImeMode.Off;
            //this.Properties.CharacterCasing = CharacterCasing.Upper;
        }
        //public new bool Modified { get; set; } = false;

        //bool ShouldSerializeModified() { return Modified != false; }

        /// <summary>
        /// 정규식 사용시 구분 문자도 들어가서 나중에 뺴주기 위해 여기에 세팅
        /// </summary>
        [Category("AIMS"), Browsable(true), Description("정규식 사용시 구분문자 빼주기 위해 구분문자 저장")]
        public string MaskLiteral { get; set; } = string.Empty;
        bool ShouldSerializeMaskLiteral() { return MaskLiteral != string.Empty; }

        //protected override void OnTextChanged(EventArgs e)
        //{
        //    if (this.isKeyProcessing)
        //        this.Modified = true;
        //    base.OnTextChanged(e);
        //}

        #region Override
        public override string Text
        {
            get
            {
                string result = string.Empty;
                if (this.EditValue != null)
                {
                    if (MaskLiteral != string.Empty && MaskLiteral != "")
                        result = EditValue.ToString().Replace(MaskLiteral, "");
                    else
                        result = EditValue.ToString();
                }
                return result;
            }
            set
            {
                //this.m_modified = true;
                //this.IsModified = true;

                //if (value != null)
                //{
                //    value = value.ToString().ToUpper();
                //}

                base.Text = value;
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
        #endregion

        //private void InitializeComponent()
        //{
        //    DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
        //    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(aMaskEdit));
        //    DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
        //    this.fProperties = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
        //    this.aDateEdit1 = new Bifrost.Win.Controls.aDateEdit();
        //    ((System.ComponentModel.ISupportInitialize)(this.fProperties)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.aDateEdit1.Properties.CalendarTimeProperties)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.aDateEdit1.Properties)).BeginInit();
        //    this.SuspendLayout();
        //    // 
        //    // fProperties
        //    // 
        //    this.fProperties.Name = "fProperties";
        //    // 
        //    // aDateEdit1
        //    // 
        //    this.aDateEdit1.DateFormat = "MM\\/dd\\/yyyy";
        //    this.aDateEdit1.EditValue = "";
        //    this.aDateEdit1.Location = new System.Drawing.Point(0, 0);
        //    this.aDateEdit1.Name = "aDateEdit1";
        //    this.aDateEdit1.Properties.AutoHeight = false;
        //    editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
        //    editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
        //    this.aDateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
        //    new DevExpress.XtraEditors.Controls.EditorButton(editorButtonImageOptions1, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, null),
        //    new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions2)});
        //    this.aDateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
        //    new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
        //    this.aDateEdit1.Properties.Mask.EditMask = "MM\\/dd\\/yyyy";
        //    this.aDateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
        //    this.aDateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
        //    this.aDateEdit1.Properties.NullDate = new System.DateTime(((long)(0)));
        //    this.aDateEdit1.Size = new System.Drawing.Size(100, 20);
        //    this.aDateEdit1.TabIndex = 0;
        //    ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.aDateEdit1.Properties.CalendarTimeProperties)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.aDateEdit1.Properties)).EndInit();
        //    this.ResumeLayout(false);

        //}
    }
}
