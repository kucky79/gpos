using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_ITEM_SALE_SEARCH01 : POSFormBase
    {

        DataTable _dtUnitSum;

        public M_POS_ITEM_SALE_SEARCH01()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitControl()
        {
            CH.SetDateEditFont(dtpFrom);
            CH.SetDateEditFont(dtpTo);

            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpFrom.Text = POSGlobal.SaleDt;
            dtpTo.Text = POSGlobal.SaleDt;

            //gridViewDetail.Columns["DT_SO"].ColumnEdit = GridHelper.SetGridMask(Bifrost.CommonFunction.MaskType.DATE);

            gridViewMain.OptionsView.AllowCellMerge = true;
            gridViewDetail.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            gridViewMain.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewMain.Columns["AM"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewDetail.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewDetail.Columns["AM"].SummaryItem.DisplayFormat = "{0:##,##0.####}";
        }

        private void InitEvent()
        {
            gridViewMain.FocusedRowChanged += GridViewMain_FocusedRowChanged;
            gridViewMain.CellMerge += GridViewMain_CellMerge;
            gridViewDetail.CellMerge += GridViewDetail_CellMerge;

            btnItem.Click += BtnItem_Click;
            btnClear.Click += BtnClear_Click;

            tokenEditItem.SizeChanged += TokenEditItem_SizeChanged;
        }

        private void GridViewMain_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "NM_ITEM")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["NM_ITEM"].ToString().Equals(dr2["NM_ITEM"].ToString());

            }
            else if (e.Column.FieldName == "AM_TOT")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보
                e.Merge = dr1["NM_ITEM"].ToString().Equals(dr2["NM_ITEM"].ToString()) && dr1["AM_TOT"].ToString().Equals(dr2["AM_TOT"].ToString());
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        private void TokenEditItem_SizeChanged(object sender, EventArgs e)
        {
            panelControlHeader.Size = new System.Drawing.Size(panelControlHeader.Size.Width, 65 + tokenEditItem.Size.Height);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            //aLookUpEditItem.Properties.DataSource = null;

            tokenEditItem.Properties.Tokens.BeginUpdate();
            tokenEditItem.Properties.Tokens.Clear();
            tokenEditItem.Properties.Tokens.EndUpdate();

            tokenEditItem.EditValue = null;
        }

        private void BtnItem_Click(object sender, EventArgs e)
        {
            try
            {
                P_POS_CONTENTS P_POS_CONTENTS = new P_POS_CONTENTS();
                P_POS_CONTENTS.ContentsType = ContentsMode.Item;

                if (P_POS_CONTENTS.ShowDialog() == DialogResult.OK)
                {
                    //SetControl ctr = new SetControl();
                    //ctr.SetCombobox(aLookUpEditCust.Properties.DataSource, CH.GetCode(P_POS_CONTENTS.SelectedContent));
                    //SetCombobox(aLookUpEditItem, CH.GetCode(P_POS_CONTENTS.SelectedContent), false);

                    //BindingList<string> OldValues = tokenEditItem.EditValue as BindingList<string>;


                    BindingList<string> values = tokenEditItem.EditValue as BindingList<string>;
                    if (values == null)
                        values = new BindingList<string>();

                    //string value = string.Empty;

                    tokenEditItem.Properties.EditValueType = DevExpress.XtraEditors.TokenEditValueType.List;

                    foreach (KeyValuePair<string, string> item in P_POS_CONTENTS.SelectedContent)
                    {
                        if (!(values.Contains(item.Key)))
                        {
                            tokenEditItem.Properties.BeginUpdate();
                            tokenEditItem.Properties.Tokens.AddToken(item.Value, item.Key);
                            tokenEditItem.Properties.EndUpdate();

                            values.Add(item.Key);
                        }
                    }

                    tokenEditItem.EditValue = values;



                    if (P_POS_CONTENTS != null)
                    {
                        P_POS_CONTENTS.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void GridViewDetail_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "NM_CUST")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["NM_CUST"].ToString().Equals(dr2["NM_CUST"].ToString());
            }

            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        private void GridViewMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewMain.GetFocusedRow() == null) return;

                string ItemCode = gridViewMain.GetFocusedRowCellValue("CD_ITEM").ToString();

                gridViewDetail.ActiveFilterString = "CD_ITEM = '" + ItemCode + "'";
                gridViewDetail.BestFitColumns();

                if (_dtUnitSum != null && _dtUnitSum.Rows.Count > 0)
                {
                    DataRow[] _dr = _dtUnitSum.Select("CD_ITEM = '" + ItemCode + "'");
                    if (_dr.Length > 0)
                    {

                        switch (_dr.Length)
                        {
                            case 1:
                                txtUnit01.Text = A.GetString(_dr[0]["DC_ITEM"]);
                                txtUnit02.Text = string.Empty;
                                txtUnit03.Text = string.Empty;
                                txtUnit04.Text = string.Empty;
                                break;
                            case 2:
                                txtUnit01.Text = A.GetString(_dr[0]["DC_ITEM"]);
                                txtUnit02.Text = A.GetString(_dr[1]["DC_ITEM"]);
                                txtUnit03.Text = string.Empty;
                                txtUnit04.Text = string.Empty;

                                break;
                            case 3:
                                txtUnit01.Text = A.GetString(_dr[0]["DC_ITEM"]);
                                txtUnit02.Text = A.GetString(_dr[1]["DC_ITEM"]);
                                txtUnit03.Text = A.GetString(_dr[2]["DC_ITEM"]);
                                txtUnit04.Text = string.Empty;
                                break;
                            case 4:
                                txtUnit01.Text = A.GetString(_dr[0]["DC_ITEM"]);
                                txtUnit02.Text = A.GetString(_dr[1]["DC_ITEM"]);
                                txtUnit03.Text = A.GetString(_dr[2]["DC_ITEM"]);
                                txtUnit04.Text = A.GetString(_dr[3]["DC_ITEM"]);
                                break;

                            default:
                                break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void BtnCtrClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCtrSearch_Click(object sender, EventArgs e)
        {
            OnView();
        }

        public override void OnView()
        {
            LoadData.StartLoading(this);

            DataSet _ds = Search(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, string.Empty, CH.GetTokenString(tokenEditItem) });// A.GetDatatableToString(aLookUpEditItem.Properties.DataSource) });
            _dtUnitSum = _ds.Tables[2];
            gridMain.Binding(_ds.Tables[0], true);
            gridDetail.Binding(_ds.Tables[1], true);
            LoadData.EndLoading();

            if(gridMain.MainView.RowCount == 1)
            {
                string ItemCode = gridViewMain.GetFocusedRowCellValue("CD_ITEM").ToString();

                gridViewDetail.ActiveFilterString = "CD_ITEM = '" + ItemCode + "'";
                gridViewDetail.BestFitColumns();
            }

        }

        public override void OnPrint()
        {
            string result = string.Empty;

            P_POS_PRINT P_POS_PRINT = new P_POS_PRINT();
            if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
            {
                result = P_POS_PRINT.ResultPrint;

                switch (result)
                {
                    case "AT": //감열지 전체
                        PrintThemal();
                        break;
                    case "ST": //감열지 선택
                        PrintThemalSeleted();
                        break;
                    case "AP": //일반 전체
                        PrintNormal();
                        break;
                    case "SP": //일반 선택       
                        PrintNormalDetail();
                        break;
                    case "N": //미발행
                        break;
                }

            }
        }

        public override void OnExcelExport()
        {
            GridHelper.ExcelExport(gridMain, "상품별판매현황");
        }

        private void PrintThemal()
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string Amount = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "\n");

            sb.Append("==========================================\n");
            sb.Append(" No.    상  품     수량/단위    판 매 액  \n");
            sb.Append("------------------------------------------\n");

            int padLen = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridViewMain.RowCount; i++)
            {
                //decimal SaleAmt = A.GetDecimal(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["AM"]));
                //if (SaleAmt != 0)
                {
                    str1 = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["NM_ITEM"]));
                    str2 = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["DC_ITEM"]));

                    Amount = A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["AM"]));

                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");

                    string FirstGroup = (i + 1).ToString() + " " + str1;

                    sb.Append(FirstGroup);
                    //단가
                    //sb.Append(UnitAmount.PadLeft(17, ' '));
                    //수량 단위
                    padLen = 31 - (Encoding.Default.GetBytes(FirstGroup).Length + Encoding.Default.GetBytes(str2).Length);

                    string SecondGroup;

                    if (padLen < 0)
                    {
                        sb.Append(PrinterCommand.NewLine);
                        SecondGroup = str2.PadLeft(30, ' ');
                        sb.Append(SecondGroup);
                        padLen = Encoding.Default.GetBytes(SecondGroup).Length;
                    }
                    else
                    {
                        SecondGroup = A.SetString("", padLen, ' ') + str2;
                        sb.Append(SecondGroup);
                        padLen = Encoding.Default.GetBytes(FirstGroup + SecondGroup).Length;
                    }

                    //금액
                    sb.Append(Amount.PadLeft(11, ' '));
                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridViewMain.Columns["AM"].SummaryItem.SummaryValue);


            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("------------------------------------------\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalSeleted()
        {
            string CustomerName = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            string Amount = string.Empty;
            int padLen = 0;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "(선택)\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "\n");
            sb.Append("상    품 : " + gridViewMain.GetFocusedRowCellValue("NM_ITEM") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.    거래처                판 매 액    \n");
            sb.Append("------------------------------------------\n");

            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridViewDetail.RowCount; i++)
            {
                //decimal SaleAmt = A.GetDecimal(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["AM"]));

                //if (SaleAmt != 0)
                {
                    str1 = A.GetString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["NM_CUST"]));
                    str2 = A.GetString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["DC_ITEM"]));

                    Amount = A.GetNumericString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["AM"]));

                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");

                    string FirstGroup = (i + 1).ToString() + " " + str1;

                    sb.Append(FirstGroup);
                    //단가
                    //sb.Append(UnitAmount.PadLeft(17, ' '));
                    //수량 단위
                    padLen = 31 - (Encoding.Default.GetBytes(FirstGroup).Length + Encoding.Default.GetBytes(str2).Length);

                    string SecondGroup;

                    if (padLen < 0)
                    {
                        sb.Append(PrinterCommand.NewLine);
                        SecondGroup = str2.PadLeft(31, ' ');
                        sb.Append(SecondGroup);
                        padLen = Encoding.Default.GetBytes(SecondGroup).Length;
                    }
                    else
                    {
                        SecondGroup = A.SetString("", padLen, ' ') + str2;
                        sb.Append(SecondGroup);
                        padLen = Encoding.Default.GetBytes(FirstGroup + SecondGroup).Length;
                    }

                    //금액
                    sb.Append(Amount.PadLeft(11, ' '));
                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridViewDetail.Columns["AM"].SummaryItem.SummaryValue);


            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("------------------------------------------\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintNormal()
        {
            POSPrintHelper.POSReportPrint("R_POS_ITEM_SALE_SEARCH01", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "CD_ITEM" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, string.Empty, CH.GetTokenString(tokenEditItem) });// A.GetDatatableToString(aLookUpEditItem.Properties.DataSource) });
        }

        private void PrintNormalDetail()
        {
            if (gridViewDetail.RowCount > 0)
            {
                POSPrintHelper.POSReportPrint("R_POS_ITEM_SALE_SEARCH02", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "CD_ITEM" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, string.Empty, A.GetString(gridViewMain.GetFocusedRowCellValue("CD_ITEM")) });
            }
            else
            {
                ShowMessageBoxA("선택된 행이 존재하지 않습니다.", Bifrost.Common.MessageType.Warning);
                return;
            }
        }

    }
}
