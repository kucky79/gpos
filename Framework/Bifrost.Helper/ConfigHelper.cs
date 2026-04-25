using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bifrost.Helper
{
    public class ConfigHelper
    {
        private const string _configFileName = @"SystemConfig.xml";

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
                string Path = AppDomain.CurrentDomain.BaseDirectory + _configFileName;
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
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public static SystemConfig SetConfig(SystemConfig systemConfig, string module)
        {
            string query = "INSERT INTO SYS_CONFIG  " + Environment.NewLine;
            query += "(CD_FIRM, CD_MODULE, CD_CTRL, NM_CTRL, DC_CTRL, TP_CTRL, DC_CONFIG, CD_CODE, DC_RMK)" + Environment.NewLine;
            query += "VALUES" + Environment.NewLine;
            query += "(N'" + Global.FirmCode + "', N'" + module + "', N'" + systemConfig.ConfigCode + "', N'" + systemConfig.ConfigName + "', N'" + systemConfig.ConfigValue + "', N'";
            query += systemConfig.ConfigType + "', N'" + systemConfig.ConfigDescript + "', N'" + systemConfig.RefCode + "', N'" + systemConfig.Remark + "')";

            DBHelper.ExecuteNonQuery(query);

            SaveConfigData();

            return GetConfig(systemConfig.ConfigCode);
        }

        private static void SaveConfigData()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + _configFileName;

                DataSet ds = DBHelper.GetDataSet("USP_SYS_CONFIG_XML_S", new object[] { Global.FirmCode });
                ds.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }

    public class SystemConfig
    {
        public string ConfigCode { get; set; } = string.Empty;
        public string ConfigName { get; set; } = string.Empty;
        public string ConfigValue { get; set; } = string.Empty;
        public string ConfigType { get; set; } = string.Empty;
        public string ConfigDescript { get; set; } = string.Empty;
        public string RefCode { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;

    }
}
