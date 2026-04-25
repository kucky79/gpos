using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HiNet.Framework.Win.Controls
{
    public partial class XTextBox : UserControl
    {
        public XTextBox()
        {
            InitializeComponent();
        }

        public int LableWidth
        {
            get { return xLabel1.Width; }
            set { xLabel1.Width = value; }
        }

        public string Res
        {
            get { return xLabel1.Resource; }
            set { xLabel1.Resource = value; }
        }

        public string Text  
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }
    }
}
