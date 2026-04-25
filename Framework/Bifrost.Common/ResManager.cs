using System;
using System.Collections;
using System.Resources;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Reflection;

using System.Data;
using System.Data.SqlClient;
using Bifrost.Data;
using Bifrost;

namespace Bifrost.Common
{
	#region ResManager
	/// <summary>
	/// DBResourceManager manages resources for multiple cultures
	/// Resources are read from the database
	/// </summary>
	public  class ResManager
	{
        private string _Language = "KO";
        private string resourceType = "Resource";

        private static Hashtable resXmlDocs = new Hashtable();
		private static Hashtable msgXmlDocs = new Hashtable();

        public ResManager()
        {
            
        }
        public ResManager(string LanguageName)
        {
            Language = LanguageName;
        }
        public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }

        private string _Module = string.Empty;
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }

        /// <summary>
        /// get xml document on default culture
        /// </summary>
        /// <returns></returns>
        private XmlDocument GetXmlDoc(string resourceType)
		{
            
			object xml = null;
            if (resourceType == "Resource")
			{
                xml = resXmlDocs[Language];
				if (xml == null)
				{
                    xml = LoadXmlFile(resourceType);
                    resXmlDocs[Language] = xml;
				}
			}
			//else
			//{
   //             xml = msgXmlDocs[Language];
			//	if (xml == null)
			//	{
   //                 xml = LoadXmlFile(resourceType);
   //                 msgXmlDocs[Language] = xml;
			//	}
			//}

			return (XmlDocument)xml;
		}
        /// <summary>
        /// get xml document on choice culture
        /// </summary>
        /// <param name="LanguageName">choice Language: "KO" or "JP" or "CN" or "EN"</param>
        /// <returns></returns>
        private XmlDocument GetXmlDoc(string resourceType, string LanguageName)
        {
            object xml = null;
            if (resourceType == "Resource")
            {
                xml = resXmlDocs[LanguageName];
                if (xml == null)
                {
                    xml = LoadXmlFile(resourceType, LanguageName);
                    resXmlDocs[LanguageName] = xml;
                }
            }
            //else
            //{
            //    xml = msgXmlDocs[LanguageName];
            //    if (xml == null)
            //    {
            //        xml = LoadXmlFile(resourceType,LanguageName);
            //        msgXmlDocs[LanguageName] = xml;
            //    }
            //}

            return (XmlDocument)xml;
        }
        /// <summary>
        /// get xml document on choice culture
        /// </summary>
        /// <param name="LanguageName">choice Language: "KO" or "JP" or "CN" or "EN"</param>
        /// <returns></returns>
        private XmlDocument GetXmlDoc(string resourceType, string systemType, string LanguageName)
        {
            object xml = null;
            if (resourceType == "Resource")
            {
                xml = resXmlDocs[systemType+LanguageName];
                if (xml == null)
                {
                    xml = LoadXmlFile(resourceType, systemType, LanguageName);
                    resXmlDocs[systemType + LanguageName] = xml;
                }
            }
            //else
            //{
            //    xml = msgXmlDocs[LanguageName];
            //    if (xml == null)
            //    {
            //        xml = LoadXmlFile(resourceType,LanguageName);
            //        msgXmlDocs[LanguageName] = xml;
            //    }
            //}

            return (XmlDocument)xml;
        }

        /// <summary>
        /// Load xml file on default culture
        /// </summary>
        private XmlDocument LoadXmlFile(string resourceType)
		{
            
			Assembly ass ;//= this.GetType().Assembly;
            //Assembly thisAss = this.GetType().Assembly;

			Stream stream = null;
			XmlDocument xmlDoc = null;
			try
			{
                if (AppDomain.CurrentDomain.FriendlyName == "DefaultDomain")
                    ass = Assembly.Load("NF.Framework.Resource");
                else
                    ass = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + @"NF.Framework.Resource.dll");

                string resName = string.Concat(ass.GetName().Name, ".", resourceType, "s_", Language, ".xml");
				
                stream = ass.GetManifestResourceStream(resName);
				xmlDoc = new XmlDocument();
				xmlDoc.Load(stream);
			}
			catch
			{

			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return xmlDoc;
			
		}
        /// <summary>
        /// Load xml file on choice culture
        /// </summary>
        /// <param name="LanguageName"></param>
        /// <returns></returns>
        private XmlDocument LoadXmlFile(string resourceType, string LanguageName)
        {
            Assembly ass;// = this.GetType().Assembly;
            Stream stream = null;
            XmlDocument xmlDoc = null;
            try
            {
                ass = Assembly.Load("NF.Framework.Resource");
                //string resName = string.Concat(ass.GetName().Name, ".", resourceType, "s_", LanguageName, ".xml");
                string resName = string.Concat(resourceType, "s_", LanguageName, ".xml");
                //stream = ass.GetManifestResourceStream(resName);
                xmlDoc = new XmlDocument();
                xmlDoc.Load(resName);
            }
            catch
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return xmlDoc;

        }

        /// <summary>
        /// Load xml file on choice culture
        /// </summary>
        /// <param name="LanguageName"></param>
        /// <returns></returns>
        private XmlDocument LoadXmlFile(string resourceType, string systemType, string LanguageName)
        {
            Stream stream = null;
            XmlDocument xmlDoc = null;
            try
            {
                string resName = string.Concat(resourceType, "s_", systemType, "_", LanguageName, ".xml");
                string path = AppDomain.CurrentDomain.BaseDirectory + @"LanguageResource\";
                xmlDoc = new XmlDocument();
                xmlDoc.Load(path + resName);
            }
            catch
            {

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return xmlDoc;

        }

        //private string GetConnectionString()
        //{
        //    return Base.NFDecrypt(System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"]);
        //}

        #region Internal GetString

        /// <summary>
        /// Get resource text from db
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string DBInternalGetString(string resourceID)
        {
            try
            {
                string resourceName = DBHelper.ExecuteScalar("AP_SYS_DD_S", new object[] { Global.FirmCode, Language, resourceID }) as string;
                return resourceName;
            }
            catch
            {
                return resourceID;
            }
        }
		/// <summary>
        /// Get resource text from embedded file in assembly
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
        private string ResInternalGetString(string resourceID)
		{
            //string resourceType = resourceID.Substring(0, 1) == "R" ? "Resource" : "Message";
            string resText = string.Empty;
            try
            {
                resText = ResInternalGetString(GetXmlDoc(resourceType), resourceID);
            }
            catch { }
            finally
            {
            }

            return resText;
		}
        /// <summary>
        /// Return resource from embedded file in assembly
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        private string ResInternalGetString(string LanguageName, string resourceID)
        {
            //string resourceType = resourceID.Substring(0, 1) == "R" ? "Resource" : "Message";
            string resText = string.Empty;
            try
            {
                resText = ResInternalGetString(GetXmlDoc(resourceType,LanguageName), resourceID);
            }
            catch { }
            finally
            {
            }

            return resText;
        }

        /// <summary>
        /// Return resource from embedded file in assembly
        /// </summary>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        private string ResInternalGetString(string LanguageName, string systemType, string resourceID)
        {
            //string resourceType = resourceID.Substring(0, 1) == "R" ? "Resource" : "Message";
            string resText = string.Empty;
            try
            {
                Module = systemType;
                resText = ResInternalGetString(GetXmlDoc(resourceType, systemType, LanguageName), resourceID);
            }
            catch { }
            finally
            {
            }

            return resText;
        }

        /// <summary>
        /// Get resource text from file
        /// </summary>
        /// <param name="resourceFile"></param>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        private string ResInternalGetString2(string resourceFile, string resourceID)
		{
			StreamReader sr = null;
			XmlDocument xmlDoc = null;

			string resText = string.Empty;
			try
			{
				sr = new StreamReader(resourceFile);
				xmlDoc = new XmlDocument();
				xmlDoc.Load(sr);
				resText = ResInternalGetString(xmlDoc, resourceID);
			}
			catch { }
			finally
			{
				if (sr != null) sr.Close();
				if (xmlDoc != null) xmlDoc = null;
			}

			return resText;

		}
        /// <summary>
        /// Return resource's text in xml document
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        private string ResInternalGetString(XmlDocument xmlDoc, string resourceID)
        {
            //아예 리소르가 없을경우엔 무조건 넣어줌
            if (xmlDoc.ChildNodes.Count != 2)
            {
                DBHelper.ExecuteNonQuery("AP_SYS_DD_I", new object[] { Global.FirmCode, Module, resourceID, resourceID, resourceID, resourceID, resourceID, resourceID, Global.UserID });
                return resourceID;
            }
            XmlNode node = xmlDoc.ChildNodes[1].SelectSingleNode(string.Format("descendant::{0}[@id='{1}']", resourceType, resourceID));
            if (node != null)
            {
                return node.InnerText;
            }
            else
            {
                //@CD_FIRM
                //@CD_MODULE
                //@CD_DD
                //@NM_DD
                //@NM_KR
                //@NM_EN
                //@NM_CN
                //@NM_JP
                //@CD_USER_REG

                //리소스가 없을때는 db에도 인서트 해준다
                DBHelper.ExecuteNonQuery("AP_SYS_DD_I", new object[] { Global.FirmCode, Module, resourceID, resourceID, resourceID, resourceID, resourceID, resourceID, Global.UserID });
                return resourceID;
            }
        }

		#endregion Internal GetString

		#region Public

		/// <summary>
		/// Get resource string 
		/// </summary>
		/// <param name="subSystemType"></param>
		/// <param name="name"></param>
		/// <returns></returns>
        public string GetString(string resourceID)
		{
            if (AppConfigReader.Default.UseDBResource)
                return ResInternalGetString(resourceID);
            else
                return resourceID;
                //return DBInternalGetString(resourceID);				
        }
        /// <summary>
        /// Get resource string 
        /// </summary>
        /// <param name="LanguageName">choice Language: "KO" or "JP" or "CN" or "EN"</param>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public string GetString(string LanguageName, string resourceID)
		{
            this.Language = LanguageName;

			//if (!AppConfigReader.Default.UseDBResource)
                return ResInternalGetString(LanguageName, resourceID);
			//else
                //return DBInternalGetString(resourceID);				
		}

        /// <summary>
        /// Get resource string 
        /// </summary>
        /// <param name="LanguageName">choice Language: "KO" or "JP" or "CN" or "EN"</param>
        /// <param name="resourceID"></param>
        /// <returns></returns>
        public string GetString(string LanguageName, string systemType, string resourceID)
        {
            this.Language = LanguageName;
            this.Module = systemType;
            return ResInternalGetString(LanguageName, systemType, resourceID);
        }


        #endregion Public

        #region Resource Saving

        public DataSet LoadResources(string cultureName, string resourceText, string resourceType)
        {
            DataSet resourceDatatable = DBHelper.GetDataSet("AP_SYS_DD_S", new object[] { Global.FirmCode, cultureName, "" });
            return resourceDatatable;
        }

        private bool SaveResources(DataSet resourceData)
        {
            if (resourceData == null) return false;

            SpInfo si = new SpInfo();
            si.DataValue = resourceData;
            si.FirmCode = Global.FirmCode;
            si.UserID = Global.UserID;
            si.SpNameInsert = "AP_SYS_DD_I";
            si.SpNameUpdate = "AP_SYS_DD_U";
            si.SpNameDelete = "AP_SYS_DD_D";
            si.SpParamsInsert = new string[] { "CD_FIRM", "CD_MODULE", "CD_DD", "NM_DD", "NM_KR", "NM_EN", "NM_CN", "NM_JP", "CD_USER_REG" };
            si.SpParamsUpdate = new string[] { "CD_FIRM", "CD_MODULE", "CD_DD", "NM_DD", "NM_KR", "NM_EN", "NM_CN", "NM_JP", "CD_USER_AMD" };
            si.SpParamsDelete = new string[] { "CD_FIRM", "CD_MODULE", "CD_DD" };

            return DBHelper.Save(si);
        }

        //public string GenerateCode(string resourceType)
        //{
        //    SqlDataAdapter dsCommand = new SqlDataAdapter();

        //    SqlCommand loadCommand = new SqlCommand(resourceType == "Message" ? "NF.WP_SY_GenerateMessageCode" : "NF.WP_SY_GenerateResourceCode",
        //        new SqlConnection(GetConnectionString()));

        //    loadCommand.CommandType = CommandType.StoredProcedure;
        //    SqlParameterCollection sqlParms = loadCommand.Parameters;

        //    sqlParms.Add(new SqlParameter("@CultureName", SqlDbType.Char, 2));
        //    sqlParms[0].Value = Language == "" ? "KO" : Language;

        //    if (loadCommand.Connection.State == ConnectionState.Closed)
        //        loadCommand.Connection.Open();
        //    string newExCode = (string)loadCommand.ExecuteScalar();

        //    // disposing
        //    loadCommand.Dispose();

        //    return newExCode;
        //}

        #endregion

    }

	#endregion

	#region SqlErrorMapping

	public class SqlErrorMapping
	{
		/// <summary>
		/// Read the message id from xml file
		/// </summary>
		/// <param name="sqlErrorCode"></param>
		/// <returns></returns>
		private string ResGetMsgID(string sqlErrorCode)
		{
			Assembly ass = this.GetType().Assembly;
			Stream stream = null;
			XmlDocument xmlDoc = null;

			string resText = string.Empty;
			try
			{
				string resName = string.Concat(ass.GetName().Name, ".SqlErrors.xml");
				stream = ass.GetManifestResourceStream(resName);

				xmlDoc = new XmlDocument();
				xmlDoc.Load(stream);
				if (xmlDoc.ChildNodes.Count != 2) return string.Empty;

				XmlNode node = xmlDoc.ChildNodes[1].SelectSingleNode(string.Format("descendant::sqlerror[@sqlerrorcode='{0}']", sqlErrorCode));
				if (node != null)
					resText = node.Attributes["messageid"].Value;
			}
			catch { }
			finally
			{
				if (xmlDoc != null) xmlDoc = null;
				if (stream != null) stream.Close();
			}

			return resText;
		}

		/// <summary>
		/// Read messageid from db
		/// </summary>
		/// <param name="sqlErrorCode"></param>
		/// <returns></returns>
        //private string DBGetMsgID(string sqlErrorCode)
        //{
        //    string messageID = string.Empty;
        //    try
        //    {
        //        ICOWebService.COWebService coWebService = new Bifrost.Common.ICOWebService.COWebService();
        //        coWebService.Url = AppConfigReader.Default.NFCOWebService;
        //        coWebService.UseDefaultCredentials = true;
        //        DataSet ds = coWebService.ExecutePopupQuery("Framework", "sp_GetMessageIDBySqlError", new string[] { "@SqlError" }, new string[] { sqlErrorCode });
        //        if (ds.Tables[0].Rows.Count != 0)
        //        {
        //            messageID = ds.Tables[0].Rows[0]["MessageID"].ToString();
        //        }
        //    }
        //    catch
        //    {
        //        messageID = string.Empty;
        //    }

        //    return messageID;
        //}

		/// <summary>
		/// Get message id accordig to sqlErrrorCode
		/// </summary>
		/// <param name="sqlErrorCode"></param>
		/// <returns></returns>
		public string GetMessageID(string sqlErrorCode)
		{
			//if (!AppConfigReader.Default.UseDBResource)
			//{
				return ResGetMsgID(sqlErrorCode);
			//}
			//else
			//	return DBGetMsgID(sqlErrorCode);
		}
	}

	#endregion
}
