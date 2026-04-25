using DevExpress.XtraGrid.Views.Grid;
using NF.A2P;
using NF.A2P.Helper;
using NF.Framework.Common;
using NF.Framework.Win;
using NF.Framework.Win.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NF.A2P.Helper.aGridHelper;
using System.Collections;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_FORMCODE : PopupBase
    {

        DataTable dt = new DataTable();
        private string _Tppd = "";
        public string Fgrpay = "";
        public string Tppd
        {
            get { return _Tppd; }
            set { _Tppd = value; }
        }
        private string _Popupstyle = "";
        public string Popupstyle
        {
            get { return _Popupstyle; }
            set { _Popupstyle = value; }
        }
        public string SearchCondition
        {
            set
            {
                //this.OnSearch();
                txtSearch.Text = value;

                if (txtSearch.Text.Length > 1)
                {
                    this.OnSearch();
                }
            }
        }

        public POPUP_FORMCODE()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        //public POPUP_FORMCODE(string SearchCode)
        //{
        //    InitializeComponent();
        //    InitForm();
        //    InitEvent();

        //    //OnSearch();
        //    SearchCondition = SearchCode;
        //}

        //public POPUP_FORMCODE(string Tppopup, string Popupst)
        //{
        //    Popupstyle = Popupst;
        //    InitializeComponent();
        //    InitForm();
        //    InitEvent();

        //    //OnSearch();
        //}
        //public POPUP_FORMCODE(string Tppopup, string Popupst, string fgrpay)
        //{
        //    Popupstyle = Popupst;                        
        //    InitializeComponent();
        //    InitForm();
        //    InitEvent();

        //    //OnSearch();
        //}

        private void InitEvent()
        {
            this.aGridM.KeyDown += new System.Windows.Forms.KeyEventHandler(this._GridM_KeyDown);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);

            aButton_Search.Click += AButton_Search_Click;
            txtSearch.KeyDown += TxtSearch_KeyDown;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnSearch();
            }
        }

        private void AButton_Search_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void InitForm()
        {
            try
            {
               
                #region Grid Initialize

                SetColumn CODE = new SetColumn(gridView1, "CD_FORM", "양식코드", aGridColumnStyle.Text, 100, false, true);
                SetColumn NAME = new SetColumn(gridView1, "NM_FORM", "양식명", aGridColumnStyle.Text, 150, false, true);
                if (Popupstyle == "2")
                {
                    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                    gridView1.OptionsSelection.MultiSelect = true;
                }
                SetGridStyle(aGridM, false, true, false);
                gridView1.IndicatorWidth = 29;
                #endregion Grid Initialize

                //폼 위치 조정
                CenterToParent();

                //자동검색
                if (this.AutoSearch)
                {
                    this.OnSearch();
                }
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
                string StrTppd = Tppd;
                //string FirmCode = MdiForm.Global.CompanyCode;
                dt = DBHelper.GetDataTable("AP_GRW_DCFORM_POP_S", new object[] { Global.FirmCode, txtSearch.Text });
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
                //if (Popupstyle == "2")
                //{
                //    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                //        ReturnData.Add(i.ToString(), (DataRow)gridDT.Rows[((GridView)aGridM.MainView).GetSelectedRows()[i]]);
                //}
                //else
                //{
                //    ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGridM.MainView).GetFocusedDataSourceRowIndex()]);
                //}
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGridM.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBoxA("데이터가 존재하지 않습니다.", MessageType.Information);
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
