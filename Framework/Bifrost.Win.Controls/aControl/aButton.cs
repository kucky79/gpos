using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bifrost.Common;
using System.Runtime.InteropServices;

namespace Bifrost.Win.Controls
{
    #region aButton

    /// <summary>
    /// Summary description for UserControl1.
    /// </summary>
    [DefaultEvent("Click"), DefaultProperty("Resource")]


    [ToolboxItem(true)]
    public class aButton : UserControl//, IXControl
	{
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private FormSettings Settings = new FormSettings(SubSystemType.FRAMEWORK);

        private const int FIXED_BUTTON_SIZE = 24;

        private Color _TempBackColor = Color.LightGray;
		private bool _HasFocus;
		private bool _Pressed;
		private Rectangle _HoverRectangle;
        private ResManager resManager = new ResManager();

        Color _BackColorDefault = new Color();// System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        Color _BackColorHOver = new Color();//System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
        Color _BorderColorDefalut = System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
        Color _BorderColorHOver = new Color();//System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
        Color _BorderColorDel = new Color();//
        Color _BorderColorAdd = new Color();//

        Color _FontColorDefault = System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
        Color _FontColorHOver = new Color();//System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        Color _None = new Color();// System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        private static Image img_btn_enable = Images.btn_enabled;
        private static Image img_btn_disable = Images.btn_disabled;
        private static Image img_btn_hover = Images.btn_hover;
        private static Image img_btn_ref = Images.btn_Ref_default;
        private static Image img_btn_ref_disable = Images.btn_Ref_disable;
        private static Image img_btn_ref_hover = Images.btn_Ref_HOver;
        private static Image img_btn_add = Images.btn_Add_default;
        private static Image img_btn_add_disable = Images.btn_Add_disable;
        private static Image img_btn_add_hover = Images.btn_Add_HOver;
        private static Image img_btn_del = Images.btn_Del_default;
        private static Image img_btn_del_disable = Images.btn_Del_disable;
        private static Image img_btn_del_hover = Images.btn_Del_HOver;
        private static Image img_btn_etc = Images.btn_etc;
        private static Image img_btn_etc_hover = Images.btn_etc_HOver;
        private static Image img_btn_2depth = Images.btn_2depth;
        private static Image img_btn_2depth_hover = Images.btn_2depth_HOver;

        public delegate void SelectedChangedEventHandler(object sender, SelectedChangedEventArgs e);
        public event SelectedChangedEventHandler SelectedChanged;
		public void OnSelectedChanged(SelectedChangedEventArgs e)
		{
            SelectedChanged?.Invoke(this, e);
        }

        public delegate void PaintEventHandler(object sender, PaintEventArgs e);
        public event PaintEventHandler PaintChanged;

        #region Properties
        private string _Resource = string.Empty;
        [DefaultValue(typeof(String), ""), Description("리소스를 입력합니다.")]
        public string Resource
        {
            get { return _Resource; }
            set { _Resource = value; }

            //    if (value == "")
            //    {
            //        _Resource = string.Empty;
            //        this.Text = "aButton";
            //    }
            //    else if (value.Substring(0, 1).ToUpper() == "R")
            //    {
            //        _Resource = value.ToUpper();
            //        this.Text = resManager.GetString(value.ToUpper());
            //    }
            //    else
            //    {
            //        MessageBox.Show("Resource Code 형식에 맞지 않습니다.", "속성오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;

        [DefaultValue(typeof(ContentAlignment), "ContentAlignment.MiddleCenter")]
        public ContentAlignment TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; Invalidate(); }
        }

        private ContentAlignment _ImageAlign = ContentAlignment.MiddleLeft;

        [DefaultValue(typeof(ContentAlignment), "ContentAlignment.MiddleLeft")]
        public ContentAlignment ImageAlign
        {
            get { return _ImageAlign; }
            set { _ImageAlign = value; Invalidate(); }
        }

