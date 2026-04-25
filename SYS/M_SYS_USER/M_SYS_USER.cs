using System;
using System.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Mask;
using System.Drawing;
using System.Windows.Forms;
using Bifrost.Win;
using Bifrost.Helper;
using DevExpress.XtraGrid.Views.Grid;
using Bifrost;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bifrost.Common;

namespace SYS
{
    public partial class M_SYS_USER : BifrostFormBase
    {
        private M_SYS_USER_D _biz = null;
        public M_SYS_USER()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeControl();
            InitializeEvent();
            _biz = new M_SYS_USER_D();
        }

        private void InitializeGrid()
        {
            #region Main Grid
            aGridMain.AddNewRowLastColumn = false;

            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;

            //RepositoryItemLookUpEdit repositoryItemLookUpEdit1 = new RepositoryItemLookUpEdit();
            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            RepositoryItemButtonEdit riButton1 = new RepositoryItemButtonEdit();

            gridColumn2.ColumnEdit = repositoryItemTextEdit1;
            repositoryItemTextEdit1.UseSystemPasswordChar = true;

            riButton1.ButtonClick += RiButton1_ButtonClick;
            gridColumn7.ColumnEdit = riButton1;

            //gridColumn8.ColumnEdit = repositoryItemLookUpEdit1;
            //gridColumn8.ColumnEdit = SetGridLookUpItem(CH.GetCode("SYS002", true));

            ////gridColumn9.ColumnEdit = repositoryItemLookUpEdit1;
            //gridColumn9.ColumnEdit = CH.SetGridLookUpItem(CH.GetCode("SYS002", true));

            
            ////gridColumn10.ColumnEdit = repositoryItemLookUpEdit1;
            //gridColumn10.ColumnEdit = CH.SetGridLookUpItem(CH.GetCodeRe("SYS_GPMENUH", true));

            //ColumnSetting(ref col_Main1, "User ID", "CD_USER", true, 100);
            //ColumnSetting(ref col_Main2, "Password", "NO_PWD", true, 100);
            //ColumnSetting(ref col_Main3, "User Name", "NM_USER", true, 140);
            //ColumnSetting(ref col_Main14, "TEL NO.", "NO_TEL", true, 110);
            //ColumnSetting(ref col_Main15, "FAX NO.", "NO_FAX", true, 110);
            //ColumnSetting(ref col_Main16, "EMAIL", "DC_EML", true, 170);
            //ColumnSetting(ref col_Main4, "사용자구분", "FG_USER", true, 80);
            //ColumnSetting(ref col_Main5, "사원명", "NM_EMP", true, 140);
            //ColumnSetting(ref col_Main6, "거래처", "NM_CUST", false);
            //ColumnSetting(ref col_Main7, "사용유무", "YN_USE", true, 80);
            //ColumnSetting(ref col_Main8, "그룹메뉴사용유무", "YN_GPMENU", true, 100);
            //ColumnSetting(ref col_Main9, "메뉴그룹", "CD_GPMENU", true, 140);
            //ColumnSetting(ref col_Main10, "CD_FIRM", "CD_FIRM", false);
            //ColumnSetting(ref col_Main11, "FG_USER", "FG_USER", false);
            //ColumnSetting(ref col_Main12, "YN_FMSG_RCV", "YN_FMSG_RCV", false);
            //ColumnSetting(ref col_Main13, "CD_EMP", "CD_EMP", false);
            //ColumnSetting(ref col_Main7, "메일아이디", "CD_GPMENU", false);
            //ColumnSetting(ref col_Main7, "메일비밀번호", "CD_GPMENU", false);

            #endregion

            #region Sub Grid

            RepositoryItemLookUpEdit repositoryItemLookUpEditServer = new RepositoryItemLookUpEdit();
            //repositoryItemLookUpEditServer.QueryCloseUp += RepositoryItemLookUpEditServer_QueryCloseUp;
            RepositoryItemCheckEdit repositoryItemCheckEditServer = new RepositoryItemCheckEdit();
            repositoryItemCheckEditServer.ValueChecked = "Y";
            repositoryItemCheckEditServer.ValueUnchecked = "N";
            repositoryItemCheckEditServer.ValueGrayed = "";

            #endregion
        }

        private void InitializeControl()
        {
            SetControl ctr = new SetControl();
            ctr.SetCombobox(aLookUpEditUseYN, CH.GetCode("SYS002", true), false);
        }

        private void InitializeEvent()
        {
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            gridView1.InitNewRow += GridView1_InitNewRow;
            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.DoubleClick += GridView1_DoubleClick;
            gridView1.RowCellClick += GridView1_RowCellClick;

        }

