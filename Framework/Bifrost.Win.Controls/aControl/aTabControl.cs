using DevExpress.XtraTab;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
    public partial class aTabControl : XtraTabControl
    {
        public aTabControl()
        {
            InitializeComponent();

           
        }

        public aTabControl(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            foreach (XtraTabPage _Page in this.TabPages)
            {
                _Page.AutoScroll = true;
                //_Page.AutoScrollMargin = new System.Drawing.Size(20, 20);
                //_Page.AutoScrollMinSize = new System.Drawing.Size(_Page.Width - 40, _Page.Height - 40);
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            LookAndFeel.SkinName = "AIMS_SUB";
            LookAndFeel.UseDefaultLookAndFeel = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //this.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
        }
    }
}
