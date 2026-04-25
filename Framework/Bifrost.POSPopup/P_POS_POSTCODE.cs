
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace POS
{ 
    public partial class P_POS_POSTCODE : POSPopupBase
    {
        private DataTable dt = new DataTable();
        private int pageCount = 1;
        public P_POS_POSTCODE()
        {
            InitializeComponent();
            InitForm();
            InitEvent();

        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "우편번호 조회";

                #region Grid Initialize

                //SetColumn PostCode = new SetColumn(gridView1, "CD_POST", "우편번호", aGridColumnStyle.Text, 60, false, true);
                //SetColumn Address1 = new SetColumn(gridView1, "DC_ADD1", "주소", aGridColumnStyle.Text, 300, false, true);
                //SetColumn Address2 = new SetColumn(gridView1, "DC_ADD2", "주소", aGridColumnStyle.Text, 300, false, true);
                //SetGridStyle(aGrid1, false, true, false);

                #endregion Grid Initialize

                dt.Columns.Add("CD_POST", typeof(string));
                dt.Columns.Add("DC_ADD1", typeof(string));
                dt.Columns.Add("DC_ADD2", typeof(string));

                gridMain.Binding(dt);

                //폼 위치 조정
                CenterToParent();

                //자동검색
                if (this.AutoSearch)
                {
                    this.OnSearch();
                }

                aTextEdit_Search.Select();

            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private void InitEvent()
        {
            gridMain.KeyDown += _Grid1_KeyDown;
            gridView1.DoubleClick += gridView1_DoubleClick;
            btnSearch.Click += AButton_Search_Click;
            btnMore.Click += AButton_More_Click;
            aTextEdit_Search.KeyDown += ATextEdit_Search_KeyDown;

            btnDone.Click += BtnDone_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            OnOK();
        }

        #region Buttons'  handlers
        protected override void OnSearch()
        {
            try
            {
                if (aTextEdit_Search.Text == string.Empty)
                {
                    ShowMessageBoxA("검색 내용을 넣어주세요.", MessageType.Warning);
                    aTextEdit_Search.Focus();
                    return;
                }

                dt.Clear();
                pageCount = 1;

                GetPostDate();

                if (this.gridMain.DefaultView.RowCount > 0)
                {
                    if (!this.gridMain.Focused) { this.gridMain.Focus(); }
                }
            }
            catch (Exception ex)
            {
                HandleWinException(ex);
            }
        }

        private int TmpCount = 0;
        private void GetPostDate()
        {
            gridView1.BeginUpdate();

            string resultString = string.Empty;

            List<string> TmpAddress = new List<string>();
            TmpCount = 0;
            resultString = ApiHelper.GetPosetCode(aTextEdit_Search.Text, pageCount, 50, TmpAddress, out TmpCount);

            if (TmpCount > 0)
            {
                btnMore.Visible = true;
                btnMore.Text = "더보기 (" + (TmpCount - gridView1.DataRowCount).ToString() + ")..";
                //aTextEdit_Search.Width = 474;

            }
            else
            {
                btnMore.Visible = false;
                //aTextEdit_Search.Width = 580;
            }

            if (TmpAddress.Count > 0)
            {
                for (int i = 0; i < TmpAddress.Count / 3; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow["CD_POST"] = TmpAddress[i * 3];
                    newRow["DC_ADD1"] = TmpAddress[i * 3 + 1];
                    newRow["DC_ADD2"] = TmpAddress[i * 3 + 2];
                    dt.Rows.Add(newRow);
                }
            }
            gridView1.EndUpdate();
        }

        protected override void OnOK()
        {
            if (gridMain.MainView.RowCount > 0)
            {
                DataTable gridDT = (DataTable)gridMain.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)gridMain.MainView).GetFocusedDataSourceRowIndex()]);
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
        private void _Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }

        private void ATextEdit_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnSearch();
                gridView1.BestFitColumns();
            }
        }

        private void AButton_More_Click(object sender, EventArgs e)
        {
            pageCount = pageCount + 1;

            GetPostDate();

            if (this.gridMain.DefaultView.RowCount > 0)
            {
                if (!this.gridMain.Focused) { this.gridMain.Focus(); }
            }

            gridView1.BestFitColumns();

            gridView1.MoveLastVisible();

        }

        private void AButton_Search_Click(object sender, EventArgs e)
        {
            OnSearch();
        }


    }

}
