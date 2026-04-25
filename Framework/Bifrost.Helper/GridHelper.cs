using Bifrost.CommonFunction;
using Bifrost.Grid;
using Bifrost.Office;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraPrinting;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Bifrost.Helper
{

    public enum aGridColumnStyle
    {
        Default, Text, Numeric, Date, DateFull, Time, Time2, CheckBox, SingleDropDown, SingleColumnDropDown, MultiColumnDropDown, DropDownCalendar, EditPopup, Button, Quantity, Price, Amount, Rate, Ym,
        Price_K, Res, Biz
    }

    public enum aGridColumnEdit
    {
        LookUpEdit, CheckEdit
    }


    public static class GridHelper
    {

        private static Form GetParentForm(Control parent)
        {
            if (parent is Form)
                return parent as Form;

            return parent.FindForm();
        }
        public static void ExcelExport(aGrid gridControl, string Subtitle)
        {
            Bifrost.Helper.POSConfig cfgExcelViewer = POSConfigHelper.GetConfig("SYS004");
            bool UseExcelViewer = cfgExcelViewer.ConfigValue == "Y" ? true : false;
            ExcelExport(gridControl, Subtitle, UseExcelViewer);
        }

        public static void ExcelExport(aGrid gridControl, string Subtitle, bool isView)
        {
            GridView view = gridControl.MainView as GridView;

            //view.BestFitColumns();
            view.OptionsPrint.AutoWidth = false;
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

            XlsxExportOptionsEx xlsxOptions = new XlsxExportOptionsEx();
            xlsxOptions.ShowGridLines = true;   // 라인출력 
            xlsxOptions.SheetName = "Export";    // sheet 명
            xlsxOptions.ExportType = DevExpress.Export.ExportType.WYSIWYG;    // ExportType
            xlsxOptions.TextExportMode = TextExportMode.Value;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = Subtitle + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xlsx";

            if (DownloadPath == string.Empty || DownloadPath == "")
            {

                //초기 파일명을 지정할 때 사용한다.
                saveFileDialog.Filter = "Excel|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        gridControl.ExportToXlsx(saveFileDialog.FileName, xlsxOptions);

                        if (isView)
                        {
                            LoadData.StartLoading(GetParentForm(gridControl), "로딩중....", "엑셀 파일을 열고 있습니다.");
                            aExcelViewer aExcelViewer = new aExcelViewer();
                            aExcelViewer.FilePath = saveFileDialog.FileName;
                            aExcelViewer.Show();
                            LoadData.EndLoading();
                        }
                        else
                        {
                            Process process = new Process();
                            process.StartInfo.FileName = saveFileDialog.FileName;
                            process.Start();
                        }
                    }
                }
            }
            else
            {
                if (saveFileDialog.FileName != "")
                {
                    gridControl.ExportToXlsx(DownloadPath + @"\\" + saveFileDialog.FileName, xlsxOptions);
                    if (isView)
                    {
                        LoadData.StartLoading(GetParentForm(gridControl), "로딩중....", "엑셀 파일을 열고 있습니다.");
                        aExcelViewer aExcelViewer = new aExcelViewer();
                        aExcelViewer.FilePath = saveFileDialog.FileName;
                        aExcelViewer.Show();
                        LoadData.EndLoading();
                    }
                    else
                    {
                        Process process = new Process();
                        process.StartInfo.FileName = saveFileDialog.FileName;
                        process.Start();
                    }
                }
            }
        }


        #region GridView를 포커스 이동없이 강제 업데이트
        public static void UpdateGridView(object[] GridViewArray)
        {
            for (int i = 0; i < GridViewArray.Length; i++)
            {
                GridView gridView1 = GridViewArray[i] as GridView;

                if (gridView1.IsEditing)
                    gridView1.CloseEditor();

                if (gridView1.FocusedRowModified)
                    gridView1.UpdateCurrentRow();
            }
        }

        public static void UpdateGridView(GridView GridView)
        {
            if (GridView.IsEditing)
                GridView.CloseEditor();

            if (GridView.FocusedRowModified)
                GridView.UpdateCurrentRow();
        }
        #endregion

        #region SetGridStyle (Grid 초기 폼 세팅(상단 그룹박스, Sorting, 하단 Sum))
        public static void SetGridStyle(aGrid gridControl, bool _showGroup, bool _allowSort, bool _showSum)
        {
            GridView gridView = gridControl.MainView as GridView;

            if (gridView == null)
                return;
            if (_showSum)
            {
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    //상단캡션 정렬
                    gridView.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //하단Sum 값 적용
                    if (gridView.Columns[i].DisplayFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
                    {
                        gridView.Columns[i].Summary.Add(DevExpress.Data.SummaryItemType.Sum, gridView.Columns[i].FieldName, "{0:" + gridView.Columns[i].DisplayFormat.FormatString + "}");
                    }
                }
            }
            gridView.OptionsView.ShowGroupPanel = _showGroup;
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            gridView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridView.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gridView.OptionsCustomization.AllowSort = _allowSort;
            gridView.OptionsView.ShowFooter = _showSum;
            gridView.IndicatorWidth = 29;
            gridView.OptionsNavigation.EnterMoveNextColumn = true;



            //DataTable tempDT = new DataTable();
            //tempDT.TableName = "Table";
            //for (int i = 0; i < gridView.Columns.Count; i++)
            //{
            //    DataColumn dc = new DataColumn(gridView.Columns[i].FieldName);
            //    dc.DataType = gridView.Columns[i].ColumnType;
            //    //dc.DefaultValue = string.Empty;
            //    tempDT.Columns.Add(dc);
            //}
            //_DataBind(gridControl, tempDT);
        }

        public static void SetGridStyle(aGrid gridControl, bool _allowSort, bool _showSum)
        {
            GridView gridView = gridControl.MainView as GridView;

            if (_showSum)
            {
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    //상단캡션 정렬
                    gridView.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //하단Sum 값 적용
                    if (gridView.Columns[i].DisplayFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
                    {
                        gridView.Columns[i].Summary.Add(DevExpress.Data.SummaryItemType.Sum, gridView.Columns[i].FieldName, "{0:" + gridView.Columns[i].DisplayFormat.FormatString + "}");
                    }
                }
            }
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            gridView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            gridView.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gridView.OptionsCustomization.AllowSort = _allowSort;
            gridView.OptionsView.ShowFooter = _showSum;
            gridView.IndicatorWidth = 29;

            DataTable tempDT = new DataTable();
            tempDT.TableName = "Table";
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                DataColumn dc = new DataColumn(gridView.Columns[i].FieldName);
                dc.DataType = gridView.Columns[i].ColumnType;
                //dc.DefaultValue = string.Empty;
                tempDT.Columns.Add(dc);
            }
            _DataBind(gridControl, tempDT);
        }
        #endregion

        #region Internal Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        private static DataTable _GetGridDataSource(aGrid gridControl)
        {
            object dataSource = gridControl.DataSource;
            string dataMember = gridControl.DataMember == null ? string.Empty : gridControl.DataMember;

            if (dataSource is DataTable)
            {
                return (DataTable)dataSource;
            }
            else if (dataSource is DataViewManager)
            {
                if (dataMember.Contains("."))
                {
                    return ((DataViewManager)dataSource).DataSet.Relations[dataMember.Substring(dataMember.LastIndexOf(".") + 1)].ChildTable;
                }
                else
                {
                    return ((DataViewManager)dataSource).DataSet.Tables[dataMember];
                }
            }
            else if (dataSource is DataSet)
            {
                return ((DataSet)dataSource).Tables[dataMember];
            }
            else if (dataSource is DataView)
            {
                return ((DataView)dataSource).Table;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        private static DataTable _GetGridDataTable(object dataSource, string dataMember)
        {
            if (dataSource is DataTable)
            {
                return (DataTable)dataSource;
            }
            else if (dataSource is DataViewManager)
            {
                if (dataMember.Contains("."))
                {
                    return ((DataViewManager)dataSource).DataSet.Relations[dataMember.Substring(dataMember.LastIndexOf(".") + 1)].ChildTable;
                }
                else
                {
                    return ((DataViewManager)dataSource).DataSet.Tables[dataMember];
                }
            }
            else if (dataSource is DataSet)
            {
                return ((DataSet)dataSource).Tables[dataMember];
            }
            else if (dataSource is DataView)
            {
                return ((DataView)dataSource).Table;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Internal Binding method, all DataBind method use this
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="dtData"></param>
        private static void _DataBind(aGrid gridControl, object dtData)
        {
            _DataBind(gridControl, dtData, "Table");
        }

        /// <summary>
        /// Internal Binding method, all DataBind method use this
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="dtData"></param>
        private static void _DataBind(aGrid gridControl, object dtData, string dataMember)
        {
            GridColumnCollection columns = ((ColumnView)gridControl.Views[0]).Columns;

            Hashtable befColumns = new Hashtable();
            for (int i = 0; i < columns.Count; i++)
            {
                if (((ColumnView)gridControl.Views[0]).Columns[i].Visible) continue;

                befColumns.Add((((ColumnView)gridControl.Views[0]).Columns[i].FieldName), null);
            }

            gridControl.DataSource = dtData;
            if (!(dtData is DataTable)) gridControl.DataMember = dataMember;
            DataTable _dtData = _GetGridDataTable(dtData, dataMember);
            //gridControl.DataSource = _dtData;

            columns = ((ColumnView)gridControl.Views[0]).Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                GridColumn column = columns[i];
                if (!befColumns.Contains(column.FieldName))
                    column.Visible = true;

                if (_dtData == null) continue;
                if (!_dtData.Columns.Contains(column.FieldName)) continue;
                _dtData.Columns[column.FieldName].AllowDBNull = true;
                _dtData.Columns[column.FieldName].ReadOnly = false;

                //MaxLength 
                //if (_dtData.Columns[column.FieldName].MaxLength != -1)
                //    column. = _dtData.Columns[column.FieldName].MaxLength;
            }

            ///
            /// Finalize
            /// 
            befColumns = null;
            columns = null;
            //boundColumns = null;	

            GridView gView = new GridView();

            gView = gridControl.MainView as GridView;
            InitEvent(gView);
            gView.UpdateCurrentRow();

        }
        #endregion

        #region Event
        private static void InitEvent(GridView gView)
        {
            gView.KeyDown += gridView_KeyDown;
        }

        private static void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.KeyCode == Keys.V && e.Control)
            {
                foreach (var item in view.GetSelectedCells())
                {
                    view.SetRowCellValue(item.RowHandle, view.FocusedColumn, view.GetRowCellValue(item.RowHandle, item.Column));
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region GridSetColumn
        public class SetColumn
        {
            #region Protected

            protected aGrid gridControl;
            protected GridView gridView;
            protected GridColumn columnObject;

            protected void ApplyColumnStyle(aGridColumnStyle columnStyle)
            {
                RepositoryItemTextEdit riMaskedTextEdit = new RepositoryItemTextEdit();

                #region
                switch (columnStyle)
                {
                    case aGridColumnStyle.Default:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        break;

                    case aGridColumnStyle.Text:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        break;

                    case aGridColumnStyle.Numeric:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        // Format					
                        //this.DataType = typeof(System.Decimal);
                        //this.DecimalCount = 2;

                        break;
                    case aGridColumnStyle.Date:
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        string pattern = @"yyyy\/MM\/dd";
                        string regPattern = "([0-9][0-9][0-9][0-9])-(0[0-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])";
                        //riMaskedTextEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        //riMaskedTextEdit.DisplayFormat.FormatString = pattern;// A.GetDatePattern;

                        //riMaskedTextEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        //riMaskedTextEdit.EditFormat.FormatString = pattern;

                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                        riMaskedTextEdit.Mask.EditMask = regPattern;
                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created
                        //riMaskedTextEdit.CustomDisplayText += RiMaskedTextEdit_CustomDisplayText_Date;

                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;
                    case aGridColumnStyle.Res:
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                        riMaskedTextEdit.Mask.EditMask = "([0-9][0-9][0-9][0-9][0-9][0-9])-([1-4][0-9][0-9][0-9][0-9][0-9][0-9])";
                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created

                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;
                    case aGridColumnStyle.Biz:
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                        riMaskedTextEdit.Mask.EditMask = "([0-9][0-9][0-9])-([0-9][0-9])-([0-9][0-9][0-9][0-9][0-9])";
                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created

                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;
                    case aGridColumnStyle.DateFull:
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        //string pattern = @"yyyy\/MM\/dd";
                        //string regPattern = 
                        //riMaskedTextEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        //riMaskedTextEdit.DisplayFormat.FormatString = pattern;// A.GetDatePattern;

                        //riMaskedTextEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        //riMaskedTextEdit.EditFormat.FormatString = pattern;

                        riMaskedTextEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        riMaskedTextEdit.DisplayFormat.FormatString = "yyyy-MM-dd hh:mm:ss";

                        //riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                        //riMaskedTextEdit.Mask.EditMask = "([0-9][0-9][0-9][0-9])-(0[0-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (0[0-9]|1[0-9]):(0[0-9]|1[0-9]):(0[0-9]|1[0-9])"; 
                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created
                        //riMaskedTextEdit.CustomDisplayText += RiMaskedTextEdit_CustomDisplayText_Date;

                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;
                    case aGridColumnStyle.Ym:
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                        riMaskedTextEdit.Mask.EditMask = "([0-9][0-9][0-9][0-9])-(0[0-9]|1[0-2])";
                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true;


                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created

                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;
                    case aGridColumnStyle.Time:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        // Format & Mask
                        //this.FormatString = "HH:mm";
                        this.MaskInput = "hh:mm";
                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                        riMaskedTextEdit.Mask.EditMask = MaskInput;
                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created
                        columnObject.ColumnEdit = riMaskedTextEdit;
                        break;
                    case aGridColumnStyle.Time2:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        // Format & Mask
                        //this.FormatString = "HH:mm";
                        this.MaskInput = "hh:mm:ss";
                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
                        riMaskedTextEdit.Mask.EditMask = MaskInput;
                        riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created
                        columnObject.ColumnEdit = riMaskedTextEdit;
                        break;
                    case aGridColumnStyle.CheckBox:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        RepositoryItemCheckEdit _repChkYN = new RepositoryItemCheckEdit();
                        _repChkYN.ValueChecked = "Y";
                        _repChkYN.ValueUnchecked = "N";
                        _repChkYN.ValueGrayed = "";
                        columnObject.ColumnEdit = _repChkYN;
                        ShowHeaderCheckBox = true;

                        _repChkYN.CheckedChanged += _repChkYN_CheckedChanged;


                        break;

                    //case aGridColumnStyle.SingleDropDown:
                    //    columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;

                    //    columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //    break;
                    //case aGridColumnStyle.SingleColumnDropDown:
                    //    columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                    //    columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //    break;
                    //case aGridColumnStyle.MultiColumnDropDown:
                    //    columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                    //    columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //    break;
                    //case aGridColumnStyle.DropDownCalendar:
                    //    columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownCalendar;
                    //    columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //    break;

                    //case aGridColumnStyle.EditPopup:
                    //    columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
                    //    columnObject.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;

                    //    break;
                    //case aGridColumnStyle.Button:
                    //    columnObject.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
                    //    columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //    // Format & display
                    //    break;

                    case aGridColumnStyle.Price:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.DisplayFormat.FormatString = "n2";

                        //edit format
                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        riMaskedTextEdit.Mask.EditMask = "n2";
                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;
                    case aGridColumnStyle.Price_K:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.DisplayFormat.FormatString = "n0";

                        //edit format
                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        riMaskedTextEdit.Mask.EditMask = "n0";
                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;

                    case aGridColumnStyle.Quantity:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.DisplayFormat.FormatString = "n0";
                        columnObject.SummaryItem.DisplayFormat = "n0";

                        //edit format
                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        riMaskedTextEdit.Mask.EditMask = "n0";
                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;

                    case aGridColumnStyle.Amount:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.DisplayFormat.FormatString = "{0:n2}";

                        //edit format
                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        riMaskedTextEdit.Mask.EditMask = "n2";
                        columnObject.ColumnEdit = riMaskedTextEdit;


                        break;

                    case aGridColumnStyle.Rate:
                        columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        columnObject.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        columnObject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        columnObject.DisplayFormat.FormatString = "n2";

                        //edit format
                        riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        riMaskedTextEdit.Mask.EditMask = "n2";
                        columnObject.ColumnEdit = riMaskedTextEdit;

                        break;


                    default:
                        break;
                }
                #endregion
            }

            protected void ApplyColumnEdit(aGridColumnEdit columnEdit)
            {
                RepositoryItemLookUpEdit LookUpEdit = new RepositoryItemLookUpEdit();
                RepositoryItemCheckEdit CheckEdit = new RepositoryItemCheckEdit();
                CheckEdit.ValueChecked = "Y";
                CheckEdit.ValueUnchecked = "N";
                switch (columnEdit)
                {
                    case aGridColumnEdit.CheckEdit:
                        columnObject.ColumnEdit = CheckEdit;
                        break;
                }

            }
            private void _repChkYN_CheckedChanged(object sender, EventArgs e)
            {
                gridView.SetFocusedRowCellValue(columnObject.ColumnEditName, ((DevExpress.XtraEditors.CheckEdit)sender).Checked ? "Y" : "N");
            }

            #endregion

            #region Properties

            private String _ColumnTitle;

            /// <summary>
            /// Header Title
            /// </summary>
            public String ColumnTitle
            {
                get { return _ColumnTitle; }
                set
                {
                    _ColumnTitle = value;
                    columnObject.Caption = value;
                }
            }

            private aGridColumnStyle _ColumnStyle;

            /// <summary>
            /// Apply column Style: see CMAX.Framework.Win.Controls.aGridColumnStyle
            /// </summary>
            public aGridColumnStyle ColumnStyle
            {
                get { return _ColumnStyle; }
                set
                {
                    _ColumnStyle = value;
                    ApplyColumnStyle(value);
                }
            }

            private String _ColumnField = string.Empty;

            /// <summary>
            /// Database column bound to this UltraColumn. This column belongs to DataSource of Grid control.
            /// </summary>
            public String ColumnField
            {
                get { return _ColumnField; }
                set
                {
                    _ColumnField = value;
                    columnObject.FieldName = value;
                }
            }

            private bool _Editable = false;

            /// <summary>
            /// Set/Get the editing status of this column
            /// </summary>
            public bool Editable
            {
                get { return _Editable; }
                set
                {
                    _Editable = value;
                    columnObject.OptionsColumn.AllowEdit = value;
                    if (!value) columnObject.AppearanceCell.ForeColor = ColorTranslator.FromHtml("#808080");
                }
            }

            private bool _Visible = true;

            /// <summary>
            /// Show/Hide this columns in Grid
            /// </summary>
            public bool Visible
            {
                get { return _Visible; }
                set
                {
                    _Visible = value;
                    columnObject.Visible = value;
                }
            }
            private aGridColumnEdit _ColumEdit;

            public aGridColumnEdit ColumnEdit
            {
                get { return _ColumEdit; }
                set
                {
                    _ColumEdit = value;
                    ApplyColumnEdit(value);
                }
            }

            //private bool _AllowRowSummaries = false;

            ///// <summary>
            ///// Allow this column to supporting summary
            ///// </summary>
            //public bool AllowRowSummaries
            //{
            //    get { return _AllowRowSummaries; }
            //    set
            //    {
            //        _AllowRowSummaries = value;
            //        columnObject.AllowRowSummaries = value ? Infragistics.Win.UltraWinGrid.AllowRowSummaries.True : Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            //    }
            //}


            ///// <summary>
            ///// Set/Get datatype of this column
            ///// </summary>
            //public Type DataType
            //{
            //    get { return columnObject.ColumnType; }
            //    set
            //    {
            //        try
            //        {
            //            columnObject.ColumnType = value;
            //        }
            //        catch { }
            //    }
            //}

            //private object _DefaultCellValue = null;

            ///// <summary>
            ///// Default value of each cell in this column
            ///// </summary>
            //public object DefaultCellValue
            //{
            //    get { return _DefaultCellValue; }
            //    set
            //    {
            //        _DefaultCellValue = value;
            //        columnObject.DefaultCellValue = value;
            //    }
            //}

            private int _DecimalCount = 2;

            /// <summary>
            /// 1200.1234 --> 
            /// default: 2
            /// </summary>
            public int DecimalCount
            {
                get { return _DecimalCount; }
                set
                {
                    _DecimalCount = value;
                    columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    columnObject.DisplayFormat.FormatString = "0";
                }
            }

            private String _MaskInput;

            /// <summary>
            /// Set/Get mask string for this column
            /// </summary>
            public String MaskInput
            {
                get { return _MaskInput; }
                set
                {
                    _MaskInput = value;
                    columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    columnObject.DisplayFormat.FormatString = value;
                }
            }


            private String _FormatString;

            /// <summary>
            /// Returns or sets the format string used to control the display of text in this column.
            /// ie: yyyy--MM-dd: date format
            /// </summary>
            public String FormatString
            {
                get { return _FormatString; }
                set
                {
                    _FormatString = value;
                    columnObject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    columnObject.DisplayFormat.FormatString = value;
                }
            }

            private FixedStyle _Style;

            /// <summary>
            /// Specifies whether this column is fixed on scrolling
            /// </summary>
            public FixedStyle Fixed
            {
                get { return _Style; }
                set
                {
                    _Style = value;
                    columnObject.Fixed = value;
                }
            }


            private int _Width;

            /// <summary>
            /// Returns or sets the width of this column
            /// </summary>
            public int Width
            {
                get { return _Width; }
                set
                {
                    _Width = value;
                    columnObject.Width = value;
                }
            }


            private int _FieldLen = 0;

            /// <summary>
            /// Maximum character count can be entered to cell
            /// Default: unlimited
            /// </summary>
            public int FieldLen
            {
                get { return _FieldLen; }
                set { _FieldLen = value; }
            }

            private bool _ShowHeaderCheckBox = true;

            /// <summary>
            /// Show/Hide CheckBox in header of checkbox column
            /// </summary>
            public bool ShowHeaderCheckBox
            {
                get { return _ShowHeaderCheckBox; }
                set
                {
                    _ShowHeaderCheckBox = value;
                    this.columnObject.Tag = value;
                }
            }

            #endregion Properties

            #region Constructors

            public SetColumn(GridView gridView)
            {
                this.gridView = gridView;
            }

            public SetColumn(GridView gridView, string columnField)
            {
                this.gridView = gridView;

                columnObject = gridView.Columns.AddVisible(columnField, string.Empty);
                gridView.Columns.Add(columnObject);
            }

            public SetColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable)
            {
                this.gridView = gridView;

                columnObject = gridView.Columns.AddVisible(columnField, columnTitle);

                this.Width = columnWidth;
                this.Editable = editable;
                ApplyColumnStyle(aGridColumnStyle.Text);
            }

            public SetColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, bool visible)
            {
                this.gridView = gridView;

                columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
                this.Width = columnWidth;
                this.Editable = editable;
                this.Visible = visible;
                ApplyColumnStyle(aGridColumnStyle.Text);
            }

            public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable)
            {
                this.gridView = gridView;

                columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
                this.Width = columnWidth;
                this.Editable = editable;
                ApplyColumnStyle(columnStyle);
            }

            public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable, bool visible)
            {
                this.gridView = gridView;

                columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
                this.Width = columnWidth;
                this.Editable = editable;
                this.Visible = visible;
                ApplyColumnStyle(columnStyle);
            }

            public SetColumn(GridView gridView, string columnField, string columnTitle, aGridColumnStyle columnStyle, int columnWidth, bool editable, bool visible, string formatstring)
            {
                this.gridView = gridView;

                columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
                this.Width = columnWidth;
                this.Editable = editable;
                this.Visible = visible;
                this.FormatString = formatstring;
                ApplyColumnStyle(columnStyle);

            }

            public SetColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, bool visible, aGridColumnEdit columnEdit)
            {
                this.gridView = gridView;

                columnObject = gridView.Columns.AddVisible(columnField, columnTitle);
                this.Width = columnWidth;
                this.Editable = editable;
                this.Visible = visible;
                ApplyColumnEdit(columnEdit);

            }

            #endregion

            #region Static

            /// <summary>
            /// Return a SetColumn object from current existing aGridColumn
            /// </summary>
            /// <param name="gridControl"></param>
            /// <param name="columnField"></param>
            /// <param name="columnStyle"></param>
            /// <param name="columnTitle"></param>
            /// <param name="columnWidth"></param>
            /// <param name="editable"></param>
            /// <returns></returns>
            public static SetColumn FromGridColumn(GridView gridView, string columnField, aGridColumnStyle columnStyle, string columnTitle, int columnWidth, bool editable)
            {
                SetColumn helperColumn = new SetColumn(gridView);
                helperColumn.columnObject = gridView.Columns[columnField];

                helperColumn.ColumnTitle = columnTitle;
                helperColumn.Width = columnWidth;
                helperColumn.Editable = editable;
                helperColumn.ColumnStyle = columnStyle;

                return helperColumn;
            }


            /// <summary>
            /// Return a SetColumn object from current existing aGridColumn
            /// </summary>
            /// <param name="gridControl"></param>
            /// <param name="columnField"></param>
            /// <param name="columnStyle"></param>
            /// <param name="columnTitle"></param>
            /// <param name="columnWidth"></param>
            /// <param name="editable"></param>
            /// <returns></returns>
            public static SetColumnDropDown FromGridColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, string[] codeList)
            {
                SetColumnDropDown helperColumn = new SetColumnDropDown(gridView);
                helperColumn.columnObject = gridView.Columns[columnField];

                helperColumn.ColumnTitle = columnTitle;
                helperColumn.Width = columnWidth;
                helperColumn.Editable = editable;
                helperColumn.ColumnStyle = aGridColumnStyle.SingleDropDown;

                DevExpress.XtraEditors.Repository.RepositoryItemComboBox ItemCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                ItemCombo.Items.AddRange(codeList);
                helperColumn.columnObject.ColumnEdit = ItemCombo;

                return helperColumn;
            }


            /// <summary>
            /// Return a aGridHelperDropDownColumn object from current existing aGridColumn
            /// </summary>
            /// <param name="gridControl"></param>
            /// <param name="columnField"></param>
            /// <param name="columnTitle"></param>
            /// <param name="singleDropDown"></param>
            /// <param name="columnWidth"></param>
            /// <param name="editable"></param>
            /// <param name="textList"></param>
            /// <param name="valueList"></param>
            /// <returns></returns>
            public static SetColumnDropDown FromaGridColumn(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, DataTable dataSource)
            {
                SetColumnDropDown helperColumn = new SetColumnDropDown(gridView);
                helperColumn.columnObject = gridView.Columns[columnField];

                helperColumn.ColumnTitle = columnTitle;
                helperColumn.Width = columnWidth;
                helperColumn.Editable = editable;
                helperColumn.ColumnStyle = aGridColumnStyle.MultiColumnDropDown;

                ///
                /// Check validation
                /// 
                if (dataSource == null)
                    throw new Exception("Datatable 정보가 없습니다.\nDatatable을 확인해주세요");

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit LookupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                LookupEdit.DataSource = dataSource;
                helperColumn.columnObject.ColumnEdit = LookupEdit;

                return helperColumn;
            }

            #endregion
        }
        #endregion

        #region GridSetColumnDropDown

        public class SetColumnDropDown : SetColumn
        {


            #region Properties

            private Object _DataSource = null;

            /// <summary>
            /// Apply datasource for this column. Use in case of DropDown column
            /// </summary>
            public Object DataSource
            {
                get { return _DataSource; }
                set { _DataSource = value; }
            }

            private String _DataTextField;

            /// <summary>
            /// Table column which data will be displayed in Grid
            /// </summary>
            public String DataTextField
            {
                get { return _DataTextField; }
                set { _DataTextField = value; }
            }

            private String _DataValueField;

            /// <summary>
            /// Table column which data will be set to DBField column. Use only for DropDown column
            /// </summary>
            public String DataValueField
            {
                get { return _DataValueField; }
                set { _DataValueField = value; }
            }

            #endregion

            #region Constructors

            /// <summary>
            /// Create a Single DropDown column  with value & text list
            /// </summary>
            /// <param name="gridView">The Grid control which this column belongs to</param>
            /// <param name="columnField">The database field which bound to this column in grid</param>
            /// <param name="columnTitle">Title of this column</param>
            /// <param name="columnWidth">Width of this column in grid</param>
            /// <param name="editable">Indicate this column is editable or not</param>
            /// <param name="codeList">list of text will be displayed in dropdown</param>
            public SetColumnDropDown(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, string[] codeList)
                : base(gridView, columnField, columnTitle, columnWidth, editable)
            {

                DevExpress.XtraEditors.Repository.RepositoryItemComboBox ItemCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                ItemCombo.Items.AddRange(codeList);

                //바인딩
                columnObject.ColumnEdit = ItemCombo;
            }

            /// <summary>
            /// Create a Single DropDown column  with value & text list
            /// </summary>
            /// <param name="gridView">The Grid control which this column belongs to</param>
            /// <param name="columnField">The database field which bound to this column in grid</param>
            /// <param name="columnTitle">Title of this column</param>
            /// <param name="columnWidth">Width of this column in grid</param>
            /// <param name="editable">Indicate this column is editable or not</param>
            /// <param name="codeList">list of text will be displayed in dropdown</param>
            /// <param name="nameList">list of value will be assigned to grid cell's value when selected</param>
            public SetColumnDropDown(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, string[] codeList, object[] nameList)
                : base(gridView, columnField, columnTitle, columnWidth, editable)
            {
                ///
                /// Check validation
                /// 
                if (codeList.Length != nameList.Length)
                    throw new Exception("CodeList와 NameList는 수가 일치해야합니다.");

                DevExpress.XtraEditors.Repository.RepositoryItemComboBox ItemCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                ItemCombo.Items.AddRange(codeList);

                //바인딩
                columnObject.ColumnEdit = ItemCombo;
            }

            /// <summary>
            /// Create a Single DropDown column 
            /// </summary>
            /// <param name="gridControl">The Grid control which this column belongs to</param>
            /// <param name="columnField">The database field which bound to this column in grid</param>
            /// <param name="columnTitle">Title of this column</param>
            /// <param name="columnWidth">Width of this column in grid</param>
            /// <param name="editable">Indicate this column is editable or not</param>
            /// <param name="dataSource">The datasource which is bound to dropdown</param>
            public SetColumnDropDown(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, DataTable dataSource)
            : base(gridView, columnField, columnTitle, columnWidth, editable)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit LookupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                SetControl scr = new SetControl();
                scr.SetCombobox(LookupEdit, dataSource);
                columnObject.ColumnEdit = LookupEdit;
            }

            /// <summary>
            /// Create a Single DropDown column 
            /// </summary>
            /// <param name="gridControl">The Grid control which this column belongs to</param>
            /// <param name="columnField">The database field which bound to this column in grid</param>
            /// <param name="columnTitle">Title of this column</param>
            /// <param name="columnWidth">Width of this column in grid</param>
            /// <param name="editable">Indicate this column is editable or not</param>
            /// <param name="dataSource">The datasource which is bound to dropdown</param>
            public SetColumnDropDown(GridView gridView, string columnField, string columnTitle, int columnWidth, bool editable, bool codeNameVisible, DataTable dataSource)
            : base(gridView, columnField, columnTitle, columnWidth, editable)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit LookupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                SetControl scr = new SetControl();
                scr.SetCombobox(LookupEdit, dataSource, codeNameVisible);
                columnObject.ColumnEdit = LookupEdit;
            }

            public SetColumnDropDown(GridView gridView) : base(gridView)
            {
            }

            #endregion UltraGridHelperDropDownColumn

        }
        #endregion

        #region GetSelectedValues
        /// <summary>
        /// aGrid에 check박스로 선택한 값을 datatable로 가져오는 함수
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static DataTable GetSelectedValues(GridView view)
        {
            DataTable resultDt = new DataTable();

            resultDt = (view.DataSource as DataView).ToTable().Clone();

            int[] selectedRows = view.GetSelectedRows();

            foreach (int i in selectedRows)
            {
                int rowHandle = selectedRows[i];

                if (!view.IsGroupRow(rowHandle))
                {
                    DataRow row = view.GetDataRow(i);
                    resultDt.Rows.Add(row.ItemArray);
                }
            }
            return resultDt;
        }
        /// <summary>
        /// aGrid에 check박스로 선택한 값을 object배열로 가져오는 함수
        /// </summary>
        /// <param name="view"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static object[] GetSelectedValues(GridView view, string columnName)
        {
            int[] selectedRows = view.GetSelectedRows();
            object[] result = new object[selectedRows.Length];

            foreach (int i in selectedRows)
            {
                int rowHandle = selectedRows[i];
                if (!view.IsGroupRow(rowHandle))
                {
                    result[i] = view.GetRowCellValue(rowHandle, columnName);
                }
                else
                    result[i] = -1; // default value
            }
            return result;
        }
        #endregion

        #region SetDecimalPoint
        public static void SetDecimalPoint(GridControl aGrid, string column)
        {
            SetDecimalPoint(aGrid, column, 0);
        }
        public static void SetDecimalPoint(GridControl aGrid, string column, decimal dPoint)
        {

            GridView gView = aGrid.FocusedView as GridView;

            string DecimalPoint = "n" + dPoint.ToString();

            if (gView.Columns[column].DisplayFormat.FormatType == DevExpress.Utils.FormatType.Numeric)
            {
                gView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
                RepositoryItemTextEdit RepositoryItemTextEdit = new RepositoryItemTextEdit();

                //display format
                gView.Columns[column].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gView.Columns[column].DisplayFormat.FormatString = DecimalPoint;

                //edit format
                RepositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                RepositoryItemTextEdit.Mask.EditMask = DecimalPoint;
                gView.Columns[column].ColumnEdit = RepositoryItemTextEdit;
            }

        }

        public static void SetDecimalPoint(GridControl aGrid, string[] columns)
        {
            SetDecimalPoint(aGrid, columns, 0);
        }

        public static void SetDecimalPoint(GridControl aGrid, string[] columns, decimal dPoint)
        {
            GridView gView = aGrid.FocusedView as GridView;

            string DecimalPoint = "n" + dPoint.ToString();
            RepositoryItemTextEdit RepositoryItemTextEdit = new RepositoryItemTextEdit();

            for (int i = 0; i < columns.Length; i++)
            {
                //display format
                gView.Columns[columns[i]].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gView.Columns[columns[i]].DisplayFormat.FormatString = DecimalPoint;

                //edit format
                RepositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                RepositoryItemTextEdit.Mask.EditMask = DecimalPoint;
                gView.Columns[columns[i]].ColumnEdit = RepositoryItemTextEdit;
            }
        }

        public static void SetDecimalPoint(GridView gView, string[] columns, decimal dPoint)
        {
            string DecimalPoint = "n" + dPoint.ToString();
            RepositoryItemTextEdit RepositoryItemTextEdit = new RepositoryItemTextEdit();

            for (int i = 0; i < columns.Length; i++)
            {
                //display format
                gView.Columns[columns[i]].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gView.Columns[columns[i]].DisplayFormat.FormatString = DecimalPoint;

                //edit format
                RepositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                RepositoryItemTextEdit.Mask.EditMask = DecimalPoint;
                gView.Columns[columns[i]].ColumnEdit = RepositoryItemTextEdit;
            }
        }
        #endregion

        public static RepositoryItemTextEdit SetGridMask(MaskType keyType)
        {
            RepositoryItemTextEdit riMaskedTextEdit = new RepositoryItemTextEdit();

            switch (keyType)
            {
                case MaskType.DATE:
                    riMaskedTextEdit.Enter += RiMaskedTextEdit_Enter;
                    //riMaskedTextEdit.Leave += RiMaskedTextEdit_Leave;

                    riMaskedTextEdit.CustomDisplayText += RiMaskedTextEdit_CustomDisplayText_Date;
                    riMaskedTextEdit.EditValueChanged += RiMaskedTextEdit_EditValueChanged;
                    riMaskedTextEdit.Validating += RiMaskedTextEdit_Validating;

                    //riMaskedTextEdit.EditValueChanging += RiMaskedTextEdit_EditValueChanging;
                    //riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    //riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created
                    //riMaskedTextEdit.Mask.EditMask = A.GetDatePatternRegex;

                    break;
                case MaskType.TIME:
                    riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    riMaskedTextEdit.Mask.EditMask = "(0[0-9]|1[0-9]|2[0-3])([0-5][0-9])";//"(0[0-2]|0[0-4]):(0[0-5]|0[0-9])";
                    riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created
                    riMaskedTextEdit.CustomDisplayText += RiMaskedTextEdit_CustomDisplayText_Time;
                    break;
                case MaskType.NUMERIC:
                    riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    riMaskedTextEdit.Mask.EditMask = "n0";//"(0[0-2]|0[0-4]):(0[0-5]|0[0-9])";
                    riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true; // If enabled, the mask is also applied even when the cell's editor is not created
                    break;
                case MaskType.YYMM:
                    riMaskedTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    riMaskedTextEdit.Mask.EditMask = "([0-9][0-9][0-9][0-9])-(0[0-9]|1[0-2])";
                    riMaskedTextEdit.Mask.UseMaskAsDisplayFormat = true;
                    break;

                default:
                    break;
            }

            return riMaskedTextEdit;
        }

        private static void RiMaskedTextEdit_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private static void RiMaskedTextEdit_EditValueChanging(object sender, ChangingEventArgs e)
        {
            TextEdit riEditor = sender as TextEdit;
            Console.WriteLine(riEditor.Text);
            DateTime parsedDate;
            bool parsedSuccessfully = DateTime.TryParseExact(riEditor.Text.ToString(), A.GetDatePattern.Replace(@"\/", "").Replace("/", "").Replace("-", ""), CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            if (parsedSuccessfully)
            {
                riEditor.EditValue = parsedDate.Date.ToString(_outPattern);
                return;
            }
            else
            {
                riEditor.EditValue = e.OldValue;
                return;
            }
        }

        private static void RiMaskedTextEdit_Leave(object sender, EventArgs e)
        {
            TextEdit riEditor = sender as TextEdit;

            string date = A.GetString(riEditor.EditValue);

            //string regPattern = @"([123][0-9])[0-9][0-9](0[1-9]|1[012])([012][1-9]|[123]0|31)"; //"(0[0-9]|1[0-2])(0[1-9]|[1-2][0-9]|3[0-1])([0-9][0-9][0-9][0-9])";
            //riEditor.Properties.Mask.EditMask = regPattern;

            DateTime parsedDate;
            bool parsedSuccessfully = DateTime.TryParseExact(date, A.GetDatePattern.Replace(@"\/", "").Replace("/", "").Replace("-", ""), CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            if (parsedSuccessfully)
            {
                riEditor.EditValue = parsedDate.Date.ToString(_outPattern);
                riEditor.Text = riEditor.EditValue.ToString();
            }

            if (date == string.Empty)
            {
                riEditor.EditValue = _oldValue;
                riEditor.Text = _oldValue;
            }
        }

        private static string _outPattern = "yyyyMMdd";

        private static string _oldValue = string.Empty;
        private static void RiMaskedTextEdit_Enter(object sender, EventArgs e)
        {

            TextEdit riEditor = sender as TextEdit;

            //string regPattern = A.GetDatePatternRegex;
            string date = A.GetString(riEditor.EditValue);
            _oldValue = date;
            //riEditor.EditValue = string.Empty;

            //string regPattern = @"(0[1-9]|1[012])([012][1-9]|[123]0|31)([123][0-9])[0-9][0-9]"; //"(0[0-9]|1[0-2])(0[1-9]|[1-2][0-9]|3[0-1])([0-9][0-9][0-9][0-9])";

            //riEditor.Properties.Mask.EditMask = regPattern;

            //riEditor.EditValue = GetChangeDateFormat(date, _outPattern, A.GetDatePattern.Replace(@"\/", "").Replace("/", "").Replace("-", ""));


            //DateTime parsedDate;
            //bool parsedSuccessfully = DateTime.TryParseExact(date, _outPattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            //if (parsedSuccessfully)
            //{
            //    riEditor.EditValue = parsedDate.Date.ToString(A.GetDatePattern.Replace(@"\/", "").Replace("/", "").Replace("-", ""));
            //    riEditor.Text = riEditor.EditValue.ToString();
            //}
        }

        private static void RiMaskedTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit riEditor = sender as TextEdit;

            DateTime parsedDate;
            bool parsedSuccessfully = DateTime.TryParseExact(riEditor.EditValue.ToString(), A.GetDatePattern.Replace(@"\/", "").Replace("/", "").Replace("-", ""), CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            if (parsedSuccessfully)
            {
                riEditor.EditValue = parsedDate.Date.ToString(_outPattern);
                return;
            }
            else
            {
                riEditor.EditValue = _oldValue;
                return;
            }
        }

        private static void RiMaskedTextEdit_CustomDisplayText_Date(object sender, CustomDisplayTextEventArgs e)
        {
            string date = string.Empty;
            if (e.Value == null)
                date = "";
            else
                date = e.Value.ToString();
            DateTime parsedDate;
            bool parsedSuccessfully = DateTime.TryParseExact(date, _outPattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

            if (parsedSuccessfully)
                e.DisplayText = parsedDate.Date.ToString(A.GetDatePattern);
        }

        private static void RiMaskedTextEdit_CustomDisplayText_Time(object sender, CustomDisplayTextEventArgs e)
        {

            string textMask = string.Empty;
            if (e.Value == null)
                textMask = "";
            else
                textMask = e.Value.ToString();

            if (textMask == "")
            {
                textMask = "0000";
            }
            string pattern = "{0:00:##}"; // define a date pattern according to your requirements
            //    //e.DisplayText = string.Format("([0-9][0-9][0-9][0-9])/(0[0-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])", DateTime.ParseExact(date, "yyyyMMdd", KR_Format));
            e.DisplayText = String.Format(pattern, A.GetDecimal((textMask.ToString().Trim())));
        }

        /// <summary>
        /// Filter를 하기위해 사용하는 GridLookup
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static RepositoryItemGridLookUpEdit SetGridLookUpItem2(DataTable dt)
        {

            DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1 = new RepositoryItemGridLookUpEdit();
            repositoryItemGridLookUpEdit1.AutoHeight = false;
            repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            repositoryItemGridLookUpEdit1.DisplayMember = "NAME";
            repositoryItemGridLookUpEdit1.ValueMember = "CODE";
            repositoryItemGridLookUpEdit1.ShowFooter = false;

            repositoryItemGridLookUpEdit1.DataSource = dt;

            repositoryItemGridLookUpEdit1.PopulateViewColumns();

            repositoryItemGridLookUpEdit1.View.Columns.ColumnByFieldName("CODE").Visible = false;
            repositoryItemGridLookUpEdit1.View.Columns.ColumnByFieldName("CD_CLAS").Visible = false;
            repositoryItemGridLookUpEdit1.View.Columns.ColumnByFieldName("NO_SEQ").Visible = false;
            repositoryItemGridLookUpEdit1.View.Columns.ColumnByFieldName("DC_REF1").Visible = false;
            return repositoryItemGridLookUpEdit1;
        }

        public static RepositoryItemLookUpEdit SetGridLookUpItem(DataTable dt)
        {
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dt;
            repositoryItemLookUpEdit.ValueMember = dt.Columns[0].ToString();
            repositoryItemLookUpEdit.DisplayMember = dt.Columns[1].ToString();
            repositoryItemLookUpEdit.NullText = string.Empty;
            repositoryItemLookUpEdit.ShowNullValuePromptWhenFocused = true;
            repositoryItemLookUpEdit.ShowLines = false;
            repositoryItemLookUpEdit.ShowHeader = false;
            repositoryItemLookUpEdit.ShowFooter = false;
            repositoryItemLookUpEdit.UseDropDownRowsAsMaxCount = true;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.PopupFormMinSize = new System.Drawing.Size(50, 50);
            repositoryItemLookUpEdit.PopupResizeMode = ResizeMode.LiveResize;
            repositoryItemLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;

            //Name만 보이게 설정
            repositoryItemLookUpEdit.Columns.AddRange(new LookUpColumnInfo[] { new LookUpColumnInfo(dt.Columns[1].ToString()) });

            return repositoryItemLookUpEdit;
        }

        public static RepositoryItemLookUpEdit SetGridLookUpItem(DataTable dt, string[] objValue, string[] objName)
        {
            if (objValue.Length != objName.Length)
            {
                throw new Exception("코드(Code)와 명칭(Name)의 수가 다릅니다.\n갯수를 맞추어 주세요");
            }

            for (int i = 0; i < objValue.Length; i++)
            {
                if (!dt.Columns.Contains(objValue[i]))
                {
                    throw new Exception("컬럼이 존재하지 않습니다.\n컬럼명을 확인해주세요");
                }
            }

            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dt;
            repositoryItemLookUpEdit.ValueMember = objValue[0];
            repositoryItemLookUpEdit.DisplayMember = objValue[0];
            repositoryItemLookUpEdit.NullText = string.Empty;
            repositoryItemLookUpEdit.ShowNullValuePromptWhenFocused = true;
            repositoryItemLookUpEdit.ShowLines = false;
            repositoryItemLookUpEdit.ShowHeader = true;
            repositoryItemLookUpEdit.ShowFooter = true;
            repositoryItemLookUpEdit.UseDropDownRowsAsMaxCount = true;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.PopupFormMinSize = new System.Drawing.Size(50, 50);
            repositoryItemLookUpEdit.PopupResizeMode = ResizeMode.LiveResize;
            repositoryItemLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;

            for (int i = 0; i < objValue.Length; i++)
            {
                //Code, Name이 순으로 보이게 설정
                repositoryItemLookUpEdit.Columns.AddRange(new LookUpColumnInfo[] { new LookUpColumnInfo(objValue[i], objName[i]) });
            }

            return repositoryItemLookUpEdit;
        }

        private static string GetChangeDateFormat(string dt, string inputPattern, string outPattern)
        {
            string resultDt = string.Empty;

            DateTime parsedDate;
            bool parsedSuccessfully = DateTime.TryParseExact(dt, inputPattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            if (parsedSuccessfully)
            {
                resultDt = parsedDate.Date.ToString(outPattern);
            }

            return resultDt;
        }

    }


}
