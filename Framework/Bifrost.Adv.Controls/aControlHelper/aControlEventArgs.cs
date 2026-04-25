using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Adv.Controls.aControlHelper
{
    public delegate void aControlEventHandler(object sender, aControlEventArgs e);

    public class aControlEventArgs : EventArgs
    {
        DataRow _ReturnDataRow;
        DataTable _ReturnDataTable;

        private string[] _PopUpParam;
        private string _UserPopUpParam;

        private Control _Control;
        private PopUpReturn _HelpReturn;
        private PopUpParam _HelpParam;

        public aControlEventArgs(PopUpReturn helpreturn, PopUpParam param, Control ctrl)
        {
            _HelpReturn = helpreturn;
            _HelpParam = param;
            _Control = ctrl;
        }

        public PopUpReturn HelpReturn
        {
            get { return _HelpReturn; }
        }

        public PopUpParam HelpParam
        {
            get { return _HelpParam; }
        }

        public DialogResult DialogResult
        {
            get { return _HelpReturn.DialogResult; }
        }

        public PopUpHelper.PopUpID PopUpID
        {
            get { return _HelpParam.PopUpID; }
        }

        public string ControlName
        {
            get { return _Control.Name; }
        }

        public string CodeName
        {
            get { return _HelpReturn.CodeName; }
        }

        public string CodeValue
        {
            get { return _HelpReturn.CodeValue; }
        }

        public DataRow ReturnDataRow
        {
            get { return _HelpReturn.Row; }
        }

        public DataTable ReturnDataTable
        {
            get { return _HelpReturn.DataTable; }
        }

        //public string[] PopUpParam
        //{
        //    get { return _PopUpParam; }
        //}

        //public string UserPopUpParam
        //{
        //    //get { return _UserPopUpParam; }
        //    set { _UserPopUpParam = value; }
        //}

        //public bool Complete
        //{
        //    set
        //    {
        //        if (_Control is aCodeText)
        //            ((aCodeText)_Control).Complete = value;
        //        else if (_Control is aCodeNText)
        //            ((aCodeNText)_Control).Complete = value;
        //    }
        //}

        public bool QueryCancel
        {
            set
            {
                _HelpParam.IsQueryCancel = value;
            }
        }
    }
}
