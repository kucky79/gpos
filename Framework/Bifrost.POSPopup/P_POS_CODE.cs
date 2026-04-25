using System;
using System.Data;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid;
using Bifrost.Win;
using Bifrost.Helper;
using Bifrost;
using Bifrost.Common;

namespace POS
{
    public partial class P_POS_CODE : POSPopupBase
    {
        DataTable dt = new DataTable();

        public string Cdclasref { get; set; } = "";

        public string Cdflagref { get; set; } = "";

        public string Code { get; set; } = "";

        public string[] SelectedCode { get; set; }

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


        public P_POS_CODE()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        public P_POS_CODE(string ClassCode, string SearchCode)
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
            this.gridMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this._GridM_KeyDown);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            aButton_Search.Click += AButton_Search_Click;
            txtSearch.KeyDown += TxtSearch_KeyDown;

            btnDone.Click += BtnDone_Click;
            btnCancel.Click += BtnCancel_Click;
            this.Load += P_POS_CODE_Load;
        }

        private void P_POS_CODE_Load(object sender, EventArgs e)
        {
            if (AutoSearch)
            {
                OnSearch();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            OnCancel();
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            OnOK();
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
                //SetGridStyle(gridMain, false, true, false);
                gridView1.BestFitColumns();
                #endregion Grid Initialize

                //폼 위치 조정
                CenterToParent();
                gridMain.Focus();

                
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
                string BlankYN = "N";
                string ClassCode = Code;
                if(A.GetString(Cdclasref) == "" && A.GetString(Cdflagref) == "")
                    dt = DBHelper.GetDataTable("USP_POS_CODE_POPUP_S", new object[] { POSGlobal.StoreCode, ClassCode, BlankYN, txtSearch.Text });
                else if(A.GetString(Cdclasref) != "" && A.GetString(Cdflagref) == "")
                    dt = DBHelper.GetDataTable("USP_POS_CODE_POPUP_S", new object[] { POSGlobal.StoreCode, ClassCode, BlankYN, txtSearch.Text, "P", Cdclasref, Cdflagref });
                else if (A.GetString(Cdclasref) == ""  && A.GetString(Cdflagref) != "")
                    dt = DBHelper.GetDataTable("USP_POS_CODE_POPUP_S", new object[] { POSGlobal.StoreCode, ClassCode, BlankYN, txtSearch.Text, "P", Cdclasref, Cdflagref });


                dt.PrimaryKey = new DataColumn[] { dt.Columns["CODE"] };


                if(SelectedCode != null && SelectedCode.Length > 0)
                {
                    for (int i = 0; i < SelectedCode.Length; i++)
                    {
                        if (A.GetString(SelectedCode[i]) != string.Empty)
                        {
                            DataRow dr = dt.Rows.Find(SelectedCode[i]);
                            dt.Rows.Remove(dr);
                        }
                    }
                }

                dt.AcceptChanges();

                gridMain.Binding(dt);

                if (this.gridMain.DefaultView.RowCount > 0)
                {
                    if (!this.gridMain.Focused) { this.gridMain.Focus(); }
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
            if (gridMain.MainView.RowCount > 0 )
            {
                DataTable gridDT = (DataTable)gridMain.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)gridMain.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBox("선택된 행이 존재하지 않습니다.", MessageType.Warning);
                return;
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
