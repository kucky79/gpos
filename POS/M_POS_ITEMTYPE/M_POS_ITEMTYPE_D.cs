using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_ITEMTYPE
    {
        internal DataSet Search(object[] obj)
        {
            return DBHelper.GetDataSet("USP_POS_ITEMTYPE_S", obj);
        }

        internal bool Save(DataTable dtD)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dtD != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtD;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_CODEL_I";
                si.SpNameUpdate = "USP_POS_CODEL_U";
                si.SpNameDelete = "USP_POS_CODEL_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE", "NO_SEQ", "CD_CLAS_REF", "CD_FLAG_REF", "DC_REF1",
                                                    "DC_REF2", "DC_REF3", "DC_REF4", "DC_REF5", "DC_RMK" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE", "NO_SEQ", "CD_CLAS_REF", "CD_FLAG_REF", "DC_REF1",
                                                    "DC_REF2", "DC_REF3", "DC_REF4", "DC_REF5", "DC_RMK" };
                si.SpParamsDelete = new string[] { "CD_STORE", "CD_CLAS", "CD_FLAG" };
                sic.Add(si);
            }

            DBHelper.Save(sic);

            return true;
        }
    }
}
