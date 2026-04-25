using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_PO_SEARCH01
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_PO_SEARCH01", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

    }
}
