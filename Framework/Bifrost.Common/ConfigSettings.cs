using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.IO;
using System.Xml;

namespace Bifrost.Common
{
    public class ConfigSettings
    {
        private static XmlDocument settingsDoc = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        private static string GetString(string settingKey)
        {
            if (settingsDoc.ChildNodes.Count != 2) return string.Empty;
            XmlNode node = settingsDoc.ChildNodes[1].SelectSingleNode(string.Format("descendant::{0}", settingKey));
            if (node != null)
            {
                return node.InnerText;
            }
            else
                return string.Empty;

        }

        /// <summary>
        /// GetStringSetting
        ///	Get the setting value in configuration associcated with the assembly contains this ConfigSettings class
        /// </summary>
        /// <param name="settingKey">Key to get value</param>
        /// <returns>value or string.Empty</returns>
        public static string GetStringSetting(string settingKey)
        {
            if (settingsDoc != null)
                return GetString(settingKey);

            Assembly ass = Assembly.GetExecutingAssembly();
            Stream stream = null;
            string resText = string.Empty;
            try
            {
                stream = ass.GetManifestResourceStream("Bifrost.Common.Settings.config");                
                settingsDoc = new XmlDocument();
                settingsDoc.Load(stream);
                resText = GetString(settingKey);
            }
            catch (Exception ex)
            {
                resText = ex.Message;
            }
            finally
            {
                if (stream != null) stream.Close();
            }

            return resText;
        }

        /// <summary>
        /// GetColorSetting
        ///		Get Color object from settings
        /// </summary>
        /// <param name="section"></param>
        /// <param name="settingKey">Key to get value</param>
        /// <returns></returns>
        public static System.Drawing.Color GetColorSetting(string settingKey)
        {
            string settingValue = GetStringSetting(settingKey);
            if (settingValue == string.Empty) return System.Drawing.Color.Empty;
            if (settingValue.Contains("#"))
                return System.Drawing.ColorTranslator.FromHtml(settingValue);
            else
            {
                string[] v = settingValue.Split(',');
                return System.Drawing.Color.FromArgb(Convert.ToInt32(v[0]), Convert.ToInt32(v[1]), Convert.ToInt32(v[2]));
            }
        }
        
        /// <summary>
        /// GetInt32Setting
        ///		Get int32 value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="settingKey">Key to get value</param>
        /// <returns></returns>
        public static System.Int32 GetInt32Setting(string settingKey)
        {
            string settingValue = GetStringSetting(settingKey);
            return settingValue == string.Empty ? 0 : System.Convert.ToInt32(settingValue);
        }

    }
}
