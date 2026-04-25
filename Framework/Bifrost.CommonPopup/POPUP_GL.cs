using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using NF.A2P;
using NF.A2P.Helper;
using NF.Framework.Common;
using NF.Framework.Win;
using System;
using System.Data;
using System.Windows.Forms;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_GL : PopupBase
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

        public POPUP_GL()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_GL(string SearchCode)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            SearchCondition = SearchCode;
        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "GL CODE";

                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsView.ShowGroupPanel = false;

                gridColumn1.Width = 80;
                gridColumn2.Width = 70;
                gridColumn3.Width = 110;
                gridColumn4.Width = 250;
                gridColumn5.Width = 140;
                gridColumn6.Width = 150;

                SetControl ctr = new SetControl();
                ctr.SetCombobox(aLookUpEdit1, CH.GetCode("MAS011", true), false);
                ctr.SetCombobox(aLookUpEdit2, CH.GetCode("MAS_BIZ", true), false);

                aLookUpEdit1.Properties.DropDownRows = 15;
                aLookUpEdit1.Properties.ShowLines = false;
                aLookUpEdit1.Properties.ShowHeader = false;
                aLookUpEdit1.Properties.ShowFooter = false;

                aLookUpEdit2.Properties.DropDownRows = 15;
                aLookUpEdit2.Properties.ShowLines = false;
                aLookUpEdit2.Properties.ShowHeader = false;
                aLookUpEdit2.Properties.ShowFooter = false;

                CenterToParent();

                if (this.AutoSearch)
                {
                    this.OnSearch();
                }

                aGrid1.ForceInitialize();
                aGrid1.Select();

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }

        }

        private void InitEvent()
        {
            aGrid1.KeyDown += new KeyEventHandler(AGrid1_KeyDown);
            gridView1.DoubleClick += new EventHandler(GridView1_DoubleClick);
        }

        protected override void OnSearch()
        {
            try
            {
                string ClassCode = Code;

                dt = DBHelper.GetDataTable("AP_H_MAS_GL_S", new object[] { Global.FirmCode, aTextEdit1.Text, aTextEdit2.Text, aLookUpEdit1.EditValue, aLookUpEdit2.EditValue, "P" });
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
            if (aGrid1.MainView.RowCount > 0)
            {
                DataTable gridDT = (DataTable)aGrid1.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGrid1.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBox(this.ResReader.GetString("M02497"), MessageType.Information);
            }
        }

        protected override void OnCancel()
        {
            this.Close();
        }

        private void AGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }

        
    }
}
