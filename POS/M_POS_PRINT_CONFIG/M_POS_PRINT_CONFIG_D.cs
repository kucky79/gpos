using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_PRINT_CONFIG
    {
        internal DataSet Search(object[] obj)
        {
            return DBHelper.GetDataSet("USP_POS_PRINT_CONFIG_S", obj);
        }

        internal bool Save(DataTable dtH, DataTable dtD)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtH;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_PRINT_CONFIGH_I";
                si.SpNameUpdate = "USP_POS_PRINT_CONFIGH_U";
                si.SpNameDelete = "USP_POS_PRINT_CONFIGH_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "TP_PRINT", "NM_PRINT", "DC_RMK" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "TP_PRINT", "NM_PRINT", "DC_RMK" };
                si.SpParamsDelete = new string[] { "CD_STORE", "TP_PRINT" };
                sic.Add(si);
            }

            if (dtD != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtD;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_PRINT_CONFIGL_I";
                si.SpNameUpdate = "USP_POS_PRINT_CONFIGL_U";
                si.SpNameDelete = "USP_POS_PRINT_CONFIGL_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "TP_PRINT", "NO_LINE", "TP_REPORT", "NM_REPORT", "YN_USE", "DC_RMK" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "TP_PRINT", "NO_LINE", "TP_REPORT", "NM_REPORT", "YN_USE", "DC_RMK" };
                si.SpParamsDelete = new string[] { "CD_STORE", "TP_PRINT", "NO_LINE" };
                sic.Add(si);
            }

            DBHelper.Save(sic);

            return true;
        }
    }
}
