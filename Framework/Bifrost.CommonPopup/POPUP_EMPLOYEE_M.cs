using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NF.A2P;
using NF.A2P.Helper;
using NF.Framework.Common;
using NF.Framework.Win;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_EMPLOYEE_M : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Menucode = "";

        public string Menucode
        {
            get { return _Menucode; }
            set { _Menucode = value; }
        }
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

        public POPUP_EMPLOYEE_M()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_EMPLOYEE_M(string SearchCode)
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
            aButton_Search.Click += AButton_Search_Click;
            aTextEdit1.KeyDown += TxtSearch_KeyDown;

        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnSearch();
            }
        }

        private void InitForm()
        {
            PopupTitle = "EMPLOYEE POPUP";

            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.MultiSelect = true;
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
                
                //aLookUpEdit_Dept.EditValue
                dt = DBHelper.GetDataTable("AP_H_MAS_EMPLOYEE_M_S", new object[] { Global.FirmCode, aLookUpEdit_Office.EditValue, aTextEdit1.Text, aCodeText_Dept.CodeValue, aLookUpEdit3.EditValue,  "P" , Menucode , Global.UserID});
                aGrid1.Binding(dt);

                if (this.aGrid1.DefaultView.RowCount > 0)
                {
                    if (!this.aGrid1.Focused)
                    {
                        this.aGrid1.Focus();
                    }
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
                    string StrCdemp = "";
                    string StrNmemp = "";

                    ArrayList Arrcdemp = new ArrayList();
                    ArrayList Arrnmemp = new ArrayList();
                    gridView1.Columns["CD_EMP"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        Arrcdemp.Add(A.GetString(gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "CD_EMP")));
                        Arrnmemp.Add(A.GetString(gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], "NM_EMP")));

                    }
                    if (Arrcdemp.Count > 0)
                    {
                        StrCdemp = string.Join("|", Arrcdemp.ToArray());
                    }
                    if (Arrnmemp.Count > 0)
                    {
                        StrNmemp = string.Join(", ", Arrnmemp.ToArray());
                    }
                    ReturnData.Add("CD_EMP_U", StrCdemp);
                    ReturnData.Add("NM_EMP_U", StrNmemp);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    ShowMessageBox(this.ResReader.GetString("M02497"), MessageType.Information);
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
        private void AButton_Search_Click(object sender, EventArgs e)
        {
            OnSearch();
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
