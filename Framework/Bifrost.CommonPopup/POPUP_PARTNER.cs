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
    public partial class POPUP_PARTNER : PopupBase
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
                txtSearch.Text = value;
            }
        }

        public POPUP_PARTNER()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_PARTNER(string SearchCode)
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
            _GridM.KeyDown += new KeyEventHandler(_GridM_KeyDown);
            gridView1.DoubleClick += new EventHandler(gridView1_DoubleClick);
        }
        private void InitGrid()
        {
            // 
            // gridColumn_CdPartner
            // 
            this.gridColumn_CdPartner.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_CdPartner.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_CdPartner.Caption = "Partner Code";
            this.gridColumn_CdPartner.FieldName = "CD_PARTNER";
            this.gridColumn_CdPartner.Name = "gridColumn_CdPartner";
            this.gridColumn_CdPartner.Visible = true;
            this.gridColumn_CdPartner.VisibleIndex = 0;
            this.gridColumn_CdPartner.Width = 85;
            // 
            // gridColumn_NmPartner
            // 
            this.gridColumn_NmPartner.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_NmPartner.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_NmPartner.Caption = "Partner Name";
            this.gridColumn_NmPartner.FieldName = "NM_PARTNER_ENG";
            this.gridColumn_NmPartner.Name = "gridColumn_NmPartner";
            this.gridColumn_NmPartner.Visible = true;
            this.gridColumn_NmPartner.VisibleIndex = 1;
            this.gridColumn_NmPartner.Width = 200;
            // 
            // gridColumn_DcAddressBL
            // 
            this.gridColumn_DcAddressBL.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_DcAddressBL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_DcAddressBL.Caption = "B/L Address";
            this.gridColumn_DcAddressBL.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn_DcAddressBL.FieldName = "DC_ADDRESS_BL";
            this.gridColumn_DcAddressBL.Name = "gridColumn_DcAddressBL";
            this.gridColumn_DcAddressBL.Visible = true;
            this.gridColumn_DcAddressBL.VisibleIndex = 2;
            this.gridColumn_DcAddressBL.Width = 300;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gridColumn_TelNo
            // 
            this.gridColumn_TelNo.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_TelNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_TelNo.Caption = "Tel No.";
            this.gridColumn_TelNo.FieldName = "NO_TEL";
            this.gridColumn_TelNo.Name = "gridColumn_TelNo";
            this.gridColumn_TelNo.Visible = true;
            this.gridColumn_TelNo.VisibleIndex = 3;
            this.gridColumn_TelNo.Width = 120;
            // 
            // gridColumn_TpPartner
            // 
            this.gridColumn_TpPartner.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_TpPartner.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_TpPartner.Caption = "Partner Type";
            this.gridColumn_TpPartner.FieldName = "TP_PARTNER";
            this.gridColumn_TpPartner.Name = "gridColumn_TpPartner";
            this.gridColumn_TpPartner.Visible = true;
            this.gridColumn_TpPartner.VisibleIndex = 4;
            this.gridColumn_TpPartner.Width = 150;
            this.gridColumn_TpPartner.ColumnEdit = CH.SetGridLookUpItem(CH.GetCode("SAL003"));
            // 
            // gridColumn_AccountID
            // 
            this.gridColumn_AccountID.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_AccountID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_AccountID.Caption = "Account ID";
            this.gridColumn_AccountID.FieldName = "CD_ACCOUNTID";
            this.gridColumn_AccountID.Name = "gridColumn_AccountID";
            this.gridColumn_AccountID.Visible = true;
            this.gridColumn_AccountID.VisibleIndex = 5;
            this.gridColumn_AccountID.Width = 100;

            gridColumn_CdPartner.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridColumn_NmPartner.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "Partner Search";
                InitGrid();
                //폼 위치 조정
                CenterToParent();

                //자동검색
                if (this.AutoSearch)
                {
                    this.OnSearch();
                }
                

                _GridM.ForceInitialize();
                _GridM.Select();

                SetControl ctr = new SetControl();
                ctr.SetCombobox(aLookUpEdit_PartnerType, CH.GetCode("SAL003", true));

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
                
                dt = DBHelper.GetDataTable("AP_H_MAS_PARTNER_S", new object[] { Global.FirmCode, txtSearch.Text, "P"  });
                _GridM.Binding(dt);

                if (this._GridM.DefaultView.RowCount > 0)
                {
                    if (!this._GridM.Focused) { this._GridM.Focus(); }
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
            if (_GridM.MainView.RowCount > 0)
            {
                DataTable gridDT = (DataTable)_GridM.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)_GridM.MainView).GetFocusedDataSourceRowIndex()]);
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

        #endregion Buttons'  handlers

        private void _GridM_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_GridM.Focused)
                return;
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }
    }
}
