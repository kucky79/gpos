using Bifrost;
using Bifrost.Win;
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
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
    public partial class M_POS_PRINT : POSFormBase
    {
        public M_POS_PRINT()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            SetGridInit(gridView1);
            SetGridInit(gridView2);
        }

        private void SetGridInit(GridView view)
        {
            view.OptionsView.ShowGroupPanel = false;
            //view.OptionsView.ColumnAutoWidth = false;
            view.OptionsView.ShowAutoFilterRow = false;
            view.OptionsCustomization.AllowSort = true;
            view.OptionsCustomization.AllowFilter = false;

            view.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;
            view.RowCountChanged += GridView_RowCountChanged;
            view.RowCellStyle += GridView_RowCellStyle;

            //ViewRepositoryCollection viewCollection = gridItem.ViewCollection;
            //foreach (GridView gView in viewCollection)
            {
                //항번추가 이벤트
                view.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;
                //앞에 인디케이터 AutoWidth 이벤트
                view.RowCountChanged += GridView_RowCountChanged;
                //로우스타일
                view.RowCellStyle += GridView_RowCellStyle;
            }

            //그리드 높이
            view.UserCellPadding = new Padding(0, 5, 0, 5);

        }

        public override void OnView()
        {
            DataSet ds = Search(new object[] { POSGlobal.StoreCode });

            gridH.DataSource = ds.Tables[0];
            gridL.DataSource = ds.Tables[1];
        }

        public override void OnSave()
        {
            DataTable dtH = gridH.DataSource as DataTable;
            DataTable dtL = gridL.DataSource as DataTable;
            DataTable dtHChange = dtH.GetChanges();
            DataTable dtLChange = dtL.GetChanges();

            bool result = Save(dtHChange, dtLChange);

            if(result)
            {
                ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
                if (dtHChange != null) dtHChange.AcceptChanges();
                if (dtLChange != null) dtLChange.AcceptChanges();
            }
        }

        #region Grid No Setting
        private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void GridView_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = ((GridView)sender);

            if (!view.GridControl.IsHandleCreated) return;

            object obj = view.DataSource;
            int _cnt = 0;

            Graphics gr = Graphics.FromHwnd(view.GridControl.Handle);
            SizeF size;

            switch (obj.GetType().Name)
            {
                case nameof(DataView):
                    DataView _dv = view.DataSource as DataView;
                    _cnt = _dv.Count;
                    size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                    view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + 10;
                    break;

                case nameof(DataTable):
                    DataTable _dt = view.DataSource as DataTable;
                    _cnt = _dt.Rows.Count;
                    size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                    view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + 10;
                    break;

                case nameof(DataSet):
                    DataSet _ds = view.DataSource as DataSet;
                    for (int i = 0; i < _ds.Tables.Count; i++)
                    {
                        _cnt = _ds.Tables[i].Rows.Count;
                        size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                        view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + 10;
                    }
                    break;
            }
        }

        private void GridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.OptionsColumn.AllowEdit == false)
            {
                //Apply the appearance of the SelectedRow
                //e.Appearance.Assign(view.PaintAppearance.SelectedRow);
                //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
                e.Appearance.Options.UseForeColor = true;
                e.Appearance.ForeColor = Color.Gray;
            }
        }
        #endregion Grid No Setting
    }
}
