using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.XtraEditors;
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
    public partial class M_POS_MENU : POSFormBase
    {
        public M_POS_MENU()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        bool isUseOldPos = false;

        bool isUsePurchase = false;

        bool isUseInventory = false;

        //bool isUseSaleOld = false;


        private void InitForm()
        {
            CH.SetDateEditFont(dtpSale);

            //영업일자 가져오기
            Bifrost.Helper.POSConfig cfgSaleDt = POSConfigHelper.GetConfig("SYS002");
            //세팅이 없으면 다시 넣어줌
            if (cfgSaleDt == null)
            {
                cfgSaleDt = new Bifrost.Helper.POSConfig();
                cfgSaleDt.ConfigCode = "SYS002";
                cfgSaleDt.ConfigName = "영업일자";
                cfgSaleDt.ConfigValue = A.GetToday;
                cfgSaleDt.ConfigDescript = "영업일자를 설정합니다.";
                POSConfigHelper.SetConfig(cfgSaleDt, "SYS");
            }

            //엑셀 뷰어 사용여부
            Bifrost.Helper.POSConfig cfgExcelViewer = POSConfigHelper.GetConfig("SYS004");
            //세팅이 없으면 다시 넣어줌
            if (cfgExcelViewer == null)
            {
                cfgExcelViewer = new Bifrost.Helper.POSConfig();
                cfgExcelViewer.ConfigCode = "SYS004";
                cfgExcelViewer.ConfigName = "내장 엑셀뷰어 사용여부";
                cfgExcelViewer.ConfigValue = "N";
                cfgExcelViewer.ConfigDescript = "사용 : Y / 미사용 : N";
                POSConfigHelper.SetConfig(cfgExcelViewer, "SYS");
            }

            POSGlobal.SaleDt = cfgSaleDt.ConfigValue;
            dtpSale.Text = POSGlobal.SaleDt;
            MdiForm.SetSystemDate(dtpSale.Text);
            SetAutoSaleDt();

            //구버전 조회 세팅
            Bifrost.Helper.POSConfig cfgOldPOS= POSConfigHelper.GetConfig("POS009");
            //세팅이 없으면 다시 넣어줌
            if (cfgOldPOS == null)
            {
                cfgOldPOS = new Bifrost.Helper.POSConfig();
                cfgOldPOS.ConfigCode = "POS009";
                cfgOldPOS.ConfigName = "구버전 조회 사용 여부";
                cfgOldPOS.ConfigValue = "N";
                POSConfigHelper.SetConfig(cfgOldPOS, "POS");
            }

            isUseOldPos = cfgOldPOS.ConfigValue == "Y" ? true : false;
            tabOld.PageVisible = isUseOldPos;


            //터치 화면 설정(대분류 / 상품)
            Bifrost.Helper.POSConfig cfgScreen = POSConfigHelper.GetConfig("POS010");
            //세팅이 없으면 다시 넣어줌
            if (cfgScreen == null)
            {
                cfgScreen = new Bifrost.Helper.POSConfig();
                cfgScreen.ConfigCode = "POS010";
                cfgScreen.ConfigName = "터치 화면 설정(대분류 / 상품)";
                cfgScreen.ConfigValue = "C";
                cfgScreen.ConfigDescript = "I : 상품 /  C : 대분류";

                POSConfigHelper.SetConfig(cfgScreen, "POS");
            }

            //구매모듈 사용
            Bifrost.Helper.POSConfig cfgPurchase = POSConfigHelper.GetConfig("POS011");
            //세팅이 없으면 다시 넣어줌
            if (cfgPurchase == null)
            {
                cfgPurchase = new Bifrost.Helper.POSConfig();
                cfgPurchase.ConfigCode = "POS011";
                cfgPurchase.ConfigName = "구매모듈 사용여부";
                cfgPurchase.ConfigValue = "N";

                POSConfigHelper.SetConfig(cfgPurchase, "POS");
            }
            isUsePurchase = cfgPurchase.ConfigValue == "Y" ? true : false;
            tabPurchase.PageVisible = isUsePurchase;


            //재고모듈 사용
            Bifrost.Helper.POSConfig cfgInventory = POSConfigHelper.GetConfig("POS012");
            //세팅이 없으면 다시 넣어줌
            if (cfgInventory == null)
            {
                cfgInventory = new Bifrost.Helper.POSConfig();
                cfgInventory.ConfigCode = "POS012";
                cfgInventory.ConfigName = "재고모듈 사용여부";
                cfgInventory.ConfigValue = "N";

                POSConfigHelper.SetConfig(cfgInventory, "POS");
            }
            isUseInventory = cfgInventory.ConfigValue == "Y" ? true : false;
            tabInventory.PageVisible = isUseInventory;


            ////화면구성 구버전처럼
            //Bifrost.Helper.POSConfig cfgSaleOld = POSConfigHelper.GetConfig("POS013");
            ////세팅이 없으면 다시 넣어줌
            //if (cfgSaleOld == null)
            //{
            //    cfgSaleOld = new Bifrost.Helper.POSConfig();
            //    cfgSaleOld.ConfigCode = "POS013";
            //    cfgSaleOld.ConfigName = "판매등록 구버전 스타일";
            //    cfgSaleOld.ConfigValue = "N";

            //    POSConfigHelper.SetConfig(cfgSaleOld, "POS");
            //}
            //isUseSaleOld = cfgSaleOld.ConfigValue == "Y" ? true : false;

            //초기화버튼 설정 POS014 C : 거래처 / N :원래대로 선택되어있는 
            POSConfig cfgInitBtn = POSConfigHelper.GetConfig("POS014");
            
            if (cfgInitBtn == null)
            {
                cfgInitBtn = new Bifrost.Helper.POSConfig();
                cfgInitBtn.ConfigCode = "POS014";
                cfgInitBtn.ConfigName = "초기화 버튼 설정";
                cfgInitBtn.ConfigValue = "N";
                cfgInitBtn.ConfigDescript = "C : 거래처 / N :원래대로";
                POSConfigHelper.SetConfig(cfgInitBtn, "POS");
            }

            //글꼴 크기
            Bifrost.Helper.POSConfig cfgFontSize = POSConfigHelper.GetConfig("POS015");
            //세팅이 없으면 다시 넣어줌
            if (cfgFontSize == null)
            {
                cfgFontSize = new Bifrost.Helper.POSConfig();
                cfgFontSize.ConfigCode = "POS015";
                cfgFontSize.ConfigName = "판매등록 글꼴크기";
                cfgFontSize.ConfigValue = "16";
                cfgFontSize.ConfigDescript = "15~20 추천 기본 16";
                POSConfigHelper.SetConfig(cfgFontSize, "POS");
            }

            //출력물 설정
            Bifrost.Helper.POSConfig cfgPrintSequence = POSConfigHelper.GetConfig("PRT003");
            //세팅이 없으면 다시 넣어줌
            if (cfgPrintSequence == null)
            {
                cfgPrintSequence = new Bifrost.Helper.POSConfig();
                cfgPrintSequence.ConfigCode = "PRT003";
                cfgPrintSequence.ConfigName = "출력 순서 설정";
                cfgPrintSequence.ConfigValue = "R";
                cfgPrintSequence.ConfigDescript = "영수증먼저 : R/ 작업서먼저 : W";
                POSConfigHelper.SetConfig(cfgPrintSequence, "PRT");
            }

            //거래명세서 설정(작은 / 큰)
            Bifrost.Helper.POSConfig cfgPrintPaper = POSConfigHelper.GetConfig("PRT011");
            //세팅이 없으면 다시 넣어줌
            if (cfgPrintPaper == null)
            {
                cfgPrintPaper = new Bifrost.Helper.POSConfig();
                cfgPrintPaper.ConfigCode = "PRT011";
                cfgPrintPaper.ConfigName = "거래명세서 종이 설정";
                cfgPrintPaper.ConfigValue = "A5";
                cfgPrintPaper.ConfigDescript = "작은 사이즈 : A5 / 큰사이즈 : A4";
                POSConfigHelper.SetConfig(cfgPrintPaper, "PRT");
            }


            //영수증설정
            Bifrost.Helper.POSConfig cfgPrintDate = POSConfigHelper.GetConfig("RPT002");
            //세팅이 없으면 다시 넣어줌
            if (cfgPrintDate == null)
            {
                cfgPrintDate = new Bifrost.Helper.POSConfig();
                cfgPrintDate.ConfigCode = "RPT002";
                cfgPrintDate.ConfigName = "판매시간 / 판매일 표기 설정";
                cfgPrintDate.ConfigValue = "T";
                cfgPrintDate.ConfigDescript = "판매시간 : T / 판매일 : D";
                POSConfigHelper.SetConfig(cfgPrintDate, "RPT");
            }


            //영수증설정
            Bifrost.Helper.POSConfig cfgReceit = POSConfigHelper.GetConfig("RPT003");
            //세팅이 없으면 다시 넣어줌
            if (cfgReceit == null)
            {
                cfgReceit = new Bifrost.Helper.POSConfig();
                cfgReceit.ConfigCode = "RPT003";
                cfgReceit.ConfigName = "영수증 설정";
                cfgReceit.ConfigValue = "D";
                cfgReceit.ConfigDescript = "두줄표기 : D / 한줄표기 : S";
                POSConfigHelper.SetConfig(cfgReceit, "RPT");
            }

            // 영수증 하단 여백 설정
            Bifrost.Helper.POSConfig cfgReceitSpace = POSConfigHelper.GetConfig("RPT004");
            //세팅이 없으면 다시 넣어줌
            if (cfgReceitSpace == null)
            {
                cfgReceitSpace = new Bifrost.Helper.POSConfig();
                cfgReceitSpace.ConfigCode = "RPT004";
                cfgReceitSpace.ConfigName = "영수증 하단여백";
                cfgReceitSpace.ConfigValue = "0";
                cfgReceitSpace.ConfigDescript = "기본값 0줄";
                POSConfigHelper.SetConfig(cfgReceitSpace, "RPT");
            }


            // 영수증 출력 장수설정
            Bifrost.Helper.POSConfig cfgReceitCount = POSConfigHelper.GetConfig("RPT005");
            //세팅이 없으면 다시 넣어줌
            if (cfgReceitCount == null)
            {
                cfgReceitCount = new Bifrost.Helper.POSConfig();
                cfgReceitCount.ConfigCode = "RPT005";
                cfgReceitCount.ConfigName = "영수증 출력매수";
                cfgReceitCount.ConfigValue = "1";
                cfgReceitCount.ConfigDescript = "기본값 : 1매";
                POSConfigHelper.SetConfig(cfgReceitCount, "RPT");
            }

            // 영수증 순번 자리수 설정
            Bifrost.Helper.POSConfig cfgReceitNo = POSConfigHelper.GetConfig("RPT006");
            //세팅이 없으면 다시 넣어줌
            if (cfgReceitNo == null)
            {
                cfgReceitNo = new Bifrost.Helper.POSConfig();
                cfgReceitNo.ConfigCode = "RPT006";
                cfgReceitNo.ConfigName = "영수증 순번 자릿수";
                cfgReceitNo.ConfigValue = "2";
                cfgReceitNo.ConfigDescript = "기본값 : 2";
                POSConfigHelper.SetConfig(cfgReceitNo, "RPT");
            }



            //이전, 다음 클릭시 날짜 고정
            Bifrost.Helper.POSConfig cfgFixSaleDt = POSConfigHelper.GetConfig("POS016");
            //세팅이 없으면 다시 넣어줌
            if (cfgFixSaleDt == null)
            {
                cfgFixSaleDt = new Bifrost.Helper.POSConfig();
                cfgFixSaleDt.ConfigCode = "POS016";
                cfgFixSaleDt.ConfigName = "영업날짜 고정";
                cfgFixSaleDt.ConfigValue = "Y"; ;
                cfgFixSaleDt.ConfigDescript = "Y : 고정 / N :입력한대로. 이전 조회 후 원상태에서 날짜 고정을 위한 값";
                POSConfigHelper.SetConfig(cfgFixSaleDt, "POS");
            }


            //임시저장 설정
            Bifrost.Helper.POSConfig cfgTmpOrder = POSConfigHelper.GetConfig("POS017");
            //세팅이 없으면 다시 넣어줌
            if (cfgTmpOrder == null)
            {
                cfgTmpOrder = new Bifrost.Helper.POSConfig();
                cfgTmpOrder.ConfigCode = "POS017";
                cfgTmpOrder.ConfigName = "임시저장 설정";
                cfgTmpOrder.ConfigValue = "O";
                cfgTmpOrder.ConfigDescript = "건별 : O / 거래처별 : C";
                POSConfigHelper.SetConfig(cfgTmpOrder, "POS");
            }

            //임시저장 작업서 바로 출력 설정
            Bifrost.Helper.POSConfig cfgTmpPrintDirect = POSConfigHelper.GetConfig("POS018");
            //세팅이 없으면 다시 넣어줌
            if (cfgTmpPrintDirect == null)
            {
                cfgTmpPrintDirect = new Bifrost.Helper.POSConfig();
                cfgTmpPrintDirect.ConfigCode = "POS018";
                cfgTmpPrintDirect.ConfigName = "임시저장 바로 출력";
                cfgTmpPrintDirect.ConfigValue = "N";
                cfgTmpPrintDirect.ConfigDescript = "바로출력 : Y / 팝업 선택 : N";
                POSConfigHelper.SetConfig(cfgTmpPrintDirect, "POS");
            }

            //임시저장 작업서 신규만 출력
            Bifrost.Helper.POSConfig cfgTmpPrintNew = POSConfigHelper.GetConfig("POS019");
            //세팅이 없으면 다시 넣어줌
            if (cfgTmpPrintNew == null)
            {
                cfgTmpPrintNew = new Bifrost.Helper.POSConfig();
                cfgTmpPrintNew.ConfigCode = "POS019";
                cfgTmpPrintNew.ConfigName = "임시저장 신규 품목만 출력";
                cfgTmpPrintNew.ConfigValue = "A";
                cfgTmpPrintNew.ConfigDescript = "신규품목만 : N / 전체 : A";
                POSConfigHelper.SetConfig(cfgTmpPrintNew, "POS");
            }

            //임시저장 작업서 신규만 출력
            Bifrost.Helper.POSConfig cfgUseCreditCard = POSConfigHelper.GetConfig("POS020");
            //세팅이 없으면 다시 넣어줌
            if (cfgUseCreditCard == null)
            {
                cfgUseCreditCard = new Bifrost.Helper.POSConfig();
                cfgUseCreditCard.ConfigCode = "POS020";
                cfgUseCreditCard.ConfigName = "신용카드 사용유무";
                cfgUseCreditCard.ConfigValue = "Y";
                cfgUseCreditCard.ConfigDescript = "사용 : Y / 미사용 : N";
                POSConfigHelper.SetConfig(cfgUseCreditCard, "POS");
            }

            //임시저장 작업서 신규만 출력
            Bifrost.Helper.POSConfig cfgCustNonpaid = POSConfigHelper.GetConfig("POS021");
            //세팅이 없으면 다시 넣어줌
            if (cfgCustNonpaid == null)
            {
                cfgCustNonpaid = new Bifrost.Helper.POSConfig();
                cfgCustNonpaid.ConfigCode = "POS021";
                cfgCustNonpaid.ConfigName = "거래처 외상금액 관리";
                cfgCustNonpaid.ConfigValue = "Y";
                cfgCustNonpaid.ConfigDescript = "사용 : Y / 미사용 : N";
                POSConfigHelper.SetConfig(cfgCustNonpaid, "POS");
            }

            //작업서 마이너스 수량 안보이게 설정
            Bifrost.Helper.POSConfig cfgWorkSheetMinus = POSConfigHelper.GetConfig("RPT010");
            //세팅이 없으면 다시 넣어줌
            if (cfgWorkSheetMinus == null)
            {
                cfgWorkSheetMinus = new Bifrost.Helper.POSConfig();
                cfgWorkSheetMinus.ConfigCode = "RPT010";
                cfgWorkSheetMinus.ConfigName = "작업서 -수량 포함 설정";
                cfgWorkSheetMinus.ConfigValue = "Y";
                cfgWorkSheetMinus.ConfigDescript = "포함 : Y / 미포함 : N";
                POSConfigHelper.SetConfig(cfgWorkSheetMinus, "RPT");
            }

            // 작업서 하단 여백 설정
            Bifrost.Helper.POSConfig cfgWorkSheetSpace = POSConfigHelper.GetConfig("RPT011");
            //세팅이 없으면 다시 넣어줌
            if (cfgWorkSheetSpace == null)
            {
                cfgWorkSheetSpace = new Bifrost.Helper.POSConfig();
                cfgWorkSheetSpace.ConfigCode = "RPT011";
                cfgWorkSheetSpace.ConfigName = "작업서 하단여백";
                cfgWorkSheetSpace.ConfigValue = "0";
                cfgWorkSheetSpace.ConfigDescript = "기본값 0줄";
                POSConfigHelper.SetConfig(cfgWorkSheetSpace, "RPT");
            }

            // 작업서 출력매수 설정
            Bifrost.Helper.POSConfig cfgWorkSheetCount = POSConfigHelper.GetConfig("RPT012");
            //세팅이 없으면 다시 넣어줌
            if (cfgWorkSheetCount == null)
            {
                cfgWorkSheetCount = new Bifrost.Helper.POSConfig();
                cfgWorkSheetCount.ConfigCode = "RPT012";
                cfgWorkSheetCount.ConfigName = "작업서 출력매수";
                cfgWorkSheetCount.ConfigValue = "1";
                cfgWorkSheetCount.ConfigDescript = "기본값 : 1매";
                POSConfigHelper.SetConfig(cfgWorkSheetCount, "RPT");
            }


        }

        private void SetAutoSaleDt()
        {
            //영업일자 자동 세팅
            Bifrost.Helper.POSConfig cfgAutoSaleDt = POSConfigHelper.GetConfig("POS008");
            //세팅이 없으면 다시 넣어줌
            if (cfgAutoSaleDt == null)
            {
                cfgAutoSaleDt = new Bifrost.Helper.POSConfig();
                cfgAutoSaleDt.ConfigCode = "POS008";
                cfgAutoSaleDt.ConfigName = "영업일자동설정";
                cfgAutoSaleDt.ConfigValue = "N";
                cfgAutoSaleDt.RefCode = "19:00";
                POSConfigHelper.SetConfig(cfgAutoSaleDt, "POS");
            }

            if (cfgAutoSaleDt.ConfigValue == "Y")
            {
                string SettingTime = cfgAutoSaleDt.RefCode;

                toggleSwitchSaleDt.IsOn = true;
                lblSaltDtDescrip.Text = "※  설정된 기준 시간은 " + SettingTime + " 입니다.";

                dtpSale.Enabled = false;

                //설정 시간이 00:00시면 무조건 현재일
                if (SettingTime == "00:00")
                {
                    dtpSale.Text = A.GetToday;
                }
                //설정 시간이 24:00시면 무조건 +1일
                else if (SettingTime == "24:00")
                {
                    dtpSale.Text = A.GetSomeMonthSomeDay(0, 1);
                }
                //그외는 설정 시간에 따라 설정
                else 
                {
                    DateTime SetDt;
                    DateTime NowDt = DateTime.Now;

                    DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd") + " " + SettingTime, out SetDt);

                    TimeSpan timeDiff = NowDt - SetDt;

                    //설정시간보다 뒤면 영업일을 내일로 고정함
                    if (timeDiff.Ticks > 0)
                    {
                        dtpSale.Text = A.GetSomeMonthSomeDay(0, 1);
                    }
                    else
                    {
                        dtpSale.Text = A.GetToday;
                    }
                }
                
                MdiForm.SetSystemDate(dtpSale.Text);
            }
            else
            {
                toggleSwitchSaleDt.IsOn = false;
                lblSaltDtDescrip.Text = string.Empty;
                dtpSale.Enabled = true;
            }
        }

        private void InitEvent()
        {
            //판매
            btnMenuSale01.Click += BtnMenu_Click;
            btnMenuSale02.Click += BtnMenu_Click;
            btnMenuSale03.Click += BtnMenu_Click;
            btnMenuSale04.Click += BtnMenu_Click;
            btnMenuSale05.Click += BtnMenu_Click;
            btnMenuSale06.Click += BtnMenu_Click;
            btnMenuSale07.Click += BtnMenu_Click;
            btnMenuSale08.Click += BtnMenu_Click;
            btnMenuSale09.Click += BtnMenu_Click;

            //설정
            btnMenuSetting01.Click += BtnMenu_Click;
            btnMenuSetting02.Click += BtnMenu_Click;
            btnMenuSetting03.Click += BtnMenu_Click;
            btnMenuSetting04.Click += BtnMenu_Click;
            btnMenuSetting05.Click += BtnMenu_Click;
            btnMenuSetting06.Click += BtnMenu_Click;
            btnMenuSetting07.Click += BtnMenu_Click;
            btnMenuSetting08.Click += BtnMenu_Click;
            btnMenuSetting09.Click += BtnMenu_Click;
            btnMenuSetting10.Click += BtnMenu_Click;

            //구매
            btnMenuPurchase01.Click += BtnMenu_Click;
            btnMenuPurchase02.Click += BtnMenu_Click;
            btnMenuPurchase03.Click += BtnMenu_Click;
            btnMenuPurchase04.Click += BtnMenu_Click;
            btnMenuPurchase05.Click += BtnMenu_Click;
            btnMenuPurchase06.Click += BtnMenu_Click;
            btnMenuPurchase07.Click += BtnMenu_Click;

            //재고관리
            btnMenuInv01.Click += BtnMenu_Click;
            btnMenuInv02.Click += BtnMenu_Click;
            btnMenuInv03.Click += BtnMenu_Click;
            btnMenuInv04.Click += BtnMenu_Click;


            //구버전
            btnMenuOld01.Click += BtnMenu_Click;
            btnMenuOld02.Click += BtnMenu_Click;
            btnMenuOld03.Click += BtnMenu_Click;
            btnMenuOld04.Click += BtnMenu_Click;
            btnMenuOld05.Click += BtnMenu_Click;

            btnMenuOld51.Click += BtnMenu_Click;
            btnMenuOld52.Click += BtnMenu_Click;
            btnMenuOld53.Click += BtnMenu_Click;
            btnMenuOld54.Click += BtnMenu_Click;

            dtpSale.EditValueChanged += DtpSale_EditValueChanged;
            toggleSwitchSaleDt.EditValueChanged += ToggleSwitchSaleDt_EditValueChanged;

        }

        private void ToggleSwitchSaleDt_EditValueChanged(object sender, EventArgs e)
        {
            Bifrost.Helper.POSConfig cfgAutoSaleDt = POSConfigHelper.GetConfig("POS008");

            if (toggleSwitchSaleDt.IsOn)
            {
                //영업일자 자동 세팅
                cfgAutoSaleDt.ConfigValue = "Y";
                POSConfigHelper.SetConfig(cfgAutoSaleDt, "POS");
                dtpSale.Enabled = false;

                lblSaltDtDescrip.Text = "※ 자동 설정된 기준 시간은 " + cfgAutoSaleDt.RefCode + " 입니다.";

            }
            else
            {
                //영업일자 자동 세팅
                cfgAutoSaleDt.ConfigValue = "N";
                POSConfigHelper.SetConfig(cfgAutoSaleDt, "POS");
                dtpSale.Enabled = true;
                lblSaltDtDescrip.Text = string.Empty;

            }

        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            string Tag = ((aButtonSizeFree)sender).Tag.ToString();

            //if (isUseSaleOld)
            //{
            //    if(Tag == "M_POS_SALE")
            //    {
            //        Tag = Tag + "_OLD";
            //    }
            //}

            string MenuClass = "POS." + Tag;
            string MenuLocation = Tag + ".dll";

            Bifrost.Data.MenuData menu = new Bifrost.Data.MenuData
            {
                MenuID = ((aButtonSizeFree)sender).Name,
                MenuClass = MenuClass,
                MenuLocation = MenuLocation,
                MenuName = ((aButtonSizeFree)sender).SubTitle
            };

            MdiForm.CreateChildForm(menu);
        }

        private void DtpSale_EditValueChanged(object sender, EventArgs e)
        {
            MdiForm.SetSystemDate(dtpSale.Text);
            dtpSale.DoValidate();
        }


    }
}
