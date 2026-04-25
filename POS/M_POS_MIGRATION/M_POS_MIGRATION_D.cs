using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_MIGRATION
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_STORE_S", obj);
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
                si.SpNameInsert = "USP_POS_STORE_I";
                si.SpNameUpdate = "USP_POS_STORE_U";
                si.SpNameDelete = "USP_POS_STORE_D";
                si.SpParamsInsert = new string[] {  "CD_STORE", "NM_STORE", "NM_CEO", "NO_BIZ", "DC_BIZ1", "DC_BIZ2", "NO_POST", "DC_ADDR1", "DC_ADDR2", "NO_TEL1",
                                                    "NO_TEL2", "NO_FAX1", "NO_FAX2", "DC_HOMPAGE", "NO_ACCOUNT1", "NO_ACCOUNT2", "YN_USE", "CD_FIRM", "DC_RMK"
                                                };
                si.SpParamsUpdate = new string[] {  "CD_STORE", "NM_STORE", "NM_CEO", "NO_BIZ", "DC_BIZ1", "DC_BIZ2", "NO_POST", "DC_ADDR1", "DC_ADDR2", "NO_TEL1",
                                                    "NO_TEL2", "NO_FAX1", "NO_FAX2", "DC_HOMPAGE", "NO_ACCOUNT1", "NO_ACCOUNT2", "YN_USE", "CD_FIRM", "DC_RMK"
                                                };
                si.SpParamsDelete = new string[] { "CD_STORE" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);

                
            }

            return DBHelper.Save(sc);
        }
    }
}
