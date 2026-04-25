using System;
using System.Data;
using System.Text;

using Bifrost;
using Bifrost.Helper;

namespace POS
{
    partial class M_POS_PURCHASE
    {
        private void PrintReceit()
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
                sb.Append(ConvertFontSize(2, 2));
                sb.Append("영  수  증\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(ConvertFontSize(1, 2));
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
                    ItemDescrip = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"])).Replace(" * 1", "");
                    Amount = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM"]));
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
                sb.Append(ConvertFontSize(2, 2));
                sb.Append("외 상  수 금\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(ConvertFontSize(1, 2));
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
                int padLen = 0;

                object rtnCarNo = DBHelper.ExecuteScalar("SELECT NO_CAR FROM POS_CUST WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND CD_CUST = '" + CustomerCode + "'");

                object rtnTime = DBHelper.ExecuteScalar("SELECT CONVERT(NVARCHAR(8), TM_REG, 8) FROM POS_SOH WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND NO_PO = '" + SlipNo + "'");

                StringBuilder sb = new StringBuilder();

                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.AlignCenter);
                sb.Append(ConvertFontSize(2, 2));
                sb.Append("작  업  서\n");
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append("\n");
                sb.Append("==========================================\n");
                sb.Append(ConvertFontSize(1, 2));
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
                sb.Append(ConvertFontSize(1, 2));

                for (int i = 0; i < gridViewItem.RowCount; i++)
                {
                    ItemName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_ITEM"]));
                    ItemDescrip = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"])).Replace(" * 1", "");
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
                PRINT_LPT.Print(PrintPort, sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// FONT 명령어의 글자사이즈 속성을 변환 합니다.
        /// </summary>
        /// <param name="width">가로</param>
        /// <param name="height">세로</param>
        /// <returns>가로 x 세로</returns>
        private string ConvertFontSize(int width, int height)
        {
            int _w, _h;

            //가로변환
            if (width == 1)
                _w = 0;
            else if (width == 2)
                _w = 16;
            else if (width == 3)
                _w = 32;
            else if (width == 4)
                _w = 48;
            else if (width == 5)
                _w = 64;
            else if (width == 6)
                _w = 80;
            else if (width == 7)
                _w = 96;
            else if (width == 8)
                _w = 112;
            else _w = 0;

            //세로변환
            if (height == 1)
                _h = 0;
            else if (height == 2)
                _h = 1;
            else if (height == 3)
                _h = 2;
            else if (height == 4)
                _h = 3;
            else if (height == 5)
                _h = 4;
            else if (height == 6)
                _h = 5;
            else if (height == 7)
                _h = 6;
            else if (height == 8)
                _h = 7;
            else _h = 0;

            //가로x세로
            int sum = _w + _h;
            string result = PrinterCommand.GS + "!" + PrinterCommand.DecimalToCharString(sum);

            return result;
        }
    }
}
