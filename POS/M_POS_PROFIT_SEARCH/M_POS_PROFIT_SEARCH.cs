using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using Bifrost.Win.Controls;
using DevExpress.Utils.Win;
using DevExpress.XtraCharts;
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
    public partial class M_POS_PROFIT_SEARCH : POSFormBase
    {
        public M_POS_PROFIT_SEARCH()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitControl()
        {
            VisibleBtnDelete = false;
            VisibleBtnSave = false;
            VisibleBtnNew = false;

            dtpFrom.Text = POSGlobal.SaleDt;
            dtpTo.Text = POSGlobal.SaleDt;

            gridViewItem.Columns["AM_RCP"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_RCP"].SummaryItem.DisplayFormat = "합 : {0:##,##0.####}";
            
            gridViewItem.Columns["AM_ISU"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_ISU"].SummaryItem.DisplayFormat = "합 : {0:##,##0.####}";

            gridViewItem.Columns["AM_PROFIT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            gridViewItem.Columns["AM_PROFIT"].SummaryItem.DisplayFormat = "합 : {0:##,##0.####}";

            gridViewItem.OptionsView.AllowCellMerge = true;

            CH.SetDateEditFont(dtpFrom);
            CH.SetDateEditFont(dtpTo);
        }

        private void InitEvent()
        {
            gridViewItem.FocusedRowChanged += GridViewMain_FocusedRowChanged;
            gridViewItem.CellMerge += GridViewItem_CellMerge;

        }

        private void GridViewItem_CellMerge(object sender, CellMergeEventArgs e)
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

        private void GridViewMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridViewItem.GetFocusedRow() == null) return;

                string ItemCode = gridViewItem.GetFocusedRowCellValue("CD_ITEM").ToString();

                gridViewDetail.ActiveFilterString = "CD_ITEM = '" + ItemCode + "'";
                gridViewDetail.BestFitColumns();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        //private void GetItemUnit(string ItemCode, decimal ItemQty)
        //{
        //    DataTable dtUnit = DBHelper.GetDataTable("USP_POS_ITEM_UNIT_S", new object[] { POSGlobal.StoreCode, ItemCode });

        //    for (int i = 0; i < dtUnit.Rows.Count; i++)
        //    {
        //        Control[] findCtl = Controls.Find("lblUnit" + (i + 1).ToString(), true);

        //        decimal UnitQty = A.GetDecimal(dtUnit.Rows[i]["QT_UNIT"]);

        //        if (findCtl != null && findCtl.Length > 0)
        //        {
        //            decimal qt = ItemQty / (UnitQty == 0 ? 1 : UnitQty);
        //            findCtl[0].Text = A.GetNumericString(qt) + dtUnit.Rows[i]["NM_UNIT"].ToString();
        //            findCtl[0].Enabled = true;
        //        }
        //    }
        //}

        private void BtnCust_Click(object sender, EventArgs e)
        {
            P_POS_CONTENTS P_POS_CONTENTS = new P_POS_CONTENTS();
            P_POS_CONTENTS.ContentsType = ContentsMode.Customer;

            if (P_POS_CONTENTS.ShowDialog() == DialogResult.OK)
            {
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

            DataSet _ds = Search(new object[] { POSGlobal.StoreCode, dtpFrom.Text, dtpTo.Text });
            gridMain.Binding(_ds.Tables[0], true);
            gridDetail.Binding(_ds.Tables[1], true);
            chartControlItem.DataSource = _ds.Tables[0];




            chartControlItem.Series[0].Name = "상품";
            chartControlItem.Series[0].ArgumentDataMember = "NM_ITEM";
            chartControlItem.Series[0].ValueDataMembers[0] = "AM_PROFIT";
            
            chartControlItem.Series[0].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;


            //gridDetail.Binding(_ds.Tables[1], true);

            LoadData.EndLoading();

        }

        //private void SetPieChartData(ChartControl chart, DataTable dt)
        //{
        //    ViewType viewType = ViewType.Pie;

        //    Dictionary<string, double> totalInfo = new Dictionary<string, double>();

        //    ////TotalQty 집계
        //    //foreach (DataRow row in dt.Rows)
        //    //{
        //    //    string product = row["PRODUCT"].ToString();
        //    //    string year = row["YEAR"].ToString();
        //    //    int qty = (int)row["QTY"];

        //    //    if (totalInfo.ContainsKey(product) == false)
        //    //        totalInfo.Add(product, 0);

        //    //    totalInfo[product] += qty;
        //    //}

        //    Series series = new Series("Total", ViewType.Pie);
        //    chart.Series.Add(series);

        //    //Legend 표시부분 변경
        //    series.LegendTextPattern = "{A}";

        //    foreach (KeyValuePair<string, double> info in totalInfo)
        //    {
        //        string product = info.Key;
        //        double qty = info.Value;

        //        SeriesPoint point = new SeriesPoint(product, qty);
        //        series.Points.Add(point);
        //    }

        //    //ChartTitle 생성
        //    ChartTitle title = new ChartTitle();
        //    title.Text = viewType.ToString();
        //    chart.Titles.Add(title);
        //}

    }
}
