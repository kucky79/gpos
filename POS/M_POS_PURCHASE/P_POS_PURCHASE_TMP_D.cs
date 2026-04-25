using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class P_POS_PURCHASE_TMP
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_PO_PS", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

    }
}
