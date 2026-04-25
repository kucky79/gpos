using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

/// Framework 컴포넌
using NF.Framework.Common;
using NF.A2P.Data;
using NF.Framework.Win;
using NF.Framework.Win.Controls;

using NF.A2P;
using NF.A2P.Helper;
using static NF.A2P.Helper.aGridHelper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_BRDDOC : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Fg = "";
        public string Fg
        {
            get { return _Fg; }
            set { _Fg = value; }
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


        public POPUP_BRDDOC()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_BRDDOC(string FG, string SearchCode)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            Fg = FG;
            OnSearch();
            //SearchCondition = SearchCode;
        }

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
                PopupTitle = "리스트조회";

                #region Grid Initialize

                SetColumn CODE = new SetColumn(gridView1, "CODE", "코드", aGridColumnStyle.Text, 150, false, true);
                SetColumn NAME = new SetColumn(gridView1, "NAME", "명", aGridColumnStyle.Text, 150, false, true);
                SetGridStyle(aGridM, false, true, false);
                gridView1.IndicatorWidth = 29;

                gridView1.BestFitColumns();
                #endregion Grid Initialize

                //폼 위치 조정
                CenterToParent();
                aGridM.Focus();

                if(AutoSearch)
                {
                    OnSearch();
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
                string FGcode = Fg;
                if(FGcode == "BRD")
                    dt = DBHelper.GetDataTable("AP_GRW_BRD_SET_S2", new object[] { Global.FirmCode, Global.EmpCode });
                else if (FGcode == "DOC")
                    dt = DBHelper.GetDataTable("AP_GRW_DOC_S", new object[] { Global.FirmCode, Global.EmpCode });
                aGridM.Binding(dt);

                if (this.aGridM.DefaultView.RowCount > 0)
                {
                    if (!this.aGridM.Focused) { this.aGridM.Focus(); }
                    gridView1.SelectRow(gridView1.FocusedRowHandle);
                }
            }
            catch(Exception ex)
            {
                HandleWinException(ex);
            }
        }

        protected override void OnOK()
        {
            if (aGridM.MainView.RowCount > 0 )
            {
                DataTable gridDT = (DataTable)aGridM.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)aGridM.MainView).GetFocusedDataSourceRowIndex()]);
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
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }
    }
}
