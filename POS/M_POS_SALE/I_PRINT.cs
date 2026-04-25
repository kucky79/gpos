using System;
using System.Data;
using System.Text;

using Bifrost;
using Bifrost.Helper;

namespace POS
{
    partial class M_POS_SALE
    {
        /// <summary>
        /// 영수증출력(수량/단가/금액)
        /// </summary>
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

                for (int j = 0; j < RptReceitCount; j++)
                {
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
                    if (RptSaleTime == "T")
                    {
                        sb.Append("판매  시간 : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + " " + A.GetString(_dtH.Rows[0]["TM_REG"]) + "\n"); ;
                    }
                    else if (RptSaleTime == "D")
                    {
                        sb.Append("판  매  일 : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + "\n");
                    }
                    sb.Append("==========================================\n");
                    sb.Append(" No.    상품명     수량/단위      금  액  \n");
                    sb.Append("------------------------------------------\n");

                    if (ReceitType == ReceitMode.Double)
                    {
                        for (int i = 0; i < gridViewItem.RowCount; i++)
                        {
                            ItemName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_ITEM"]));
                            UnitAmount = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["UM"]));
                            //20200413 묶음이 10일경우 * 1 문자열 치환하면 사라져서 묶음이 1일경우만 날리게 수정
                            QtyBundle = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["QT"]));
                            ItemDescrip = QtyBundle == 1 ? A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"])).Replace(" * 1", "") : A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"]));

                            Amount = A.GetNumericString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["AM"]));
                            padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                            sb.Append(PrinterCommand.AlignLeft);
                            //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                            sb.Append((i + 1).ToString().PadLeft(RptReceitNo, ' ') + " " + ItemName);
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

                            string FirstGroup = (i + 1).ToString().PadLeft(RptReceitNo, ' ') + " " + ItemName;

                            sb.Append(FirstGroup);
                            //단가
                            //sb.Append(UnitAmount.PadLeft(17, ' '));
                            //수량 단위
                            padLen = 31 - (Encoding.Default.GetBytes(FirstGroup).Length + Encoding.Default.GetBytes(ItemDescrip).Length);

                            string SecondGroup;

                            if (padLen < 0)
                            {
                                sb.Append(PrinterCommand.NewLine);
                                SecondGroup = ItemDescrip.PadLeft(31, ' ');
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
                            sb.Append(Amount.PadLeft(11, ' '));
                            sb.Append(PrinterCommand.NewLine);
                        }
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
                    sb.Append(dtStore.Rows[0]["NO_ACCOUNT1"]);
                    sb.Append(PrinterCommand.NewLine);
                    sb.Append(dtStore.Rows[0]["NO_ACCOUNT2"]);
                    sb.Append(PrinterCommand.NewLine);

                    sb.Append(PrinterCommand.InitializePrinter);
                    sb.Append(PrinterCommand.LineFeed(5 + RptReceitSpace));
                    sb.Append(PrinterCommand.Cut);
                    PrinterCommand.Print(PrintPort, sb.ToString());
                    sb.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //수금영수증출력
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
                if (RptSaleTime == "T")
                {
                    sb.Append("수금  시간 : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + " " + A.GetString(_dtH.Rows[0]["TM_REG"]) + "\n"); ;
                }
                else if (RptSaleTime == "D")
                {
                    sb.Append("수  금  일 : " + dtpSale.DateTime.ToString("yyyy-MM-dd") + "\n");
                }
                sb.Append("==========================================\n");
                sb.Append(" 전미수액 : " + txtPreNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 입 금 액 : " + txtClsPayAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append(" 신용카드 : " + txtClsCreditAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("------------------------------------------\n");
                sb.Append(" 총미수액 : " + txtTotalNonPaidAmt.Text.PadLeft(30, ' ') + "\n");
                sb.Append("==========================================\n");

                sb.Append(PrinterCommand.NewLine);
                sb.Append(PrinterCommand.InitializePrinter);
                sb.Append(PrinterCommand.LineFeed(5 + RptReceitSpace));
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
                string ItemName = string.Empty;
                string UnitAmount = string.Empty;
                string ItemDescrip = string.Empty;
                string Amount = string.Empty;
                decimal QtyBundle;
                decimal QtyUnit;
                string NewItem = string.Empty;

                int padLen = 0;

                object rtnCarNo = DBHelper.ExecuteScalar("SELECT NO_CAR FROM POS_CUST WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND CD_CUST = '" + CustomerCode + "'");

                object rtnTime = DBHelper.ExecuteScalar("SELECT CONVERT(NVARCHAR(8), TM_REG, 8) FROM POS_SOH WHERE CD_STORE = '" + POSGlobal.StoreCode + "' AND NO_SO = '" + SlipNo + "'");

                for (int j = 0; j < RptWorkSheetCount; j++)
                {
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
                    if (SlipFlag != "T")
                    {
                        sb.Append("판매시간  : " + rtnTime + "\n"); ;
                    }
                    sb.Append("==========================================\n");
                    sb.Append(" No.       상품명            수량/단위    \n");
                    sb.Append("------------------------------------------\n");

                    string str1 = string.Empty;
                    string str2 = string.Empty;
                    sb.Append(PrinterCommand.ConvertFontSize(1, 2));

                    int RowCnt = 0;
                    //20200702 거래처별 아이템 갯수 가져오기

                    int ItemCnt = A.GetInt(DBHelper.ExecuteScalar("USP_POS_GET_TMP_CUST_CNT", new object[] { POSGlobal.StoreCode, dtpSale.Text, CustomerCode, "S" }));
                    ItemCnt = ItemCnt == 0 ? 0 : ItemCnt - (gridViewItem.RowCount);


                    for (int i = 0; i < gridViewItem.RowCount; i++)
                    {

                        NewItem = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["FG_LAST"])) == "NEW" ? "★" : string.Empty;
                        ItemName = A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["NM_ITEM"]));

                        //20200413 묶음이 10일경우 * 1 문자열 치환하면 사라져서 묶음이 1일경우만 날리게 수정
                        QtyBundle = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["QT"]));
                        QtyUnit = A.GetDecimal(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["QT_UNIT"]));
                        ItemDescrip = QtyBundle == 1 ? A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"])).Replace(" * 1", "") : A.GetString(gridViewItem.GetRowCellValue(i, gridViewItem.Columns["DC_ITEM"]));

                        //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                        
                        //수량이 0보다 작고 작업서 설정에 따라 뺌
                        if (QtyUnit < 0 && !RptWorkSheetMinus)
                        {
                            //sb.Append(PrinterCommand.AlignLeft);
                            ////sb.Append(" No.    상품명     수량/단위    금  액    \n");
                            //str1 = " " + (i + 1).ToString().PadLeft(3, ' ') + " " + ItemName;
                            //str2 = ItemDescrip;
                            //sb.Append(str1 + "".PadLeft(42 - (Encoding.Default.GetBytes(str1).Length + Encoding.Default.GetBytes(str2).Length), '.') + str2);
                            //sb.Append(PrinterCommand.NewLine);
                        }
                        //일반적인 케이스는 전부 출력
                        else
                        {
                            //임시전표
                            if (SlipFlag == "T")
                            {
                                if (TmpPrintNewItem == "N") //신규품목만
                                {
                                    if (NewItem != string.Empty)
                                    {
                                        
                                        sb.Append(PrinterCommand.AlignLeft);
                                        //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                                        str1 = (ItemCnt + 1).ToString().PadLeft(RptReceitNo, ' ') + " " + ItemName;
                                        str2 = ItemDescrip;
                                        sb.Append(str1 + "".PadLeft(42 - (Encoding.Default.GetBytes(str1).Length + Encoding.Default.GetBytes(str2).Length), '.') + str2);
                                        sb.Append(PrinterCommand.NewLine);
                                        ItemCnt++;
                                        RowCnt++;
                                    }
                                }
                                else if (TmpPrintNewItem == "A") //전체
                                {
                                    sb.Append(PrinterCommand.AlignLeft);
                                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                                    str1 = NewItem + (i + 1).ToString().PadLeft(RptReceitNo, ' ') + " " + ItemName;
                                    str2 = ItemDescrip;
                                    sb.Append(str1 + "".PadLeft(42 - (Encoding.Default.GetBytes(str1).Length + Encoding.Default.GetBytes(str2).Length), '.') + str2);
                                    sb.Append(PrinterCommand.NewLine);
                                    RowCnt++;
                                }
                            }
                            else//정상전표
                            {
                                sb.Append(PrinterCommand.AlignLeft);
                                //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                                str1 = NewItem + (i + 1).ToString().PadLeft(RptReceitNo, ' ') + " " + ItemName;
                                str2 = ItemDescrip;
                                sb.Append(str1 + "".PadLeft(42 - (Encoding.Default.GetBytes(str1).Length + Encoding.Default.GetBytes(str2).Length), '.') + str2);
                                sb.Append(PrinterCommand.NewLine);
                                RowCnt++;
                            }
                        }
                    }

                    if (RowCnt > 0)
                    {
                        sb.Append(PrinterCommand.InitializePrinter);
                        sb.Append(PrinterCommand.LineFeed(5 + RptWorkSheetSpace));
                        sb.Append(PrinterCommand.Cut);
                        PrinterCommand.Print(PrintPort, sb.ToString());
                        sb.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
