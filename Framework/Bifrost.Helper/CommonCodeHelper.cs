using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bifrost.Win;

namespace Bifrost.Helper
{
    public class CommonCodeHelper : CommonCodeHelperBase
    {
        private DataSet _AllCodes = null;
        private string _Langusge = "KO";
        private bool _LoadOnDemand = false;
        private string _CompanyCode = "";

        /// <summary>
        /// 공통코드 관련 Load
        /// </summary>
        /// <param name="strCompanyCode">CompanyCode</param>
        /// <param name="strLangusge">Langusge</param>
        /// <param name="loadOnDemand">False : 시작시 전체 코드를 한번 가져온다.</param>
        public CommonCodeHelper(string strCompanyCode, string strLangusge, bool loadOnDemand) : base(strCompanyCode, strLangusge, loadOnDemand)
        {
            _Langusge = strLangusge;
            _LoadOnDemand = loadOnDemand;
            _CompanyCode = strCompanyCode;

            if (!_LoadOnDemand)
            {
                _AllCodes = DBHelper.GetDataSet("AP_SYS_GET_ALL_CODE_S", new object[] { _CompanyCode, _Langusge });
            }
        }

        #region GetSubCodes

        public override DataTable GetSubCodes(string ClassCode)
        {
            return GetSubCodes(ClassCode, false, "", "");
        }

        public override DataTable GetSubCodes(string ClassCode, bool blnIncludeEmpty)
        {
            return GetSubCodes(ClassCode, blnIncludeEmpty, "", "");
        }

        public override DataTable GetSubCodes(string ClassCode, string IncludeAllString, object includeAllValue)
        {
            return GetSubCodes(ClassCode, true, IncludeAllString, includeAllValue);
        }

        public override DataTable GetSubCodes(string ClassCode, bool bIncludeEmpty, string IncludeAllString, object includeAllValue)
        {
            DataTable subCodeTable;

            if (_LoadOnDemand)
            {
                subCodeTable = DBHelper.GetDataTable("AP_SYS_GET_CODE_S", new object[] { _CompanyCode, ClassCode, _Langusge });
            }
            else
            {
                subCodeTable = _AllCodes.Tables[0].Clone();

                DataRow[] returnRows = _AllCodes.Tables[0].Select(string.Format("CD_CLAS = '{0}'", ClassCode));
                foreach (DataRow dr in returnRows)
                {
                    subCodeTable.LoadDataRow(dr.ItemArray, true);
                }
            }

            if (bIncludeEmpty)
            {
                DataRow dr1 = subCodeTable.NewRow();

                dr1["NM_FLAG"] = IncludeAllString;
                dr1["CD_FLAG"] = includeAllValue;
                subCodeTable.Rows.InsertAt(dr1, 0);
            }

            return subCodeTable;
        }
        /// <summary>
        /// CommonCode Return Filtering
        /// </summary>
        /// <param name="strClassCode">ClassCode</param>
        /// <param name="bIncludeEmpty">첫출 공백포함여부</param>
        /// <param name="IncludeAllString">표시문구("" 사용시 전체 사용 않함)</param>
        /// <param name="includeAllValue">Value Member</param>
        /// <param name="filterExpression">필터링 Query(Item1,Item1,Item1,Item1,Item1,ExtendInt,Extendfloat,Extendbit)</param>
        /// <returns></returns>
        public override DataTable GetSubCodes(string ClassCode, bool bIncludeEmpty, string IncludeAllString, object includeAllValue, string filterExpression, string DisplayMember, string ValueMember)
        {
            DataTable subCodeTable;

            if (_LoadOnDemand)
            {
                subCodeTable = DBHelper.GetDataTable("AP_SYS_GET_CODE_S", new object[] { _CompanyCode, ClassCode, _Langusge });
            }
            else
            {
                subCodeTable = _AllCodes.Tables[0].Clone();
                DataRow[] returnRows;
                if (filterExpression == "")
                    returnRows = _AllCodes.Tables[0].Select(string.Format("CD_CLAS = '{0}'", ClassCode));
                else
                    returnRows = _AllCodes.Tables[0].Select(string.Format("CD_CLAS = '{0}' and {1}", ClassCode, filterExpression));


                foreach (DataRow dr in returnRows)
                {
                    subCodeTable.LoadDataRow(dr.ItemArray, true);
                }
            }

            if (bIncludeEmpty)
            {
                DataRow dr1 = subCodeTable.NewRow();

                dr1["NM_FLAG"] = IncludeAllString;
                dr1["CD_FLAG"] = includeAllValue;

                subCodeTable.Rows.InsertAt(dr1, 0);
            }

            subCodeTable.Columns["NM_FLAG"].ColumnName = DisplayMember == "" ? "NM_FLAG" : DisplayMember;
            subCodeTable.Columns["CD_FLAG"].ColumnName = ValueMember == "" ? "CD_FLAG" : ValueMember;

            return subCodeTable;
        }

        #endregion
    }
}
