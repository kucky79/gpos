using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_SALE
    {
        internal DataSet SearchSalesOrder(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("USP_POS_SO_S", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }

        internal DataSet SearchTempOrderByOrder(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("USP_POS_TO_TMP_ORDER_S", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }
        
        internal DataSet SearchTempOrderByCustomer(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("USP_POS_TO_TMP_CUST_S", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }

        internal DataTable SearchCust(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_CUST_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchCustInfo(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_CUST_INFO_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataSet SearchItemALL(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("USP_POS_GET_ITEM", obj);
            DBHelper.SetDefaultValue(ds);
            return ds;
        }

        internal DataTable SearchItem(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_ITEM_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchItemType(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_GET_ITEMTYPE", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchItemFavorite(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_POS_CUST_ITEM_FAVORITE_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal bool SaveAfterUpdate(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("USP_POS_SOL_AFTER_IU", obj);
        }

        internal bool SaveReturn(object[] obj)
        {
            //CD_STORE, NO_SO, NO_EMP
            return DBHelper.ExecuteNonQuery("USP_POS_SO_RETURN_I", obj);
        }

        internal bool Save(DataTable dtSOH, DataTable dtSOL, DataTable dtPay, List<object> listParam)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtSOL != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtSOL;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_SOL_I";
                si.SpNameUpdate = "USP_POS_SOL_U";
                si.SpNameDelete = "USP_POS_SOL_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "NO_SO", "NO_LINE", "CD_ITEM", "DC_ITEM", "QT", "CD_UNIT", "QT_UNIT", "UM", "AM",
                                                    "FG_VAT", "AM_VAT", "AM_NET", "YN_RETURN", "NO_MGMT", "NO_MGMT_LINE", "DC_RMK", "CD_USER_REG"
                                                     };
                si.SpParamsUpdate = new string[] { "CD_STORE", "NO_SO", "NO_LINE", "CD_ITEM", "DC_ITEM", "QT", "CD_UNIT", "QT_UNIT", "UM", "AM",
                                                    "FG_VAT", "AM_VAT", "AM_NET", "YN_RETURN", "NO_MGMT", "NO_MGMT_LINE", "DC_RMK", "CD_USER_AMD"
                                                    };
                si.SpParamsDelete = new string[] { "CD_STORE", "NO_SO", "NO_LINE", "YN_RETURN" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }

            if (dtSOH != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtSOH;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_SOH_I";
                si.SpNameUpdate = "USP_POS_SOH_U";
                si.SpParamsInsert = new string[] { "CD_STORE", "NO_SO", "DT_SO", "CD_CUST", "FG_SO", "YN_RETURN", "AM", "AM_VAT", "AM_DISCOUNT", "AM_TOT",
                                                    "NO_EMP", "DC_RMK", "CD_USER_REG" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "NO_SO", "DT_SO", "CD_CUST", "FG_SO", "YN_RETURN", "AM", "AM_VAT", "AM_DISCOUNT", "AM_TOT",
                                                    "NO_EMP", "DC_RMK", "CD_USER_AMD" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);

                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "DT_SO", listParam[1]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_CUST", listParam[2]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "YN_RETURN", listParam[3]);


                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "DT_SO", listParam[1]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "CD_CUST", listParam[2]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "YN_RETURN", listParam[3]);


                sc.Add(si);
            }

            if (dtPay != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtPay;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_CLSH_I";
                si.SpNameUpdate = "USP_POS_CLSH_U";
                si.SpParamsInsert = new string[] { "CD_STORE", "NO_CLS", "DT_CLS", "CD_CUST", "YN_RETURN", "AM_PAY", "AM_CREDIT", "AM_NONPAID", "NO_SO", "NO_EMP", "DC_RMK", "CD_USER_REG" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "NO_CLS", "DT_CLS", "CD_CUST", "YN_RETURN", "AM_PAY", "AM_CREDIT", "AM_NONPAID", "NO_SO", "NO_EMP", "DC_RMK", "CD_USER_AMD" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                sc.Add(si);
            }


            return DBHelper.Save(sc);
        }

        internal bool Delete(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("USP_POS_SO_D", obj);
        }

        internal bool SaveTemp(DataTable dtH, DataTable dtL, List<object> listParam)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtL != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtL;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_TOL_I";
                si.SpNameUpdate = "USP_POS_TOL_U";
                si.SpNameDelete = "USP_POS_TOL_D";
                si.SpParamsInsert = new string[] { "CD_STORE", "NO_SLIP", "FG_SLIP", "NO_LINE", "FG_LAST", "CD_ITEM", "DC_ITEM", "QT", "CD_UNIT", "QT_UNIT",
                                                   "UM", "AM", "FG_VAT", "AM_VAT", "AM_NET", "NO_MGMT", "NO_MGMT_LINE", "DC_RMK", "CD_USER_REG"
                                                     };
                si.SpParamsUpdate = new string[] { "CD_STORE", "NO_SLIP", "FG_SLIP", "NO_LINE", "FG_LAST", "CD_ITEM", "DC_ITEM", "QT", "CD_UNIT", "QT_UNIT",
                                                   "UM", "AM", "FG_VAT", "AM_VAT", "AM_NET", "NO_MGMT", "NO_MGMT_LINE", "DC_RMK", "CD_USER_AMD"
                                                    };
                si.SpParamsDelete = new string[] { "CD_STORE", "NO_SLIP", "FG_SLIP", "NO_LINE" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SLIP", listParam[0]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "FG_SLIP", listParam[3]);

                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SLIP", listParam[0]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "FG_SLIP", listParam[3]);

                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SLIP", listParam[0]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "FG_SLIP", listParam[3]);

                sc.Add(si);
            }

            if (dtH != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtH;
                si.FirmCode = POSGlobal.StoreCode;
                si.UserID = POSGlobal.UserID;
                si.SpNameInsert = "USP_POS_TOH_I";
                si.SpNameUpdate = "USP_POS_TOH_U";
                si.SpParamsInsert = new string[] { "CD_STORE", "NO_SLIP", "FG_SLIP", "ST_SLIP", "DT_SLIP", "CD_CUST", "AM_TOT", "NO_EMP", "DC_RMK",
                                                   "CD_USER_REG" };
                si.SpParamsUpdate = new string[] { "CD_STORE", "NO_SLIP", "FG_SLIP", "ST_SLIP", "DT_SLIP", "CD_CUST", "AM_TOT", "NO_EMP", "DC_RMK",
                                                   "CD_USER_AMD" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SLIP", listParam[0]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "DT_SLIP", listParam[1]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_CUST", listParam[2]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "FG_SLIP", listParam[3]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "ST_SLIP", "P");

                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SLIP", listParam[0]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "DT_SLIP", listParam[1]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "CD_CUST", listParam[2]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "FG_SLIP", listParam[3]);
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "ST_SLIP", "P");

                sc.Add(si);
            }

            if (listParam != null)
            {
                SpInfo si = new SpInfo();
                si.SpNameNonQuery = "USP_POS_TOL_AFTER_IU";
                si.SpParamsNonQuery = new object[] { POSGlobal.StoreCode, listParam[0], listParam[3] };
                sc.Add(si);
            }


            return DBHelper.Save(sc);
        }

        internal bool UpdateTemp(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("USP_POS_TO_AFTER_U", obj);
        }

        internal bool DeleteTemp(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("USP_POS_TO_D", obj);
        }
    }
}
