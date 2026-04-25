using System;
using System.Net;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web.Services.Protocols;

namespace Bifrost.Data
{
    public class POSGlobalData : IDisposable
    {

        public POSGlobalData()
        {
        }
        public POSGlobalData(DataRow gbDataRow)
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


        public DataTable Menus { get; set; }

        public DataTable MyMenus { get; set; }

		public MenuData StartupMenu	{ get; set; }

		public CookieContainer CookieContainer { get;  set; }

		public AuthHeader AuthHeader { get;  set; }

        [Description("Shared Data")]
        public Hashtable SharedData { get; set; }

        #region POSGlobalData 
        public string Language { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
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

        public string SaleDt { get; set; }

        #region IDisposable Members

        public void Dispose(){}

		#endregion
	}

}
