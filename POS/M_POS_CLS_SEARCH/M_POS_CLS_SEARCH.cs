using Bifrost;
using Bifrost.Helper;
using Bifrost.Office;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
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
    public partial class M_POS_CLS_SEARCH : POSFormBase
    {
        public string CustomerCode { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;


        private readonly Color ColorReturn = Color.LightSteelBlue;

        public M_POS_CLS_SEARCH()
        {
            InitializeComponent();
            InitializeControl();
            InitEvent();
        }

        private void InitEvent()
        {
            btnCust.Click += BtnCust_Click;
            btnClear.Click += BtnClear_Click;

            gridView1.CellMerge += GridView1_CellMerge;
            gridView2.CellMerge += GridView2_CellMerge;

            gridView1.RowCellStyle += GridView_RowCellStyle;
            gridView2.RowCellStyle += GridView_RowCellStyle;
            gridView2.CustomSummaryCalculate += GridView2_CustomSummaryCalculate;
            gridView2.RowStyle += GridView_RowStyle;

            TabControlMain.SelectedPageChanged += TabControlMain_SelectedPageChanged;
            Shown += M_POS_CLS_SEARCH_Shown;
        }

        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;

            string ColValue;
            string ColLast;

            if (e.RowHandle >= 0)
            {
                ColValue = A.GetString(View.GetRowCellValue(e.RowHandle, "NO_ROW"));
                ColLast = A.GetString(View.GetRowCellValue(e.RowHandle, "NO_MAX"));
                if (ColValue == "999" && ColLast != "1") 
                {
                    e.Appearance.ForeColor = Color.Crimson;
                }
                else
                {
                    e.Appearance.ForeColor = default;
                }
            }
        }

        private void M_POS_CLS_SEARCH_Shown(object sender, EventArgs e)
        {
            OnView();
        }

        private void TabControlMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            OnView();
        }

        decimal totalPrice;

        private void GridView2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
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

        private void GridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (A.GetString(view.GetRowCellValue(e.RowHandle, "FG_SO")) == "R")
            {
                e.Appearance.Options.UseForeColor = true;
                e.Appearance.ForeColor = ColorReturn;
            }
        }

        private void GridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "NM_CUST")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["NM_CUST"].ToString().Equals(dr2["NM_CUST"].ToString());
            }

            else if (e.Column.FieldName == "DT_SO")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["DT_SO"].ToString().Equals(dr2["DT_SO"].ToString()) && dr1["NM_CUST"].ToString().Equals(dr2["NM_CUST"].ToString());
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        private void GridView2_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "NM_CUST")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["NO_SO"].ToString().Equals(dr2["NO_SO"].ToString());
            }

            else if (e.Column.FieldName == "DT_SO")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["NO_SO"].ToString().Equals(dr2["NO_SO"].ToString());
            }

            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            aLookUpEditCust.Properties.DataSource = null;
        }

        private void BtnCust_Click(object sender, EventArgs e)
        {
            P_POS_CONTENTS P_POS_CONTENTS = new P_POS_CONTENTS();
            P_POS_CONTENTS.ContentsType = ContentsMode.Customer;
            P_POS_CONTENTS.CustomerType = "2";

            if (P_POS_CONTENTS.ShowDialog() == DialogResult.OK)
            {
                //SetControl ctr = new SetControl();
                //ctr.SetCombobox(aLookUpEditCust.Properties.DataSource, CH.GetCode(P_POS_CONTENTS.SelectedContent));
                CH.SetCombobox(aLookUpEditCust, CH.GetCode(P_POS_CONTENTS.SelectedContent), false);

                if (P_POS_CONTENTS != null)
                {
                    P_POS_CONTENTS.Dispose();
                }
            }
        }

        private void InitializeControl()
        {
            Bifrost.Helper.POSConfig cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");

            if(cfgUseCreditCard.ConfigValue == "Y")
            {
                gridView1.Columns["AM_CREDIT"].Visible = true;
                gridView2.Columns["AM_CREDIT"].Visible = true;
            }
            else
            {
                gridView1.Columns["AM_CREDIT"].Visible = false;
                gridView2.Columns["AM_CREDIT"].Visible = false;
            }

            CH.SetDateEditFont(dtpFrom);
            CH.SetDateEditFont(dtpTo);

            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpFrom.Text = POSGlobal.SaleDt;
            dtpTo.Text = POSGlobal.SaleDt;


            gridView1.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            //gridView2.Columns["NO"].Summary.Add(DevExpress.Data.SummaryItemType.None);
            //gridView2.Columns["NO"].SummaryItem.DisplayFormat = "합계";

            gridView1.Columns["AM_SALE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_SALE"].SummaryItem.DisplayFormat = "{0:##,###.####}";

            gridView1.Columns["AM_PAY"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_PAY"].SummaryItem.DisplayFormat = "{0:##,###.####}";

            gridView1.Columns["AM_CREDIT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_CREDIT"].SummaryItem.DisplayFormat = "{0:##,###.####}";

            gridView2.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Custom);
            gridView2.Columns["AM"].SummaryItem.DisplayFormat = "{0:##,###.####}";

            gridView2.Columns["AM_PAY"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView2.Columns["AM_PAY"].SummaryItem.DisplayFormat = "{0:##,###.####}";

            gridView2.Columns["AM_CREDIT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView2.Columns["AM_CREDIT"].SummaryItem.DisplayFormat = "{0:##,###.####}";


            //GridHelper.SetDecimalPoint(gridMain, new string[] { "AM_OPEN", "AM_SALE", "AM_PAY", "AM_CREDIT", "AM_NONPAID" }, 0);
            gridView1.Columns["DT_SO"].ColumnEdit = GridHelper.SetGridMask(Bifrost.CommonFunction.MaskType.DATE);

            //GridHelper.SetDecimalPoint(gridDetail, new string[] { "AM", "AM_OPEN", "AM_PAY", "AM_CREDIT", "AM_NONPAID" }, 0);
            gridView2.Columns["DT_SO"].ColumnEdit = GridHelper.SetGridMask(Bifrost.CommonFunction.MaskType.DATE);

            gridView1.OptionsView.AllowCellMerge = true;
            gridView2.OptionsView.AllowCellMerge = true;

            //gridView1.Columns["AM_TOT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            //gridView1.Columns["AM_TOT"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            //gridView2.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            //gridView2.Columns["AM"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            AutoSearch = true;
        }

        public override void OnView()
        {
            //if (txtCust.Text == string.Empty)
            //    CustomerCode = string.Empty;

            object[] obj = new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonReturnYN.EditValue, aRadioButtonSort.EditValue };


            if (TabControlMain.SelectedTabPageIndex == 0)
            {
                LoadData.StartLoading(this);

                DataTable dt = Search1(obj);
                gridMain.Binding(dt, true);

                LoadData.EndLoading();
            }
            else if (TabControlMain.SelectedTabPageIndex == 1)
            {
                LoadData.StartLoading(this);

                DataTable dt = Search2(obj);
                gridDetail.Binding(dt, true);

                LoadData.EndLoading();
            }
        }

        public override void OnPrint()
        {
            try
            {
                //프린터타입 가져오기
                Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
                string PrintType = cfgPrint.ConfigValue;

                switch (TabControlMain.SelectedTabPageIndex)
                {
                    case 0:
                        switch (PrintType)
                        {
                            case "A": //둘다
                                string result;

                                P_POS_PRINT P_POS_PRINT = new P_POS_PRINT();
                                P_POS_PRINT.StartPosition = FormStartPosition.Manual;
                                P_POS_PRINT.PrintText = new string[] { "감열지", "일반" };
                                P_POS_PRINT.PrintTag = new string[] { "T", "P" };

                                //P_POS_PRINT.PrintText = new string[] { "감열지(전체)", "감열지(현금)", "감열지(신용)", "감열지(외상)", "일반" };
                                //P_POS_PRINT.PrintTag = new string[] { "T", "TM", "TC", "TN", "P" };
                                P_POS_PRINT.Location = this.PointToScreen(new Point(this.Size.Width / 2 - P_POS_PRINT.Size.Width / 2, this.Size.Height / 2 - P_POS_PRINT.Size.Height / 2));

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "T":
                                            PrintThemal();
                                            break;
                                        case "TM":
                                            PrintThemal(result);
                                            break;
                                        case "TC":
                                            PrintThemal(result);
                                            break;
                                        case "TN":
                                            PrintThemal(result);
                                            break;
                                        case "P":
                                            PrintNormal();
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                break;
                            case "T": //감열지
                                PrintThemal();
                                break;
                            case "P": //일반
                                PrintNormal();
                                break;
                        }
                        break;
                    case 1:
                        switch (PrintType)
                        {
                            case "A": //둘다
                                string result;

                                P_POS_PRINT P_POS_PRINT = new P_POS_PRINT();
                                P_POS_PRINT.StartPosition = FormStartPosition.Manual;
                                P_POS_PRINT.PrintText = new string[] { "감열지", "일반" };
                                P_POS_PRINT.PrintTag = new string[] { "T", "P" };

                                //P_POS_PRINT.PrintText = new string[] { "감열지(전체)", "감열지(현금)", "감열지(신용)", "감열지(외상)", "일반" };
                                //P_POS_PRINT.PrintTag = new string[] { "T", "TM", "TC", "TN", "P" };
                                P_POS_PRINT.Location = this.PointToScreen(new Point(this.Size.Width / 2 - P_POS_PRINT.Size.Width / 2, this.Size.Height / 2 - P_POS_PRINT.Size.Height / 2));

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "T":
                                            PrintThemalDetail();
                                            break;
                                        case "TM":
                                            PrintThemal(result);
                                            break;
                                        case "TC":
                                            PrintThemal(result);
                                            break;
                                        case "TN":
                                            PrintThemal(result);
                                            break;
                                        case "P":
                                            PrintNormalDetail();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            case "T": //감열지
                                PrintThemalDetail();
                                break;
                            case "P": //일반
                                PrintNormalDetail();
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnExcelExport()
        {
            if (TabControlMain.SelectedTabPageIndex == 0)
            {
                
                GridHelper.ExcelExport(gridMain, SubTitle + "_기본");
            }
            else
            {
                GridHelper.ExcelExport(gridDetail, SubTitle + "_상세");
            }
        }

        private void PrintThemal()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;
            Bifrost.Helper.POSConfig cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(기본)\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");

            int blankLen = 31;


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                CustomerName = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"]));
                SaleDt = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["DT_SO"]));
                SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                sb.Append(PrinterCommand.AlignLeft);
                sb.Append(SaleDt + " / " + CustomerName + "\n");

                sb.Append("이전외상 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_OPEN"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("판매금액 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_SALE"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("현금수금 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_PAY"])).PadLeft(blankLen, ' ') + "\n");
                if (cfgUseCreditCard.ConfigValue == "Y")
                    sb.Append("신용카드 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_CREDIT"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("외상금액 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_NONPAID"])).PadLeft(blankLen, ' ') + "\n");
                sb.Append("------------------------------------------\n");
            }


            sb.Append("판 매 합 : " + A.GetNumericString(gridView1.Columns["AM_SALE"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("현 금 합 : " + A.GetNumericString(gridView1.Columns["AM_PAY"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            if (cfgUseCreditCard.ConfigValue == "Y")
                sb.Append("카 드 합 : " + A.GetNumericString(gridView1.Columns["AM_CREDIT"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemal(string ThemalType)
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;

            Bifrost.Helper.POSConfig cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(기본)\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");

            int blankLen = 31;


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                CustomerName = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"]));
                SaleDt = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["DT_SO"]));
                SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                sb.Append(PrinterCommand.AlignLeft);
                sb.Append(SaleDt + " / " + CustomerName + "\n");
                //sb.Append("------------------------------------------\n");
                sb.Append(PrinterCommand.NewLine);

                if (ThemalType == "TM") //현금
                {
                    sb.Append("이전외상 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_OPEN"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("현금수금 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_PAY"])).PadLeft(blankLen, ' ') + "\n");
                    //sb.Append("신용카드 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_CREDIT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("외상금액 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_NONPAID"])).PadLeft(blankLen, ' ') + "\n");
                }
                else if (ThemalType == "TC") //신용카드
                {
                    sb.Append("이전외상 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_OPEN"])).PadLeft(blankLen, ' ') + "\n");
                    //sb.Append("현금수금 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_PAY"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("신용카드 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_CREDIT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("외상금액 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_NONPAID"])).PadLeft(blankLen, ' ') + "\n");
                }
                else if (ThemalType == "TN") //외상
                {
                    sb.Append("이전외상 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_OPEN"])).PadLeft(blankLen, ' ') + "\n");
                    //sb.Append("현금수금 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_PAY"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("신용카드 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_CREDIT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("외상금액 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_NONPAID"])).PadLeft(blankLen, ' ') + "\n");
                }


                sb.Append("------------------------------------------\n");
            }


            sb.Append("판 매 합 : " + A.GetNumericString(gridView1.Columns["AM_SALE"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("현 금 합 : " + A.GetNumericString(gridView1.Columns["AM_PAY"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            if (cfgUseCreditCard.ConfigValue == "Y")
                sb.Append("카 드 합 : " + A.GetNumericString(gridView1.Columns["AM_CREDIT"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            //PrinterCommand.Print("COM1", sb.ToString());
            sb.Clear();
        }

         
        private void PrintThemalDetail()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;

            Bifrost.Helper.POSConfig cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");

            string ItemName = string.Empty;
            string UnitAmount = string.Empty;
            string ItemDescrip = string.Empty;
            string Amount = string.Empty;
            decimal QtyBundle;
            int padLen = 0;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n"); 
            sb.Append("(상세)\n");
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

            for (int i = 0; i < gridView2.RowCount; i++)
            {
                //헤더는 첫행만, 수금만일경우엔 마지막행만 있으므로 마지막행만
                if (A.GetInt(gridView2.GetRowCellValue(i, gridView2.Columns["NO_ROW"])) == 1 || A.GetInt(gridView2.GetRowCellValue(i, gridView2.Columns["NO_MAX"])) == 1)
                {
                    CustomerName = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["NM_CUST"]));
                    SaleDt = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["DT_SO"]));
                    SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append("==========================================\n");
                    sb.Append(SaleDt + " / " + CustomerName + "\n");
                    sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append("------------------------------------------\n");

                }

                if (A.GetInt(gridView2.GetRowCellValue(i, gridView2.Columns["NO_ROW"])) != 999)
                {
                    ItemName = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["NM_ITEM"]));
                    UnitAmount = A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["UM"]));
                    QtyBundle = A.GetDecimal(gridView2.GetRowCellValue(i, gridView2.Columns["QT"]));


                    ItemDescrip = QtyBundle == 1 ? A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["DC_ITEM"])).Replace(" * 1", "") : A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["DC_ITEM"]));
                    Amount = A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM"]));
                    padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;


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
                else if (A.GetInt(gridView2.GetRowCellValue(i, gridView2.Columns["NO_ROW"])) == 999)
                {
                    sb.Append("------------------------------------------\n");

                    sb.Append("이전외상 : " + A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM_OPEN"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("판매금액 : " + A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM_SALE"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("현금수금 : " + A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM_PAY"])).PadLeft(blankLen, ' ') + "\n");
                    if (cfgUseCreditCard.ConfigValue == "Y")
                        sb.Append("신용카드 : " + A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM_CREDIT"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("외상금액 : " + A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM_NONPAID"])).PadLeft(blankLen, ' ') + "\n");
                    sb.Append("******************************************\n");
                    sb.Append(PrinterCommand.NewLine);
                }
            }

            sb.Append("판 매 합 : " + A.GetNumericString(gridView2.Columns["AM"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("현 금 합 : " + A.GetNumericString(gridView2.Columns["AM_PAY"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            if (cfgUseCreditCard.ConfigValue == "Y")
                sb.Append("카 드 합 : " + A.GetNumericString(gridView2.Columns["AM_CREDIT"].SummaryItem.SummaryValue).PadLeft(blankLen, ' ') + "\n");
            sb.Append("==========================================\n");

            //decimal SumValue = A.GetDecimal(gridView2.Columns["AM"].SummaryItem.SummaryValue);

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
            string[] strParams = new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), A.GetString(aRadioButtonReturnYN.EditValue), A.GetString(aRadioButtonSort.EditValue) };

            POSPrintHelper.POSReportPrint("R_POS_CLS_SEARCH01", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "YN_RETURN", "FG_SORT" }, strParams);
        }

        private void PrintNormalDetail()
        {
            string[] strParams = new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), A.GetString(aRadioButtonReturnYN.EditValue), A.GetString(aRadioButtonSort.EditValue) };

            POSPrintHelper.POSReportPrint("R_POS_CLS_SEARCH02", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "YN_RETURN", "FG_SORT" }, strParams);

        }

    }
}
