using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Bifrost.Win.Controls
{
    public class ConfigUtil
    {
        /// <summary>
        /// XML 파일 쓰기
        /// </summary>
        public static bool WriteXmlData(string xmlFilePath, string xmlFileName, DataSet tmpDataSet)
        {
            bool isOk = false;
            try
            {
                DirectoryInfo di = new DirectoryInfo(xmlFilePath);
                if (di.Exists == false)
                {
                    di.Create();
                }
                tmpDataSet.WriteXml(xmlFilePath + xmlFileName, XmlWriteMode.WriteSchema);

                isOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                isOk = false;
            }
            return isOk;
        }

        /// <summary>
        /// XML 파일 읽기
        /// </summary>
        public static SystemConfig GetConfig(string ConfigCode)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                string Path = AppDomain.CurrentDomain.BaseDirectory + @"AIMS_Config.xml";
                FileInfo xmlFile = new FileInfo(Path);

                if (!xmlFile.Exists)
                    return null;

                xmldoc.Load(Path);

                XmlNodeList xnList = xmldoc.GetElementsByTagName("Table"); //접근할 노드
                XmlNode node = xmldoc.ChildNodes[1].SelectSingleNode(string.Format("descendant::Table[ConfigCode='{0}']", ConfigCode));

                if (node == null)
                    return null;

                string ConfigName = node["ConfigName"] == null ? string.Empty : node["ConfigName"].InnerText;
                string ConfigValue = node["ConfigValue"] == null ? string.Empty : node["ConfigValue"].InnerText;
                string ConfigDescript = node["ConfigDescript"] == null ? string.Empty : node["ConfigDescript"].InnerText;
                string RefCode = node["RefCode"] == null ? string.Empty : node["RefCode"].InnerText;
                string Remark = node["Remark"] == null ? string.Empty : node["Remark"].InnerText;

                SystemConfig SystemConfig = new SystemConfig();
                SystemConfig.ConfigCode = ConfigCode;
                SystemConfig.ConfigName = ConfigName;
                SystemConfig.ConfigValue = ConfigValue;
                SystemConfig.ConfigDescript = ConfigDescript;
                SystemConfig.RefCode = RefCode;
                SystemConfig.Remark = Remark;

                return SystemConfig;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        //Save는 없다 오로지 Load만 있을뿐....
        //public static SystemConfig SetConfig(SystemConfig systemConfig, string module)
        //{
        //    string query = "INSERT INTO SYS_CONFIG  " + Environment.NewLine;
        //    query += "(CD_FIRM, CD_MODULE, CD_CTRL, NM_CTRL, DC_CTRL, TP_CTRL, DC_CONFIG, CD_CODE, DC_RMK)" + Environment.NewLine;
        //    query += "VALUES" + Environment.NewLine;
        //    query += "(N'" + Global.FirmCode + "', N'" + module + "', N'" + systemConfig.ConfigCode + "', N'" + systemConfig.ConfigName + "', N'" + systemConfig.ConfigValue + "', N'";
        //    query += systemConfig.ConfigType + "', N'" + systemConfig.ConfigDescript + "', N'" + systemConfig.RefCode + "', N'" + systemConfig.Remark + "')";

        //    DBHelper.ExecuteNonQuery(query);

        //    SaveConfigData();

        //    return GetConfig(systemConfig.ConfigCode);
        //}

        //private static void SaveConfigData()
        //{
        //    try
        //    {
        //        string path = AppDomain.CurrentDomain.BaseDirectory + "AIMS_Config.xml";

        //        DataSet ds = DBHelper.GetDataSet("AP_SYS_CONFIG_XML_S", new object[] { Global.FirmCode });
        //        ds.WriteXml(path, XmlWriteMode.WriteSchema);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

    }

    public class SystemConfig
    {
        private string _ConfigCode = string.Empty;
        public string ConfigCode
        {
            get { return _ConfigCode; }
            set { _ConfigCode = value; }
        }

        private string _ConfigName = string.Empty;
        public string ConfigName
        {
            get { return _ConfigName; }
            set { _ConfigName = value; }
        }

        private string _ConfigValue = string.Empty;
        public string ConfigValue
        {
            get { return _ConfigValue; }
            set { _ConfigValue = value; }
        }
        private string _ConfigDescript = string.Empty;
        public string ConfigDescript
        {
            get { return _ConfigDescript; }
            set { _ConfigDescript = value; }
        }
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        private string _Remark = string.Empty;
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        private string _ConfigType = string.Empty;
        public string ConfigType
        {
            get { return _ConfigType; }
            set { _ConfigType = value; }
        }

    }
}
