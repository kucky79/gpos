using Bifrost;
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
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
    public partial class P_POS_TMP_ITEM : POSPopupBase
    {

        int RptWorkSheetSpace = 0;


        public string SlipDt { get; set; } = string.Empty;

        public string SlipType { get; set; } = string.Empty;

        public string PrintPort { get; set; } = string.Empty;

        DataTable _dt;

        public P_POS_TMP_ITEM()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        private void InitForm()
        {
            PopupTitle = "임시저장 상품별 합계";

            gridView1.OptionsView.ShowGroupPanel = false;
            //gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsCustomization.AllowSort = true;
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsView.AllowCellMerge = true;

            //그리드 높이
            gridView1.UserCellPadding = new Padding(0, 5, 0, 5);

            CH.SetDateEditFont(dtpSale, 16F);

            //프린터포트
            POSConfig configPrintPort = POSConfigHelper.GetConfig("PRT001");
            PrintPort = configPrintPort.ConfigValue;

            // 작업서 하단 여백 설정
            Bifrost.Helper.POSConfig cfgWorkSheetSpace = POSConfigHelper.GetConfig("RPT011");
            RptWorkSheetSpace = A.GetInt(cfgWorkSheetSpace.ConfigValue);
        }

        private void InitEvent()
        {
            this.Load += P_POS_SALE_Load;
            gridView1.DoubleClick += GridView1_DoubleClick;
            gridView1.CellMerge += GridView1_CellMerge;

            dtpSale.DateTimeChanged += DtpSale_DateTimeChanged;

            btnCancel.Click += BtnCancel_Click;
            btnDone.Click += BtnDone_Click;
            btnPrint.Click += BtnPrint_Click;
        }

        private void GridView1_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "NM_ITEM")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["NM_ITEM"].ToString().Equals(dr2["NM_ITEM"].ToString());
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
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
                                case "T": //감열지
                                    PrintThemal();
                                    break;
                                case "P": //일반
                                    PrintNormal();
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

        private void BtnDone_Click(object sender, EventArgs e)
        {
            OnOK();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DtpSale_DateTimeChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }

        private void P_POS_SALE_Load(object sender, EventArgs e)
        {
            dtpSale.Text = SlipDt;
        }

        protected override void OnSearch()
        {
            _dt = DBHelper.GetDataTable("USP_POS_TO_ITEM_S", new object[] { POSGlobal.StoreCode, dtpSale.Text, SlipType });
            gridList.Binding(_dt, true);
        }

        

        protected override void OnShown(EventArgs e)
        {
            OnSearch();
        }

        protected override void OnOK()
        {
            if (gridView1.RowCount > 0)
            {
                DataTable gridDT = (DataTable)gridList.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)gridList.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBoxA("데이터가 존재하지 않습니다.", MessageType.Information);
            }
        }

        //작업지시서
        private void PrintThemal()
        {
            try
            {
                string ItemName = string.Empty;
                string UnitAmount = string.Empty;
                string ItemDescrip = string.Empty;
                string Amount = string.Empty;
                decimal QtyBundle;
                decimal QtyUnit;
                string NewItem = string.Empty;

                int padLen = 0;

                StringBuilder sb = new StringBuilder();

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.AlignCenter);
                sb.Append(PrinterCommand.ConvertFontSize(2, 2));
                sb.Append("임시저장 상품별 합계\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(PrinterCommand.InitializePrinter);

                sb.Append("판 매 일  : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + "\n");
                sb.Append("==========================================\n");
                sb.Append(" No.       상품명            수량/단위    \n");
                sb.Append("------------------------------------------\n");

                string str1 = string.Empty;
                string str2 = string.Empty;
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));

                int RowCnt = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    ItemName = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_ITEM"]));

                    //20200413 묶음이 10일경우 * 1 문자열 치환하면 사라져서 묶음이 1일경우만 날리게 수정
                    QtyBundle = A.GetDecimal(gridView1.GetRowCellValue(i, gridView1.Columns["QT"]));
                    QtyUnit = A.GetDecimal(gridView1.GetRowCellValue(i, gridView1.Columns["QT_UNIT"]));
                    ItemDescrip = QtyBundle == 1 ? A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["DC_ITEM"])).Replace(" * 1", "") : A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["DC_ITEM"]));

                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                    //수량이 0보다 작고 작업서 설정에 따라 뺌

                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    str1 = (i + 1).ToString() + " " + ItemName;
                    str2 = ItemDescrip;
                    sb.Append(str1 + "".PadLeft(42 - (Encoding.Default.GetBytes(str1).Length + Encoding.Default.GetBytes(str2).Length), '.') + str2);
                    sb.Append(PrinterCommand.NewLine);
                    RowCnt++;
                }

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5 + RptWorkSheetSpace));
                sb.Append(PrinterCommand.Cut);
                PrinterCommand.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrintNormal()
        {
            POSPrintHelper.POSReportPrint("R_POS_TMP_ITEM_S", new string[] { "CD_STORE", "DT_SLIP", "FG_SLIP" }, new object[] { POSGlobal.StoreCode, dtpSale.Text, SlipType });
        }
    }
}
