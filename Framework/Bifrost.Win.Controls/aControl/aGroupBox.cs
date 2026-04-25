using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bifrost.Win.Controls.aControl
{
    public class aGroupBox : GroupBox
    {
        public aGroupBox() : base()
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
        }

        private bool _readOnly;
        [Category("AIMS"),  DefaultValue(false)]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; UpdateChildren(this.Controls, _readOnly); }
        }

        public bool ShouldSerializeReadOnly()
        {
            return ReadOnly != false;
        }

        private void UpdateChildren(ControlCollection controls, bool readOnly)
        {
            foreach (Control c in controls)
            {
                if (c is BaseEdit)
                {
                    ((BaseEdit)c).Properties.ReadOnly = readOnly;
                }
                else
                {
                    UpdateChildren(c.Controls, readOnly);
                }
            }
        }
    }
}
