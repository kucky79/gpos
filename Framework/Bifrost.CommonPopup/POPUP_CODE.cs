using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Bifrost.Helper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using Bifrost.Win;
using Bifrost.Common;

namespace Bifrost.CommonPopup
{
    public partial class POPUP_CODE : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Code = "";
        private string _Cdclasref = "";
        private string _Cdflagref = "";

        public string Cdclasref
        {
            get { return _Cdclasref; }
            set { _Cdclasref = value; }
        }

        public string Cdflagref
        {
            get { return _Cdflagref; }
            set { _Cdflagref = value; }
        }

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
                txtSearch.Text = value;

                if (txtSearch.Text.Length > 1)
                {
                    this.OnSearch();
                }
            }
        }


        public POPUP_CODE()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_CODE(string ClassCode, string SearchCode)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            Code = ClassCode;
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
                PopupTitle = "코드정보조회";

                #region Grid Initialize

                //SetColumn CODE = new SetColumn(gridView1, "CODE", "코드", aGridColumnStyle.Text, 150, false, true);
                //SetColumn NAME = new SetColumn(gridView1, "NAME", "코드명", aGridColumnStyle.Text, 150, false, true);
                //SetGridStyle(aGridM, false, true, false);
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
            {   //2018-10-11 MAS_CODE조회시 CD_FLAG_REF 조건 추가 진영
                string BlankYN = "N";
                string ClassCode = Code;
                if(A.GetString(Cdclasref) == "" && A.GetString(Cdflagref) == "")
                    dt = DBHelper.GetDataTable("AP_H_MAS_CODE_S", new object[] { Global.FirmCode, ClassCode, BlankYN, txtSearch.Text });
                else if(A.GetString(Cdclasref) != "" && A.GetString(Cdflagref) == "")
                    dt = DBHelper.GetDataTable("AP_H_MAS_CODE_REF_S", new object[] { Global.FirmCode, ClassCode, BlankYN, txtSearch.Text, "P", Cdclasref, Cdflagref });
                else if (A.GetString(Cdclasref) == "" && A.GetString(Cdflagref) != "")
                    dt = DBHelper.GetDataTable("AP_H_MAS_CODE_REF_S", new object[] { Global.FirmCode, ClassCode, BlankYN, txtSearch.Text, "P", Cdclasref, Cdflagref });

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
                ShowMessageBox("선택된 행이 존재하지 않습니다.", MessageType.Information);
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
