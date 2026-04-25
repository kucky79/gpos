using DevExpress.XtraGrid.Views.Grid;
using Bifrost;
using Bifrost.Helper;
using Bifrost.Common;
using Bifrost.Win;
using System;
using System.Data;
using System.Windows.Forms;

namespace Bifrost.Adv.Controls.PopUp
{
    public partial class POPUP_CUST : PopupBase
    {
        DataTable dt = new DataTable();

        public string Code { get; set; } = "";

        public string sctYN { get; set; } = "N";

        public string SearchCondition
        {
            set
            {
                txtSearch.Text = value;
            }
        }

        public POPUP_CUST()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_CUST(string SearchCode)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            SearchCondition = SearchCode;

        }

        protected override void OnShown(EventArgs e)
        {
            //검색값이 있으면 무조건 자동검색
            if (txtSearch.Text.Length > 1)
            {
                this.OnSearch();
            }

            base.OnShown(e);
        }
        private void InitEvent()
        {
            aGridCust.KeyDown += new KeyEventHandler(_GridM_KeyDown);
            gridView1.DoubleClick += new EventHandler(gridView1_DoubleClick);

            txtSearch.KeyDown += TxtSearch_KeyDown;
            btnSearch.Click += BtnSearch_Click;
            aGridCust.ProcessGridKey += AGridCust_ProcessGridKey; ;

        }

        private void AGridCust_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                OnSearch();
            }
        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "Partner Search";
                //폼 위치 조정
                CenterToParent();

                //자동검색
                if (this.AutoSearch)
                {
                    this.OnSearch();
                }
                

                aGridCust.ForceInitialize();
                aGridCust.Select();

                SetControl ctr = new SetControl();
                ctr.SetCombobox(aLookUpEdit_PartnerType, CH.GetCode("MAS032", true));

                txtSearch.Select();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        #region Buttons'  handlers
        protected override void OnSearch()
        {
            try
            {
                string ClassCode = Code;
                string SctYN = sctYN == "Y" ? "9" : "";

                dt = DBHelper.GetDataTable("AP_H_MAS_CUST_S", new object[] { Global.FirmCode, aLookUpEdit_PartnerType.EditValue, txtSearch.Text, "P", SctYN });
                aGridCust.Binding(dt, true);

                if (this.aGridCust.DefaultView.RowCount > 0)
                {
                    if (!this.aGridCust.Focused) { this.aGridCust.Focus(); }
                }
                else
                {
                    ShowMessageBoxA("The code not found. Please search again.", MessageType.Information);
                    return;
                }
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            if (aGridCust.MainView.RowCount > 0)
            {
                DataTable gridDT = (DataTable)aGridCust.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGridCust.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBoxA("선택하신 행이 존재하지 않습니다.", MessageType.Information);
            }
        }

        protected override void OnCancel()
        {
            this.Close();
        }

        #endregion Buttons'  handlers

        private void _GridM_KeyDown(object sender, KeyEventArgs e)
        {
            if (!aGridCust.Focused)
                return;
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }
    }
}
