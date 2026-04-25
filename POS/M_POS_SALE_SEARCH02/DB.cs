using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_SALE_SEARCH02
    {
        internal DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_SALE_SEARCH02_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
