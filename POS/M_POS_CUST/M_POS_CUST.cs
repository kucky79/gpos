using Bifrost;
using Bifrost.Grid;
using Bifrost.Helper;
using Bifrost.Win;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class M_POS_CUST : POSFormBase
    {

        Bifrost.Helper.POSConfig cfgCustNonpaid;

        public M_POS_CUST()
        {
            InitializeComponent();
            InitializeControl();
            InitializeEvent();

        }

        private void InitializeEvent()
        {
            gridView1.InitNewRow += GridView1_InitNewRow;
            txtNonpaidSaleAmt.EditValueChanged += ANumericText1_EditValueChanged;
            
            Shown += M_POS_CUST_Shown;
            Activated += M_POS_CUST_Activated;

            btnPostCode.Click += BtnPostCode_Click;
        }

        private void M_POS_CUST_Activated(object sender, EventArgs e)
        {
            if (cfgCustNonpaid.ConfigValue == "Y")
            {
                txtNonpaidPurchaseAmt.ReadOnly = false;
                txtNonpaidSaleAmt.ReadOnly = false;
            }
            else
            {
                txtNonpaidPurchaseAmt.ReadOnly = true;
                txtNonpaidSaleAmt.ReadOnly = true;
            }
        }

        private void BtnPostCode_Click(object sender, EventArgs e)
        {
            P_POS_POSTCODE P_POS_POSTCODE = new P_POS_POSTCODE();
            if (P_POS_POSTCODE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataRow drRow = (DataRow)P_POS_POSTCODE.ReturnData["ReturnData"];
                if (drRow != null)
                {
                    txtPostCode.Text = drRow["CD_POST"].ToString().Trim();
                    txtAddr1.Text = drRow["DC_ADD1"].ToString().Trim();
                }

                if (P_POS_POSTCODE != null)
                {
                    P_POS_POSTCODE.Dispose();
                }
            }
        }

        private void M_POS_CUST_Shown(object sender, EventArgs e)
        {
            OnView();
        }

        private void ANumericText1_EditValueChanged(object sender, EventArgs e)
        {
            //if(aCheckBox2.CheckState == CheckState.Unchecked)
            //{
            //    ShowMessageBoxA("외상여부가 체크된 거래처만 외상금액 입력 가능합니다.", Bifrost.Common.MessageType.Warning);
            //    aNumericText1.EditValue = 0;
            //    return;
            //}
        }

        decimal MaxCustomerCode = 0;
        private void GridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            DataTable _dt = gridMain.DataSource as DataTable;

            //decimal CustomerCode = A.GetDecimal(_dt.Compute("MAX(CD_CUST)", ""));
            ++MaxCustomerCode;

            view.SetRowCellValue(e.RowHandle, view.Columns["YN_USE"], "Y");
            view.SetRowCellValue(e.RowHandle, view.Columns["YN_NONPAID"], "Y");
            view.SetRowCellValue(e.RowHandle, view.Columns["ST_ROW"], "I");
            view.SetRowCellValue(e.RowHandle, view.Columns["CD_CUST"], A.GetString(MaxCustomerCode).PadLeft(4, '0'));
            view.SetRowCellValue(e.RowHandle, view.Columns["FG_CUST"], "2");

        }


        private void InitializeControl()
        {
            SetControl ctr = new SetControl();
            ctr.SetCombobox(aLookUpEditCustomerType, CH.GetPOSCode("MAS032", true));
            ctr.SetCombobox(aLookUpEditCustFlag, CH.GetPOSCode("MAS032"));

            //ctr.SetCombobox(aLookUpEditUseYN, CH.GetPOSCode("SYS010", true));


            gridMain.SetBinding(panelMain, gridView1, new object[] { txtCustomerCode });

            MaxCustomerCode = A.GetDecimal(DBHelper.ExecuteScalar("SELECT MAX(CD_CUST) FROM POS_CUST WHERE CD_STORE = '" + POSGlobal.StoreCode + "'"));

            //aLookUpEditCustomerType.EditValue = "2"; //매출처

            gridMain.VerifyNotNull = new string[] { "CD_CUST", "NM_CUST" };

            cfgCustNonpaid = POSConfigHelper.GetConfig("POS021");
            
            
        }

        public override void OnInsert()
        {
            gridView1.AddNewRow();
            gridView1.BestFitColumns();
            gridView1.UpdateCurrentRow();
            gridView1.UpdateSummary();
        }

        DataSet _ds;
        public override void OnView()
        {
            txtNonpaidSaleAmt.EditValueChanged -= ANumericText1_EditValueChanged;

            LoadData.StartLoading(this);

            _ds = Search(new object[] { POSGlobal.StoreCode, txtSearch.Text, rboSearch.EditValue, aLookUpEditCustomerType.EditValue });
            gridMain.Binding(_ds.Tables[0], true);
            aGridExcel.Binding(_ds.Tables[0], true);

            LoadData.EndLoading();

            txtNonpaidSaleAmt.EditValueChanged += ANumericText1_EditValueChanged;
        }

        public override void OnSave()
        {
            DataTable dtChange = gridMain.GetChanges();
            if (dtChange == null)
            {
                ShowMessageBoxA("변경된 내용이 없습니다.", Bifrost.Common.MessageType.Warning);
                return;
            }
            bool result = Save(dtChange);

            if (result)
            {
                gridMain.AcceptChanges();
                ShowMessageBoxA("저장이 완료되었습니다.", Bifrost.Common.MessageType.Information);
            }
        }

        public override void OnDelete()
        {
            if (A.GetString(gridView1.GetFocusedRowCellValue("ST_ROW")) == "I")
            {
                gridView1.DeleteSelectedRows();
            }
            else
            {
                ShowMessageBoxA("신규 추가한 건만 삭제할수 있습니다.", Bifrost.Common.MessageType.Error);
                return;
            }
        }

        public override void OnExcelExport()
        {

            GridHelper.ExcelExport(aGridExcel, "거래처");


            //if (_ds != null)

            //{
            //    string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            //    Bifrost.IniFile inifile = new Bifrost.IniFile();

            //    //Download
            //    string DownloadPath = inifile.IniReadValue("Download", "PATH", Path);

            //    if (DownloadPath == string.Empty)
            //    {
            //        //default 
            //        inifile.IniWriteValue("Download", "PATH", string.Empty, Path);
            //        DownloadPath = inifile.IniReadValue("Download", "PATH", Path);
            //    }

            //    SaveFileDialog saveFileDialog = new SaveFileDialog();
            //    saveFileDialog.FileName = "거래처_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            //    if (DownloadPath == string.Empty || DownloadPath == "")
            //    {

            //        //초기 파일명을 지정할 때 사용한다.
            //        saveFileDialog.Filter = "Excel|*.xlsx";
            //        saveFileDialog.Title = "Save an Excel File";
            //        saveFileDialog.ShowDialog();

            //        if (saveFileDialog.FileName != "")
            //        {
            //            ExcelHelper.SaveExcelDB(saveFileDialog.FileName, _ds);

            //            Process process = new Process();
            //            process.StartInfo.FileName = saveFileDialog.FileName;
            //            process.Start();
            //        }
            //    }
            //    else
            //    {
            //        if (saveFileDialog.FileName != "")
            //        {
            //            ExcelHelper.SaveExcelDB(DownloadPath + @"\\" + saveFileDialog.FileName, _ds);
            //            Process process = new Process();
            //            process.StartInfo.FileName = DownloadPath + @"\\" + saveFileDialog.FileName;
            //            process.Start();
            //        }
            //    }

            //}
        }
    }
}
