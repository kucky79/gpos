using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Reflection;
using Bifrost.Framework;

namespace Bifrost.SY.DSL.SYS
{
	public class MenuDSL : DSLBase
	{
		public MenuDSL()
		{
		}

		public MenuDSL(SubSystemType subSysType) : base(subSysType)
		{
		}

		#region Menus

		/// <summary>
		/// Return menus which the user has permissions on
		/// </summary>
		/// <param name="langID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetAuthorizedMenus(string FirmCode, string langID, string userID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
				dsReturn = DBHelper.GetDataSet("WP_SY_GetAuthorizedMenus", new object[] { FirmCode, langID, userID });
            }
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}

		/// <summary>
		/// Return menus which the user has permissions on
		/// </summary>
		/// <param name="langID"></param>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetAuthorizedMenus(string langID, int groupID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
                dsReturn = DBHelper.GetDataSet("UP_CO_GetAuthorizedMenusByGroup", new object[] { langID, groupID });
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}


		/// <summary>
		/// Get all menus by system type 
		///		- ERP,...
		///		
		/// </summary>
		/// <param name="systemType"></param>
		/// <returns></returns>
        public DataSet GetMenus(string FirmCode, string langID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
                dsReturn = DBHelper.GetDataSet("WP_SY_GetMenus", new object[] { FirmCode, langID });
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}

		/// <summary>
		/// Get all menus by system type 
		///		- ERP,...
		///		
		/// </summary>
		/// <param name="systemType"></param>
		/// <returns></returns>
		public DataSet GetMenusByMenuGroup(int menuGroup, string langID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
                dsReturn = DBHelper.GetDataSet("UP_CO_GetMenusByMenuGroup", new object[] { menuGroup, langID });
			}
			catch (Exception ex)
			{
				BifrostException.HandleDSLException(SubSystemType.SYS, ex, this.GetType());
			}
			finally
			{
				LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
			}

			return dsReturn;
		}

		#endregion

		#region MyMenus

		/// <summary>
		/// Return all my menus of a user
		/// </summary>
		/// <param name="langID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetMyMenus(string langID, string userID)
		{
			//∑Œ±ÎΩ√¿€
			TimeStamp ts = null;
			LoggingStart(ref ts);
			DataSet dsReturn = null;
			try
			{
                dsReturn = DBHelper.GetDataSet("UP_CO_GetMyMenus", new object[] { langID, userID });
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
		#endregion

		#region Menus ±««—

		/// <summary>
		/// Return list of authorized users/group on a menu
		/// </summary>
		/// <param name="menuID"></param>
		/// <param name="cultureName"></param>
		/// <returns></returns>
		public DataSet GetAuthorizedObjectsOnMenu(int menuID, string cultureName)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			DataSet dsResult = null;

			try
			{
				this.SetSqlCommand(SqlCommandType.SelectCommand, "UP_CO_GetAuthObjectsOnMenu", CommandType.StoredProcedure);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@menuID", SqlDbType.Int, 0, menuID);
				this.AddSqlParameter(SqlCommandType.SelectCommand, "@cultureName", SqlDbType.Char, 5, cultureName);

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
		/// Save ±««—
		/// </summary>
		/// <param name="menuID"></param>
		/// <param name="cultureName"></param>
		/// <param name="objects"></param>
		/// <returns></returns>
		public bool SaveAuthorizedObjectsOnMenu(int menuID, DataSet objects)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			bool bReturn = false;
			try
			{
				this.SetSqlCommand(SqlCommandType.InsertCommand, "UP_CO_SaveMenuAuth", CommandType.StoredProcedure);
				this.insertCommand.CommandTimeout = 90;
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@updateType", SqlDbType.TinyInt, 0, 0);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@menuID", SqlDbType.Int, 0, menuID);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@objectID", SqlDbType.VarChar, 20, ParameterDirection.Input, "objectID");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@objectType", SqlDbType.SmallInt, 0, ParameterDirection.Input, "objectType");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@create", SqlDbType.Bit, 0, ParameterDirection.Input, "allowCreate");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@read", SqlDbType.Bit, 0, ParameterDirection.Input, "allowRead");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@update", SqlDbType.Bit, 0, ParameterDirection.Input, "allowUpdate");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@delete", SqlDbType.Bit, 0, ParameterDirection.Input, "allowDelete");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@exportExcel", SqlDbType.Bit, 0, ParameterDirection.Input, "allowExportExcel");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@print", SqlDbType.Bit, 0, ParameterDirection.Input, "allowPrint");

				this.SetSqlCommand(SqlCommandType.UpdateCommand, "UP_CO_SaveMenuAuth", CommandType.StoredProcedure);
				this.updateCommand.CommandTimeout = 90;
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@updateType", SqlDbType.TinyInt, 0, 1);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@menuID", SqlDbType.Int, 0, menuID);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@objectID", SqlDbType.VarChar, 20, ParameterDirection.Input, "objectID");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@objectType", SqlDbType.SmallInt, 0, ParameterDirection.Input, "objectType");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@create", SqlDbType.Bit, 0, ParameterDirection.Input, "allowCreate");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@read", SqlDbType.Bit, 0, ParameterDirection.Input, "allowRead");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@update", SqlDbType.Bit, 0, ParameterDirection.Input, "allowUpdate");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@delete", SqlDbType.Bit, 0, ParameterDirection.Input, "allowDelete");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@exportExcel", SqlDbType.Bit, 0, ParameterDirection.Input, "allowExportExcel");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@print", SqlDbType.Bit, 0, ParameterDirection.Input, "allowPrint");

				this.SetSqlCommand(SqlCommandType.DeleteCommand, "UP_CO_SaveMenuAuth", CommandType.StoredProcedure);
				this.deleteCommand.CommandTimeout = 90;
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@updateType", SqlDbType.TinyInt, 0, 2);
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@menuID", SqlDbType.Int, 0, menuID);
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@objectID", SqlDbType.VarChar, 20, ParameterDirection.Input, "objectID");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@objectType", SqlDbType.SmallInt, 0, ParameterDirection.Input, "objectType");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@create", SqlDbType.Bit, 0, ParameterDirection.Input, "allowCreate");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@read", SqlDbType.Bit, 0, ParameterDirection.Input, "allowRead");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@update", SqlDbType.Bit, 0, ParameterDirection.Input, "allowUpdate");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@delete", SqlDbType.Bit, 0, ParameterDirection.Input, "allowDelete");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@exportExcel", SqlDbType.Bit, 0, ParameterDirection.Input, "allowExportExcel");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@print", SqlDbType.Bit, 0, ParameterDirection.Input, "allowPrint");

				this.UpdateData(objects, false);

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

		/// <summary>
		/// Save ±««—
		/// </summary>
		/// <param name="objectIsGroup"></param>
		/// <param name="objectID"></param>
		/// <param name="objects"></param>
		/// <returns></returns>
		public bool SaveAuthorizedMenusOnObject(bool objectIsGroup, string objectID, DataSet objects)
		{
			TimeStamp oTimeStamp = null;
			LoggingStart(ref oTimeStamp);

			bool bReturn = false;
			try
			{
				this.SetSqlCommand(SqlCommandType.InsertCommand, "UP_CO_SaveMenuAuth", CommandType.StoredProcedure);
				this.insertCommand.CommandTimeout = 90;
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@updateType", SqlDbType.TinyInt, 0, 0);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@menuID", SqlDbType.Int, 0, ParameterDirection.Input, "menuID");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@objectID", SqlDbType.VarChar, 20, objectID);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@objectType", SqlDbType.Bit, 0, objectIsGroup);
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@create", SqlDbType.Bit, 0, ParameterDirection.Input, "allowCreate");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@read", SqlDbType.Bit, 0, ParameterDirection.Input, "allowRead");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@update", SqlDbType.Bit, 0, ParameterDirection.Input, "allowUpdate");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@delete", SqlDbType.Bit, 0, ParameterDirection.Input, "allowDelete");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@exportExcel", SqlDbType.Bit, 0, ParameterDirection.Input, "allowExportExcel");
				this.AddSqlParameter(SqlCommandType.InsertCommand, "@print", SqlDbType.Bit, 0, ParameterDirection.Input, "allowPrint");

				this.SetSqlCommand(SqlCommandType.UpdateCommand, "UP_CO_SaveMenuAuth", CommandType.StoredProcedure);
				this.updateCommand.CommandTimeout = 90;
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@updateType", SqlDbType.TinyInt, 0, 1);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@menuID", SqlDbType.Int, 0, ParameterDirection.Input, "menuID");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@objectID", SqlDbType.VarChar, 20, objectID);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@objectType", SqlDbType.Bit, 0, objectIsGroup);
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@create", SqlDbType.Bit, 0, ParameterDirection.Input, "allowCreate");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@read", SqlDbType.Bit, 0, ParameterDirection.Input, "allowRead");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@update", SqlDbType.Bit, 0, ParameterDirection.Input, "allowUpdate");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@delete", SqlDbType.Bit, 0, ParameterDirection.Input, "allowDelete");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@exportExcel", SqlDbType.Bit, 0, ParameterDirection.Input, "allowExportExcel");
				this.AddSqlParameter(SqlCommandType.UpdateCommand, "@print", SqlDbType.Bit, 0, ParameterDirection.Input, "allowPrint");

				this.SetSqlCommand(SqlCommandType.DeleteCommand, "UP_CO_SaveMenuAuth", CommandType.StoredProcedure);
				this.deleteCommand.CommandTimeout = 90;
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@updateType", SqlDbType.TinyInt, 0, 2);
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@menuID", SqlDbType.Int, 0, ParameterDirection.Input, "menuID");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@objectID", SqlDbType.VarChar, 20, objectID);
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@objectType", SqlDbType.Bit, 0, objectIsGroup);
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@create", SqlDbType.Bit, 0, ParameterDirection.Input, "allowCreate");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@read", SqlDbType.Bit, 0, ParameterDirection.Input, "allowRead");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@update", SqlDbType.Bit, 0, ParameterDirection.Input, "allowUpdate");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@delete", SqlDbType.Bit, 0, ParameterDirection.Input, "allowDelete");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@exportExcel", SqlDbType.Bit, 0, ParameterDirection.Input, "allowExportExcel");
				this.AddSqlParameter(SqlCommandType.DeleteCommand, "@print", SqlDbType.Bit, 0, ParameterDirection.Input, "allowPrint");

				this.UpdateData(objects, false);
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

	}
}
