using System;
using System.Data;
using System.Text;

using Bifrost;
using Bifrost.Helper;

namespace POS
{
    partial class M_POS_SALE_OLD
    {
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
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));
                sb.Append("고  객  명 : " + txtCust.Text + "\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("상      호 : " + POSGlobal.StoreName + "\n");
                sb.Append("사업자번호 : " + dtStore.Rows[0]["NO_BIZ"] + "\n");
                sb.Append("대  표  자 : " + dtStore.Rows[0]["NM_CEO"] + "\n");
                sb.Append("주      소 : " + dtStore.Rows[0]["DC_ADDR1"] + " " + dtStore.Rows[0]["DC_ADDR2"] + "\n");
                sb.Append("TEL : " + dtStore.Rows[0]["NO_TEL1"] + " / " + dtStore.Rows[0]["NO_TEL2"] + "\n");
                sb.Append("FAX : " + dtStore.Rows[0]["NO_FAX1"] + "\n");
                sb.Append("담  당  자 : " + POSGlobal.EmpName + "\n");
                sb.Append("거래  번호 : " + SlipNo + "\n");
                sb.Append("판매  시간 : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + "\n"); ;
                sb.Append("==========================================\n");
                sb.Append(" No.    상품명     수량/단위    금  액    \n");
                sb.Append("------------------------------------------\n");

                for (int i = 0; i < gridViewItem.RowCount; i++)
                {
                    ItemName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_ITEM"]));
                    UnitAmount = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["UM"]));
                    
                    //20200413 묶음이 10일경우 * 1 문자열 치환하면 사라져서 묶음이 1일경우만 날리게 수정
                    QtyBundle = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["QT"]));
                    ItemDescrip = QtyBundle == 1 ? A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"])).Replace(" * 1", "") : A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"]));
                    
                    Amount = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM"]));

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
                sb.Append("==========================================\n");
                sb.Append(" 매 출 액 : " + txtPayAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 할    인 : " + txtDiscountAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 합    계 : " + txtPaySumAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("==========================================\n");
                sb.Append(" 전미수액 : " + txtPreNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 총 금 액 : " + txtReceiveAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("------------------------------------------\n");
                sb.Append(" 입 금 액 : " + txtClsPayAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 신용카드 : " + txtClsCreditAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 총미수액 : " + txtTotalNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("------------------------------------------\n");
                sb.Append("------------------------------------------\n");

                //비고 가져오기
                sb.Append(A.GetString(DBHelper.ExecuteScalar("SELECT NO_ACCOUNT1 FROM POS_STORE WHERE CD_STORE = '" + POSGlobal.StoreCode + "'")));
                sb.Append(PrinterCommand.NewLine);
                sb.Append(A.GetString(DBHelper.ExecuteScalar("SELECT NO_ACCOUNT2 FROM POS_STORE WHERE CD_STORE = '" + POSGlobal.StoreCode + "'")));
                sb.Append(PrinterCommand.NewLine);

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5));
                sb.Append(PrinterCommand.Cut);
                PRINT_LPT.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));
                sb.Append("고 객 명  : " + txtCust.Text + "\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("상    호  : " + POSGlobal.StoreName + "\n");
                sb.Append("사업자번호 : " + dtStore.Rows[0]["NO_BIZ"] + "\n");
                sb.Append("대  표  자 : " + dtStore.Rows[0]["NM_CEO"] + "\n");
                sb.Append("주      소 : " + dtStore.Rows[0]["DC_ADDR1"] + " " + dtStore.Rows[0]["DC_ADDR2"] + "\n");
                sb.Append("TEL : " + dtStore.Rows[0]["NO_TEL1"] + " / " + dtStore.Rows[0]["NO_TEL2"] + "\n");
                sb.Append("FAX : " + dtStore.Rows[0]["NO_FAX1"] + "\n");
                sb.Append("담 당 자  : " + POSGlobal.EmpName + "\n");
                sb.Append("거래 번호 : " + SlipNo + "\n");
                sb.Append("판매 시간 : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss") + "\n"); ;
                sb.Append("==========================================\n");
                sb.Append(" 전미수액 : " + txtPreNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 입 금 액 : " + txtClsPayAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 신용카드 : " + txtClsCreditAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("------------------------------------------\n");
                sb.Append(" 총미수액 : " + txtTotalNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("==========================================\n");

                sb.Append(PrinterCommand.NewLine);
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5));
                sb.Append(PrinterCommand.Cut);
                PRINT_LPT.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrintWorkSheet()
        {
            try
            {
                string ItemName = string.Empty;
                string UnitAmount = string.Empty;
                string ItemDescrip = string.Empty;
                string Amount = string.Empty;
                decimal QtyBundle;

                int padLen = 0;

                object rtnCarNo = DBHelper.ExecuteScalar("SELECT NO_CAR FROM POS_CUST WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND CD_CUST = '" + CustomerCode + "'");

                object rtnTime = DBHelper.ExecuteScalar("SELECT CONVERT(NVARCHAR(8), TM_REG, 8) FROM POS_SOH WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND NO_SO = '" + SlipNo + "'");

                StringBuilder sb = new StringBuilder();

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.AlignCenter);
                sb.Append(PrinterCommand.ConvertFontSize(2, 2));
                sb.Append("작  업  서\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));
                sb.Append("고 객 명  : " + txtCust.Text + "\n");
                sb.Append("차    량  : " + A.GetString(rtnCarNo) + "\n");
                sb.Append(PrinterCommand.InitializePrinter);

                sb.Append("판 매 일  : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + "\n");
                sb.Append("판매시간  : " + rtnTime + "\n"); ;
                sb.Append("==========================================\n");
                sb.Append(" No.       상품명            수량/단위    \n");
                sb.Append("------------------------------------------\n");

                string str1 = string.Empty;
                string str2 = string.Empty;
                sb.Append(PrinterCommand.ConvertFontSize(1, 2));

                for (int i = 0; i < gridViewItem.RowCount; i++)
                {
                    
                    ItemName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_ITEM"]));

                    //20200413 묶음이 10일경우 * 1 문자열 치환하면 사라져서 묶음이 1일경우만 날리게 수정
                    QtyBundle = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["QT"]));
                    ItemDescrip = QtyBundle == 1 ? A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"])).Replace(" * 1", "") : A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"]));
                    
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    str1 = " " + (i + 1).ToString().PadLeft(3, ' ') + " " + ItemName;
                    str2 = ItemDescrip;

                    int itemLenth = 42 - (Encoding.Default.GetBytes(str1).Length + Encoding.Default.GetBytes(str2).Length);

                    if (itemLenth > 0)
                    {
                        sb.Append(str1 + "".PadLeft(itemLenth, '.') + str2);
                    }
                    else
                    {
                        sb.Append(str1);
                        sb.Append(PrinterCommand.NewLine);
                        sb.Append("".PadLeft(42 - Encoding.Default.GetBytes(str2).Length, '.') + str2);
                    }
                    sb.Append(PrinterCommand.NewLine);
                }

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5));
                sb.Append(PrinterCommand.Cut);
                PRINT_LPT.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
