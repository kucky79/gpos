using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
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
    public partial class M_POS_SALE_SEARCH02 : POSFormBase
    {
        public string CustomerCode { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        ReceitMode ReceitType { get; set; } = ReceitMode.Double;

        private readonly Color ColorReturn = Color.FromArgb(209, 39, 79);

        decimal totalPrice;

        Bifrost.Helper.POSConfig cfgUseCreditCard;

        public M_POS_SALE_SEARCH02()
        {
            InitializeComponent();
            InitializeControl();
            InitEvent();
        }

        private void InitEvent()
        {
        }

        private void InitializeControl()
        {
            CH.SetDateEditFont(dtpFrom);
            CH.SetDateEditFont(dtpTo);

            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpFrom.Text = POSGlobal.SaleDt;
            dtpTo.Text = POSGlobal.SaleDt;

            this.gridView1.OptionsView.AllowCellMerge = false;

            gridView1.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridView1.Columns["AM_VAT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_VAT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridView1.Columns["AM_NET"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_NET"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridView1.Columns["AM_PAY"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_PAY"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridView1.Columns["AM_CREDIT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_CREDIT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridView1.Columns["AM_NONPAID"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridView1.Columns["AM_NONPAID"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            //신용카드설정
            cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");

            if (cfgUseCreditCard.ConfigValue == "Y")
            {
                gridView1.Columns["AM_CREDIT"].Visible = true;
            }
            else
            {
                gridView1.Columns["AM_CREDIT"].Visible = false;
            }

            //영수증설정
            //Bifrost.Helper.POSConfig cfgReceit = POSConfigHelper.GetConfig("RPT003");

            //switch (cfgReceit.ConfigValue)
            //{
            //    case "D":
            //        ReceitType = ReceitMode.Double;
            //        break;
            //    case "S":
            //        ReceitType = ReceitMode.Single;
            //        break;
            //    default:
            //        ReceitType = ReceitMode.Etc;
            //        break;
            //}

            //부가세 설정 화면 보이기 디폴트는 N임
            //POSConfig configVatYN = POSConfigHelper.GetConfig("POS004");
            //if(configVatYN.ConfigValue == "Y")
            //{
            //    gridView1.Columns["AM_VAT"].Visible = true;
            //    gridView1.Columns["AM_NET"].Visible = true;
            //}
            //else
            //{
            //    gridView1.Columns["AM_VAT"].Visible = false;
            //    gridView1.Columns["AM_NET"].Visible = false;
            //}
        }

        private void TabControlMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            OnView();
        }

        #region 그리드 이벤트 모음
        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;

            string ColName, ColMax;
            if (e.RowHandle >= 0)
            {
                ColName = A.GetString(View.GetRowCellValue(e.RowHandle, "NO_ROW"));
                ColMax = A.GetString(View.GetRowCellValue(e.RowHandle, "NO_MAX"));
                if ((ColName == "999" || ColName == ColMax) && ColName != string.Empty)
                {
                    e.Appearance.ForeColor = ColorReturn;
                }
                else
                {
                    e.Appearance.ForeColor = default;
                }
            }
        }

        private void GridView1_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "DT_SALE")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["DT_SALE"].ToString().Equals(dr2["DT_SALE"].ToString()) && dr1["NM_CUST"].ToString().Equals(dr2["NM_CUST"].ToString()) ;
            }
            else if (e.Column.FieldName == "NM_CUST")//Name 컬럼만 Merge
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

        private void GridView2_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "DT_SO")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["NO_SO"].ToString().Equals(dr2["NO_SO"].ToString());
            }
            else if (e.Column.FieldName == "NM_CUST")//Name 컬럼만 Merge
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

        private void GridView3_CellMerge(object sender, CellMergeEventArgs e)
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

        private void GridView4_CellMerge(object sender, CellMergeEventArgs e)
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

        #endregion 그리드 이벤트 모음

        #region 버튼 이벤트

        #endregion 버튼 이벤트

        #region 프린트 모음
        private void PrintThemalAll()
        {
            string CustomerName = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(전체)" + "\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                sb.Append("거 래 처 : " + A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"])) + "\n");
                sb.Append("판매금액 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM"])).PadLeft(31, ' ') + "\n");
                sb.Append("현금수금 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_PAY"])).PadLeft(31, ' ') + "\n");
                if (cfgUseCreditCard.ConfigValue == "Y")
                    sb.Append("카드수금 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_CREDIT"])).PadLeft(31, ' ') + "\n");
                sb.Append("외상금액 : " + A.GetNumericString(gridView1.GetRowCellValue(i, gridView1.Columns["AM_NONPAID"])).PadLeft(31, ' ') + "\n");
                sb.Append("------------------------------------------\n");
            }


            //sb.Append("==========================================\n");
            //sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            //sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
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
            sb.Append("(판매금액)" + "\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.  거래처                  판 매 액    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                decimal NonPaidAmt = A.GetDecimal(gridView1.GetRowCellValue(i, gridView1.Columns["AM"]));

                if (NonPaidAmt != 0)
                {
                    CustomerName = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"]));
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    거래처                판 매 액    \n");
                    //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                    int TotalLenth = A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + " " + A.GetNumericString(NonPaidAmt));
                    int blankLen;

                    if (TotalLenth > 42)
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + "\n");
                        blankLen = 42;
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }
                    else
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName);
                        blankLen = 42 - (A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName));
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }

                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridView1.Columns["AM"].SummaryItem.SummaryValue);


            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalPay()
        {
            string CustomerName = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(현금수금)" + "\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.  거래처                  현금수금    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                decimal NonPaidAmt = A.GetDecimal(gridView1.GetRowCellValue(i, gridView1.Columns["AM_PAY"]));

                if (NonPaidAmt != 0)
                {
                    CustomerName = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"]));
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    거래처                판 매 액    \n");
                    //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                    int TotalLenth = A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + " " + A.GetNumericString(NonPaidAmt));
                    int blankLen;

                    if (TotalLenth > 42)
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + "\n");
                        blankLen = 42;
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }
                    else
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName);
                        blankLen = 42 - (A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName));
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }

                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridView1.Columns["AM_PAY"].SummaryItem.SummaryValue);


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
            string CustomerName = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(카드수금)" + "\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.  거래처                  카드수금    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                decimal NonPaidAmt = A.GetDecimal(gridView1.GetRowCellValue(i, gridView1.Columns["AM_CREDIT"]));

                if (NonPaidAmt != 0)
                {
                    CustomerName = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"]));
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    거래처                판 매 액    \n");
                    //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                    int TotalLenth = A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + " " + A.GetNumericString(NonPaidAmt));
                    int blankLen;

                    if (TotalLenth > 42)
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + "\n");
                        blankLen = 42;
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }
                    else
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName);
                        blankLen = 42 - (A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName));
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }

                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridView1.Columns["AM_CREDIT"].SummaryItem.SummaryValue);


            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalNonpaid()
        {
            string CustomerName = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(외상금액)" + "\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.  거래처                  외상금액    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                decimal NonPaidAmt = A.GetDecimal(gridView1.GetRowCellValue(i, gridView1.Columns["AM_NONPAID"]));

                if (NonPaidAmt != 0)
                {
                    CustomerName = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"]));
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    거래처                판 매 액    \n");
                    //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                    int TotalLenth = A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + " " + A.GetNumericString(NonPaidAmt));
                    int blankLen;

                    if (TotalLenth > 42)
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + "\n");
                        blankLen = 42;
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }
                    else
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName);
                        blankLen = 42 - (A.GetByteLength(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName));
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }

                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridView1.Columns["AM_NONPAID"].SummaryItem.SummaryValue);


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
            POSPrintHelper.POSReportPrint("R_POS_SALE_SEARCH02_RPT01", new string[] { "CD_STORE", "DT_FROM", "DT_TO" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text });
        }
        #endregion 프린트 모음


        public override void OnView()
        {

            LoadData.StartLoading(this);

            DataTable dt = Search1(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text });
            gridMain.Binding(dt, true);

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
                        if (cfgUseCreditCard.ConfigValue == "Y")
                            P_POS_PRINT.PrintText = new string[] { "감열지\n(전체)", "감열지\n(판매금액)", "감열지\n(현금수금)", "감열지\n(카드수금)", "감열지\n(외상금액)", "일반" };
                        else
                            P_POS_PRINT.PrintText = new string[] { "감열지\n(전체)", "감열지\n(판매금액)", "감열지\n(현금수금)", "감열지\n(외상금액)", "일반" };

                        if (cfgUseCreditCard.ConfigValue == "Y")
                            P_POS_PRINT.PrintTag = new string[] { "TA", "TS", "TP", "TC", "TN", "P" };
                        else
                            P_POS_PRINT.PrintTag = new string[] { "TA", "TS", "TP", "TN", "P" };

                        P_POS_PRINT.Location = this.PointToScreen(new Point(this.Size.Width / 2 - P_POS_PRINT.Size.Width / 2, this.Size.Height / 2 - P_POS_PRINT.Size.Height / 2));
                        

                        if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                        {
                            result = P_POS_PRINT.ResultPrint;

                            switch (result)
                            {
                                case "TA":
                                    PrintThemalAll();
                                    break;
                                case "TS":
                                    PrintThemal();
                                    break;
                                case "TP":
                                    PrintThemalPay();
                                    break;
                                case "TC":
                                    PrintThemalCredit();
                                    break;
                                case "TN":
                                    PrintThemalNonpaid();
                                    break;
                                case "P":
                                    PrintNormal();
                                    break;
                                default:
                                    break;
                            }
                        }

                        break;
                    case "TA": //감열지
                        PrintThemalAll();
                        break;
                    case "TS": //감열지
                        PrintThemal();
                        break;
                    case "TP": //감열지
                        PrintThemalPay();
                        break;
                    case "TC": //감열지
                        PrintThemalCredit();
                        break;
                    case "TN": //감열지
                        PrintThemalNonpaid();
                        break;
                    case "P": //일반
                        PrintNormal();
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
            GridHelper.ExcelExport(gridMain, SubTitle);
        }
    }
}
