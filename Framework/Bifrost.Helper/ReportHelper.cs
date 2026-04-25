using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;
using DevExpress.XtraReports;

namespace Bifrost.Helper
{
    public class ReportHelper
    {
        public static XtraReport report;

        #region Report Preview
        public static void ReportView(string MenuID, string[] parameterNames, object[] parameterValues, DataTable parameterDt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DBHelper.GetDataTable("AP_SYS_REPORT_CONFIG_S", new object[] { Global.FirmCode, MenuID });

                DataRow[] Reports = dt.Select();

                DataRow ReturnData;
                string ReportFile = string.Empty;

                if (Reports.Length > 1)
                {
                    //RptSelect choiceRpt = new RptSelect(MenuID);
                    //if (choiceRpt.ShowDialog() == DialogResult.OK)
                    //{
                    //    ReturnData = choiceRpt.ReturnData;
                    //    ReportFile = ReturnData["CD_REPORT"].ToString();
                    //}

                    //choiceRpt.Dispose();
                }
                else
                {
                    ReportFile = Reports[0]["CD_REPORT"].ToString();//["CD_REPORT"].ToString();
                }


                if (ReportFile != string.Empty)
                {
                    FormCollection fc = Application.OpenForms;
                    if (fc.Count > 0)
                    {
                        //LoadData.StartLoading(fc[fc.Count - 1].FindForm(), "View Report.", "Loading......");
                        string Path = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\", "") + SetReportPath + ReportFile + ".repx";

                        report = XtraReport.FromFile(Path, true);

                        #region 파라메터추가
                        if (parameterNames != null && parameterNames.Length > 0)
                        {
                            for (int i = 0; i < parameterNames.Length; i++)
                            {
                                report.Parameters[parameterNames[i]].Value = parameterValues[i].ToString();
                                report.Parameters[parameterNames[i]].Visible = false;
                            }
                            //하위 내용은 파라메터를 추가하는거임. 이미 레포트에 파라메터가 추가되어있으므로 매칭만 시켜줘야함
                            //Parameter param1 = new Parameter();
                            //param1.Name = parameterNames[i].ToString();
                            //param1.Type = typeof(System.String);
                            //param1.Value = parameterValues[i].ToString();
                            //param1.Description = parameterNames[i].ToString();
                            //param1.Visible = true;
                            //report.Parameters.Add(param1);
                            report.RequestParameters = false;
                        }
                        #endregion

                        #region 데이터셋추가
                        if (parameterDt != null)
                        {
                            report.DataSource = parameterDt;
                        }
                        //데이터셋을 추가하면 레포트에서 테이블을 확인할수 없다
                        //if (parameterDs != null)
                        //{
                        //    if (parameterDs.Length > 0)
                        //    {
                        //        DataSet ds = new DataSet();
                        //        ds = DataTableAsDataSet(parameterDs);
                        //        report.DataSource = ds;
                        //        ds.Reset();
                        //    }
                        //}
                        #endregion

                        // Show the report's Print Preview.
                        report.ExportOptions.PrintPreview.ActionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.Open;
                        report.ExportOptions.PrintPreview.DefaultExportFormat = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
                        report.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                        //report.ExportOptions.PrintPreview.DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //report.ExportOptions.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;

                        ReportPrintTool printTool = new ReportPrintTool(report);
                        printTool.ShowPreview();//ShowPreviewDialog();// Dialog로 띄우면 Modal
                        //LoadData.EndLoading();
                    }
                }
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportView(string[] ReportFile, string[] parameterNames, object[] parameterValues)
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

                        string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                        string LocalPath = SetReportPath;
                        if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                        {
                            mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                        }
                        else
                        {
                            mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                        }
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
                        mainReport.BeforePrint += MainReport_BeforePrint;
                        mainReport.CreateDocument();

                        if (ReportFile.Length > 1)
                        {
                            for (int i = 1; i < ReportFile.Length; i++)
                            {
                                XtraReport tempReport;
                                tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                                //레포트 접속정보변경
                                tempReport = ChangeReportsConnetion(tempReport);

                                if (parameterNames != null && parameterNames.Length > 0)
                                {
                                    for (int j = 0; j < parameterNames.Length; j++)
                                    {
                                        tempReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                        tempReport.Parameters[parameterNames[j]].Visible = false;
                                    }
                                    tempReport.RequestParameters = false;
                                }
                                tempReport.BeforePrint += MainReport_BeforePrint;
                                tempReport.CreateDocument();
                                mainReport.Pages.AddRange(tempReport.Pages);
                            }
                        }

                        //mainReport.PrintingSystem.Watermark.Image = System.Drawing.Image.FromFile("D:/BG-logo2.png");
                        mainReport.PrintingSystem.ContinuousPageNumbering = true;
                        mainReport.PrintingSystem.ShowMarginsWarning = false;                        

                        ReportPrintTool printTool = new ReportPrintTool(mainReport);
                        printTool.ShowPreview();// ShowPreviewDialog();// Dialog로 띄우면 Modal
                        //LoadData.EndLoading();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new  Exception(Ex.ToString());
            }
        }

