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
    public partial class POPUP_COUNTRY : PopupBase
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
                txtSearch.Text = value;

                if (txtSearch.Text.Length > 1)
                {
                    this.OnSearch();
                }
            }
        }

        public POPUP_COUNTRY()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public POPUP_COUNTRY(string ClassCode, string SearchCode)
        {
            InitializeComponent();
            InitForm();
            InitEvent();

            Code = ClassCode;
            SearchCondition = SearchCode;
        }

        private void InitEvent()
        {
            this.aGridM.KeyDown += new System.Windows.Forms.KeyEventHandler(this._GridM_KeyDown);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            txtSearch.KeyDown += TxtSearch_KeyDown;

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
                PopupTitle = "Country 정보조회";

                #region Grid Initialize

                SetColumn CODE = new SetColumn(gridView1, "CD_COUNTRY", "Country Code", aGridColumnStyle.Text, 150, false, true);
                SetColumn NAME = new SetColumn(gridView1, "NM_COUNTRY", "Country Name", aGridColumnStyle.Text, 150, false, true);
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
                //string FirmCode = MdiForm.Global.CompanyCode;
                dt = DBHelper.GetDataTable("AP_H_MAS_COUNTRY_S", new object[] { Global.FirmCode, txtSearch.Text, "P" });
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
