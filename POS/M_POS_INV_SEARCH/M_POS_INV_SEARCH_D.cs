using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_INV_SEARCH
    {
        internal DataSet Search(object[] obj)
        {
            DataSet dt = DBHelper.GetDataSet("USP_POS_INV_SEARCH", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
