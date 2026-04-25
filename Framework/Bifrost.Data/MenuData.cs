#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.IO;
using System.Runtime.Serialization;

#endregion

namespace Bifrost.Data
{
    public class MenuData : IDisposable
    {
        #region Constants

        public const string CNST_ROOT_MENUID = "0";
        public const string CNST_INVALID_MENUID = "";
        public const int CNST_ROOT_LEVEL = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// Key for form tab
        /// </summary>
        /// <value></value>
        public string MenuKey
        {
            get
            {
                return this.MenuID;
            }
        }
        public string MenuID; // key in db
        public int MenuSeq;
        public int MenuLevel; // start from 0 (roow), 
        public MenuType MenuType = MenuType.Item;
        public MenuFormType FormType = MenuFormType.WinForm;
        public string MenuLocation; // Url/Dll
        public string MenuClass; // Web:NULL/Win: Full class name: 
        public string MenuName;
        public string MenuPath = string.Empty;
        public bool MenuVisible = true; // 
        public string ParentMenuID = CNST_ROOT_MENUID;
        public MenuAccessData Permissions = new MenuAccessData();

        #endregion

        public MenuData(string menuId,
            int menuSeq,
            int menuLevel,
            MenuType menuType,
            MenuFormType menuFormType,
            string menuLocation,
            string menuClass,
            string menuName,
            bool menuVisible,
            string parentMenuId,
            int menuGroup)
        {
            this.MenuID = menuId;
            this.MenuSeq = menuSeq;
            this.MenuLevel = menuLevel;
            this.MenuType = menuType;
            this.FormType = menuFormType;
            this.MenuLocation = menuLocation;
            this.MenuClass = menuClass;
            this.MenuName = menuName;
            this.MenuVisible = menuVisible;
            this.ParentMenuID = parentMenuId;
        }

        public MenuData(string menuId,
            int menuSeq,
            int menuLevel,
            MenuType menuType,
            MenuFormType menuFormType,
            string menuLocation,
            string menuClass,
            string menuName,
            bool menuVisible,
            string parentMenuId,
            int menuGroup,
            int popWidth, int popHeight)
        {
            this.MenuID = menuId;
            this.MenuSeq = menuSeq;
            this.MenuLevel = menuLevel;
            this.MenuType = menuType;
            this.FormType = menuFormType;
            this.MenuLocation = menuLocation;
            this.MenuClass = menuClass;
            this.MenuName = menuName;
            this.MenuVisible = menuVisible;
            this.ParentMenuID = parentMenuId;
        }

        public MenuData()
        {
            Permissions = new MenuAccessData();
        }

        public static MenuData FromDataRow(DataRow menuDataRow)
        {
            MenuData mData = new MenuData();
            mData.MenuID = menuDataRow["CD_MENU"].ToString();                                    // SYS_CMENU.CD_MENU
            mData.MenuSeq = ConvertToInt32(menuDataRow["SEQ_MENU"]);                             // SYS_CMENU.SEQ_MENU
            mData.MenuLevel = ConvertToInt32(menuDataRow["LV_MENU"]);                            // SYS_CMENU.LV_MENU
            mData.MenuType = (MenuType)ConvertToInt32(menuDataRow["TP_MENU"]);                   // SYS_CMENU.TP_MENU            //MFM : 폴더, REG : 등록dll, REP : 조회dll
            mData.FormType = (MenuFormType)Convert.ToInt16(menuDataRow["TP_MENUFORM"]);          // SYS_CMENU.TP_MENUFORM        (0 : Winform / 1 : WebForm)
            mData.MenuLocation = Convert.ToString(menuDataRow["CD_MENU_LOCATION"]);              // SYS_CMENU.CD_MENU_LOCATION   (WebForm : Location : /Net2ERP/Environment/CM_Format/CM_FormatEntry01.aspx / WinForm : DLL : NET2WIN.Win.POP.CMN.dll)
            mData.MenuClass = Convert.ToString(menuDataRow["CD_MENU_CLASS"]);                    // SYS_CMENU.CD_MENU_CLASS      (WebForm : NULL / WinForm : Full class name: NET2WIN.Win.POP.CMN.POP_WorkEntry01)
            mData.MenuName = Convert.ToString(menuDataRow["NM_MENU"]);                           // SYS_CMENU.NM_MENU
            mData.MenuVisible = Convert.ToBoolean(menuDataRow["YN_VISIBLE"].ToString() == "Y" ? true : false);                    // SYS_CMENU.YN_VISIBLE
            mData.ParentMenuID = menuDataRow["CD_MENU_PARENT"].ToString();                       // SYS_CMENU.CD_MENU_PARENT

            try
            {
                mData.Permissions = new MenuAccessData(
                    ConvertToBoolean(menuDataRow["AUTH_VIEW"]),
                    ConvertToBoolean(menuDataRow["AUTH_SAVE"]),
                    ConvertToBoolean(menuDataRow["AUTH_INSERT"]),
                    ConvertToBoolean(menuDataRow["AUTH_UPDATE"]),
                    ConvertToBoolean(menuDataRow["AUTH_DELETE"]),
                    ConvertToBoolean(menuDataRow["AUTH_EXCEL"]),
                    ConvertToBoolean(menuDataRow["AUTH_PRINT"]));
            }
            catch
            {
                mData.Permissions = new MenuAccessData();
            }

            /// 
            /// Get the path
            /// 


            return mData;
        }

        private static bool ConvertToBoolean(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }
            else
            {
                if (value.GetType().Name == "Boolean")
                {
                    return Convert.ToBoolean(value);
                }
                else
                {
                    string sValue = value.ToString().Trim();
                    return sValue == "Y";
                }
            }
        }

        #region Serializable Implements

        //Deserialization constructor.
        public MenuData(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            this.MenuID = (string)info.GetValue("CD_MENU", typeof(string));
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("CD_MENU", this.MenuID);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region Utilities

        private static int ConvertToInt32(object value)
        {
            int intValue = 0;
            try
            {
                intValue = Convert.ToInt32(value);
            }
            catch { }
            return intValue;
        }

        #endregion
    }

    public enum MenuType
    {
        Group, Item, Popup
    }

    public enum MenuFormType
    {
        WinForm, WebForm
    }

}
