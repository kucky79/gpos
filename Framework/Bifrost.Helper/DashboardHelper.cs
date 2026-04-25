using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml;
using DevExpress.DashboardWin;
using DevExpress.XtraEditors;
using System.Text;
namespace Bifrost.Helper
{
    public class DashboardHelper
    {

        private static string TmpServerIP = string.Empty;

        public static void DashboardView(DashboardDesigner Dsd, string Strboardtitle, DataSet DsM)
        {
            try
            {
                string StrItem = "";
                string StrLayout = "";
                if (DsM.Tables[0].Rows.Count > 0)
                {
                    StrItem = A.GetString(DsM.Tables[0].Rows[0]["DC_ITEM1"]) + A.GetString(DsM.Tables[0].Rows[0]["DC_ITEM2"]) + A.GetString(DsM.Tables[0].Rows[0]["DC_ITEM3"]) +
                        A.GetString(DsM.Tables[0].Rows[0]["DC_ITEM4"]) + A.GetString(DsM.Tables[0].Rows[0]["DC_ITEM5"]);
                    StrLayout = A.GetString(DsM.Tables[0].Rows[0]["DC_LAYOUT1"]) + A.GetString(DsM.Tables[0].Rows[0]["DC_LAYOUT2"]) + A.GetString(DsM.Tables[0].Rows[0]["DC_LAYOUT3"]) +
                        A.GetString(DsM.Tables[0].Rows[0]["DC_LAYOUT4"]) + A.GetString(DsM.Tables[0].Rows[0]["DC_LAYOUT5"]);
                }
                MemoryStream stream = new MemoryStream();
                string Strproc = "";
                string StrtmpProc = "";
                ArrayList Arrporc = new ArrayList();
                for (int i = 0; i < DsM.Tables[2].Rows.Count; i++)
                {
                    if (A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"]) == "")
                        continue;
                    if (StrtmpProc == "")
                        StrtmpProc = A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"]);
                    Arrporc.Add(A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"]));
                }
                if(Arrporc.Count > 0)
                    Strproc = string.Join("|", Arrporc.ToArray());

                DataSet Ds = DBHelper.GetDataSet("AP_GET_PROC_INFO", new string[] { Strproc });
                XmlWriter writer = XmlWriter.Create(stream);

                writer.WriteStartElement("Dashboard"); //Dashboard
                writer.WriteAttributeString("CurrencyCulture", "ko-KR");
                writer.WriteStartElement("Title");//Title
                writer.WriteAttributeString("Text", Strboardtitle);
                writer.WriteEndElement();//Title
                writer.WriteStartElement("DataSources");//DataSources

                writer.WriteStartElement("SqlDataSource");//SqlDataSource
                writer.WriteAttributeString("ComponentName", "dashboardSqlDataSource1");
                writer.WriteStartElement("Name");//Name
                writer.WriteValue("SQL Data Source 1");
                writer.WriteEndElement();//Name
                writer.WriteStartElement("Connection");//Connection
                writer.WriteAttributeString("Name", "Connection");
                writer.WriteAttributeString("ProviderKey", "MSSqlServer");
                writer.WriteStartElement("Parameters");//Parameters
                writer.WriteStartElement("Parameter");//Parameter
                writer.WriteAttributeString("Name", "server");
                writer.WriteAttributeString("Value", GetServerIP);
                writer.WriteEndElement();//Parameter
                writer.WriteStartElement("Parameter");//Parameter
                writer.WriteAttributeString("Name", "database");
                writer.WriteAttributeString("Value", "AIMS2");
                writer.WriteEndElement();//Parameter
                writer.WriteStartElement("Parameter");//Parameter
                writer.WriteAttributeString("Name", "useIntegratedSecurity");
                writer.WriteAttributeString("Value", "False");
                writer.WriteEndElement();//Parameter
                writer.WriteStartElement("Parameter");//Parameter
                writer.WriteAttributeString("Name", "read only");
                writer.WriteAttributeString("Value", "1");
                writer.WriteEndElement();//Parameter
                writer.WriteStartElement("Parameter");//Parameter
                writer.WriteAttributeString("Name", "generateConnectionHelper");
                writer.WriteAttributeString("Value", "false");
                writer.WriteEndElement();//Parameter
                writer.WriteStartElement("Parameter");//Parameter
                writer.WriteAttributeString("Name", "userid");
                writer.WriteAttributeString("Value", "AIMS2");
                writer.WriteEndElement();//Parameter
                writer.WriteStartElement("Parameter");//Parameter
                writer.WriteAttributeString("Name", "password");
                writer.WriteAttributeString("Value", "AIMS2");
                writer.WriteEndElement();//Parameter
                writer.WriteEndElement();//Parameters
                writer.WriteEndElement();//Connection
                ArrayList Arrlist = new ArrayList();
                for (int i = 0; i < DsM.Tables[2].Rows.Count; i++)
                {
                //    for (int i = 0; i < Strprocs.Length; i++)
                //{

                    writer.WriteStartElement("Query");//Query
                    writer.WriteAttributeString("Type", "StoredProcQuery");
                    writer.WriteAttributeString("Name", A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"])+ A.GetString(DsM.Tables[2].Rows[i]["DC_PARAM"]));
                    int j = 0;
                    string Strtype = "";
                    foreach (DataRow rows in Ds.Tables[0].Rows)
                    {
                        if (j > 0 && A.GetString(rows["SPECIFIC_NAME"]) != A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"])) break;
                        if (A.GetString(rows["SPECIFIC_NAME"]) != A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"])) continue;
                        j++;

                        writer.WriteStartElement("Parameter");//Parameter
                        writer.WriteAttributeString("Name", A.GetString(rows["PARAMETER_NAME"]));
                        if (A.GetString(rows["DATA_TYPE"]).ToUpper().Contains("CHAR") || A.GetString(rows["DATA_TYPE"]).ToUpper().Contains("TEXT"))
                        {
                            Strtype = "String";
                        }
                        else if (A.GetString(rows["DATA_TYPE"]).ToUpper().Contains("NUMERIC"))
                        {
                            Strtype = "Decimal";
                        }
                        else
                        {
                            Strtype = "Int32";
                        }
                        if (A.GetString(DsM.Tables[2].Rows[i]["DC_PARAM"]) != "" && (A.GetString(rows["PARAMETER_NAME"]).Contains("CD_BRD") || A.GetString(rows["PARAMETER_NAME"]).Contains("CD_DCITEM")))
                        {
                            Arrlist.Add(A.GetString(rows["PARAMETER_NAME"]));
                            writer.WriteAttributeString("Type", "System." + Strtype);
                            writer.WriteValue(A.GetString(DsM.Tables[2].Rows[i]["DC_PARAM"]));
                        }
                        else
                        {
                            writer.WriteAttributeString("Type", "DevExpress.DataAccess.Expression");
                            writer.WriteValue("(System." + Strtype + ", mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089)([Parameters." + A.GetString(rows["PARAMETER_NAME"]).Substring(1) + "])");
                            
                        }
                        writer.WriteEndElement();//Parameter
                    }

                    writer.WriteStartElement("ProcName");//ProcName
                    writer.WriteValue(A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"]));
                    writer.WriteEndElement();//ProcName
                    writer.WriteEndElement();//Query
                }
                writer.WriteStartElement("ResultSchema");//ResultSchema
                writer.WriteStartElement("DataSet");//DataSet
                writer.WriteAttributeString("Name", "SQL Data Source 1");

                for (int i = 0; i < DsM.Tables[2].Rows.Count; i++)
                {
                    writer.WriteStartElement("View");//View
                    writer.WriteAttributeString("Name", A.GetString(DsM.Tables[2].Rows[i]["NM_PROC"]) + A.GetString(DsM.Tables[2].Rows[i]["DC_PARAM"]));

                    writer.WriteEndElement();//View
                }
                writer.WriteEndElement();//DataSet
                writer.WriteEndElement();//ResultSchema
                writer.WriteEndElement();//SqlDataSource
                writer.WriteEndElement();//DataSources

                writer.WriteStartElement("Parameters");//Parameters
                string Strtype2 = "";
                foreach (DataRow rows in Ds.Tables[1].Rows)
                {
                    if (Arrlist.Count > 0)
                    {
                        if (Arrlist.Contains(A.GetString(rows["PARAMETER_NAME"])) && (A.GetString(rows["PARAMETER_NAME"]).Contains("CD_BRD") || A.GetString(rows["PARAMETER_NAME"]).Contains("CD_DCITEM")))
                            continue;
                    }
                    writer.WriteStartElement("Parameter");//Parameter
                    writer.WriteAttributeString("Name", A.GetString(rows["PARAMETER_NAME"]).Substring(1));
                    if (A.GetString(rows["DATA_TYPE"]).ToUpper().Contains("CHAR") == false && A.GetString(rows["DATA_TYPE"]).ToUpper().Contains("TEXT") == false)
                    {
                        if (A.GetString(rows["DATA_TYPE"]).ToUpper().Contains("NUMERIC"))
                        {
                            Strtype2 = "Decimal";
                        }
                        else
                        {
                            Strtype2 = "Int32";
                        }
                        writer.WriteAttributeString("Type", "System." + Strtype2 + ", mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                    }
                    writer.WriteEndElement();//Parameter
                }

                writer.WriteEndElement();//Parameters

                //if (A.GetString(StrItem) == "")
                //{
                    StringBuilder str = new StringBuilder();
                    if (DsM.Tables[1].Rows.Count > 0)
                    {
                        str.Append(@"<Items>");
                        int count = 0;
                        foreach(DataRow row in DsM.Tables[1].Rows)
                        {
                            count++;
                            str.Append(@"<Grid ComponentName=""gridDashboardItem"+A.GetString(count) +@""" Name=""" + A.GetString(row["DC_TITLE"]) + @""" DataSource=""dashboardSqlDataSource1"" DataMember=""" + A.GetString(row["NM_PROC"]) + A.GetString(row["DC_PARAM"]) + @""">");
                            switch(A.GetString(row["NM_PROC"]))
                            {
                                case "AP_HRS_BBORD_BRD_S":
                                    str.Append(@"<DataItems>
                                        <Dimension DataMember=""FG_DATA"" UniqueName=""DataItem0"" />
                                        <Dimension DataMember=""CD_BRD"" UniqueName=""DataItem1"" />
                                        <Dimension DataMember=""NO_NTC"" UniqueName=""DataItem2"" />
                                        <Dimension DataMember=""NM_NTC"" Name=""Title"" UniqueName=""DataItem3"" />
                                        <Dimension DataMember=""DT_WRITE"" Name=""Date"" UniqueName=""DataItem4"" />
                                      </DataItems>
                                      <HiddenDimensions>
                                        <Dimension UniqueName=""DataItem0"" />
                                        <Dimension UniqueName=""DataItem1"" />
                                        <Dimension UniqueName=""DataItem2"" />
                                      </HiddenDimensions>
                                      <GridColumns>
                                        <GridDimensionColumn Weight=""130"">
                                          <Dimension UniqueName=""DataItem3"" />
                                        </GridDimensionColumn>
                                        <GridDimensionColumn Weight=""40"">
                                          <Dimension UniqueName=""DataItem4""/>
                                        </GridDimensionColumn>
                                      </GridColumns >
                                      <GridOptions ColumnWidthMode=""Manual"" />");
                                    break;
                                case "AP_HRS_BBORD_DOC_S":
                                    str.Append(@"<DataItems>
                                        <Dimension DataMember=""FG_DATA"" UniqueName=""DataItem0"" />
                                        <Dimension DataMember=""CD_DCITEM"" UniqueName=""DataItem1"" />
                                        <Dimension DataMember=""NO_APP"" UniqueName=""DataItem2"" />
                                        <Dimension DataMember=""NM_DOC"" Name=""Title"" UniqueName=""DataItem3"" />
                                        <Dimension DataMember=""DT_APP"" Name=""Date"" UniqueName=""DataItem4"" />
                                      </DataItems>
                                      <HiddenDimensions>
                                        <Dimension UniqueName=""DataItem0"" />
                                        <Dimension UniqueName=""DataItem1"" />
                                        <Dimension UniqueName=""DataItem2"" />
                                      </HiddenDimensions>
                                      <GridColumns>
                                        <GridDimensionColumn Weight=""130"">
                                          <Dimension UniqueName=""DataItem3"" />
                                        </GridDimensionColumn>
                                        <GridDimensionColumn Weight=""40"">
                                          <Dimension UniqueName=""DataItem4"" />
                                        </GridDimensionColumn>
                                      </GridColumns >
                                      <GridOptions ColumnWidthMode=""Manual"" />");

                                    break;
                                case "AP_HRS_BBORD_DOC_1_S": //결재대기함
                                case "AP_HRS_BBORD_DOC_2_S": //기안함
                                case "AP_HRS_BBORD_DOC_3_S": //진행함
                                case "AP_HRS_BBORD_DOC_4_S": //완료함
                                case "AP_HRS_BBORD_DOC_5_S": //반려함
                                case "AP_HRS_BBORD_DOC_7_S": //문서수신함
                                    str.Append(@"<DataItems>
                                        <Dimension DataMember=""FG_DATA"" UniqueName=""DataItem0"" />
                                        <Dimension DataMember=""NO_APP"" UniqueName=""DataItem1"" />
                                        <Dimension DataMember=""NM_DOC"" Name=""Title"" UniqueName=""DataItem2"" />
                                        <Dimension DataMember=""DT_APP"" Name=""Date"" UniqueName=""DataItem3"" />
                                      </DataItems>
                                      <HiddenDimensions>
                                        <Dimension UniqueName=""DataItem0"" />
                                        <Dimension UniqueName=""DataItem1"" />
                                      </HiddenDimensions>
                                      <GridColumns>
                                        <GridDimensionColumn Weight=""130"">
                                          <Dimension UniqueName=""DataItem2"" />
                                        </GridDimensionColumn>
                                        <GridDimensionColumn Weight=""40"">
                                          <Dimension UniqueName=""DataItem3"" />
                                        </GridDimensionColumn>
                                      </GridColumns >
                                      <GridOptions ColumnWidthMode=""Manual"" />");

                                    break;
                                case "AP_HRS_BBORD_DOC_6_S": //개인문서함
                                    str.Append(@"<DataItems>
                                        <Dimension DataMember=""FG_DATA"" UniqueName=""DataItem0"" />
                                        <Dimension DataMember=""NO_APP"" UniqueName=""DataItem1"" />
                                        <Dimension DataMember=""CD_FORM"" UniqueName=""DataItem2"" />
                                        <Dimension DataMember=""NM_DOC"" Name=""Title"" UniqueName=""DataItem3"" />
                                        <Dimension DataMember=""DT_APP"" Name=""Date"" UniqueName=""DataItem4"" />
                                      </DataItems>
                                      <HiddenDimensions>
                                        <Dimension UniqueName=""DataItem0"" />
                                        <Dimension UniqueName=""DataItem1"" />
                                        <Dimension UniqueName=""DataItem2"" />
                                      </HiddenDimensions>
                                      <GridColumns>
                                        <GridDimensionColumn Weight=""130"">
                                          <Dimension UniqueName=""DataItem3"" />
                                        </GridDimensionColumn>
                                        <GridDimensionColumn Weight=""40"">
                                          <Dimension UniqueName=""DataItem4"" />
                                        </GridDimensionColumn>
                                      </GridColumns >
                                      <GridOptions ColumnWidthMode=""Manual"" />");
                                    break;

                            }
                            str.Append(@"</Grid>");
                        }
                        str.Append(@"</Items>");
                    }
                    else {

                        str.Append(@"
     <Items>
    <Grid ComponentName=""gridDashboardItem1"" Name=""Grid 1"">
      <GridOptions />
    </Grid >
    <Grid ComponentName=""gridDashboardItem2"" Name=""Grid 2"">
      <GridOptions />
    </Grid>
    <Grid ComponentName=""gridDashboardItem3"" Name=""Grid 3"">
      <GridOptions />
    </Grid>
    <Grid ComponentName=""gridDashboardItem4"" Name=""Grid 4"">
      <GridOptions />
    </Grid>
    <Grid ComponentName=""gridDashboardItem5"" Name=""Grid 5"">
      <GridOptions />
    </Grid>
    <Grid ComponentName=""gridDashboardItem6"" Name=""Grid 6"">
      <GridOptions />
    </Grid>
  </Items>");
                    }
                    writer.WriteRaw(str.ToString());
                    StrItem = str.ToString();
                //}
                //else
                //{
                //    writer.WriteRaw(StrItem);

                //}
                if (A.GetString(StrLayout) == "")
                {
                    //writer.Close();
                    //byte[] StingData = System.Text.Encoding.UTF8.GetBytes(StrLayout);
                    //stream.Write(StingData, 0, StingData.Length);
                    StrLayout = @"
  <LayoutTree>
    <LayoutGroup Orientation=""Vertical"">
      <LayoutGroup Weight=""32.258064516129032"">
        <LayoutItem DashboardItem=""gridDashboardItem1"" Weight=""45.054945054945058"" />
        <LayoutItem DashboardItem=""gridDashboardItem2"" Weight=""45.054945054945058"" />
      </LayoutGroup>
      <LayoutGroup Weight=""32.258064516129032"">
        <LayoutItem DashboardItem=""gridDashboardItem3"" Weight=""45.054945054945058"" />
        <LayoutItem DashboardItem=""gridDashboardItem4"" Weight=""45.054945054945058"" />
      </LayoutGroup>
      <LayoutGroup Weight=""32.258064516129032"">
        <LayoutItem DashboardItem=""gridDashboardItem5"" Weight=""45.054945054945058"" />
        <LayoutItem DashboardItem=""gridDashboardItem6"" Weight=""45.054945054945058"" />
      </LayoutGroup>
    </LayoutGroup>
  </LayoutTree> ";

                    writer.WriteRaw(StrLayout);
                }
                else
                {
                    writer.WriteRaw(StrLayout);
                }
                writer.WriteEndElement();//Dashboard
                writer.Close();

                Int32 Intlen = 0;
                string[] Stritemarr = new string[] { "", "", "", "", "" };
                string[] Strlayoutarr = new string[] { "", "", "", "", "" };
                if (DsM.Tables[0].Rows.Count == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (StrItem.Length > i * 4000)
                        {
                            Intlen = StrItem.Length - (i * 4000);
                            if (Intlen > 4000)
                                Intlen = 4000;
                            Stritemarr[i] = StrItem.Substring(i * 4000, Intlen);
                        }
                        else
                            break;

                    }
                    for (int i = 0; i < 5; i++)
                    {
                        if (StrLayout.Length > i * 4000)
                        {
                            Intlen = StrLayout.Length - (i * 4000);
                            if (Intlen > 4000)
                                Intlen = 4000;
                            Strlayoutarr[i] = StrLayout.Substring(i * 4000, Intlen);
                        }
                        else
                            break;

                    }
                    DBHelper.ExecuteNonQuery("AP_MAS_BBORD_SET_I", new object[] { Global.FirmCode, Global.UserID,
                        Stritemarr[0], Stritemarr[1], Stritemarr[2], Stritemarr[3], Stritemarr[4],
                        Strlayoutarr[0], Strlayoutarr[1], Strlayoutarr[2], Strlayoutarr[3], Strlayoutarr[4],  Global.UserID });
                }
                else if (DsM.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (StrItem.Length > i * 4000)
                        {
                            Intlen = StrItem.Length - (i * 4000);
                            if (Intlen > 4000)
                                Intlen = 4000;
                            Stritemarr[i] = StrItem.Substring(i * 4000, Intlen);
                        }
                        else
                            break;

                    }
                    for (int i = 0; i < 5; i++)
                    {
                        if (StrLayout.Length > i * 4000)
                        {
                            Intlen = StrLayout.Length - (i * 4000);
                            if (Intlen > 4000)
                                Intlen = 4000;
                            Strlayoutarr[i] = StrLayout.Substring(i * 4000, Intlen);
                        }
                        else
                            break;

                    }
                    DBHelper.ExecuteNonQuery("AP_MAS_BBORD_SET_U", new object[] { Global.FirmCode, Global.UserID,
                        Stritemarr[0], Stritemarr[1], Stritemarr[2], Stritemarr[3], Stritemarr[4],
                        Strlayoutarr[0], Strlayoutarr[1], Strlayoutarr[2], Strlayoutarr[3], Strlayoutarr[4],  Global.UserID });
                }

                //string settingsString = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                stream.Position = 0;
                //FileStream file = new FileStream("E:\\file.XML", FileMode.Create, FileAccess.Write);
                //stream.WriteTo(file);
                //file.Close();
                Dsd.LoadDashboard(stream);
                stream.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
                
        private static string GetServerIP
        {
            get
            {
                string ServerIP = string.Empty;
                string ConnectionString = "";

                IniFile inifile = new IniFile();
                string SettingPath = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";

                ConnectionString = inifile.IniReadValue("DB", "ConnectionString", SettingPath);

                if (A.GetString(ConnectionString) != "")
                {
                    char[] delimiterChars = { ';', '=' };
                    string[] words = ConnectionString.Split(delimiterChars);

                    ServerIP = words[1].ToString();
                }

                return ServerIP;
            }
        }

    }

}
