using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_OLD54
    {
        internal DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH54_DAY", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH54_DAYITEM", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search3(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH54_ITEM", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search4(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH54_MONTH", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
