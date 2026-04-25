using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_OLD03
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH03", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchSUM(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH03_SUM", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }


        internal DataTable SearchCust(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH03_CUST", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
