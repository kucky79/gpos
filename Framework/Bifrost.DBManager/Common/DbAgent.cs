using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

using Bifrost.Common.Util;

namespace Bifrost.Common
{
    public class DbAgent
    {
        private DbTransaction _tran = null;
        private DbConnection _connection = null;
        private DbCommand _cmd = null;
        private ParameterCache _pCache = null;

        private string _connectionString = string.Empty;
        private static DbAgent _instance = null;

        private DbAgent()
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            IniFile inifile = new IniFile();

            SimpleEncryptor encryptor = new SimpleEncryptor("greenpos");


            //_connectionString = inifile.IniReadValue("DB", "ConnectionString", Path);
            //if (_connectionString == "")
            {
                //default 
                inifile.IniWriteValue("DB", "OldConnectionString", encryptor.Encryptor(DesType.Encrypt, "Data Source=localhost;initial Catalog=GPOS;uid=YOUR_USER;password=YOUR_PASSWORD;"), Path);
                _connectionString = encryptor.Encryptor(DesType.Decrypt, inifile.IniReadValue("DB", "ConnectionString", Path));
            }

            //_connectionString = inifile.IniReadValue("DB", "ConnectionString", Path);
        }

        public DbAgent(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected internal static DbAgent GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbAgent();
            }

            return _instance;
        }

        protected internal static void ReSet()
        {
            _instance = new DbAgent();
        }

        protected internal static void ReSet(string connectionString)
        {
            _instance = new DbAgent(connectionString);
        }

        private void CreateConnection()
        {
            if (_connection != null)
                return;

            _connection = new SqlConnection(_connectionString);
        }

        private void CreateCommand()
        {
            if (_cmd != null)
                return;

            _cmd = new SqlCommand();

            if (_connection == null)
                CreateConnection();

            _cmd.Connection = _connection;
            _cmd.CommandTimeout = 12000; // 12000초

            // COM+ 에서는 필요없음
            if (_tran != null)
                _cmd.Transaction = _tran;
        }

        private DbDataAdapter GetDataAdapter()
        {
            return new SqlDataAdapter();
        }

        protected internal void BeginTransaction()
        {
            if (_tran != null)
                return;

            try
            {
                if (_connection == null)
                    CreateConnection();

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                _tran = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch
            {
                _connection.Close();

                throw;
            }
        }

        protected internal void CommitTransaction()
        {
            if (_tran == null)
                return;

            try
            {
                _tran.Commit();
            }
            catch
            {
                RollbackTransaction();

                throw;
            }
            finally
            {
                _connection.Close();
                _tran.Dispose();
                _tran = null;
            }
        }

        protected internal void RollbackTransaction()
        {
            if (_tran == null)
                return;

            try
            {
                _tran.Rollback();
            }
            catch
            {
            }
            finally
            {
                _connection.Close();
                _tran.Dispose();
                _tran = null;
            }
        }

        void ResetCommand(string query, CommandType cmdType)
        {
            if (_connection == null) CreateConnection();
            if (_cmd == null) CreateCommand();

            _cmd.Connection = _connection;
            _cmd.CommandText = query;
            _cmd.CommandType = cmdType;
            if (_tran != null)
                _cmd.Transaction = _tran;

            if (_cmd.Parameters != null) _cmd.Parameters.Clear();
            if (_connection.State == ConnectionState.Closed) _connection.Open();
        }

        private DbParameter[] GetParameters(string spName)
        {
            if (_pCache == null)
            {
                _pCache = new ParameterCache();
            }

            ResetCommand("USP_PARAMS_S", CommandType.StoredProcedure);

            return _pCache.GetParameters(ref _cmd, spName);
        }

        private void PrepareCommand(string cmdText, CommandType cmdType, object[] cmdParas)
        {
            if (cmdType == CommandType.StoredProcedure)
            {
                DbParameter[] cacheParas = GetParameters(cmdText);
                ResetCommand(cmdText, cmdType);

                _pCache.CachePrepareCommandSql(ref _cmd, cacheParas, cmdText, cmdType, cmdParas);
            }
            else
            {
                ResetCommand(cmdText, cmdType);
            }
        }

        protected internal ResultData FillResultTable(string spName, CommandType cmdType, object[] cmdParas)
        {
            ResultData result = new ResultData();

            try
            {
                PrepareCommand(spName, cmdType, cmdParas);

                DbDataAdapter da = GetDataAdapter();
                da.SelectCommand = _cmd;
                DataTable dt = new DataTable();

                da.Fill(dt);
                result.DataValue = dt;

                foreach (SqlParameter para in _cmd.Parameters)
                {
                    if (para.Direction == ParameterDirection.Output)
                    {
                        result.OutParamsSelect.ColumnsAdd(para.ParameterName);
                        result.OutParamsSelect[0, para.ParameterName] = para.Value;
                    }
                }

                Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName = " + spName);
                for (int i = 0; i < cmdParas.Length; i++)
                {
                    Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                }

                result.Result = true;
                return result;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        void DoWhenException()
        {
            if(_pCache != null)
                _pCache.DeleteAll();

            if (_tran != null)
                RollbackTransaction();
            else
                _connection.Close();
        }

        void DoWhenException(Exception ex)
        {
            if (_pCache != null)
                _pCache.DeleteAll();

            if (_tran != null)
                RollbackTransaction();
            else
                _connection.Close();

            //throw ex;
            //NFException.HandleUSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
        }

        #region FillDataTable

        protected internal DataTable FillDataTable(string cmdText, CommandType cmdType, object[] cmdParas)
        {
            return FillDataTable(cmdText, cmdType, cmdParas, true);
        }

        protected internal DataTable FillDataTable(string cmdText, CommandType cmdType, object[] cmdParas, bool remainLog)
        {
            try
            {
                PrepareCommand(cmdText, cmdType, cmdParas);

                DbDataAdapter da = GetDataAdapter();
                da.SelectCommand = _cmd;
                DataTable dt = new DataTable();
                dt.RemotingFormat = SerializationFormat.Binary;
                da.Fill(dt);

                //if(remainLog)
                //    InsertLogData(MakeQuery(cmdText, cmdParas));

                Debug.WriteLine("■■■■■■■■■■FillDataTable■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName = " + cmdText);
                for (int i = 0; i < cmdParas.Length; i++)
                {
                    Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                }

                return dt;
            }
            catch
            {
                DoWhenException();

                throw;
            }
        }

        protected internal DataTable FillDataTable(string spName, object[] cmdParas)
        {
            try
            {
                PrepareCommand(spName, CommandType.StoredProcedure, cmdParas);

                DbDataAdapter da = GetDataAdapter();
                da.SelectCommand = _cmd;
                DataTable dt = new DataTable();
                dt.RemotingFormat = SerializationFormat.Binary;
                da.Fill(dt);


                Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName = " + spName);
                for (int i = 0; i < cmdParas.Length; i++)
                {
                    Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                }

                return dt;
            }
            catch
            {
                DoWhenException();

                throw;
            }
        }

        protected internal DataTable FillDataTable(string query, bool remainLog)
        {
            try
            {
                PrepareCommand(query, CommandType.Text, null);

                DbDataAdapter da = GetDataAdapter();
                da.SelectCommand = _cmd;
                DataTable dt = new DataTable();

                Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
                Debug.WriteLine("■■■■■Query = " + query);
                da.Fill(dt);

                //if (remainLog)
                //    InsertLogData(query.Replace("'", "''"));
                return dt;
            }
            catch (SqlException ex)
            {
                DoWhenException();
                throw ex;
            }
        }

        protected internal DataTable FillDataTable(string query)
        {
            try
            {
                PrepareCommand(query, CommandType.Text, null);

                DbDataAdapter da = GetDataAdapter();
                da.SelectCommand = _cmd;
                DataTable dt = new DataTable();

                Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
                Debug.WriteLine("■■■■■Query = " + query);

                da.Fill(dt);
                //InsertLogData(query.Replace("'", "''"));

                return dt;
            }
            catch (SqlException ex)
            {
                DoWhenException();
                throw;
            }
        }

        protected internal ResultData FillDataTable(SpInfo spInfo)
        {
            return FillDataTable(spInfo, true);
        }

        protected internal ResultData FillDataTable(SpInfo spInfo, bool remainLog)
        {
            ResultData result = new ResultData();

            try
            {
                string spName = spInfo.SpNameSelect;
                object[] paras = spInfo.SpParamsSelect;

                DataTable dt = FillDataTable(spName, CommandType.StoredProcedure, paras, remainLog);

                result.DataValue = dt;

                result.Result = true;
                return result;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        #endregion FillDataTable


        protected internal object ExecuteScalar(string cmdText, CommandType cmdType, object[] cmdParas)
        {
            try
            {
                PrepareCommand(cmdText, cmdType, cmdParas);

                object objValue = _cmd.ExecuteScalar();


                Debug.WriteLine("■■■■■■■■■■ExecuteScalar■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName : {0}", cmdText);
                if (cmdParas != null)
                {
                    for (int i = 0; i < cmdParas.Length; i++)
                    {
                        Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                    }
                }
                if (objValue != DBNull.Value)
                    return objValue;
                else
                    return null;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        protected internal async Task<object> ExecuteScalarAsync(string cmdText, CommandType cmdType, object[] cmdParas)
        {
            try
            {
                PrepareCommand(cmdText, cmdType, cmdParas);

                object objValue = await _cmd.ExecuteScalarAsync();


                Debug.WriteLine("■■■■■■■■■■ExecuteScalarAsync■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName : {0}", cmdText);
                if (cmdParas != null)
                {
                    for (int i = 0; i < cmdParas.Length; i++)
                    {
                        Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                    }
                }
                if (objValue != DBNull.Value)
                    return objValue;
                else
                    return null;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }


        /// <summary>
        /// 2016.12.21
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdParas"></param>
        /// <returns></returns>
        protected internal object ExecuteNonQuery(string cmdText, object[] cmdParas)
        {
            try
            {
                PrepareCommand(cmdText, CommandType.Text, cmdParas);

                object objValue = _cmd.ExecuteNonQuery();


                Debug.WriteLine("■■■■■■■■■■ExecuteNonQuery■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName : {0}", cmdText);
                if (cmdParas != null)
                {
                    for (int i = 0; i < cmdParas.Length; i++)
                    {
                        Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                    }
                }
                if (objValue != DBNull.Value)
                    return objValue;
                else
                    return null;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        protected internal async Task<object> ExecuteNonQueryAsync(string cmdText, object[] cmdParas)
        {
            try
            {
                PrepareCommand(cmdText, CommandType.Text, cmdParas);
                object objValue = await _cmd.ExecuteNonQueryAsync();
                Debug.WriteLine("■■■■■■■■■■ExecuteNonQuery■■■■■■■■■■");
                Debug.WriteLine("■■■■■Qeury : {0}", cmdText);
                if (cmdParas != null)
                {
                    for (int i = 0; i < cmdParas.Length; i++)
                    {
                        Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                    }
                }
                if (objValue != DBNull.Value)
                    return objValue;
                else
                    return null;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        protected internal async Task<object> ExecuteNonQueryAsync(string spName, CommandType cmdType, object[] cmdParas)
        {
            try
            {
                PrepareCommand(spName, CommandType.StoredProcedure, cmdParas);
                object objValue = await _cmd.ExecuteNonQueryAsync();
                Debug.WriteLine("■■■■■■■■■■ExecuteNonQuery■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName : {0}", spName);
                if (cmdParas != null)
                {
                    for (int i = 0; i < cmdParas.Length; i++)
                    {
                        Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, cmdParas[i] });
                    }
                }
                if (objValue != DBNull.Value)
                    return objValue;
                else
                    return null;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        #region FillDataSet
        protected internal DataSet FillDataSet(SpInfoCollection spCollection)
        {
            if (spCollection == null)
                return null;

            DataSet ds = new DataSet();

            try
            {
                foreach (SpInfo si in spCollection)
                {
                    string spName = si.SpNameSelect;
                    object[] paras = si.SpParamsSelect;

                    DataTable dt = FillDataTable(spName, CommandType.StoredProcedure, paras);
                    ds.Tables.Add(dt);

                    //ExecuteNonQuery("SYS_TRANSACTION_LOG", paras);

                    Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
                    Debug.WriteLine("■■■■■spName = " + spName);
                    for (int i = 0; i < paras.Length; i++)
                    {
                        Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, paras[i] });
                    }
                }

                return ds;
            }
            catch
            {
                DoWhenException();

                throw;
            }
        }


        protected internal DataSet FillDataSet(string query)
        {
            try
            {
                PrepareCommand(query, CommandType.Text, null);

                DbDataAdapter da = GetDataAdapter();
                da.SelectCommand = _cmd;
                DataSet ds = new DataSet();

                da.Fill(ds);

                Debug.WriteLine("■■■■■■■■■■FillDataSet by query■■■■■■■■■■");
                Debug.WriteLine("■■■■■query = " + query);

                return ds;
            }
            catch
            {
                DoWhenException();

                throw;
            }
        }
        #endregion FillDataSet


        protected internal ResultData FillResultSet(string spName, object[] paras)
        {
            ResultData result = new ResultData();

            try
            {
                PrepareCommand(spName, CommandType.StoredProcedure, paras);

                DbDataAdapter da = GetDataAdapter();
                da.SelectCommand = _cmd;
                DataSet ds = new DataSet();

                da.Fill(ds);
                result.DataValue = ds;

                //InsertLogData(MakeQuery(spName, paras));
                foreach (SqlParameter para in _cmd.Parameters)
                {
                    if (para.Direction == ParameterDirection.Output)
                    {
                        result.OutParamsSelect.ColumnsAdd(para.ParameterName);
                        result.OutParamsSelect[0, para.ParameterName] = para.Value;
                    }
                }

                Debug.WriteLine("■■■■■■■■■■FillDataSet■■■■■■■■■■");
                Debug.WriteLine("■■■■■spName = " + spName);
                for (int i = 0; i < paras.Length; i++)
                {
                    Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { i, paras[i] });
                }

                result.Result = true;
                return result;
            }
            catch
            {
                DoWhenException();

                throw;
            }
        }

        protected internal ResultData[] Save(SpInfoCollection sic)
        {
            ResultData[] result = new ResultData[sic.Count];

            int i = 0;

            try
            {
                foreach (SpInfo si in sic)
                {
                    result[i] = Save(si);
                    i++;
                }

                return result;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        protected internal ResultData Save(SpInfo si)
        {
            ResultData result = new ResultData();

            try
            {
                if (si != null)
                {
                    DataTable dtOrigin = ((DataTable)si.DataValue);
                    DataTable dtTemp = null;

                    Debug.WriteLine("■■■■■■■■■■Save■■■■■■■■■■");
                    //Debug.WriteLine("■■■■■SpNameInsert = " + si.SpNameInsert);
                    //for (int i = 0; i < dtOrigin.Columns.Count; i++)
                    //{
                    //    Debug.WriteLine("■■■■■{0} = {1}" ,new object[] { si.SpParamsInsert[i], dtOrigin.Columns[si.SpParamsInsert[i].ToString()].ToString() });
                    //}
                    //Debug.WriteLine("■■■■■SpNameUpdate = " + si.SpNameUpdate);
                    //for (int i = 0; i < dtOrigin.Columns.Count; i++)
                    //{
                    //    Debug.WriteLine("■■■■■{0} = {1}", new object[] { si.SpParamsUpdate[i], dtOrigin.Columns[i].ToString() });
                    //}
                    //Debug.WriteLine("■■■■■SpNameDelete = " + si.SpNameDelete);
                    //for (int i = 0; i < dtOrigin.Columns.Count; i++)
                    //{
                    //    Debug.WriteLine("■■■■■{0} = {1}", new object[] { si.SpParamsDelete[i], dtOrigin.Columns[i].ToString() });
                    //}

                    if (dtOrigin != null)
                    {
                        dtTemp = dtOrigin.Copy();

                        SetDefaultValue(ref si);

                        // RowState에 적용받지 않고 실행하는 경우...
                        if (si.DataState != DataValueState.NoAccept)
                        {
                            switch (si.DataState)
                            {
                                case DataValueState.Added:
                                    if (si.SpParamsValues != null && si.SpParamsValues.Count > 0)
                                    {
                                        SingleValue[] sv = (SingleValue[])si.SpParamsValues[CommonFunction.SpState.Insert];
                                        if (sv != null && sv.Length > 0)
                                            ChangeDataTable(ref dtTemp, sv);
                                    }
                                    result.OutParamsInsert = ExecuteNonQuery(dtTemp, si.SpNameInsert, si.SpParamsInsert);
                                    break;
                                case DataValueState.Deleted:
                                    if (si.SpParamsValues != null && si.SpParamsValues.Count > 0)
                                    {
                                        SingleValue[] sv = (SingleValue[])si.SpParamsValues[CommonFunction.SpState.Delete];
                                        if (sv != null && sv.Length > 0)
                                            ChangeDataTable(ref dtTemp, sv);
                                    }
                                    result.OutParamsDelete = ExecuteNonQuery(dtTemp, si.SpNameDelete, si.SpParamsDelete);
                                    break;
                                case DataValueState.Modified:
                                    if (si.SpParamsValues != null && si.SpParamsValues.Count > 0)
                                    {
                                        SingleValue[] sv = (SingleValue[])si.SpParamsValues[CommonFunction.SpState.Update];
                                        if (sv != null && sv.Length > 0)
                                            ChangeDataTable(ref dtTemp, sv);
                                    }
                                    result.OutParamsUpdate = ExecuteNonQuery(dtTemp, si.SpNameUpdate, si.SpParamsUpdate);
                                    break;
                            }
                        }
                        else  // si.DataState == DataValueState.NoAccept
                        {
                            DataTable dtInsert = null;
                            DataTable dtUpdate = null;
                            DataTable dtDelete = null;

                            if (si.SpNameInsert != null && si.SpNameInsert != string.Empty)
                                dtInsert = dtTemp.GetChanges(DataRowState.Added);

                            if (si.SpNameUpdate != null && si.SpNameUpdate != string.Empty)
                                dtUpdate = dtTemp.GetChanges(DataRowState.Modified);

                            if (si.SpNameDelete != null && si.SpNameDelete != string.Empty)
                                dtDelete = dtTemp.GetChanges(DataRowState.Deleted);

                       

                            // Deleted
                            if (dtDelete != null && dtDelete.Rows.Count > 0)
                            {
                                DataTable dtDeleteTemp = new DataTable();
                                for (int i = 0; i < dtDelete.Columns.Count; i++)
                                    dtDeleteTemp.Columns.Add(dtDelete.Columns[i].ColumnName, dtDelete.Columns[i].DataType);

                                DataRow newrow = null;
                                DataView dvTemp = dtDelete.DefaultView;
                                dvTemp.RowStateFilter = DataViewRowState.Deleted;
                                for (int rowIndex = 0; rowIndex < dvTemp.Count; rowIndex++)
                                {
                                    newrow = dtDeleteTemp.NewRow();
                                    for (int colIndex = 0; colIndex < dtDeleteTemp.Columns.Count; colIndex++)
                                        newrow[colIndex] = dvTemp[rowIndex][colIndex];
                                    dtDeleteTemp.Rows.Add(newrow);
                                }

                                if (si.SpParamsValues != null && si.SpParamsValues.Count > 0)
                                {
                                    SingleValue[] sv = (SingleValue[])si.SpParamsValues[CommonFunction.SpState.Delete];
                                    if (sv != null && sv.Length > 0)
                                        ChangeDataTable(ref dtDeleteTemp, sv);
                                }
                                
                                SetDefaultValue(ref dtDelete, si);
                                result.OutParamsDelete = ExecuteNonQuery(dtDeleteTemp, si.SpNameDelete, si.SpParamsDelete);
                            }

                            // Modified
                            if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                            {
                                if (si.SpParamsValues != null && si.SpParamsValues.Count > 0)
                                {
                                    SingleValue[] sv = (SingleValue[])si.SpParamsValues[CommonFunction.SpState.Update];
                                    if (sv != null && sv.Length > 0)
                                        ChangeDataTable(ref dtUpdate, sv);
                                }

                                SetDefaultValue(ref dtUpdate, si);
                                result.OutParamsUpdate = ExecuteNonQuery(dtUpdate, si.SpNameUpdate, si.SpParamsUpdate);
                            }

                            // Added
                            if (dtInsert != null && dtInsert.Rows.Count > 0)
                            {
                                if (si.SpParamsValues != null && si.SpParamsValues.Count > 0)
                                {
                                    SingleValue[] sv = (SingleValue[])si.SpParamsValues[CommonFunction.SpState.Insert];
                                    if (sv != null && sv.Length > 0)
                                        ChangeDataTable(ref dtInsert, sv);
                                }

                                SetDefaultValue(ref dtInsert, si);
                                result.OutParamsInsert = ExecuteNonQuery(dtInsert, si.SpNameInsert, si.SpParamsInsert);
                            }
                        }
                    }
                    else  // SpInfo 의 DataValue 가 null 인 경우
                    {
                        if (si.SpNameNonQuery != null && si.SpNameNonQuery != string.Empty)
                        {
                            result.OutParamsSelect = ExecuteNonQuery(si.SpNameNonQuery, CommandType.StoredProcedure, si.SpParamsNonQuery);
                            result.Result = true;
                        }
                    }
                }
                
                result.Result = true;
                return result;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        private OutParameters ExecuteNonQuery(string cmdText, CommandType cmdType, object[] paraValues)
        {
            OutParameters outParameters = null;
            
            try
            {
                PrepareCommand(cmdText, cmdType, paraValues);
                _cmd.ExecuteNonQuery();

                foreach (SqlParameter para in _cmd.Parameters)
                {
                    if (para.Direction == ParameterDirection.Output)
                    {
                        if (outParameters == null)
                            outParameters = new OutParameters();

                        outParameters.ColumnsAdd(para.ParameterName);
                        outParameters[0, para.ParameterName] = para.Value;
                    }
                }
                return outParameters;
            }
            catch
            {
                DoWhenException();
                throw;
            }
        }

        private OutParameters ExecuteNonQuery(DataTable dt, string spName, string[] colNames)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return null;

            DbParameter[] cacheParas = GetParameters(spName);

            ResetCommand(spName, CommandType.StoredProcedure);

            OutParameters outParameters = new OutParameters();
            object[] param;
            Debug.WriteLine("■■■■■spName = " + spName);

            for (int dtRow = 0; dtRow < dt.Rows.Count; dtRow++)
            {
                param = new object[colNames.Length];
                for (int i = 0; i < colNames.Length; i++)
                {
                    Debug.WriteLine("■■■■■parameters[{0}] = {1}", new object[] { colNames[i], dt.Rows[dtRow][colNames[i]] });
                    param[i] = dt.Rows[dtRow][colNames[i]];
                }
                Debug.WriteLine("■■■■■------------------------------");
                //InsertLogData(MakeQuery(spName, param));
            }
            Debug.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■");
            try
            {
                return _pCache.CachePrepareCommandSqlForSave(ref _cmd, cacheParas, spName, dt, colNames);
            }
            catch
            {
                DoWhenException();

                throw;
            }
        }

        private void ChangeDataTable(ref DataTable dtTemp, SingleValue[] sv)
        {
            for (int i = 0; i < sv.Length; i++)
            {
                if (!dtTemp.Columns.Contains(sv[i].ColumnName))
                    dtTemp.Columns.Add(sv[i].ColumnName, typeof(string));
                
                //20170605 중복된 Column은 업데이트로 하기위해 주석처리
                //else
                //    throw new System.Data.DuplicateNameException(String.Format("'{0}' 열이 중복되었습니다. ", sv[i].ColumnName));
            }

            //데이터 집어넣기
            for (int index = 0; index < dtTemp.Rows.Count; index++)
            {
                for (int col = 0; col < sv.Length; col++)
                    dtTemp.Rows[index][sv[col].ColumnName.ToString()] = sv[col].DefaultValue;
            }
        }

        private void SetDefaultValue(ref SpInfo si)
        {
            SetDefaultValue(ref si, CommonFunction.SpState.Insert, "CD_STORE", si.FirmCode);
            SetDefaultValue(ref si, CommonFunction.SpState.Insert, "CD_FIRM", si.FirmCode);
            SetDefaultValue(ref si, CommonFunction.SpState.Insert, "CD_USER_REG", si.UserID);
            SetDefaultValue(ref si, CommonFunction.SpState.Insert, "CD_USER_AMD", si.UserID);

            SetDefaultValue(ref si, CommonFunction.SpState.Update, "CD_STORE", si.FirmCode);
            SetDefaultValue(ref si, CommonFunction.SpState.Update, "CD_FIRM", si.FirmCode);
            SetDefaultValue(ref si, CommonFunction.SpState.Update, "CD_USER_REG", si.UserID);
            SetDefaultValue(ref si, CommonFunction.SpState.Update, "CD_USER_AMD", si.UserID);

            SetDefaultValue(ref si, CommonFunction.SpState.Delete, "CD_STORE", si.FirmCode);
            SetDefaultValue(ref si, CommonFunction.SpState.Delete, "CD_FIRM", si.FirmCode);
            SetDefaultValue(ref si, CommonFunction.SpState.Delete, "CD_USER_REG", si.UserID);
            SetDefaultValue(ref si, CommonFunction.SpState.Delete, "CD_USER_AMD", si.UserID);
        }

        private void SetDefaultValue(ref DataTable dt, SpInfo si)
        {
            if (!dt.Columns.Contains("CD_FIRM"))
                dt.Columns.Add(new DataColumn("CD_FIRM", typeof(string)));

            if (!dt.Columns.Contains("CD_STORE"))
                dt.Columns.Add(new DataColumn("CD_STORE", typeof(string)));

            if (!dt.Columns.Contains("CD_USER_REG"))
                dt.Columns.Add(new DataColumn("CD_USER_REG", typeof(string)));

            if (!dt.Columns.Contains("CD_USER_AMD"))
                dt.Columns.Add(new DataColumn("CD_USER_AMD", typeof(string)));

            
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    dr["CD_FIRM"] = si.FirmCode;
                    if (si.FirmCode != string.Empty)
                        dr["CD_STORE"] = si.FirmCode;
                    dr["CD_USER_REG"] = si.UserID;
                    dr["CD_USER_AMD"] = si.UserID;
                }
            }
        }

        private void SetDefaultValue(ref DataTable dt, SpInfo si, bool isPOS)
        {
            if (isPOS)
            {
                if (!dt.Columns.Contains("CD_STORE"))
                    dt.Columns.Add(new DataColumn("CD_STORE", typeof(string)));
            }
            else
            {
                if (!dt.Columns.Contains("CD_FIRM"))
                    dt.Columns.Add(new DataColumn("CD_FIRM", typeof(string)));
            }

            if (!dt.Columns.Contains("CD_USER_REG"))
                dt.Columns.Add(new DataColumn("CD_USER_REG", typeof(string)));

            if (!dt.Columns.Contains("CD_USER_AMD"))
                dt.Columns.Add(new DataColumn("CD_USER_AMD", typeof(string)));


            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (isPOS)
                        dr["CD_STORE"] = si.FirmCode;
                    else
                        dr["CD_FIRM"] = si.FirmCode;

                    dr["CD_USER_REG"] = si.UserID;
                    dr["CD_USER_AMD"] = si.UserID;
                }
            }
        }

        private void SetDefaultValue(ref SpInfo si, CommonFunction.SpState actionState, string colName, string defaultValue)
        {
            string[] spParaColNames = null;

            switch (actionState)
            {
                case CommonFunction.SpState.Insert:
                    spParaColNames = si.SpParamsInsert;
                    break;
                case CommonFunction.SpState.Update:
                    spParaColNames = si.SpParamsUpdate;
                    break;
                case CommonFunction.SpState.Delete:
                    spParaColNames = si.SpParamsDelete;
                    break;
            }

            if (!HasDefaultValue(si.SpParamsValues, actionState, colName))
            {
                if (!Contains((DataTable)si.DataValue, colName) && Contains(spParaColNames, colName))
                {
                    si.SpParamsValues.Add(actionState, colName, defaultValue);
                }
            }
        }

        private bool Contains(DataTable dt, string colName)
        {
            if (dt == null)
                return false;

            return dt.Columns.Contains(colName);
        }

        private bool Contains(string[] svc, string colName)
        {
            if (svc == null)
                return false;

            for (int i = 0; i < svc.Length; i++)
            {
                if (String.Compare(colName, svc[i], true, CultureInfo.InvariantCulture) == 0)
                    return true;
            }

            return false;
        }

        private bool HasDefaultValue(SingleValueCollection svc, CommonFunction.SpState SpState, string colName)
        {
            SingleValue[] sv = svc[SpState];

            if (sv == null)
                return false;

            for (int i = 0; i < sv.Length; i++)
            {
                if (String.Compare(sv[i].ColumnName, colName, true, CultureInfo.InvariantCulture) == 0)
                    return true;
            }

            return false;
        }

        private void InsertLogData(string query, string FirmCode)
        {
            string qurey_Transaction = "INSERT INTO SYS_TRANSACTION_LOG (CD_FIRM, DC_PROC, TM_REG, CD_USER_REG)";
            qurey_Transaction += " VALUES ('" + FirmCode + "', '" + query + "', GETDATE(), '" + FirmCode + "')";
            ExecuteNonQuery(qurey_Transaction, null);
        }

        private string MakeQuery(string spName, object[] param)
        {
            string resultQuery = "EXEC " + spName;
            DbParameter[] cacheParas = GetParameters(spName);

            for (int i = 0; i < cacheParas.Length; i++)
            {
                if (param.Length - 1 < i)
                {
                    resultQuery += " " + cacheParas[i].ToString() + " = null,";
                }
                else
                {
                    resultQuery += " " + cacheParas[i].ToString() + " = ''" + param[i] + "'',";
                }
            }

            if (cacheParas.Length > 0)
            {
                resultQuery = resultQuery.Substring(0, resultQuery.LastIndexOf(','));
            }
             
            return resultQuery;
        }
    }
}
