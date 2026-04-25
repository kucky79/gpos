using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

/// Framework 컴포넌
using Bifrost.Common;
using Bifrost.Data;
using Bifrost.Win;
using Bifrost.Win.Controls;

using Bifrost;
using Bifrost.Helper;
using static Bifrost.Helper.aGridHelper;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;

namespace Bifrost.Adv.Controls.PopUp
{
    public partial class POPUP_CODE : PopupBase
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
            SearchCondition = SearchCode;
        }

        private void InitEvent()
        {
            this.aGridM.ProcessGridKey += OK_Event;
            this.aGridM.KeyDown += OK_Event;
            this.gridView1.DoubleClick += gridView1_DoubleClick;
            this.txtSearch.KeyDown += OK_Event;
            aButton_Search.Click += AButton_Search_Click;
        }

        private void AButton_Search_Click(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void OK_Event(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnOK();
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }

        private void InitForm()
        {
            try
            {
                PopupTitle = "Master Code Search";

                #region Grid Initialize

                SetColumn CODE = new SetColumn(gridView1, "CODE", "코드", aGridColumnStyle.Text, 150, false, true);
                SetColumn NAME = new SetColumn(gridView1, "NAME", "코드명", aGridColumnStyle.Text, 150, false, true);
                SetGridStyle(aGridM, false, true, false);
                gridView1.IndicatorWidth = 29;

                gridView1.BestFitColumns();
                #endregion Grid Initialize

                //폼 위치 조정
                CenterToParent();

                if(txtSearch.Text.Length > 0)
                {
                    txtSearch.Select();
                }
                else
                {
                    aGridM.Select();
                }
                

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
                string BlankYN = "N";
                string ClassCode = Code;
                
                dt = DBHelper.GetDataTable("AP_H_MAS_CODE_S", new object[] { Global.FirmCode, ClassCode, BlankYN, txtSearch.Text });
                aGridM.Binding(dt);

                if (this.aGridM.DefaultView.RowCount > 0)
                {
                    if (!this.aGridM.Focused) { this.aGridM.Select(); }
                    gridView1.SelectRow(gridView1.FocusedRowHandle);
                }
                else
                {
                    txtSearch.Select();
                    ShowMessageBox(this.ResReader.GetString("The data not found. Please search again."), MessageType.Information);

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
                ShowMessageBoxA("No items selected.\r\nSearch again and select the item on the list.", MessageType.Information);
                return;
            }
        }

        protected override void OnCancel()
        {
            this.Close();
        }

        #endregion Buttons'  handlers

        
    }
}
