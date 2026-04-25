using System;
using System.Net;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web.Services.Protocols;

namespace Bifrost.Data
{
    public class GlobalData : IDisposable
    {

        //private string _FirmCode;                   // 회사코드
        //private string _FirmName;                   // 회사명
        //private string _BizCode;                    // 접속본부코드
        //private string _BizName;                    // 접속본부명
        //private string _UserHostIP;                 // 접속 아이피
        //private string _UserHostMAC;                // 접속 MAC

        //private string _EmpCode;	                  // 사원코드
        //private string _EmpName;	                  // 사원명
        //private string _UserID;		              // 접속ID
        //private string _UserName;	                  // 접속 사용자명
        //private string _PassWord;		              // 접속 암호
        //private string _Email = string.Empty;       // Email

        public GlobalData()
        {
        }
        public GlobalData(DataRow gbDataRow)
        {
            Language = Convert.ToString(gbDataRow["CD_LANG"]).Trim();
            FirmCode = Convert.ToString(gbDataRow["CD_FIRM"]).Trim();
            FirmName = Convert.ToString(gbDataRow["NM_FIRM"]).Trim();
            BizCode = Convert.ToString(gbDataRow["CD_BIZ"]).Trim();
            BizName = Convert.ToString(gbDataRow["NM_BIZ"]).Trim();
            UserHostIP = Convert.ToString(gbDataRow["DC_IP"]).Trim();
            UserHostMAC = Convert.ToString(gbDataRow["DC_MAC"]).Trim();

            EmpCode = Convert.ToString(gbDataRow["CD_EMP"]).Trim();
            EmpName = Convert.ToString(gbDataRow["NM_EMP"]).Trim();
            UserID = Convert.ToString(gbDataRow["CD_USER"]).Trim();
            UserName = Convert.ToString(gbDataRow["NM_USER"]).Trim();
            PassWord = Convert.ToString(gbDataRow["NO_PWD"]).Trim();
            Email = Convert.ToString(gbDataRow["DC_EMAIL"]).Trim();
        }


        public DataTable Menus { get; set; }

        public DataTable MyMenus { get; set; }

		public MenuData StartupMenu	{ get; set; }

		public CookieContainer CookieContainer { get;  set; }

		public AuthHeader AuthHeader { get;  set; }

        [Description("Shared Data")]
        public Hashtable SharedData { get; set; }

        #region GlobalData 
        public string Language { get; set; }
        public string FirmCode { get; set; }
        public string FirmName { get; set; }
        public string BizCode { get; set; }
        public string BizName { get; set; }
        public string UserHostIP { get; set; }
        public string UserHostMAC { get; set; }

        #endregion

        #region Login Info Properties
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }

        #endregion

        #region IDisposable Members

        public void Dispose(){}

		#endregion
	}

    #region AuthHeader

    public class AuthHeader : SoapHeader
    {
        public const string FORMSAUTHKEY = "BiforstFormsCookieName";

        public string Ticket { get; set; }

        public string LangID { get; set; }


        public AuthHeader()
        {
            this.Ticket = string.Empty;
            this.LangID = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        public AuthHeader(string ticket)
        {
            this.Ticket = ticket;
        }

        public AuthHeader(string ticket, string langID)
        {
            this.Ticket = ticket;
            this.LangID = langID;
        }
    }

    #endregion AuthHeader

    #region SecurityLevels

    public enum SecurityLevels
    {
        SuperAdmin = 0,
        ERPAdmin = 1,
        Master = 2,
        MANAGER = 3,
        USER = 4
    }

    #endregion
}
