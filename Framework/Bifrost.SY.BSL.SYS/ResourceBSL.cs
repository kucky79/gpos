using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Bifrost.Framework;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Reflection;

namespace Bifrost.SY.BSL.SYS
{
    public class ResourceBSL : BSLBase
    {
        private SubSystemType subType = SubSystemType.SYS;

        public ResourceBSL()
        {
        }

        public string CreateResource(string MSBuildPath, string ResProjectPath, string outPath)
        {
            GenerateResourcePool(Path.GetDirectoryName(ResProjectPath));
            string ReturnValu = BuildProject(MSBuildPath, ResProjectPath, true, outPath, "Bifrost.Framework.Resource.dll");
            return ReturnValu;
        }

        private string BuildProject(string MSBuildPath, string projectFilePath, bool debugMode, string outPath, string outputFile)
        {
            string errorMessage = string.Empty;
            try
            {
                if (!File.Exists(projectFilePath))
                {
                    return string.Format("Not Exists projectFilePath : {0}", projectFilePath);
                }

                /// 
                /// Get output dir
                /// 
                string allText = File.ReadAllText(projectFilePath);

                string outputPath = string.Empty;

                // search for sth like :  <OutputPath>..</OutputPath>
                // 1234
                string startTag = "<OutputPath>";
                string endTag = "</OutputPath>";
                int p1 = allText.IndexOf(startTag);
                if (p1 != -1)
                {
                    int p2 = allText.IndexOf(endTag, p1);
                    if (p2 != -1)
                    {
                        outputPath = allText.Substring(p1 + startTag.Length, p2 - (p1 + startTag.Length + 1));
                    }

                    if (!debugMode)
                    {
                        p1 = allText.IndexOf(startTag, p1 + startTag.Length);
                        if (p1 != -1)
                        {
                            p2 = allText.IndexOf(endTag, p1);
                            if (p2 != -1)
                            {
                                outputPath = allText.Substring(p1 + startTag.Length, p2 - (p1 + startTag.Length + 1));
                            }
                        }
                    }
                }

                /// 
                /// Set current directory
                /// 
                Directory.SetCurrentDirectory(Path.GetDirectoryName(projectFilePath));

                // Build
                using (CommandShell cmdShell = new CommandShell())
                {
                    StringBuilder sbMsBuild = new StringBuilder();
                    sbMsBuild.AppendFormat("\"{0}\" \"{1}\" /t:ReBuild /p:Configuration={2}",
                        MSBuildPath, projectFilePath, debugMode ? "Debug" : "Release");

                    string logMsg = cmdShell.ExecuteCmd(sbMsBuild.ToString());
                    
                    errorMessage = ParseBuildError(logMsg);
                }

                /// 
                /// copy files to target path
                /// 
                if (outputFile != string.Empty)
                {                    
                    if (outputPath.Contains(".."))
                    {
                        if(File.Exists(Path.Combine(outPath, outputFile)))
                            File.Delete(Path.Combine(outPath, outputFile));

                        outputPath = Path.Combine(Path.GetDirectoryName(projectFilePath), outputPath);
                        File.Copy(Path.Combine(outputPath, outputFile), Path.Combine(outPath, outputFile), true);
                    }
                    else
                    {
                        if(File.Exists(Path.Combine(outPath, outputFile)))
                            File.Delete(Path.Combine(outPath, outputFile));

                        File.Copy(Path.Combine(outputPath, outputFile), Path.Combine(outPath, outputFile), true);
                    }
                }
            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }

            return errorMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullLog"></param>
        /// <returns></returns>
        private string ParseBuildError(string fullLog)
        {
            int p = fullLog.IndexOf("Build FAILED.");
            int next = fullLog.IndexOf("Time Elapsed");
            string error = string.Empty;
            if (p != -1 && next != -1)
            {
                p = p + "Build FAILED.".Length;
                error = fullLog.Substring(p, next - p);
            }
            return error;
        }
        /// <summary>
        /// Generate xml resource file from database
        /// </summary>
        /// <param name="outPath">리소스XML파일의 저장폴더(프로젝트폴더)</param>
        /// <returns></returns>
        private void GenerateResourcePool(string outPath)
        {
            /// 
            /// overwrite
            /// 
            SaveResourcesToFile("KO", "Resource", Path.Combine(outPath, "Resources_KO.xml"));
            SaveResourcesToFile("KO", "Message", Path.Combine(outPath, "Messages_KO.xml"));
            SaveResourcesToFile("EN", "Resource", Path.Combine(outPath, "Resources_EN.xml"));
            SaveResourcesToFile("EN", "Message", Path.Combine(outPath, "Messages_EN.xml"));
            SaveResourcesToFile("JP", "Resource", Path.Combine(outPath, "Resources_JP.xml"));
            SaveResourcesToFile("JP", "Message", Path.Combine(outPath, "Messages_JP.xml"));
            SaveResourcesToFile("CH", "Resource", Path.Combine(outPath, "Resources_CH.xml"));
            SaveResourcesToFile("CH", "Message", Path.Combine(outPath, "Messages_CH.xml"));
            //SaveSqlErrorsToFile(Path.Combine(outPath, "SqlErrors.xml"));
        }

        /// <summary>
        /// Save Resource To xml file
        /// </summary>
        /// <param name="cultureName"></param>
        private void SaveResourcesToFile(string cultureName, string typeText, string xmlFile)
        {
            TimeStamp ts = null;
            LoggingStart(ref ts);

            DataSet resData = null;

            try
            {
                if (File.Exists(xmlFile)) File.Delete(xmlFile);
                File.Create(xmlFile).Close();

                using (Bifrost.SY.DSL.SYS.ResourceDSL dslResource = new Bifrost.SY.DSL.SYS.ResourceDSL(subType))
                {
                    resData = dslResource.LoadResources(cultureName, typeText);
                }

                if (resData.Tables[0].Rows.Count == 0) return;

                XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.Unicode);

                writer.Formatting = Formatting.Indented;

                //Write the root element
                writer.WriteStartDocument();
                writer.WriteStartElement(typeText + "s");

                foreach (DataRow dr in resData.Tables[0].Rows)
                {
                    writer.WriteStartElement(typeText);
                    writer.WriteStartAttribute("id");
                    writer.WriteValue(typeText.Substring(0, 1) + Convert.ToString(dr[typeText + "ID"]));
                    writer.WriteEndAttribute();
                    writer.WriteValue(Convert.ToString(dr[typeText + "Text"]));
                    writer.WriteEndElement();
                }

                // end the root element
                writer.WriteEndElement();
                writer.WriteEndDocument();

                //Write the XML to file and close the writer
                writer.Close();

            }
            catch (Exception ex)
            {
                BifrostException.HandleBSLException(subType, ex, this.GetType(), MethodInfo.GetCurrentMethod().Name);
            }
            finally
            {
                LoggingEnd(ts, this, MethodInfo.GetCurrentMethod().Name);
            }
        }
    }

