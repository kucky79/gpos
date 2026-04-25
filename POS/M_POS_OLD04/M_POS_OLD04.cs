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
    public partial class M_POS_OLD04 : POSFormBase
    {
        public string CustomerCode { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        ReceitMode ReceitType { get; set; } = ReceitMode.Double;

        private readonly Color ColorReturn = Color.FromArgb(209, 39, 79);

        decimal totalPrice;

        public M_POS_OLD04()
        {
            InitializeComponent();
            InitializeControl();
            InitEvent();
        }

        private void InitEvent()
        {
            btnCust.Click += BtnCust_Click;
            btnClear.Click += BtnClear_Click;

            gridView1.CellMerge += GridView1_CellMerge;
            gridView2.CellMerge += GridView2_CellMerge;
            gridView3.CellMerge += GridView3_CellMerge;
            gridView4.CellMerge += GridView4_CellMerge;

            gridView1.RowStyle += GridView_RowStyle;
            gridView2.RowStyle += GridView_RowStyle;
            gridView3.RowStyle += GridView_RowStyle;
            gridView4.RowStyle += GridView_RowStyle;

            gridView2.CustomSummaryCalculate += GridView2_CustomSummaryCalculate;
            TabControlMain.SelectedPageChanged += TabControlMain_SelectedPageChanged;

        }

        private void InitializeControl()
        {
            CH.SetDateEditFont(dtpFrom, 20F);
            CH.SetDateEditFont(dtpTo, 20F);


            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpFrom.Text = POSGlobal.SaleDt;
            dtpTo.Text = POSGlobal.SaleDt;


            //GridHelper.SetDecimalPoint(gridMain, new string[] { "AM_SALE", "AM_VAT", "AM_TOT" }, 0);
            this.gridView1.Columns["DT_SALE"].ColumnEdit = GridHelper.SetGridMask(Bifrost.CommonFunction.MaskType.DATE);

            //GridHelper.SetDecimalPoint(gridDetail, new string[] { "UM", "AM", "QT_UNIT" }, 0);
            gridView2.Columns["DT_SO"].ColumnEdit = GridHelper.SetGridMask(Bifrost.CommonFunction.MaskType.DATE);

            //gridView4.Columns["DT_SO"].ColumnEdit = GridHelper.SetGridMask(Bifrost.CommonFunction.MaskType.YYMM);


            this.gridView1.OptionsView.AllowCellMerge = true;
            gridView2.OptionsView.AllowCellMerge = true;
            gridView3.OptionsView.AllowCellMerge = true;
            gridView4.OptionsView.AllowCellMerge = true;

            this.gridView1.Columns["AM_SALE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            this.gridView1.Columns["AM_SALE"].SummaryItem.DisplayFormat = "{0:##,##0.####}";


            gridView2.Columns["AM"].Summary.Add(DevExpress.Data.SummaryItemType.Custom);
            gridView2.Columns["AM"].SummaryItem.DisplayFormat = "{0:##,##0.####}";

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

            ////부가세 설정 화면 보이기 디폴트는 N임
            //POSConfig configVatYN = POSConfigHelper.GetConfig("POS004");
            //if(configVatYN.ConfigValue == "Y")
            //{
            //    gridView1.Columns["AM_VAT"].Visible = true;
            //}
            //else
            //{
            //    gridView1.Columns["AM_VAT"].Visible = false;
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
                if ((ColName == "999" ) && ColName != string.Empty)
                {
                    e.Appearance.ForeColor = ColorReturn;
                }
                else
                {
                    e.Appearance.ForeColor = default;
                }
            }
        }

        private void GridView2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // Initialization.  
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                totalPrice = 0;
            }
            // Calculation. 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                totalPrice += A.GetDecimal(e.FieldValue);
            }
            // Finalization.  
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (A.GetString(aRadioButtonSumYN.EditValue) == "Y")
                {
                    e.TotalValue = totalPrice / 2;
                }
                else
                {
                    e.TotalValue = totalPrice;
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

                e.Merge = dr1["DT_SALE"].ToString().Equals(dr2["DT_SALE"].ToString());
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

            if (e.Column.FieldName == "CD_ITEM")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["CD_ITEM"].ToString().Equals(dr2["CD_ITEM"].ToString());
            }
            else if (e.Column.FieldName == "NM_ITEM")//Name 컬럼만 Merge
            {
                var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
                var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

                e.Merge = dr1["CD_ITEM"].ToString().Equals(dr2["CD_ITEM"].ToString());
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

            //if (e.Column.FieldName == "NM_CUST")//Name 컬럼만 Merge
            //{
            //    var dr1 = view.GetDataRow(e.RowHandle1); //위에 행 정보
            //    var dr2 = view.GetDataRow(e.RowHandle2); //아래 행 정보

            //    e.Merge = dr1["NM_CUST"].ToString().Equals(dr2["NM_CUST"].ToString());
            //}
            //else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        #endregion 그리드 이벤트 모음

        #region 버튼 이벤트
        private void BtnClear_Click(object sender, EventArgs e)
        {
            aLookUpEditCust.Properties.DataSource = null;
            aLookUpEditCust.EditValue = null;
        }

        private void BtnCust_Click(object sender, EventArgs e)
        {
            P_POS_CONTENTS_SINGLE P_POS_CONTENTS_SINGLE = new P_POS_CONTENTS_SINGLE();
            P_POS_CONTENTS_SINGLE.ContentsType = ContentsMode.StoredProcedure;
            P_POS_CONTENTS_SINGLE.StoredProcedure = "USP_OLD_SEARCH04_CUST";
            P_POS_CONTENTS_SINGLE.dbType = DBType.Old;
            P_POS_CONTENTS_SINGLE.spParams = new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text };
            P_POS_CONTENTS_SINGLE.keyCustom = "CST_CD";
            P_POS_CONTENTS_SINGLE.PopupTitle = "거래처 조회";

            if (P_POS_CONTENTS_SINGLE.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                if (P_POS_CONTENTS_SINGLE.SeletedCode != null)
                {
                    dic.Add(P_POS_CONTENTS_SINGLE.SeletedCode, P_POS_CONTENTS_SINGLE.SeletedName);
                    CH.SetCombobox(aLookUpEditCust, CH.GetCode(dic), false);
                }

                if (P_POS_CONTENTS_SINGLE != null)
                {
                    P_POS_CONTENTS_SINGLE.Dispose();
                }
            }
        }
        #endregion 버튼 이벤트

        #region 프린트 모음
        private void PrintThemal()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(일자별)\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("거 래 처 : " + aLookUpEditCust.Text + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.  날 짜                   판 매 액    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridView1.RowCount; i++)
            {
                decimal NonPaidAmt = A.GetDecimal(gridView1.GetRowCellValue(i, gridView1.Columns["AM_SALE"]));

                SaleDt = A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["DT_SALE"]));
                SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);

                if (NonPaidAmt != 0)
                {
                    CustomerName = string.Empty;// A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["NM_CUST"]));
                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                    RowCnt++;
                    //sb.Append(" No.    거래처                판 매 액    \n");
                    //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                    int TotalLenth = A.GetByteLength(SaleDt + " " + CustomerName + " " + A.GetNumericString(NonPaidAmt));
                    int blankLen;

                    if (TotalLenth > 42)
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + SaleDt + " " + CustomerName + "\n");
                        blankLen = 42;
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }
                    else
                    {
                        sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + SaleDt + " " + CustomerName);
                        blankLen = 42 - (A.GetByteLength(SaleDt + " " + CustomerName) + 4);
                        sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                    }

                    sb.Append(PrinterCommand.NewLine);
                }

            }

            decimal SumValue = A.GetDecimal(gridView1.Columns["AM_SALE"].SummaryItem.SummaryValue);


            sb.Append("==========================================\n");
            sb.Append(" 합    계 : " + SumValue.ToString("##,##0").PadLeft(30, ' ') + "\n");
            sb.Append("------------------------------------------\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalDayItem()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;


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
            sb.Append(SubTitle + "\n");
            sb.Append("(일자별/상품별)\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("거 래 처 : " + aLookUpEditCust.Text + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");

            sb.Append(PrinterCommand.AlignLeft);

            for (int i = 0; i < gridView2.RowCount; i++)
            {
                if (A.GetInt(gridView2.GetRowCellValue(i, gridView2.Columns["NO_ROW"])) == 1)
                {
                    sb.Append("==========================================\n");
                    CustomerName = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["NM_CUST"]));
                    SaleDt = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["DT_SO"]));
                    SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2) + "-" + SaleDt.Substring(6, 2);
                    sb.Append(PrinterCommand.AlignLeft);
                    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append(SaleDt + " / " + CustomerName + "\n");
                    sb.Append(" No.    상품명     수량/단위    금  액    \n");
                    sb.Append("------------------------------------------\n");
                }

                ItemName = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["NM_ITEM"]));
                UnitAmount = A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["UM"]));
                ItemDescrip = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["DC_ITEM"])).Replace(" * 1", "");
                Amount = A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM"]));
                padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                //상품
                if (A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["CD_ITEM"])) != "99999999999999")
                {
                    sb.Append(PrinterCommand.AlignLeft);

                    if (ReceitType == ReceitMode.Double)
                    {
                        //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                        sb.Append(" " + A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["NO_ROW"])).PadLeft(3, ' ') + " " + ItemName + "\n");
                        //단가
                        sb.Append(UnitAmount.PadLeft(17, ' '));
                        //수량 단위
                        sb.Append("".PadLeft(padLen) + ItemDescrip);

                        //금액
                        sb.Append(Amount.PadLeft(11, ' '));
                        sb.Append(PrinterCommand.NewLine);
                    }
                    else if (ReceitType == ReceitMode.Single)
                    {
                        string FirstGroup = A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["NO_ROW"])).PadLeft(3, ' ') + " " + ItemName;

                        //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                        sb.Append(FirstGroup);

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

                //합계
                if (A.GetString(gridView2.GetRowCellValue(i, gridView2.Columns["NO_ROW"])) == "999" && A.GetString(aRadioButtonSumYN.EditValue) == "Y")
                {
                    sb.Append("------------------------------------------\n");
                    sb.Append("합    계 : " + A.GetNumericString(gridView2.GetRowCellValue(i, gridView2.Columns["AM"])).PadLeft(31, ' '));
                    sb.Append("******************************************\n");
                    sb.Append(PrinterCommand.NewLine);

                }
            }

            decimal SumValue = A.GetDecimal(gridView2.Columns["AM"].SummaryItem.SummaryValue);

            sb.Append("==========================================\n");
            sb.Append(" 총합계  : " + SumValue.ToString("##,##0").PadLeft(31, ' ') + "\n");
            sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalItem()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;


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
            sb.Append(SubTitle + "\n");
            sb.Append("(상품별)\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("거 래 처 : " + aLookUpEditCust.Text + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.    상품명     수량/단위       금  액 \n");
            sb.Append("------------------------------------------\n");
            sb.Append(PrinterCommand.AlignLeft);

            for (int i = 0; i < gridView3.RowCount; i++)
            {
                if (A.GetString(gridView3.GetRowCellValue(i, gridView3.Columns["NO_ROW"])) != "999")
                {
                    ItemName = A.GetString(gridView3.GetRowCellValue(i, gridView3.Columns["NM_ITEM"]));
                    ItemDescrip = A.GetNumericString(A.GetString(gridView3.GetRowCellValue(i, gridView3.Columns["QT_UNIT"]))) + A.GetString(gridView3.GetRowCellValue(i, gridView3.Columns["NM_UNIT"]));
                    Amount = A.GetNumericString(gridView3.GetRowCellValue(i, gridView3.Columns["AM"]));
                    padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;

                    //상품
                    if (A.GetString(gridView3.GetRowCellValue(i, gridView3.Columns["CD_ITEM"])) != "99999999999999")
                    {
                        sb.Append(PrinterCommand.AlignLeft);

                        //if (ReceitType == ReceitMode.Double)
                        //{
                        //    //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                        //    sb.Append(" " + A.GetString(gridView3.GetRowCellValue(i, gridView3.Columns["NO_ROW"])).PadLeft(3, ' ') + " " + ItemName + "\n");
                        //    //수량 단위
                        //    sb.Append("".PadLeft(padLen) + ItemDescrip);

                        //    //금액
                        //    sb.Append(Amount.PadLeft(11, ' '));
                        //    sb.Append(PrinterCommand.NewLine);
                        //}
                        //else if (ReceitType == ReceitMode.Single)
                        //{
                        string FirstGroup = A.GetString(i + 1).PadLeft(3, ' ') + " " + ItemName;

                        //sb.Append(" No.    상품명     수량/단위    금  액    \n");
                        sb.Append(FirstGroup);

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
                        //}

                    }
                }

                //합계
                //if (A.GetString(gridView3.GetRowCellValue(i, gridView3.Columns["NO_ROW"])) == "999")
                {
                    sb.Append("==========================================\n");
                    sb.Append("합    계 : " + A.GetNumericString(gridView3.GetRowCellValue(i, gridView3.Columns["AM"])).PadLeft(31, ' '));
                    sb.Append("==========================================\n");
                    sb.Append(PrinterCommand.NewLine);

                }
            }

            //decimal SumValue = A.GetDecimal(gridView3.Columns["AM"].SummaryItem.SummaryValue);

            //sb.Append("==========================================\n");
            //sb.Append(" 총합계  : " + SumValue.ToString("##,##0").PadLeft(31, ' ') + "\n");
            //sb.Append("==========================================\n");

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintThemalMonth()
        {
            string CustomerName = string.Empty;
            string SaleDt = string.Empty;

            DataTable dtStore = DBHelper.GetDataTable("USP_GET_POS_STORE_INFO", new object[] { POSGlobal.StoreCode });
            if (dtStore.Rows.Count == 0) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.AlignCenter);
            sb.Append(PrinterCommand.ConvertFontSize(2, 2));
            sb.Append(SubTitle + "\n");
            sb.Append("(월별)\n");
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.NewLine);
            sb.Append("==========================================\n");
            sb.Append(PrinterCommand.ConvertFontSize(1, 2));
            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append("상    호 : " + POSGlobal.StoreName + "\n");
            sb.Append("거 래 처 : " + aLookUpEditCust.Text + "\n");
            sb.Append("조회기간 : " + dtpFrom.DateTime.ToString("yyyy-MM-dd") + " ~ " + dtpTo.DateTime.ToString("yyyy-MM-dd") + "\n");
            sb.Append("출 력 일 : " + DateTime.Now.ToString("yyyy-MM-dd") + "\n");
            sb.Append("==========================================\n");
            sb.Append(" No.  년 월                   판 매 액    \n");
            sb.Append("------------------------------------------\n");



            int RowCnt = 0;

            sb.Append(PrinterCommand.AlignLeft);


            for (int i = 0; i < gridView4.RowCount; i++)
            {
                if (A.GetString(gridView4.GetRowCellValue(i, gridView4.Columns["NO_ROW"])) != "999")
                {

                    decimal NonPaidAmt = A.GetDecimal(gridView4.GetRowCellValue(i, gridView4.Columns["AM"]));

                    SaleDt = A.GetString(gridView4.GetRowCellValue(i, gridView4.Columns["DT_SO"]));
                    SaleDt = SaleDt.Substring(0, 4) + "-" + SaleDt.Substring(4, 2);

                    if (NonPaidAmt != 0)
                    {
                        CustomerName = string.Empty;// A.GetString(gridView4.GetRowCellValue(i, gridView4.Columns["NM_CUST"]));
                                                    //padLen = 14 - Encoding.Default.GetBytes(ItemDescrip).Length;
                        RowCnt++;
                        //sb.Append(" No.    거래처                판 매 액    \n");
                        //텍스트 전부 더한길이가 42자보다 길면 두줄로 나오게 해야함
                        int TotalLenth = A.GetByteLength(SaleDt + " " + CustomerName + " " + A.GetNumericString(NonPaidAmt));
                        int blankLen;

                        if (TotalLenth > 42)
                        {
                            sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + SaleDt + " " + CustomerName + "\n");
                            blankLen = 42;
                            sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                        }
                        else
                        {
                            sb.Append(RowCnt.ToString().PadLeft(3, ' ') + " " + SaleDt + " " + CustomerName);
                            blankLen = 42 - (A.GetByteLength(SaleDt + " " + CustomerName) + 4);
                            sb.Append(A.GetNumericString(NonPaidAmt).PadLeft(blankLen, ' '));
                        }

                        sb.Append(PrinterCommand.NewLine);
                    }
                }
                //합계
                if (A.GetString(gridView4.GetRowCellValue(i, gridView4.Columns["NO_ROW"])) == "999")
                {
                    sb.Append("==========================================\n");
                    sb.Append("합    계 : " + A.GetNumericString(gridView4.GetRowCellValue(i, gridView4.Columns["AM"])).PadLeft(31, ' '));
                    sb.Append("==========================================\n");
                    sb.Append(PrinterCommand.NewLine);

                }
            }


            sb.Append(PrinterCommand.InitializePrinter);
            sb.Append(PrinterCommand.LineFeed(5));
            sb.Append(PrinterCommand.Cut);
            PrinterCommand.Print(PrintPort, sb.ToString());
            sb.Clear();
        }

        private void PrintNormal()
        {
            POSPrintHelper.POSReportPrintOld("R_POS_OLD_SEARCH04_DAY", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "FG_SORT" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), A.GetString(aRadioButtonSort.EditValue) });

        }

        private void PrintNormalDayItem()
        {
            POSPrintHelper.POSReportPrintOld("R_POS_OLD_SEARCH04_DAYITEM", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "YN_SUM", "FG_SORT" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), A.GetString(aRadioButtonSumYN.EditValue), A.GetString(aRadioButtonSort.EditValue) });

        }

        private void PrintNormalItem()
        {
            POSPrintHelper.POSReportPrintOld("R_POS_OLD_SEARCH04_ITEM", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "YN_SUM", "FG_SORT" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), A.GetString(aRadioButtonSumYN.EditValue), A.GetString(aRadioButtonSort.EditValue) });

        }

        private void PrintNormalMonth()
        {
            POSPrintHelper.POSReportPrintOld("R_POS_OLD_SEARCH04_MONTH", new string[] { "CD_STORE", "DT_FROM", "DT_TO", "CD_CUST", "YN_SUM", "FG_SORT" }, new string[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), A.GetString(aRadioButtonSumYN.EditValue), A.GetString(aRadioButtonSort.EditValue) });

        }
        #endregion 프린트 모음


        public override void OnView()
        {

            if(A.GetString(aLookUpEditCust.EditValue) == string.Empty)
            {
                ShowMessageBoxA("거래처를 선택해 주세요.", Bifrost.Common.MessageType.Warning);
                return;
            }

            LoadData.StartLoading(this);

            if (TabControlMain.SelectedTabPageIndex == 0)
            {

                DataTable dt = Search1(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonSort.EditValue });
                gridMain.Binding(dt, true);

            }
            else if (TabControlMain.SelectedTabPageIndex == 1)
            {

                DataTable dt = Search2(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonSumYN.EditValue, aRadioButtonSort.EditValue });
                gridDetail.Binding(dt, true);

            }
            else if (TabControlMain.SelectedTabPageIndex == 2)
            {

                DataTable dt = Search3(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonSumYN.EditValue, aRadioButtonSort.EditValue });
                gridItem.Binding(dt, true);

            }
            else if (TabControlMain.SelectedTabPageIndex == 3)
            {

                DataTable dt = Search4(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text, A.GetDatatableToString(aLookUpEditCust.Properties.DataSource), aRadioButtonSumYN.EditValue, aRadioButtonSort.EditValue });
                gridMonth.Binding(dt, true);

            }
            LoadData.EndLoading();

        }

        public override void OnPrint()
        {
            try
            {
                //프린터타입 가져오기
                Bifrost.Helper.POSConfig cfgPrint = POSConfigHelper.GetConfig("PRT002");
                string PrintType = cfgPrint.ConfigValue;

                switch (TabControlMain.SelectedTabPageIndex)
                {
                    case 0:
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
                        break;
                    case 1:
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
                                            PrintThemalDayItem();
                                            break;
                                        case "P":
                                            PrintNormalDayItem();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            case "T": //감열지
                                PrintThemalDayItem();
                                break;
                            case "P": //일반
                                PrintNormalDayItem();
                                break;
                        }
                        break;
                    case 2:
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
                                            PrintThemalItem();
                                            break;
                                        case "P":
                                            PrintNormalItem();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            case "T": //감열지
                                PrintThemalItem();
                                break;
                            case "P": //일반
                                PrintNormalItem();
                                break;
                        }
                        break;
                    case 3:
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
                                            PrintThemalMonth();
                                            break;
                                        case "P":
                                            PrintNormalMonth();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            case "T": //감열지
                                PrintThemalMonth();
                                break;
                            case "P": //일반
                                PrintNormalMonth();
                                break;
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

        public override void OnExcelExport()
        {
            switch (TabControlMain.SelectedTabPageIndex)
            {
                case 0:
                    GridHelper.ExcelExport(gridMain, "판매현황");
                    break;
                case 1:
                    GridHelper.ExcelExport(gridDetail, "판매현황(상세)");
                    break;
                case 2:
                    GridHelper.ExcelExport(gridItem, "판매현황(상품별)");
                    break;
                case 3:
                    GridHelper.ExcelExport(gridMonth, "판매현황(월별)");
                    break;
                default:
                    break;
            }
        }
    }
}
