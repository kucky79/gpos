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
    public partial class M_POS_INV_SEARCH : POSFormBase
    {
        public M_POS_INV_SEARCH()
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

            dtpSearch.Text = POSGlobal.SaleDt;
            //gridViewItem.Columns["SALE_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            //gridViewItem.Columns["SALE_AMT"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            gridViewItem.OptionsView.AllowCellMerge = true;

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
                decimal ItemQty = A.GetDecimal(gridViewItem.GetFocusedRowCellValue("QT_INV"));

                gridViewDetail.ActiveFilterString = "CD_ITEM = '" + ItemCode + "'";
                gridViewDetail.BestFitColumns();

                lblItem.Text = gridViewItem.GetFocusedRowCellValue("NM_ITEM").ToString();
                GetItemUnit(ItemCode, ItemQty);
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void GetItemUnit(string ItemCode, decimal ItemQty)
        {
            DataTable dtUnit = DBHelper.GetDataTable("USP_POS_ITEM_UNIT_S", new object[] { POSGlobal.StoreCode, ItemCode });

            for (int i = 0; i < dtUnit.Rows.Count; i++)
            {
                Control[] findCtl = Controls.Find("lblUnit" + (i + 1).ToString(), true);

                decimal UnitQty = A.GetDecimal(dtUnit.Rows[i]["QT_UNIT"]);

                if (findCtl != null && findCtl.Length > 0)
                {
                    decimal qt = ItemQty / (UnitQty == 0 ? 1 : UnitQty);
                    findCtl[0].Text = A.GetNumericString(qt) + dtUnit.Rows[i]["NM_UNIT"].ToString();
                    findCtl[0].Enabled = true;
                }
            }
        }

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

            DataSet _ds = Search(new object[] { POSGlobal.StoreCode, dtpSearch.Text });
            gridMain.Binding(_ds.Tables[0], true);
            gridDetail.Binding(_ds.Tables[1], true);

            LoadData.EndLoading();

        }
        

    }
}
