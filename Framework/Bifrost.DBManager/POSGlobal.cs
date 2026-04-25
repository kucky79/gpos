//using Bifrost.Data;
using Bifrost.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Web.Services.Protocols;

namespace Bifrost
{
    public class POSGlobal : IDisposable
    {
        public POSGlobal()
        {
        }
        public POSGlobal(DataRow gbDataRow)
        {
            Language = Convert.ToString(gbDataRow["CD_LANG"]).Trim();
            StoreCode = Convert.ToString(gbDataRow["CD_STORE"]).Trim();
            StoreName = Convert.ToString(gbDataRow["NM_STORE"]).Trim();
            UserHostIP = Convert.ToString(gbDataRow["DC_IP"]).Trim();
            UserHostMAC = Convert.ToString(gbDataRow["DC_MAC"]).Trim();

            EmpCode = Convert.ToString(gbDataRow["CD_EMP"]).Trim();
            EmpName = Convert.ToString(gbDataRow["NM_EMP"]).Trim();
            UserID = Convert.ToString(gbDataRow["CD_USER"]).Trim();
            UserName = Convert.ToString(gbDataRow["NM_USER"]).Trim();
            PassWord = Convert.ToString(gbDataRow["NO_PWD"]).Trim();
            Email = Convert.ToString(gbDataRow["DC_EMAIL"]).Trim();

            SaleDt = DateTime.Now.ToString("yyyy-MM-dd");
        }

        #region GlobalData Properties
        /// <summary>
        /// 사용언어  
        /// </summary>
        /// <remarks>사용언어</remarks>
        public static string Language { get; set; } = "KO";
        /// <summary>
        /// 회사코드
        /// </summary>        
        public static string StoreCode { get; set; }
        /// <summary>
        /// 회사명
        /// </summary>
        public static string StoreName { get; set; }
        /// <summary>
        /// 접속 아이피
        /// </summary>
        public static string UserHostIP { get; set; }
        /// <summary>
        /// 접속 MAC
        /// </summary>
        public static string UserHostMAC { get; set; }
        /// <summary>
        /// 사원코드
        /// </summary>
        public static string EmpCode { get; set; }
        /// <summary>
        /// 사원명
        /// </summary>
        public static string EmpName { get; set; }
        /// <summary>
        /// 접속ID
        /// </summary>
        public static string UserID { get; set; }
        /// <summary>
        /// 접속 사용자명
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        /// 접속 암호
        /// </summary>
        public static string PassWord { get; set; }
        /// <summary>
        /// 사용Email
        /// </summary>
        public static string Email { get; set; } = string.Empty;
        /// <summary>
        /// 소켓IP
        /// </summary>
        public static string SocketIP { get; set; }
        /// <summary>
        /// 소켓Port
        /// </summary>
        public static string SocketPort { get; set; }
        #endregion

        public static string SaleDt { get; set; }


        public DataTable Menus { get; set; }
        public DataTable MyMenus { get; set; }
        public MenuData StartupMenu { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public AuthHeader AuthHeader { get; set; }

        [Description("Shared Data")]
        public Hashtable SharedData { get; set; } = new Hashtable();

        #region IDisposable Members

        public void Dispose() { }

        #endregion
    }

}
