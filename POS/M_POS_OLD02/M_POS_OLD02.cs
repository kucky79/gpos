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
    public partial class M_POS_OLD02 : POSFormBase
    {
        public M_POS_OLD02()
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
            gridViewItem.Columns["SALE_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["SALE_AMT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            gridViewItem.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridViewItem.OptionsView.AllowCellMerge = true;

            gridViewDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridViewDetail.OptionsView.AllowCellMerge = false;

        }

        private void InitEvent()
        {
            gridViewItem.FocusedRowChanged += GridViewMain_FocusedRowChanged;
            gridViewItem.CellMerge += GridViewMain_CellMerge;

        }

        private void GridViewMain_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "DISP_NM")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["DISP_NM"].ToString().Equals(dr2["DISP_NM"].ToString());

            }
            else if (e.Column.FieldName == "AM_TOT")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보
                e.Merge = dr1["DISP_NM"].ToString().Equals(dr2["DISP_NM"].ToString()) && dr1["AM_TOT"].ToString().Equals(dr2["AM_TOT"].ToString());
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
                if (gridViewItem.GetFocusedRow() == null) return;

                string ItemCode = gridViewItem.GetFocusedRowCellValue("MNU_CD").ToString();

                gridViewDetail.ActiveFilterString = "MNU_CD = '" + ItemCode + "'";
                gridViewDetail.BestFitColumns();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void BtnCust_Click(object sender, EventArgs e)
        {
            //P_POS_CONTENTS P_POS_CONTENTS = new P_POS_CONTENTS();
            //P_POS_CONTENTS.ContentsType = ContentsMode.StoredProcedure;
            //P_POS_CONTENTS.StoredProcedure = "USP_OLD_SEARCH03_CUST";
            //P_POS_CONTENTS.dbType = DBType.Old;
            //P_POS_CONTENTS.spParams = new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text };
            //P_POS_CONTENTS.keyCustom = "CST_CD";
            //P_POS_CONTENTS.PopupTitle = "거래처 조회";
            ////P_POS_CONTENTS.dtCustom = SearchCust(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text });

            //if (P_POS_CONTENTS.ShowDialog() == DialogResult.OK)
            //{
            //    SetCombobox(aLookUpEditCust, CH.GetCode(P_POS_CONTENTS.SelectedContent), false);

            //    if (P_POS_CONTENTS != null)
            //    {
            //        P_POS_CONTENTS.Dispose();
            //    }
            //}
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

            DataSet _ds = Search(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text });
            gridMain.Binding(_ds.Tables[0], true);
            gridDetail.Binding(_ds.Tables[1], true);

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
            string ItemName = string.Empty;
            string SaleDt = string.Empty;
            string ItemDescrip = string.Empty;


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
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.  날 짜     상  품        판 매 액    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridViewItem.RowCount; i++)
            {
                decimal SaleAmt = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["SALE_AMT"]));

                SaleDt = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["YY"])) + " " + A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["MM"]));

                ItemName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DISP_NM"]));
                ItemDescrip = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["MNU_QTY"])) + A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["UNIT_NM"]));

                RowCnt++;
                //sb.Append(" No.    거래처                판 매 액    \n");
                //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                int TotalLenth = A.GetByteLength(ItemName + " " + ItemDescrip + " " + A.GetNumericString(SaleAmt));
                int padLen;

                string FirstGroup = " " + (i + 1).ToString().PadLeft(3, ' ') + " " + ItemName;

                sb.Append(FirstGroup);
                //단가
                //sb.Append(UnitAmount.PadLeft(17, ' '));
                //수량 단위
                padLen = 28 - (Encoding.Default.GetBytes(FirstGroup).Length + Encoding.Default.GetBytes(ItemDescrip).Length);

                string SecondGroup;

                if (padLen < 0)
                {
                    sb.Append(PrinterCommand.NewLine);
                    SecondGroup = ItemDescrip.PadLeft(28, ' ');
                    sb.Append(SecondGroup);
                    padLen = Encoding.Default.GetBytes(SecondGroup).Length;
                }
                else
                {
                    SecondGroup = A.SetString("", padLen, ' ') + ItemDescrip;
                    sb.Append(SecondGroup);
                    padLen = Encoding.Default.GetBytes(FirstGroup + SecondGroup).Length;
                }

                //금액
                sb.Append(A.GetNumericString(SaleAmt).PadLeft(14, ' '));
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
            POSPrintHelper.POSReportPrintOld("R_POS_OLD02", new string[] { "CD_STORE", "DT_FROM", "DT_TO" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text });
        }

    }
}
