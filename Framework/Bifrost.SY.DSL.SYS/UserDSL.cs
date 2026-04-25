using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

using Bifrost.Framework;

namespace Bifrost.SY.DSL.SYS
{
	/// <summary>
	/// 사용자 관리 class
	/// </summary>
	public class UserDSL : DSLBase
	{
		private SubSystemType _subType = SubSystemType.SYS;

		#region Constructors

		public UserDSL() : base()		
		{
		}

		public UserDSL(SubSystemType subSystem) : base(subSystem)
		{
			_subType = subSystem;
		}

		#endregion

		#region Users

		/// <summary>
		/// Return a dataset with 1 row, containing user data, user's
		/// global settings
		/// </summary>
		/// <param name="loginID">T_CO_User userID column</param>
		/// <returns></returns>
        public DataSet GetUserInfo(string FirmCode, string LanguageCode, string userID, string loginIP)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			DataSet dsResult = null;

			try
			{
                SetSqlCommand(SqlCommandType.SelectCommand, "WP_SY_UserLogIn", CommandType.StoredProcedure);
                AddSqlParameter(SqlCommandType.SelectCommand, "@FirmCode", SqlDbType.VarChar, 8, FirmCode);
                AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.VarChar, 20, userID);
                AddSqlParameter(SqlCommandType.SelectCommand, "@LanguageCode", SqlDbType.Char, 2, LanguageCode);
				AddSqlParameter(SqlCommandType.SelectCommand, "@loginIP", SqlDbType.VarChar, 15, loginIP);
				dsResult = this.ExecuteFill();
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsResult;
		}

		/// <summary>
		/// 사용자 정보 조회(세션)
		/// </summary>
		/// <param name="strUserid">사용자ID</param>
		/// <returns></returns>
		public DataSet GetUserInfo(string strUserid)
		{
			//Start Logging
			TimeStamp ts = null;
			LoggingStart(ref ts);


			DataSet dsReturn = null;
			try
			{

				SetSqlCommand(SqlCommandType.SelectCommand, "dbo.UP_CO_GetUserInfo_GW");
				AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.Char, 20, strUserid);

				dsReturn = ExecuteFill();

			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="password"></param>
		/// <returns></returns>
        public UserLoginStatus IsAuthenticated(string FirmCode, string userID, string password)
		{
			//Start Logging
			TimeStamp ts = null;
			LoggingStart(ref ts);

			int status = (int)UserLoginStatus.UnknownError;
			try
			{
				object s = DBHelper.ExecuteScalar("AP_SYS_LOGIN_S", new object[] { FirmCode, userID, password });
                status = Convert.ToInt32(s.ToString());
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(_subType, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return (UserLoginStatus)status;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="digitalSign"></param>
		/// <param name="password"></param>
		/// <param name="digitalSignPicFileKey"></param>
		/// <param name="langCode"></param>
		/// <param name="dateTimeFormat"></param>
		/// <param name="curGroupSep"></param>
		/// <param name="curDecSep"></param>
		/// <returns></returns>
		public bool SaveUserInfo(string userID, string digitalSign, string password,
			int digitalSignPicFileKey, string langCode, string dateTimeFormat, string curGroupSep, 
			string curDecSep, string actionID, string actionBy)
		{
			//Start Logging
			TimeStamp ts = null;
			LoggingStart(ref ts);


			bool bReturn = false;
			try
			{

				this.SetSqlCommand(SqlCommandType.SelectCommand, "dbo.UP_CO_SaveUserInfo");
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@userID", SqlDbType.VarChar, 20, userID);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@password", SqlDbType.VarChar, 20, password);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@langCode", SqlDbType.Char, 5, langCode);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@datetimeFormat", SqlDbType.Char, 10, dateTimeFormat);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@currencyGroupSeparator", SqlDbType.Char, 1, curGroupSep);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@currencyDecimalSeparator", SqlDbType.Char, 1, curDecSep);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@digitalSign", SqlDbType.VarChar, 20, digitalSign);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@digitalPicSignFileKey", SqlDbType.Int, 0, digitalSignPicFileKey);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@actionID", SqlDbType.Char, 20, actionID);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@actionBy", SqlDbType.Char, 10, actionBy);
			
				ExecuteNonQuery();
				bReturn = true;
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return bReturn;
		}

