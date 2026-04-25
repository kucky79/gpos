using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class P_POS_SALE_TMP
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_SO_PS", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

    }
}
 