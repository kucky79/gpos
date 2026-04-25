using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_SALE_RECEIPT
    {
        internal DataSet SearchSalesOrder(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("USP_POS_SO_S", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }

        internal DataTable SearchSalesOrderList(object[] obj)
        {
            DataTable ds = DBHelper.GetDataTable("USP_POS_SOH_S", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }

    }
}
