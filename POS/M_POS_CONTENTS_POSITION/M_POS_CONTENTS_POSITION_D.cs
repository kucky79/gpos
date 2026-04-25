using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_CONTENTS_POSITION
    {
        internal DataSet SearchItem(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("USP_POS_ITEM_POSITION_S", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }

        internal DataTable ItemReposition(object[] obj, int seletedIndex)
        {
            DataTable dt = new DataTable();

            switch (seletedIndex)
            {
                case 0:
                    break;
                case 1:
                    dt = DBHelper.GetDataTable("USP_POS_CUST_REPOSITION", obj);
                    break;
                case 2:
                    dt = DBHelper.GetDataTable("USP_POS_ITEMTYPE_REPOSITION", obj);
                    break;
                case 3:
                    dt = DBHelper.GetDataTable("USP_POS_ITEM_REPOSITION", obj);
                    break;
            }

            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(DataTable dtCustType, DataTable dtCust, DataTable dtItemType, DataTable dtItem)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtCustType != null)
            {
                //SpInfo si = new SpInfo();
                //si.DataValue = dtCustType;
                //si.FirmCode = POSGlobal.StoreCode;
                //si.UserID = POSGlobal.UserID;
                //si.SpNameInsert = "USP_POS_ITEMTYPE_POSITION_U";
                //si.SpNameUpdate = "USP_POS_ITEMTYPE_POSITION_U";
                //si.SpParamsInsert = new string[] { "CD_STORE", "CD_ITEM", "NO_SORT" };
                //si.SpParamsUpdate = new string[] { "CD_STORE", "CD_ITEM", "NO_SORT" };
                ////si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                ////si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                ////si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                //sc.Add(si);
            }

            if (dtCust != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtCust;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_CUST_POSITION_U";
                si.SpNameUpdate = "USP_POS_CUST_POSITION_U";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CUST", "NO_SORT" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "CD_CUST", "NO_SORT" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }

            if (dtItemType != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtItemType;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_ITEMTYPE_POSITION_U";
                si.SpNameUpdate = "USP_POS_ITEMTYPE_POSITION_U";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_ITEM", "NO_SORT" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "CD_ITEM", "NO_SORT" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }

            if (dtItem != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtItem;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_ITEM_POSITION_U";
                si.SpNameUpdate = "USP_POS_ITEM_POSITION_U";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_ITEM", "NO_SORT" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "CD_ITEM", "NO_SORT" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }


            return DBHelper.Save(sc);
        }

    }
}
