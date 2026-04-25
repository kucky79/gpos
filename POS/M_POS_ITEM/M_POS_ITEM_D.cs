using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_ITEM
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_ITEM_ALL_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_ITEM_I";
                si.SpNameUpdate = "USP_POS_ITEM_U";
                si.SpNameDelete = "USP_POS_ITEM_D";
                si.SpParamsInsert = new string[] {
                    "CD_STORE", "CD_ITEM", "NM_ITEM", "DC_ITEM", "FG_ITEM", "FG_VAT", "UM_COST_PO", "UM_COST_SA", "TP_ITEM_L", "TP_ITEM_M",
                    "TP_ITEM_S", "YN_USE", "YN_INV", "NO_SORT", "DC_RMK", "CD_UNIT1","CD_UNIT2","CD_UNIT3","CD_UNIT4","YN_MAIN",
                    "QT_UNIT1","QT_UNIT2","QT_UNIT3","QT_UNIT4", "YN_PROFIT"
                    };
                si.SpParamsUpdate = new string[] {
                    "CD_STORE", "CD_ITEM", "NM_ITEM", "DC_ITEM", "FG_ITEM", "FG_VAT", "UM_COST_PO", "UM_COST_SA", "TP_ITEM_L", "TP_ITEM_M",
                    "TP_ITEM_S", "YN_USE", "YN_INV", "NO_SORT", "DC_RMK", "CD_UNIT1","CD_UNIT2","CD_UNIT3","CD_UNIT4","YN_MAIN",
                    "QT_UNIT1","QT_UNIT2","QT_UNIT3","QT_UNIT4", "YN_PROFIT"
                    };
                si.SpParamsDelete = new string[] { "CD_STORE", "CD_ITEM" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }


            return DBHelper.Save(sc);
        }
    }
}
