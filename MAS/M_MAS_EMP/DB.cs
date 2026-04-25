using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace MAS
{
    partial class M_MAS_EMP
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_MAS_EMP_S", obj);
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
                si.FirmCode = Global.FirmCode;
                si.UserID = Global.UserID;
                si.SpNameInsert = "USP_MAS_EMP_I";
                si.SpNameUpdate = "USP_MAS_EMP_U";
                si.SpNameDelete = "USP_MAS_EMP_D";
                si.SpParamsInsert = new string[] {  "CD_FIRM", "CD_EMP", "NM_EMP", "CD_BIZ", "DT_BIRTH", "NO_REGISTRATION", "NO_TEL", "NO_HP", "DC_EMAIL", "NO_POST",
                                                    "DC_ADDR1", "DC_ADDR2", "YN_USE", "DC_RMK"
                                                };
                si.SpParamsUpdate = new string[] {  "CD_FIRM", "CD_EMP", "NM_EMP", "CD_BIZ", "DT_BIRTH", "NO_REGISTRATION", "NO_TEL", "NO_HP", "DC_EMAIL", "NO_POST",
                                                    "DC_ADDR1", "DC_ADDR2", "YN_USE", "DC_RMK"
                                                };
                si.SpParamsDelete = new string[] { "CD_FIRM", "CD_EMP" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);


            }

            return DBHelper.Save(sc);
        }
    }
}
