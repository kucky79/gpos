using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList.Nodes;
using NF.A2P.Helper;
using NF.Framework.Common;
using NF.Framework.Win;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static NF.A2P.Helper.aGridHelper;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_GRWAUTH : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Cdbrd = "";
        public string Cdbrd
        {
            get { return _Cdbrd; }
            set { _Cdbrd = value; }
        }
        private string _Fgauth = "";

        public string Fgauth
        {
            get { return _Fgauth; }
            set { _Fgauth = value; }
        }
        private string _Data = "";

        public string Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        public POPUP_GRWAUTH( string cdbrd, string fgauth, string data)
        {
            Cdbrd = cdbrd;
            Fgauth = fgauth;
            Data = data;
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        private void InitEvent()
        {
            //aGrid1.ProcessGridKey += OK_Event;
            //aGrid1.KeyDown += OK_Event;
            gridView1.DoubleClick += gridView1_DoubleClick;
            gridView2.DoubleClick += gridView2_DoubleClick;
            aTree1.FocusedNodeChanged += ATree1_FocusedNodeChanged;
            aTree1.DoubleClick += ATree1_DoubleClick;
        }


        private void ATree1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                TreeListNode node = e.Node;
                string Strcddept = "";
                Strcddept = A.GetString(node.GetValue("CD_DEPT"));
                gridView1.ActiveFilterString = "CD_DEPT = '" + Strcddept + "'";
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void AButton_Search_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataTable dt = gridView2.GridControl.DataSource as DataTable;

            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);
            DataRow[] row = dt.Select("FG_INPUT = 'E' AND CD_EMP_DP = '" + gridView1.GetRowCellValue(info.RowHandle, "CD_EMP") + "'");
            if (row.Length == 0)
            {
                gridView2.AddNewRow();
                gridView2.UpdateCurrentRow();
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_FIRM", Global.FirmCode);
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_BRD", Cdbrd);
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "TP_DPP", Fgauth);
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "FG_INPUT", "E");
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_EMP_DP", gridView1.GetRowCellValue(info.RowHandle, "CD_EMP"));
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "YN_DISP", "Y");
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "DC_EMP_DP", gridView1.GetRowCellValue(info.RowHandle, "NM_EMP") + "(" + gridView1.GetRowCellValue(info.RowHandle, "CD_EMP") + ")");
            }
        }
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            gridView2.DeleteRow(gridView2.FocusedRowHandle);
        }

        private void ATree1_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = aTree1.FocusedNode;
            DataTable dt = gridView2.GridControl.DataSource as DataTable;

            DataRow[] row = dt.Select("FG_INPUT = 'D' AND CD_EMP_DP = '"+A.GetString(node.GetValue("CD_DEPT")) +"'");
            if (row.Length == 0)
            {
                gridView2.AddNewRow();
                gridView2.UpdateCurrentRow();
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_FIRM", Global.FirmCode);
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_BRD", Cdbrd);
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "TP_DPP", Fgauth);
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "FG_INPUT", "D");
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_EMP_DP", A.GetString(node.GetValue("CD_DEPT")));
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "YN_DISP", "Y");
                gridView2.SetRowCellValue(gridView2.RowCount - 1, "DC_EMP_DP", A.GetString(node.GetValue("NM_DEPT")) + "(" + A.GetString(node.GetValue("CD_DEPT"))+")");
            }
            //string  Cdauth = 
        }
        private void InitForm()
        {
            try
            {
                PopupTitle = "권한도움창";
                #region TREE Initialize
                DataSet ds = DBHelper.GetDataSet("AP_H_HRS_GRWORG_S", new object[] { Global.FirmCode, "xxxx", "xxxx" });
                aTree1.DataSource = ds.Tables[0];

                //키컬럼 설정
                aTree1.KeyFieldName = "CD_DEPT";
                //
                aTree1.ParentFieldName = "CD_DEPT_P";

                aTree1.ColumnVislble(new string[] { "CD_FIRM"});

                aTree1.ColumnReadOnly(new string[] { "NM_DEPT" });
                #endregion
                #region Grid Initialize

                DevExpress.XtraGrid.Columns.GridColumn col_H1 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_H2 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_H3 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_H4 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_H5 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_H6 = new DevExpress.XtraGrid.Columns.GridColumn();

                gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col_H1, col_H2, col_H3, col_H4, col_H5, col_H6 });

                col_H1.FieldName = "CD_FIRM";
                col_H1.Visible = false;
                
                col_H2.FieldName = "CD_DEPT";
                col_H2.Visible = false;

                col_H3.Caption = "부서";
                col_H3.FieldName = "NM_DEPT";
                col_H3.Width = 100;
                col_H3.VisibleIndex = 1;
                col_H3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                col_H4.Caption = "이름";
                col_H4.FieldName = "NM_EMP";
                col_H4.Width = 100;
                col_H4.VisibleIndex = 2;
                col_H4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                col_H4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                col_H5.Caption = "직책";
                col_H5.FieldName = "NM_ROLE";
                col_H5.Width = 100;
                col_H5.VisibleIndex = 3;
                col_H5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                col_H6.FieldName = "CD_EMP";
                col_H6.Visible = false;

                gridView1.OptionsBehavior.Editable = false;


                DevExpress.XtraGrid.Columns.GridColumn col_L1 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_L2 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_L3 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_L4 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_L5 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_L6 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_L7 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_L8 = new DevExpress.XtraGrid.Columns.GridColumn();

                gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col_L1, col_L2, col_L3, col_L4, col_L5, col_L6, col_L7, col_L8});

                col_L1.FieldName = "CD_FIRM";
                col_L1.Visible = false;

                col_L2.FieldName = "CD_BRD";
                col_L2.Visible = false;
                col_L3.FieldName = "TP_DPP";
                col_L3.Visible = false;
                col_L4.FieldName = "FG_INPUT";
                col_L4.Visible = false;
                col_L5.FieldName = "CD_EMP_DP";
                col_L5.Visible = false;
                col_L6.FieldName = "YN_DISP";
                col_L6.Visible = false;

                //col_L3.FieldName = "CD_AUTH_"+ Fgauth;
                //col_L3.Visible = false;

                //col_L4.Caption = "권한지정";
                //col_L4.FieldName = "DC_AUTH_" + Fgauth;
                //col_L4.Width = 100;
                //col_L4.VisibleIndex = 2;
                //col_L4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                //col_L4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col_L8.Caption = "권한지정";
                col_L8.FieldName = "DC_EMP_DP" ;
                col_L8.Width = 300;
                col_L8.VisibleIndex = 2;
                col_L8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                col_L8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                gridView2.OptionsBehavior.Editable = false;

                SetGridStyle(aGrid1, false, true, false);
                SetGridStyle(aGrid2, false, true, false);

                #endregion
                //aGrid2.Binding(ds.Tables[2]);
                //폼 위치 조정
                CenterToParent();


                //자동검색
                if (this.AutoSearch)
                {
                    this.OnSearch();
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        #region Buttons'  handlers
        protected override void OnSearch()
        {
            try
            {
                //string FirmCode = MdiForm.Global.CompanyCode;
                DataSet ds = DBHelper.GetDataSet("AP_H_HRS_GRWORG_S", new object[] { Global.FirmCode,Cdbrd, Fgauth});
                aGrid2.Binding(ds.Tables[2]);
                aGrid1.Binding(ds.Tables[1]);
                aTree1.DataSource = ds.Tables[0];
                aTree1.ExpandAll();

                if (A.GetString(Data) != "")
                {
                    DataTable Dt = null;
                    Dt = DBHelper.GetDataTable("AP_H_GRWAUTH_NAME_S", new object[] { Global.FirmCode, Data });
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        if (gridView2.RowCount > i)
                        {
                            if (A.GetString(gridView2.GetRowCellValue(i, "FG_INPUT")) != A.GetString(Dt.Rows[i]["FG"]) ||
                                A.GetString(gridView2.GetRowCellValue(i, "CD_EMP_DP")) != A.GetString(Dt.Rows[i]["CODE"]))
                            {
                                gridView2.SetRowCellValue(i, "CD_FIRM", Global.FirmCode);
                                gridView2.SetRowCellValue(i, "CD_BRD", Cdbrd);
                                gridView2.SetRowCellValue(i, "TP_DPP", Fgauth);
                                gridView2.SetRowCellValue(i, "FG_INPUT", A.GetString(Dt.Rows[i]["FG"]));
                                gridView2.SetRowCellValue(i, "CD_EMP_DP", A.GetString(Dt.Rows[i]["CODE"]));
                                gridView2.SetRowCellValue(i, "YN_DISP", "Y");
                                gridView2.SetRowCellValue(i, "DC_EMP_DP", A.GetString(Dt.Rows[i]["NAME"]) + "(" + A.GetString(Dt.Rows[i]["CODE"]) + ")");
                            }
                        }
                        else
                        {
                            gridView2.AddNewRow();
                            gridView2.UpdateCurrentRow();
                            gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_FIRM", Global.FirmCode);
                            gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_BRD", Cdbrd);
                            gridView2.SetRowCellValue(gridView2.RowCount - 1, "TP_DPP", Fgauth);
                            gridView2.SetRowCellValue(gridView2.RowCount - 1, "FG_INPUT", A.GetString(Dt.Rows[i]["FG"]));
                            gridView2.SetRowCellValue(gridView2.RowCount - 1, "CD_EMP_DP", A.GetString(Dt.Rows[i]["CODE"]));
                            gridView2.SetRowCellValue(gridView2.RowCount - 1, "YN_DISP", "Y");
                            gridView2.SetRowCellValue(gridView2.RowCount - 1, "DC_EMP_DP", A.GetString(Dt.Rows[i]["NAME"]) + "(" + A.GetString(Dt.Rows[i]["CODE"]) + ")");
                        }
                    }

                }
                //if (aGrid1.DefaultView.RowCount > 0)
                //{
                //    if (!aGrid1.Focused) { aGrid1.Select(); }
                //    gridView1.SelectRow(gridView1.FocusedRowHandle);
                //}
                //else
                //{
                //    //txtSearch.Select();
                //    ShowMessageBox(this.ResReader.GetString("The data not found. Please search again."), MessageType.Information);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            //if (aGrid2.MainView.RowCount > 0)
            //{
                string StrCdauth = "";
                string StrDcauth = "";
                gridView2.Columns["FG_INPUT"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView2.Columns["CD_EMP_DP"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                for (int i=0; i< gridView2.RowCount; i++)
                {
                    StrCdauth = StrCdauth + A.GetString(gridView2.GetRowCellValue(i, "FG_INPUT")) + A.GetString(gridView2.GetRowCellValue(i, "CD_EMP_DP"))+"|";
                    StrDcauth = StrDcauth + A.GetString(gridView2.GetRowCellValue(i, "DC_EMP_DP")) + ", ";
                }
                if (StrCdauth.Length > 0)
                    StrCdauth = StrCdauth.Substring(0, StrCdauth.Length - 1);
                if (StrDcauth.Length > 0)
                    StrDcauth = StrDcauth.Substring(0, StrDcauth.Length - 2);
                ReturnData.Add("CD_AUTH", StrCdauth);
                ReturnData.Add("DC_AUTH", StrDcauth);
                this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    //txtSearch.Select();

            //    ShowMessageBoxA("No items selected.\r\nSearch again and select the item on the list.", MessageType.Information);
            //    return;
            //}
        }

        protected override void OnCancel()
        {
            this.Close();
        }

        #endregion Buttons'  handlers



    }
}
