using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
    public class aGroupControl : GroupControl
    {
        private bool _readOnly;
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; UpdateChildren(this.Controls, _readOnly); }
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