		/// <summary>
		/// Do logout
		/// </summary>
		/// <param name="userID"></param>
		public void LogOut(string userID)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			try
			{
				this.SetSqlCommand(SqlCommandType.SelectCommand, "up_CO_userLogout", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.Char, 20, userID);
				this.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}
		}

		/// <summary>
		/// Return null if this user is not logged in
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="loginAtIP"></param>
		/// <returns></returns>
		public DataSet GetLoggedInUserInfo(string userID, string loginAtIP)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			DataSet dsResult = null;

			try
			{
				this.SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_GetLoggedInUserInfo", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.Char, 20, userID);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@loggedInIP", SqlDbType.VarChar, 15, loginAtIP);

				dsResult = this.ExecuteFill();
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsResult;
		}


		/// <summary>
		/// User Expired?
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public bool IsUserExpired(string userID)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			bool isExpired = false;
			try
			{
				this.SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_CheckUserUsingPeriod", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.Char, 20, userID);

				object expired = this.ExecuteScalar();
				isExpired = expired == null ? false : (int)expired != 0;
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}

			return isExpired;

		}

		/// <summary>
		/// Return 2 datatable, departments and users 
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllUsersNDepts(string cultureName)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_GetAllUsersNDepts");
				AddSqlParameter(SqlCommandType.SelectCommand, "@cultureName", SqlDbType.Char, 5, cultureName);
				dsReturn = ExecuteFill();
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}

		/// <summary>
		/// Return 2 datatable, departments and users 
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllUsers(string cultureName)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_GetAllUsers");
				AddSqlParameter(SqlCommandType.SelectCommand, "@cultureName", SqlDbType.Char, 5, cultureName);
				dsReturn = ExecuteFill();
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="changedUsers"></param>
		/// <returns></returns>
		public bool UpdateUsers(DataSet changedUsers, string actionID, string actionBy)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			bool bReturn = false;
			try
			{
				///
				/// Insert command
				/// 
				//this.SetSqlCommand(SqlCommandType.InsertCommand, "UP_CO_InsertUser", CommandType.StoredProcedure);
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@userID", SqlDbType.Char, 20, ParameterDirection.Input, "userID");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@password", SqlDbType.VarChar, 20, ParameterDirection.Input, "password");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@empCode", SqlDbType.Char, 10, ParameterDirection.Input, "empCode");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@securityLevel", SqlDbType.Int, 0, ParameterDirection.Input, "securityLevel");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@langCode", SqlDbType.Char, 5, ParameterDirection.Input, "langCode");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@datetimeFormat", SqlDbType.Char, 10, ParameterDirection.Input, "datetimeFormat");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@currencyGroupSeparator", SqlDbType.Char, 1, ParameterDirection.Input, "currencyGroupSeparator");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@currencyDecimalSeparator", SqlDbType.Char, 1, ParameterDirection.Input, "currencyDecimalSeparator");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@useFrom", SqlDbType.DateTime, 0, ParameterDirection.Input, "useFrom");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@useUntil", SqlDbType.DateTime, 0, ParameterDirection.Input, "useUntil");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@useERP", SqlDbType.Bit, 0, ParameterDirection.Input, "useERP");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@useGW", SqlDbType.Bit, 0, ParameterDirection.Input, "useGW");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@useSFA", SqlDbType.Bit, 0, ParameterDirection.Input, "useSFA");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@useBI", SqlDbType.Bit, 0, ParameterDirection.Input, "useBI");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@apprUsrGrp", SqlDbType.VarChar, 8, ParameterDirection.Input, "apprUsrGrp");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@srchUsrGrp", SqlDbType.VarChar, 8, ParameterDirection.Input, "srchUsrGrp");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@srchAuth", SqlDbType.VarChar, 8, ParameterDirection.Input, "srchAuth");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@actionBy", SqlDbType.Char, 10, ParameterDirection.Input, "actionBy");
				//this.AddSqlParameter(SqlCommandType.InsertCommand, "@actionID", SqlDbType.Char, 20, ParameterDirection.Input, "actionID");

				///
				/// Update command
				/// 
				this.SetSqlCommand(SqlCommandType.UpdateCommand, "UP_CO_UpdateUser", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@userID", SqlDbType.Char, 20, ParameterDirection.Input, "userID");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@oldUserID", SqlDbType.Char, 20, ParameterDirection.Input, "oldUserID");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@password", SqlDbType.VarChar, 20, ParameterDirection.Input, "password");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@empCode", SqlDbType.Char, 10, ParameterDirection.Input, "empCode");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@securityLevel", SqlDbType.Int, 0, ParameterDirection.Input, "securityLevel");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@langCode", SqlDbType.Char, 5, ParameterDirection.Input, "langCode");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@datetimeFormat", SqlDbType.Char, 10, ParameterDirection.Input, "datetimeFormat");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@currencyGroupSeparator", SqlDbType.Char, 1, ParameterDirection.Input, "currencyGroupSeparator");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@currencyDecimalSeparator", SqlDbType.Char, 1, ParameterDirection.Input, "currencyDecimalSeparator");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@useFrom", SqlDbType.DateTime, 0, ParameterDirection.Input, "useFrom");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@useUntil", SqlDbType.DateTime, 0, ParameterDirection.Input, "useUntil");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@useERP", SqlDbType.Bit, 0, ParameterDirection.Input, "useERP");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@useGW", SqlDbType.Bit, 0, ParameterDirection.Input, "useGW");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@useSFA", SqlDbType.Bit, 0, ParameterDirection.Input, "useSFA");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@useBI", SqlDbType.Bit, 0, ParameterDirection.Input, "useBI");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@apprUsrGrp", SqlDbType.VarChar, 8, ParameterDirection.Input, "apprUsrGrp");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@srchUsrGrp", SqlDbType.VarChar, 8, ParameterDirection.Input, "srchUsrGrp");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@srchAuth", SqlDbType.VarChar, 8, ParameterDirection.Input, "srchAuth");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@pdaAccessID", SqlDbType.VarChar, 3, ParameterDirection.Input, "pdaAccessID");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@pdaUseYN", SqlDbType.Bit, 0, ParameterDirection.Input, "pdaUseYN");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@actionBy", SqlDbType.Char, 10, actionBy);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@actionID", SqlDbType.Char, 20, actionID);

				///
				/// Delete command
				/// 
				//this.SetSqlCommand(SqlCommandType.DeleteCommand, "UP_CO_DeleteUser", CommandType.StoredProcedure);
				//this.AddSqlParameter(SqlCommandType.DeleteCommand, "@userID", SqlDbType.Char, 20, ParameterDirection.Input, "userID");

				bReturn = UpdateData(changedUsers, "Users", false);
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}


		public bool ChangeUserPassword(string userID, string newPassword)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			bool bReturn = false;
			try
			{
				this.SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_ChangeUserPassword", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@userID", SqlDbType.VarChar, 20, userID);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@password", SqlDbType.VarChar, 20, newPassword);
				ExecuteNonQuery();
				bReturn = true;
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}

		#endregion

		#region Groups


		/// <summary>
		/// Return 직위, 직책, 부서
		/// </summary>
		/// <returns></returns>
		public DataSet GetGroupRelatedData(string cultureName)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_GetGroupsRelatedData", CommandType.StoredProcedure);
				AddSqlParameter(SqlCommandType.SelectCommand, "@cultureName", SqlDbType.Char, 5, cultureName);
				dsReturn = ExecuteFill();
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}

		/// <summary>
		/// Return 그룹
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllGroups(string cultureName)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_GetAllGroups", CommandType.StoredProcedure);
				AddSqlParameter(SqlCommandType.SelectCommand, "@cultureName", SqlDbType.Char, 5, cultureName);
				dsReturn = ExecuteFill();
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}

		/// <summary>
		/// Update changes to 그룹
		/// </summary>
		/// <param name="changedGroups"></param>
		/// <returns></returns>
		public bool UpdateGroups(DataSet changedGroups, string actionID, string actionBy)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			bool bReturn = false;
			try
			{
				///
				/// Insert command
				/// 
				this.SetSqlCommand(SqlCommandType.InsertCommand, "UP_CO_InsertGroup", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@groupID", SqlDbType.Int, 0, ParameterDirection.Output, "groupID");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@groupNM", SqlDbType.VarChar, 50, ParameterDirection.Input, "groupNM");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@description", SqlDbType.VarChar, 255, ParameterDirection.Input, "description");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@deptCode", SqlDbType.Char, 10, ParameterDirection.Input, "deptCode");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@position", SqlDbType.Char, 8, ParameterDirection.Input, "position");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@duty", SqlDbType.Char, 8, ParameterDirection.Input, "duty");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@parentGroupID", SqlDbType.Int, 0, ParameterDirection.Input, "parentGroupID");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@actionBy", SqlDbType.Char, 10, actionBy);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@actionID", SqlDbType.Char, 20, actionID);

				///
				/// Update command
				/// 
				this.SetSqlCommand(SqlCommandType.UpdateCommand, "UP_CO_UpdateGroup", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@groupID", SqlDbType.Int, 0, ParameterDirection.Input, "groupID");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@groupNM", SqlDbType.VarChar, 50, ParameterDirection.Input, "groupNM");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@description", SqlDbType.VarChar, 255, ParameterDirection.Input, "description");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@deptCode", SqlDbType.Char, 10, ParameterDirection.Input, "deptCode");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@position", SqlDbType.Char, 8, ParameterDirection.Input, "position");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@duty", SqlDbType.Char, 8, ParameterDirection.Input, "duty");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@parentGroupID", SqlDbType.Int, 0, ParameterDirection.Input, "parentGroupID");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@actionBy", SqlDbType.Char, 10, actionBy);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@actionID", SqlDbType.Char, 20, actionID);

				///
				/// Delete command
				/// 
				this.SetSqlCommand(SqlCommandType.DeleteCommand, "UP_CO_DeleteGroup", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@groupID", SqlDbType.Int, 0, ParameterDirection.Input, "groupID");

				bReturn = UpdateData(changedGroups, "Table", false);
				changedGroups.Tables[0].AcceptChanges();


				/// 
				/// Users
				/// 
				foreach (DataRow dr in changedGroups.Tables[1].Rows)
				{
					if (dr.RowState == DataRowState.Deleted)
					{
						continue;
					}

					int groupID = Convert.ToInt32(dr["groupID"]);
					if (groupID < 0)
					{
						DataRow[] parentRows = changedGroups.Tables[0].Select(string.Format("oldGroupID = {0}", groupID));
						if (parentRows.Length != 0)
						{
							dr["groupID"] = parentRows[0]["groupID"];
						}
					}
				}
				

				this.SetSqlCommand(SqlCommandType.InsertCommand, "UP_CO_InsertGroupUser", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@groupID", SqlDbType.Int, 0, ParameterDirection.Input, "groupID");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@userID", SqlDbType.VarChar, 20, ParameterDirection.Input, "userID");

				///
				/// Delete command
				/// 
				this.SetSqlCommand(SqlCommandType.DeleteCommand, "UP_CO_DeleteGroupUser", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@groupID", SqlDbType.Int, 0, ParameterDirection.Input, "groupID");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@userID", SqlDbType.VarChar, 20, ParameterDirection.Input, "userID");

				bReturn = UpdateData(changedGroups, "Table1", false);
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}

		#endregion

		#region Resource - Temporary

		/// <summary>
		/// Get resource text from db
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string DBInternalGetString(string cultureName, string resourceType, string name)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			string resourceText = string.Empty;
			try
			{
				SetSqlCommand(SqlCommandType.SelectCommand, string.Concat("WP_SY_Get", resourceType));
				AddSqlParameter(SqlCommandType.SelectCommand, "@CultureName", SqlDbType.NChar, 5, cultureName);
				AddSqlParameter(SqlCommandType.SelectCommand, string.Concat("@", resourceType, "ID"), SqlDbType.NChar, 5, name.Substring(1));

                object res = ExecuteScalar();
                resourceText = res == null ? name : Convert.ToString(res);
            }
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
                resourceText = name;
			}
			finally
			{
				LoggingEnd(oTimeStamp, this, MethodInfo.GetCurrentMethod().Name);
			}

			return resourceText;
		}


		#endregion		

		#region Privates

		public static string GetAdminAccount()
		{
			return Base.BifrostDecrypt(Base.GetConfigString("ImpersonateAccount"));
		}

		public static string GetAdminPassword()
		{
			return Base.BifrostEncrypt(Base.GetConfigString("ImpersonatePassword"));
		}

		#endregion

        #region AutoCount
        public string GetAutoCount(string FirmCode, string CountType, string dateTime)
        {
            SetSqlCommand(SqlCommandType.SelectCommand, "Bifrost.SP_CM_Autocount");
            AddSqlParameter(SqlCommandType.SelectCommand, "@FirmCode", SqlDbType.NVarChar, 8, FirmCode);
			AddSqlParameter(SqlCommandType.SelectCommand, "@AutoCountNo", SqlDbType.NVarChar, 20, CountType);
			AddSqlParameter(SqlCommandType.SelectCommand, "@Dt", SqlDbType.DateTime, 8, dateTime);

			return  (string)ExecuteScalar();
        }
         
        #endregion AutoCount

        #region SY_User Transaction
        /// <summary>
        ///	User INSERT, UPDATE, DELETE
        /// </summary>
        public bool SY_UserTransaction01(DataSet changedDS, string[] strParams)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool boolData = false;

            try
            {
                SetSqlCommand(SqlCommandType.InsertCommand, "WP_SY_UserTransaction01");
                AddSqlParameter(SqlCommandType.InsertCommand, "@Mode", SqlDbType.NVarChar, 1, "I");
                AddSqlParameter(SqlCommandType.InsertCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[0]);
                AddSqlParameter(SqlCommandType.InsertCommand, "@UserID", SqlDbType.NVarChar, 20, ParameterDirection.Input, "UserID");
                AddSqlParameter(SqlCommandType.InsertCommand, "@UserName", SqlDbType.NVarChar, 30, ParameterDirection.Input, "UserName");
                AddSqlParameter(SqlCommandType.InsertCommand, "@UserEngName", SqlDbType.NVarChar, 50, ParameterDirection.Input, "UserEngName");
                AddSqlParameter(SqlCommandType.InsertCommand, "@ValidTerm", SqlDbType.NVarChar, 10, ParameterDirection.Input, "ValidTerm");
                AddSqlParameter(SqlCommandType.InsertCommand, "@UserGubun", SqlDbType.NVarChar, 1, ParameterDirection.Input, "UserGubun");
                AddSqlParameter(SqlCommandType.InsertCommand, "@UseYN", SqlDbType.Int, 4, ParameterDirection.Input, "UseYN");
                AddSqlParameter(SqlCommandType.InsertCommand, "@EmpCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "EmpCode");
                AddSqlParameter(SqlCommandType.InsertCommand, "@SalPurCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "SalPurCode");
                AddSqlParameter(SqlCommandType.InsertCommand, "@PlantCode", SqlDbType.NVarChar, 8, ParameterDirection.Input, "PlantCode");
                AddSqlParameter(SqlCommandType.InsertCommand, "@UpdateID", SqlDbType.NVarChar, 20, strParams[1]);

                SetSqlCommand(SqlCommandType.UpdateCommand, "WP_SY_UserTransaction01");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@Mode", SqlDbType.NVarChar, 1, "U");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[0]);
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UserID", SqlDbType.NVarChar, 20, ParameterDirection.Input, "UserID");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UserName", SqlDbType.NVarChar, 30, ParameterDirection.Input, "UserName");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UserEngName", SqlDbType.NVarChar, 50, ParameterDirection.Input, "UserEngName");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@ValidTerm", SqlDbType.NVarChar, 10, ParameterDirection.Input, "ValidTerm");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UserGubun", SqlDbType.NVarChar, 1, ParameterDirection.Input, "UserGubun");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UseYN", SqlDbType.Int, 4, ParameterDirection.Input, "UseYN");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@EmpCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "EmpCode");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@SalPurCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "SalPurCode");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@PlantCode", SqlDbType.NVarChar, 8, ParameterDirection.Input, "PlantCode");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UpdateID", SqlDbType.NVarChar, 20, strParams[1]);

                SetSqlCommand(SqlCommandType.DeleteCommand, "WP_SY_UserTransaction01");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@Mode", SqlDbType.NVarChar, 1, "D");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[0]);
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UserID", SqlDbType.NVarChar, 20, ParameterDirection.Input, "UserID");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UserName", SqlDbType.NVarChar, 30, ParameterDirection.Input, "UserName");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UserEngName", SqlDbType.NVarChar, 50, ParameterDirection.Input, "UserEngName");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@ValidTerm", SqlDbType.NVarChar, 10, ParameterDirection.Input, "ValidTerm");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UserGubun", SqlDbType.NVarChar, 1, ParameterDirection.Input, "UserGubun");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UseYN", SqlDbType.Int, 4, ParameterDirection.Input, "UseYN");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@EmpCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "EmpCode");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@SalPurCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "SalPurCode");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@PlantCode", SqlDbType.NVarChar, 8, ParameterDirection.Input, "PlantCode");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UpdateID", SqlDbType.NVarChar, 20, strParams[1]);


                //Transaction이 필요없는 경우
                //boolData = UpdateData(changedDS, false);
                //Transaction이 필요한 경우
                boolData = UpdateData(changedDS, true);
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
            return boolData;
        }
        #endregion

        #region SY_Role Transaction
        /// <summary>
        ///	Role INSERT, UPDATE, DELETE
        /// </summary>
        public bool SY_RoleTransaction01(DataSet changedDS, string[] strParams)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool boolData = false;

            try
            {
                SetSqlCommand(SqlCommandType.InsertCommand, "WP_SY_RoleTransaction01");
                AddSqlParameter(SqlCommandType.InsertCommand, "@Mode", SqlDbType.NVarChar, 1, "I");
                AddSqlParameter(SqlCommandType.InsertCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[0]);
                AddSqlParameter(SqlCommandType.InsertCommand, "@RoleCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "RoleCode");
                AddSqlParameter(SqlCommandType.InsertCommand, "@RoleName", SqlDbType.NVarChar, 40, ParameterDirection.Input, "RoleName");
                AddSqlParameter(SqlCommandType.InsertCommand, "@UseYN", SqlDbType.Int, 4, ParameterDirection.Input, "UseYN");
                AddSqlParameter(SqlCommandType.InsertCommand, "@UserID", SqlDbType.NVarChar, 20, strParams[1]);

                SetSqlCommand(SqlCommandType.UpdateCommand, "WP_SY_RoleTransaction01");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@Mode", SqlDbType.NVarChar, 1, "U");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[0]);
                AddSqlParameter(SqlCommandType.UpdateCommand, "@RoleCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "RoleCode");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@RoleName", SqlDbType.NVarChar, 40, ParameterDirection.Input, "RoleName");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UseYN", SqlDbType.Int, 4, ParameterDirection.Input, "UseYN");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UserID", SqlDbType.NVarChar, 20, strParams[1]);

                SetSqlCommand(SqlCommandType.DeleteCommand, "WP_SY_RoleTransaction01");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@Mode", SqlDbType.NVarChar, 1, "D");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[0]);
                AddSqlParameter(SqlCommandType.DeleteCommand, "@RoleCode", SqlDbType.NVarChar, 20, ParameterDirection.Input, "RoleCode");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@RoleName", SqlDbType.NVarChar, 40, ParameterDirection.Input, "RoleName");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UseYN", SqlDbType.Int, 4, ParameterDirection.Input, "UseYN");
                AddSqlParameter(SqlCommandType.DeleteCommand, "@UserID", SqlDbType.NVarChar, 20, strParams[1]);

                //Transaction이 필요없는 경우
                //boolData = UpdateData(changedDS, false);
                //Transaction이 필요한 경우
                boolData = UpdateData(changedDS, true);
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
            return boolData;
        }
        #endregion

        #region User Menu Transaction
        public bool SY_UserMenuTransaction01(string[] strParams)
        {
            //Start Logging
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                this.SetSqlCommand(SqlCommandType.SelectCommand, "WP_SY_UserMenuTransaction01");
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@Mode", SqlDbType.NVarChar, 1, strParams[0]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[1]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.NVarChar, 20, strParams[2]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@MenuCode", SqlDbType.NVarChar, 4000, strParams[3]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@UpdateID", SqlDbType.NVarChar, 20, strParams[4]);

                ExecuteNonQuery();
                bReturn = true;
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
            return bReturn;
        }
        #endregion

        #region Role Menu Transaction
        public bool SY_RoleMenuTransaction01(string[] strParams)
        {
            //Start Logging
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                this.SetSqlCommand(SqlCommandType.SelectCommand, "WP_SY_RoleMenuTransaction01");
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@Mode", SqlDbType.NVarChar, 1, strParams[0]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[1]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@RoleCode", SqlDbType.NVarChar, 20, strParams[2]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@MenuCode", SqlDbType.NVarChar, 4000, strParams[3]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.NVarChar, 20, strParams[4]);

                ExecuteNonQuery();
                bReturn = true;
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
            return bReturn;
        }
        #endregion

        #region Role User Transaction
        public bool SY_RoleUserTransaction01(string[] strParams)
        {
            //Start Logging
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                this.SetSqlCommand(SqlCommandType.SelectCommand, "WP_SY_RoleUserTransaction01");
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@Mode", SqlDbType.NVarChar, 1, strParams[0]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[1]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@RoleCode", SqlDbType.NVarChar, 20, strParams[2]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@UserID", SqlDbType.NVarChar, 4000, strParams[3]);
                this.AddSqlParameter(SqlCommandType.SelectCommand, "@UpdateID", SqlDbType.NVarChar, 20, strParams[4]);

                ExecuteNonQuery();
                bReturn = true;
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
            return bReturn;
        }
        #endregion

        #region User Menu Transaction (Authority)
        /// <summary>
        ///	User Menu Authority Update
        /// </summary>
        public bool SY_UserMenuAuthorityTransaction01(DataSet changedDS, string[] strParams)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool boolData = false;

            try
            {
                SetSqlCommand(SqlCommandType.UpdateCommand, "WP_SY_UserMenuAuthorityTransaction01");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@FirmCode", SqlDbType.NVarChar, 8, strParams[0]);
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UserID", SqlDbType.NVarChar, 20, ParameterDirection.Input, "UserID");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@MenuID", SqlDbType.Int, 4, ParameterDirection.Input, "MenuID");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@ReadAuthority", SqlDbType.Bit, 1, ParameterDirection.Input, "ReadAuthority");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@SaveAuthority", SqlDbType.Bit, 1, ParameterDirection.Input, "SaveAuthority");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@DeleteAuthority", SqlDbType.Bit, 1, ParameterDirection.Input, "DeleteAuthority");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@ExcelExportAuthority", SqlDbType.Bit, 1, ParameterDirection.Input, "ExcelExportAuthority");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@PrintAuthority", SqlDbType.Bit, 1, ParameterDirection.Input, "PrintAuthority");
                AddSqlParameter(SqlCommandType.UpdateCommand, "@UpdateID", SqlDbType.NVarChar, 20, strParams[1]);

                //Transaction이 필요없는 경우
                //boolData = UpdateData(changedDS, false);
                //Transaction이 필요한 경우
                boolData = UpdateData(changedDS, true);
            }
            catch (Exception ex)
            {
                BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
            return boolData;
        }
        #endregion
    }
}
