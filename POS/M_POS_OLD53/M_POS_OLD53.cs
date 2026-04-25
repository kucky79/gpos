using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.Data;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
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
    public partial class M_POS_OLD53 : POSFormBase
    {
        public M_POS_OLD53()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitControl()
        {

            CH.SetDateEditFont(dtpFrom);
            CH.SetDateEditFont(dtpTo);

            aRadioButtonReturnYN.EditValue = "S";

            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpFrom.Text = POSGlobal.SaleDt;
            dtpTo.Text = POSGlobal.SaleDt;

            gridViewMain.Columns["SALE_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewMain.Columns["SALE_AMT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewMain.Columns["CLS_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewMain.Columns["CLS_AMT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.Columns["SALE_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Custom);
            gridViewItem.Columns["SALE_AMT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.Columns["CLS_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["CLS_AMT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridViewItem.OptionsView.AllowCellMerge = true;
        }

        private void InitEvent()
        {
            gridViewItem.CellMerge += GridViewItem_CellMerge;
            gridViewItem.CustomSummaryCalculate += GridViewItem_CustomSummaryCalculate;

            btnCust.Click += BtnCust_Click;
            btnClear.Click += BtnClear_Click;

            gridViewMain.RowStyle += GridView_RowStyle;
            gridViewItem.RowStyle += GridView_RowStyle;

            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            OnView();
        }

        private readonly Color ColorReturn = Color.FromArgb(209, 39, 79);
        private readonly Color ColorSum = Color.Crimson;


        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string SaleFlag = View.GetRowCellDisplayText(e.RowHandle, "SALE_GB");
                string SaleSum = View.GetRowCellDisplayText(e.RowHandle, "NO_ROW");
                if (SaleFlag == "A") //반품
                {
                    e.Appearance.ForeColor = ColorReturn;
                }
                else
                {
                    e.Appearance.ForeColor = default;
                }

                if (SaleSum == "999" && View.Name == "gridViewItem") // 합
                {
                    e.Appearance.ForeColor = ColorSum;
                }
                else
                {
                    e.Appearance.ForeColor = default;
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            aLookUpEditCust.Properties.DataSource = null;
        }

        private void BtnCust_Click(object sender, EventArgs e)
        {
            P_POS_CONTENTS P_POS_CONTENTS = new P_POS_CONTENTS();
            P_POS_CONTENTS.ContentsType = ContentsMode.StoredProcedure;
            P_POS_CONTENTS.StoredProcedure = "USP_OLD_SEARCH53_CUST";
            P_POS_CONTENTS.dbType = DBType.Old;
            P_POS_CONTENTS.spParams = new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text };
            P_POS_CONTENTS.keyCustom = "CST_CD";
            P_POS_CONTENTS.PopupTitle = "거래처 조회";

            if (P_POS_CONTENTS.ShowDialog() == DialogResult.OK)
            {
                SetCombobox(aLookUpEditCust, CH.GetCode(P_POS_CONTENTS.SelectedContent), false);

                if (P_POS_CONTENTS != null)
                {
                    P_POS_CONTENTS.Dispose();
                }
            }
        }

        public void SetCombobox(aLookUpEdit ctr, DataTable dt, bool codeView)
        {
            ctr.Properties.ValueMember = "CODE";
            ctr.Properties.DisplayMember = "NAME";
            ctr.Properties.DataSource = dt;
            ctr.Properties.ShowFooter = false;
            ctr.Properties.ShowHeader = false;
            //ctr.Properties.BestFitRowCount = 5;
            //ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;


            ctr.Properties.NullText = string.Empty;
            ctr.Properties.ShowNullValuePromptWhenFocused = true;
            ctr.Properties.AutoHeight = false;
            ctr.Properties.ShowLines = false;
            ctr.Properties.PopupSizeable = true;
            ctr.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize;
            ctr.Properties.PopupFormMinSize = new System.Drawing.Size(50, 50);
            ctr.Properties.ShowFooter = false;
            ctr.Properties.UseDropDownRowsAsMaxCount = true;
            ctr.Properties.DropDownRows = 15;
            ctr.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            if (dt.Rows.Count > 0)
                ctr.EditValue = dt.Rows[0][ctr.Properties.ValueMember];
        }

        decimal totalPrice;

        private void GridViewItem_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // Initialization.  
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                totalPrice = 0;
            }
            // Calculation. 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                totalPrice += A.GetDecimal(e.FieldValue);
            }
            // Finalization.  
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                e.TotalValue = totalPrice / 2;
            }
        }

        private void GridViewItem_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "SALE_DT")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["RCPT_NO"].ToString().Equals(dr2["RCPT_NO"].ToString());
            }
            else if (e.Column.FieldName == "CST_NM")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["RCPT_NO"].ToString().Equals(dr2["RCPT_NO"].ToString());
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
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
            object[] objParams = new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonReturnYN.EditValue };

            LoadData.StartLoading(this);

            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                DataTable _dtSum = SearchSum(objParams);
                gridSum.Binding(_dtSum, true);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                DataTable _dtDetail = Search(objParams);
                gridDetail.Binding(_dtDetail, true);
            }
            LoadData.EndLoading();
        }

        public override void OnExcelExport()
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                GridHelper.ExcelExport(gridSum, this.SubTitle + "일자별");
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                GridHelper.ExcelExport(gridDetail, this.SubTitle + "상품별");
            }
        }

        public override void OnPrint()
        {
            try
            {
                //프린터타입 가져오기
                Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
                string PrintType = cfgPrint.ConfigValue;

                switch (PrintType)
                {
                    case "A": //둘다
                        string result;

                        P_POS_PRINT P_POS_PRINT = new P_POS_PRINT();
                        P_POS_PRINT.StartPosition = FormStartPosition.Manual;
                        P_POS_PRINT.Location = this.PointToScreen(new Point(this.Size.Width / 2 - P_POS_PRINT.Size.Width / 2, this.Size.Height / 2 - P_POS_PRINT.Size.Height / 2));
                        P_POS_PRINT.PrintText = new string[] { "감열지", "일반" };
                        P_POS_PRINT.PrintTag = new string[] { "T", "P" };

                        if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                        {
                            result = P_POS_PRINT.ResultPrint;

                            switch (result)
                            {
                                case "T":
                                    if (xtraTabControl1.SelectedTabPageIndex == 0)
                                    {
                                        PrintThemal();
                                    }
                                    else if (xtraTabControl1.SelectedTabPageIndex == 1)
                                    {
                                        PrintThemalDetail();
                                    }
                                    break;
                                case "P":
                                    if (xtraTabControl1.SelectedTabPageIndex == 0)
                                    {
                                        PrintNormal();
                                    }
                                    else if (xtraTabControl1.SelectedTabPageIndex == 1)
                                    {
                                        PrintNormalDetail();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "T": //감열지
                        if (xtraTabControl1.SelectedTabPageIndex == 0)
                        {
                            PrintThemal();
                        }
                        else if (xtraTabControl1.SelectedTabPageIndex == 1)
                        {
                            PrintThemalDetail();
                        }
                        break;
                    case "P": //일반
                        if (xtraTabControl1.SelectedTabPageIndex == 0)
                        {
                            PrintNormal();
                        }
                        else if (xtraTabControl1.SelectedTabPageIndex == 1)
                        {
                            PrintNormalDetail();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void PrintThemal()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;


            string ItemName = string.Empty;
            string UnitAmount = string.Empty;
            string ItemDescrip = string.Empty;
            string Amount = string.Empty;
            int padLen = 0;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(기본)" + "\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");

            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);

            decimal GroupSumAmt = 0;
            int blankLen = 31;

            for (int i = 0; i < gridViewMain.RowCount; i++)
            {
                //헤더는 첫행만, 수금만일경우엔 마지막행만 있으므로 마지막행만
                CustomerName = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["CST_NM"]));
                SaleDt = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["SALE_DT"]));
                SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                sb.Append(PrinterCommand.AlignLeft);
                sb.Append("==========================================\n");
                sb.Append(SaleDt + " / " + CustomerName + "\n");
                sb.Append("------------------------------------------\n");
                sb.Append("이전미납 : " + A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["OLDDEF_AMT"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("구매금액 : " + A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["SALE_AMT"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("납 입 액 : " + A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["CLS_AMT"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("미납금액 : " + A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["NEWDEF_AMT"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("******************************************\n");
                sb.Append(PrinterCommand.NewLine);
            }

            sb.Append("구 매 합 : " + A.GetNumericString(gridViewMain.Columns["SALE_AMT"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("미 납 합 : " + A.GetNumericString(gridViewMain.Columns["CLS_AMT"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("==========================================\n");

            //decimal SumValue = A.GetDecimal(gridViewItem.Columns["AM"].SummaryItem.SummaryValue);

            //sb.Append("==========================================\n");
            //sb.Append(" 총합계  : " + SumValue.ToString("##,##0").PadLeft(31, ' ') + "\n");
            //sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }


        private void PrintThemalDetail()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;


            string ItemName = string.Empty;
            string UnitAmount = string.Empty;
            string ItemDescrip = string.Empty;
            string Amount = string.Empty;
            int padLen = 0;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(상세)" + "\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");

            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);

            decimal GroupSumAmt = 0;
            int blankLen = 31;

            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                //헤더는 첫행만, 수금만일경우엔 마지막행만 있으므로 마지막행만
                if (A.GetInt(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NO_ROW"])) == 1 || A.GetInt(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NO_MAX"])) == 1)
                {
                    CustomerName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["CST_NM"]));
                    SaleDt = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["SALE_DT"]));
                    SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append("==========================================\n");
                    sb.Append(SaleDt + " / " + CustomerName + "\n");
                    //수금만일때는 넣지 않음
                    if (A.GetInt(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NO_ROW"])) != 999)
                    {
                        sb.Append(" No.    상품명     수량/단위    금  액    \n");
                        sb.Append("------------------------------------------\n");
                    }
                }

                if (A.GetInt(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NO_ROW"])) != 999)
                {
                    ItemName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DISP_NM"]));
                    UnitAmount = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["APP_AMT"]));

                    ItemDescrip = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["MNU_QTY"])) + A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["UNIT_NM"]));
                    Amount = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["SALE_AMT"]));
                    padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                    int TotalLenth = A.GetByteLength(ItemName + " " + UnitAmount + " " + ItemDescrip + " " + Amount);

                    //if (TotalLenth >= 42)
                    //{
                    //    sb.Append(ItemName + " " + CustomerName + "\n");
                    //    blankLen = 42;
                    //    sb.Append(A.GetNumericString(SaleAmt).PadLeft(blankLen, ' '));
                    //}
                    //else
                    //{
                    //    sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + SaleDt + " " + CustomerName);
                    //    blankLen = 42 - (A.GetByteLength(SaleDt + " " + CustomerName) + 4);
                    //    sb.Append(A.GetNumericString(SaleAmt).PadLeft(blankLen, ' '));
                    //}


                    sb.Append(PrinterCommand.AlignLeft);
                    sb.Append(A.GetString(ItemName));
                    sb.Append(PrinterCommand.NewLine);

                    //단가
                    sb.Append(UnitAmount.PadLeft(17, ' '));
                    //수량 단위
                    sb.Append("".PadLeft(padLen) + ItemDescrip);
                    //금액
                    sb.Append(Amount.PadLeft(11, ' '));
                    sb.Append(PrinterCommand.NewLine);
                }
                //합계
                else if (A.GetInt(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NO_ROW"])) == 999)
                {
                    sb.Append("------------------------------------------\n");
                    sb.Append("이전미납 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["OLDDEF_AMT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("구매금액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["SALE_AMT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("납 입 액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["CLS_AMT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("미납금액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NEWDEF_AMT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("******************************************\n");
                    sb.Append(PrinterCommand.NewLine);
                }
            }

            sb.Append("구 매 합 : " + A.GetNumericString(gridViewItem.Columns["SALE_AMT"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("납 입 합 : " + A.GetNumericString(gridViewItem.Columns["CLS_AMT"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("==========================================\n");

            //decimal SumValue = A.GetDecimal(gridViewItem.Columns["AM"].SummaryItem.SummaryValue);

            //sb.Append("==========================================\n");
            //sb.Append(" 총합계  : " + SumValue.ToString("##,##0").PadLeft(31, ' ') + "\n");
            //sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintNormal()
        {
            POSPrintHelper.POSReportPrintOld("R_POS_OLD53_SUM", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "YN_RETURN" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonReturnYN.EditValue.ToString() });
        }

        private void PrintNormalDetail()
        {
            POSPrintHelper.POSReportPrintOld("R_POS_OLD53", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "YN_RETURN" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonReturnYN.EditValue.ToString() });
        }
    }
}
