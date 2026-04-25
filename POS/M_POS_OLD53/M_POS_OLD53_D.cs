using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_OLD53
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH53", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchSum(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH53_SUM", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchCust(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH53_CUST", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
