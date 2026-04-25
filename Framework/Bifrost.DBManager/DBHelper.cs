
using System;
using System.Data;
using System.Threading.Tasks;

using Bifrost.Common;
using Bifrost.Common.Util;

namespace Bifrost
{
    public class DBHelper
    {
        //private static string _iniUseWebService = string.Empty;
        //private static bool _IsWebService = false;

        /// <summary>
        /// 웹서비스 사용여부체크
        /// </summary>
        //public static bool IsWebService
        //{
        //    get
        //    {
        //        if(_iniUseWebService == string.Empty)
        //        {
        //            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
        //            IniFile inifile = new IniFile();
        //            _IsWebService = inifile.IniReadValue("UpgradeServer", "WebService", Path) == "Y" ? true : false;
        //        }
        //        return _IsWebService;
        //    }
        //}

        #region Reset
        public static void Reset()
        {
            DB.ReSet();
        }

        public static void Reset(string connectionString)
        {
            DB.ReSet(connectionString);
        }
        #endregion Reset

        #region ExecuteScalar

        public static object ExecuteScalar(string spName, object[] parameters)
        {
            DbAgent agent = DbAgent.GetInstance();
            return agent.ExecuteScalar(spName, CommandType.StoredProcedure, parameters);
        }

        public static object ExecuteScalar(SpInfo si)
        {
            DbAgent agent = DbAgent.GetInstance();

            string spName = si.SpNameSelect;
            object[] paras = si.SpParamsSelect;

            return agent.ExecuteScalar(spName, CommandType.StoredProcedure, paras);
        }

        public static object ExecuteScalar(string query)
        {
            DbAgent agent = DbAgent.GetInstance();
            return agent.ExecuteScalar(query, CommandType.Text, null);
        }
        #endregion

        #region ExecuteScalarAsync

        public static async Task<object> ExecuteScalarAsync(string spName, object[] parameters)
        {
            DbAgent agent = DbAgent.GetInstance();
            return await agent.ExecuteScalarAsync(spName, CommandType.StoredProcedure, parameters);
        }

        public static async Task<object> ExecuteScalarAsync(SpInfo si)
        {
            DbAgent agent = DbAgent.GetInstance();

            string spName = si.SpNameSelect;
            object[] paras = si.SpParamsSelect;

            return await agent.ExecuteScalarAsync(spName, CommandType.StoredProcedure, paras);
        }

        public static async Task<object> ExecuteScalarAsync(string query)
        {
            DbAgent agent = DbAgent.GetInstance();
            return await agent.ExecuteScalarAsync(query, CommandType.Text, null);
        }
        #endregion

        #region ExecuteNonQuery

        /// <param name="query"></param>
        /// <returns></returns>
        public static object ExecuteNonQuery(string query)
        {
            DbAgent agent = DbAgent.GetInstance();
            object obj = null;

            agent.BeginTransaction();

            obj = agent.ExecuteNonQuery(query, null);

            agent.CommitTransaction();
            return obj;
        }


        /// <param name="spName">저장프로시저명</param>
        /// <param name="parameters">저장프로시저 파라미터, 파라미터가 없으면 null을 넘김</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string spName, object[] parameters)
        {
            DbAgent agent = DbAgent.GetInstance();

            agent.BeginTransaction();
            ResultData result = agent.FillResultTable(spName, System.Data.CommandType.StoredProcedure, parameters);
            agent.CommitTransaction();
            return result.Result;
        }

        /// <param name="spName">저장프로시저명</param>
        /// <param name="parameters">저장프로시저 파라미터, 파라미터가 없으면 null을 넘김</param>
        /// <param name="outParameters">저장프로시저 output 파라미터</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string spName, object[] parameters, out object[] outParameters)
        {
            DbAgent agent = DbAgent.GetInstance();

            agent.BeginTransaction();
            ResultData resultData = agent.FillResultTable(spName, System.Data.CommandType.StoredProcedure, parameters);
            agent.CommitTransaction();

            if (resultData == null)
            {
                outParameters = null;
                return false;
            }

            if (resultData.OutParamsSelect == null || resultData.OutParamsSelect.Rows == null || resultData.OutParamsSelect.Rows.Count == 0)
                outParameters = null;
            else
            {
                outParameters = new object[resultData.OutParamsSelect.ColCount];

                for (int c = 0; c < resultData.OutParamsSelect.ColCount; c++)
                {
                    outParameters[c] = resultData.OutParamsSelect[0, c];
                }
            }

            return resultData.Result;
        }

        #endregion

        #region ExecuteNonQueryAsync
        public static async Task<object> ExecuteNonQueryAsync(string query)
        {
            DbAgent agent = DbAgent.GetInstance();
            object obj = null;

            agent.BeginTransaction();
            obj = await agent.ExecuteNonQueryAsync(query, null);
            agent.CommitTransaction();
            return obj;
        }

