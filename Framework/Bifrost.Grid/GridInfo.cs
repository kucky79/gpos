using DevExpress.XtraEditors;
using Bifrost.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Grid
{
    public partial class GridInfo : XtraForm
    {
        public GridInfo(DataTable dtGrdiInfo, DataTable dtDataInfo)
        {
            InitializeComponent();
            InitEvent();

            gridControl1.DataSource = dtGrdiInfo;
            gridControl2.DataSource = dtDataInfo;

            gridView1.BestFitColumns();
        }

        private void InitEvent()
        {
            btnExcel.Click += BtnExcel_Click;
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = gridControl2.DataSource as DataTable;

            ds.Tables.Add(dt);

            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            IniFile inifile = new IniFile();

            //Download
            string DownloadPath = inifile.IniReadValue("Download", "PATH", Path);

            if (DownloadPath == string.Empty)
            {
                //default 
                inifile.IniWriteValue("Download", "PATH", string.Empty, Path);
                DownloadPath = inifile.IniReadValue("Download", "PATH", Path);
            }


            if (DownloadPath == string.Empty || DownloadPath == "")
            {
                //SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.FileName = "AIMS_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";

                ExcelHelper.SaveExcelDB(DownloadPath + @"\\" + "AIMS_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx", ds);

            }
            else
            {
                ExcelHelper.SaveExcelDB(DownloadPath + @"\\" + "AIMS_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx", ds);
            }

        }
    }
}
