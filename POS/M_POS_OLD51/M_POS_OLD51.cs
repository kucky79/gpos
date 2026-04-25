using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
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
    public partial class M_POS_OLD51 : POSFormBase
    {
        public M_POS_OLD51()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitControl()
        {
            CH.SetDateEditFont(dtpSearch);


            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpSearch.Text = POSGlobal.SaleDt;
            gridViewItem.Columns["SALE_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["SALE_AMT"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            gridViewItem.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridViewItem.OptionsView.AllowCellMerge = true;

        }

        private void InitEvent()
        {
            gridViewItem.CellMerge += GridViewItem_CellMerge;
        }

        private void GridViewItem_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "CUST_NM")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["CUST_NM"].ToString().Equals(dr2["CUST_NM"].ToString());
                //e.Merge = dr1["YY"].ToString().Equals(dr1["YY"].ToString());

            }
            else if (e.Column.FieldName == "YY")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["CUST_NM"].ToString().Equals(dr2["CUST_NM"].ToString()) && dr1["YY"].ToString().Equals(dr2["YY"].ToString());
            }
            else
            {
                e.Merge = false;
            }


            


            e.Handled = true;
        }

        private void BtnCust_Click(object sender, EventArgs e)
        {
            P_POS_CONTENTS P_POS_CONTENTS = new P_POS_CONTENTS();
            P_POS_CONTENTS.ContentsType = ContentsMode.Customer;

            if (P_POS_CONTENTS.ShowDialog() == DialogResult.OK)
            {
                //SetControl ctr = new SetControl();
                //ctr.SetCombobox(aLookUpEditCust.Properties.DataSource, CH.GetCode(P_POS_CONTENTS.SelectedContent));
                //CH.SetCombobox(aLookUpEditCust, CH.GetCode(P_POS_CONTENTS.SelectedContent), false);

                if (P_POS_CONTENTS != null)
                {
                    P_POS_CONTENTS.Dispose();
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            //aLookUpEditCust.Properties.DataSource = null;
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

            DataTable _dtCust = Search(new object[] { POSGlobal.StoreCode, dtpSearch.Text.Substring(0,6), dtpSearch.Text.Substring(0,6) });
            gridMain.Binding(_dtCust, true);

            LoadData.EndLoading();

        }

        public override void OnExcelExport()
        {
            GridHelper.ExcelExport(gridMain, this.SubTitle);
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
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.        거래처            판 매 액    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                decimal SaleAmt = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["SALE_AMT"]));


                CustomerName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["CUST_NM"]));
                //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                RowCnt++;
                //sb.Append(" No.    거래처                판 매 액    \n");
                //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                int TotalLenth = A.GetByteLength(CustomerName + A.GetNumericString(SaleAmt)) + 4;
                int blankLen;

                if (TotalLenth >= 42)
                {
                    sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName + "\n");
                    blankLen = 42;
                    sb.Append(A.GetNumericString(SaleAmt).PadLeft(blankLen, ' '));
                }
                else
                {
                    sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + CustomerName);
                    blankLen = 42 - (A.GetByteLength(" " + CustomerName) + 4);
                    sb.Append(A.GetNumericString(SaleAmt).PadLeft(blankLen, ' '));
                }

                sb.Append(PrinterCommand.NewLine);

            }

            decimal SumValue = A.GetDecimal(gridViewItem.Columns["SALE_AMT"].SummaryItem.SummaryValue);


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
            POSPrintHelper.POSReportPrintOld("R_POS_OLD51", new string[] { "CD_STORE", "DT_FROM", "DT_TO" }, new string[] { POSGlobal.StoreCode, dtpSearch.Text.Substring(0,6), dtpSearch.Text.Substring(0,6) });
        }
    }
}
