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
    public partial class M_POS_OLD_SALE : POSFormBase
    {

        DBHandler DB = new DBHandler();
        ReceitMode ReceitType { get; set; } = ReceitMode.Double;

        DataSet _dsTmp;
        public M_POS_OLD_SALE()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitControl()
        {
            CH.SetDateEditFont(dtpFrom);

            dtpFrom.Text = A.GetString(DBHelperOld.ExecuteScalar("SELECT MAX(SALE_DT) FROM TR_SALE_H WHERE STORE_CD = '" + POSGlobal.StoreCode + "'"));

            //그리드 금액 합계 표기
            gridViewMain.Columns["SALE_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewMain.Columns["SALE_AMT"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            //영수증설정
            Bifrost.Helper.POSConfig cfgReceit = POSConfigHelper.GetConfig("RPT003");

            switch (cfgReceit.ConfigValue)
            {
                case "D":
                    ReceitType = ReceitMode.Double;
                    break;
                case "S":
                    ReceitType = ReceitMode.Single;
                    break;
                default:
                    ReceitType = ReceitMode.Etc;
                    break;
            }

            _dsTmp = DB.SearchDetail(new object[] { POSGlobal.StoreCode, "ZZZZZZZZ" });
            AutoSearch = true;
        }

        private void InitEvent()
        {
            gridViewList.FocusedRowChanged += GridViewList_FocusedRowChanged;

            btnDatePre.Click += BtnDatePre_Click;
            btnDateNext.Click += BtnDateNext_Click;

            btnCtrPrint.Click += BtnCtrPrint_Click;
            btnCtrClose.Click += BtnCtrClose_Click;
            btnWorkSheet.Click += BtnWorkSheet_Click;
            dtpFrom.DateTimeChanged += DtpFrom_DateTimeChanged;
        }

        private void BtnDateNext_Click(object sender, EventArgs e)
        {
            dtpFrom.DateTime = dtpFrom.DateTime.AddDays(1);
        }

        private void BtnDatePre_Click(object sender, EventArgs e)
        {
            dtpFrom.DateTime = dtpFrom.DateTime.AddDays(-1);
        }

        private void DtpFrom_DateTimeChanged(object sender, EventArgs e)
        {
            OnView();

        }

        private void BtnCtrClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnWorkSheet_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewList.RowCount == 0)
                {
                    ShowMessageBoxA("선택된 항목이 존재하지 않습니다.\n조회를 해주세요", Bifrost.Common.MessageType.Warning);
                    return;
                }

                PrintWorkSheet();

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void BtnCtrPrint_Click(object sender, EventArgs e)
        {

            try
            {

                if(gridViewList.RowCount == 0)
                {
                    ShowMessageBoxA("선택된 항목이 존재하지 않습니다.\n조회를 해주세요", Bifrost.Common.MessageType.Warning);
                    return;
                }

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
                                    if (A.GetString(gridViewList.GetFocusedRowCellValue("FG_SLIP")) == "PAY")
                                        PrintNonpaidReceit();
                                    else
                                        PrintReceit();
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
                        if (A.GetString(gridViewList.GetFocusedRowCellValue("FG_SLIP")) == "PAY")
                            PrintNonpaidReceit();
                        else
                            PrintReceit();
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

        decimal SubTotalAmt = 0;
        decimal DiscountAmt = 0;

        private void GridViewList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataSet ds = DB.SearchDetail(new object[] { POSGlobal.StoreCode, gridViewList.GetFocusedRowCellValue("RCPT_NO") });
            gridMain.Binding(ds.Tables[0], true);

            if (ds.Tables[1].Rows.Count > 0)
            {
                txtPreNonPaidAmt.EditValue = ds.Tables[1].Rows[0]["OLDDEF_AMT"];

                txtReceiveAmt.EditValue = A.GetDecimal(ds.Tables[1].Rows[0]["OLDDEF_AMT"]) + A.GetDecimal(gridViewMain.Columns["SALE_AMT"].SummaryItem.SummaryValue);
                txtNonAmt.EditValue = ds.Tables[1].Rows[0]["AM_NON"];
                txtPayAmt.EditValue = A.GetString(ds.Tables[1].Rows[0]["SO_NO"]) == string.Empty ? ds.Tables[1].Rows[0]["PAY_AMT"] : ds.Tables[1].Rows[0]["AM_PAY"];
                txtTotalNonAmt.EditValue = ds.Tables[1].Rows[0]["NEWDEF_AMT"];
                SubTotalAmt = A.GetDecimal(ds.Tables[1].Rows[0]["MID_AMT"]);
                DiscountAmt = A.GetDecimal(ds.Tables[1].Rows[0]["TOTDIS_AMT"]);

                if (DiscountAmt > 0)
                {
                    panelDiscount.Visible = true;
                    txtDiscountAmt.EditValue = DiscountAmt;
                }
                else
                {
                    panelDiscount.Visible = false;
                }
            }
            gridViewList.Focus();

        }

        private void BtnSearchList_Click(object sender, EventArgs e)
        {
            OnView();
        }

        public override void OnView()
        {
            gridList.Binding(DB.SearchList(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpFrom.Text }), true);
            if (gridViewList.RowCount == 0)
            {
                gridMain.Binding(_dsTmp.Tables[0], true);
            }
            else
            {
                GridViewList_FocusedRowChanged(null, null);
            }    
        }

        private void PrintNormal()
        {
            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });

            string AccountNo1 = A.GetString(dtStore.Rows[0]["NO_ACCOUNT1"]);
            string AccountNo2 = A.GetString(dtStore.Rows[0]["NO_ACCOUNT2"]);

            POSPrintHelper.POSReportPrintOld("R_POS_OLD_SALE_RPT01", new string[] { "CD_STORE", "NO_RCPT", "NO_ACCOUNT1", "NO_ACCOUNT2" }, new string[] { POSGlobal.StoreCode, A.GetString(gridViewList.GetFocusedRowCellValue("RCPT_NO")), AccountNo1, AccountNo2 });
        }

        private void PrintReceit()
        {
            try
            {
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
                sb.Append("영  수  증\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.AlignRight);
                sb.Append("재발행\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));
                sb.Append("고  객  명 : " + gridViewList.GetFocusedRowCellValue("CST_NM") + "\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("상      호 : " + POSGlobal.StoreName + "\n");
                sb.Append("사업자번호 : " + dtStore.Rows[0]["NO_BIZ"] + "\n");
                sb.Append("대  표  자 : " + dtStore.Rows[0]["NM_CEO"] + "\n");
                sb.Append("주      소 : " + dtStore.Rows[0]["DC_ADDR1"] + " " + dtStore.Rows[0]["DC_ADDR2"] + "\n");
                sb.Append("TEL : " + dtStore.Rows[0]["NO_TEL1"] + " / " + dtStore.Rows[0]["NO_TEL2"] + "\n");
                sb.Append("FAX : " + dtStore.Rows[0]["NO_FAX1"] + "\n");
                sb.Append("담  당  자 : " + POSGlobal.EmpName + "\n");
                sb.Append("거래  번호 : " + gridViewList.GetFocusedRowCellValue("RCPT_NO") + "\n");
                sb.Append("판매  시간 : " + gridViewList.GetFocusedRowCellValue("SALE_DT") + " " + gridViewList.GetFocusedRowCellValue("SALE_TM") + "\n"); ;
                sb.Append("==========================================\n");
                sb.Append(" No.  상품명       수량/단위      금   액 \n");
                sb.Append("------------------------------------------\n");

                if (ReceitType == ReceitMode.Double)
                {
                    for (int i = 0; i < gridViewMain.RowCount; i++)
                    {
                        ItemName = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["DISP_NM"]));
                        ItemDescrip = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["DC_ITEM"]));
                        UnitAmount = A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["APP_AMT"]));
                        Amount = A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["SALE_AMT"]));
                        padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                        sb.Append(PrinterCommand.AlignLeft);
                        //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                        sb.Append(" " + (i + 1).ToString().PadLeft(3, ' ') + " " + ItemName);
                        sb.Append(PrinterCommand.NewLine);
                        //sb.Append(PrinterCommand.AlignRight);
                        //단가
                        sb.Append(UnitAmount.PadLeft(17, ' '));
                        //수량 단위
                        sb.Append("".PadLeft(padLen) + ItemDescrip);

                        //금액
                        sb.Append(Amount.PadLeft(11, ' '));
                        sb.Append(PrinterCommand.NewLine);
                    }
                }
                else if (ReceitType == ReceitMode.Single)
                {
                    for (int i = 0; i < gridViewMain.RowCount; i++)
                    {
                        ItemName = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["DISP_NM"]));
                        ItemDescrip = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["DC_ITEM"]));
                        UnitAmount = A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["APP_AMT"]));
                        Amount = A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["SALE_AMT"]));

                        sb.Append(PrinterCommand.AlignLeft);
                        //sb.Append(" No.    상품명     수량/단위    금  액    \n");

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
                        sb.Append(Amount.PadLeft(14, ' '));
                        sb.Append(PrinterCommand.NewLine);
                    }
                }

                sb.Append("------------------------------------------\n");
                //sb.Append(" 매 출 액 : " + txtPayAmt.Text.PadLeft(30, ' ') + "\n");
                var summaryValue = gridViewMain.Columns["SALE_AMT"].SummaryItem.SummaryValue;
                sb.Append(" 총    계 : " + A.GetNumericString(SubTotalAmt).PadLeft(30, ' ') + "\n"); // A.GetNumericString(summaryValue).PadLeft(30, ' ') + "\n");
                if (DiscountAmt > 0)
                {
                    sb.Append(" 할    인 : " + A.GetNumericString(DiscountAmt).PadLeft(30, ' ') + "\n"); // A.GetNumericString(summaryValue).PadLeft(30, ' ') + "\n");
                    sb.Append(" 소    계 : " + A.GetNumericString(summaryValue).PadLeft(30, ' ') + "\n");
                }
                sb.Append("==========================================\n");
                sb.Append(" 미 수 액 : " + txtPreNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 받을금액 : " + txtReceiveAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("------------------------------------------\n");
                sb.Append(" 입 금 액 : " + txtPayAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 총미수액 : " + txtTotalNonAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("==========================================\n");

                //비고 가져오기
                sb.Append(dtStore.Rows[0]["NO_ACCOUNT1"]);
                sb.Append(PrinterCommand.NewLine);
                sb.Append(dtStore.Rows[0]["NO_ACCOUNT2"]);
                sb.Append(PrinterCommand.NewLine);

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5));
                sb.Append(PrinterCommand.Cut);
                PrinterCommand.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ////수금영수증출력
        private void PrintNonpaidReceit()
        {
            try
            {
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
                sb.Append("외 상  수 금\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.AlignRight);
                sb.Append("재발행\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));
                sb.Append("고  객  명 : " + gridViewList.GetFocusedRowCellValue("CST_NM") + "\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("상      호 : " + POSGlobal.StoreName + "\n");
                sb.Append("사업자번호 : " + dtStore.Rows[0]["NO_BIZ"] + "\n");
                sb.Append("대  표  자 : " + dtStore.Rows[0]["NM_CEO"] + "\n");
                sb.Append("주      소 : " + dtStore.Rows[0]["DC_ADDR1"] + " " + dtStore.Rows[0]["DC_ADDR2"] + "\n");
                sb.Append("TEL : " + dtStore.Rows[0]["NO_TEL1"] + " / " + dtStore.Rows[0]["NO_TEL2"] + "\n");
                sb.Append("FAX : " + dtStore.Rows[0]["NO_FAX1"] + "\n");
                sb.Append("담  당  자 : " + POSGlobal.EmpName + "\n");
                sb.Append("거래  번호 : " + gridViewList.GetFocusedRowCellValue("RCPT_NO") + "\n");
                sb.Append("판매  시간 : " + gridViewList.GetFocusedRowCellValue("SALE_DT") + " " + gridViewList.GetFocusedRowCellValue("SALE_TM") + "\n"); ;
                sb.Append("==========================================\n");
                sb.Append(" 미 수 액 : " + txtPreNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 받을금액 : " + txtReceiveAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("------------------------------------------\n");
                sb.Append(" 입 금 액 : " + txtPayAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 총미수액 : " + txtTotalNonAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("==========================================\n");

                sb.Append(PrinterCommand.NewLine);
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5));
                sb.Append(PrinterCommand.Cut);
                PrinterCommand.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //작업지시서
        private void PrintWorkSheet()
        {
            try
            {
                if (gridViewMain.RowCount == 0) return;

                string ItemName = string.Empty;
                string UnitAmount = string.Empty;
                string ItemDescrip = string.Empty;
                string Amount = string.Empty;
                string QtyBundle;

                int padLen = 0;

                object rtnCarNo = "";//= DBHelper.ExecuteScalar("SELECT NO_CAR FROM POS_CUST WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND CD_CUST = '" + CustomerCode + "'");

                StringBuilder sb = new StringBuilder();

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.AlignCenter);
                sb.Append(PrinterCommand.ConvertFontSize(2, 2));
                sb.Append("작  업  서\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));
                sb.Append("고  객  명 : " + gridViewList.GetFocusedRowCellValue("CST_NM") + "\n");
                //sb.Append("차    량  : " + A.GetString(rtnCarNo) + "\n");
                sb.Append(PrinterCommand.InitializePrinter);

                sb.Append("판 매 일  : " + gridViewList.GetFocusedRowCellValue("SALE_DT") + "\n");
                sb.Append("판매시간  : " + gridViewList.GetFocusedRowCellValue("SALE_TM") + "\n"); ;
                sb.Append("==========================================\n");
                sb.Append(" No.       상품명            수량/단위    \n");
                sb.Append("------------------------------------------\n");

                string str1 = string.Empty;
                string str2 = string.Empty;
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));

                for (int i = 0; i < gridViewMain.RowCount; i++)
                {

                    ItemName = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["DISP_NM"]));
                    ItemDescrip = A.GetString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["DC_ITEM"]));
                    QtyBundle = A.GetNumericString(gridViewMain.GetRowCellValue(i, gridViewMain.Columns["APP_AMT"]));
                    padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;


                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    str1 = " " + (i + 1).ToString().PadLeft(3, ' ') + " " + ItemName;
                    str2 = ItemDescrip;
                    sb.Append(str1 + "".PadLeft(42 - (Encoding.Default.GetBytes(str1).Length + Encoding.Default.GetBytes(str2).Length), '.') + str2);
                    sb.Append(PrinterCommand.NewLine);
                }

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5));
                sb.Append(PrinterCommand.Cut);
                PrinterCommand.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        enum ReceitMode
        {
            Single,
            Double,
            Etc
        }
    }
}