        public static async Task<object> ExecuteNonQueryAsync(string spName, object[] parameters)
        {
            DbAgent agent = DbAgent.GetInstance();
            object obj = null;

            agent.BeginTransaction();
            obj = await agent.ExecuteNonQueryAsync(spName, System.Data.CommandType.StoredProcedure, parameters);
            agent.CommitTransaction();
            return obj;
        }
        #endregion

        #region GetDataTable

        /// <summary>
        /// DataTable 리턴
        /// </summary>
        /// <param name="query">SELECT 쿼리문</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string query)
        {
            return GetDataTable(query, true);
        }

        public static DataTable GetDataTable(string query, bool RemainLog)
        {
            DbAgent agent = DbAgent.GetInstance();
            return agent.FillDataTable(query, RemainLog);
        }


        /// <summary>
        /// DataTable 리턴
        /// </summary>
        /// <param name="spName">저장프로시저명</param>
        /// <param name="parameters">저장프로시저 파라미터</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, object[] parameters)
        {
            return GetDataTable(spName, parameters, true);
        }

        public static DataTable GetDataTable(string spName, object[] parameters, bool remainLog)
        {
            DbAgent agent = DbAgent.GetInstance();

            SpInfo si = new SpInfo();
            si.SpNameSelect = spName;
            si.SpParamsSelect = parameters;

            ResultData resultData = agent.FillDataTable(si, remainLog) as ResultData;

            if (resultData == null)
                return null;

            return resultData.DataValue as DataTable;
        }

        /// <summary>
        /// DataTable 리턴
        /// </summary>
        /// <param name="spName">저장프로시저명</param>
        /// <param name="parameters">저장프로시저 파라미터, 파라미터가 없으면 null을 넘김</param>
        /// <param name="outParameters">저장프로시저 output 파라미터</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, object[] parameters, out object[] outParameters)
        {
            DbAgent agent = DbAgent.GetInstance();

            SpInfo si = new SpInfo();
            si.SpNameSelect = spName;
            si.SpParamsSelect = parameters;
            ResultData resultData = agent.FillDataTable(si) as ResultData;

            if (resultData == null)
            {
                outParameters = null;
                return null;
            }

            if (resultData.OutParamsSelect == null || resultData.OutParamsSelect.Rows == null || resultData.OutParamsSelect.Rows.Count == 0)
                outParameters = null;
            else
            {
                outParameters = new object[resultData.OutParamsSelect.ColCount];

                for (int c = 0; c < resultData.OutParamsSelect.ColCount; c++)
                    outParameters[c] = resultData.OutParamsSelect[0, c];
            }

            return resultData.DataValue as DataTable;
        }

        /// <summary>
        /// DataTable 리턴
        /// </summary>
        /// <param name="spName">저장프로시저명</param>
        /// <param name="parameters">저장프로시저 파라미터</param>
        /// <param name="Sort">정렬(ex NO_DOCU ASC, CD_PC DESC)</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, object[] parameters, string Sort)
        {
            DbAgent agent = DbAgent.GetInstance();

            SpInfo si = new SpInfo();
            si.SpNameSelect = spName;
            si.SpParamsSelect = parameters;
            ResultData resultData = agent.FillDataTable(si) as ResultData;

            if (resultData == null)
                return null;

            DataTable dt = resultData.DataValue as DataTable;
            dt.DefaultView.Sort = Sort;
            dt = dt.DefaultView.ToTable();
            dt.AcceptChanges();
            
            return dt;
        }

        /// <summary>
        /// DataTable 리턴
        /// </summary>
        /// <param name="spName">저장프로시저명</param>
        /// <param name="parameters">저장프로시저 파라미터, 파라미터가 없으면 null을 넘김</param>
        /// <param name="outParameters">저장프로시저 output 파라미터</param>
        /// <param name="Sort">정렬(ex NO_DOCU ASC, CD_PC DESC)</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, object[] parameters, out object[] outParameters, string Sort)
        {
            DbAgent agent = DbAgent.GetInstance();

            SpInfo si = new SpInfo();
            si.SpNameSelect = spName;
            si.SpParamsSelect = parameters;
            ResultData resultData = agent.FillDataTable(si) as ResultData;

            if (resultData == null)
            {
                outParameters = null;
                return null;
            }

            if (resultData.OutParamsSelect == null || resultData.OutParamsSelect.Rows == null || resultData.OutParamsSelect.Rows.Count == 0)
                outParameters = null;
            else
            {
                outParameters = new object[resultData.OutParamsSelect.ColCount];

                for (int c = 0; c < resultData.OutParamsSelect.ColCount; c++)
                    outParameters[c] = resultData.OutParamsSelect[0, c];
            }

            DataTable dt = resultData.DataValue as DataTable;
            dt.DefaultView.Sort = Sort;
            dt = dt.DefaultView.ToTable();
            dt.AcceptChanges();

            return dt;
        }

