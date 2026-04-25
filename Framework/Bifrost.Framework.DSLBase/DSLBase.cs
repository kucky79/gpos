using System;
using System.Data;
using System.Data.SqlClient;

using System.Configuration;
using System.Reflection;
using System.Transactions;
using System.Text;

using Bifrost.Framework;

namespace Bifrost.Framework
{
    public class DSLBase : IDisposable
    {
        #region Privates

        private SubSystemType subType = SubSystemType.FRAMEWORK;

        /// <summary>
        /// 서브팀상태값
        /// </summary>
        private bool bSubStatus = false;

        /// <summary>
        /// 로그레벨
        /// </summary>
        private string strLogLevel = string.Empty;

        //
        // DataSetCommand object
        //
        private SqlDataAdapter dAdapter;

        ///// <summary>
        ///// 특적 DB의 CRUD 등을 구현하기 위한 command 객체들
        ///// </summary>
        protected SqlCommand loadCommand;
        protected SqlCommand insertCommand;
        protected SqlCommand updateCommand;
        protected SqlCommand deleteCommand;

        /// <summary>
        /// DB에 사용되는 SqlConnection
        /// </summary>
        private SqlConnection _connection;

        private void _SetConnection()
        {
            try
            {
                string subtype = "FRAMEWORK";
                if (!string.IsNullOrEmpty(this.GetType().Namespace))
                {
                    string[] typeNamespace = this.GetType().Namespace.Split('.');

                    if (typeNamespace.Length > 1)
                        subtype = typeNamespace[1];
                }

                string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();

                string DBConnectionString = inifile.IniReadValue("DB", "ConnectionString", Path);
                if (DBConnectionString == "")
                {
                    //default 
                    inifile.IniWriteValue("DB", "ConnectionString", "Data Source=localhost; initial Catalog=AIMS2;uid=YOUR_USER;password=YOUR_PASSWORD;", Path);
                    DBConnectionString = inifile.IniReadValue("DB", "ConnectionString", Path);
                }
                //_connection = new SqlConnection(Base.NFDecrypt(ConfigurationManager.AppSettings["DBConnectionString"]));
                _connection = new SqlConnection(DBConnectionString);
                //_connection = new SqlConnection("Data Source = localhost; initial Catalog = HinetERP; uid = YOUR_USER; password = YOUR_PASSWORD;");

            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
        }

        private void _SetConnection(SubSystemType subSysType)
        {
            try
            {
                string strSubType = null;

                strSubType = SystemBase.ReturnFileName(subSysType);

                string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();

                string DBConnectionString = inifile.IniReadValue("DB", "ConnectionString", Path);
                if (DBConnectionString == "")
                {
                    //default 
                    inifile.IniWriteValue("DB", "ConnectionString", "Data Source=localhost;initial Catalog=HinetERP;uid=YOUR_USER;password=YOUR_PASSWORD;", Path);
                    DBConnectionString = inifile.IniReadValue("DB", "ConnectionString", Path);
                }
                _connection = new SqlConnection(DBConnectionString);

                //_connection = new SqlConnection(Base.NFDecrypt(ConfigurationManager.AppSettings["DBConnectionString"]));
                //_connection = new SqlConnection("Data Source = localhost; initial Catalog = SNOTES; uid = YOUR_USER; password = YOUR_PASSWORD;");
                //SqlConnection DBConnectionString = new SqlConnection("Data Source=localhost;initial Catalog=HinetERP;uid=YOUR_USER;password=YOUR_PASSWORD;");
                //_connection = new SqlConnection(DBConnectionString.ToString());

                //_connection = new SqlConnection("Data Source = localhost; initial Catalog = HinetERP; uid = YOUR_USER; password = YOUR_PASSWORD;");
                ////_connection = new SqlConnection(DBConnectionString);

            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
        }

        private SubSystemType SubSystemFromString(string subSysType)
        {
     
            switch (subSysType)
            {
                case "FRAMEWORK":
                    return SubSystemType.FRAMEWORK;
                case "MAS":
                    return SubSystemType.MAS;
                case "INT":
                    return SubSystemType.INT;
                case "SEA":
                    return SubSystemType.SEA;
                default:
                    return SubSystemType.FRAMEWORK;
            }
        }

        #endregion Privates

        #region Constructors
        /// <summary>
        /// 일반 생성자
        /// </summary>
        public DSLBase()
        {
            //
            // Create the DataSetCommand
            //
            _SetConnection();
            dAdapter = new SqlDataAdapter();

            ///
            /// Retrieve SubSystemType
            /// 
            string[] typeNamespace = this.GetType().Namespace.Split('.');
            if (typeNamespace.Length > 1)
                subType = SubSystemFromString(typeNamespace[1]);

            if (LogHandler.LogSubSystem)
            {
                strLogLevel = ConfigurationManager.AppSettings["LogLevel-" + typeNamespace[1]];
                bSubStatus = (strLogLevel == "ON"  ? true : false);
            }


            /// 
            /// default to korea
            /// 
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ko-KR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ko-KR");
        }

        /// <summary>
        /// 특정 시스템의 Connection을 얻어 오기 위한 생성자
        /// </summary>
        public DSLBase(SubSystemType subSystem)
        {
            //
            // Create the DataSetCommand
            //
            _SetConnection(subSystem);
            dAdapter = new SqlDataAdapter();

            ///
            /// Retrieve SubSystemType
            /// 
            subType = subSystem;
            
            if (LogHandler.LogSubSystem)
            {                
                strLogLevel = ConfigurationManager.AppSettings["LogLevel-" + subType.ToString("f")];
                bSubStatus = (strLogLevel == "ON" ? true : false);
            }
        }

        /// <summary>
        ///     Dispose of this object's resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }


        #endregion

        #region Protected

        #region Disposing

        /// <summary>
        ///		Free the instance variables of this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return; // we're being collected, so let the GC take care of this object

            if (loadCommand != null)
            {
                if (loadCommand.Connection != null)
                {
                    loadCommand.Parameters.Clear();
                    loadCommand.Connection.Dispose();
                }
                loadCommand.Dispose();
            }

            if (insertCommand != null)
            {
                insertCommand.Parameters.Clear();
                insertCommand.Dispose();
            }

            if (updateCommand != null)
            {
                updateCommand.Parameters.Clear();
                updateCommand.Dispose();
            }

            if (deleteCommand != null)
            {
                deleteCommand.Parameters.Clear();
                deleteCommand.Dispose();
            }

            if (_connection != null)
                _connection.Dispose();

            if (dAdapter != null)
            {
                dAdapter.Dispose();
                dAdapter = null;
            }
        }

        #endregion

        #region 로깅셋팅
        /// <summary>
        /// 로깅시작
        /// </summary>
        /// <param name="ts">TimeStamp개체</param>
        /// <example>
        /// TimeStamp를 넘겨서 로깅을 시작한다.
        /// <code>
        ///		private void test()
        ///		{
        ///			//로깅시작
        ///			TimeStamp ts = null;
        ///			LoggingStart(ref ts);
        ///
        ///			TestBSLClass_Nx oClass = null;
        ///			try
        ///			{
        ///				using(oClass = new TestBSLClass_Nx())
        ///				{
        ///					DataGrid1.DataSource = oClass.GetData();
        ///					DataGrid1.DataBind();
        ///					SetFocus(TextBox1.ID.ToString());
        ///				}
        ///		
        ///			}
        ///			catch(Exception ex)
        ///			{
        ///				this.errorMessage = ex.ToString();
        ///			}
        ///			finally
        ///			{
        ///				//로깅끝
        ///				LoggingEnd(ts,this,MethodInfo.GetCurrentMethod().Name);
        ///			}
        ///		}
        /// </code>
        /// </example>
        protected void LoggingStart(ref TimeStamp ts)
        {
            //Comment
            try
            {
                if (bSubStatus)
                {
                    ts = new TimeStamp();
                    ts.TimeStampStart();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 로깅끝
        /// </summary>
        /// <param name="ts">TimeStamp개체</param>
        /// <param name="target">this</param>
        /// <param name="sServiceName">서비스이름</param>
        /// <example>
        /// TimeStamp를 넘겨서 로깅을 종료한다.
        /// <code>
        ///		private void test()
        ///		{
        ///			//로깅시작
        ///			TimeStamp ts = null;
        ///			LoggingStart(ref ts);
        ///
        ///			TestBSLClass_Nx oClass = null;
        ///			try
        ///			{
        ///				using(oClass = new TestBSLClass_Nx())
        ///				{
        ///					DataGrid1.DataSource = oClass.GetData();
        ///					DataGrid1.DataBind();
        ///					SetFocus(TextBox1.ID.ToString());
        ///				}
        ///		
        ///			}
        ///			catch(Exception ex)
        ///			{
        ///				this.errorMessage = ex.ToString();
        ///			}
        ///			finally
        ///			{
        ///				//로깅끝
        ///				LoggingEnd(ts,this,MethodInfo.GetCurrentMethod().Name);
        ///			}
        ///		}
        /// </code>
        /// </example>
        protected void LoggingEnd(TimeStamp ts, object target, string sServiceName)
        {
            //Comment
            try
            {
                if (bSubStatus && ts != null)
                {
                    ts.TimeStampEnd(target, sServiceName);
                    //					if (ts != null)	ts.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AddSqlParameter

        /// <summary>
        /// Add a SqlParameter to Sql command indicated by sqlCmdType
        /// </summary>
        /// <param name="sqlCmdType"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        protected SqlParameter AddSqlParameter(SqlCommandType sqlCmdType, string paramName, object paramValue)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            SqlParameter parm = null;
            try
            {
                parm = new SqlParameter(paramName, paramValue);
                parm.Direction = ParameterDirection.Input;

                switch (sqlCmdType)
                {
                    case SqlCommandType.SelectCommand:
                        if (loadCommand == null) throw new Exception("You must use SetSqlCommand first.");
                        loadCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.InsertCommand:
                        if (insertCommand == null) throw new Exception("You must use InsertCommand first.");
                        insertCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.UpdateCommand:
                        if (updateCommand == null) throw new Exception("You must use UpdateCommand first.");
                        updateCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.DeleteCommand:
                        if (deleteCommand == null) throw new Exception("You must use DeleteCommand first.");
                        deleteCommand.Parameters.Add(parm);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return parm;
        }

        /// <summary>
        /// Add a Sql parameter to Sql command, setting by SetSqlCommand
        /// </summary>
        /// <param name="sqlCmdType"></param>
        /// <param name="paramName"></param>
        /// <param name="paramType"></param>
        /// <param name="paramSize"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        protected SqlParameter AddSqlParameter(SqlCommandType sqlCmdType, string paramName, SqlDbType paramType, int paramSize, object paramValue)
        {
            return AddSqlParameter(sqlCmdType, paramName, paramType, paramSize, paramValue, ParameterDirection.Input);
        }

        /// <summary>
        /// Add a Sql parameter to Sql command, setting by SetSqlCommand
        /// </summary>
        /// <param name="sqlCmdType"></param>
        /// <param name="paramName"></param>
        /// <param name="paramType"></param>
        /// <param name="paramSize"></param>
        /// <param name="paramValue"></param>
        /// <param name="paramDirection"></param>
        /// <returns></returns>
        protected SqlParameter AddSqlParameter(SqlCommandType sqlCmdType, string paramName, SqlDbType paramType, int paramSize, object paramValue, ParameterDirection paramDirection)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            SqlParameter parm = null;
            try
            {
                parm = new SqlParameter(paramName, paramType, paramSize);
                parm.Direction = paramDirection;
                parm.Value = paramValue;

                switch (sqlCmdType)
                {
                    case SqlCommandType.SelectCommand:
                        if (loadCommand == null) throw new Exception("You must use SelectCommand first.");
                        loadCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.InsertCommand:
                        if (insertCommand == null) throw new Exception("You must use InsertCommand first.");
                        insertCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.UpdateCommand:
                        if (updateCommand == null) throw new Exception("You must use UpdateCommand first.");
                        updateCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.DeleteCommand:
                        if (deleteCommand == null) throw new Exception("You must use DeleteCommand first.");
                        deleteCommand.Parameters.Add(parm);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return parm;

        }

        ///// <summary>
        ///// Add a Sql parameter to Sql command, setting by SetSqlCommand
        ///// </summary>
        ///// <param name="sqlCmdType"></param>
        ///// <param name="paramName"></param>
        ///// <param name="paramType"></param>
        ///// <param name="paramSize"></param>
        ///// <param name="sourceColumn"></param>
        ///// <returns></returns>
        //protected SqlParameter AddSqlParameter(SqlCommandType sqlCmdType, string paramName, SqlDbType paramType, int paramSize, string sourceColumn)
        //{
        //    return AddSqlParameter(sqlCmdType, paramName, paramType, paramSize, sourceColumn, ParameterDirection.Input);
        //}

        /// <summary>
        /// Add a Sql parameter to Sql command, setting by SetSqlCommand
        /// </summary>
        /// <param name="sqlCmdType"></param>
        /// <param name="paramName"></param>
        /// <param name="paramType"></param>
        /// <param name="paramSize"></param>
        /// <param name="sourceColumn"></param>
        /// <param name="paramDirection"></param>
        /// <returns></returns>
        protected SqlParameter AddSqlParameter(SqlCommandType sqlCmdType, string paramName, SqlDbType paramType, int paramSize, ParameterDirection paramDirection, string sourceColumn)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            SqlParameter parm = null;
            try
            {
                parm = new SqlParameter(paramName, paramType, paramSize, sourceColumn);
                parm.Direction = paramDirection;

                switch (sqlCmdType)
                {
                    case SqlCommandType.SelectCommand:
                        if (loadCommand == null) throw new Exception("You must use SetSqlCommand first.");
                        loadCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.InsertCommand:
                        if (insertCommand == null) throw new Exception("You must use InsertCommand first.");
                        insertCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.UpdateCommand:
                        if (updateCommand == null) throw new Exception("You must use UpdateCommand first.");
                        updateCommand.Parameters.Add(parm);
                        break;

                    case SqlCommandType.DeleteCommand:
                        if (deleteCommand == null) throw new Exception("You must use DeleteCommand first.");
                        deleteCommand.Parameters.Add(parm);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return parm;
        }

        #endregion AddSqlParameter

        #region SetSqlCommand

        /// <summary>
        /// Create SqlCommand, default CommandType is StoredProcedure
        /// </summary>
        /// <param name="commandText"></param>
        protected void SetSqlCommand(SqlCommandType sqlCmdType, string commandText)
        {
            SetSqlCommand(sqlCmdType, commandText, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Create SqlCommand, providing commandType
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parms"></param>
        protected void SetSqlCommand(SqlCommandType sqlCmdType, string commandText, CommandType commandType)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            try
            {
                switch (sqlCmdType)
                {
                    case SqlCommandType.SelectCommand:
                        loadCommand = new SqlCommand(commandText, _connection);
                        loadCommand.CommandType = commandType;
                        break;
                    case SqlCommandType.InsertCommand:
                        insertCommand = new SqlCommand(commandText, _connection);
                        insertCommand.CommandType = commandType;
                        break;
                    case SqlCommandType.UpdateCommand:
                        updateCommand = new SqlCommand(commandText, _connection);
                        updateCommand.CommandType = commandType;
                        break;
                    case SqlCommandType.DeleteCommand:
                        deleteCommand = new SqlCommand(commandText, _connection);
                        deleteCommand.CommandType = commandType;
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
        }

        #endregion SetSqlCommand

        #endregion Protected

        #region Public members

        
        #region Functions

        #region ExecuteFill

        /// <summary>
        /// Return DataSet based on Settings by SetSqlCommand statement
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteFill()
        {
            return ExecuteFill(false);
        }

        /// <summary>
        /// Return DataSet based on Settings by SetSqlCommand statement
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteFill(bool includeSchema)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            DataSet dsResult = null;

            try
            {
                dsResult = new DataSet();
                dAdapter.SelectCommand = loadCommand;
                if (includeSchema)
                    dAdapter.FillSchema(dsResult, SchemaType.Source);
                dAdapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                StringBuilder sMethodInfo = new StringBuilder();
                if (loadCommand.CommandType == CommandType.StoredProcedure)
                    sMethodInfo.AppendFormat("Stored procedure: {0}", loadCommand.CommandText);
                else
                    sMethodInfo.AppendFormat("Query : {0}", loadCommand.CommandText);

                sMethodInfo.Append("\r\n");
                sMethodInfo.Append("Parameters:");
                sMethodInfo.Append("\r\n");
                for (int i = 0; i < loadCommand.Parameters.Count; i++)
                {
                    sMethodInfo.AppendFormat("{0} = {1}", loadCommand.Parameters[i].ParameterName, loadCommand.Parameters[i].Value.ToString());
                    sMethodInfo.Append("\r\n");
                }
                BifrostException.HandleDSLException(subType, ex, this.GetType(), sMethodInfo.ToString());

            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return dsResult;
        }



        #endregion ExecuteFill

        #region WebPageExecuteFill
        /// <summary>
        /// Web에서 Page을 위해서 DataSet를 리턴받는 ExecuteFill(데이타 얻을때 이용된다.) 
        /// </summary>
        /// <param name="commandText">스토어드 프로시저명</param>
        /// <param name="sqlParameter">SqlParameter Array</param>
        /// <param name="intPageSize">PageSize</param>
        /// <param name="intPageIndex">PageIndex</param>
        /// <returns>DataSet으로 결과를 받는다.</returns>
        protected DataSet WebPageExecuteFill(int intPageSize, int intPageIndex)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            DataSet dsResult = null;

            try
            {
                dsResult = new DataSet();
                dAdapter.SelectCommand = loadCommand;

                if ((intPageSize <= 0) || (intPageIndex <= 0))
                {
                    //					daNF.SelectCommand.Parameters.Clear();
                    return dsResult;
                }

                dAdapter.Fill(dsResult, (intPageIndex - 1) * intPageSize, intPageSize, "None");

                //daNF.Fill(dsResult, (intPageIndex - 1) * intPageSize, intPageSize, "None");
                //				daNF.SelectCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return dsResult;

        }
        #endregion ExecuteFill

        #region ExecuteScalar

        /// <summary>
        /// Return single value based on Settings by SetSqlCommand statement
        /// </summary>
        /// <returns></returns>
        public object ExecuteScalar()
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            object retValue = null;

            try
            {
                dAdapter.SelectCommand = loadCommand;
                if (dAdapter.SelectCommand.Connection.State == ConnectionState.Closed) 
                    dAdapter.SelectCommand.Connection.Open();
                retValue = dAdapter.SelectCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                StringBuilder sMethodInfo = new StringBuilder();

                if (loadCommand.CommandType == CommandType.StoredProcedure)
                    sMethodInfo.AppendFormat("Stored procedure: {0}", loadCommand.CommandText);
                else
                    sMethodInfo.AppendFormat("Query : {0}", loadCommand.CommandText);

                sMethodInfo.Append("\r\n");
                sMethodInfo.Append("Parameters:");
                sMethodInfo.Append("\r\n");
                for (int i = 0; i < loadCommand.Parameters.Count; i++)
                {
                    sMethodInfo.AppendFormat("{0}={1}", loadCommand.Parameters[i].ParameterName, loadCommand.Parameters[i].Value.ToString());
                    sMethodInfo.Append("\r\n");
                }
                BifrostException.HandleDSLException(subType, ex, this.GetType(), sMethodInfo.ToString());
            }
            finally
            {
                if (dAdapter.SelectCommand.Connection.State == ConnectionState.Open)
                {
                    dAdapter.SelectCommand.Connection.Close();
                }

                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return retValue;
        }

        #endregion ExecuteScalar

        #region ExecuteNonQuery

        /// <summary>
        /// Processing without return any value, excepts number of row count.
        /// The command is set by SetSqlCommand statement
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            int numberOfRowCount = 0;

            try
            {
                dAdapter.SelectCommand = loadCommand;
                if (dAdapter.SelectCommand.Connection.State == ConnectionState.Closed) dAdapter.SelectCommand.Connection.Open();
                numberOfRowCount = dAdapter.SelectCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StringBuilder sMethodInfo = new StringBuilder();
                if (loadCommand.CommandType == CommandType.StoredProcedure)
                    sMethodInfo.AppendFormat("Stored procedure: {0}", loadCommand.CommandText);
                else
                    sMethodInfo.AppendFormat("Query : {0}", loadCommand.CommandText);

                sMethodInfo.Append("\r\n");
                sMethodInfo.Append("Parameters:");
                sMethodInfo.Append("\r\n");
                for (int i = 0; i < loadCommand.Parameters.Count; i++)
                {
                    sMethodInfo.AppendFormat("{0}={1}", loadCommand.Parameters[i].ParameterName, loadCommand.Parameters[i].Value.ToString());
                    sMethodInfo.Append("\r\n");
                }
                BifrostException.HandleDSLException(subType, ex, this.GetType(), sMethodInfo.ToString());
            }
            finally
            {
                if (dAdapter.SelectCommand.Connection.State == ConnectionState.Open)
                    dAdapter.SelectCommand.Connection.Close();

                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return numberOfRowCount;
        }

        #endregion ExecuteNonQuery

        #region ExecuteReader

        /// <summary>
        /// Return a SqlDataReader object based on settings by SetSqlCommand statement.
        /// Remember to close the SqlDataReader object
        /// </summary>
        /// <returns></returns>
        public SqlDataReader ExecuteReader()
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            SqlDataReader sqlReader = null;

            try
            {
                dAdapter.SelectCommand = loadCommand;
                if (dAdapter.SelectCommand.Connection.State == ConnectionState.Closed) dAdapter.SelectCommand.Connection.Open();
                sqlReader = dAdapter.SelectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                StringBuilder sMethodInfo = new StringBuilder();
                if (loadCommand.CommandType == CommandType.StoredProcedure)
                    sMethodInfo.AppendFormat("Stored procedure: {0}", loadCommand.CommandText);
                else
                    sMethodInfo.AppendFormat("Query : {0}", loadCommand.CommandText);
                sMethodInfo.Append("\r\n");
                sMethodInfo.Append("Parameters:");
                sMethodInfo.Append("\r\n");
                for (int i = 0; i < loadCommand.Parameters.Count; i++)
                {
                    sMethodInfo.AppendFormat("{0}={1}", loadCommand.Parameters[i].ParameterName, loadCommand.Parameters[i].Value.ToString());
                    sMethodInfo.Append("\r\n");
                }
                BifrostException.HandleDSLException(subType, ex, this.GetType(), sMethodInfo.ToString());
            }
            finally
            {
                //if (daNF.SelectCommand.Connection.State == ConnectionState.Open)
                //    daNF.SelectCommand.Connection.Close();

                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return sqlReader;
        }

        #endregion ExecuteReader

        #region UpdateData

        public int UpdateDataTable(DataTable dsObject, bool transactionYN)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            int nResult = -1;

            ///
            /// Transaction
            ///

            if (transactionYN)
            {
                //				TransactionOptions transOption = new TransactionOptions();
                //				transOption.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;

                using (TransactionScope transactionscope1 = new TransactionScope(TransactionScopeOption.Required))//, transOption))
                {
                    try
                    {
                        dAdapter.InsertCommand = insertCommand;
                        dAdapter.UpdateCommand = updateCommand;
                        dAdapter.DeleteCommand = deleteCommand;

                        nResult = dAdapter.Update(dsObject);

                        //
                        // Check for table errors to see if the update failed.
                        //
                        if (dsObject.HasErrors)
                        {
                            dsObject.GetErrors()[0].ClearErrors();
                        }
                        else
                        {
                            dsObject.AcceptChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
                    }
                    finally
                    {
                        LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
                    }

                    transactionscope1.Complete();
                }
            }
            else
            {
                try
                {
                    dAdapter.InsertCommand = insertCommand;
                    dAdapter.UpdateCommand = updateCommand;
                    dAdapter.DeleteCommand = deleteCommand;

                    nResult = dAdapter.Update(dsObject);

                    //
                    // Check for table errors to see if the update failed.
                    //
                    if (dsObject.HasErrors)
                    {
                        dsObject.GetErrors()[0].ClearErrors();
                    }
                    else
                    {
                        dsObject.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
                }
                finally
                {
                    LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
                }
            }

            return nResult;
        }

        /// <summary>
        /// Process Insert, Update, Delete once using internal DataAdapter.
        /// The DataSet must contain all columns necessary for Insert, Update, Delete 
        /// command which SourceColumn of each SqlParameter bound to.
        /// </summary>
        /// <param name="dsObject"></param>
        /// <returns>True if OK, False means error occur. Get the error rows by 
        /// dsObject.Tables[0].GetErrors() to get rows having errors</returns>
        public bool UpdateData(DataSet dsObject, bool transactionYN)
        {
            return UpdateData(dsObject, null, transactionYN);
        }

        /// <summary>
        /// Process Insert, Update, Delete once using internal DataAdapter.
        /// The DataSet must contain all columns necessary for Insert, Update, Delete 
        /// command which SourceColumn of each SqlParameter bound to.
        /// </summary>
        /// <param name="dsObject"></param>
        /// <param name="srcTable"></param>
        /// <param name="transactionYN"></param>
        /// <returns>True if OK, False means error occur. Get the error rows by 
        /// dsObject.Tables[0].GetErrors() to get rows having errors</returns>
        public bool UpdateData(DataSet dsObject, string srcTable, bool transactionYN)
        {
            return UpdateData2(dsObject, srcTable, transactionYN) != -1;
        }

        /// <summary>
        /// Process Insert, Update, Delete once using internal DataAdapter.
        /// The DataSet must contain all columns necessary for Insert, Update, Delete 
        /// command which SourceColumn of each SqlParameter bound to.
        /// </summary>
        /// <param name="dsObject"></param>
        /// <param name="srcTable"></param>
        /// <param name="transactionYN"></param>
        /// <returns>True if OK, False means error occur. Get the error rows by 
        /// dsObject.Tables[0].GetErrors() to get rows having errors</returns>
        public int UpdateData2(DataSet dsObject, bool transactionYN)
        {
            return UpdateData2(dsObject, null, transactionYN);
        }

        /// <summary>
        /// Process Insert, Update, Delete once using internal DataAdapter.
        /// The DataSet must contain all columns necessary for Insert, Update, Delete 
        /// command which SourceColumn of each SqlParameter bound to.
        /// </summary>
        /// <param name="dsObject"></param>
        /// <param name="srcTable"></param>
        /// <returns>True if OK, False means error occur. Get the error rows by 
        /// dsObject.Tables[0].GetErrors() to get rows having errors</returns>
        public int UpdateData2(DataSet dsObject, string srcTable, bool transactionYN)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            int nResult = -1;

            ///
            /// Transaction
            ///

            if (transactionYN)
            {
                //				TransactionOptions transOption = new TransactionOptions();
                //				transOption.IsolationLevel = System.Transactions.IsolationLevel.Snapshot;

                using (TransactionScope transactionscope1 = new TransactionScope(TransactionScopeOption.Required))//, transOption))
                {
                    try
                    {
                        dAdapter.InsertCommand = insertCommand;
                        dAdapter.UpdateCommand = updateCommand;
                        dAdapter.DeleteCommand = deleteCommand;

                        if (srcTable != null)
                            nResult = dAdapter.Update(dsObject, srcTable);
                        else
                            nResult = dAdapter.Update(dsObject);

                        //
                        // Check for table errors to see if the update failed.
                        //
                        if (dsObject.HasErrors)
                        {
                            dsObject.Tables[0].GetErrors()[0].ClearErrors();
                        }
                        else
                        {
                            if (srcTable != null)
                                dsObject.Tables[srcTable].AcceptChanges();
                            else
                                dsObject.AcceptChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
                    }
                    finally
                    {
                        LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
                    }

                    transactionscope1.Complete();
                }
            }
            else
            {
                try
                {
                    dAdapter.InsertCommand = insertCommand;
                    dAdapter.UpdateCommand = updateCommand;
                    dAdapter.DeleteCommand = deleteCommand;

                    if (srcTable != null)
                        nResult = dAdapter.Update(dsObject, srcTable);
                    else
                        nResult = dAdapter.Update(dsObject);

                    //
                    // Check for table errors to see if the update failed.
                    //
                    if (dsObject.HasErrors)
                    {
                        dsObject.Tables[0].GetErrors()[0].ClearErrors();
                    }
                    else
                    {
                        if (srcTable != null)
                            dsObject.Tables[srcTable].AcceptChanges();
                        else
                            dsObject.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    BifrostException.HandleDSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
                }
                finally
                {
                    LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
                }
            }

            return nResult;
        }

        #endregion UpdateData

        #endregion Functions

        #endregion Public members
    }
    
    #region Misc Classes

    public enum SqlCommandType
    {
        SelectCommand, InsertCommand, UpdateCommand, DeleteCommand
    }

    #endregion
}
