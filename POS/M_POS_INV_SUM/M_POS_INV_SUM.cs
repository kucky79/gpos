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
    public partial class M_POS_INV_SUM : POSFormBase
    {
        public M_POS_INV_SUM()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitControl()
        {
            dtpSearch.Text = POSGlobal.SaleDt;
            //gridViewItem.Columns["SALE_AMT"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);
            //gridViewItem.Columns["SALE_AMT"].SummaryItem.DisplayFormat = "합계 : {0:##,##0.####}";

            gridViewItem.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridViewItem.OptionsView.AllowCellMerge = true;

        }

        private void InitEvent()
        {
            btnInvSum.Click += BtnInvSum_Click;
            gridViewItem.FocusedRowChanged += GridViewMain_FocusedRowChanged;
            gridViewItem.CellMerge += GridViewItem_CellMerge;
            this.Shown += M_POS_INV_SUM_Shown;

        }

        private void M_POS_INV_SUM_Shown(object sender, EventArgs e)
        {
            OnView();
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

        private void BtnInvSum_Click(object sender, EventArgs e)
        {
            try
            {
                if (ShowMessageBoxA("기초재고량이 전부 삭제되고 재집계되어집니다.\n진행하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
                {
                    DBHelper.ExecuteNonQuery("USP_POS_INV_SUM", new object[] { POSGlobal.StoreCode, dtpSearch.Text });
                    OnView();
                }
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void GridViewMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //try
            //{
            //    if (gridViewItem.GetFocusedRow() == null) return;

            //    string ItemCode = gridViewItem.GetFocusedRowCellValue("MNU_CD").ToString();

            //    gridViewDetail.ActiveFilterString = "MNU_CD = '" + ItemCode + "'";
            //    gridViewDetail.BestFitColumns();
            //}
            //catch (Exception ex)
            //{
            //    HandleWinException(ex);
            //}
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

            if (_ds.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = "※ " + A.GetString(_ds.Tables[0].Rows[0]["TM_REG"]) + "에 최종 마감 된 내용입니다.";
            }

            LoadData.EndLoading();

        }
        

    }
}
