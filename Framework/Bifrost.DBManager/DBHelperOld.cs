
using System;
using System.Data;
using System.Threading.Tasks;

using Bifrost.Common;
using Bifrost.Common.Util;

namespace Bifrost
{
    public class DBHelperOld
    {
        #region Reset
        public static void Reset()
        {
            DB.ReSet();
        }
        #endregion Reset


        #region ExecuteScalar

        public static object ExecuteScalar(string spName, object[] parameters)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

            return agent.ExecuteScalar(spName, CommandType.StoredProcedure, parameters);
        }

        public static object ExecuteScalar(SpInfo si)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

            string spName = si.SpNameSelect;
            object[] paras = si.SpParamsSelect;

            return agent.ExecuteScalar(spName, CommandType.StoredProcedure, paras);
        }

        public static object ExecuteScalar(string query)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

            return agent.ExecuteScalar(query, CommandType.Text, null);
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 2016.12.21
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static object ExecuteNonQuery(string query)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();
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
            DbAgentOld agent = DbAgentOld.GetInstance();

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
            DbAgentOld agent = DbAgentOld.GetInstance();

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

        #region GetDataTable

        /// <summary>
        /// DataTable 리턴
        /// </summary>
        /// <param name="sql">SELECT 쿼리문</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

            return agent.FillDataTable(sql);
        }

        /// <summary>
        /// DataTable 리턴
        /// </summary>
        /// <param name="spName">저장프로시저명</param>
        /// <param name="parameters">저장프로시저 파라미터</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, object[] parameters)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

            SpInfo si = new SpInfo();
            si.SpNameSelect = spName;
            si.SpParamsSelect = parameters;

            ResultData resultData = agent.FillDataTable(si) as ResultData;

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
            DbAgentOld agent = DbAgentOld.GetInstance();

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
        /// <param name="Sort">정열(ex NO_DOCU ASC, CD_PC DESC)</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, object[] parameters, string Sort)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

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
        /// <param name="Sort">정열(ex NO_DOCU ASC, CD_PC DESC)</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, object[] parameters, out object[] outParameters, string Sort)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

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
            DbAgentOld agent = DbAgentOld.GetInstance();

            if (query == "")
                throw new Exception("쿼리를 넘겨주세요.");

            return agent.FillDataSet(query);
        }

        public static DataSet GetDataSet(string spName, object[] parameters)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

            if (spName == "")
                throw new Exception("프로시저명을 넘겨야 합니다.");

            ResultData resultData = agent.FillResultSet(spName, parameters) as ResultData;
            return resultData.DataValue as DataSet;
        }

        public static DataSet GetDataSet(string[] spNames, object[][] parameters)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

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
            DbAgentOld agent = DbAgentOld.GetInstance();

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
                    dt.DefaultView.Sort = Sort[k];
                    Newdt = dt.DefaultView.ToTable();
                    Newdt.AcceptChanges();
                }
                Newds.Tables.Add(Newdt);
                k++;
            }

            return Newds;
        }

        public static DataSet GetDataSet(string[] spNames, object[][] parameters, string[] Sort)
        {
            DbAgentOld agent = DbAgentOld.GetInstance();

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

            DbAgentOld agent = DbAgentOld.GetInstance();

            agent.BeginTransaction();
            ResultData retValue = agent.Save(si);
            agent.CommitTransaction();

            return retValue.Result;

        }

        public static bool Save(SpInfoCollection spCollection)
        {
            if (spCollection == null || spCollection.Count == 0)
                return true;

            DbAgentOld agent = DbAgentOld.GetInstance();

            agent.BeginTransaction();
            ResultData[] retValue = agent.Save(spCollection);
            agent.CommitTransaction();


            if(retValue == null)
                return false;

            for (int i = 0; i < retValue.Length; i++)
            {

                if (!retValue[i].Result)
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
