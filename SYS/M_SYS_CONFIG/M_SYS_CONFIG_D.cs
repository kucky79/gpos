using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace SYS
{
    partial class M_SYS_CONFIG
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_SYS_CONFIG_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(DataTable dtH)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtH;
                si.FirmCode = Global.FirmCode;
                si.UserID = Global.UserID;
                si.SpNameInsert = "USP_SYS_CONFIG_I";
                si.SpNameUpdate = "USP_SYS_CONFIG_U";
                si.SpNameDelete = "USP_SYS_CONFIG_D";
                si.SpParamsInsert = new string[] { "CD_FIRM", "CD_MODULE", "CD_CTRL", "NM_CTRL", "DC_CTRL", "TP_CTRL", "DC_CONFIG", "CD_CODE", "NUM_CTRL", "DC_RMK" };
                si.SpParamsUpdate = new string[] { "CD_FIRM", "CD_MODULE", "CD_CTRL", "NM_CTRL", "DC_CTRL", "TP_CTRL", "DC_CONFIG", "CD_CODE", "NUM_CTRL", "DC_RMK" };
                si.SpParamsDelete = new string[] { "CD_FIRM", "CD_MODULE", "CD_CTRL" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }


            return DBHelper.Save(sc);
        }
    }
}
