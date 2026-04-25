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

namespace NF.A2P.CommonPopup
{
    public partial class POPUP_HRSDEDUCT : PopupBase
    {
        DataTable dt = new DataTable();
        private string _Tpdeduct = "";
        public string Tpdeduct
        {
            get { return _Tpdeduct; }
            set { _Tpdeduct = value; }
        }

        private string _Ymadj = "";
        public string Ymadj
        {
            get { return _Ymadj; }
            set { _Ymadj = value; }
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

        public POPUP_HRSDEDUCT()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_HRSDEDUCT(string Strymadj)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            Ymadj = Strymadj;
        }

        public POPUP_HRSDEDUCT(string Strtpdeduct, string SearchCode)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            Tpdeduct = Strtpdeduct;
            //OnSearch();
            SearchCondition = SearchCode;
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
                PopupTitle = "공제 정보조회";

                #region Grid Initialize

                SetColumn CODE = new SetColumn(gridView1, "CD_DEDUCT", "공제코드", aGridColumnStyle.Text, 100, false, true);
                SetColumn NAME = new SetColumn(gridView1, "NM_DEDUCT", "공제명", aGridColumnStyle.Text, 150, false, true);
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
                string StrTpdeduct = Tpdeduct;
                //string FirmCode = MdiForm.Global.CompanyCode;
                if (Ymadj != "")
                {   /* 2018-12-19
                       진영 추가
                    연말정산화면에서 사용. 기준 년월에서 가장최근 비과세 정보조회*/
                    dt = DBHelper.GetDataTable("AP_H_HRS_DEDUCT_YTX_S", new object[] { Global.FirmCode, StrTpdeduct, Ymadj, txtSearch.Text });
                    aGridM.Binding(dt);
                }
                else
                {
                    dt = DBHelper.GetDataTable("AP_H_HRS_DEDUCT_S", new object[] { Global.FirmCode, StrTpdeduct, txtSearch.Text });
                    aGridM.Binding(dt);
                }

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
