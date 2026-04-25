using System;
using System.Data;
using System.Reflection;
using System.Transactions;

using Bifrost.SY.DSL.SYS;
using Bifrost.Framework;

namespace Bifrost.SY.BSL.SYS
{
    public class UserBSL : BSLBase
	{
		#region 사용자 인증

        /// <summary>
        /// Authenticate user, return loginStatus, user information
        /// 
        /// </summary>
        /// <param name="cultureName"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="loginIP"></param>
        /// <param name="fromGroupware"></param>
        /// <returns></returns>
        public DataSet IsAuthenticated(string CompanyCode, string LanguageCode, string userID, string password, string loginIP)
        {
            UserLoginStatus loginStatus = UserLoginStatus.UnknownError;

            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);

            DataSet dsReturn = null;

            try
            {
                using (UserDSL userDSL = new UserDSL(SubSystemType.SYS))
                {
                    string realUserID = Base.BifrostDecrypt(userID);
                    string realPassword = Base.BifrostEncrypt(password);

                    loginStatus = userDSL.IsAuthenticated(CompanyCode, realUserID, realPassword);

                    if (loginStatus == UserLoginStatus.LogInSuccess)
                    {
                        dsReturn = userDSL.GetUserInfo(CompanyCode, LanguageCode, realUserID, loginIP);
                    }
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

            /// 
            /// user table
            /// 
            if (dsReturn != null)
            {
                returnDS.Tables.Add(dsReturn.Tables[0].Copy());
                returnDS.Tables[1].TableName = "UserInfo";
            }

            ///
            /// Return
            /// 
            return returnDS;
        }
		/// <summary>
		/// Do log out
		/// </summary>
		/// <param name="userID"></param>
		public void LogOut(string userID)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			try
			{
				using (UserDSL userDSL = new UserDSL())
				{
					userDSL.LogOut(userID);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public bool IsUserExpired(string userID)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;
			try
			{
				using (UserDSL userDSL = new UserDSL())
				{
					bReturn = userDSL.IsUserExpired(userID);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}

		#endregion 사용자 인증

		#region 사용자관리 화면

		/// <summary>
		/// Return department and user tables
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
				using (UserDSL userDSL = new UserDSL())
				{
					dsReturn = userDSL.GetAllUsersNDepts(cultureName);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
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
		/// <returns></returns>
		public DataSet GetAllUsers(string cultureName)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				using (UserDSL userDSL = new UserDSL())
				{
					dsReturn = userDSL.GetAllUsers(cultureName);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
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
            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);
            bool bReturn = false;

            // impersonate
            try
            {
                DataSet dsTmp = changedUsers.Copy();
                using (UserDSL userDSL = new UserDSL(SubSystemType.FRAMEWORK))
                {
                    bReturn = userDSL.UpdateUsers(changedUsers, actionID, actionBy);
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        /// <summary>
        /// Change user's password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        public bool ChangeUserPassword(string userID, string password)
        {
            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);
            bool bReturn = false;

            try
            {
                using (UserDSL userDSL = new UserDSL(SubSystemType.FRAMEWORK))
                {
                    bReturn = userDSL.ChangeUserPassword(userID, password);
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        
        /* UpdateUsers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changedUsers"></param>
        /// <returns></returns>
        public bool UpdateUsers(DataSet changedUsers, bool activeDirInfoChanged, string actionID, string actionBy)
        {
            //로깅시작
            TimeStamp ts = null;
            LoggingStart(ref ts);
            bool bReturn = false;

            // impersonate
            try
            {
                using (TransactionScope transactionscope1 = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0)))//, transOption))
                {
                    if (activeDirInfoChanged)
                    {
                        string sErrorMsg = string.Empty;

                        using (ActiveDirectory ad = new ActiveDirectory())
                        {
                            foreach (DataRow dr in changedUsers.Tables["Users"].Rows)
                            {
                                string userID = dr["userID"].ToString().Trim();

                                if (userID == string.Empty)
                                {
                                    string orgUserID = dr["oldUserID"].ToString().Trim();
                                    if (ad.Exists(orgUserID))
                                        ad.Remove(orgUserID);
                                }
                                else
                                {
                                    if (!ad.Exists(userID))
                                    {
                                        sErrorMsg += ad.Add(userID,
                                            dr["empCode"].ToString().Trim(),
                                            dr["deptCode"].ToString().Trim(),
                                            dr["empName"].ToString().Trim(),
                                            dr["password"].ToString(), Convert.ToInt16(dr["securityLevel"]) == 2);
                                    }
                                    else
                                    {
                                        sErrorMsg += ad.Enable(userID);
                                        sErrorMsg += ad.SetPassword(userID, dr["password"].ToString());
                                        sErrorMsg += ad.CreateMailBox(userID);
                                    }
                                }
                            }
                        }

                        if (sErrorMsg != string.Empty)
                        {
                            throw new Exception(sErrorMsg);
                        }
                    }

                    DataSet dsTmp = changedUsers.Copy();
                    using (UserDSL userDSL = new UserDSL(SubSystemType.FRAMEWORK))
                    {
                        bReturn = userDSL.UpdateUsers(changedUsers, actionID, actionBy);
                    }
                    using (UserDSL userDSL = new UserDSL(SubSystemType.PP))
                    {
                        bReturn = userDSL.UpdateUsers(dsTmp, actionID, actionBy);
                    }

                    transactionscope1.Complete();
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        */
        /* SyncUsersWithAD
		/// <summary>
		/// 
		/// </summary>
		/// <param name="changedUsers"></param>
		/// <param name="activeDirInfoChanged"></param>
		/// <param name="actionID"></param>
		/// <param name="actionBy"></param>
		/// <returns></returns>
		public bool SyncUsersWithAD(DataSet changedUsers, bool createMailBox, string actionID, string actionBy)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;

			// impersonate
			try
			{
				using (ActiveDirectory ad = new ActiveDirectory())
				{
					foreach (DataRow dr in changedUsers.Tables["Users"].Rows)
					{
						string userID = dr["userID"].ToString().Trim();

						if (!ad.Exists(userID))
						{
							ad.Add(userID,
								dr["empCode"].ToString().Trim(),
								dr["deptCode"].ToString().Trim(),
								dr["empName"].ToString().Trim(),
								dr["password"].ToString(), Convert.ToInt16(dr["securityLevel"]) == 1);
						}

						if (createMailBox)
						{
							ad.CreateMailBox(userID);
						}
					}
				}

				bReturn = true;

			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}
        */
        /* CreateUserMailBoxes
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userList"></param>
		/// <param name="actionID"></param>
		/// <param name="actionBy"></param>
		/// <returns></returns>
		public bool CreateUserMailBoxes(DataSet userList, string actionID, string actionBy)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;

			// impersonate
			try
			{
				using (ActiveDirectory ad = new ActiveDirectory())
				{
					foreach (DataRow dr in userList.Tables["Users"].Rows)
					{
						string userID = dr["userID"].ToString().Trim();

						if (ad.Exists(userID))
						{
							ad.CreateMailBox(userID);
						}
					}
				}

				bReturn = true;

			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}
        */
        /* UpdateUserEmail
		/// <summary>
		/// Update user's email
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public string UpdateUserEmail(string userID, string email)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			string sReturn = string.Empty;

			try
			{
				using (ActiveDirectory ad = new ActiveDirectory())
				{
					sReturn = ad.UpdateEmail(userID, email);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return sReturn;
		}
        */
        /* UpdateGroupEmail
		/// <summary>
		/// Group's email
		/// </summary>
		/// <param name="deptCode"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public string UpdateGroupEmail(string deptCode, string email)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			string sReturn = string.Empty;

			try
			{
				using (ActiveDirectory ad = new ActiveDirectory())
				{
					sReturn = ad.UpdateGroupEmail(deptCode, email);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return sReturn;
		}
        */
        /* UpdateUserDepartment
		/// <summary>
		/// 사용장의 부서
		/// </summary>
		/// <param name="cn"></param>
		/// <param name="deptCode"></param>
		/// <returns></returns>
		public string UpdateUserDepartment(string cn, string deptCode)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			string sReturn = string.Empty;

			try
			{
				using (ActiveDirectory ad = new ActiveDirectory())
				{
					sReturn = ad.UpdateUserDepartment(cn, deptCode);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return sReturn;
		}
        */
        /* ChangeUserPassword
		/// <summary>
		/// Change user's password
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="password"></param>
		public bool ChangeUserPassword(string userID, string password)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;

			try
			{
				using (TransactionScope transactionscope1 = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0)))//, transOption))
				{

					using (UserDSL userDSL = new UserDSL(SubSystemType.FRAMEWORK))
					{
						bReturn = userDSL.ChangeUserPassword(userID, password);
					}

					using (UserDSL userDSL = new UserDSL(SubSystemType.PP))
					{
						userDSL.ChangeUserPassword(userID, password);
					}

					using (ActiveDirectory ad = new ActiveDirectory())
					{
						bReturn = ad.SetPassword(userID, password) == string.Empty;
					}

					transactionscope1.Complete();
				}

			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}
        */
        /* CheckUserID
		/// <summary>
		/// Check if this ID exists?
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public bool CheckUserID(string userID)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;

			try
			{
				using (ActiveDirectory ad = new ActiveDirectory())
				{
					bReturn = ad.Exists(userID);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}
        */
		#endregion 사용자관리 화면

		#region 사용자 정보 조회(세션)

		/// <summary>
		/// 사용자 정보 조회(세션)
		/// </summary>
		/// <param name="strUserid">사용자ID</param>
		/// <returns></returns>
		public DataSet GetUserInfo(string strUserid)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);

			DataSet dsReturn = null;
			try
			{
				using(UserDSL dslGwLogin = new UserDSL())
				{
					dsReturn = dslGwLogin.GetUserInfo(strUserid);
					dsReturn.Tables[0].TableName = "UserInfo";
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}
			return dsReturn;
		}

		/// <summary>
        /// SaveUserInfo
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
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;
			try
			{
				using (TransactionScope transactionscope1 = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0)))//, transOption))
				{
					using (UserDSL userDSL = new UserDSL(SubSystemType.FRAMEWORK))
					{
						bReturn = userDSL.SaveUserInfo(userID, digitalSign, password,
						digitalSignPicFileKey, langCode, dateTimeFormat, curGroupSep,
						curDecSep, actionID, actionBy);
						bReturn &= ChangeUserPassword(userID, password);
					}
					transactionscope1.Complete();
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;
		}

		#endregion

		#region Group관리 화면

		/// <summary>
		/// Return 직위, 직책, 부서, 그룹
		/// </summary>
		/// <param name="cultureName"></param>
		/// <returns></returns>
		public DataSet GetGroupRelatedData(string cultureName)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				UserDSL userDSL = new UserDSL();
				dsReturn = userDSL.GetGroupRelatedData(cultureName);
				dsReturn.Tables[0].TableName = "Position";
				dsReturn.Tables[1].TableName = "Duty";
				dsReturn.Tables[2].TableName = "Dept";
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
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
		/// <param name="cultureName"></param>
		/// <returns></returns>
		public DataSet GetAllGroups(string cultureName)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				UserDSL userDSL = new UserDSL();
				dsReturn = userDSL.GetAllGroups(cultureName);
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
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
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;

			try
			{
				DataSet dsTmp = changedGroups.Copy();
				using (UserDSL userDSL = new UserDSL(SubSystemType.FRAMEWORK))
				{
					bReturn = userDSL.UpdateGroups(changedGroups, actionID, actionBy);
				}
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return bReturn;		
		}
        /* UpdateDepts
		/// <summary>
		/// 
		/// </summary>
		/// <param name="deptCode"></param>
		/// <param name="deptName"></param>
		/// <param name="parentDeptCode"></param>
		/// <returns></returns>
		public string UpdateDepts(string deptCode, string deptName, string parentDeptCode, string[] userList)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			string sReturn = string.Empty;

			try
			{
				using (ActiveDirectory ad = new ActiveDirectory())
				{
					sReturn = ad.UpdateGroup(deptCode, deptName, parentDeptCode, userList);
				}
			}
			catch (Exception ex)
			{
				sReturn = ex.Message;
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return sReturn;
		}
		*/
		#endregion

		#region Resource - Temporary
	
		/// <summary>
		/// Get resource text from 
		/// </summary>
		/// <param name="cultureName"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public string DBInternalGetString(string cultureName, string resourceType, string name)
		{
			//로깅시작
			TimeStamp ts = null;
			LoggingStart(ref ts);
			string sReturn = string.Empty;
			try
			{
				UserDSL userDSL = new UserDSL(SubSystemType.FRAMEWORK);
				sReturn = userDSL.DBInternalGetString(cultureName, resourceType, name);
			}
			catch (Exception ex)
			{
				BifrostException.HandleBSLException(SubSystemType.FRAMEWORK, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return sReturn;

		}

		#endregion

        #region  SY_User Transaction
        /// <summary>
        ///	SY_User INSERT, UPDATE, DELETE
        /// </summary>
        public bool SY_UserTransaction01(DataSet changedDS, string[] strParams)
        {
            /// SP를 호출하여 SqlCommandType.SelectCommand 
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                using (Bifrost.SY.DSL.SYS.UserDSL objDSL = new Bifrost.SY.DSL.SYS.UserDSL(SubSystemType.SYS))
                {
                    bReturn = objDSL.SY_UserTransaction01(changedDS, strParams);
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        #endregion

        #region  SY_Role Transaction
        /// <summary>
        ///	SY_Role INSERT, UPDATE, DELETE
        /// </summary>
        public bool SY_RoleTransaction01(DataSet changedDS, string[] strParams)
        {
            /// SP를 호출하여 SqlCommandType.SelectCommand 
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                using (Bifrost.SY.DSL.SYS.UserDSL objDSL = new Bifrost.SY.DSL.SYS.UserDSL(SubSystemType.SYS))
                {
                    bReturn = objDSL.SY_RoleTransaction01(changedDS, strParams);
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        #endregion

        #region  SY_UserMenu Transaction
        /// <summary>
        ///	SY_UserMenu INSERT, UPDATE, DELETE
        /// </summary>
        public bool SY_UserMenuTransaction01(string[] strParams)
        {
            /// SP를 호출하여 SqlCommandType.SelectCommand 
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                using (Bifrost.SY.DSL.SYS.UserDSL objDSL = new Bifrost.SY.DSL.SYS.UserDSL(SubSystemType.SYS))
                {
                    bReturn = objDSL.SY_UserMenuTransaction01(strParams);
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        #endregion

        #region  SY_RoleMenu Transaction
        /// <summary>
        ///	SY_RoleMenu INSERT, UPDATE, DELETE
        /// </summary>
        public bool SY_RoleMenuTransaction01(string[] strParams)
        {
            /// SP를 호출하여 SqlCommandType.SelectCommand 
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                using (Bifrost.SY.DSL.SYS.UserDSL objDSL = new Bifrost.SY.DSL.SYS.UserDSL(SubSystemType.SYS))
                {
                    bReturn = objDSL.SY_RoleMenuTransaction01(strParams);
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        #endregion

        #region  SY_RoleUser Transaction
        /// <summary>
        ///	SY_RoleUser INSERT, UPDATE, DELETE
        /// </summary>
        public bool SY_RoleUserTransaction01(DataSet changedDS, string[] strParams)
        {
            /// SP를 호출하여 SqlCommandType.SelectCommand 
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                using (Bifrost.SY.DSL.SYS.UserDSL objDSL = new Bifrost.SY.DSL.SYS.UserDSL(SubSystemType.SYS))
                {
                    for (int i = 0; i < changedDS.Tables[0].Rows.Count; i++)
                    {
                        bReturn = objDSL.SY_RoleUserTransaction01(new string[] {  strParams[0],
                                                                                strParams[1],
                                                                                strParams[2],
                                                                                changedDS.Tables[0].Rows[i]["UserID"].ToString(),
                                                                                strParams[3]
                                                                             });
                    }
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        #endregion

        #region  SY_UserMenu Transaction (Authority)
        /// <summary>
        ///	SY_UserMenu Authority Update
        /// </summary>
        public bool SY_UserMenuAuthorityTransaction01(DataSet changedDS, string[] strParams)
        {
            /// SP를 호출하여 SqlCommandType.SelectCommand 
            TimeStamp ts = null;
            LoggingStart(ref ts);

            bool bReturn = false;

            try
            {
                using (Bifrost.SY.DSL.SYS.UserDSL objDSL = new Bifrost.SY.DSL.SYS.UserDSL(SubSystemType.SYS))
                {
                    bReturn = objDSL.SY_UserMenuAuthorityTransaction01(changedDS, strParams);
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(SubSystemType.SYS, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }

            return bReturn;
        }
        #endregion
	}
}