        public static void ReportView(string[] ReportFile, ArrayList paramList, ArrayList paramValueList)
        {
            try
            {
                if (ReportFile != null)
                {
                    FormCollection fc = Application.OpenForms;
                    if (fc.Count > 0)
                    {
                        //LoadData.StartLoading(fc[fc.Count-1].FindForm(), "View Report.", "Loading......");
                        XtraReport mainReport;

                        string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                        string LocalPath = SetReportPath;
                        if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                        {
                            mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                        }
                        else
                        {
                            mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                        }

                        //레포트 접속정보변경
                        mainReport = ChangeReportsConnetion(mainReport);

                        string[] parameterNames = (string[])paramList[0];
                        object[] parameterValues = (object[])paramValueList[0];
                        if (parameterNames != null && parameterNames.Length > 0)
                        {
                            for (int j = 0; j < parameterNames.Length; j++)
                            {
                                mainReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                mainReport.Parameters[parameterNames[j]].Visible = false;
                            }
                            mainReport.RequestParameters = false;
                        }

                        //IEnumerable<XRSubreport> subReports = mainReport.AllControls<XRSubreport>();
                        //foreach (var subReport in subReports)
                        //{

                        //    ((XRSubreport)subReport).ReportSourceUrl = System.IO.Path.Combine(mainReport.Parameters["Dir"].Value.ToString(), ((XRSubreport)subReport).ReportSourceUrl);


                        //    XtraReport tmpSubReport;

                        //    string im = subReport.ReportSourceUrl;
                        //    tmpSubReport = XtraReport.FromFile(im.Replace("~\\", ""), true);
                        //    ChangeReportsConnetion(tmpSubReport);
                        //    //XtraReport r = XtraReport.FromStream(new MemoryStream(myStorage.GetData(subReport.ReportSourceUrl)), true);
                        //    //subReport.ReportSourceUrl = string.Empty;
                        //    //subReport.ReportSource = r;
                        //}

                        mainReport.ExportOptions.PrintPreview.ActionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.Open;
                        mainReport.ExportOptions.PrintPreview.DefaultExportFormat = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
                        mainReport.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                        //mainReport.ExportOptions.PrintPreview.DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //mainReport.ExportOptions.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;
                        mainReport.BeforePrint += MainReport_BeforePrint;
                        mainReport.CreateDocument();

                        if (ReportFile.Length > 1)
                        {
                            for (int i = 1; i < ReportFile.Length; i++)
                            {
                                XtraReport tempReport;

                                if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                                {
                                    tempReport = XtraReport.FromFile(LocalPath + ReportFile[i] + ".repx", true);
                                }
                                else
                                {
                                    tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                                }

                                //레포트 접속정보변경
                                tempReport = ChangeReportsConnetion(tempReport);

                                string[] paramSubNames = (string[])paramList[i];
                                object[] paramSubValues = (object[])paramValueList[i];

                                if (paramSubNames != null && paramSubNames.Length > 0)
                                {
                                    for (int j = 0; j < paramSubNames.Length; j++)
                                    {
                                        tempReport.Parameters[paramSubNames[j]].Value = paramSubValues[j];
                                        tempReport.Parameters[paramSubNames[j]].Visible = false;
                                    }
                                    tempReport.RequestParameters = false;
                                }
                                tempReport.BeforePrint += MainReport_BeforePrint;
                                tempReport.CreateDocument();
                                mainReport.Pages.AddRange(tempReport.Pages);
                            }
                        }

                        GetReportIP(mainReport);
                        mainReport.PrintingSystem.ContinuousPageNumbering = true;
                        mainReport.PrintingSystem.ShowMarginsWarning = false;
                        
                        // Show the report's Print Preview.
                        ReportPrintTool printTool = new ReportPrintTool(mainReport);
                        //printTool.PrintingSystem.StartPrint += PrintingSystem_StartPrint;
                        printTool.ShowPreview();//ShowPreviewDialog(); // Dialog로 띄우면 Modal
                        //LoadData.EndLoading();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }
        
        public static void ReportView(string[] ReportFile, string[] parameterNames, object[] parameterValues, string tpExport)
        {
            try
            {
                if (ReportFile != null)
                {
                    XtraReport mainReport;

                    string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                    string LocalPath = SetReportPath;
                    if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                    {
                        mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                    }
                    else
                    {
                        mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                    }
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
                    mainReport.BeforePrint += MainReport_BeforePrint;
                    mainReport.CreateDocument();

                    if (ReportFile.Length > 1)
                    {
                        for (int i = 1; i < ReportFile.Length; i++)
                        {
                            XtraReport tempReport;
                            tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                            //레포트 접속정보변경
                            tempReport = ChangeReportsConnetion(tempReport);

                            if (parameterNames != null && parameterNames.Length > 0)
                            {
                                for (int j = 0; j < parameterNames.Length; j++)
                                {
                                    tempReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                    tempReport.Parameters[parameterNames[j]].Visible = false;
                                }
                                tempReport.RequestParameters = false;
                            }
                            tempReport.BeforePrint += MainReport_BeforePrint;
                            tempReport.CreateDocument();
                            mainReport.Pages.AddRange(tempReport.Pages);
                        }
                    }

                    if(tpExport == "P")
                    {
                        //EXPORT TO PDF
                        string reportPath = Application.StartupPath + "/TempDownload/Quotation.pdf";
                        mainReport.ExportToPdf(reportPath);
                    }else if(tpExport == "E")
                    {
                        //EXPORT TO EXCEL
                        string reportPath = Application.StartupPath + "/TempDownload/Quotation.xlsx";
                        mainReport.ExportToXlsx(reportPath);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportView(string[] ReportFile, string[] parameterNames, object[] parameterValues, string tpExport, string reportPath)
        {
            try
            {
                if (ReportFile != null)
                {
                    XtraReport mainReport;

                    string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                    string LocalPath = SetReportPath;
                    if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                    {
                        mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                    }
                    else
                    {
                        mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                    }
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
                    mainReport.BeforePrint += MainReport_BeforePrint;
                    mainReport.CreateDocument();

                    if (ReportFile.Length > 1)
                    {
                        for (int i = 1; i < ReportFile.Length; i++)
                        {
                            XtraReport tempReport;
                            tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                            //레포트 접속정보변경
                            tempReport = ChangeReportsConnetion(tempReport);

                            if (parameterNames != null && parameterNames.Length > 0)
                            {
                                for (int j = 0; j < parameterNames.Length; j++)
                                {
                                    tempReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                    tempReport.Parameters[parameterNames[j]].Visible = false;
                                }
                                tempReport.RequestParameters = false;
                            }
                            tempReport.BeforePrint += MainReport_BeforePrint;
                            tempReport.CreateDocument();
                            mainReport.Pages.AddRange(tempReport.Pages);
                        }
                    }

                    if (tpExport == "P")
                    {
                        //EXPORT TO PDF
                        mainReport.ExportToPdf(reportPath);
                    }
                    else if (tpExport == "E")
                    {
                        //EXPORT TO EXCEL
                        mainReport.ExportToXlsx(reportPath);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportView(string[] ReportFile, ArrayList paramList, ArrayList paramValueList, string tpExport)
        {
            try
            {
                if (ReportFile != null)
                {
                    XtraReport mainReport;

                    string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                    string LocalPath = SetReportPath;
                    if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                    {
                        mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                    }
                    else
                    {
                        mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                    }

                    //레포트 접속정보변경
                    mainReport = ChangeReportsConnetion(mainReport);

                    string[] parameterNames = (string[])paramList[0];
                    object[] parameterValues = (object[])paramValueList[0];
                    if (parameterNames != null && parameterNames.Length > 0)
                    {
                        for (int j = 0; j < parameterNames.Length; j++)
                        {
                            mainReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                            mainReport.Parameters[parameterNames[j]].Visible = false;
                        }
                        mainReport.RequestParameters = false;
                    }

                    //IEnumerable<XRSubreport> subReports = mainReport.AllControls<XRSubreport>();
                    //foreach (var subReport in subReports)
                    //{

                    //    ((XRSubreport)subReport).ReportSourceUrl = System.IO.Path.Combine(mainReport.Parameters["Dir"].Value.ToString(), ((XRSubreport)subReport).ReportSourceUrl);


                    //    XtraReport tmpSubReport;

                    //    string im = subReport.ReportSourceUrl;
                    //    tmpSubReport = XtraReport.FromFile(im.Replace("~\\", ""), true);
                    //    ChangeReportsConnetion(tmpSubReport);
                    //    //XtraReport r = XtraReport.FromStream(new MemoryStream(myStorage.GetData(subReport.ReportSourceUrl)), true);
                    //    //subReport.ReportSourceUrl = string.Empty;
                    //    //subReport.ReportSource = r;
                    //}


                    mainReport.BeforePrint += MainReport_BeforePrint;
                    mainReport.CreateDocument();


                    if (ReportFile.Length > 1)
                    {
                        for (int i = 1; i < ReportFile.Length; i++)
                        {
                            XtraReport tempReport;


                            if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                            {
                                tempReport = XtraReport.FromFile(LocalPath + ReportFile[i] + ".repx", true);
                            }
                            else
                            {
                                tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                            }

                            //레포트 접속정보변경
                            tempReport = ChangeReportsConnetion(tempReport);

                            string[] paramSubNames = (string[])paramList[i];
                            object[] paramSubValues = (object[])paramValueList[i];

                            if (paramSubNames != null && paramSubNames.Length > 0)
                            {
                                for (int j = 0; j < paramSubNames.Length; j++)
                                {
                                    tempReport.Parameters[paramSubNames[j]].Value = paramSubValues[j];
                                    tempReport.Parameters[paramSubNames[j]].Visible = false;
                                }
                                tempReport.RequestParameters = false;
                            }
                            tempReport.BeforePrint += MainReport_BeforePrint;
                            tempReport.CreateDocument();
                            mainReport.Pages.AddRange(tempReport.Pages);
                        }
                    }

                    GetReportIP(mainReport);
                    mainReport.PrintingSystem.ContinuousPageNumbering = true;
                    mainReport.PrintingSystem.ShowMarginsWarning = false;

                    if (tpExport == "P")
                    {
                        //EXPORT TO PDF
                        string reportPath = Application.StartupPath + "/TempDownload/Quotation.pdf";
                        mainReport.ExportToPdf(reportPath);
                    }
                    else if (tpExport == "E")
                    {
                        //EXPORT TO EXCEL
                        string reportPath = Application.StartupPath + "/TempDownload/Quotation.xlsx";
                        mainReport.ExportToXlsx(reportPath);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportView(string[] ReportFile, ArrayList paramList, ArrayList paramValueList, string tpExport, string reportPath)
        {
            try
            {
                if (ReportFile != null)
                {
                    XtraReport mainReport;

                    string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                    string LocalPath = SetReportPath;
                    if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                    {
                        mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                    }
                    else
                    {
                        mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                    }

                    //레포트 접속정보변경
                    mainReport = ChangeReportsConnetion(mainReport);

                    string[] parameterNames = (string[])paramList[0];
                    object[] parameterValues = (object[])paramValueList[0];
                    if (parameterNames != null && parameterNames.Length > 0)
                    {
                        for (int j = 0; j < parameterNames.Length; j++)
                        {
                            mainReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                            mainReport.Parameters[parameterNames[j]].Visible = false;
                        }
                        mainReport.RequestParameters = false;
                    }

                    //IEnumerable<XRSubreport> subReports = mainReport.AllControls<XRSubreport>();
                    //foreach (var subReport in subReports)
                    //{

                    //    ((XRSubreport)subReport).ReportSourceUrl = System.IO.Path.Combine(mainReport.Parameters["Dir"].Value.ToString(), ((XRSubreport)subReport).ReportSourceUrl);


                    //    XtraReport tmpSubReport;

                    //    string im = subReport.ReportSourceUrl;
                    //    tmpSubReport = XtraReport.FromFile(im.Replace("~\\", ""), true);
                    //    ChangeReportsConnetion(tmpSubReport);
                    //    //XtraReport r = XtraReport.FromStream(new MemoryStream(myStorage.GetData(subReport.ReportSourceUrl)), true);
                    //    //subReport.ReportSourceUrl = string.Empty;
                    //    //subReport.ReportSource = r;
                    //}


                    mainReport.BeforePrint += MainReport_BeforePrint;
                    mainReport.CreateDocument();


                    if (ReportFile.Length > 1)
                    {
                        for (int i = 1; i < ReportFile.Length; i++)
                        {
                            XtraReport tempReport;


                            if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                            {
                                tempReport = XtraReport.FromFile(LocalPath + ReportFile[i] + ".repx", true);
                            }
                            else
                            {
                                tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                            }

                            //레포트 접속정보변경
                            tempReport = ChangeReportsConnetion(tempReport);

                            string[] paramSubNames = (string[])paramList[i];
                            object[] paramSubValues = (object[])paramValueList[i];

                            if (paramSubNames != null && paramSubNames.Length > 0)
                            {
                                for (int j = 0; j < paramSubNames.Length; j++)
                                {
                                    tempReport.Parameters[paramSubNames[j]].Value = paramSubValues[j];
                                    tempReport.Parameters[paramSubNames[j]].Visible = false;
                                }
                                tempReport.RequestParameters = false;
                            }
                            tempReport.BeforePrint += MainReport_BeforePrint;
                            tempReport.CreateDocument();
                            mainReport.Pages.AddRange(tempReport.Pages);
                        }
                    }

                    GetReportIP(mainReport);
                    mainReport.PrintingSystem.ContinuousPageNumbering = true;
                    mainReport.PrintingSystem.ShowMarginsWarning = false;

                    if (tpExport == "P")
                    {
                        //EXPORT TO PDF
                        mainReport.ExportToPdf(reportPath);
                    }
                    else if (tpExport == "E")
                    {
                        //EXPORT TO EXCEL
                        mainReport.ExportToXlsx(reportPath);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportView(string[] ReportFile, string[] parameterNames, object[] parameterValues, DocumentViewer Dv)
        {
            try
            {
                if (ReportFile == null || Application.OpenForms.Count <= 0)
                    return;
                string str = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + "Reports\\";
                string setReportPath = ReportHelper.SetReportPath;
                XtraReport ReportFile1 = !(str != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + setReportPath) ? XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + "Reports\\" + ReportFile[0] + ".repx", true) : XtraReport.FromFile(setReportPath + ReportFile[0] + ".repx", true);
                ReportHelper.GetReportIP(ReportFile1);
                XtraReport xtraReport1 = ReportHelper.ChangeReportsConnetion(ReportFile1);
                if (parameterNames != null && (uint)parameterNames.Length > 0U)
                {
                    for (int index = 0; index < parameterNames.Length; ++index)
                    {
                        xtraReport1.Parameters[parameterNames[index]].Value = parameterValues[index];
                        xtraReport1.Parameters[parameterNames[index]].Visible = false;
                    }
                    xtraReport1.RequestParameters = false;
                }
                xtraReport1.ExportOptions.PrintPreview.ActionAfterExport = ActionAfterExport.Open;
                xtraReport1.ExportOptions.PrintPreview.DefaultExportFormat = PrintingSystemCommand.ExportPdf;
                xtraReport1.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                xtraReport1.BeforePrint += new PrintEventHandler(ReportHelper.MainReport_BeforePrint);
                xtraReport1.CreateDocument();
                if (ReportFile.Length > 1)
                {
                    for (int index1 = 1; index1 < ReportFile.Length; ++index1)
                    {
                        XtraReport xtraReport2 = ReportHelper.ChangeReportsConnetion(XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + "Reports\\" + ReportFile[index1] + ".repx", true));
                        if (parameterNames != null && (uint)parameterNames.Length > 0U)
                        {
                            for (int index2 = 0; index2 < parameterNames.Length; ++index2)
                            {
                                xtraReport2.Parameters[parameterNames[index2]].Value = parameterValues[index2];
                                xtraReport2.Parameters[parameterNames[index2]].Visible = false;
                            }
                            xtraReport2.RequestParameters = false;
                        }
                        xtraReport2.BeforePrint += new PrintEventHandler(ReportHelper.MainReport_BeforePrint);
                        xtraReport2.CreateDocument();
                        xtraReport1.Pages.AddRange((IEnumerable)xtraReport2.Pages);
                    }
                }
                xtraReport1.PrintingSystem.ContinuousPageNumbering = true;
                xtraReport1.PrintingSystem.ShowMarginsWarning = false;
                ReportPrintTool reportPrintTool = new ReportPrintTool((IReport)xtraReport1);
                Dv.DocumentSource = (object)xtraReport1;
                xtraReport1.CreateDocument();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #endregion Report Preview

        #region Report Direct
        public static void ReportPrint(string MenuID, string[] parameterNames, object[] parameterValues, DataTable parameterDt)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DBHelper.GetDataTable("AP_SYS_REPORT_CONFIG_S", new object[] { Global.FirmCode, MenuID });

                DataRow[] Reports = dt.Select();

                DataRow ReturnData;
                string ReportFile = string.Empty;

                if (Reports.Length > 1)
                {
                    //RptSelect choiceRpt = new RptSelect(MenuID);
                    //if (choiceRpt.ShowDialog() == DialogResult.OK)
                    //{
                    //    ReturnData = choiceRpt.ReturnData;
                    //    ReportFile = ReturnData["CD_REPORT"].ToString();
                    //}

                    //choiceRpt.Dispose();
                }
                else
                {
                    ReportFile = Reports[0]["CD_REPORT"].ToString();//["CD_REPORT"].ToString();
                }


                if (ReportFile != string.Empty)
                {
                    FormCollection fc = Application.OpenForms;
                    if (fc.Count > 0)
                    {
                        //LoadData.StartLoading(fc[fc.Count - 1].FindForm(), "View Report.", "Loading......");
                        string Path = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\", "") + SetReportPath + ReportFile + ".repx";

                        report = XtraReport.FromFile(Path, true);

                        #region 파라메터추가
                        if (parameterNames != null && parameterNames.Length > 0)
                        {
                            for (int i = 0; i < parameterNames.Length; i++)
                            {
                                report.Parameters[parameterNames[i]].Value = parameterValues[i].ToString();
                                report.Parameters[parameterNames[i]].Visible = false;
                            }
                            //하위 내용은 파라메터를 추가하는거임. 이미 레포트에 파라메터가 추가되어있으므로 매칭만 시켜줘야함
                            //Parameter param1 = new Parameter();
                            //param1.Name = parameterNames[i].ToString();
                            //param1.Type = typeof(System.String);
                            //param1.Value = parameterValues[i].ToString();
                            //param1.Description = parameterNames[i].ToString();
                            //param1.Visible = true;
                            //report.Parameters.Add(param1);
                            report.RequestParameters = false;
                        }
                        #endregion

                        #region 데이터셋추가
                        if (parameterDt != null)
                        {
                            report.DataSource = parameterDt;
                        }
                        //데이터셋을 추가하면 레포트에서 테이블을 확인할수 없다
                        //if (parameterDs != null)
                        //{
                        //    if (parameterDs.Length > 0)
                        //    {
                        //        DataSet ds = new DataSet();
                        //        ds = DataTableAsDataSet(parameterDs);
                        //        report.DataSource = ds;
                        //        ds.Reset();
                        //    }
                        //}
                        #endregion

                        // Show the report's Print Preview.
                        report.ExportOptions.PrintPreview.ActionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.Open;
                        report.ExportOptions.PrintPreview.DefaultExportFormat = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
                        report.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                        //report.ExportOptions.PrintPreview.DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //report.ExportOptions.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;

                        ReportPrintTool printTool = new ReportPrintTool(report);
                        printTool.Print();
                        //LoadData.EndLoading();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportPrint(string[] ReportFile, string[] parameterNames, object[] parameterValues)
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

                        string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                        string LocalPath = SetReportPath;
                        if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                        {
                            mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                        }
                        else
                        {
                            mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                        }
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
                        mainReport.BeforePrint += MainReport_BeforePrint;
                        mainReport.CreateDocument();

                        if (ReportFile.Length > 1)
                        {
                            for (int i = 1; i < ReportFile.Length; i++)
                            {
                                XtraReport tempReport;
                                tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                                //레포트 접속정보변경
                                tempReport = ChangeReportsConnetion(tempReport);

                                if (parameterNames != null && parameterNames.Length > 0)
                                {
                                    for (int j = 0; j < parameterNames.Length; j++)
                                    {
                                        tempReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                        tempReport.Parameters[parameterNames[j]].Visible = false;
                                    }
                                    tempReport.RequestParameters = false;
                                }
                                tempReport.BeforePrint += MainReport_BeforePrint;
                                tempReport.CreateDocument();
                                mainReport.Pages.AddRange(tempReport.Pages);
                            }
                        }

                        mainReport.PrintingSystem.ContinuousPageNumbering = true;
                        mainReport.PrintingSystem.ShowMarginsWarning = false;

                        ReportPrintTool printTool = new ReportPrintTool(mainReport);
                        printTool.Print();
                        //LoadData.EndLoading();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportPrint(string[] ReportFile, ArrayList paramList, ArrayList paramValueList)
        {
            try
            {
                if (ReportFile != null)
                {
                    FormCollection fc = Application.OpenForms;
                    if (fc.Count > 0)
                    {
                        //LoadData.StartLoading(fc[fc.Count-1].FindForm(), "View Report.", "Loading......");
                        XtraReport mainReport;

                        string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                        string LocalPath = SetReportPath;
                        if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                        {
                            mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                        }
                        else
                        {
                            mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                        }

                        //레포트 접속정보변경
                        mainReport = ChangeReportsConnetion(mainReport);

                        string[] parameterNames = (string[])paramList[0];
                        object[] parameterValues = (object[])paramValueList[0];
                        if (parameterNames != null && parameterNames.Length > 0)
                        {
                            for (int j = 0; j < parameterNames.Length; j++)
                            {
                                mainReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                mainReport.Parameters[parameterNames[j]].Visible = false;
                            }
                            mainReport.RequestParameters = false;
                        }

                        //IEnumerable<XRSubreport> subReports = mainReport.AllControls<XRSubreport>();
                        //foreach (var subReport in subReports)
                        //{

                        //    ((XRSubreport)subReport).ReportSourceUrl = System.IO.Path.Combine(mainReport.Parameters["Dir"].Value.ToString(), ((XRSubreport)subReport).ReportSourceUrl);


                        //    XtraReport tmpSubReport;

                        //    string im = subReport.ReportSourceUrl;
                        //    tmpSubReport = XtraReport.FromFile(im.Replace("~\\", ""), true);
                        //    ChangeReportsConnetion(tmpSubReport);
                        //    //XtraReport r = XtraReport.FromStream(new MemoryStream(myStorage.GetData(subReport.ReportSourceUrl)), true);
                        //    //subReport.ReportSourceUrl = string.Empty;
                        //    //subReport.ReportSource = r;
                        //}

                        mainReport.ExportOptions.PrintPreview.ActionAfterExport = DevExpress.XtraPrinting.ActionAfterExport.Open;
                        mainReport.ExportOptions.PrintPreview.DefaultExportFormat = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
                        mainReport.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                        //mainReport.ExportOptions.PrintPreview.DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        //mainReport.ExportOptions.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;
                        mainReport.BeforePrint += MainReport_BeforePrint;
                        mainReport.CreateDocument();

                        if (ReportFile.Length > 1)
                        {
                            for (int i = 1; i < ReportFile.Length; i++)
                            {
                                XtraReport tempReport;

                                if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                                {
                                    tempReport = XtraReport.FromFile(LocalPath + ReportFile[i] + ".repx", true);
                                }
                                else
                                {
                                    tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                                }

                                //레포트 접속정보변경
                                tempReport = ChangeReportsConnetion(tempReport);

                                string[] paramSubNames = (string[])paramList[i];
                                object[] paramSubValues = (object[])paramValueList[i];

                                if (paramSubNames != null && paramSubNames.Length > 0)
                                {
                                    for (int j = 0; j < paramSubNames.Length; j++)
                                    {
                                        tempReport.Parameters[paramSubNames[j]].Value = paramSubValues[j];
                                        tempReport.Parameters[paramSubNames[j]].Visible = false;
                                    }
                                    tempReport.RequestParameters = false;
                                }
                                tempReport.BeforePrint += MainReport_BeforePrint;
                                tempReport.CreateDocument();
                                mainReport.Pages.AddRange(tempReport.Pages);
                            }
                        }

                        GetReportIP(mainReport);
                        mainReport.PrintingSystem.ContinuousPageNumbering = true;
                        mainReport.PrintingSystem.ShowMarginsWarning = false;

                        // Show the report's Print Preview.
                        ReportPrintTool printTool = new ReportPrintTool(mainReport);
                        //printTool.PrintingSystem.StartPrint += PrintingSystem_StartPrint;
                        printTool.Print();
                        //LoadData.EndLoading();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        public static void ReportPrint(string[] ReportFile, string[] parameterNames, object[] parameterValues, string tpExport)
        {
            try
            {
                if (ReportFile != null)
                {
                    XtraReport mainReport;

                    string DefaultPath = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\";
                    string LocalPath = SetReportPath;
                    if (DefaultPath != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + LocalPath)
                    {
                        mainReport = XtraReport.FromFile(LocalPath + ReportFile[0] + ".repx", true);
                    }
                    else
                    {
                        mainReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[0] + ".repx", true);
                    }
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
                    mainReport.BeforePrint += MainReport_BeforePrint;
                    mainReport.CreateDocument();

                    if (ReportFile.Length > 1)
                    {
                        for (int i = 1; i < ReportFile.Length; i++)
                        {
                            XtraReport tempReport;
                            tempReport = XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + @"Reports\" + ReportFile[i] + ".repx", true);
                            //레포트 접속정보변경
                            tempReport = ChangeReportsConnetion(tempReport);

                            if (parameterNames != null && parameterNames.Length > 0)
                            {
                                for (int j = 0; j < parameterNames.Length; j++)
                                {
                                    tempReport.Parameters[parameterNames[j]].Value = parameterValues[j];
                                    tempReport.Parameters[parameterNames[j]].Visible = false;
                                }
                                tempReport.RequestParameters = false;
                            }
                            tempReport.BeforePrint += MainReport_BeforePrint;
                            tempReport.CreateDocument();
                            mainReport.Pages.AddRange(tempReport.Pages);
                        }
                    }

                    if (tpExport == "P")
                    {
                        //EXPORT TO PDF
                        string reportPath = Application.StartupPath + "/TempDownload/Quotation.pdf";
                        mainReport.ExportToPdf(reportPath);
                    }
                    else if (tpExport == "E")
                    {
                        //EXPORT TO EXCEL
                        string reportPath = Application.StartupPath + "/TempDownload/Quotation.xlsx";
                        mainReport.ExportToXlsx(reportPath);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
        }

        
        public static void ReportPrint(string[] ReportFile, string[] parameterNames, object[] parameterValues, DocumentViewer Dv)
        {
            try
            {
                if (ReportFile == null || Application.OpenForms.Count <= 0)
                    return;
                string str = AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + "Reports\\";
                string setReportPath = ReportHelper.SetReportPath;
                XtraReport ReportFile1 = !(str != AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + setReportPath) 
                        ? XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + "Reports\\" + ReportFile[0] + ".repx", true) 
                        : XtraReport.FromFile(setReportPath + ReportFile[0] + ".repx", true);
                ReportHelper.GetReportIP(ReportFile1);
                XtraReport xtraReport1 = ReportHelper.ChangeReportsConnetion(ReportFile1);
                if (parameterNames != null && (uint)parameterNames.Length > 0U)
                {
                    for (int index = 0; index < parameterNames.Length; ++index)
                    {
                        xtraReport1.Parameters[parameterNames[index]].Value = parameterValues[index];
                        xtraReport1.Parameters[parameterNames[index]].Visible = false;
                    }
                    xtraReport1.RequestParameters = false;
                }
                xtraReport1.ExportOptions.PrintPreview.ActionAfterExport = ActionAfterExport.Open;
                xtraReport1.ExportOptions.PrintPreview.DefaultExportFormat = PrintingSystemCommand.ExportPdf;
                xtraReport1.ExportOptions.PrintPreview.ShowOptionsBeforeExport = false;
                xtraReport1.BeforePrint += new PrintEventHandler(ReportHelper.MainReport_BeforePrint);
                xtraReport1.CreateDocument();
                if (ReportFile.Length > 1)
                {
                    for (int index1 = 1; index1 < ReportFile.Length; ++index1)
                    {
                        XtraReport xtraReport2 = ReportHelper.ChangeReportsConnetion(XtraReport.FromFile(AppDomain.CurrentDomain.BaseDirectory.Replace("Dev_Deploy\\Win\\", "") + "Reports\\" + ReportFile[index1] + ".repx", true));
                        if (parameterNames != null && (uint)parameterNames.Length > 0U)
                        {
                            for (int index2 = 0; index2 < parameterNames.Length; ++index2)
                            {
                                xtraReport2.Parameters[parameterNames[index2]].Value = parameterValues[index2];
                                xtraReport2.Parameters[parameterNames[index2]].Visible = false;
                            }
                            xtraReport2.RequestParameters = false;
                        }
                        xtraReport2.BeforePrint += new PrintEventHandler(ReportHelper.MainReport_BeforePrint);
                        xtraReport2.CreateDocument();
                        xtraReport1.Pages.AddRange((IEnumerable)xtraReport2.Pages);
                    }
                }
                xtraReport1.PrintingSystem.ContinuousPageNumbering = true;
                xtraReport1.PrintingSystem.ShowMarginsWarning = false;
                ReportPrintTool reportPrintTool = new ReportPrintTool((IReport)xtraReport1);
                Dv.DocumentSource = (object)xtraReport1;
                xtraReport1.CreateDocument();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #endregion Report Preview

        #region Event Handler
        private static void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Duplex = System.Drawing.Printing.Duplex.Vertical;
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
                tmpDataSource.ConnectionName = GetReportServerIP + "_" + GetReportServerCatalog + "_Connection"; //GetReportServerIP + "_AIMS2_Connection";
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

            //foreach (var item in (sender as XRSubreport).ReportSource.ComponentStorage)
            //{
            //    if (item is SqlDataSource)
            //    {
            //        ((SqlDataSource)item).ConnectionParameters = msSqlConnectionParameters1;
            //    }
            //}
        }
        #endregion Event Handler

        #region Method
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
                tmpDataSource.ConnectionName = GetReportServerIP + "_" + GetReportServerCatalog + "_Connection"; //"_AIMS2_Connection";
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

                tmpReportDataSource.ConnectionName = GetReportServerIP + "_AIMS2_Connection";
                tmpReportDataSource.Connection.ConnectionString = "Data Source=" + GetReportServerIP + ";Initial Catalog=" + GetReportServerCatalog + "; User Id=" + GetReportServerId + "; Password=" + GetReportServerPw + "; Connection Timeout = 60";
                ReportFile.DataSource = tmpReportDataSource;
            }

            return ReportFile;
        }
        #endregion Method

        #region Report Info Get / Set
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

        private static string SetReportPath
        {
            get
            {
                string ReportPATH = string.Empty;

                IniFile inifile = new IniFile();
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                ReportPATH = inifile.IniReadValue("UpgradeServer", "IP", SettingPath);
                return ReportPATH;
            }
        }

        private static string GetReportServerIP
        {
            get
            {
                string ReportServerIP = string.Empty;

                IniFile inifile = new IniFile();
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                ReportServerIP = inifile.IniReadValue("ReportServer", "IP", SettingPath);

                if (ReportServerIP == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("ReportServer", "IP", TmpServerIP, SettingPath);
                    ReportServerIP = inifile.IniReadValue("ReportServer", "IP", SettingPath);
                }

                return ReportServerIP;
            }
        }

        private static string GetReportServerCatalog
        {
            get
            {
                string ReportServerCatalog = string.Empty;

                IniFile inifile = new IniFile();
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                ReportServerCatalog = inifile.IniReadValue("ReportServer", "Catalog", SettingPath);

                if (ReportServerCatalog == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("ReportServer", "Catalog", "AIMS2", SettingPath);
                    ReportServerCatalog = inifile.IniReadValue("ReportServer", "Catalog", SettingPath);
                }

                return ReportServerCatalog;
            }
        }

        private static string GetReportServerId
        {
            get
            {
                string ReportServerId = string.Empty;

                IniFile inifile = new IniFile();
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                ReportServerId = inifile.IniReadValue("ReportServer", "uid", SettingPath);

                if (ReportServerId == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("ReportServer", "uid", "AIMS2", SettingPath);
                    ReportServerId = inifile.IniReadValue("ReportServer", "uid", SettingPath);
                }

                return ReportServerId;
            }
        }

        private static string GetReportServerPw
        {
            get
            {
                string ReportServerPw = string.Empty;

                IniFile inifile = new IniFile();
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                ReportServerPw = inifile.IniReadValue("ReportServer", "password", SettingPath);

                if (ReportServerPw == string.Empty)
                {
                    //default 
                    inifile.IniWriteValue("ReportServer", "password", "AIMS2", SettingPath);
                    ReportServerPw = inifile.IniReadValue("ReportServer", "password", SettingPath);
                }

                return ReportServerPw;
            }
        }

        private static DataSet DataTableAsDataSet(DataTable[] dtArray)
        {
            DataSet ds = new DataSet();
            for (int i = 0; i < dtArray.Length; i++)
            {
                ds.Tables.Add(dtArray[i]);
            }
            return ds;
        }

        #endregion Report Info Get / Set
    }

}
