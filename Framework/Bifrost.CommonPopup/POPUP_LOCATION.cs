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
    public partial class POPUP_LOCATION : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Code = string.Empty;
        private string _UserPopUpParam = string.Empty;
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

                if (txtSearch.Text.Length > 1)
                {
                    this.OnSearch();
                }
            }
        }

        public POPUP_LOCATION()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_LOCATION(string ClassCode, string SearchCode, string UserPopUpParam)
        {
            InitializeComponent();

            Code = ClassCode;
            _UserPopUpParam = UserPopUpParam;
            SearchCondition = SearchCode;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitForm();
            InitEvent();
        }

        private void InitEvent()
        {
            this.aGridM.KeyDown += new KeyEventHandler(this._GridM_KeyDown);
            this.gridView1.DoubleClick += new EventHandler(this.gridView1_DoubleClick);
        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "Location";

                gridView1.OptionsView.ShowGroupPanel = false;

                SetControl ctr = new SetControl();
                ctr.SetCombobox(aLookUpEdit_AirSea, CH.GetCode("MAS013"));

                aLookUpEdit_AirSea.EditValue = CH.SplitString(_UserPopUpParam, ";")[0];

                //폼 위치 조정
                CenterToParent();
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
                dt = DBHelper.GetDataTable("AP_H_MAS_LOCATION_S", new object[] { Global.FirmCode, txtSearch.Text, "P", A.GetString(aLookUpEdit_AirSea.EditValue) });
                aGridM.Binding(dt);

                if (this.aGridM.DefaultView.RowCount > 0)
                { 
                    if (!this.aGridM.Focused) { this.aGridM.Focus(); }
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            if (aGridM.MainView.RowCount > 0)
            {
                DataTable gridDT = (DataTable)aGridM.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGridM.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBox("선택된 내용이 없습니다.", MessageType.Warning);
            }
        }

        protected override void OnCancel()
        {
            this.Close();
        }

        #endregion Buttons'  handlers

        private void _GridM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }
    }
}
