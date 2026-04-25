using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace MAS
{
    partial class M_MAS_CODE
    {
        internal DataSet Search(object[] obj)
        {
            return DBHelper.GetDataSet("USP_MAS_CODE_S", obj);
        }

        internal bool Save(DataTable dtH, DataTable dtD)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtH;
                si.FirmCode = Global.FirmCode;
                si.UserID = Global.UserID;
                si.SpNameInsert = "USP_MAS_CODEH_I";
                si.SpNameUpdate = "USP_MAS_CODEH_U";
                si.SpNameDelete = "USP_MAS_CODEH_D";
                si.SpParamsInsert = new string[] { "CD_FIRM", "CD_MODULE", "CD_CLAS", "NM_CLAS", "YN_SYSTEM", "YN_USE", "DC_RMK", "CD_USER_REG" };
                si.SpParamsUpdate = new string[] { "CD_FIRM", "CD_MODULE", "CD_CLAS", "NM_CLAS", "YN_SYSTEM", "YN_USE", "DC_RMK", "CD_USER_AMD" };
                si.SpParamsDelete = new string[] { "CD_FIRM", "CD_MODULE", "CD_CLAS" };
                sic.Add(si);
            }

            if (dtD != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtD;
                si.FirmCode = Global.FirmCode;
                si.UserID = Global.UserID;
                si.SpNameInsert = "USP_MAS_CODEL_I";
                si.SpNameUpdate = "USP_MAS_CODEL_U";
                si.SpNameDelete = "USP_MAS_CODEL_D";
                si.SpParamsInsert = new string[] { "CD_FIRM", "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE", "NO_SEQ", "CD_CLAS_REF", "CD_FLAG_REF", "DC_REF1",
                                                    "DC_REF2", "DC_REF3", "DC_REF4", "DC_REF5", "DC_RMK" };
                si.SpParamsUpdate = new string[] { "CD_FIRM", "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE", "NO_SEQ", "CD_CLAS_REF", "CD_FLAG_REF", "DC_REF1",
                                                    "DC_REF2", "DC_REF3", "DC_REF4", "DC_REF5", "DC_RMK" };
                si.SpParamsDelete = new string[] { "CD_FIRM", "CD_CLAS", "CD_FLAG" };
                sic.Add(si);
            }

            DBHelper.Save(sic);

            return true;
        }
    }
}
