using System;
using System.Data;
using System.Reflection;
using System.Transactions;

using Bifrost.Framework;

namespace Bifrost.SY.BSL.SYS
{
    public class MenuBSL : BSLBase
	{
		private string _CultureName;

		public string CultureName
		{
			get { return _CultureName; }
			set { _CultureName = value; }
		}
	

		public MenuBSL(string cultureName)
		{
			this._CultureName = cultureName;
		}

		#region Menus

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public DataSet GetMenus(string CompanyCode)
        {
            //∑Œ±ÎΩ√¿€
            TimeStamp ts = null;
            LoggingStart(ref ts);
            DataSet dsReturn = null;
            try
            {
                using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL())
                {
                    dsReturn = menuDSL.GetMenus(CompanyCode,_CultureName);
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
		/// <param name="systemType"></param>
		/// <returns></returns>
        public DataSet GetAuthorizedMenus(string CompanyCode,string userID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL())
				{
                    dsReturn = menuDSL.GetAuthorizedMenus(CompanyCode,_CultureName, userID);
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
		/// Return all authorized menus by group
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetAuthorizedMenus(int groupID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL())
				{
					dsReturn = menuDSL.GetAuthorizedMenus(_CultureName, groupID);
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
		/// Return authorization value
		/// </summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns></returns>
		private object _CombineAuthValue(object value1, object value2)
		{
			if (value1 == DBNull.Value && value2 == DBNull.Value) return false;
			return _BoolConverter(value1) & _BoolConverter(value2);
		}

		/// <summary>
		/// Return bool from object
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private bool _BoolConverter(object value)
		{
			try
			{
				if (value != null && value.GetType().FullName == "System.Boolean")
					return (bool)value;
				else
					return (value == DBNull.Value || value == null) ? false :
						Convert.ToInt16(value.ToString().Trim()) == 1;

			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="menuGroup"></param>
		/// <returns></returns>
		public DataSet GetMenusByMenuGroup(int menuGroup)
		{
			using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL())
			{
				return menuDSL.GetMenusByMenuGroup(menuGroup, _CultureName);
			}
		}

		#endregion Menus

		#region MyMenus


		/// <summary>
		/// Return my menus
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetMyMenus(string userID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL())
				{
					dsReturn = menuDSL.GetMyMenus(_CultureName, userID);
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

		#endregion

		#region Menu ±««—

		/// <summary>
		/// 
		/// </summary>
		/// <param name="menuID"></param>
		/// <param name="cultureName"></param>
		/// <returns></returns>
		public DataSet GetAuthorizedObjectsOnMenu(int menuID, string cultureName)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL())
				{
					dsReturn = menuDSL.GetAuthorizedObjectsOnMenu(menuID, cultureName);
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
		/// <param name="menuID"></param>
		/// <param name="cultureName"></param>
		/// <param name="objects"></param>
		/// <returns></returns>
		public bool SaveAuthorizedObjectsOnMenu(int menuID, string cultureName, DataSet objects)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;
			try
			{
                //using (TransactionScope transactionscope1 = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0)))//, transOption))
                //{
					DataSet dsTmp = objects.Copy();
					using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL(SubSystemType.FRAMEWORK))
					{
						bReturn = menuDSL.SaveAuthorizedObjectsOnMenu(menuID, objects);
					}
					//transactionscope1.Complete();
				//}
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
		/// Save authorized menus
		/// </summary>
		/// <param name="objectIsGroup"></param>
		/// <param name="objectID"></param>
		/// <param name="programs"></param>
		/// <returns></returns>
		public bool SaveAuthorizedMenusOnObject(bool objectIsGroup, string objectID, DataSet programs)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			bool bReturn = false;
			try
			{
                //using (TransactionScope transactionscope1 = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0)))//, transOption))
                //{
					DataSet dsTmp = programs.Copy();
					using (Bifrost.SY.DSL.SYS.MenuDSL menuDSL = new Bifrost.SY.DSL.SYS.MenuDSL(SubSystemType.FRAMEWORK))
					{
						bReturn = menuDSL.SaveAuthorizedMenusOnObject(objectIsGroup, objectID, programs);
					}
					//transactionscope1.Complete();
				//}
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
	}
}
