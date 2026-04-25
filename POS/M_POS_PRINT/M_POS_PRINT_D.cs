using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_PRINT
    {
        internal DataSet Search(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("USP_POS_PRINT_S", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }

        internal bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtH;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_PRINTH_I";
                si.SpNameUpdate = "USP_POS_PRINTH_U";
                si.SpNameDelete = "USP_POS_PRINTH_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_PRINT", "NM_PRINT", "CD_MENU" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "CD_PRINT", "NM_PRINT", "CD_MENU" };
                si.SpParamsDelete = new string[] { "CD_STORE", "CD_PRINT" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }

            if (dtL != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtL;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_PRINTL_I";
                si.SpNameUpdate = "USP_POS_PRINTL_U";
                si.SpNameDelete = "USP_POS_PRINTL_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_PRINT", "NO_LINE", "DC_PRINT", "CD_FIELD", "TP_ALIGN", "YN_EOL", "YN_BOLD", "QT_FONT_WIDTH", "QT_FONT_HEIGHT",
                                                    "YN_USE", "DC_RMK", "CD_USER_REG"
                                                    };
                si.SpParamsUpdate = new string[] { "CD_STORE", "CD_PRINT", "NO_LINE", "DC_PRINT", "CD_FIELD", "TP_ALIGN", "YN_EOL", "YN_BOLD", "QT_FONT_WIDTH", "QT_FONT_HEIGHT",
                                                    "YN_USE", "DC_RMK", "CD_USER_AMD" };

                si.SpParamsDelete = new string[] { "CD_STORE", "CD_PRINT", "NO_LINE" };

                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }

        internal bool Delete(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("USP_POS_PRINT_D", obj);
        }
    }
}
