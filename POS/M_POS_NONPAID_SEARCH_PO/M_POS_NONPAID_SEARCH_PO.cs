using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
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
    public partial class M_POS_NONPAID_SEARCH_PO : POSFormBase
    {
        DataTable _dtCust = new DataTable();
        Bifrost.Helper.POSConfig cfgUseCreditCard;

        public M_POS_NONPAID_SEARCH_PO() 
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitControl()
        {
            CH.SetDateEditFont(dtpSearch);
            CH.SetDateEditFont(dtpFrom);
            CH.SetDateEditFont(dtpTo);

            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpSearch.Text = POSGlobal.SaleDt;

            dtpFrom.Text = POSGlobal.SaleDt.Substring(0, 6) + "01";
            dtpTo.Text = POSGlobal.SaleDt;

            gridViewItem.Columns["AM_NONPAID_SUM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_NONPAID_SUM"].SummaryItem.DisplayFormat =  "{0:##,##0.####}";

            gridViewDetail.Columns["AM_PAY"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewDetail.Columns["AM_PAY"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewDetail.Columns["AM_CREDIT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewDetail.Columns["AM_CREDIT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewDetail.Columns["AM_PAY_TOT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewDetail.Columns["AM_PAY_TOT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            splitMain.SplitterPosition = 1270;

            cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");

            if (cfgUseCreditCard.ConfigValue == "Y")
            {
                gridViewDetail.Columns["AM_CREDIT"].Visible = true;
                gridViewDetail.Columns["AM_PAY_TOT"].Visible = true;
            }
            else
            {
                gridViewDetail.Columns["AM_CREDIT"].Visible = false;
                gridViewDetail.Columns["AM_PAY_TOT"].Visible = false;
            }

        }

        private void InitEvent()
        {
            gridViewItem.FocusedRowChanged += GridViewItem_FocusedRowChanged;
            btnPaySearch.Click += BtnPaySearch_Click;
            btnPrint.Click += BtnPrint_Click;

            dtpFrom.DateTimeChanged += DateTimeChanged;
            dtpTo.DateTimeChanged += DateTimeChanged;
        }

        private void DateTimeChanged(object sender, EventArgs e)
        {
            SearchPay();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
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
                                PrintThemalPay();
                                break;
                            case "P":
                                PrintNormalPay();
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "T": //감열지
                    PrintThemalPay();
                    break;
                case "P": //일반프린터
                    PrintNormalPay();
                    break;
                default:
                    break;
            }
        }

        bool IsPayClick = true;

        private void BtnPaySearch_Click(object sender, EventArgs e)
        {
            if (IsPayClick)
            {
                IsPayClick = false;
                splitMain.SplitterPosition = 540;
            }
            else
            {
                IsPayClick = true;
                splitMain.SplitterPosition = 1270;
            }
        }

        private void GridViewItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchPay();
        }

        private void SearchPay()
        {
            DataTable _dt = SearchDetail(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, gridViewItem.GetFocusedRowCellValue("CD_CUST") });
            gridDetail.Binding(_dt, true);
        }

        public override void OnView()
        {
            LoadData.StartLoading(this);

            _dtCust = Search(new object[] { POSGlobal.StoreCode, string.Empty, rboSearch.EditValue, dtpSearch.Text, "P" });
            gridMain.Binding(_dtCust, true);
            LoadData.EndLoading();

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
                                    PrintThemal();
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
                    case "P": //일반프린터
                        PrintNormal();
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
            GridHelper.ExcelExport(gridMain, this.SubTitle);
        }

        private void PrintThemal()
        {
            string CustomerName = string.Empty;

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
            sb.Append("조 회 일 : " + dtpSearch.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.    거래처                미 납 액    \n");
            sb.Append("==========================================\n");

            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                decimal NonPaidAmt = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_NONPAID_SUM"]));

                if (NonPaidAmt != 0)
                {
                    CustomerName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_CUST"]));
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName);

                    int blankLen = 42 - (A.GetByteLength(CustomerName) + 4);
                    //단가
                    sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, '-'));

                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridViewItem.Columns["AM_NONPAID_SUM"].SummaryItem.SummaryValue);


            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintNormal()
        {
            POSPrintHelper.POSReportPrint("POS_NONPAID_SEARCH_PO", new string[] { "P_CD_STORE", "P_SEARCH", "P_YN_USE", "P_DT_SALE" }, new string[] { POSGlobal.StoreCode, string.Empty, A.GetString(rboSearch.EditValue), dtpSearch.Text });

        }

        private void PrintNormalPay()
        {
            if (cfgUseCreditCard.ConfigValue == "Y")
            {
                POSPrintHelper.POSReportPrint("R_POS_NONPAID_SEARCH_PO_PAY", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetString(gridViewItem.GetFocusedRowCellValue("CD_CUST")) });
            }
            else 
            {
                POSPrintHelper.POSReportPrint("R_POS_NONPAID_SEARCH_PO_PAY_WO_CREDIT", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetString(gridViewItem.GetFocusedRowCellValue("CD_CUST")) });
            }
        }

        private void PrintThemalPay()
        {
            string SaleDt = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append("납 입 관 리" + "\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("거 래 처 : " + A.GetString(gridViewItem.GetFocusedRowCellValue("NM_CUST")) + "\n");
            sb.Append("조 회 일 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.    날  짜                수 금 액    \n");
            sb.Append("==========================================\n");

            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridViewDetail.RowCount; i++)
            {

                if (cfgUseCreditCard.ConfigValue == "Y")
                {
                    SaleDt = A.GetString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["DT_BILL"]));
                    SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append(RowCnt.ToString().PadLeft(2, ' ') + " " + SaleDt);
                    sb.Append(PrinterCommand.NewLine);

                    sb.Append("현    금 : " + A.GetNumericString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["AM_PAY"])).PadLeft(31, ' '));
                    sb.Append(PrinterCommand.NewLine);
                    sb.Append("신용카드 : " + A.GetNumericString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["AM_CREDIT"])).PadLeft(31, ' '));
                    sb.Append(PrinterCommand.NewLine);
                    sb.Append("납 입 합 : " + A.GetNumericString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["AM_PAY_TOT"])).PadLeft(31, ' '));
                    sb.Append(PrinterCommand.NewLine);
                    sb.Append("------------------------------------------\n");

                }
                else
                {
                    SaleDt = A.GetString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["DT_BILL"]));
                    SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append(RowCnt.ToString().PadLeft(2, ' ') + " " + SaleDt);

                    int blankLen = 42 - (A.GetByteLength(SaleDt) + 3);
                    //단가
                    sb.Append(A.GetNumericString(gridViewDetail.GetRowCellValue(i, gridViewDetail.Columns["AM_PAY"])).PadLeft(blankLen, ' '));
                    sb.Append(PrinterCommand.NewLine);
                }
            }

            decimal SumValue = A.GetDecimal(gridViewDetail.Columns["AM_PAY"].SummaryItem.SummaryValue);
            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

    }
}
