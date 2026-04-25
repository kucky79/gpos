using Bifrost;
using Bifrost.Helper;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost
{
    public class POSPrintHelper
    {
        public static void POSReportPrint(string ReportFile, string[] parameterNames, object[] parameterValues)
        {
            try
            {
                if (ReportFile != null)
                {
                    FormCollection fc = Application.OpenForms;
                    if (fc.Count > 0)
                    {
                        //LoadData.StartLoading(fc[fc.Count - 1].FindForm(), "View Report.", "Loading......");
                        XtraReport mainReport;

                        string DefaultPath = AppDomain.CurrentDomain.BaseDirectory;
                        mainReport = XtraReport.FromFile(DefaultPath + ReportFile + ".repx", true);
         
                        //레포트에 있는 ip를 뽑아서 Tmp에 저장. 로컬 세팅에 데이터가 없을경우 디폴트를 서버가 아닌 레포트에 있는 ip를 저장하기 위해서
                        GetReportIP(mainReport);

                        //레포트 접속정보변경
                        mainReport = ChangeReportsConnetion(mainReport);

                        if (parameterNames != null && parameterNames.Length > 0)
                        {
                            for (int j = 0; j < parameterNames.Length; j++)
                            {
                                mainReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                mainReport.Parameters[parameterNames[j]].Visible = false;
                            }
                            mainReport.RequestParameters = false;
                        }

                        mainReport.ExportOptions.PrintPreview.ActionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.Open;
                        mainReport.ExportOptions.PrintPreview.DefaultExportFormat = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
                        mainReport.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                        //mainReport.ExportOptions.PrintPreview.DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //mainReport.ExportOptions.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;
                        //mainReport.BeforePrint += MainReport_BeforePrint;
                        mainReport.CreateDocument();


                        mainReport.PrintingSystem.ContinuousPageNumbering = true;
                        mainReport.PrintingSystem.ShowMarginsWarning = false;

                        PrinterSettings settings = new PrinterSettings();
                        string defaultPrinterName = settings.PrinterName;

                        ReportPrintTool printTool = new ReportPrintTool(mainReport);
                        printTool.Print(defaultPrinterName);
                        //LoadData.EndLoading();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void POSReportPrintOld(string ReportFile, string[] parameterNames, object[] parameterValues)
        {
            try
            {
                if (ReportFile != null)
                {
                    FormCollection fc = Application.OpenForms;
                    if (fc.Count > 0)
                    {
                        //LoadData.StartLoading(fc[fc.Count - 1].FindForm(), "View Report.", "Loading......");
                        XtraReport mainReport;

                        string DefaultPath = AppDomain.CurrentDomain.BaseDirectory;
                        mainReport = XtraReport.FromFile(DefaultPath + ReportFile + ".repx", true);

                        //레포트에 있는 ip를 뽑아서 Tmp에 저장. 로컬 세팅에 데이터가 없을경우 디폴트를 서버가 아닌 레포트에 있는 ip를 저장하기 위해서
                        GetReportIP(mainReport);

                        //레포트 접속정보변경
                        mainReport = ChangeOldReportsConnetion(mainReport);

                        if (parameterNames != null && parameterNames.Length > 0)
                        {
                            for (int j = 0; j < parameterNames.Length; j++)
                            {
                                mainReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                mainReport.Parameters[parameterNames[j]].Visible = false;
                            }
                            mainReport.RequestParameters = false;
                        }

                        mainReport.ExportOptions.PrintPreview.ActionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.Open;
                        mainReport.ExportOptions.PrintPreview.DefaultExportFormat = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
                        mainReport.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                        //mainReport.ExportOptions.PrintPreview.DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //mainReport.ExportOptions.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;
                        //mainReport.BeforePrint += MainReport_BeforePrint;
                        mainReport.CreateDocument();


                        mainReport.PrintingSystem.ContinuousPageNumbering = true;
                        mainReport.PrintingSystem.ShowMarginsWarning = false;

                        PrinterSettings settings = new PrinterSettings();
                        string defaultPrinterName = settings.PrinterName;

                        ReportPrintTool printTool = new ReportPrintTool(mainReport);
                        printTool.Print(defaultPrinterName);
                        //LoadData.EndLoading();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        private static void MainReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            IEnumerable<XRSubreport> subReports = (sender as XtraReport).AllControls<XRSubreport>();
            foreach (var subReport in subReports)
            {
                subReport.BeforePrint += item_BeforePrint;
            }
        }

        private static void item_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MsSqlConnectionParameters msSqlConnectionParameters1 = new MsSqlConnectionParameters();
            SqlDataSource tmpDataSource = null;
            ArrayList ArrayParameter = new ArrayList();

            //레포트에 있는 SP스토리지 커넥션정보 일괄 변경
            for (int i = 0; i < (sender as XRSubreport).ReportSource.ComponentStorage.Count; i++)
            {
                tmpDataSource = (sender as XRSubreport).ReportSource.ComponentStorage[i] as SqlDataSource;

                msSqlConnectionParameters1.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                msSqlConnectionParameters1.DatabaseName = GetReportServerCatalog;
                msSqlConnectionParameters1.ServerName = GetReportServerIP;
                msSqlConnectionParameters1.UserName = GetReportServerId;
                msSqlConnectionParameters1.Password = GetReportServerPw;
                tmpDataSource.ConnectionName = GetReportServerIP + "_" + GetReportServerCatalog + "_Connection"; 
                tmpDataSource.ConnectionParameters = msSqlConnectionParameters1;
                tmpDataSource.Connection.ConnectionString = "Data Source=" + GetReportServerIP + ";Initial Catalog=" + GetReportServerCatalog + "; User Id=" + GetReportServerId + "; Password=" + GetReportServerPw;

                ArrayParameter.Add(tmpDataSource);
            }

           //레포트에 있는 SP스토리지 삭제
           (sender as XRSubreport).ReportSource.ComponentStorage.Clear();
            //레포트에 바뀐 sp스토리지 다시 추가
            for (int i = 0; i < ArrayParameter.Count; i++)
            {
                (sender as XRSubreport).ReportSource.ComponentStorage.Add(ArrayParameter[i] as IComponent);
            }
        }

        private static XtraReport ChangeReportsConnetion(XtraReport ReportFile)
        {
            SqlDataSource tmpDataSource = null;
            MsSqlConnectionParameters msSqlConnectionParameters1 = new MsSqlConnectionParameters();

            ArrayList ArrayParameter = new ArrayList();

            //레포트에 있는 SP스토리지 커넥션정보 일괄 변경
            for (int i = 0; i < ReportFile.ComponentStorage.Count; i++)
            {
                tmpDataSource = ReportFile.ComponentStorage[i] as SqlDataSource;
                msSqlConnectionParameters1.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                msSqlConnectionParameters1.DatabaseName = GetReportServerCatalog;
                msSqlConnectionParameters1.ServerName = GetReportServerIP;
                msSqlConnectionParameters1.UserName = GetReportServerId;
                msSqlConnectionParameters1.Password = GetReportServerPw;
                tmpDataSource.ConnectionName = GetReportServerIP + "_" + GetReportServerCatalog + "_Connection";
                tmpDataSource.ConnectionParameters = msSqlConnectionParameters1;

                tmpDataSource.Connection.ConnectionString = "Data Source=" + GetReportServerIP + ";Initial Catalog=" + GetReportServerCatalog + "; User Id=" + GetReportServerId + "; Password=" + GetReportServerPw + "; Connection Timeout = 60";
                ArrayParameter.Add(tmpDataSource);
            }

            //레포트에 있는 SP스토리지 삭제
            ReportFile.ComponentStorage.Clear();
            //레포트에 바뀐 sp스토리지 다시 추가
            for (int i = 0; i < ArrayParameter.Count; i++)
            {
                ReportFile.ComponentStorage.Add(ArrayParameter[i] as IComponent);
            }

            //Report에 있는 기본 DataSource 바꾸기
            SqlDataSource tmpReportDataSource = null;
            if (ReportFile.DataSource != null)
            {
                tmpReportDataSource = ReportFile.DataSource as SqlDataSource;
                MsSqlConnectionParameters tmpReportDataParamaters = new MsSqlConnectionParameters();
                tmpReportDataParamaters = tmpReportDataSource.ConnectionParameters as MsSqlConnectionParameters;

                tmpReportDataParamaters.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                tmpReportDataParamaters.DatabaseName = GetReportServerCatalog;
                tmpReportDataParamaters.ServerName = GetReportServerIP;
                tmpReportDataParamaters.UserName = GetReportServerId;
                tmpReportDataParamaters.Password = GetReportServerPw;
                tmpReportDataSource.ConnectionParameters = tmpReportDataParamaters;
                ReportFile.DataSource = tmpReportDataSource;
            }
            else
            {
                //없는 경우가 있어 이때는 디폴트 정보 넣어줌
                tmpReportDataSource = new SqlDataSource();
                MsSqlConnectionParameters tmpReportDataParamaters = new MsSqlConnectionParameters();

                tmpReportDataParamaters.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                tmpReportDataParamaters.DatabaseName = GetReportServerCatalog;
                tmpReportDataParamaters.ServerName = GetReportServerIP;
                tmpReportDataParamaters.UserName = GetReportServerId;
                tmpReportDataParamaters.Password = GetReportServerPw;
                tmpReportDataSource.ConnectionParameters = tmpReportDataParamaters;

                tmpReportDataSource.ConnectionName = GetReportServerIP + "_Connection";
                tmpReportDataSource.Connection.ConnectionString = "Data Source=" + GetReportServerIP + ";Initial Catalog=" + GetReportServerCatalog + "; User Id=" + GetReportServerId + "; Password=" + GetReportServerPw + "; Connection Timeout = 60";
                ReportFile.DataSource = tmpReportDataSource;
            }

            return ReportFile;
        }


        private static XtraReport ChangeOldReportsConnetion(XtraReport ReportFile)
        {
            SqlDataSource tmpDataSource = null;
            MsSqlConnectionParameters msSqlConnectionParameters1 = new MsSqlConnectionParameters();

            ArrayList ArrayParameter = new ArrayList();

            //레포트에 있는 SP스토리지 커넥션정보 일괄 변경
            for (int i = 0; i < ReportFile.ComponentStorage.Count; i++)
            {
                tmpDataSource = ReportFile.ComponentStorage[i] as SqlDataSource;
                msSqlConnectionParameters1.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                msSqlConnectionParameters1.DatabaseName = GetOldReportServerCatalog;
                msSqlConnectionParameters1.ServerName = GetOldReportServerIP;
                msSqlConnectionParameters1.UserName = GetOldReportServerId;
                msSqlConnectionParameters1.Password = GetOldReportServerPw;
                tmpDataSource.ConnectionName = GetOldReportServerIP + "_" + GetOldReportServerCatalog + "_Connection";
                tmpDataSource.ConnectionParameters = msSqlConnectionParameters1;

                tmpDataSource.Connection.ConnectionString = "Data Source=" + GetOldReportServerIP + ";Initial Catalog=" + GetOldReportServerCatalog + "; User Id=" + GetOldReportServerId + "; Password=" + GetOldReportServerPw + "; Connection Timeout = 60";
                ArrayParameter.Add(tmpDataSource);
            }

            //레포트에 있는 SP스토리지 삭제
            ReportFile.ComponentStorage.Clear();
            //레포트에 바뀐 sp스토리지 다시 추가
            for (int i = 0; i < ArrayParameter.Count; i++)
            {
                ReportFile.ComponentStorage.Add(ArrayParameter[i] as IComponent);
            }

            //Report에 있는 기본 DataSource 바꾸기
            SqlDataSource tmpReportDataSource = null;
            if (ReportFile.DataSource != null)
            {
                tmpReportDataSource = ReportFile.DataSource as SqlDataSource;
                MsSqlConnectionParameters tmpReportDataParamaters = new MsSqlConnectionParameters();
                tmpReportDataParamaters = tmpReportDataSource.ConnectionParameters as MsSqlConnectionParameters;

                tmpReportDataParamaters.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                tmpReportDataParamaters.DatabaseName = GetOldReportServerCatalog;
                tmpReportDataParamaters.ServerName = GetOldReportServerIP;
                tmpReportDataParamaters.UserName = GetOldReportServerId;
                tmpReportDataParamaters.Password = GetOldReportServerPw;
                tmpReportDataSource.ConnectionParameters = tmpReportDataParamaters;
                ReportFile.DataSource = tmpReportDataSource;
            }
            else
            {
                //없는 경우가 있어 이때는 디폴트 정보 넣어줌
                tmpReportDataSource = new SqlDataSource();
                MsSqlConnectionParameters tmpReportDataParamaters = new MsSqlConnectionParameters();

                tmpReportDataParamaters.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                tmpReportDataParamaters.DatabaseName = GetOldReportServerCatalog;
                tmpReportDataParamaters.ServerName = GetOldReportServerIP;
                tmpReportDataParamaters.UserName = GetOldReportServerId;
                tmpReportDataParamaters.Password = GetOldReportServerPw;
                tmpReportDataSource.ConnectionParameters = tmpReportDataParamaters;

                tmpReportDataSource.ConnectionName = GetOldReportServerIP + "_Connection";
                tmpReportDataSource.Connection.ConnectionString = "Data Source=" + GetOldReportServerIP + ";Initial Catalog=" + GetOldReportServerCatalog + "; User Id=" + GetOldReportServerId + "; Password=" + GetOldReportServerPw + "; Connection Timeout = 60";
                ReportFile.DataSource = tmpReportDataSource;
            }

            return ReportFile;
        }

        private static string TmpServerIP = string.Empty;

        private static void GetReportIP(XtraReport ReportFile)
        {
            SqlDataSource tmpDataSource = null;
            MsSqlConnectionParameters msSqlConnectionParameters1 = new MsSqlConnectionParameters();

            string ReportIP = string.Empty;

            for (int i = 0; i < ReportFile.ComponentStorage.Count; i++)
            {
                tmpDataSource = ReportFile.ComponentStorage[i] as SqlDataSource;

                msSqlConnectionParameters1 = tmpDataSource.ConnectionParameters as MsSqlConnectionParameters;
                ReportIP = msSqlConnectionParameters1.ServerName;
            }

            Console.Write(ReportIP);
            TmpServerIP = ReportIP;
        }

        //private static string SetReportPath
        //{
        //    get
        //    {
        //        string ReportPATH = string.Empty;

        //        IniFile inifile = new IniFile();
        //        string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
        //        inifile.IniWriteValue("DB", "OldConnectionString", "Data Source=localhost; initial Catalog=GPOS_OLD;uid=YOUR_USER;password=YOUR_PASSWORD;", SettingPath);

        //        return ReportPATH;
        //    }
        //}

        
        private static string GetReportServerIP
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "ConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[1];
            }
        }

        private static string GetReportServerCatalog
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "ConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[3];
            }
        }

        private static string GetReportServerId
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "ConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[5];
            }
        }

        private static string GetReportServerPw
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "ConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[7];
            }
        }


        private static string GetOldReportServerIP
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "OldConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[1];
            }
        }

        private static string GetOldReportServerCatalog
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "OldConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[3];
            }
        }

        private static string GetOldReportServerId
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "OldConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[5];
            }
        }

        private static string GetOldReportServerPw
        {
            get
            {
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                IniFile inifile = new IniFile();
                string ReportServerIP = inifile.IniReadValue("DB", "OldConnectionString", SettingPath);

                char[] delimiterChars = { ';', '=' };
                string[] words = ReportServerIP.Split(delimiterChars);

                //IP = words[1];
                //CATALOG = words[3];
                //DBID = words[5];
                //DBPassword = words[7];

                return words[7];
            }
        }
    }
}
