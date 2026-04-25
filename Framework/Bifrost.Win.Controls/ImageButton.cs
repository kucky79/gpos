using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HiNet.Framework.Win.Controls
{
	public partial class ImageButtion : PictureBox
	{
		private bool _Pressed = false;

        private ButtonImages _buttonImage = ButtonImages.Left16;

        public ImageButtion ButtonImage
        {
            get { return _buttonImage; }
            set
            {
                _buttonImage = value;
                switch (value)
                {
                    case ButtonImages.Left16:
                        this.Image = Images.btnLeft16;
                        this.Width = Images.btnLeft16.Width + 2;
                        this.Height = Images.btnLeft16.Height + 2;
                        break;
                    case ButtonImages.Right16:
                        this.Image = Images.btnRight16;
                        this.Width = Images.btnRight16.Width + 2;
                        this.Height = Images.btnRight16.Height + 2;
                        break;
                    case ButtonImages.Top16:
                        this.Image = Images.Top16;
                        this.Width = Images.btnTop16.Width + 2;
                        this.Height = Images.btnTop16.Height + 2;
                        break;
                    case ButtonImages.Bottom16:
                        this.Image = Images.Bottom16;
                        this.Width = Images.btnBottom16.Width + 2;
                        this.Height = Images.btnBottom16.Height + 2;
                        break;
                }
            }
        }

        public ImageButtion()
		{
			InitializeComponent();

			//this.Image = Properties.Resources.btn_popup;
			//this.SizeMode = PictureBoxSizeMode.CenterImage;

			// TODO: Add any initialization after the InitComponent call
			this.SetStyle(ControlStyles.ResizeRedraw, true);

			// This supports mouse movement such as the mouse wheel
			this.SetStyle(ControlStyles.UserMouse, true);

			// This allows the control to be transparent
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			// This helps with drawing the control so that it doesn't flicker
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,
				true);

			// This updates the styles
			this.UpdateStyles();

			this.Width = Images.btn_popup.Width + 2;
			this.Height = Images.btn_popup.Height + 2;
		}

		private System.Windows.Forms.FlatStyle _FlatStyle = FlatStyle.Flat;

		/// <summary>
		/// Not used
		/// </summary>
		public System.Windows.Forms.FlatStyle FlatStyle
		{
			get { return _FlatStyle; }
			set { _FlatStyle = value; }
		}
	

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaintBackground(pe);

			Point pnt = new Point((pe.ClipRectangle.Width - Images.btn_popup.Width) / 2, (pe.ClipRectangle.Height - Images.btn_popup.Height) / 2);


			if (_Pressed)
			{
				pnt.X = pnt.X + 1;
				pe.Graphics.DrawImage(Images.btn_popup, pnt);
			}
			else
			{
				pe.Graphics.DrawImage(Images.btn_popup, pnt);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			_Pressed = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			_Pressed = false;
			Invalidate();
		}
	}
}
