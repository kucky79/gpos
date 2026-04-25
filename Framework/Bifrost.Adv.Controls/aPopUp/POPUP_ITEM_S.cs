using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Bifrost;
using Bifrost.Helper;
using Bifrost.Common;
using Bifrost.Win;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Adv.Controls.PopUp
{
    public partial class POPUP_ITEM_S : PopupBase
    {
        DataTable _dt = new DataTable();
        string CondTP = "P";

        public POPUP_ITEM_S()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "품목 조회";

                InitGrid();
                //폼 위치 조정
                CenterToParent();

                //자동검색
                if (this.AutoSearch)
                {
                    this.OnSearch();
                }

                aTextEdit_ItemName.Select();

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void InitGrid()
        {
            gvItem.OptionsView.ShowGroupPanel = false;
            gvItem.OptionsView.ColumnAutoWidth = false;
            gvItem.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            gvItem.OptionsSelection.EnableAppearanceFocusedCell = false;
            //gridView1.OptionsSelection.MultiSelect = true;
            //gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = false;

            gcItem5.ColumnEdit = aGridHelper.SetGridLookUpItem(CH.GetCode("PRT401"));

            //그리드 DATASOURCE 초기화
            _dt = DBHelper.GetDataTable("AP_H_MAS_ITEM_S", new object[] { A.GetDummyString, ItemCode, ItemName, PartnerCode, ItemType, UseYN, BOMYN, ItemCodeURL, AcctType, CondTP });

            aGrid1.Binding(_dt, true);
        }

        private void InitEvent()
        {
            aTextEdit_ItemCode.KeyDown += ATextEdit_KeyDown;
            aTextEdit_ItemName.KeyDown += ATextEdit_KeyDown;
            aTextEdit_Cust.KeyDown += ATextEdit_KeyDown;

            btnSearch.Click += BtnSearch_Click;

            aGrid1.ProcessGridKey += AGrid1_KeyDown;
            gvItem.DoubleClick += GridView1_DoubleClick;
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }

        private void AGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        #region Buttons'  handlers
        protected override void OnSearch()
        {
            try
            {
                Search();

                if (this.aGrid1.DefaultView.RowCount > 0)
                {
                    if (!this.aGrid1.Focused) { this.aGrid1.Focus(); }
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            if (gvItem.RowCount > 0)
            {
                DataTable gridDT = (DataTable)aGrid1.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGrid1.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBoxA("선택한 데이터가 존재하지 않습니다.", MessageType.Information);
            }
        }

        protected override void OnCancel()
        {
            this.Close();
        }

        #endregion Buttons'  handlers

        private void ATextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                Search();
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if(aTextEdit_ItemCode.Text == string.Empty && aTextEdit_ItemName.Text == string.Empty && aTextEdit_Cust.Text == string.Empty && AcctType == string.Empty)
            {
                if(ShowMessageBoxA("전체검색을 하면 시간이 오래 걸릴수 있습니다.\n계속하시겠습니까?", Bifrost.Common.MessageType.Question) == DialogResult.Yes)
                {
                    LoadData.StartLoading(this, "Searching.....", "품목을 조회하고 있습니다.");
                    Search();
                    LoadData.EndLoading();
                }
            }
            else
            {
                LoadData.StartLoading(this, "Searching.....", "품목을 조회하고 있습니다.");
                Search();
                LoadData.EndLoading();
            }
        }



        #region 
        private string _ItemCode = string.Empty;
        private string _ItemName = string.Empty;
        private string _PartnerCode = string.Empty;
        private string _ItemType = string.Empty;
        private string _UseYN = string.Empty;
        private string _BOMYN = string.Empty;
        private string _ItemCodeURL = string.Empty;
        private string _AcctType = string.Empty;

        public string ItemCode
        {
            get { return _ItemCode; }
            set
            {
                _ItemCode = value;
                aTextEdit_ItemCode.Text = ItemCode;
            }
        }

        public string ItemName
        {
            get { return _ItemName; }
            set
            {
                _ItemName = value;
                aTextEdit_ItemName.Text = ItemName;
            }
        }

        public string PartnerCode
        {
            get { return _PartnerCode; }
            set
            {
                _PartnerCode = value;
                aTextEdit_Cust.Text = PartnerCode;
            }
        }

        public string ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }

        public string UseYN
        {
            get { return _UseYN; }
            set { _UseYN = value; }
        }

        public string BOMYN
        {
            get { return _BOMYN; }
            set { _BOMYN = value; }
        }

        public string ItemCodeURL
        {
            get { return _ItemCodeURL; }
            set { _ItemCodeURL = value; }
        }

        public string AcctType
        {
            get { return _AcctType; }
            set { _AcctType = value; }
        }
        #endregion

        private void Search()
        {
            _dt = DBHelper.GetDataTable("AP_H_MAS_ITEM_S", new object[] { Global.FirmCode, aTextEdit_ItemCode.Text, aTextEdit_ItemName.Text, aTextEdit_Cust.Text, ItemType, UseYN, BOMYN, ItemCodeURL, AcctType, CondTP });
            aGrid1.Binding(_dt);

            gvItem.OptionsView.ColumnAutoWidth = false;
            gvItem.BestFitColumns();
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo = gvItem.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            if (viewInfo.ViewRects.ColumnTotalWidth < viewInfo.ViewRects.ColumnPanelWidth)
                gvItem.OptionsView.ColumnAutoWidth = true;

            if (this.aGrid1.DefaultView.RowCount > 0)
            {
                if (!this.aGrid1.Focused) { this.aGrid1.Select(); }
                gvItem.SelectRow(gvItem.FocusedRowHandle);

            }
            else
            {
                aTextEdit_ItemName.Select();
            }
        }


    }
}
