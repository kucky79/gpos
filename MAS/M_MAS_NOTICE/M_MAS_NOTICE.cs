using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraEditors.Controls;
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

namespace MAS
{
    public partial class M_MAS_NOTICE : BifrostFormBase
    {
        public M_MAS_NOTICE()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitEvent()
        {
            btnAddRow.Click += BtnAddRow_Click;
            btnDeleteRow.Click += BtnDeleteRow_Click;

            gridView1.InitNewRow += GridView1_InitNewRow;
        }

        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            //DataTable _dt = gridMain.DataSource as DataTable;

            view.SetRowCellValue(e.RowHandle, view.Columns["DT_NOTICE"], A.GetToday);

            view.SetRowCellValue(e.RowHandle, view.Columns["ST_NOTICE"], "A");
            view.SetRowCellValue(e.RowHandle, view.Columns["FG_NOTICE"], "N");
        }

        private void BtnDeleteRow_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.BestFitColumns();
            gridView1.UpdateCurrentRow();
        }

        private void InitControl()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();

            repositoryItemLookUpEdit1.DataSource = CH.GetCode("MAS110");
            repositoryItemLookUpEdit1.ValueMember = "CODE";
            repositoryItemLookUpEdit1.DisplayMember = "NAME";
            repositoryItemLookUpEdit1.NullText = string.Empty;
            repositoryItemLookUpEdit1.ShowNullValuePromptWhenFocused = true;
            repositoryItemLookUpEdit1.ShowLines = false;
            repositoryItemLookUpEdit1.ShowHeader = false;
            repositoryItemLookUpEdit1.ShowFooter = false;
            repositoryItemLookUpEdit1.PopupFormMinSize = new System.Drawing.Size(55, 50);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo colName1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "코드명", 10);
            repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { colName1 });


            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();

            repositoryItemLookUpEdit2.DataSource = CH.GetCode("MAS111");
            repositoryItemLookUpEdit2.ValueMember = "CODE";
            repositoryItemLookUpEdit2.DisplayMember = "NAME";
            repositoryItemLookUpEdit2.NullText = string.Empty;
            repositoryItemLookUpEdit2.ShowNullValuePromptWhenFocused = true;
            repositoryItemLookUpEdit2.ShowLines = false;
            repositoryItemLookUpEdit2.ShowHeader = false;
            repositoryItemLookUpEdit2.ShowFooter = false;
            repositoryItemLookUpEdit2.PopupFormMinSize = new System.Drawing.Size(55, 50);
            DevExpress.XtraEditors.Controls.LookUpColumnInfo colName2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "코드명", 10);
            repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { colName2 });


            gridView1.Columns["FG_NOTICE"].ColumnEdit = repositoryItemLookUpEdit1;
            gridView1.Columns["ST_NOTICE"].ColumnEdit = repositoryItemLookUpEdit2;

            gridMain.SetBinding(panelNotice, gridView1, new object[] { });

            OnView();
        }

        public override void OnView()
        {
            DataTable dt = Search(new object[] { Global.FirmCode, dtpFromS.Text, dtpToS.Text, rbtnFlag.EditValue, rbtnStatus.EditValue });
            gridMain.Binding(dt, true);

        }

        public override void OnSave()
        {
            DataTable dtChange = gridMain.GetChanges();
            if (dtChange == null)
            {
                ShowMessageBoxA("변경된 내용이 없습니다.", Bifrost.Common.MessageType.Warning);
                return;
            }

            //A.GetSlipNoCommon("MAS", )
            bool result = Save(dtChange);

            if (result)
            {
                gridMain.AcceptChanges();
                ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
            }
        }

        private void aRadioButton2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
