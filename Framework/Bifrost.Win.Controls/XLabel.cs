using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NF.Framework.Common;
//using NF.Framework.Win;

namespace NF.Framework.Win.Controls
{
	public partial class XLabel : Label, IXControl
	{
		//private Color _BackColor;
		public XLabel()
		{
			InitializeComponent();

			this.Margin = new Padding(1);
			this.Padding = new Padding(0, 0, 5, 0);		
		}
        private ResManager resManager = new ResManager();
        private string _Resource = string.Empty;
        public string xResource
        {
            get{return _Resource;}
            set
            {
                if (value == "")
                {
                    _Resource = string.Empty;
                    this.Text = "XLable";
                }
                else if (value.Substring(0,1).ToUpper() == "R") 
                {
                    _Resource = value.ToUpper();
                    this.Text = resManager.GetString(value.ToUpper());
                }                
                else
                {
                    MessageBox.Show("Resource Code 형식에 맞지 않습니다.", "속성오류", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }
        public string Resource
        {
            get { return _Resource; }
            set
            {
                if (value == "")
                {
                    _Resource = string.Empty;
                    this.Text = "XLable";
                }
                else if (value.Substring(0, 1).ToUpper() == "R")
                {
                    _Resource = value.ToUpper();
                    this.Text = resManager.GetString(value.ToUpper());
                }
                else
                {
                    MessageBox.Show("Resource Code 형식에 맞지 않습니다.", "속성오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
		#region Overrides

		/// <summary>
		/// Override AutoSize property of base Label
		/// </summary>
		public override bool AutoSize
		{
			get
			{
				return false;
			}
		}

		private ContentAlignment _TextAlign = ContentAlignment.MiddleRight;

		/// <summary>
		/// Label is always MidleRight alignment
		/// </summary>
		public override ContentAlignment TextAlign
		{
			get
			{
				return _TextAlign;;
			}
			set
			{
				_TextAlign = value;
			}
		}

		#endregion

		#region IXControl Members

        public void OnSubSystemTypeChanged(SubSystemType subSysType, bool Header)
		{
            this.BackColor = Header ? UIHelper.GetFormSettings(this).HeaderBackColor : UIHelper.GetFormSettings(this).TDLabelBackColor;
		}

		#endregion
}
}