    public class CommandShell : IDisposable
    {
        private Process cmdProcess = null;
        //		private Form1 fom = null;

        public CommandShell()
        {
            //			fom = f;
        }
        #region Disposing
        /// <summary>
        ///     Dispose of this object's resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }

        /// <summary>
        ///		Free the instance variables of this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return; // 
            //			threadOutput.
        }

        #endregion

        public string ExecuteCmd(string sCommand)
        {
            ProcessStartInfo pinfo = new ProcessStartInfo("cmd");
            string strOutput = string.Empty;
            StreamReader sr;
            StreamReader err;
            StreamWriter sw;

            cmdProcess = new Process();

            cmdProcess.StartInfo = pinfo;

            pinfo.UseShellExecute = false;
            pinfo.CreateNoWindow = true;
            pinfo.RedirectStandardError = true;
            pinfo.RedirectStandardInput = true;
            pinfo.RedirectStandardOutput = true;

            cmdProcess.Start();

            sw = cmdProcess.StandardInput;
            sr = cmdProcess.StandardOutput;
            err = cmdProcess.StandardError;

            sw.AutoFlush = true;
            sw.WriteLine(sCommand);

            sw.Close();

            strOutput = sr.ReadToEnd();
            strOutput += err.ReadToEnd();

            cmdProcess.Close();

            return strOutput;
        }


        private void ReadError()
        {
            char[] buffer = new char[1024];
            int len = 0;
            string sError = string.Empty;

            while (!cmdProcess.HasExited)
            {
                len = cmdProcess.StandardError.Read(buffer, 0, 1024);

                if (len > 0)
                {
                    sError = new String(buffer, 0, len);
                }
            }
        }

        private void ReadOutput()
        {
            char[] buffer = new char[1024];
            int len = 0;
            string output;

            while (!cmdProcess.HasExited)
            {
                len = cmdProcess.StandardOutput.Read(buffer, 0, 1024);
                if (len > 0)
                {
                    output = new String(buffer, 0, len);
                }
            }
        }
    }
}
