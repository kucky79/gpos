using Bifrost;
using System.Collections.Generic;
using System.Data;

namespace POS
{
    partial class M_POS_CUST
    {
        internal DataSet Search(object[] obj)
        {
            DataSet dt = DBHelper.GetDataSet("USP_POS_CUST_S_MAS", obj);
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
                si.SpNameInsert = "USP_POS_CUST_I";
                si.SpNameUpdate = "USP_POS_CUST_U";
                si.SpNameDelete = "USP_POS_CUST_D";
                si.SpParamsInsert = new string[] {
                                                "CD_STORE", "CD_CUST", "NM_CUST", "NM_CEO", "NO_BIZ", "TP_CUST_L", "TP_CUST_M", "TP_CUST_S", "NO_TEL1", "NO_TEL2",
                                                "NO_HP", "NO_FAX1", "NO_FAX2", "NO_CAR", "DC_BIZ1", "DC_BIZ2", "NO_POST", "DC_ADDR1", "DC_ADDR2", "NO_ACCOUNT1",
                                                "NO_ACCOUNT2", "AM_NONPAID", "YN_NONPAID", "NO_SORT", "YN_USE", "DT_LAST", "DC_RMK", "CD_USER_REG", "FG_CUST", "AM_NONPAID_PO"
                    }; 
                si.SpParamsUpdate = new string[] {
                                                "CD_STORE", "CD_CUST", "NM_CUST", "NM_CEO", "NO_BIZ", "TP_CUST_L", "TP_CUST_M", "TP_CUST_S", "NO_TEL1", "NO_TEL2",
                                                "NO_HP", "NO_FAX1", "NO_FAX2", "NO_CAR", "DC_BIZ1", "DC_BIZ2", "NO_POST", "DC_ADDR1", "DC_ADDR2", "NO_ACCOUNT1",
                                                "NO_ACCOUNT2", "AM_NONPAID", "YN_NONPAID", "NO_SORT", "YN_USE", "DT_LAST", "DC_RMK", "CD_USER_AMD", "FG_CUST", "AM_NONPAID_PO"
                    };
                si.SpParamsDelete = new string[] { "CD_STORE", "CD_CUST" };
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Insert, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Update, "NO_SO", listParam[0]);
                //si.SpParamsValues.Add(Bifrost.CommonFunction.SpState.Delete, "NO_SO", listParam[0]);
                sc.Add(si);
            }


            return DBHelper.Save(sc);
        }

    }
}
