using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Adv.Controls
{
    public class ButtonEx : Button
    {
        private System.ComponentModel.Container components = null;


        const int WM_NCPAINT = 0x85;
        const uint RDW_INVALIDATE = 0x1;
        const uint RDW_IUPDATENOW = 0x100;
        const uint RDW_FRAME = 0x400;
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprc, IntPtr hrgn, uint flags);

        Color _BackColorDefault = System.Drawing.ColorTranslator.FromHtml("#F3F8FB");
        Color _BorderColorDefalut = System.Drawing.ColorTranslator.FromHtml("#D5DEE1");

        private Color _TempBackColor = Color.LightGray;
        private bool _HasFocus;
        private bool _Pressed;
        private Rectangle _HoverRectangle;


        public ButtonEx() : base()
        {
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

            this.AutoSize = false;
            //this.BorderColor = _BorderColorDefalut;
            //this.BackColor = _BackColorDefault;

            this.MouseMove += aButton_MouseMove;
            this.MouseLeave += aButton_MouseLeave;
            this.MouseDown += aButton_MouseDown;
            this.MouseUp += aButton_MouseUp;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }


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
            if (this._HoverRectangle.IntersectsWith(new Rectangle(pt, new Size(1, 1))))
            {
                if (!_HasFocus)
                {
                    _HasFocus = true;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            bool DrawHover = false;
            _HoverRectangle = new Rectangle(0, 0, this.Width, this.Height);

            Point ptImage;
            //' Used to hold the dimensions of the current image
            int iWidth = 0; int iHeight = 0;
            if (DrawHover)
            {
                if (this.HoverImage != null)
                {
                    iWidth = this.HoverImage.Width;
                    iHeight = this.HoverImage.Height;
                }
                else
                {
                    if (this.Image != null)
                    {
                        iWidth = this.Image.Width;
                        iHeight = this.Image.Height;
                    }
                }
            }
            else
            {
                if (this.Image != null)
                {
                    iWidth = this.Image.Width;
                    iHeight = this.Image.Height;
                }
            }

            //' The mouse is hovering over the control
            if (_HasFocus)
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
            else
            {
                //this.ForeColor = _ForeColor;
            }

            //' Make sure we even need to draw an image to begin
            //' with. There may not be any images in this control.
            if (iHeight != 0)
            {
                int Depth_Padding = 0;
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
                        ptImage = new Point((this.Width - iWidth) - Depth_Padding, Convert.ToInt32((this.Height / 2) - (iHeight / 2)));
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
                    if (this.HoverImage != null)
                    {
                        g.DrawImage(this.HoverImage, ptImage.X, ptImage.Y, this.HoverImage.Width, this.HoverImage.Height);
                    }
                    else
                    {
                        if (this.Image != null)
                        {
                            g.DrawImage(this.Enabled ? this.Image : this.DisabledImage, ptImage.X, ptImage.Y, this.Image.Width, this.Image.Height);
                        }
                    }
                }
                else
                {
                    if (this.Image != null)
                    {
                        g.DrawImage(this.Enabled ? this.Image : this.DisabledImage, ptImage.X, ptImage.Y, this.Image.Width, this.Image.Height);
                    }
                }
            }

            #region Text 
            //' Get the dimensions of the text.
            //' This will return a SizeF object that has height, width, etc.
            SizeF ptSize = g.MeasureString(Text, Font);

            PointF ptF = new PointF();
            //' Get the location to set the Text.
            switch (this.TextAlign)
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
                    ptF.X = this.Width;
                    ptF.X -= ptSize.Width;
                    break;

                case ContentAlignment.MiddleCenter:
                    ptF.Y = Convert.ToInt32(this.Height / 2);
                    ptF.Y -= ptSize.Height / 2;
                    //ptF.Y += 2;
                    ptF.X = Convert.ToInt32(this.Width / 2);
                    ptF.X -= ptSize.Width / 2;
                    ptF.X += 2;
                    break;

                case ContentAlignment.MiddleLeft:
                    ptF.Y = Convert.ToInt32(this.Height / 2);
                    ptF.Y -= ptSize.Height / 2;
                    //ptF.Y += -2;
                    ptF.X = 10;
                    break;

                case ContentAlignment.MiddleRight:
                    ptF.Y = Convert.ToInt32(this.Height / 2);
                    ptF.Y -= ptSize.Height / 2;
                    //ptF.Y += 2;
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
            }

            //' Draw the Text for the control
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), ptF);



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

        #region Properties

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

        private Color _SelectedColor = Color.LightGray;

        [DefaultValue(typeof(Color), "Color.LightGray")]
        public Color SelectedColor
        {
            get { return _SelectedColor; }
            set { _SelectedColor = value; Invalidate(); }
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

        private bool _UseDefaultImages = true;
        /// <summary>
        /// Images, Bifrost.Common.Images.btn_enabled, btn_disabled, btn_hover
        /// </summary>
        public bool UseDefaultImages
        {
            get { return _UseDefaultImages; }
            set { _UseDefaultImages = value; }
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

    }
}
