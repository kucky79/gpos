using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
    public partial class aTransparentPanel : System.Windows.Forms.Panel
    {
        public aTransparentPanel()
        {

        }

        public aTransparentPanel(IContainer container)
        {
            container.Add(this);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

    }

}
