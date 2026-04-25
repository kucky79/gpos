using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Bifrost
{
    public class LogHandler : Base
    {
        #region 로깅설정
        /// <summary>
        /// 서브팀로깅레벨설정
        /// </summary>
        public static bool LogSubSystem
        {
            get
            {
                string strTemp = GetConfigString("SystemLogLevel");
                return (strTemp == "Y" ? true : false);
            }
        }
        #endregion

        #region Publish Exception기록
        /// <summary>
        /// Exception기록
        /// </summary>
        /// <param name="systemName">서브시스템명</param>
        /// <param name="methodIBifrosto">method IBifrosto</param>
        /// <param name="layer">레이어명</param>
        /// <param name="exception">Exception</param>
        public static Exception Publish(string systemName, string methodIBifrosto, string layer, System.Exception exception)
        {
            string tmp = string.Empty;
            StringBuilder msg = null;
            StackTrace st1 = null;
            StackTrace st2 = null;
            StackFrame sf = null;

            try
            {
                //Stack과 Exception을 하나의 msg에 담는다.
                msg = new StringBuilder(4096);

                //Stack트래이싱하는부분
                st1 = new StackTrace(1, true);
                msg.Append("==============[Bifrost System Error Tracing]==============\r\n");
                msg.Append("[MethodIBifrosto]\r\n");
                msg.Append(methodIBifrosto);
                msg.Append("\r\n");
                msg.Append("[CallStackTrace]\r\n");
                for (int i = 0; i < st1.FrameCount; i++)
                {
                    sf = st1.GetFrame(i);
                    tmp = sf.GetMethod().DeclaringType.FullName + "." + sf.GetMethod().Name;
                    if (tmp.IndexOf("System") != 0 && tmp.IndexOf("Microsoft") != 0)
                    {
                        msg.Append(tmp);
                        msg.Append(" : (" + sf.GetFileLineNumber() + ")");
                        msg.Append("\r\n");
                    }
                }

                //Exception을 트래이싱하는부분
                st2 = new StackTrace(exception, true);
                msg.Append("\r\n[ErrStackTrace]\r\n");
                for (int i = 0; i < st2.FrameCount; i++)
                {
                    sf = st2.GetFrame(i);
                    tmp = sf.GetMethod().DeclaringType.FullName + "." + sf.GetMethod().Name;
                    if (tmp.IndexOf("System") != 0 && tmp.IndexOf("Microsoft") != 0)
                    {
                        msg.Append(tmp);
                        msg.Append(" : (" + sf.GetFileLineNumber() + ")");
                        msg.Append("\r\n");
                    }
                }

                //msg에 현재 트래이싱된 시간을 담도록 한다.
                msg.Append("\r\n[DateTime] : " + DateTime.Now.ToString() + "\r\n");

                //만약 Sql관련 Exception이면 추가항목을 넣어주도록 한다.
                if (exception.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    SqlException sqlErr = (SqlException)exception;
                    msg.Append("\r\n[SqlException] ");
                    msg.Append("\r\nException Type: ").Append(sqlErr.GetType());
                    msg.Append("\r\nErrors: ").Append(sqlErr.Errors);
                    msg.Append("\r\nClass: ").Append(sqlErr.Class);
                    msg.Append("\r\nLineNumber: ").Append(sqlErr.LineNumber);
                    msg.Append("\r\nMessage: ").Append("{" + sqlErr.Message + "}");
                    msg.Append("\r\nNumber: ").Append(sqlErr.Number);
                    msg.Append("\r\nProcedure: ").Append(sqlErr.Procedure);
                    msg.Append("\r\nServer: ").Append(sqlErr.Server);
                    msg.Append("\r\nState: ").Append(sqlErr.State);
                    msg.Append("\r\nSource: ").Append(sqlErr.Source);
                    msg.Append("\r\nTargetSite: ").Append(sqlErr.TargetSite);
                    msg.Append("\r\nHelpLink: ").Append(sqlErr.HelpLink);
                }
                //Sql관련Exception외에 작업들..
                else
                {
                    msg.Append("\r\n[Exception] ");
                    msg.Append("\r\n" + "DetailMsg: {" + exception.Message + "}");
                }
                // Create the source, if it does not already exist.
                if(GetConfigString("ExceptionLogDBWrite") == "Y")
                    WriteLog(msg.ToString(), systemName, layer);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new Exception(msg.ToString());
        }
        #endregion

        #region WriteLog 실제로그기록
        /// <summary>
        /// 실제 이벤트로그기록
        /// </summary>
        /// <param name="sMessage">에러메세지</param>
        /// <param name="sSubSystem">서브시스템명</param>
        /// <param name="sLayer">레이어명</param>
        private static void WriteLog(string sMessage, string sSubSystem, string sLayer)
        {
            //string sSource = string.Empty;
            //EventLog Log = null;
            try
            {
                #region
                // 사용자 지정 로그 이름의 처음 여덟자만 의미가 있어서 substring함.(Event Log에서 에러를 던짐)
                //                if (sSubSystem.Length > 5) 
                //                {
                //                   sSubSystem = sSubSystem.Substring(0,4);
                //  				 sSubSystem = sSubSystem.Replace(".","");
                //                }
                //                sSource = sSubSystem + "." + sLayer; //이벤트로그명 결정

                //                if (!EventLog.SourceExists(sSource))
                //                {
                //                    EventLog.CreateEventSource(sSource,sSource); //없으면 만든다.
                //                }

                //                // Inserting into event log
                //                Log = new EventLog();
                //                Log.Source = sSource;
                //                Log.WriteEntry(sMessage, EventLogEntryType.Error);		
                #endregion
                /// 
                /// Trace log
                /// 
                TraceWrite(DateTime.Now, sMessage, sSubSystem);
            }
            catch// (Exception ex)
            {
                //throw ex;
            }
            finally
            {
                //if (Log != null) 
                //{
                //    Log.Close();
                //    Log.Dispose();
                //}
            }
        }

        /*
        /// <summary>
        ///  실제 커스텀 로그를 남기기 위한 함수.
        /// </summary>
        /// <param name="sMessage">메세지</param>
        /// <param name="sSubSystem">서브시스템명</param>
        /// <param name="sLayer">레이어명</param>
        /// <param name="eventId">이벤트아이디</param>
        /// <param name="category">카테고리</param>
        //		private static void WriteLog(string sMessage, string sSubSystem, string sLayer, int eventId, short category)
        //		{
        //			string sSource = string.Empty;
        //			EventLog Log = null;
        //			try
        //			{
        //				sSubSystem = sSubSystem.Substring(0,4);
        //				sSubSystem = sSubSystem.Replace(".","");
        //				sSource = sSubSystem + "." + sLayer; //이벤트로그명 결정
        //
        //				if (!EventLog.SourceExists(sSource))
        //				{
        //					EventLog.CreateEventSource(sSource,sSource);
        //				}
        //							
        //				// Inserting into event log
        //				Log    = new EventLog();
        //				Log.Source      = sSource;
        //				Log.WriteEntry(sMessage, EventLogEntryType.IBifrostormation, eventId, category );
        //				
        //			}
        //			catch(Exception ex)
        //			{
        //				throw ex;
        //			}
        //			finally
        //			{
        //				Log.Dispose();
        //				Log.Close();
        //			}
        //		}
         * */
        #endregion

        #region Tracing To DB and TraceListener

        /// <summary>
        /// 
        /// </summary>
        /// <param name="when"></param>
        /// <param name="sMessage"></param>
        /// <param name="sSubSystem"></param>
        private static void TraceWrite(DateTime when, string sMessage, string sSubSystem)
        {
            try
            {
                // insert to db
                string loginID = string.Empty;
                if (HttpContext.Current.Session != null)
                {
                    loginID = HttpContext.Current.Session["USERID"] == null ? string.Empty : HttpContext.Current.Session["USERID"].ToString();
                }

                string clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                string serverIP = GetLocalIPAddress();

                ExecuteNonQuery("UP_CO_InsertTraceLog",
                    new string[] { "@exceptionWhen", "@subSystem", "@clientIP", "@serverIP", "@loginID", "@traceMessage" },
                    new object[] { when, sSubSystem, clientIP, serverIP, loginID, sMessage });

                //SendMsgRemoteListeners(when, sSubSystem, clientIP, serverIP, loginID, sMessage);
            }
            catch// (Exception ex)
            {
                //throw ex;
            }
            finally
            {
            }
        }

        private static void ExecuteNonQuery(string spName, string[] paramNames, object[] paramValues)
        {

            string connString = GetConfigString("DBConnectionString");
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(spName, new SqlConnection(connString));
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramNames.Length; i++)
                {
                    cmd.Parameters.AddWithValue(paramNames[i], paramValues[i]);
                }
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// Return current local pc's ip address
        /// </summary>
        /// <returns></returns>
        private static string GetLocalIPAddress()
        {
            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            return addresses[0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="when"></param>
        /// <param name="subSystem"></param>
        /// <param name="clientIP"></param>
        /// <param name="serverIP"></param>
        /// <param name="loginID"></param>
        /// <param name="logMessage"></param>
        private static void SendMsgRemoteListeners(DateTime when, string subSystem, string clientIP, string serverIP, string loginID, string logMessage)
        {
            TcpClient oTcpClient = null;
            NetworkStream oStream = null;

            try
            {
                oTcpClient = new TcpClient();


                string ipAddress = Base.GetConfigString("TRACE_LISTENER");
                int port = Convert.ToInt32(ipAddress.Substring(ipAddress.IndexOf(":") + 1));
                ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(":"));

                oTcpClient.Connect(ipAddress, port);

                // Get the stream, convert to bytes
                oStream = oTcpClient.GetStream();

                // formulate string
                StringBuilder sendMsg = new StringBuilder();
                sendMsg.Append(when.ToString("yyyyMMdd.HHmmss"));
                sendMsg.Append(subSystem.Trim() == "" ? "CO" : subSystem.Trim());
                sendMsg.AppendFormat("#{0}#{1}#{2}#{3}", clientIP, serverIP, loginID, logMessage);

                byte[] byteMsg = System.Text.Encoding.Default.GetBytes(sendMsg.ToString());

                // now send it
                oStream.Write(byteMsg, 0, byteMsg.Length);

            }
            catch
            {
            }
            finally
            {
                if (oStream != null)
                    oStream.Close();
                if (oTcpClient != null)
                    oTcpClient.Close();
            }
        }

        #endregion
    }
}
