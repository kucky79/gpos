#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Bifrost.Win
{
    public partial class WebFormBase : FormBase
    {
        public WebFormBase()
        {
            InitializeComponent();
        }

        public void SetLocation(string href)
        {
            axWebBrowser1.Navigate(href);
        }

        private void webBrowser1_NewWindow2(object sender, AxSHDocVw.DWebBrowserEvents2_NewWindow2Event e)
        {
            WebFormBase wf = new WebFormBase();
            e.ppDisp = wf.axWebBrowser1.Application;
            wf.Show(this);
        }

        private void webBrowser1_ClientToHostWindow(object sender, AxSHDocVw.DWebBrowserEvents2_ClientToHostWindowEvent e)
        {
            this.Width = e.cX + 10;
            this.Height = e.cY + 36;// 10;		
        }

        private void webBrowser1_TitleChange(object sender, AxSHDocVw.DWebBrowserEvents2_TitleChangeEvent e)
        {
            this.Text = e.text;
        }

        private void axWebBrowser1_ProgressChange(object sender, AxSHDocVw.DWebBrowserEvents2_ProgressChangeEvent e)
        {
            if(e.progressMax != 0)
                MdiForm.OnUpdateProgress(e.progress * 100 / e.progressMax, true);
        }

        private void axWebBrowser1_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            MdiForm.OnUpdateProgress(0, true);
        }

    }
}
