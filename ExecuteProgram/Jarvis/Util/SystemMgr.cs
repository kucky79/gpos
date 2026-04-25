using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;

using Bifrost;
using Bifrost.Data;
using Bifrost.Win.Controls;

namespace Bifrost.Win
{
    public class SystemMgr 
    {
        bool _bSubStatus = true;

        #region Logging
        protected void LoggingStart(ref TimeStamp ts)
        {
            //Comment
            try
            {
                if (_bSubStatus)
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

        protected void LoggingEnd(TimeStamp ts, object target, string sServiceName)
        {
            //Comment
            try
            {
                if (_bSubStatus && ts != null)
                {
                    ts.TimeStampEnd(target, sServiceName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Logging

        /// <summary>
        /// Login 인증
        /// </summary>
        /// <param name="strFirmCode"></param>
        /// <param name="strCultureName"></param>
        /// <param name="encodedUserID"></param>
        /// <param name="encodedUserPW"></param>
        /// <param name="sIPAddr"></param>
        /// <returns></returns>
        public DataSet Authenticate(string strFirmCode, string strCultureName, string encodedUserID, string encodedUserPW, string sIPAddr, string sMACAddr)
        {
            UserLoginStatus loginStatus = UserLoginStatus.UnknownError;

            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);

            DataSet dsReturn = null;

            try
            {
                string realUserID = Base.BifrostDecrypt(encodedUserID);
                string realPassword = Base.BifrostDecrypt(encodedUserPW);

                object s = DBHelper.ExecuteScalar("USP_SYS_LOGIN_S", new object[] { strFirmCode, realUserID, realPassword });
                loginStatus = (UserLoginStatus)Convert.ToInt32(s.ToString());


                if (loginStatus == UserLoginStatus.LogInSuccess)
                {
                    dsReturn = GetUserInfo(strFirmCode, strCultureName, realUserID, sIPAddr, sMACAddr);
                }
            }
            catch
            {
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            /// 
            /// return to client
            /// 
            DataSet returnDS = new DataSet();

            // Loginstatus table
            DataTable loginStatusTB = new DataTable("LoginStatus");
            loginStatusTB.Columns.Add("LoginStatus", typeof(int));

            DataRow dr = loginStatusTB.NewRow();
            dr["LoginStatus"] = (int)loginStatus;
            loginStatusTB.Rows.Add(dr);

            loginStatusTB.AcceptChanges();
            returnDS.Tables.Add(loginStatusTB);

            if (dsReturn != null)
            {
                returnDS.Tables.Add(dsReturn.Tables[0].Copy());
                returnDS.Tables[1].TableName = "UserInfo";
            }

            return returnDS;
        }

        /// <summary>POS Login 인증</summary>
        /// <param name="strStoreCode"></param>
        /// <param name="strCultureName"></param>
        /// <param name="encodedUserID"></param>
        /// <param name="encodedUserPW"></param>
        /// <param name="strIPAddr"></param>
        /// <param name="strMACAddr"></param>
        /// <returns></returns>
        public DataSet AuthenticatePOS(string strStoreCode, string strCultureName, string encodedUserID, string encodedUserPW, string strIPAddr, string strMACAddr)
        {
            UserLoginStatus loginStatus = UserLoginStatus.UnknownError;

            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);

            DataSet dsReturn = null;

            try
            {
                string realUserID = Base.BifrostDecrypt(encodedUserID);
                string realPassword = Base.BifrostDecrypt(encodedUserPW);

                object s = DBHelper.ExecuteScalar("USP_POS_LOGIN_S", new object[] { strStoreCode, realUserID, realPassword });
                loginStatus = (UserLoginStatus)Convert.ToInt32(s.ToString());


                if (loginStatus == UserLoginStatus.LogInSuccess)
                {
                    dsReturn = GetPOSUserInfo(strStoreCode, strCultureName, realUserID, strIPAddr, strMACAddr);
                }
            }
            catch(Exception ex)
            {
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            /// return to client
            DataSet returnDS = new DataSet();

            // Loginstatus table
            DataTable loginStatusTB = new DataTable("LoginStatus");
            loginStatusTB.Columns.Add("LoginStatus", typeof(int));

            DataRow dr = loginStatusTB.NewRow();
            dr["LoginStatus"] = (int)loginStatus;
            loginStatusTB.Rows.Add(dr);

            loginStatusTB.AcceptChanges();
            returnDS.Tables.Add(loginStatusTB);

            if (dsReturn != null)
            {
                returnDS.Tables.Add(dsReturn.Tables[0].Copy());
                returnDS.Tables[1].TableName = "UserInfo";
            }

            return returnDS;
        }

        /// <summary>
		/// Return a dataset with 1 row, containing user data, user's
		/// global settings
		/// </summary>
		/// <param name="loginID">userID column</param>
		/// <returns></returns>
        public DataSet GetUserInfo(string FirmCode, string LanguageCode, string userID, string loginIP, string loginMAC)
        {
            TimeStamp oTimeStamp = null;
            LoggingStart(ref oTimeStamp);

            DataSet dsResult = null;

            try
            {
                dsResult = DBHelper.GetDataSet("USP_SYS_USER_LOGIN", new object[] { FirmCode, userID, LanguageCode, loginIP, loginMAC });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
            }

            return dsResult;
        }

        /// <summary>
		/// Return a dataset with 1 row, containing user data, user's global settings
		/// </summary>
		/// <param name="loginID">userID column</param>
		/// <returns></returns>
        public DataSet GetPOSUserInfo(string StoreCode, string LanguageCode, string userID, string loginIP, string loginMAC)
        {
            TimeStamp oTimeStamp = null;
            LoggingStart(ref oTimeStamp);

            DataSet dsResult = null;

            try
            {
                dsResult = DBHelper.GetDataSet("USP_POS_USER_LOGIN", new object[] { StoreCode, userID, LanguageCode, loginIP, loginMAC });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
            }

            return dsResult;
        }


        public void LogOut(string userID)
        {
            TimeStamp oTimeStamp = null;
            LoggingStart(ref oTimeStamp);

            try
            {
                DBHelper.ExecuteNonQuery("USP_SYS_USER_LOGOUT", new object[] { userID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
            }
        }

        #region Menu관리/MyMenus 관리

        /// <summary>
        /// Return my menus
        /// </summary>
        /// <param name="firmCode"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetMyMenus(string firmCode, string userID)
        {
            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);
            DataTable dsReturn = null;
            try
            {
                dsReturn = DBHelper.GetDataTable("USP_SYS_BOOKMARK_S", new object[] { firmCode, userID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return dsReturn;
        }

        /// <summary>
        /// Return all authorized menus
        /// </summary>
        /// <param name="cultureName"></param>

        public DataSet GetAuthorizedMenus(string firmCode, string cultureName, string userID)
        {
            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);
            DataSet dsReturn = null;
            try
            {
                dsReturn = DBHelper.GetDataSet("USP_SYS_GET_AUTH_MENU_S", new object[] { firmCode, cultureName, userID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return dsReturn;
        }

        /// <summary>
        /// Update menus info
        /// </summary>
        /// <param name="cultureName"></param>
        /// <param name="changedMenus"></param>
        /// <returns></returns>
        public int UpdateMenus(string[] Parameter)
        {
            //if (syWebService == null)
            //{
            //    syWebService = new NF.SY.Win.SYS.ISYWebService.SYWebService_WBLK();
            //    syWebService.UseDefaultCredentials = true;
            //    string mainUrl = Bifrost.Common.AppConfigReader.Default.NFSYWebService;
            //    syWebService.Url = string.Concat(mainUrl.Substring(0, mainUrl.LastIndexOf(".")), "_WBLK", mainUrl.Substring(mainUrl.LastIndexOf(".")));
            //}

            //return syWebService.UpdateMenus(Parameter);
            return 0;
        }

        /// <summary>
        /// Update my menus
        /// </summary>
        /// <param name="cultureName"></param>
        /// <param name="changedMenus"></param>
        /// <returns></returns>
        public bool UpdateMyMenus(string cultureName, DataSet changedMenus)
        {
            //if (syWebService == null)
            //{
            //    syWebService = new NF.SY.Win.SYS.ISYWebService.SYWebService_WBLK();
            //    syWebService.UseDefaultCredentials = true;
            //    string mainUrl = Bifrost.Common.AppConfigReader.Default.NFSYWebService;
            //    syWebService.Url = string.Concat(mainUrl.Substring(0, mainUrl.LastIndexOf(".")), "_WBLK", mainUrl.Substring(mainUrl.LastIndexOf(".")));
            //}

            //return syWebService.UpdateMyMenus(cultureName, changedMenus);
            return true;
        }

        /// <summary>
        /// Save list of authorized programs
        /// </summary>
        /// <param name="objectIsGroup"></param>
        /// <param name="objectID"></param>
        /// <param name="programs"></param>
        /// <returns></returns>
        public bool SaveAuthorizedMenusOnObject(bool objectIsGroup, string objectID, string cultureName, DataSet programs)
        {
            //if (syWebService == null)
            //{
            //    syWebService = new NF.SY.Win.SYS.ISYWebService.SYWebService_WBLK();
            //    syWebService.UseDefaultCredentials = true;
            //    string mainUrl = Bifrost.Common.AppConfigReader.Default.NFSYWebService;
            //    syWebService.Url = string.Concat(mainUrl.Substring(0, mainUrl.LastIndexOf(".")), "_WBLK", mainUrl.Substring(mainUrl.LastIndexOf(".")));
            //}

            //return syWebService.SaveAuthorizedMenusOnObject(objectIsGroup, objectID, cultureName, programs);
            return true;
        }
        #endregion

        public class DataSetCodec
        {
            // private ctor; all members are static
            private DataSetCodec()
            {
            }
            public static byte[] CompressDataSet(DataSet ds)
            {
                ds.RemotingFormat = SerializationFormat.Binary;
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, ds);
                byte[] inbyt = ms.ToArray();
                System.IO.MemoryStream objStream = new MemoryStream();
                System.IO.Compression.DeflateStream objZS = new System.IO.Compression.DeflateStream(objStream, System.IO.Compression.CompressionMode.Compress);
                objZS.Write(inbyt, 0, inbyt.Length);
                objZS.Flush();
                objZS.Close();
                return objStream.ToArray();
            }

            public static DataSet DecompressDataSet(byte[] bytDs, out int len)
            {
                System.Diagnostics.Debug.Write(bytDs.Length.ToString());
                DataSet outDs = new DataSet();
                MemoryStream inMs = new MemoryStream(bytDs);
                inMs.Seek(0, 0);
                DeflateStream zipStream = new DeflateStream(inMs, CompressionMode.Decompress, true);
                byte[] outByt = ReadFullStream(zipStream);
                zipStream.Flush();
                zipStream.Close();
                MemoryStream outMs = new MemoryStream(outByt);
                outMs.Seek(0, 0);
                outDs.RemotingFormat = SerializationFormat.Binary;
                BinaryFormatter bf = new BinaryFormatter();
                len = (int)outMs.Length;
                outDs = (DataSet)bf.Deserialize(outMs, null);
                return outDs;
            }

            public static byte[] ReadFullStream(Stream stream)
            {
                byte[] buffer = new byte[32768];
                using (MemoryStream ms = new MemoryStream())
                {
                    while (true)
                    {
                        int read = stream.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                            return ms.ToArray();
                        ms.Write(buffer, 0, read);
                    }

                }
            }
        }
    }
}