        private void RiButton1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Callpopup(gridView1, gridView1.FocusedColumn.FieldName, gridView1.FocusedRowHandle);
        }
       
        private void Checkpopup(GridView view, string Fieldname, int Rowhandle, string StrValue)
        {
            if (A.GetString(view.GetRowCellValue(Rowhandle, Fieldname)) == "")
                return;

            DataTable dt = new DataTable();

            switch (Fieldname)
            {
                case "NM_EMP":

                    dt = DBHelper.GetDataTable("AP_MAS_EMP_S", new object[] { Global.FirmCode, "", "", "", StrValue });
                    if (dt.Rows.Count == 1)
                    {
                        view.CellValueChanged -= GridView1_CellValueChanged;
                        view.SetRowCellValue(Rowhandle, "CD_EMP", dt.Rows[0]["CD_EMP"]);
                        view.SetRowCellValue(Rowhandle, "NM_EMP", dt.Rows[0]["NM_EMP"]);
                        view.CellValueChanged += GridView1_CellValueChanged;
                    }
                    else
                    {
                        view.SetRowCellValue(Rowhandle, "CD_EMP", "");
                        view.SetRowCellValue(Rowhandle, "NM_EMP", "");
                        //Callpopup(view, Fieldname, Rowhandle);
                    }
                    break;
                case "NM_CUST":

                    dt = DBHelper.GetDataTable("AP_MAS_CUST_S", new object[] { Global.FirmCode,  "", StrValue });
                    if (dt.Rows.Count == 1)
                    {
                        view.CellValueChanged -= GridView1_CellValueChanged;
                        view.SetRowCellValue(Rowhandle, "CD_CUST", dt.Rows[0]["CD_CUST"]);
                        view.SetRowCellValue(Rowhandle, "NM_CUST", dt.Rows[0]["NM_CUST"]);
                        view.CellValueChanged += GridView1_CellValueChanged;
                    }
                    else
                    {
                        //Callpopup(view, Fieldname, Rowhandle);
                    }
                    break;
            }
        }


        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridControl = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            try
            {
                GridView view = sender as GridView;
                switch (e.Column.FieldName)
                {
                    case "NM_EMP":
                        Checkpopup(view, view.FocusedColumn.FieldName, view.FocusedRowHandle, A.GetString(e.Value));
                        break;
                    case "NO_PWD":


                        gridView1.CellValueChanged -= GridView1_CellValueChanged;
                        view.SetRowCellValue(e.RowHandle, view.Columns["NO_PWD"], A.GetString(e.Value));
                        gridView1.CellValueChanged += GridView1_CellValueChanged;

                        break;
                    default:
                        break;
                }
                view.UpdateCurrentRow();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

        private void GridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, "CD_FIRM", Global.FirmCode);   //Office Code
                view.SetRowCellValue(e.RowHandle, "YN_USE", "Y");
                
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                //Callpopup(view, info.Column.FieldName, info.RowHandle);
            }
        }
        
        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle < 0)
                    return;
                if (gridView1.GetDataRow(e.FocusedRowHandle).RowState != DataRowState.Added && gridView1.GetDataRow(e.FocusedRowHandle).RowState != DataRowState.Detached)
                {
                    gridView1.Columns["CD_USER"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    gridView1.Columns["CD_USER"].OptionsColumn.AllowEdit = true;
                }


                if (gridView1.GetFocusedRow() == null) return;

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
                object[] obj = new object[] { Global.FirmCode, aLookUpEditUseYN.EditValue, aTextEdit1.Text };

                DataTable dt = _biz.Search(obj);
                aGridMain.Binding(dt);
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
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (A.GetString(gridView1.GetRowCellValue(i, gridView1.Columns["CD_USER"])) == "")
                    {
                        ShowMessageBoxA("진행중인 건이 있습니다.", MessageType.Warning);
                        return;
                    }
                }
                gridView1.AddNewRow();
                gridView1.UpdateCurrentRow();
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
                DataTable dtChanges = aGridMain.GetChanges();

                if (ChkBeforeSave(dtChanges)) { 
                    if(dtChanges == null)
                    {
                        ShowMessageBox("변경된 내용이 존재하지 않습니다.", MessageType.Information);
                        return;
                    }
                }else
                {
                    return;
                }

                bool result = _biz.Save(dtChanges);

                if (!result)
                {
                    ShowMessageBox("저장 실패했습니다.", MessageType.Error);
                    return;
                }

                ShowMessageBox("저장이 완료되었습니다.", MessageType.Information);
                aGridMain.AcceptChanges();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public override void OnAddRow()
        {
            try
            {
                gridView1.AddNewRow();
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
                gridView1.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
               HandleWinException(ex);
            }
        }

        private bool ChkBeforeSave(DataTable dtChanges)
        {
            if (dtChanges == null)
            {
                return false;
            }

            string[] VerifyNotNull = new string[] { "CD_USER", "NO_PWD", "NM_USER", "NM_EMP" };

            foreach (DataRow row in dtChanges.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                foreach (string item in VerifyNotNull)
                {
                    if (row[item] == null || row[item].ToString() == "")
                    {
                        ShowMessageBoxA("'" + gridView1.Columns[item].Caption + "'은 필수 항목입니다.", MessageType.Warning);
                        return false;
                    }
                }
            }

            return true;
        }

        private void GridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var hitInfo = gridView1.CalcHitInfo(e.Location);
            if (hitInfo == null || hitInfo.Column != gridColumn12) return;

            if (ShowMessageBox("암호 초기화를 하시겠습니까?", MessageType.Question) == DialogResult.Yes)
            {
                string cdUser = A.GetString(gridView1.GetFocusedDataRow()["CD_USER"]);
                object[] obj = new object[] { Global.FirmCode, cdUser };

                bool result = _biz.resetPassword(obj);

                if (!result)
                {
                    ShowMessageBox("초기화가 실패했습니다.", MessageType.Error);
                    return;
                }

                ShowMessageBox("초기화되었습니다.", MessageType.Information);
            }
            else return;
        }
    }
}
