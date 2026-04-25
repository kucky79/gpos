using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_OLD51
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SEARCH51", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
