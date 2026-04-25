using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace MAS
{
    partial class M_MAS_NOTICE
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_MAS_NOTICE_S", obj);
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
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_MAS_NOTICE_I";
                si.SpNameUpdate = "USP_MAS_NOTICE_U";
                si.SpNameDelete = "USP_MAS_NOTICE_D";
                si.SpParamsInsert = new string[] {  "CD_FIRM", "NO_NOTICE", "DT_NOTICE", "DT_FROM", "DT_TO", "FG_NOTICE", "ST_NOTICE", "DC_NOTICE", "DC_RMK", 
                                                    "CD_USER_REG"
                                                };
                si.SpParamsUpdate = new string[] {  "CD_FIRM", "NO_NOTICE", "DT_NOTICE", "DT_FROM", "DT_TO", "FG_NOTICE", "ST_NOTICE", "DC_NOTICE", "DC_RMK", 
                                                    "CD_USER_REG"
                                                };
                si.SpParamsDelete = new string[] { "CD_FIRM", "NO_NOTICE" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);


            }

            return DBHelper.Save(sc);
        }
    }
}
