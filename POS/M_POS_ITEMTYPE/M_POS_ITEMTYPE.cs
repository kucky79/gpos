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
    public partial class M_POS_ITEMTYPE : POSFormBase
    {
        public M_POS_ITEMTYPE()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();


            OnView();
        }

        private void InitializeGrid()
        {
            gridViewMain.OptionsView.ShowGroupPanel = false;
            gridViewMain.OptionsView.ColumnAutoWidth = false;
            gridViewMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            //SetControl ctr = new SetControl();
            //ctr.SetCombobox(aLookUpEditModule, CH.GetPOSCode("SYS001",  true));
        }

        private void InitializeEvent()
        {
            gridViewMain.InitNewRow += GridView2_InitNewRow;
        }


        private void GridView2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            DataTable _dt = gridMain.DataSource as DataTable;

            decimal SeqNo = A.GetDecimal(_dt.Compute("MAX(NO_SEQ)", ""));
            ++SeqNo;

            decimal FlagCode = A.GetDecimal(_dt.Compute("MAX(CD_FLAG)", ""));
            ++FlagCode;

            view.SetRowCellValue(e.RowHandle, view.Columns["CD_FLAG"], A.GetString(FlagCode).PadLeft(3, '0'));

            view.SetRowCellValue(e.RowHandle, view.Columns["CD_CLAS"], "POS102");
            view.SetRowCellValue(e.RowHandle, view.Columns["YN_SYSTEM"], "Y");
            view.SetRowCellValue(e.RowHandle, view.Columns["YN_USE"], "Y");
            view.SetRowCellValue(e.RowHandle, view.Columns["ST_ROW"], "I");
            view.SetRowCellValue(e.RowHandle, view.Columns["NO_SEQ"], SeqNo);

        }

        public override void OnView()
        {
            try
            {
                object[] obj = new object[] { POSGlobal.StoreCode, string.Empty, string.Empty };
                DataSet dsSearch = Search(obj);

                gridMain.Binding(dsSearch.Tables[1], true);
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnInsert()
        {
            try
            {
                gridViewMain.AddNewRow();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnDelete()
        {
            try
            {
                if (A.GetString(gridViewMain.GetFocusedRowCellValue("ST_ROW")) == "I")
                {
                    gridViewMain.DeleteSelectedRows();
                }
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

                DataTable dtDChanges = gridMain.GetChanges();

                if (dtDChanges == null)
                {
                    ShowMessageBoxA("변경된 내용이 존재하지 않습니다.", MessageType.Information);
                    return;
                }

                bool Result = Save(dtDChanges);

                if (!Result)
                {
                    ShowMessageBoxA("저장이 실패하였습니다.", MessageType.Error);
                    return;
                }

                ShowMessageBoxA("저장이 완료되었습니다.", MessageType.Information);
                gridMain.AcceptChanges();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private bool BeforeSave()
        {
            string[] VerifyNotNullCodeL = new string[] { "CD_CLAS", "CD_FLAG", "NM_FLAG", "YN_SYSTEM", "YN_USE" };

            //if (!CheckColumn(aGrid1, VerifyNotNullCodeH)) return false;
            //if (!CheckColumn(gridMain, VerifyNotNullCodeL)) return false;

            return true;
        }
    }
}
