using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_SO_SEARCH01 : POSFormBase
    {
        public M_POS_SO_SEARCH01()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }
        POSConfig cfgUseCreditCard;
        POSConfig cfgVatYN;

        private void InitControl()
        {
            CH.SetDateEditFont(dtpSearch);

            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpSearch.Text = POSGlobal.SaleDt;

            gridViewItem.Columns["AM_TOT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_TOT"].SummaryItem.DisplayFormat =  "{0:##,##0.####}";

            gridViewItem.Columns["AM_CREDIT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_CREDIT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.Columns["QT_CREDIT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["QT_CREDIT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.Columns["AM_CREDIT_DIFF"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_CREDIT_DIFF"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.Columns["AM_VAT_ITEM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_VAT_ITEM"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.Columns["AM_VAT_DIFF"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_VAT_DIFF"].SummaryItem.DisplayFormat = "{0:##,##0.####}";
        }

        private void InitEvent()
        {
            btnCust.Click += BtnCust_Click;
            btnClear.Click += BtnClear_Click;

            Activated += M_POS_SO_SEARCH01_Activated;
        }

        private void M_POS_SO_SEARCH01_Activated(object sender, EventArgs e)
        {
            cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");
            cfgVatYN = POSConfigHelper.GetConfig("POS004");

            if (cfgUseCreditCard.ConfigValue == "Y")
            {
                gridViewItem.Columns["AM_CREDIT"].Visible = true;
                gridViewItem.Columns["QT_CREDIT"].Visible = true;
                gridViewItem.Columns["AM_CREDIT_DIFF"].Visible = true;
            }
            else
            {
                gridViewItem.Columns["AM_CREDIT"].Visible = false;
                gridViewItem.Columns["QT_CREDIT"].Visible = false;
                gridViewItem.Columns["AM_CREDIT_DIFF"].Visible = false;
            }

            if (cfgVatYN.ConfigValue == "Y")
            {
                gridViewItem.Columns["AM_VAT_ITEM"].Visible = true;
                gridViewItem.Columns["AM_VAT_DIFF"].Visible = true;
            }
            else
            {
                gridViewItem.Columns["AM_VAT_ITEM"].Visible = false;
                gridViewItem.Columns["AM_VAT_DIFF"].Visible = false;
            }
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            aLookUpEditCust.Properties.DataSource = null;
        }

        private void DtpSearch_Popup(object sender, EventArgs e)
        {
            DateEdit edit = sender as DateEdit;
            PopupDateEditForm form = (edit as IPopupControl).PopupWindow as PopupDateEditForm;
            form.Calendar.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.YearInfo;
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

            DataTable _dtCust = Search(new object[] { POSGlobal.StoreCode, dtpSearch.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource) });
            gridMain.Binding(_dtCust, true);

            LoadData.EndLoading();

        }

        string DownloadPath = string.Empty;

        public override void OnPrint()
        {
            try
            {
                //프린터타입 가져오기
                Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
                string PrintType = cfgPrint.ConfigValue;

                List<string> printMenu = new List<string>();
                List<string> printCode = new List<string>();

                printMenu.Add("감열지");
                if (cfgUseCreditCard.ConfigValue == "Y")
                    printMenu.Add("감열지\n(신용카드)");
                if (cfgVatYN.ConfigValue == "Y")
                    printMenu.Add("감열지\n(부가세)");
                printMenu.Add("일반");
                if (cfgUseCreditCard.ConfigValue == "Y")
                    printMenu.Add("일반\n(신용카드)");
                if (cfgVatYN.ConfigValue == "Y")
                    printMenu.Add("일반\n(부가세)");

                printCode.Add("T");
                if (cfgUseCreditCard.ConfigValue == "Y")
                    printCode.Add("TC");
                if (cfgVatYN.ConfigValue == "Y")
                    printCode.Add("TV");
                printCode.Add("P");
                if (cfgUseCreditCard.ConfigValue == "Y")
                    printCode.Add("PC");
                if (cfgVatYN.ConfigValue == "Y")
                    printCode.Add("PV");


                switch (PrintType)
                {
                    case "A": //둘다
                        string result;

                        P_POS_PRINT P_POS_PRINT = new P_POS_PRINT();
                        P_POS_PRINT.StartPosition = FormStartPosition.Manual;
                        P_POS_PRINT.PrintText = printMenu.ToArray();
                        P_POS_PRINT.PrintTag = printCode.ToArray();
                        P_POS_PRINT.Location = this.PointToScreen(new Point(this.Size.Width / 2 - P_POS_PRINT.Size.Width / 2, this.Size.Height / 2 - P_POS_PRINT.Size.Height / 2));

                        if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                        {
                            result = P_POS_PRINT.ResultPrint;

                            switch (result)
                            {
                                case "T":
                                    PrintThemal();
                                    break;
                                case "TC":
                                    PrintThemalCredit();
                                    break;
                                case "TV":
                                    PrintThemalVat();
                                    break;
                                case "P":
                                    PrintNormal();
                                    break;
                                case "PC":
                                    PrintNormalCredit();
                                    break;
                                case "PV":
                                    PrintNormalVat();
                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                    case "T": //감열지
                        PrintThemal();
                        break;
                    case "TC":
                        PrintThemalCredit();
                        break;
                    case "TV":
                        PrintThemalVat();
                        break;
                    case "P": //일반
                        PrintNormal();
                        break;
                    case "PC":
                        PrintNormalCredit();
                        break;
                    case "PV":
                        PrintNormalVat();
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
            GridHelper.ExcelExport(gridMain, "월별판매조회");
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
            sb.Append("조 회 월 : " + dtpSearch.DateTime.ToString("yyyy-MM") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.    거래처                매 출 액    \n");
            sb.Append("------------------------------------------\n");

            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                decimal NonPaidAmt = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_TOT"]));

                if (NonPaidAmt != 0)
                {
                    CustomerName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_CUST"]));
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName);

                    int blankLen = 42 - (A.GetByteLength(CustomerName) + 4);
                    //단가
                    sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));

                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridViewItem.Columns["AM_TOT"].SummaryItem.SummaryValue);


            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalCredit()
        {
            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(신용카드)" + "\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조 회 월 : " + dtpSearch.DateTime.ToString("yyyy-MM") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.    거래처                금    액    \n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.AlignLeft);
            decimal CardAmt = 0;

            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                CardAmt = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_CREDIT"]));

                //if (CardAmt > 0)
                {
                    sb.Append("거 래 처 : " + A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_CUST"])) + " " + A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["QT_CREDIT"])) + "건\n");
                    sb.Append("매출금액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_TOT"])).PadLeft(31, ' ') + "\n");
                    sb.Append("카드금액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_CREDIT"])).PadLeft(31, ' ') + "\n");
                    sb.Append("차    액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_CREDIT_DIFF"])).PadLeft(31, ' ') + "\n");
                    sb.Append("------------------------------------------\n");
                }
            }
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalVat()
        {
            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(부가세상품)" + "\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("조 회 월 : " + dtpSearch.DateTime.ToString("yyyy-MM") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.    거래처                금    액    \n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.AlignLeft);
            decimal CardAmt = 0;

            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                CardAmt = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_VAT_ITEM"]));

                //if (CardAmt > 0)
                {
                    sb.Append("거 래 처 : " + A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_CUST"])) + "\n");
                    sb.Append("매출금액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_TOT"])).PadLeft(31, ' ') + "\n");
                    sb.Append("부 가 세 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_VAT_ITEM"])).PadLeft(31, ' ') + "\n");
                    sb.Append("차    액 : " + A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM_VAT_DIFF"])).PadLeft(31, ' ') + "\n");
                    sb.Append("------------------------------------------\n");
                }
            }
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintNormal()
        {
            POSPrintHelper.POSReportPrint("R_POS_SO_SEARCH01", new string[] { "CD_STORE", "DT_SO", "CD_CUST" }, new string[] { POSGlobal.StoreCode, dtpSearch.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource) });
        }

        private void PrintNormalCredit()
        {
            POSPrintHelper.POSReportPrint("R_POS_SO_SEARCH01_CREDIT", new string[] { "CD_STORE", "DT_SO", "CD_CUST" }, new string[] { POSGlobal.StoreCode, dtpSearch.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource) });
        }

        private void PrintNormalVat()
        {
            POSPrintHelper.POSReportPrint("R_POS_SO_SEARCH01_VAT", new string[] { "CD_STORE", "DT_SO", "CD_CUST" }, new string[] { POSGlobal.StoreCode, dtpSearch.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource) });
        }
    }
}
