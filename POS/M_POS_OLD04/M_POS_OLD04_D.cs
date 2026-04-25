using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_OLD04
    {
        internal DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH04_DAY", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH04_DAYITEM", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search3(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH04_ITEM", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search4(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH04_MONTH", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
