using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bifrost;
using Bifrost.Helper;
using Bifrost.Common;
using Bifrost.Win;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Bifrost.Adv.Controls.aPopUp
{
    public partial class POPUP_EMPLOYEE : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Code = "";

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string SearchCondition
        {
            set
            {
                //this.OnSearch();

                aTextEdit1.Text = value;

                if (aTextEdit1.Text.Length > 1)
                {
                    this.OnSearch();
                }
            }
        }

        public POPUP_EMPLOYEE()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_EMPLOYEE(string SearchCode)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            SearchCondition = SearchCode;
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
            PopupTitle = "EMPLOYEE POPUP";

            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowGroupPanel = false;

            gridColumn1.Width = 100;
            gridColumn2.Width = 170;
            gridColumn3.Width = 170;
            gridColumn4.Width = 200;

            SetControl ctr = new SetControl();
            ctr.SetCombobox(aLookUpEdit_Office, CH.GetCode("MAS_BIZNM", true), false);
            ctr.SetCombobox(aLookUpEdit3, CH.GetCode("HRS003", true), false);
            //ctr.SetCombobox(aLookUpEdit_Dept, CH.GetCode("MAS_DEPT", true), false);

            aLookUpEdit_Office.Properties.DropDownRows = 15;
            aLookUpEdit_Office.Properties.ShowLines = false;
            aLookUpEdit_Office.Properties.ShowHeader = false;
            aLookUpEdit_Office.Properties.ShowFooter = false;
            aLookUpEdit_Office.EditValue = Global.BizCode;

            aLookUpEdit3.Properties.ShowLines = false;
            aLookUpEdit3.Properties.ShowHeader = false;
            aLookUpEdit3.Properties.ShowFooter = false;

        }

        protected override void OnSearch()
        {
            try
            {
                string ClassCode = Code;
                
                dt = DBHelper.GetDataTable("AP_H_MAS_EMPLOYEE_S", new object[] { Global.FirmCode, aLookUpEdit_Office.EditValue, aTextEdit1.Text, aCodeText_Dept.CodeValue, aLookUpEdit3.EditValue, "P" });
                aGrid1.Binding(dt);

                if (this.aGrid1.DefaultView.RowCount > 0)
                {
                    if (!this.aGrid1.Focused)
                    {
                        this.aGrid1.Select();
                    }
                    gridView1.SelectRow(gridView1.FocusedRowHandle);

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
                    ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGrid1.MainView).GetFocusedDataSourceRowIndex()]);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    aTextEdit1.Select();

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

                this.DialogResult = DialogResult.Cancel;
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

            if(info.InRow || info.InRowCell)
            {
                OnOK();
            }
        }
    }
}
