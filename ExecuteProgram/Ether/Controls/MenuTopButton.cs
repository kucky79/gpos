using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bifrost.Common;
using System.Runtime.InteropServices;

namespace Bifrost.Win
{
    #region TopButton

    /// <summary>
    /// Summary description for UserControl1.
    /// </summary>
    [DefaultEvent("Click"), DefaultProperty("Resource")]
	public class MenuTopButton : UserControl
	{
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private const int FIXED_BUTTON_SIZE = 19;

        private Color _TempBackColor = Color.LightGray;
		private bool _HasFocus;
		private bool _Pressed;
		private Rectangle _HoverRectangle;
        private ResManager resManager = new ResManager();

        public delegate void SelectedChangedEventHandler(object sender, SelectedChangedEventArgs e);
        public event SelectedChangedEventHandler SelectedChanged;
		public void OnSelectedChanged(SelectedChangedEventArgs e)
		{
            SelectedChanged?.Invoke(this, e);
        }

        public delegate void PaintEventHandler(object sender, PaintEventArgs e);
        public event PaintEventHandler PaintChanged;


        #region Component Designer generated code

        public MenuTopButton() : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
           
            #region SetStyle
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
            #endregion

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
            this.Name = "TopButton";
            this.Size = new System.Drawing.Size(55, 55);
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
						ptImage = new Point((this.Width - iWidth) - 5, (this.Height - iHeight) - 3);
						break;
					case ContentAlignment.MiddleCenter:
						ptImage = new Point(Convert.ToInt32((this.Width / 2) - (iWidth / 2)), Convert.ToInt32((this.Height / 2) - (iHeight / 2)));
						break;
					case ContentAlignment.MiddleLeft:
						ptImage = new Point(5, Convert.ToInt32((this.Height / 2) - (iHeight / 2)));
						break;
					case ContentAlignment.MiddleRight:
						ptImage = new Point((this.Width - iWidth) - 10, Convert.ToInt32((this.Height / 2) - (iHeight / 2)));
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

            #region TextAlign
            //' Get the dimensions of the text.
            //' This will return a SizeF object that has height, width, etc.
            SizeF ptSize = g.MeasureString(Text, Font);

			PointF ptF = new PointF();
			//' Get the location to set the Text.
			switch(this.TextAlign)
			{
				case ContentAlignment.BottomCenter:
					ptF.Y = this.Height;
					ptF.Y -= ptSize.Height + 7;
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
					ptF.X = this.Width;
					ptF.X -= ptSize.Width;
					break;

				case ContentAlignment.MiddleCenter:
					ptF.Y = Convert.ToInt32(this.Height / 2);
					ptF.Y -= ptSize.Height / 2;
					ptF.Y += 2;
					ptF.X = Convert.ToInt32(this.Width / 2);
					ptF.X -= ptSize.Width / 2;
					ptF.X += 2;
					break;

				case ContentAlignment.MiddleLeft:
					ptF.Y = Convert.ToInt32(this.Height / 2);
					ptF.Y -= ptSize.Height / 2;
                    ptF.Y += 2;
                    ptF.X = 2;
					break;

				case ContentAlignment.MiddleRight:
					ptF.Y = Convert.ToInt32(this.Height / 2);
					ptF.Y -= ptSize.Height / 2;
                    ptF.Y += 2;
                    ptF.X = this.Width;
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
					ptF.X = this.Width;
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

		#region Properties
        
		private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;

		[DefaultValue(typeof(ContentAlignment), "ContentAlignment.MiddleCenter")]
		public ContentAlignment TextAlign
		{
			get { return _TextAlign; }
			set { _TextAlign = value; Invalidate(); }
		}

        private string _HiddenText = string.Empty;

        public string HiddenText
        {
            get { return _HiddenText; }
            set { _HiddenText = value; }
        }

        private ContentAlignment _ImageAlign = ContentAlignment.MiddleLeft;

		[DefaultValue(typeof(ContentAlignment), "ContentAlignment.MiddleLeft")]
		public ContentAlignment ImageAlign
		{
			get { return _ImageAlign; }
			set { _ImageAlign = value; Invalidate();}
		}

        private Color _BackColor = Color.Empty;// Settings.ButtonBackColor;
        [DefaultValue(typeof(Color), "Color.Empty")]
        public override Color BackColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }

        private Color _ForeColor = Color.Empty;// Settings.ButtonBackColor;
        [DefaultValue(typeof(Color), "Color.Empty")]
        public override Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }


        Color _BorderColor = Color.Empty;
        [DefaultValue(typeof(Color), "Color.Empty")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        Color _HoverColor = Color.Transparent;
        [DefaultValue(typeof(Color), "Color.Empty")]
        public Color HoverColor
        {
            get { return _HoverColor; }
            set { _HoverColor = value; Invalidate(); }
        }

        private Color _SelectedColor= Color.LightGray;

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
				 if(value && !Selectable) return;
				_Selected = value;
				if(!value)
					_HasFocus = false;
				Invalidate();
				if(value)
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
			set { _UnSelectOtherButtons = value;
			if (this.Selected)
				if (value)
					PrivateUnSelectOtherButtons();
			}
		}

		[Browsable(true), Description("Text that appears in the Button."), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; Invalidate(); }
		}

		private System.Drawing.Image _Image;

		[DefaultValue(typeof(Image), "null"), Description("Image that is shown when not mouse over.")]
		public System.Drawing.Image Image
		{
			get { return _Image; }
			set { _Image = value; Invalidate();}
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

        private bool _UseDefaultImages = true;

		/// <summary>
		/// Images, Bifrost.Common.Images.btn_enabled, btn_disabled, btn_hover
		/// </summary>
		public bool UseDefaultImages
		{
			get { return _UseDefaultImages; }
			set 
			{ 
				_UseDefaultImages = value;
                if (value)
                {
                    this.BorderStyle = BorderStyle.None;
                    this.DoubleBuffered = true;
                    PaintChanged?.Invoke(this, null);
                }

            }
		}	

		private string _GroupKey;

		[DefaultValue(typeof(string), ""), Description("Enter a value to group like-buttons together, this will affect the unselect behavior.")]
		public string GroupKey
		{
			get { return _GroupKey; }
			set { _GroupKey = value; }
		}

		private bool _Border = true;

		[DefaultValue(typeof(bool), "true"), Description("Draw the border around the control.")]
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

		[DefaultValue(typeof(bool), "false"), Description("Can this button be selected.")]
		public bool Selectable
		{
			get { return _Selectable; }
			set { _Selectable = value; }
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
							if (((MenuTopButton)itm).GroupKey == this.GroupKey)
							{
								((MenuTopButton)itm).Selected = true;
							}
						}
						else
						{
							if (((MenuTopButton)itm).GroupKey == null)
							{
								((MenuTopButton)itm).Selected = false;
							}
						}
					}
				}
			}
		}
        #endregion

        public class SelectedChangedEventArgs : EventArgs
        {
            private MenuTopButton _Button;

            public MenuTopButton Button
            {
                get { return _Button; }
                set { _Button = value; }
            }

            public SelectedChangedEventArgs(MenuTopButton theButton)
            {
                this._Button = theButton;
            }

        }

    }

	#endregion 
}
