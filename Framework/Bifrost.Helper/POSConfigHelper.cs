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
    public class POSConfigHelper
    {
        private const string _configFileName = @"POSConfig.xml";

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
        public static POSConfig GetConfig(string ConfigCode)
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

                POSConfig POSConfig = new POSConfig
                {
                    ConfigCode = ConfigCode,
                    ConfigName = ConfigName,
                    ConfigValue = ConfigValue,
                    ConfigDescript = ConfigDescript,
                    RefCode = RefCode,
                    Remark = Remark
                };

                return POSConfig;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public static POSConfig SetConfig(POSConfig POSConfig, string module)
        {
            //string query = "INSERT INTO POS_CONFIG  " + Environment.NewLine;
            //query += "(CD_STORE, CD_MODULE, CD_CTRL, NM_CTRL, DC_CTRL, TP_CTRL, DC_CONFIG, CD_CODE, DC_RMK)" + Environment.NewLine;
            //query += "VALUES" + Environment.NewLine;
            //query += "(N'" + POSGlobal.StoreCode + "', N'" + module + "', N'" + POSConfig.ConfigCode + "', N'" + POSConfig.ConfigName + "', N'" + POSConfig.ConfigValue + "', N'";
            //query += POSConfig.ConfigType + "', N'" + POSConfig.ConfigDescript + "', N'" + POSConfig.RefCode + "', N'" + POSConfig.Remark + "')";

            DBHelper.ExecuteNonQuery("USP_POS_CONFIG_I", new object[] { POSGlobal.StoreCode, module, POSConfig.ConfigCode, POSConfig.ConfigName, POSConfig.ConfigValue, POSConfig.ConfigType, POSConfig.ConfigDescript, POSConfig.RefCode, DBNull.Value, POSConfig.Remark });

            SaveConfigData();

            return GetConfig(POSConfig.ConfigCode);
        }

        private static void SaveConfigData()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + _configFileName;

                DataSet ds = DBHelper.GetDataSet("USP_POS_CONFIG_XML_S", new object[] { POSGlobal.StoreCode });
                ds.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }

    public class POSConfig
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
