using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_SALE_SEARCH01
    {
        internal DataTable SearchDay(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_SALE_SEARCH01_DAY", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchDayItem(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_SALE_SEARCH02", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchItem(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_SALE_SEARCH03", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchMonth(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_SALE_SEARCH04", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
