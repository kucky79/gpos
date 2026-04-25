using DevExpress.XtraGrid.Views.Grid;
using NF.A2P;
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
    public partial class POPUP_BANK : PopupBase
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

        public POPUP_BANK()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_BANK(string SearchCode)
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
            aTextEdit1.KeyDown += TxtSearch_KeyDown;
            aTextEdit2.KeyDown += TxtSearch_KeyDown;

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
            try
            {
                PopupTitle = "BANK CODE";

                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsView.ShowGroupPanel = false;

                gridColumn_BankCd.Width = 100;
                gridColumn_BizCd.Width = 70;
                gridColumn_BankNm.Width = 200;
                gridColumn_GlCd.Width = 80;
                gridColumn_InitAm.Width = 120;
                gridColumn_InitTm.Width = 100;
                gridColumn_CurrCd.Width = 90;
                gridColumn_RevYn.Width = 60;
                gridColumn_CostYn.Width = 60;
                gridColumn_EndTm.Width = 100;
                gridColumn_UseYn.Width = 80;

                SetControl ctr = new SetControl();
                ctr.SetCombobox(aLookUpEdit1, CH.GetCode("MAS_BIZ", true), false);
                aLookUpEdit1.Properties.DropDownRows = 15;
                aLookUpEdit1.Properties.ShowLines = false;
                aLookUpEdit1.Properties.ShowHeader = false;
                aLookUpEdit1.Properties.ShowFooter = false;

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

        protected override void OnSearch()
        {
            try
            {
                string ClassCode = Code;

                dt = DBHelper.GetDataTable("AP_H_MAS_BANK_S", new object[] { Global.FirmCode, aTextEdit1.Text, aTextEdit2.Text, aLookUpEdit1.EditValue, "P" });
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
                    DataTable gridDT = (DataTable)aGrid1.DataSource;
                    ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGrid1.MainView).GetFocusedDataSourceRowIndex()]);
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