        private Color _BackColor = Color.Empty;// Settings.ButtonBackColor;
        [DefaultValue(typeof(Color), "Color.Empty")]
        public override Color BackColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }

        [DefaultValue(typeof(Color), "Color.Empty")]
        public Color BorderColor { get; set; } = Color.Empty;


        private Color _ForeColor = Color.Empty;// Settings.ButtonBackColor;
        [DefaultValue(typeof(Color), "Color.Empty")]
        public override Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        Color _HoverColor = Color.Transparent;
        [DefaultValue(typeof(Color), "Color.Empty")]
        public Color HoverColor
        {
            get { return _HoverColor; }
            set { _HoverColor = value; Invalidate(); }
        }

        private Color _SelectedColor = Color.LightGray;

        [DefaultValue(typeof(Color), "Color.LightGray")]
        public Color SelectedColor
        {
            get { return _SelectedColor; }
            set { _SelectedColor = value; Invalidate(); }
        }

        private bool _Selected = false;
        public bool Selected
        {
            get { return _Selected; }
            set
            {
                if (value && !Selectable) return;
                _Selected = value;
                if (!value)
                    _HasFocus = false;
                Invalidate();
                if (value)
                {
                    PrivateUnSelectOtherButtons();
                    //OnSelectedChanged(new SelectedChangedEventArgs(this));
                }
            }
        }

        private bool _UnSelectOtherButtons = false;
        [DefaultValue(typeof(Boolean), "true")]
        public bool UnSelectOtherButtons
        {
            get { return _UnSelectOtherButtons; }
            set
            {
                _UnSelectOtherButtons = value;
                if (this.Selected)
                    if (value)
                        PrivateUnSelectOtherButtons();
            }
        }

        [Browsable(true), Description("Text that appears in the Button."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override String Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }

        private System.Drawing.Image _Image;

        [DefaultValue(typeof(Image), "null"), Description("Image that is shown when not mouse over.")]
        public System.Drawing.Image Image
        {
            get { return _Image; }
            set { _Image = value; Invalidate(); }
        }

        private System.Drawing.Image _HoverImage;

        [DefaultValue(typeof(Image), "null"), Description("Hover image that is shown when the mouse is over the control.")]
        public System.Drawing.Image HoverImage
        {
            get { return _HoverImage; }
            set { _HoverImage = value; Invalidate(); }
        }

        private System.Drawing.Image _DisabledImage;

        [DefaultValue(typeof(Image), "null"), Description("Disabled image that is shown when the button is disabled.")]
        public System.Drawing.Image DisabledImage
        {
            get { return _DisabledImage; }
            set { _DisabledImage = value; Invalidate(); }
        }


        ButtonDesignType _ButtonDesignType = ButtonDesignType.NONE;
        public ButtonDesignType ButtonDesignType
        {
            get { return _ButtonDesignType; }
            set
            {
                _ButtonDesignType = value;
                UseDefaultImages = true;
                //PaintChanged?.Invoke(this, null);
            }
        }
        private bool _UseDefaultImages = true;

        /// <summary>
        /// Images, Bifrost.Common.Images.btn_enabled, btn_disabled, btn_hover
        /// </summary>
        public bool UseDefaultImages
        {
            get { return _UseDefaultImages; }
            set
            {
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);

                _UseDefaultImages = value;
                if (value)
                {
                    switch (this.ButtonDesignType)
                    {
                        case ButtonDesignType.NONE:
                            this.ImageAlign = ContentAlignment.MiddleRight;
                            this.TextAlign = ContentAlignment.MiddleLeft;

                            this._HoverImage = img_btn_hover;// Images.btn_hover;
                            this._Image = img_btn_enable;//Images.btn_enabled;
                            this._DisabledImage = img_btn_disable;// Images.btn_disabled;
                            this._HoverForeColor = _FontColorHOver;
                            this.BorderStyle = BorderStyle.None;
                            this.BorderColor = _BorderColorDefalut;
                            this.HoverColor = _BackColorHOver;
                            this.BackColor = _BackColorDefault;
                            this.ForeColor = _FontColorDefault;
                            break;
                        case ButtonDesignType.ADD:
                            this.ImageAlign = ContentAlignment.MiddleCenter;
                            this.BorderColor = System.Drawing.ColorTranslator.FromHtml("#0079DF"); ;
                            this.BackColor = Color.White;

                            this._HoverImage = img_btn_add_hover;// Images.btn_Add_HOver;
                            this._Image = img_btn_add;// Images.btn_Add_default;
                            this._DisabledImage = img_btn_add_disable;// Images.btn_Add_disable;
                            this._HoverForeColor = _None;
                            this.Text = string.Empty;
                            this.Height = FIXED_BUTTON_SIZE;
                            this.Width = FIXED_BUTTON_SIZE;
                            this.HoverColor = System.Drawing.ColorTranslator.FromHtml("#0079DF");

                            this.BorderStyle = BorderStyle.None;
                            //this.BorderColor = _BorderColorAdd;
                            break;
                        case ButtonDesignType.DELETE:
                            this.ImageAlign = ContentAlignment.MiddleCenter;
                            this.BorderColor = System.Drawing.ColorTranslator.FromHtml("#17C2B8");
                            this.BackColor = Color.White;

                            this._HoverImage = img_btn_del_hover;// mages.btn_Del_HOver;
                            this._Image = img_btn_del;// Images.btn_Del_default;
                            this._DisabledImage = img_btn_del_disable;// Images.btn_Del_disable;
                            this._HoverForeColor = _None;
                            this.Text = string.Empty;
                            this.Height = FIXED_BUTTON_SIZE;
                            this.Width = FIXED_BUTTON_SIZE;
                            this.HoverColor = System.Drawing.ColorTranslator.FromHtml("#17C2B8");

                            this.BorderStyle = BorderStyle.None;
                            //this.BorderColor = _BorderColorDel;
                            break;
                        case ButtonDesignType.REFERENCE:
                            this.ImageAlign = ContentAlignment.MiddleCenter;
                            this.BorderColor = Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(212)))), ((int)(((byte)(228)))));

                            this._HoverImage = img_btn_ref_hover;// Images.btn_Ref_HOver;
                            this._Image = img_btn_ref;// Images.btn_Ref_default;
                            this._DisabledImage = img_btn_ref_disable;// Images.btn_Ref_disable;
                            this._HoverForeColor = _None;
                            this.HoverColor = Color.Transparent;
                            this.Text = string.Empty;
                            this.Height = FIXED_BUTTON_SIZE;
                            this.Width = FIXED_BUTTON_SIZE;
                            this.BorderStyle = BorderStyle.None;
                            //this.BorderColor = _BorderColorDefalut;
                            break;

                        case ButtonDesignType.TOP_LV1:
                            this.ImageAlign = ContentAlignment.MiddleRight;
                            this.TextAlign = ContentAlignment.MiddleLeft;


                            this._HoverImage = img_btn_hover;// Images.btn_hover;
                            this._Image = img_btn_enable;//Images.btn_enabled;
                            this._DisabledImage = img_btn_disable;// Images.btn_disabled;

                            this._HoverForeColor = _FontColorHOver;
                            this.BorderStyle = BorderStyle.None;
                            this.BorderColor = _BorderColorDefalut;
                            this.HoverColor = _BackColorHOver;
                            this.BackColor = _BackColorDefault;
                            this.ForeColor = _FontColorDefault;
                            break;

                        case ButtonDesignType.TOP_LV2:
                            this.ImageAlign = ContentAlignment.MiddleRight;
                            this.TextAlign = ContentAlignment.MiddleLeft;

                            this._HoverImage = img_btn_2depth_hover;// Images.btn_2depth_HOver;
                            this._Image = img_btn_2depth;// Images.btn_2depth;
                            this._DisabledImage = Images.btn_disabled;
                            this._HoverForeColor = _FontColorHOver;
                            this.BorderStyle = BorderStyle.None;
                            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            this.BorderColor = System.Drawing.ColorTranslator.FromHtml("#AC86BB");
                            this.HoverColor = System.Drawing.ColorTranslator.FromHtml("#AC86BB");
                            this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#AC86BB");
                            break;

                        case ButtonDesignType.ADD_LABEL:
                            this.ImageAlign = ContentAlignment.MiddleRight;

                            this._HoverImage = img_btn_add_hover;// Images.btn_Add_HOver;
                            this._Image = img_btn_add;// Images.btn_Add_default;
                            this._DisabledImage = img_btn_add_disable;// Images.btn_Add_disable;
                            this._HoverForeColor = _None;
                            this.Height = FIXED_BUTTON_SIZE;
                            this.BorderStyle = BorderStyle.None;
                            this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0079DF");
                            this.HoverForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            this.BorderColor = System.Drawing.ColorTranslator.FromHtml("#0079DF");
                            this.HoverColor = System.Drawing.ColorTranslator.FromHtml("#0079DF");

                            this.TextAlign = ContentAlignment.MiddleLeft;

                            break;

                        case ButtonDesignType.DEL_LABEL:
                            this.ImageAlign = ContentAlignment.MiddleRight;

                            this._HoverImage = img_btn_del_hover;// Images.btn_Del_HOver;
                            this._Image = img_btn_del;// Images.btn_Del_default;
                            this._DisabledImage = img_btn_del_disable;// Images.btn_Del_disable;
                            this._HoverForeColor = _None;
                            this.Height = FIXED_BUTTON_SIZE;
                            this.BorderStyle = BorderStyle.None;
                            this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#17C2B8");
                            this.HoverForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            this.BorderColor = System.Drawing.ColorTranslator.FromHtml("#17C2B8");
                            this.HoverColor = System.Drawing.ColorTranslator.FromHtml("#17C2B8");
                            this.TextAlign = ContentAlignment.MiddleLeft;
                            break;

                        case ButtonDesignType.ETC:
                            this.ImageAlign = ContentAlignment.MiddleRight;

                            this._HoverImage = img_btn_etc_hover;// Images.btn_etc_HOver;
                            this._Image = img_btn_etc;// Images.btn_etc;
                            this._DisabledImage = Images.btn_disabled;
                            this._HoverForeColor = _None;
                            this.Height = FIXED_BUTTON_SIZE;
                            this.BorderStyle = BorderStyle.None;
                            this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5D626D");
                            this.HoverForeColor = System.Drawing.ColorTranslator.FromHtml("#0079DF");

                            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            this.BorderColor = System.Drawing.ColorTranslator.FromHtml("#D4DDE0");
                            this.HoverColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            this.TextAlign = ContentAlignment.MiddleLeft;
                            break;

                        case ButtonDesignType.POP_SEARCH:

                            this._HoverImage = null;
                            this._Image = null;
                            //this._DisabledImage = null;
                            this._HoverForeColor = _None;
                            this.Height = FIXED_BUTTON_SIZE;
                            this.Width = 100;
                            this.BorderStyle = BorderStyle.None;
                            this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                            this.HoverForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#00CDAC");
                            this.BorderColor = Color.Transparent;// System.Drawing.ColorTranslator.FromHtml("#00CDAC");
                            this.HoverColor = System.Drawing.ColorTranslator.FromHtml("#00AB8B");
                            this.TextAlign = ContentAlignment.MiddleCenter;
                            break;
                    }

                    PaintChanged?.Invoke(this, null);
                }

            }
        }

        private String _GroupKey;

        [DefaultValue(typeof(String), ""), Description("Enter a value to group like-buttons together, this will affect the unselect behavior.")]
        public String GroupKey
        {
            get { return _GroupKey; }
            set { _GroupKey = value; }
        }

        private bool _Border = true;

        [DefaultValue(typeof(Boolean), "true"), Description("Draw the border around the control.")]
        public bool Border
        {
            get { return _Border; }
            set { _Border = value; Invalidate(); }
        }

        private Color _HoverForeColor = Color.Empty;

        [DefaultValue(typeof(Color), "Color.Empty"), Description("When mouse hovers, this will be the ForeColor for the Font if set.")]
        public Color HoverForeColor
        {
            get { return _HoverForeColor; }
            set { _HoverForeColor = value; }
        }

        private bool _Selectable;

        [DefaultValue(typeof(Boolean), "false"), Description("Can this button be selected.")]
        public bool Selectable
        {
            get { return _Selectable; }
            set { _Selectable = value; }
        }

        #endregion

        #region Component Designer generated code

        public aButton() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
            _None = Color.Transparent;

            _BorderColorDel = System.Drawing.ColorTranslator.FromHtml("#17C2B8");
            _BorderColorAdd = System.Drawing.ColorTranslator.FromHtml("#0079DF");

            _BackColorDefault = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            _BackColorHOver = System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
            _BorderColorDefalut = System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
            _BorderColorHOver = System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
            _FontColorDefault = System.Drawing.ColorTranslator.FromHtml("#4CA3DE");
            _FontColorHOver = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

            // TODO: Add any initialization after the InitComponent call
            this.SetStyle(ControlStyles.ResizeRedraw, true);

			// This supports mouse movement such as the mouse wheel
			this.SetStyle(ControlStyles.UserMouse, true);

			// This allows the control to be transparent
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			// This helps with drawing the control so that it doesn't flicker
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

			// This updates the styles
			this.UpdateStyles();

            //' Sets the hover time to 30 milliseconds. This is used
            //' to trigger the control to change the backcolor back to the
            //' original value due to the mouse no longer being over
            //' the control. 
            //'
            //' The time is used because it's more reliable than using the
            //' MouseLeave or MouseEnter events, which depending on the speed
            //' of the mouse, may or may not get fired.
            //HoverTimer = new System.Timers.Timer(30);
            //HoverTimer.Elapsed += new System.Timers.ElapsedEventHandler(HoverTimer_Elapsed);

            this.ButtonDesignType = ButtonDesignType.NONE;
            this.UseDefaultImages = true;

            this.MouseMove += new MouseEventHandler(aButton_MouseMove);
			this.MouseLeave += new EventHandler(aButton_MouseLeave);
			this.MouseDown += new MouseEventHandler(aButton_MouseDown);
			this.MouseUp += new MouseEventHandler(aButton_MouseUp);
			this.Click += new EventHandler(aButton_Click);
		}

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // aButton
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BorderColor = _BorderColorDefalut;
            this.ForeColor = _FontColorDefault;//_ForeColor;
            //this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(163)))), ((int)(((byte)(222)))));
            this.Name = "aButton";
            this.Size = new System.Drawing.Size(172, 35);


            this.ResumeLayout(false);
        }


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion

		#region Overriding

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
		{
			base.OnPaint(pe);

            if (this.Size.Height != FIXED_BUTTON_SIZE)
                this.Size = new Size(this.Size.Width, FIXED_BUTTON_SIZE);

            //this.BackColor = _BackColorDefault;
            //this.ForeColor = _FontColorDefault;
            //this.BorderColor = _BorderColorDefalut;


            Graphics g = pe.Graphics;
			bool DrawHover = false;
			_HoverRectangle = new Rectangle(0, 0, this.Width, this.Height);

            //' The mouse is hovering over the control
            if (_HasFocus)
            {
                if (this.Selected)
                {
                    Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
                    SolidBrush b = new SolidBrush(this.SelectedColor);
                    g.FillRectangle(b, rec);
                }
                else
                {
                    //' Make sure the color is not Nothing, or Empty
                    //' which is the equivalent to nothing for a color.
                    if (!HoverColor.Equals(Color.Empty))
                    {
                        Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
                        SolidBrush b = new SolidBrush(this.HoverColor);
                        g.FillRectangle(b, rec);
                        //this.ForeColor = _FontColorHOver;
                    }
                }
            }
            else
            {
                //this.ForeColor = _ForeColor;
            }

            #region Draw the Hover image 
            if (!this.Selected)
			{
                DrawHover = true;
            }
			else
			{
				if(this.Selected)
				{
					Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
					SolidBrush b = new SolidBrush(this.SelectedColor);
					g.FillRectangle(b, rec);
				}            
			}
            #endregion

            Point ptImage;
			//' Used to hold the dimensions of the current image
			int iWidth = 0; int iHeight = 0;
			if(DrawHover)
			{
				if(this.HoverImage != null)
				{
					iWidth = this.HoverImage.Width;
					iHeight = this.HoverImage.Height;
				}
				else
				{
					if(this.Image != null)
					{
						iWidth = this.Image.Width;
						iHeight = this.Image.Height;
					}
				}
            }
			else
			{
				if(this.Image != null)
				{
					iWidth = this.Image.Width;
					iHeight = this.Image.Height;
				}
            }

			//' Make sure we even need to draw an image to begin
			//' with. There may not be any images in this control.
			if(iHeight != 0)
			{
                int Depth_Padding = 0;

                switch (this.ButtonDesignType)
                {
                    case ButtonDesignType.DEL_LABEL:
                        Depth_Padding = 5;
                        break;
                    case ButtonDesignType.ADD_LABEL:
                        Depth_Padding = 5;
                        break;
                    default:
                        Depth_Padding = 10;
                        break;
                }
                //' Get the position of the image
                switch (ImageAlign)
				{
					case ContentAlignment.BottomCenter:
						ptImage = new Point(Convert.ToInt32((this.Width / 2) - (iWidth / 2)), (this.Height - iHeight) - 3);
						break;
					case ContentAlignment.BottomLeft:
						ptImage = new Point(5, (this.Height - iHeight) - 3);
						break;
					case ContentAlignment.BottomRight:
						ptImage = new Point((this.Width - iWidth), (this.Height - iHeight) - 3);
						break;
					case ContentAlignment.MiddleCenter:
						ptImage = new Point(Convert.ToInt32((this.Width / 2) - (iWidth / 2)), Convert.ToInt32((this.Height / 2) - (iHeight / 2)));
						break;
					case ContentAlignment.MiddleLeft:
						ptImage = new Point(5, Convert.ToInt32((this.Height / 2) - (iHeight / 2)));
						break;
					case ContentAlignment.MiddleRight:
						ptImage = new Point((this.Width - iWidth), Convert.ToInt32((this.Height / 2) - (iHeight / 2)));
						break;
					case ContentAlignment.TopCenter:
						ptImage = new Point(Convert.ToInt32((this.Width / 2) - (iWidth / 2)), 5);
						break;
					case ContentAlignment.TopLeft:
						ptImage = new Point(5, 5);
						break;
					default:
						ptImage = new Point((this.Width - iWidth) - 5, 5);
						break;
				}

				if (_HasFocus)
				{
					if(this.HoverImage != null){
						g.DrawImage(this.HoverImage, ptImage.X, ptImage.Y, this.HoverImage.Width, this.HoverImage.Height);
					}
					else
					{
						if(this.Image != null)
						{
							g.DrawImage(this.Enabled ? this.Image : this.DisabledImage, ptImage.X, ptImage.Y, this.Image.Width, this.Image.Height);
						}
					}
				}                
				else
				{
					if(this.Image != null)
					{
						g.DrawImage(this.Enabled ? this.Image : this.DisabledImage, ptImage.X, ptImage.Y, this.Image.Width, this.Image.Height);
					}
                }
            }

            #region Text 
            //' Get the dimensions of the text.
            //' This will return a SizeF object that has height, width, etc.
            SizeF ptSize = g.MeasureString(Text, Font);

            int Height = 0;
            if(ControlUtil.checkLetterKO(Text))
            {
                Height = 2;
            }

            PointF ptF = new PointF();
			//' Get the location to set the Text.
			switch(this.TextAlign)
			{
				case ContentAlignment.BottomCenter:
					ptF.Y = this.Height;
					ptF.Y -= ptSize.Height;
					ptF.X = Convert.ToInt32(this.Width / 2);
					ptF.X -= ptSize.Width / 2;
					break;

				case ContentAlignment.BottomLeft:
					ptF.Y = this.Height;
					ptF.Y -= ptSize.Height;
					ptF.X = 2;
					break;

				case ContentAlignment.BottomRight:
					ptF.Y = this.Height;
					ptF.Y -= ptSize.Height;
                    ptF.X = this.Width - this.Image.Width;
                    ptF.X -= ptSize.Width;
					break;

				case ContentAlignment.MiddleCenter:
					ptF.Y = Convert.ToInt32(this.Height / 2);
					ptF.Y -= ptSize.Height / 2;
                    ptF.Y += Height;
                    ptF.X = Convert.ToInt32(this.Width / 2);
					ptF.X -= ptSize.Width / 2;
					ptF.X += 2;
					break;

				case ContentAlignment.MiddleLeft:
					ptF.Y = Convert.ToInt32(this.Height / 2);
					ptF.Y -= ptSize.Height / 2;
                    ptF.Y += Height;
                    ptF.X = 10;
					break;

				case ContentAlignment.MiddleRight:
					ptF.Y = Convert.ToInt32(this.Height / 2);
					ptF.Y -= ptSize.Height / 2;
                    ptF.Y += Height;
                    ptF.X = this.Width - this.Image.Width;
					ptF.X -= ptSize.Width;
					break;

				case ContentAlignment.TopCenter:
					ptF.Y = 0;
					ptF.X = Convert.ToInt32(this.Width / 2);
					ptF.X -= ptSize.Width / 2;
					break;

				case ContentAlignment.TopLeft:
					ptF.Y = 0;
					ptF.X = 0;
					break;

				case ContentAlignment.TopRight:
					ptF.Y = 0;
                    ptF.X = this.Width - this.Image.Width;
                    ptF.X -= ptSize.Width;
					break;
			}
            #endregion

            if (_Pressed)
			{
				ptF.X = ptF.X + 1;
                //this.ForeColor = _FontColorHOver;
			}

			if(_HasFocus && !this.Selected)
			{
				//' Draw the Text for the control using the HoverForeColor
				if(!this.HoverForeColor.Equals(Color.Empty))
				{
					g.DrawString(this.Text, this.Font, new SolidBrush(this.HoverForeColor), ptF);
				}
				else
				{
					g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), ptF);
				}
            }
			else
			{
				//' Draw the Text for the control
				g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), ptF);
			}

            //switch (this.ButtonDesignType)
            //{
            //    case ButtonDesignType.NONE:
            //        break;
            //    case ButtonDesignType.ADD:
            //        this.BorderColor = _BorderColorAdd;
            //        break;
            //    case ButtonDesignType.DELETE:
            //        this.BorderColor = _BorderColorDel;
            //        break;
            //    case ButtonDesignType.REFERENCE:
            //        this.BorderColor = _BorderColorDefalut;
            //        break;
            //}

            //' Draw the border
            if (Border)
            {
                if (!BorderColor.Equals(Color.Empty))
                {
                    Pen linePen = new Pen(new SolidBrush(BorderColor), 1);
                    g.DrawRectangle(linePen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                }
            }

        }

		#endregion

		

		#region Events Handlers

		void aButton_MouseUp(object sender, MouseEventArgs e)
		{
			_Pressed = false;
			Invalidate();
		}

		void aButton_MouseDown(object sender, MouseEventArgs e)
		{
			_Pressed = true;
			Invalidate();
		}

		void aButton_MouseLeave(object sender, EventArgs e)
		{
			_HasFocus = false;
			Invalidate();
		}

		void aButton_MouseMove(object sender, MouseEventArgs e)
		{
			Point pt = new Point(e.X, e.Y);
			if(Selected) return;
			if(this._HoverRectangle.IntersectsWith(new Rectangle(pt, new Size(1, 1))))
			{
				if(!_HasFocus)
				{
					_HasFocus = true;
					Invalidate();
				}
			}
		}

		void aButton_Click(object sender, EventArgs e)
		{
			if (!this.Selected)
			{
				if (this.Selectable)
				{
					_Selected = true;
				}
				Invalidate();
				if (Selectable)
				{
					PrivateUnSelectOtherButtons();
				}

				//' This will raise the event
				//' that tells the host that the
				//' current selected item in the group,
				//' if there is one, is different.
				//OnSelectedChanged(new SelectedChangedEventAecrgs(this));
			}       
		}

		private void PrivateUnSelectOtherButtons()
		{
			foreach (Control itm in this.Parent.Controls)
			{
				string itmTypeName = itm.GetType().Name;
				if (this.GetType().Name == itmTypeName)
				{
					if (itm.Name != this.Name)
					{
						if (this.GroupKey != null)
						{
							if (((aButton)itm).GroupKey == this.GroupKey)
							{
								((aButton)itm).Selected = true;
							}
						}
						else
						{
							if (((aButton)itm).GroupKey == null)
							{
								((aButton)itm).Selected = false;
							}
						}
					}
				}
			}
		}
        #endregion

        #region IXControl Members

        //public void OnSubSystemTypeChanged(SubSystemType subSysType,bool Header)
        //{
        //	//Settings = UIHelper.GetFormSettings(this);
        //}

        public class SelectedChangedEventArgs : EventArgs
        {
            private aButton _Button;

            public aButton Button
            {
                get { return _Button; }
                set { _Button = value; }
            }

            public SelectedChangedEventArgs(aButton theButton)
            {
                this._Button = theButton;
            }

        }

        #endregion
    }

	#endregion 
}
