using Bifrost;
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraGrid.Views.Base;
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
    public partial class M_POS_PRINT_CONFIG : POSFormBase
    {
        public M_POS_PRINT_CONFIG()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();

        }

        private void InitializeGrid()
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView2.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView2.OptionsView.ColumnAutoWidth = false;
            gridView2.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            //SetControl ctr = new SetControl();
            //ctr.SetCombobox(aLookUpEditModule, CH.GetPOSCode("SYS001",  true));
        }

        private void InitializeEvent()
        {
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView2.InitNewRow += GridView2_InitNewRow;
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
        }

        private void GridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //GridView view = sender as GridView;
            //view.SetRowCellValue(e.RowHandle, view.Columns["YN_USE"], "Y");
        }

        private void GridView2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, view.Columns["TP_PRINT"], gridView1.GetFocusedRowCellValue("TP_PRINT"));
            view.SetRowCellValue(e.RowHandle, view.Columns["YN_USE"], "Y");
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridView1.GetFocusedRow() == null) return;

                string PrintType = gridView1.GetFocusedRowCellValue("TP_PRINT").ToString();

                gridView2.ActiveFilterString = "TP_PRINT = '" + PrintType + "'";
                gridView2.BestFitColumns();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnView()
        {
            try
            {
                object[] obj = new object[] { POSGlobal.StoreCode, txtSearch.Text };
                DataSet dsSearch = Search(obj);

                gridDtetail.Binding(dsSearch.Tables[1], true);
                gridMain.Binding(dsSearch.Tables[0], true);

                //if (gridView1.RowCount == 0)
                //{
                //    ShowMessageBoxA("The data not found. Please search again.", MessageType.Information);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                if (!BeforeSave()) return;

                DataTable dtHChanges = gridMain.GetChanges();
                DataTable dtDChanges = gridDtetail.GetChanges();

                if (dtHChanges == null && dtDChanges == null)
                {
                    ShowMessageBoxA("변경된 내용이 존재하지 않습니다.", MessageType.Information);
                    return;
                }

                bool Result = Save(dtHChanges, dtDChanges);

                if (!Result)
                {
                    ShowMessageBoxA("저장이 실패하였습니다.", MessageType.Error);
                    return;
                }

                ShowMessageBoxA("저장이 완료되었습니다.", MessageType.Information);
                gridMain.AcceptChanges();
                gridDtetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private bool BeforeSave()
        {
            //string[] VerifyNotNullCodeH = new string[] { "CD_MODULE", "CD_CLAS", "NM_CLAS", "YN_SYSTEM", "YN_USE" };
            //string[] VerifyNotNullCodeL = new string[] { "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE" };

            //if (!CheckColumn(aGrid1, VerifyNotNullCodeH)) return false;
            //if (!CheckColumn(aGrid2, VerifyNotNullCodeL)) return false;

            return true;
        }
    }
}
