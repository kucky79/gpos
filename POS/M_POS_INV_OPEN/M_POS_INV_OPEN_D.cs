using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_INV_OPEN
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_INV_OPEN_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchItem(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_ITEM_S", obj);
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
                si.SpNameInsert = "USP_POS_INV_OPEN_I";
                si.SpNameUpdate = "USP_POS_INV_OPEN_U";
                si.SpNameDelete = "USP_POS_INV_OPEN_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_ITEM", "CD_UNIT", "QT_OPEN", "UM_OPEN", "AM_OPEN", "DC_RMK", "CD_USER_REG" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "CD_ITEM", "CD_UNIT", "QT_OPEN", "UM_OPEN", "AM_OPEN", "DC_RMK", "CD_USER_AMD" };
                si.SpParamsDelete = new string[] { "CD_STORE", "CD_ITEM", "CD_UNIT" };
                
                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}
