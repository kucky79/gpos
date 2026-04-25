using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NF.A2P.Helper;
using NF.Framework.Common;
using NF.Framework.Win;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_PTITLE : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Ym = "";
        public string Ym
        {
            get { return _Ym; }
            set { _Ym = value; }
        }

        public string SearchCondition
        {
            set
            {
                //this.OnSearch();

                //aTextEdit1.Text = value;

                //if (aTextEdit1.Text.Length > 1)
                //{
                //    this.OnSearch();
                //}
            }
        }

        public POPUP_PTITLE()
        {
            InitializeComponent();
            InitForm();
            InitializeGrid();
            InitEvent();
        }

        public POPUP_PTITLE(string Strym)
        {
            InitializeComponent();
            InitForm();
            InitializeGrid();
            InitEvent();

            Ym = Strym;
        }

        private void InitializeGrid()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            #region HEADER

            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riMaskedTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            riMaskedTextEdit.Mask.EditMask = "####/##/##";
            riMaskedTextEdit.Mask.MaskType = MaskType.Simple;
            riMaskedTextEdit.Mask.SaveLiteral = false;
            riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true;
            riMaskedTextEdit.Mask.ShowPlaceHolders = false;
            riMaskedTextEdit.Mask.AutoComplete = AutoCompleteType.Optimistic;
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riMaskedTextEditYYMM = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            riMaskedTextEditYYMM.Mask.EditMask = "####/##";
            riMaskedTextEditYYMM.Mask.MaskType = MaskType.Simple;
            riMaskedTextEditYYMM.Mask.SaveLiteral = false;
            riMaskedTextEditYYMM.Mask.UseMaskAsDisplayFormat = true;
            riMaskedTextEditYYMM.Mask.ShowPlaceHolders = false;
            riMaskedTextEditYYMM.Mask.AutoComplete = AutoCompleteType.Optimistic;
            repositoryItemCheckEdit1.ValueChecked = "Y";
            repositoryItemCheckEdit1.ValueUnchecked = "N";


            DevExpress.XtraGrid.Columns.GridColumn col_B1 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B2 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B3 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B4 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B5 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B6 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B7 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B8 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B9 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B10 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B11 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B12 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B13 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B14 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn col_B15 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col_B1, col_B2, col_B3, col_B4, col_B5, col_B6, col_B7, col_B10,
                col_B11, col_B14, col_B15});

            col_B1.FieldName = "CD_FIRM";
            col_B1.Visible = false;

            col_B2.Caption = "년월";
            col_B2.FieldName = "YM";
            col_B2.Width = 100;
            col_B2.VisibleIndex = 0;
            col_B2.ColumnEdit = riMaskedTextEditYYMM;
            col_B2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            col_B3.Caption = "사원구분";
            col_B3.FieldName = "FG_HIRE";
            col_B3.Width = 100;
            col_B3.VisibleIndex = 1;
            col_B3.ColumnEdit = CH.SetGridLookUpItem(CH.GetCode("HRS002"));
            col_B3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //col_B4.OptionsColumn.AllowEdit = false;


            col_B4.Caption = "급여구분";
            col_B4.FieldName = "TP_PAY";
            col_B4.Width = 100;
            col_B4.VisibleIndex = 2;
            col_B4.ColumnEdit = CH.SetGridLookUpItem(CH.GetCode("HRS405"));
            col_B4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            col_B5.Caption = "순번";
            col_B5.FieldName = "NO_SEQ";
            col_B5.Width = 100;
            col_B5.VisibleIndex = 3;
            col_B5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            col_B6.Caption = "급상여";
            col_B6.FieldName = "CD_PB";
            col_B6.Width = 100;
            col_B6.VisibleIndex = 4;
            col_B6.ColumnEdit = CH.SetGridLookUpItem(CH.GetCode("HRS404"));
            col_B6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            col_B7.Caption = "지급일자";
            col_B7.FieldName = "DT_PAY";
            col_B7.Width = 100;
            col_B7.VisibleIndex = 5;
            col_B7.ColumnEdit = riMaskedTextEdit;
            col_B7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            col_B8.Caption = "급여기간FROM";
            col_B8.FieldName = "DT_PAY_F";
            col_B8.Width = 100;
            col_B8.VisibleIndex = 6;
            col_B8.ColumnEdit = riMaskedTextEdit;
            col_B8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            col_B9.Caption = "급여기간TO";
            col_B9.FieldName = "DT_PAY_T";
            col_B9.Width = 100;
            col_B9.VisibleIndex = 7;
            col_B9.ColumnEdit = riMaskedTextEdit;
            col_B9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            col_B10.Caption = "세금시작월";
            col_B10.FieldName = "YM_START";
            col_B10.Width = 100;
            col_B10.VisibleIndex = 8;
            col_B10.ColumnEdit = riMaskedTextEditYYMM;
            col_B10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            col_B11.Caption = "세금종료월";
            col_B11.FieldName = "YM_END";
            col_B11.Width = 100;
            col_B11.VisibleIndex = 9;
            col_B11.ColumnEdit = riMaskedTextEditYYMM;
            col_B11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            col_B12.Caption = "급여TITLE";
            col_B12.FieldName = "DC_TITLE";
            col_B12.Width = 100;
            col_B12.VisibleIndex = 10;
            col_B12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            col_B13.Caption = "동월세금포함여부";
            col_B13.FieldName = "YN_STAX";
            col_B13.Width = 100;
            col_B13.VisibleIndex = 11;
            col_B13.ColumnEdit = repositoryItemCheckEdit1;
            col_B13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            col_B14.Caption = "상여포함여부(출력)";
            col_B14.FieldName = "YN_INBONUS";
            col_B14.Width = 100;
            col_B14.VisibleIndex = 12;
            col_B14.ColumnEdit = repositoryItemCheckEdit1;
            col_B14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            col_B15.Caption = "마감일자";
            col_B15.FieldName = "DT_CLOSE";
            col_B15.Width = 100;
            col_B15.VisibleIndex = 13;
            col_B15.ColumnEdit = riMaskedTextEdit;
            col_B15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            //col_B10.Caption = "연봉총액";
            //col_B10.FieldName = "AM_TOTAL";
            //col_B10.Width = 100;
            //col_B10.VisibleIndex = 8;
            //col_B10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //col_B10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //col_B10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //col_B10.OptionsColumn.AllowEdit = false;


            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            //gridView1.OptionsSelection.MultiSelect = true;


            #endregion

            aGridHelper.SetGridStyle(aGrid1, false, false, false);

        }
        private void InitEvent()
        {
            aGrid1.KeyDown += AGrid1_KeyDown;
            gridView1.DoubleClick += GridView1_DoubleClick;
            aGrid1.ProcessGridKey += _GridM_ProcessGridKey;
            aButton_Search.Click += AButton_Search_Click;
        }

        private void AButton_Search_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void _GridM_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void InitForm()
        {
            PopupTitle = "급여TITLE 도움창";

            SetControl ctr = new SetControl();

            ctr.SetCombobox(aLookUpEdit_Fghire, CH.GetCode("HRS002", true), false);
            ctr.SetCombobox(aLookUpEdit_Tppay, CH.GetCode("HRS405", true), false);
            aDateMonth_Ymfrom.Text = A.GetToday.Substring(0, 4) + "01";
            aDateMonth_Ymto.Text = A.GetToday.Substring(0, 6);


        }

        protected override void OnSearch()
        {
            try
            {
                string Strym = Ym;

                dt = DBHelper.GetDataTable("AP_HRS_PTITLE_POP_S", new object[] { Global.FirmCode, aDateMonth_Ymfrom.Text, aDateMonth_Ymto.Text, aLookUpEdit_Tppay.EditValue, aLookUpEdit_Fghire.EditValue });
                aGrid1.Binding(dt);

                if (this.aGrid1.DefaultView.RowCount > 0)
                {
                    if (!this.aGrid1.Focused)
                    {
                        this.aGrid1.Select();
                    }
                    //gridView1.SelectRow(gridView1.FocusedRowHandle);

                }
                else
                {
                    ShowMessageBoxA("The data not found. Please search again.", MessageType.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            try
            {
                if (aGrid1.MainView.RowCount > 0)
                {
                    DataTable gridDT = (DataTable)aGrid1.DataSource;
                    ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGrid1.MainView).GetSelectedRows()[0]]);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {

                    ShowMessageBoxA("No items selected.\r\nSearch again and select the item on the list.", MessageType.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnCancel()
        {
            try
            {
                this.Close();

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void AGrid1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                Point pt = view.GridControl.PointToClient(Control.MousePosition);
                DoRowDoubleClick(view, pt);
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
                OnOK();
            }
        }
    }
}
