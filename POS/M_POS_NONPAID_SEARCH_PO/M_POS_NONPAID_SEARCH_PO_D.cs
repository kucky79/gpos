using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_NONPAID_SEARCH_PO
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_CUST_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_NONPAID_SEARCH_PO_DETAIL", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
