using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bifrost.FileTransfer;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace AppStarter
{
    public partial class AppStarter : Form
    {
        public AppStarter()
        {
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            //this.Load += AppStarter_Load;
            this.Shown += AppStarter_Shown;
        }

        private void AppStarter_Shown(object sender, EventArgs e)
        {
            //this.Hide();
            Thread.Sleep(3000);
            new UpdateProgForm().Show(this);
        }

        private void AppStarter_Load(object sender, EventArgs e)
        {
            //this.Hide();
            Thread.Sleep(2000);
            new UpdateProgForm().Show(this);
        }
    }
}