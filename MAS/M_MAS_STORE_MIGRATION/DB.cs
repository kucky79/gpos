using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace MAS
{
    partial class M_MAS_STORE_MIGRATION
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("USP_MAS_STORE_S", obj);
            DBHelper.SetDefaultValue(dt);
            return dt;
        }

        internal bool SaveAll(DataTable dtStore, DataTable dtItem, DataTable dtItemUnit, DataTable dtUnitCode, DataTable dtItemTypeCode, DataTable dtCustomerSale, DataTable dtCustomerPurchase, DataTable dtItemPriceSale, DataTable dtItemPricePurchase,  string storeCode)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtStore != null)
            {
                //기존에 있는거 삭제하기 위해
                SpInfo siBeforeStore = new SpInfo();
                siBeforeStore.SpNameNonQuery = "USP_POS_MIGRATION_STORE_I_BEFORE";
                siBeforeStore.SpParamsNonQuery = new string[] { storeCode };
                sc.Add(siBeforeStore);

                SpInfo si = new SpInfo();
                si.DataValue = dtStore;
                si.SpNameInsert = "USP_POS_MIGRATION_STORE_I";
                si.SpParamsInsert = new string[] {  "CD_STORE", "NM_STORE", "NM_CEO", "NO_BIZ", "DC_BIZ1", "DC_BIZ2", "NO_POST", "DC_ADDR1", "DC_ADDR2", "NO_TEL1",
                                                    "NO_TEL2", "NO_FAX1", "NO_FAX2", "DC_HOMPAGE", "NO_ACCOUNT1", "NO_ACCOUNT2", "YN_USE", "CD_FIRM", "DC_RMK"
                                                };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);

                sc.Add(si);
            }

            if (dtItem != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtItem;
                si.SpNameInsert = "USP_POS_MIGRATION_ITEM_I";
                si.SpParamsInsert = new string[] {  "CD_STORE", "CD_ITEM", "NM_ITEM", "DC_ITEM", "FG_ITEM", "FG_VAT", "UM_COST_PO", "UM_COST_SA", "TP_ITEM_L", "TP_ITEM_M",
                                                    "TP_ITEM_S", "YN_USE", "YN_INV", "NO_SORT"
                                                };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }

            if (dtItemUnit != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtItemUnit;
                si.SpNameInsert = "USP_POS_MIGRATION_ITEM_UNIT_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_ITEM", "CD_UNIT", "NO_SORT", "QT_UNIT", "YN_MAIN" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }

            if (dtUnitCode != null)
            {
                //기존에 있는거 삭제하기 위해
                SpInfo siBeforeCode = new SpInfo();
                siBeforeCode.SpNameNonQuery = "USP_POS_MIGRATION_CODE_I_BEFORE";
                siBeforeCode.SpParamsNonQuery = new string[] { storeCode,  "POS101" };
                sc.Add(siBeforeCode);

                SpInfo si = new SpInfo();
                si.DataValue = dtUnitCode;

                si.SpNameInsert = "USP_POS_MIGRATION_CODE_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }

            if (dtItemTypeCode != null)
            {
                //기존에 있는거 삭제하기 위해
                SpInfo siBeforeCode = new SpInfo();
                siBeforeCode.SpNameNonQuery = "USP_POS_MIGRATION_CODE_I_BEFORE";
                siBeforeCode.SpParamsNonQuery = new string[] { storeCode, "POS102" };
                sc.Add(siBeforeCode);

                SpInfo si = new SpInfo();
                si.DataValue = dtItemTypeCode;

                si.SpNameInsert = "USP_POS_MIGRATION_CODE_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }

            if (dtCustomerSale != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtCustomerSale;

                si.SpNameInsert = "USP_POS_MIGRATION_CUST_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CUST", "NM_CUST", "NM_CEO", "NO_BIZ", "FG_CUST", "TP_CUST_L", "TP_CUST_M", "TP_CUST_S", "NO_TEL1",
                                                    "NO_TEL2", "NO_HP", "NO_FAX1", "NO_FAX2", "NO_CAR", "AM_NONPAID", "AM_NONPAID_PO", "NO_SORT", "YN_USE" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }

            if (dtCustomerPurchase != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtCustomerPurchase;

                si.SpNameInsert = "USP_POS_MIGRATION_CUST_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CUST", "NM_CUST", "NM_CEO", "NO_BIZ", "FG_CUST", "TP_CUST_L", "TP_CUST_M", "TP_CUST_S", "NO_TEL1",
                                                    "NO_TEL2", "NO_HP", "NO_FAX1", "NO_FAX2", "NO_CAR", "AM_NONPAID", "AM_NONPAID_PO", "NO_SORT", "YN_USE" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }

            if (dtItemPriceSale != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtItemPriceSale;

                si.SpNameInsert = "USP_POS_CUST_ITEM_PRICE_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CUST", "CD_ITEM", "FG_SLIP", "CD_UNIT", "UM" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }

            if (dtItemPricePurchase != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dtItemPricePurchase;

                si.SpNameInsert = "USP_POS_CUST_ITEM_PRICE_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CUST", "CD_ITEM", "FG_SLIP", "CD_UNIT", "UM" };
                si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "CD_STORE", storeCode);
                sc.Add(si);
            }


            if (storeCode != string.Empty)
            {
                SpInfo si = new SpInfo();
                si.SpNameNonQuery = "USP_POS_MIGRATION_ITEM_UNIT_I_AFTER";
                si.SpParamsNonQuery = new string[] { storeCode };
                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }

        internal bool SaveStore(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.SpNameInsert = "USP_POS_MIGRATION_STORE_I";
                si.SpParamsInsert = new string[] {  "CD_STORE", "NM_STORE", "NM_CEO", "NO_BIZ", "DC_BIZ1", "DC_BIZ2", "NO_POST", "DC_ADDR1", "DC_ADDR2", "NO_TEL1",
                                                    "NO_TEL2", "NO_FAX1", "NO_FAX2", "DC_HOMPAGE", "NO_ACCOUNT1", "NO_ACCOUNT2", "YN_USE", "CD_FIRM", "DC_RMK"
                                                };
                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }

        internal bool SaveItem(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.SpNameInsert = "USP_POS_MIGRATION_ITEM_I";
                si.SpParamsInsert = new string[] {  "CD_STORE", "CD_ITEM", "NM_ITEM", "DC_ITEM", "FG_ITEM", "FG_VAT", "UM_COST_PO", "UM_COST_SA", "TP_ITEM_L", "TP_ITEM_M",
                                                    "TP_ITEM_S", "YN_USE", "YN_INV", "NO_SORT"
                                                };
                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }

        internal bool SaveItemUnit(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.SpNameInsert = "USP_POS_MIGRATION_ITEM_UNIT_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_ITEM", "CD_UNIT", "NO_SORT", "QT_UNIT", "YN_MAIN" };
                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }

        internal bool AfterSaveItemUnit(string StoreCode)
        {
            return DBHelper.ExecuteNonQuery("USP_POS_MIGRATION_ITEM_UNIT_I_AFTER", new object[] { StoreCode });
        }

        internal bool BeforeSaveCode(string StoreCode)
        {
            return DBHelper.ExecuteNonQuery("USP_POS_MIGRATION_CODE_I_BEFORE", new object[] { StoreCode });
        }

        internal bool SaveCode(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                
                si.SpNameInsert = "USP_POS_MIGRATION_CODE_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE" };
                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }

        internal bool SaveCustomer(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                
                si.SpNameInsert = "USP_POS_MIGRATION_CUST_I";
                si.SpParamsInsert = new string[] { "CD_STORE", "CD_CUST", "NM_CUST", "NM_CEO", "NO_BIZ", "FG_CUST", "TP_CUST_L", "TP_CUST_M", "TP_CUST_S", "NO_TEL1",
                                                    "NO_TEL2", "NO_HP", "NO_FAX1", "NO_FAX2", "NO_CAR", "AM_NONPAID", "AM_NONPAID_PO", "NO_SORT", "YN_USE" };
                sc.Add(si);
            }

            return DBHelper.Save(sc);
        }
    }
}
