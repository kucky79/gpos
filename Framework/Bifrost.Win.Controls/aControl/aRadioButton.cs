using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Bifrost.Win.Controls
{
    public partial class aRadioButton : RadioGroup
    {
        public aRadioButton()
        {
            InitializeComponent();
        }

        private string _SelectItem = string.Empty;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Select
        {
            get { return _SelectItem; }
            set
            {
                if (_SelectItem == string.Empty)
                    return;
                this.IsModified = true;
                value = this.EditValue.ToString();
                _SelectItem = value;
            }
        }

        private string _selectedValue = string.Empty;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (_selectedValue == string.Empty)
                    return;
                this.IsModified = true;
                value = this.EditValue.ToString();
                _selectedValue = value;
            }
        }

        bool ShouldSerializeSelect()
        {
            return Select != string.Empty;
        }

    }
}
