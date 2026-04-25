using Bifrost;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class DBHandler
    {
        public DataTable SearchList(object[] obj)
        {
            DataTable dt = DBHelperOld.GetDataTable("USP_OLD_SALE_LIST", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        public DataSet SearchDetail(object[] obj)
        {
            DataSet dt = DBHelperOld.GetDataSet("USP_OLD_SALE_DETAIL", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

    }
}
