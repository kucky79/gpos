using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_CLS_SEARCH
    {
        internal DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_CLS_SEARCH01", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_CLS_SEARCH02", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

    }
}
    