        #endregion

        #region GetDataSet

        public static DataSet GetDataSet(string query)
        {
            DbAgent agent = DbAgent.GetInstance();

            if (query == "")
                throw new Exception("쿼리를 넘겨주세요.");

            return agent.FillDataSet(query);
        }

        public static DataSet GetDataSet(string spName, object[] parameters)
        {
            DbAgent agent = DbAgent.GetInstance();

            if (spName == "")
                throw new Exception("프로시저명을 넘겨야 합니다.");

            ResultData resultData = agent.FillResultSet(spName, parameters) as ResultData;
            return resultData.DataValue as DataSet;
        }

        public static DataSet GetDataSet(string[] spNames, object[][] parameters)
        {
            DbAgent agent = DbAgent.GetInstance();

            if (spNames == null || spNames.Length == 0)
                throw new Exception("프로시저명을 넘겨야 합니다.");

            SpInfoCollection spc = new SpInfoCollection();

            for (int i = 0; i < spNames.Length; i++)
            {
                if (spNames[i] == "")
                    throw new Exception("프로시저명을 넘겨야 합니다.");

                SpInfo si = new SpInfo();
                si.SpNameSelect = spNames[i];
                if (parameters != null && parameters.Length != 0)
                {
                    if (parameters[i] != null)
                        si.SpParamsSelect = parameters[i];
                }
                spc.Add(si);
            }

            return agent.FillDataSet(spc) as DataSet;
        }

        public static DataSet GetDataSet(string spName, object[] parameters, string[] Sort)
        {
            DbAgent agent = DbAgent.GetInstance();

            if (spName == "")
                throw new Exception("프로시저명을 넘겨야 합니다.");

            ResultData resultData = agent.FillResultSet(spName, parameters) as ResultData;
            DataSet ds = resultData.DataValue as DataSet;

            int k = 0;
            DataSet Newds = new DataSet();
            DataTable Newdt = new DataTable();

            foreach (DataTable dt in ds.Tables)
            {
                Newdt = dt.Copy();
                if (k < Sort.Length)
                {
                    //컬럼이 없을경우 무시
                    if (dt.Columns[Sort[k]] != null)
                    {
                        dt.DefaultView.Sort = Sort[k];
                        Newdt = dt.DefaultView.ToTable();
                        Newdt.AcceptChanges();
                    }
                }
                Newds.Tables.Add(Newdt);
                k++;
            }

            return Newds;
        }

        public static DataSet GetDataSet(string[] spNames, object[][] parameters, string[] Sort)
        {
            DbAgent agent = DbAgent.GetInstance();

            if (spNames == null || spNames.Length == 0)
                throw new Exception("프로시저명을 넘겨야 합니다.");

            SpInfoCollection spc = new SpInfoCollection();

            for (int i = 0; i < spNames.Length; i++)
            {
                if (spNames[i] == "")
                    throw new Exception("프로시저명을 넘겨야 합니다.");

                SpInfo si = new SpInfo();
                si.SpNameSelect = spNames[i];
                if (parameters != null && parameters.Length != 0)
                {
                    if (parameters[i] != null)
                        si.SpParamsSelect = parameters[i];
                }
                spc.Add(si);
            }

            DataSet ds = agent.FillDataSet(spc) as DataSet;

            int k = 0;
            DataSet Newds = new DataSet();
            DataTable Newdt = new DataTable();

            foreach (DataTable dt in ds.Tables)
            {
                Newdt = dt.Copy();
                if (k < Sort.Length)
                {
                    dt.DefaultView.Sort = Sort[k];
                    Newdt = dt.DefaultView.ToTable();
                    Newdt.AcceptChanges();
                }
                Newds.Tables.Add(Newdt);
                k++;
            }

            return Newds;
        }

        #endregion

        #region Save
        public static bool Save(SpInfo si)
        {
            if (si == null || si.DataValue == null)
                return true;

            DbAgent agent = DbAgent.GetInstance();

            agent.BeginTransaction();
            ResultData retValue = agent.Save(si);
            agent.CommitTransaction();

            return retValue.Result;

        }

        public static bool Save(SpInfoCollection spCollection)
        {
            if (spCollection == null || spCollection.Count == 0)
                return true;

            DbAgent agent = DbAgent.GetInstance();

            agent.BeginTransaction();
            ResultData[] resultData = agent.Save(spCollection);
            agent.CommitTransaction();


            if(resultData == null)
                return false;

            for (int i = 0; i < resultData.Length; i++)
            {

                if (!resultData[i].Result)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Set Default Value
        public static void SetDefaultValue(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                SetDefaultValue(dt);
            }
        }

        public static void SetDefaultValue(DataTable dt)
        {
            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == typeof(int))
                    col.DefaultValue = 0;
                else if (col.DataType == typeof(decimal))
                    col.DefaultValue = 0.00;
            }
        }
        #endregion Set Default Value
    }

}
