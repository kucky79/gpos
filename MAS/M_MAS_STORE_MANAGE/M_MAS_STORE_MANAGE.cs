using Bifrost;
using Bifrost.Helper;
using Bifrost.Win;
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
    public partial class M_MAS_STORE_MANAGE : BifrostFormBase
    {
        public M_MAS_STORE_MANAGE()
        {
            InitializeComponent();
            InitControl();
            InitEvent();
        }

        private void InitEvent()
        {
            btnCreateStore.Click += BtnCreateStore_Click;
            btnAddRow.Click += BtnAddRow_Click;

            this.Load += M_MAS_STORE_MANAGE_Load;

            gridViewStore.RowCellStyle += GridViewStore_RowCellStyle;
        }

        private void GridViewStore_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            string UseYN = view.GetRowCellDisplayText(e.RowHandle, view.Columns["YN_USE"]);

            if (e.RowHandle > 0)
            {
                if (UseYN == "N")
                {
                    e.Appearance.Options.UseForeColor = true;
                    e.Appearance.ForeColor = Color.Crimson;
                }
            }
        }

        private void M_MAS_STORE_MANAGE_Load(object sender, EventArgs e)
        {
            OnView();
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            gridViewStore.AddNewRow();
            gridViewStore.UpdateCurrentRow();


            aLookUpEditArea.Enabled = true;
            aLookUpEditItem.Enabled = true;

            btnCreateStore.Enabled = true;
        }

        private void BtnCreateStore_Click(object sender, EventArgs e)
        {
            string AreaCode = A.GetString(aLookUpEditArea.EditValue);
            string BizType = A.GetString(aLookUpEditItem.EditValue);

            if (AreaCode == string.Empty || BizType == string.Empty)
            {
                ShowMessageBoxA("선택된 항목이 없습니다.\n항목을 선택해주세요", Bifrost.Common.MessageType.Warning);
                return;
            }

            int MaxStoreNo = A.GetInt(DBHelper.ExecuteScalar("USP_GET_MAX_STORE", new object[] { AreaCode + BizType }));

            string StoreCode = AreaCode + BizType + (MaxStoreNo + 1).ToString().PadLeft(4, '0');

            DBHelper.ExecuteNonQuery("USP_CREATE_STORE", new object[] { StoreCode });

            txtStoreCode.Text = StoreCode;
            txtStoreName.Text = "신규";
        }

        private void InitControl()
        {
            SetControl scr = new SetControl();
            scr.SetCombobox(aLookUpEditArea, CH.GetCode("MAS100"));
            scr.SetCombobox(aLookUpEditItem, CH.GetCode("MAS101"));

            scr.SetCombobox(aLookUpEditSearchArea, CH.GetCode("MAS100", true));
            scr.SetCombobox(aLookUpEditSearchBizItem, CH.GetCode("MAS101", true));

            aGridStore.SetBinding(panelMain, gridViewStore, new object[] { txtStoreCode });

        }

        public override void OnView()
        {
            aGridStore.Binding(Search(new object[] { aLookUpEditSearchArea.EditValue, aLookUpEditSearchBizItem.EditValue, txtSearch.Text }), true);
        }

        public override void OnSave()
        {
            DataTable dtChange = aGridStore.GetChanges();

            if(dtChange != null)
            {
                bool result = Save(dtChange);
                if(result)
                {
                    aGridStore.AcceptChanges();
                    ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
                }
            }

        }
    }
}