using Bifrost;
using Bifrost.Common;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
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
    public partial class P_POS_PURCHASE_TMP : POSPopupBase
    {
        public string SaleDt { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;

        public TempOrderType OrderType { get; set; } = TempOrderType.Order;

        DataTable _dt;

        public P_POS_PURCHASE_TMP()
        {
            InitializeComponent();
            InitForm();
            InitEvent();
        }

        private void InitForm()
        {
            PopupTitle = "구매등록 임시저장 리스트";

            gridView1.OptionsView.ShowGroupPanel = false;
            //gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsCustomization.AllowSort = true;
            gridView1.OptionsCustomization.AllowFilter = false;

            //그리드 높이
            gridView1.UserCellPadding = new Padding(0, 5, 0, 5);
        }

        private void InitEvent()
        {
            this.Load += P_POS_SALE_Load;
            gridView1.DoubleClick += GridView1_DoubleClick;
            dtpSale.DateTimeChanged += DtpSale_DateTimeChanged;

            btnCancel.Click += BtnCancel_Click;
            btnDone.Click += BtnDone_Click;

            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string SlipNo = A.GetString(gridView1.GetFocusedRowCellValue("NO_PO"));
            string CustomerName = A.GetString(gridView1.GetFocusedRowCellValue("NM_CUST"));
            string CustomerCode = A.GetString(gridView1.GetFocusedRowCellValue("CD_CUST"));

            if (ShowMessageBoxA(CustomerName + " 거래처의 임시저장건을 삭제하시겠습니까?", MessageType.Question) == DialogResult.Yes)
            {
                bool result = false;
                if (OrderType == TempOrderType.Order)
                    result = DBHelper.ExecuteNonQuery("USP_POS_PO_TMP_D", new object[] { POSGlobal.StoreCode, SlipNo });
                else if (OrderType == TempOrderType.Customer)
                    result = DBHelper.ExecuteNonQuery("USP_POS_PO_TMP_CUST_D", new object[] { POSGlobal.StoreCode, CustomerCode, dtpSale.Text });

                if (result)
                {
                    ShowMessageBoxA("삭제되었습니다.", MessageType.Information);
                    OnSearch();
                    return;
                }
            }
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            OnOK();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DtpSale_DateTimeChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            OnOK();
        }

        private void P_POS_SALE_Load(object sender, EventArgs e)
        {
            txtCust.Text = CustomerName;
            dtpSale.Text = SaleDt;
        }

        protected override void OnSearch()
        {
            string tmpOrderType = string.Empty;

            if (OrderType == TempOrderType.Order)
                tmpOrderType = "O";
            else if (OrderType == TempOrderType.Customer)
                tmpOrderType = "C";

            _dt = Search(new object[] { POSGlobal.StoreCode, dtpSale.Text, CustomerCode, tmpOrderType });
            gridList.Binding(_dt, true);
        }

        protected override void OnShown(EventArgs e)
        {
            OnSearch();
        }

        protected override void OnOK()
        {
            if (gridView1.RowCount > 0)
            {
                DataTable gridDT = (DataTable)gridList.DataSource;
                ReturnData.Add("ReturnData", (DataRow)gridDT.Rows[((GridView)gridList.MainView).GetFocusedDataSourceRowIndex()]);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                ShowMessageBoxA("데이터가 존재하지 않습니다.", MessageType.Information);
            }
        }
        
    }
}
