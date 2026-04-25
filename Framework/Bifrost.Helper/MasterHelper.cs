using System;
using System.Collections.Generic;
using System.Data;

namespace Bifrost.Helper
{
    public class MasterHelper
    {
        private static Dictionary<string, DataRow> cacheOffice = new Dictionary<string, DataRow>();
        public static DataRow GetOffice(string cdBiz)
        {
            if (A.GetString(cdBiz) == string.Empty) return null;
            if (!cacheOffice.ContainsKey(cdBiz))
            {
                DataTable dt = DBHelper.GetDataTable("AP_MAS_BIZ_HELPER_S", new object[] { Global.FirmCode, cdBiz });
                if (dt.Rows.Count > 0)
                    cacheOffice.Add(cdBiz, dt.Rows[0]);
                else
                    throw new Exception("["+ cdBiz + "] Is not registered!!");
            }
            return cacheOffice[cdBiz];
        }

        private static Dictionary<string, DataRow> cachePartner = new Dictionary<string, DataRow>();
        public static DataRow GetPartner(string cdPartner)
        {
            if (A.GetString(cdPartner) == string.Empty) return null;
            if (!cachePartner.ContainsKey(cdPartner))
            {
                DataTable dt = DBHelper.GetDataTable("AP_MAS_PARTNER_HELPER_S", new object[] { Global.FirmCode, cdPartner });
                if (dt.Rows.Count > 0)
                    cachePartner.Add(cdPartner, dt.Rows[0]);
                else
                    throw new Exception("[" + cdPartner + "] Is not registered!!");
            }
            return cachePartner[cdPartner];
        }
    }
}
