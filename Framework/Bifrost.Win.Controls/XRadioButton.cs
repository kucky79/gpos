using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NF.Framework.Common;

namespace NF.Framework.Win.Controls
{
    public partial class XRadioButton : RadioButton
    {
        public XRadioButton()
        {
            InitializeComponent();
        }

        public XRadioButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private ResManager resManager = new ResManager();
        private string _Resource = string.Empty;
        public string xResource
        {
            get { return _Resource; }
            set
            {
                if (value == "")
                {
                    _Resource = string.Empty;
                    this.Text = "RadioButton";
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
        public string Resource
        {
            get { return _Resource; }
            set
            {
                if (value == "")
                {
                    _Resource = string.Empty;
                    this.Text = "RadioButton";
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
    }
}
