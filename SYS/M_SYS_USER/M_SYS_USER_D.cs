using Bifrost;
using System;
using System.Data;

namespace SYS
{
    class M_SYS_USER_D
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_SYS_USER_S", obj);
            return dt;
        }

        internal bool Save(DataTable dtChanges)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dtChanges;
            si.FirmCode = Global.FirmCode;
            si.UserID = Global.UserID;
            si.SpNameInsert = "USP_SYS_USER_I";
            si.SpNameUpdate = "USP_SYS_USER_U";
            si.SpNameDelete = "USP_SYS_USER_D";
            si.SpParamsInsert = new string[] { "CD_FIRM", "CD_USER", "NM_USER", "NO_PWD", "CD_EMP", "YN_USE", "DC_RMK" };
            si.SpParamsUpdate = new string[] { "CD_FIRM", "CD_USER", "NM_USER", "NO_PWD", "CD_EMP", "YN_USE", "DC_RMK" };
            si.SpParamsDelete = new string[] { "CD_FIRM", "CD_USER" };

            return DBHelper.Save(si);
        }

        internal bool resetPassword(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("USP_SYS_USER_PW_U", obj);
        }

    }
}
