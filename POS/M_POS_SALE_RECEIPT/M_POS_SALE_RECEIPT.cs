using Bifrost;
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_SALE_RECEIPT : POSFormBase
    {
        #region 변수모음


        //메인 데이터테이블
        DataTable _dtH = new DataTable();
        DataTable _dtL = new DataTable();
        DataTable _dtPay = new DataTable();

        bool IsFirst { get; set; } = true;

        string SlipFlag { get; set; } = "A";

        // 팝업창 위치 고정
        Point PopupPoint = new Point(100, 100);


        //폰트 크기
        static float fontSizeMain = 17F;

        //품목 폰트 타입
        static Font FontMain = new Font("카이겐고딕 KR Regular", fontSizeMain);//, GraphicsUnit.Pixel, 0);
        #endregion

        public M_POS_SALE_RECEIPT()
        {
            InitializeComponent();
            InitEvent();

            //패널 순서
            panelPayInfo.SendToBack();


            gridViewItem.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            gridViewItem.Appearance.Row.ForeColor = Color.Empty;
            //gridViewItem.Appearance.Row.Options.UseForeColor = true;
            //CustomDrawRowIndicator(gridItem, gridViewItem);
        }

        private void InitEvent()
        {
            //마우스 커서 숨기기
            //ShowCursor(false);
            Load += FormLoad;

            btnCtrClose.Click += BtnCtrClose_Click;                         //종료
            btnCtrPrint.Click += BtnCtrPrint_Click;                            //영수증 재출력
            btnSearchList.Click += BtnSearchList_Click;


            btnDatePre.Click += BtnDatePre_Click;
            btnDateNext.Click += BtnDateNext_Click;

            #region 그리드 조작 버튼
            btnGridUp.Click += BtnGridUp_Click;
            btnGridUpMax.Click += BtnGridUpMax_Click;
            btnGridDown.Click += BtnGridDown_Click;
            btnGridDownMax.Click += BtnGridDownMax_Click;
            #endregion 그리드 조작 버튼


            #region Grid 관련 
            gridViewList.FocusedRowChanged += GridViewList_FocusedRowChanged;

            gridViewItem.RowCountChanged += GridViewItem_RowCountChanged;
            gridViewItem.OptionsView.ShowGroupPanel = false;
            //gridViewItem.OptionsView.ColumnAutoWidth = false;
            gridViewItem.OptionsView.ShowAutoFilterRow = false;
            gridViewItem.OptionsCustomization.AllowSort = true;
            gridViewItem.OptionsCustomization.AllowFilter = false;

            //그리드 금액 합계 표기
            gridViewItem.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            //gridViewItem.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "AM", "{0:#,###}");


            gridViewItem.Columns["AM_VAT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_VAT"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

            //그리드 높이
            gridViewItem.UserCellPadding = new Padding(0, 3, 0, 3);
            #endregion Grid 관련 

            //panelKeypadItem.Dock = DockStyle.Fill;// = panelPayInfo.Size;

            dtpSearch.DateTimeChanged += DtpSearch_DateTimeChanged;

        }

        private void BtnDateNext_Click(object sender, EventArgs e)
        {
            dtpSearch.DateTime = dtpSearch.DateTime.AddDays(1);
        }

        private void BtnDatePre_Click(object sender, EventArgs e)
        {
            dtpSearch.DateTime = dtpSearch.DateTime.AddDays(-1);
        }

        private void DtpSearch_DateTimeChanged(object sender, EventArgs e)
        {
            OnView();
        }

        private void BtnSearchList_Click(object sender, EventArgs e)
        {
            OnView();
        }

        private void GridViewList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SlipNo = A.GetString(gridViewList.GetFocusedRowCellValue("NO_SO"));
            Search(SlipNo);
        }



        private bool IsWorkCancelQuestion { get; set; }


        private string SlipNo { get; set; } = string.Empty;

        private InitButtonMode InitBtnMode { get; set; } = InitButtonMode.Normal;

        private void FormActivated(object sender, EventArgs e)
        {
            InitConfig();
        }

        private void InitConfig()
        {
            //좌우 바뀜 디폴트는 L임
            POSConfig configScreen = POSConfigHelper.GetConfig("POS001");

            if (configScreen.ConfigValue == "R")
            {
                splitContainerMain.SplitterPosition = splitContainerMain.Panel2.Width;
                splitContainerMain.SwapPanels();
            }

            //부가세 설정 화면 보이기 디폴트는 N임
            POSConfig configVatYN = POSConfigHelper.GetConfig("POS004");

            if (A.GetString(configVatYN.ConfigValue) == "Y")
            {
                panelSaleVatAmt.Visible = true;
                //20191206 합계는 나오게
                //panelSaleSumAmt.Visible = true;
            }
            else
            {
                panelSaleVatAmt.Visible = false;
                //20191206 합계는 나오게
                //panelSaleSumAmt.Visible = false;
            }


            //매출액 그리드에 있어서 일단 숨김
            panelSaleAmt.Visible = false;

            //작업취소시 확인여부 기본은 N
            POSConfig configWorkCancelYN = POSConfigHelper.GetConfig("POS007");

            if (A.GetString(configWorkCancelYN.ConfigValue) == "Y")
            {
                IsWorkCancelQuestion = true;
            }
            else
            {
                IsWorkCancelQuestion = false;
            }

            //초기화버튼 설정 POS014 C : 거래처 / N :원래대로 선택되어있는 
            POSConfig configInitBtn = POSConfigHelper.GetConfig("POS014");
            InitBtnMode = configInitBtn.ConfigValue == "C" ? InitButtonMode.Customer : InitButtonMode.Normal;

            //글꼴 크기
            Bifrost.Helper.POSConfig cfgFontSize = POSConfigHelper.GetConfig("POS015");

            fontSizeMain = A.GetFloat(cfgFontSize.ConfigValue);

            FontMain = new Font("카이겐고딕 KR Regular", fontSizeMain);

            #region 메인 키패드 폰트 사이즈

            lblSaleInfoPreAmt.Font = FontMain;
            lblSaleInfoVat.Font = FontMain;
            lblSaleInfoSaleAmt.Font = FontMain;
            lblSaleInfoDiscountAmt.Font = FontMain;
            lblSaleInfoGroupSumAmt.Font = FontMain;
            lblSaleInfoTotalAmt.Font = FontMain;
            lblInputAmt.Font = FontMain;
            labelControl13.Font = FontMain;
            lblCreditCardAmt.Font = FontMain;
            labelControl14.Font = FontMain;
            lblTotalNonpaidAmt.Font = FontMain;
            labelControl15.Font = FontMain;

          
            btnCtrPrint.Font = FontMain;
            btnCtrClose.Font = FontMain;
            #endregion 메인 키패드 폰트 사이즈


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

        }

        ReceitMode ReceitType { get; set; } = ReceitMode.Double;

        private void FormLoad(object sender, EventArgs e)
        {
            InitForm();

            InitConfig();
            Activated += FormActivated;

            OnView();
        }



        #region 우측하단 컨트롤패널버튼 이벤트 모음
        private void BtnCtrClose_Click(object sender, EventArgs e)
        {
            //OnHomeClick();

            //if (ShowMessageBoxA("판매등록을 닫으시겠습니까?", MessageType.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void BtnCtrPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (SlipNo == string.Empty)
                {
                    ShowMessageBoxA("조회된 거래건이 없습니다.\n먼저 이전거래를 조회하세요.", MessageType.Information);
                    return;
                }

                Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
                string PrintType = cfgPrint.ConfigValue;

                // 거래명세서 종이 설정
                Bifrost.Helper.POSConfig cfgPrintPaper = POSConfigHelper.GetConfig("PRT011");
                string PrtPaperType = cfgPrintPaper.ConfigValue;

                string result = string.Empty;
               

                switch (SlipFlag)
                {
                    case "A"://정상 판매
                        P_POS_PRINT_SALE P_POS_PRINT_SALE = new P_POS_PRINT_SALE();
                        if (P_POS_PRINT_SALE.ShowDialog() == DialogResult.OK)
                        {
                            result = P_POS_PRINT_SALE.ResultPrint;

                            switch (result)
                            {
                                case "A": //영수증 작업서
                                    PrintReceit();
                                    Thread.Sleep(500);
                                    PrintWorkSheet();
                                    break;
                                case "R": //영수증
                                    PrintReceit();
                                    break;
                                case "W": //작업서
                                    PrintWorkSheet();
                                    break;
                                case "I": //거래명세        
                                    if (PrtPaperType == "A5")
                                        POSPrintHelper.POSReportPrint("POS_SALE_RPT01", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                    else if (PrtPaperType == "A4")
                                        POSPrintHelper.POSReportPrint("POS_SALE_RPT02", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                    break;
                                case "N": //미발행
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case "P"://수금만
                        P_POS_PRINT P_POS_PRINT = new P_POS_PRINT();
                        P_POS_PRINT.StartPosition = FormStartPosition.Manual;
                        P_POS_PRINT.Location = this.PointToScreen(new Point(this.Size.Width / 2 - P_POS_PRINT.Size.Width / 2, this.Size.Height / 2 - P_POS_PRINT.Size.Height / 2));

                        switch (PrintType)
                        {
                            case "A": //둘다
                                
                                P_POS_PRINT.PrintText = new string[] { "입금표\n(일반)", "입금표\n(감열지)" };
                                P_POS_PRINT.PrintTag = new string[] { "P", "T" };

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "T":
                                            PrintNonpaidReceit();
                                            break;
                                        case "P":
                                            if (PrtPaperType == "A5")
                                                POSPrintHelper.POSReportPrint("POS_SALE_RPT01", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                            else if (PrtPaperType == "A4")
                                                POSPrintHelper.POSReportPrint("POS_SALE_RPT02", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                            break;
                                    }
                                }

                                break;
                            case "T": //감열지
                                P_POS_PRINT.PrintText = new string[] { "입금표\n(감열지)" };
                                P_POS_PRINT.PrintTag = new string[] { "T" };

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "T":
                                            PrintNonpaidReceit();
                                            break;
                                    }
                                }
                                break;
                            case "P": //일반
                                P_POS_PRINT.PrintText = new string[] { "입금표\n(일반)" };
                                P_POS_PRINT.PrintTag = new string[] { "P" };

                                if (P_POS_PRINT.ShowDialog() == DialogResult.OK)
                                {
                                    result = P_POS_PRINT.ResultPrint;

                                    switch (result)
                                    {
                                        case "P":
                                            if (PrtPaperType == "A5")
                                                POSPrintHelper.POSReportPrint("POS_SALE_RPT01", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                            else if (PrtPaperType == "A4")
                                                POSPrintHelper.POSReportPrint("POS_SALE_RPT02", new string[] { "CD_STORE", "NO_SO" }, new string[] { POSGlobal.StoreCode, SlipNo });
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
                    case "T"://임시
                        P_POS_PRINT_WORKSHEET P_POS_PRINT_WORKSHEET = new P_POS_PRINT_WORKSHEET();
                        if (P_POS_PRINT_WORKSHEET.ShowDialog() == DialogResult.OK)
                        {
                            result = P_POS_PRINT_WORKSHEET.ResultPrint;

                            switch (result)
                            {
                                case "W": //작업서
                                    PrintWorkSheet();
                                    break;
                                case "N": //미발행
                                    break;
                                default:
                                    break;
                            }
                        }
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


        #endregion 우측하단 컨트롤패널버튼 이벤트 모음

        #region 그리드 하단 조작 업다운 버튼 이벤트
        private void BtnGridDownMax_Click(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount > 0)
                gridViewItem.FocusedRowHandle = gridViewItem.RowCount - 1;
        }

        private void BtnGridUpMax_Click(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount == 0)
                return;

            gridViewItem.FocusedRowHandle = 0;
        }

        private void BtnGridDown_Click(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount == 0)
                return;

            int index = (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle + (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo.Count - 1;
            if (index >= 0)
            {
                gridViewItem.FocusedRowHandle = index;
                gridViewItem.TopRowIndex = index;
            }
            else
                gridViewItem.FocusedRowHandle = 0;
        }

        private void BtnGridUp_Click(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount == 0)
                return;

            int index = (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle - (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo.Count;
            if (index >= 0)
                gridViewItem.FocusedRowHandle = index;
            else
                gridViewItem.FocusedRowHandle = 0;
        }
        #endregion 그리드 하단 조작 업다운 버튼 이벤트

        #region 그리드 관련 이벤트
        private void GridViewItem_RowCountChanged(object sender, EventArgs e)
        {
            if (gridViewItem.RowCount == 0) return;


            //int index = (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo[0].RowHandle + (gridViewItem.GetViewInfo() as GridViewInfo).RowsInfo.Count;
            if (gridViewItem.RowCount > 18)
                panelGridNav.Visible = true;
            else
                panelGridNav.Visible = false;
        }

        #endregion 그리드 관련 이벤트
        
        bool IsItemTypeFix = false;


        private void SetGridFooter()
        {
            decimal PreNonPaidAmt = A.GetDecimal(txtPreNonPaidAmt.EditValue);       //외상잔액
            decimal ReceiveAmt = A.GetDecimal(txtReceiveAmt.EditValue);             //받을금액
            decimal DiscountAmt = A.GetDecimal(txtDiscountAmt.EditValue);             //할인액


            decimal SaleAmt = A.GetDecimal(gridViewItem.Columns["AM"].SummaryItem.SummaryValue);
            decimal VatAmt = A.GetDecimal(gridViewItem.Columns["AM_VAT"].SummaryItem.SummaryValue);

            txtPayAmt.EditValue = SaleAmt;
            txtVatAmt.EditValue = VatAmt;

            txtPaySumAmt.EditValue = SaleAmt + VatAmt - DiscountAmt;
            txtReceiveAmt.EditValue = SaleAmt + VatAmt + PreNonPaidAmt - DiscountAmt;

        }




        private void InitForm()
        {
            CH.SetDateEditFont(dtpSearch);
            dtpSearch.Text = POSGlobal.SaleDt;
        }

        #region 오버라이드 메서드
        public override void OnView()
        {
            try
            {
                DataTable dtList = SearchSalesOrderList(new object[] { POSGlobal.StoreCode, dtpSearch.Text });

                gridList.Binding(dtList, true);
                GridViewList_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        #endregion 오버라이드 메서드

        DataTable _dtHInit;
        DataTable _dtLInit;
        DataTable _dtPayInit;

        private void Search(string pSlipNo)
        {
            string[] obj = new string[] { POSGlobal.StoreCode, pSlipNo };
            DataSet ds;

            //최초 조회시 빈 데이터를 넣어둠
            if (IsFirst)
            {
                ds = SearchSalesOrder(obj);

                _dtH = _dtHInit = ds.Tables[0];
                _dtL = _dtLInit = ds.Tables[1];
                _dtPay = _dtPayInit = ds.Tables[2];
            }
            else
            {
                // 재조회시 더미일때는 빈 데이터 셋 넣음
                if (pSlipNo == A.GetDummyString)
                {
                    _dtH = _dtHInit;
                    _dtL = _dtLInit;
                    _dtPay = _dtPayInit;
                }
                else
                {
                    ds = SearchSalesOrder(obj);

                    _dtH = ds.Tables[0];
                    _dtL = ds.Tables[1];
                    _dtPay = ds.Tables[2];
                }
            }

            if (_dtH.Rows.Count == 1)
            {
                SlipFlag = A.GetString(_dtH.Rows[0]["FG_SO"]);
                txtDiscountAmt.EditValue = A.GetDecimal(_dtH.Rows[0]["AM_DISCOUNT"]);

                //결제정보 테이블에 데이터가 있을때만 조회
                if (_dtPay.Rows.Count == 1)
                {
                    txtClsPayAmt.EditValue = A.GetDecimal(_dtPay.Rows[0]["AM_PAY"]);
                    txtClsCreditAmt.EditValue = A.GetDecimal(_dtPay.Rows[0]["AM_CREDIT"]);
                }
                else
                {
                    txtClsPayAmt.EditValue = 0;
                    txtClsCreditAmt.EditValue = 0;
                }
            }

            if (SlipFlag == "P")
            {
                panelPaid.Visible = true;
            }
            else
            {
                panelPaid.Visible = false;
            }

            decimal NonpaidAmt = A.GetDecimal(DBHelper.ExecuteScalar("USP_POS_GET_CUST_NONPAID_AMT", new object[] { POSGlobal.StoreCode, gridViewList.GetFocusedRowCellValue("CD_CUST"), pSlipNo }));
            txtPreNonPaidAmt.EditValue = NonpaidAmt;


            gridItem.Binding(_dtL, true);
            SetGridFooter();

            //총외상잔액
            //결제정보 테이블에 데이터가 있을때만 조회
            if (_dtPay.Rows.Count == 1)
                txtTotalNonPaidAmt.EditValue = A.GetDecimal(txtReceiveAmt.EditValue) - (A.GetDecimal(txtClsPayAmt.EditValue) + A.GetDecimal(txtClsCreditAmt.EditValue));
            else
                txtTotalNonPaidAmt.EditValue = 0;

        }

        #region ENUM 모음
        public enum JobMode
        {
            New,
            Modify,
            Read,
            NewAfterRead,
        }

        private enum PayMode
        {
            None,
            Paid
        }

        private enum InitButtonMode
        {
            Customer,
            Normal
        }

        enum ReceitMode
        {
            Single,
            Double,
            Etc
        }


        #endregion ENUM 모음
    }
}
