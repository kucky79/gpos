using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_OLD52
    {
        internal DataSet Search(object[] obj)
        {
            DataSet dt = DBHelperOld.GetDataSet("USP_OLD_SEARCH52", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }
    }
}
