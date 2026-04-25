using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_PURCHASE_SEARCH
    {
        internal DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_PURCHASE_SEARCH01", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_PURCHASE_SEARCH02", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search3(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_PURCHASE_SEARCH03", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search4(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_PURCHASE_SEARCH04", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
