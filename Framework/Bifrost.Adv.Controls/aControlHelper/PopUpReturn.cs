using System;

using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bifrost.Win;

namespace Bifrost.Adv.Controls.aControlHelper
{
    public class PopUpReturn
    {
        #region -> Member

        private DataRow[] _Rows;
        private DataRow _Row;

        private string _CodeValue;
        private string _CodeName;

        private DialogResult _DialogResult;
        private PopUpHelper.PopUpID _PopUpID;

        public string _userPopUpID;

        #endregion

        #region -> Construct

        public PopUpReturn(PopUpHelper.PopUpID PopUpID)
        {
            _PopUpID = PopUpID;
            _DialogResult = DialogResult.Cancel;
            _userPopUpID = string.Empty;
        }

        public PopUpReturn()
        {
            _DialogResult = DialogResult.Cancel;
            _userPopUpID = string.Empty;
        }

        #endregion

        #region -> Property

        private DataRow[] Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }

        public DataRow Row
        {
            get { return _Row; }
            set { _Row = value; }
        }

        public string CodeValue
        {
            get { return _CodeValue; }
            set { _CodeValue = value; }
        }

        public string CodeName
        {
            get { return _CodeName; }
            set { _CodeName = value; }
        }

        public DialogResult DialogResult
        {
            get { return _DialogResult; }
            set { _DialogResult = value; }
        }

        public DataTable DataTable
        {
            get
            {
                if (_Rows != null)
                {
                    if (_Rows.Length != 0)
                    {
                        DataTable ResultDt = new DataTable();
                        ResultDt = _Rows[0].Table.Clone();

                        foreach (DataRow row in _Rows)
                        {
                            ResultDt.ImportRow(row);
                        }
                        return ResultDt;
                    }
                }
                return null;
            }
        }

        public PopUpHelper.PopUpID PopUpID
        {
            get { return this._PopUpID; }
            set { this._PopUpID = value; }
        }

        #endregion

        #region ConfirmField

        public void ConfirmField()
        {
            if (this.DataTable == null)
            {
                System.Windows.Forms.MessageBox.Show("DataTable is not Exist!");
            }
            else
            {
                string temp = "■■■■■  Table Column Name  ■■■■■";
                temp += "\n 총 컬럼수 : " + this.DataTable.Columns.Count.ToString() + "\n";

                int count = 0;
                foreach (System.Data.DataColumn col in this.DataTable.Columns)
                {
                    temp += "[ " + col.ColumnName + " ] ";
                    if (count == 4)
                    {
                        count = 0;
                        temp += "\n";
                    }
                    else
                        count++;
                }
                System.Windows.Forms.MessageBox.Show(temp);
            }
        }

        #endregion

        public int RowCount
        {
            get
            {
                if (_Rows == null || _Rows.Length == 0)
                    return 0;

                return _Rows.Length;
            }
        }

        public string UserPopUpID
        {
            get
            {
                return _userPopUpID;
            }
            set
            {
                _userPopUpID = value;
            }
        }
    }
}
