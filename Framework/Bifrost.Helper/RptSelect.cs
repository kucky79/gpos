using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Bifrost.Win;

namespace Bifrost.Helper
{
    public partial class RptSelect : PopupBase
    {
        DataTable dt;
        public RptSelect(string MenuCode)
        {
            InitializeComponent();
            InitForm(MenuCode);
            InitEvent();
        }

        public void InitForm(string MenuCode)
        {
            try
            {
                dt = DBHelper.GetDataTable("AP_SYS_REPORT_CONFIG_S", new object[] { Global.FirmCode, MenuCode });
                
                #region master

                DevExpress.XtraGrid.Columns.GridColumn col_M1 = new DevExpress.XtraGrid.Columns.GridColumn();
                DevExpress.XtraGrid.Columns.GridColumn col_M2 = new DevExpress.XtraGrid.Columns.GridColumn();
                gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col_M1, col_M2 });

                col_M1.Caption = "NO";
                col_M1.FieldName = "NO_LINE";
                col_M1.Width = 30;
                col_M1.VisibleIndex = 0;

                col_M2.Caption = "Report Name";
                col_M2.FieldName = "NM_REPORT";
                col_M2.Width = 100;
                col_M2.VisibleIndex = 1;
                col_M2.BestFit();

                #endregion

                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsCustomization.AllowSort = false;

                aGrid1.DataSource = dt;
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;

                CenterToParent();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        public void InitEvent()
        {
            gridView1.DoubleClick += GridView1_DoubleClick;
            Activated += ChoiceRpt_Activated;
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

        private void DoRowDoubleClick(GridView view, Point pt)
        {

            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                object val = view.GetRowCellValue(info.RowHandle, info.Column);
                if (val != null)
                {
                    ReturnData = view.GetDataRow(info.RowHandle);
                    //MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}, value: {2}", info.RowHandle, colCaption, val));
                    OnOK();
                }
            }
        }

        #region Form & Grid Events' handlers

        private void ChoiceRpt_Activated(object sender, EventArgs e)
        {
            if(this.gridView1.RowCount > 0)
            {
                if (!this.aGrid1.Focused)
                    this.aGrid1.Focus();
            }
        }

       
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnOK();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void OnOK()
        {
            ReturnData = _ReturnData;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        DataRow _ReturnData;

        /// <summary>
        /// Get the data return on Enter
        /// </summary>
        public DataRow ReturnData
        {
            get { return _ReturnData; }
            set { _ReturnData = value; }
        }

        #endregion Form & Grid Events' handlers

        private void ChoiceRpt_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }



     
    }
}
