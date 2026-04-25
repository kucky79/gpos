using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_ITEM_SALE_SEARCH01
    {
        internal DataSet Search(object[] obj)
        {
            DataSet dt = DBHelper.GetDataSet("USP_POS_ITEM_SALE_SEARCH01", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

    }
}
