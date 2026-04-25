using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_OLD01
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH01", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
