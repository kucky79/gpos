using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;

using System.Diagnostics;
using System.IO;


using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Menu;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace Bifrost.Grid
{
    public partial class  aGrid : GridControl
    {
        private SetControlBinding _binding = null;
        private string[] _disabledEditingColumns = new string[] { };

        //gridView_MouseDown을 위한 변수
        private bool _isSelect = false;

        public delegate void aGridRowNoHandler();
 
        #region property
        private bool _isUpper = false;
        private GridView gridView1;

        [Category("Bifrost"), Browsable(true), Description("Chracter CasCading")]
        public bool isUpper
        {
            get { return _isUpper; }
            set
            {
                _isUpper = value;
                //강제 캡락 주석처리
                //if (_isUpper)
                //{
                //    //CapsLock 강제설정
                //    if (GetKeyState(20) == 0) // 0이면 눌려지지않음 1이면 눌려짐
                //    {
                //        PressKey(20); //keycode : 20 = capslock
                //    }
                //}
            }
        }

        [Category("Bifrost"), Browsable(true), Description("Layout Saving")]
        public bool isSaveLayout { get; set; } = false;

        [Category("Bifrost"), Browsable(true), Description("Layout Version")]
        private string _LayoutVersion = string.Empty;
        public string LayoutVersion
        {
            get
            {
                GridView view = this.MainView as GridView;
                if (view != null)
                {
                    view.OptionsLayout.LayoutVersion = _LayoutVersion;
                }
                return _LayoutVersion;
            }
            set
            {
                GridView view = this.MainView as GridView;
                if (view != null)
                {
                    _LayoutVersion = value;
                    view.OptionsLayout.LayoutVersion = _LayoutVersion;
                }
            }
        }

        [Category("Bifrost"), Browsable(true), Description("When last column, Press Enter key new row")]
        /// <summary>
        /// Grid Last column에서 엔터키나 탭키로 new row
        /// </summary>
        public bool AddNewRowLastColumn { get; set; } = false;

        [Category("Bifrost"), Browsable(true), Description("Column Edit Disable")]
        public string[] disabledEditingColumns { get; set; } = null;

        //bool ShouldSerializedisabledEditingColumns()
        //{
        //    return disabledEditingColumns != null;
        //}


        #region Verify Column
        [Browsable(false), Description("그리드에 Primary Key를 설정합니다.")]
        public string[] VerifyPrimaryKey { get; set; } = null;

        bool ShouldSerializeVerifyPrimaryKey()
        {
            return VerifyPrimaryKey != null;
        }

        [Browsable(false), Description("그리드에 빈값, 0이 허용되지 않는 컬럼을 설정합니다. 저장시 적용됩니다.")]
        public string[] VerifyNotNull { get; set; } = null;

        bool ShouldSerializeVerifyNotNully()
        {
            return VerifyNotNull != null;
        }

        [Browsable(false), Description("그리드에 컬럼값이 null경우 해당행을 삭제 합니다. 저장시 적용됩니다.")]
        public string[] VerifyNullDelete { get; set; } = null;

        bool ShouldSerializeVerifyNullDelete()
        {
            return VerifyNullDelete != null;
        }
        #endregion Verify Column

        public bool SetBindingEvnet { get; set; } = true;

        /// <summary>
        /// Active되어있는 셀의 Old 값을 가져옵니다. CellValueChange에서만 가져올수 있습니다.
        /// </summary>
        public string OldValue
        {
            get { return _oldValue; }
        }
        /// <summary>
        /// Active되어있는 셀의 new 값을 가져옵니다. CellValueChange에서만 가져올수 있습니다.
        /// </summary>
        public string NewValue
        {
            get { return _newValue; }
        }
        #endregion 

        #region 강제 CapsLock 설정
        [DllImport("User32.dll")]
        //선언합니다.
        public static extern void keybd_event(
          byte bVk, // virtual-key code 
          byte bScan, // hardware scan code 
          int dwFlags, // function options 
          ref int dwExtraInfo // additional keystroke data 
         );


        [DllImport("user32.dll")]
        private static extern short GetKeyState(int keyCode);

        private void PressKey(byte _Key)
        {
            const int KEYUP = 0x0002;
            int Info = 0;

            keybd_event(_Key, 0, 0, ref Info);   // key 다운
            keybd_event(_Key, 0, KEYUP, ref Info);  // key 업

        }
        #endregion

        #region Context Menu

        DXMenuItem[] menuItems;
        void InitializeMenuItems()
        {
            DXMenuItem itemExcel = new DXMenuItem("Excel Export", ItemExcel_Click);
            DXMenuItem itemExcelHidden = new DXMenuItem("Excel Export(With Hidden Column)", ItemExcelHidden_Click);

            menuItems = new DXMenuItem[] { itemExcel, itemExcelHidden };
        }

        #endregion

        #region Event 모음
        bool ChkEvent = false;
        int indicatorWidth = 15;
        private void InitEvent()
        {
            ChkEvent = true;
            GridView view = this.FocusedView as GridView;
            //this.TabStop = false;

            view.OptionsCustomization.AllowSort = false;
            view.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            view.OptionsBehavior.FocusLeaveOnTab = true;
            view.OptionsBehavior.AutoSelectAllInEditor = true;
            view.OptionsNavigation.AutoFocusNewRow = true;
            view.OptionsNavigation.AutoMoveRowFocus = true;
            view.OptionsNavigation.UseTabKey = true;
            view.OptionsNavigation.UseOfficePageNavigation = true;

            view.OptionsView.RowAutoHeight = true;

            //항번추가 초기화

            object obj = this.DataSource;
            int _cnt = 0;

            Graphics gr = Graphics.FromHwnd(view.GridControl.Handle);
            SizeF size;

            switch (obj.GetType().Name)
            {
                case nameof(DataTable):
                    DataTable _dt = this.DataSource as DataTable;
                    _cnt = _dt.Rows.Count;
                    size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                    view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + indicatorWidth;
                    break;

                case nameof(DataSet):
                    DataSet _ds = this.DataSource as DataSet;
                    for (int i = 0; i < _ds.Tables.Count; i++)
                    {
                        _cnt = _ds.Tables[i].Rows.Count;
                        size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                        view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + indicatorWidth;
                    }
                    break;
            }

            ViewRepositoryCollection viewCollection = this.ViewCollection;
            foreach (GridView gView in viewCollection)
            {
                //항번추가 이벤트
                gView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
                //앞에 인디케이터 AutoWidth 이벤트
                gView.RowCountChanged += gridView_RowCountChanged;
                //로우스타일(폰트 색상)
                //gView.RowCellStyle += View_RowCellStyle;
            }

            
            //cell or Row 선택 이벤트
            //view.MouseDown += gridView_MouseDown;
            
            //Context Menu 세팅
            InitializeMenuItems();
            //Context메뉴에 컬럼 픽스 추가
            view.PopupMenuShowing += View_PopupMenuShowing;
            
            
            //그리드 즉시 입력 적용
            view.CellValueChanging += View_CellValueChanging;
            view.CellValueChanged += View_CellValueChanged;
            
            //그리드 셀 포커스 이동시 Edit모드 해제
            view.ColumnPositionChanged += View_ColumnPositionChanged;

            //포커스 이동시 그전 데이터 업데이트
            view.FocusedColumnChanged += View_FocusedColumnChanged;

            //자동 그리드 데이터 업데이트를 위해 넣었으나, GridLookUp 과의 충돌로 일단 주석
            //view.LostFocus += View_LostFocus;

            //그리드 Sorting 후 첫 행으로 선택
            view.EndSorting += View_EndSorting;
            
            ////그리드 더블클릭시 에디트모드 들어가게
            //view.ShowingEditor += View_ShowingEditor;
            ////그리드 셀 더블클릭 에디트
            //this.MouseDoubleClick += AGrid_MouseDoubleClick;

            //그리드 선택한 cell Ctrl + C
            view.KeyDown += View_KeyDown;

            //셀에 대문자 출력(소문자가 안되고 무조건 대문자)
            //view.ShownEditor += View_ShownEditor;

            //선택체크박스 한번에 체크
            //헤더 클릭 소팅 해제

            view.MouseDown += View_MouseDown;
            view.MouseUp += View_MouseUp;

            //버튼으로 행추가시 new row 컬럼에 바로 커서 이동
            //view.RowUpdated += View_RowUpdated;
            view.ValidateRow += View_ValidateRow;

            //lastRow Add New Rows
            //view.GridControl.EditorKeyDown += GridControl_EditorKeyDown;
            //view.GridControl.KeyDown += GridControl_KeyDown;

           
            //Primary Key 설정을 위한
            view.InitNewRow += View_InitNewRow;

            // 그리드 헤더 라인 삭제
            //view.CustomDrawColumnHeader += View_CustomDrawColumnHeader;

        }

        private void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null || e.Column.FieldName != "Category_Name")
                return;

            e.Appearance.DrawBackground(e.Cache, e.Bounds);
            foreach (DevExpress.Utils.Drawing.DrawElementInfo info in e.Info.InnerElements)
            {
                if (!info.Visible) continue;
                DevExpress.Utils.Drawing.ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            }
            e.Painter.DrawCaption(e.Info, e.Info.Caption, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
            e.Handled = true;

        }

        //private void GridViewItem_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        //{
        //    if (e.Column == null || e.Column.FieldName != "Category_Name")
        //        return;
        //    // Fill column headers with the specified colors. 
        //    e.Cache.FillRectangle(Color.White, e.Bounds);
        //    e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
        //    // Draw the filter and sort buttons. 
        //    foreach (DrawElementInfo info in e.Info.InnerElements)
        //    {
        //        if (!info.Visible) continue;
        //        ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
        //    }
        //    e.Handled = true;
        //}



        DXMenuCheckItem CreateCheckItem(string caption, GridColumn column, FixedStyle style, Image image, bool grouping)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, column.Fixed == style, image, new EventHandler(OnFixedClick));
            item.BeginGroup = grouping;
            item.Tag = new MenuInfo(column, style);
            return item;
        }

        //Menu item click handler 
        void OnFixedClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            MenuInfo info = item.Tag as MenuInfo;
            if (info == null) return;
            info.Column.Fixed = info.Style;
        }

        //The class that stores menu specific information 
        class MenuInfo
        {
            public MenuInfo(GridColumn column, FixedStyle style)
            {
                this.Column = column;
                this.Style = style;
            }
            public FixedStyle Style;
            public GridColumn Column;
        }

        private void View_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;

            if (VerifyPrimaryKey == null) return;

            for (int i = 0; i < VerifyPrimaryKey.Length; i++)
            {
                view.SetRowCellValue(e.RowHandle, VerifyPrimaryKey[i], string.Empty);
            }
        }

        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.OptionsColumn.AllowEdit == false)
            {
                //Apply the appearance of the SelectedRow
                //e.Appearance.Assign(view.PaintAppearance.SelectedRow);
                //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
                e.Appearance.Options.UseForeColor = true;
                e.Appearance.ForeColor = Color.Gray;
            }
        }

        private void View_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            GridView view = this.FocusedView as GridView;

            if (view.FocusedRowHandle == GridControl.NewItemRowHandle)
            {
                this.BeginInvoke(new MethodInvoker(() => {
                    view.FocusedColumn = view.VisibleColumns[0];

                    view.ShowEditor();
                }));
            }
        }

        private void View_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (_isCanceled)
                return;

            if (sender.GetType().Name == nameof(GridView))
            {
                //최초 행추가 했을 경우에만 타게, 그 이후에는 정상작동하므료 태울필요 없음
                DataRow currentRow = view.GetFocusedDataRow();
                if (currentRow == null) return;
                if (currentRow.RowState == DataRowState.Added && view.FocusedRowHandle == 0)
                {
                    if (e.PrevFocusedColumn.ColumnEdit == null) //TextEdit 경우
                    {
                        if (e.PrevFocusedColumn.ReadOnly == true)
                        {
                            return;
                        }
                        else if (e.PrevFocusedColumn.OptionsColumn.AllowEdit == false)
                        {
                            return;
                        }

                        if (_previousRowHandle == view.FocusedRowHandle && _value != null)
                            view.SetRowCellValue(view.FocusedRowHandle, e.PrevFocusedColumn, _value);

                        _value = null;
                    }
                    else if (e.PrevFocusedColumn.ColumnEdit.GetType().Name == "RepositoryItemLookUpEdit")
                    {
                        if (e.PrevFocusedColumn.ReadOnly == true)
                        {
                            return;
                        }
                        else if (e.PrevFocusedColumn.OptionsColumn.AllowEdit == false)
                        {
                            return;
                        }

                        if (_previousRowHandle == view.FocusedRowHandle && _value != null)
                            view.SetRowCellValue(view.FocusedRowHandle, e.PrevFocusedColumn, _value);

                        _value = null;
                    }

                    view.PostEditor();
                    view.GetFocusedDataRow().EndEdit();
                    view.UpdateCurrentRow();
                }
            }
        }

        private void View_LostFocus(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            if (_isCanceled)
                return;

            if (sender.GetType().Name == nameof(GridView) || sender.GetType().Name == nameof(BandedGridView))
            {
                if(view.RowCount > 0)
                { 
                    view.PostEditor();
                    view.GetFocusedDataRow().EndEdit();
                    view.UpdateCurrentRow();
                }
            }
        }

        #region Last new Row

        //void GridControl_KeyDown(object sender, KeyEventArgs e)
        //{
        //    e.Handled = OnKeyDown(e.KeyCode, e.Modifiers);
        //}

        //void GridControl_EditorKeyDown(object sender, KeyEventArgs e)
        //{
        //    e.Handled = OnKeyDown(e.KeyCode, e.Modifiers);
        //}
        //private bool OnKeyDown(Keys keyCode, Keys modifiers)
        //{
        //    if (keyCode == Keys.Enter || keyCode == Keys.Tab)
        //    {
        //        return CheckAddNewRow();
        //    }
        //    return false;
        //}

        private bool CheckAddNewRow()
        {
            GridView view = this.FocusedView as GridView;

            if (view.FocusedColumn.VisibleIndex == view.VisibleColumns.Count - 1)
            {
                if (view.IsNewItemRow(view.FocusedRowHandle))
                {
                    view.PostEditor();
                    view.UpdateCurrentRow();
                }
                //마지막컬럼에서 엔터나 탭 누를시 New Row 여부
                if (view.IsLastRow && AddNewRowLastColumn)
                    return AddNewRow();
            }
            return false;
        }

        private bool AddNewRow()
        {
            GridView view = this.FocusedView as GridView;

            view.AddNewRow();
            view.FocusedColumn = view.VisibleColumns[0];
            return true;
        }
        #endregion

        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            //20200420 소팅은 필요없으니 주석 대신에 넓이 최적화로 변경
            if (hitInfo.InColumn)
            {
                hitInfo.Column.BestFit();
                hitInfo.Column.SortOrder = DevExpress.Data.ColumnSortOrder.None;
            }

            //소팅 순서 바꾸기 
            //if (hitInfo.InColumn && hitInfo.Column.SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
            //{
            //    hitInfo.Column.SortOrder = DevExpress.Data.ColumnSortOrder.None;
            //}
        }

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if(view != null && e != null)
            KeepSelection(e, view);
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.OptionsSelection.MultiSelectMode == GridMultiSelectMode.RowSelect)
                {
                    Clipboard.SetText(view.GetFocusedDisplayText());
                    e.Handled = true;
                }
                else
                {
                    Copy(view, false);
                    //Clipboard.SetText(view.GetSelectedCells());
                    //e.Handled = true;
                }
            }
        }

        private string[,] selection;
        private Rectangle selectedArea = Rectangle.Empty;

        private void ClearSelection()
        {
            selection = null;
            selectedArea.X = int.MaxValue;
        }
        private void Copy(GridView fView, bool cut)
        {
            ClearSelection();
            GridCell[] cells = fView.GetSelectedCells();
            if (cells.Length == 0) return;
            int minCol, minRow, maxCol, maxRow;
            minCol = minRow = int.MaxValue;
            maxCol = maxRow = 0;
            foreach (GridCell cell in cells)
            {
                minCol = Math.Min(cell.Column.VisibleIndex, minCol);
                minRow = Math.Min(cell.RowHandle, minRow);
                maxCol = Math.Max(cell.Column.VisibleIndex, maxCol);
                maxRow = Math.Max(cell.RowHandle, maxRow);
            }
            selection = new string[maxCol - minCol + 1, maxRow - minRow + 1];
            foreach (GridCell cell in cells)
            {
                selection[cell.Column.VisibleIndex - minCol, cell.RowHandle - minRow] =
                    fView.GetRowCellValue(cell.RowHandle, cell.Column).ToString();
                if (cut) fView.SetRowCellValue(cell.RowHandle, cell.Column, null);
            }
            selectedArea = new Rectangle(minCol, minRow, maxCol - minCol, maxRow - minRow);
        }

        private void View_ShownEditor(object sender, EventArgs e)
        {
            GridView view = this.FocusedView as GridView;
            if (isUpper)
            {
                RepositoryItemTextEdit editor = (RepositoryItemTextEdit)this.RepositoryItems.Add("TextEdit");
                editor.CharacterCasing = CharacterCasing.Upper;

                foreach (GridColumn col in view.Columns)
                    col.ColumnEdit = editor;
            }
        }

        private void View_EndSorting(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            view.FocusedRowHandle = 0;
        }
        
        private int GetNextIndex(GridView grid)
        {
            int index = grid.VisibleColumns.IndexOf(grid.FocusedColumn);
            if (index < grid.VisibleColumns.Count - 1)
                index++;
            else
                index = 0;
            return index;
        }
        private void KeepSelection(MouseEventArgs e, GridView view)
        {
            try
            {
                GridHitInfo hi = view.CalcHitInfo(e.Location);

                if (hi.InRowCell)
                {
                    if (hi.Column == null) return;
                    if (hi.Column.FieldName == "DX$CheckboxSelectorColumn")
                    {
                        bool isSelected = view.IsRowSelected(hi.RowHandle);

                        if (isSelected)
                        {
                            view.UnselectRow(hi.RowHandle);
                            (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;

                        }
                        else
                        {
                            view.SelectRow(hi.RowHandle);
                            (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
                        }
                    }

                    if (hi.Column.ColumnEdit != null)
                    {
                        //그리드 CheckEdit일 경우에만 바로 체크, 언체크 되게 하려고. 이조건이 없으면 전체가 에디트 모드로 바뀜
                        if (hi.Column.ColumnEdit.GetType() == typeof(RepositoryItemCheckEdit))
                        {
                            view.FocusedRowHandle = hi.RowHandle;
                            view.FocusedColumn = hi.Column;
                            view.ShowEditor();
                            CheckEdit edit = (view.ActiveEditor as CheckEdit);
                            if (edit != null)
                            {
                                edit.Toggle();
                                (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
                            }
                        }
                        else if (hi.Column.ColumnEdit.GetType() == typeof(RepositoryItemTextEdit))
                        {
                            view.FocusedRowHandle = hi.RowHandle;
                            view.FocusedColumn = hi.Column;
                            view.ShowEditor();

                            if (view.IsEditorFocused)
                            {
                                view.CloseEditor();

                            }
                            //TextEdit edit = (view.ActiveEditor as TextEdit);
                            //edit.MouseDoubleClick += Edit_MouseDoubleClick;
                            //if (edit != null)
                            //{
                            //    view.CloseEditor();
                            //}
                        }
                    }
                    else //Default는 TextEdit임
                    {
                        if(view.GetFocusedDataRow() != null)
                        view.GetFocusedDataRow().EndEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void View_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                if (e.MenuType == GridMenuType.Row)
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    // Delete existing menu items, if any.
                    e.Menu.Items.Clear();
                    // Add a submenu with a single menu item.
                    GridView view = sender as GridView;
                    view.FocusedRowHandle = e.HitInfo.RowHandle;
                    for (int i = 0; i < menuItems.Length; i++)
                    {
                        e.Menu.Items.Add(menuItems[i]);
                    }
                }
            }

            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
            {
                DevExpress.XtraGrid.Menu.GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                //Erasing the default menu items 
                //menu.Items.Clear();
                if (menu.Column != null)
                {
                    //Adding new items 
                    menu.Items.Add(CreateCheckItem("Fixed None", menu.Column, FixedStyle.None, Properties.Resources.ColumnFix_None, true));
                    menu.Items.Add(CreateCheckItem("Fixed Left", menu.Column, FixedStyle.Left, Properties.Resources.ColumnFix_Left, false));
                    menu.Items.Add(CreateCheckItem("Fixed Right", menu.Column, FixedStyle.Right, Properties.Resources.ColumnFix_Right, false));
                }
            }
        }

        string DownloadPath = string.Empty;
        private void ItemExcel_Click(object sender, EventArgs e)
        {
            GridView view = this.MainView as GridView;

            //view.BestFitColumns();
            view.OptionsPrint.AutoWidth = false;
            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            IniFile inifile = new IniFile();

            //Download
            DownloadPath = inifile.IniReadValue("Download", "PATH", Path);

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
            saveFileDialog.FileName = "Bifrost_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            if (DownloadPath == string.Empty || DownloadPath == "")
            {
              
                //초기 파일명을 지정할 때 사용한다.
                saveFileDialog.Filter = "Excel|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        this.ExportToXlsx(saveFileDialog.FileName, xlsxOptions);
                        Process process = new Process();
                        process.StartInfo.FileName = saveFileDialog.FileName;
                        process.Start();
                    }
                }
            }
            else
            {
                if (saveFileDialog.FileName != "")
                {
                    this.ExportToXlsx(DownloadPath + @"\\" + saveFileDialog.FileName, xlsxOptions);
                    Process process = new Process();
                    process.StartInfo.FileName = DownloadPath + @"\\" + saveFileDialog.FileName;
                    process.Start();
                }
            }
        }

        private void ItemExcelHidden_Click(object sender, EventArgs e)
        {
            GridView view = this.MainView as GridView;

            string Path = AppDomain.CurrentDomain.BaseDirectory + @"Setting.ini";
            IniFile inifile = new IniFile();

            //Download
            DownloadPath = inifile.IniReadValue("Download", "PATH", Path);

            if (DownloadPath == string.Empty)
            {
                //default 
                inifile.IniWriteValue("Download", "PATH", string.Empty, Path);
                DownloadPath = inifile.IniReadValue("Download", "PATH", Path);
            }

            XlsxExportOptionsEx xlsxOptions = new XlsxExportOptionsEx();
            xlsxOptions.ShowGridLines = true;   // 라인출력 
            xlsxOptions.ExportType = DevExpress.Export.ExportType.Default;    // ExportType
            xlsxOptions.TextExportMode = TextExportMode.Text;
            xlsxOptions.RawDataMode = true;

            //aGrid1.ExportToXlsx(exportFilePath, xlsxOptions);
            string tempPath = Application.StartupPath + @"\Temp\";
            DirectoryInfo di = new DirectoryInfo(tempPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            //view.SaveLayoutToXml(tempPath + "tempLayout.xml");
            //foreach (GridColumn column in view.Columns)
            //{
            //    column.Visible = true;
            //}
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Bifrost_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            if (DownloadPath == string.Empty || DownloadPath == "")
            {

                //초기 파일명을 지정할 때 사용한다.
                saveFileDialog.Filter = "Excel|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    this.ExportToXlsx(saveFileDialog.FileName, xlsxOptions);
                    Process process = new Process();
                    process.StartInfo.FileName = saveFileDialog.FileName;
                    process.Start();
                }
            }
            else
            {
                if (saveFileDialog.FileName != "")
                {
                    this.ExportToXlsx(DownloadPath + @"\\" + saveFileDialog.FileName, xlsxOptions);
                    Process process = new Process();
                    process.StartInfo.FileName = DownloadPath + @"\\" + saveFileDialog.FileName;
                    process.Start();
                }
            }
            //view.RestoreLayoutFromXml(tempPath + "tempLayout.xml");
        }


        private void AGrid_MouseLeave(object sender, EventArgs e)
        {
            GridView view = this.FocusedView as GridView;
            view.PostEditor();
            view.ShowEditor();
            view.UpdateCurrentRow();
        }

        object _value;
        int _previousRowHandle;
        private void View_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            GridView view = this.FocusedView as GridView;

            //columnEdit 세팅했을경우에 바로 입력되게
            if (e.Column.ColumnEdit != null)
            {
                if (view.ActiveEditor == null)
                    return;
                if (view.ActiveEditor.GetType().Name == "MemoEdit")
                {
                    return;
                    //view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);

                    //엔터로 셀 높이 같이 늘려주는건데 좀 이상 나중에 확인
                    //MemoEdit memoEdit = (view.ActiveEditor as MemoEdit);
                    //int selectionStart = memoEdit.SelectionStart;
                    //int selectionLength = memoEdit.SelectionLength;

                    //view.PostEditor();
                    //view.ShowEditor();

                    //memoEdit.SelectionStart = selectionStart;
                    //memoEdit.SelectionLength = selectionLength;

                }
                else if (view.ActiveEditor.GetType().Name == "TextEdit") //Mask 할일이 있으면 TextEdit로 써서
                {
                    //view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);

                    //view.PostEditor();
                    view.GetFocusedDataRow().EndEdit();
                    //view.UpdateCurrentRow();
                    return;
                }
                else if (view.ActiveEditor.GetType().Name == "ButtonEdit")
                {
                    return;
                }
                else if (view.ActiveEditor.GetType().Name == "LookUpEdit")
                {
                    //이걸 적용하면, 키 입력시 바로 적용됨
                    //view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);
                    _value = e.Value;

                    //view.PostEditor();
                    //view.GetFocusedDataRow().EndEdit();
                    //view.UpdateCurrentRow();
                    return;
                }
                else if (view.ActiveEditor.GetType().Name == "GridLookUpEdit")
                {
                    //이걸 적용하면, 키 입력시 바로 적용됨
                    //view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);
                    //_value = e.Value;

                    view.PostEditor();
                    //view.GetFocusedDataRow().EndEdit();
                    view.UpdateCurrentRow();
                    return;
                }
                else //그외의 경우는 CheckEdit 같은 마우스 클릭 한방으로 해결되는거라 
                {
                    view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);

                    view.PostEditor();
                    view.GetFocusedDataRow().EndEdit();
                    view.UpdateCurrentRow();
                }
            }
            else //columnEdit 가 없으면, 무조건 TextEdit다
            {
                //view.SetFocusedRowCellValue(view.FocusedColumn, e.Value);
                //view.PostEditor(); //Eidt 모드에서 나가게 해줌
                _value = e.Value;
                _previousRowHandle = e.RowHandle;
                view.GetFocusedDataRow().EndEdit();
                //view.CloseEditor();
                //view.UpdateCurrentRow(); //현재 행 업데이트와 함께 포커스 아웃. TextEdit는 이러면 무조건 첫글자 치고 이벤트 실행되서 글자가 안쳐짐
            }
        }

        private string _oldValue = string.Empty;
        private string _newValue = string.Empty;

        private void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView view = this.FocusedView as GridView;

            if (view.ActiveEditor == null) return;

            string oldValue = string.Empty;
            string newValue = GetString(e.Value);//view.ActiveEditor != null ? GetString(view.ActiveEditor.EditValue) : GetString(view.GetFocusedRowCellValue(e.Column));  

            //이걸 넣어주면, 코딩으로 행추가시 값이 빠지는 케이스가 생김
            //if (view.ActiveEditor == null)
            //{
            //    view.PostEditor();
            //    view.UpdateCurrentRow();
            //    view.CloseEditor();
            //    view.ValidateEditor();
            //    return;
            //}

            if (view.ActiveEditor.GetType().Name == "CheckEdit")
            {
                CheckEdit _CheckEdit = view.ActiveEditor as CheckEdit;
                if (_CheckEdit.Checked)
                {
                    oldValue = "Y";
                    newValue = "N";
                }
                else
                {
                    oldValue = "N";
                    newValue = "Y";
                }
                _oldValue = newValue;
                _newValue = oldValue;
            }
            else if (view.ActiveEditor.GetType().Name == "MemoEdit")
            {
                //view.GetFocusedDataRow().EndEdit();
                view.CloseEditor();
            }
            else if (view.ActiveEditor.GetType().Name == "LookupEdit")
            {
                return;
            }
            else
            {
                oldValue = GetString(view.ActiveEditor.OldEditValue);
                newValue = GetString(view.ActiveEditor.EditValue);

                _oldValue = oldValue;
                _newValue = newValue;
            }

            if (oldValue == newValue) return;

            view.PostEditor();
            view.UpdateCurrentRow();
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            GridView gridView = ((GridView)sender);

            if (gridView.OptionsSelection.MultiSelectMode == DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect)
                return;

            bool _MultiSelect = false;
            GridMultiSelectMode _SelectMode = GridMultiSelectMode.RowSelect;

            bool _MultiSelectTmp = false;
            GridMultiSelectMode _SelectModeTmp = GridMultiSelectMode.RowSelect;

            _MultiSelect = gridView.OptionsSelection.MultiSelect;
            _SelectMode = gridView.OptionsSelection.MultiSelectMode;

            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                _MultiSelectTmp = _MultiSelect;
                _SelectModeTmp = _SelectMode;

                gridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
                gridView.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
                gridView.OptionsSelection.MultiSelect = true;
                gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
                _isSelect = true;
            }
            else
            {
                if (_isSelect)
                {
                    _MultiSelect = _MultiSelectTmp;
                    _SelectMode = _SelectModeTmp;
                }
                gridView.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.False;
                gridView.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
                gridView.OptionsSelection.MultiSelect = _MultiSelect;
                gridView.OptionsSelection.MultiSelectMode = _SelectMode;
                _isSelect = false;
            }
        }

        private void AGrid_DataSourceChanged(object sender, EventArgs e)
        {
            if(!ChkEvent)
                InitEvent();

            DataTable dt = this.DataSource as DataTable;

            if (VerifyPrimaryKey == null) return;

            DataColumn[] pkColumn = new DataColumn[VerifyPrimaryKey.Length];
            for (int i = 0; i < VerifyPrimaryKey.Length; i++)
            {
                pkColumn[i] = dt.Columns[VerifyPrimaryKey[i]];
            }

            dt.PrimaryKey = pkColumn;

        }

        private void gridView_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = ((GridView)sender);

            if (!view.GridControl.IsHandleCreated) return;

            object obj = this.DataSource;
            int _cnt = 0;

            Graphics gr = Graphics.FromHwnd(view.GridControl.Handle);
            SizeF size;

            switch (obj.GetType().Name)
            {
                case nameof(DataTable):
                    DataTable _dt = this.DataSource as DataTable;
                    _cnt = _dt.Rows.Count;
                    size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                    view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + indicatorWidth;
                    break;

                case nameof(DataSet):
                    DataSet _ds = this.DataSource as DataSet;
                    for (int i = 0; i < _ds.Tables.Count; i++)
                    {
                        _cnt = _ds.Tables[i].Rows.Count;
                        size = gr.MeasureString(_cnt.ToString(), view.PaintAppearance.Row.GetFont());
                        view.IndicatorWidth = Convert.ToInt32(size.Width + 1.5F) + GridPainter.Indicator.ImageSize.Width + indicatorWidth;
                    }
                    break;
            }
        }

        private void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void View_ColumnPositionChanged(object sender, EventArgs e)
        {
            GridView view = this.FocusedView as GridView;

            view.PostEditor();
            if (view.GetFocusedDataRow() == null) return;
            view.GetFocusedDataRow().EndEdit();
            view.UpdateCurrentRow();

            view.CloseEditor();
        }

        bool _isCanceled = false;
        private void AGrid_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridView view = this.FocusedView as GridView;

            //int FocusedColumnIndex = view.FocusedColumn.VisibleIndex;
            //int LastColumnIndex =  view.VisibleColumns.Count - 1;
            //int FocusedRowIndex = view.FocusedRowHandle;
            //int NextIndex = GetNextIndex(view);


            if (e.Modifiers == Keys.Shift)    // Ctrl 키 조합
            {
                if (e.KeyCode == Keys.Tab)
                {
                    MoveFocusPre(view, e);
                }
            }
            else if (e.Modifiers != Keys.Shift)
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    #region Keys : Enter
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (view.FocusedColumn.ColumnEdit != null && view.FocusedColumn.ColumnEdit.EditorTypeName == "MemoEdit")
                        {
                        }
                        else
                        {
                            GridControl grid = sender as GridControl;
                            PopupBaseEdit edit = grid.FocusedView.ActiveEditor as PopupBaseEdit;
                            if (edit != null && edit.IsPopupOpen)
                            {
                                return;
                            }
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            MoveFocus(view, e);
                        }
                    }
                    else if (e.KeyCode == Keys.Tab)
                    {
                        GridControl grid = sender as GridControl;
                        PopupBaseEdit edit = grid.FocusedView.ActiveEditor as PopupBaseEdit;
                        if (edit != null && edit.IsPopupOpen)
                        {
                            return;
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        MoveFocus(view, e);
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    #region Keys : Delete
                    if (!view.IsDataRow(view.FocusedRowHandle))
                        return;

                    GridColumn FocusedColumn = view.FocusedColumn;
                    int RowHandle = view.FocusedRowHandle;

                    //MemoEdit일경우
                    //if ((view.FocusedColumn.ColumnEdit != null && view.FocusedColumn.ColumnEdit.EditorTypeName == "MemoEdit") || (view.FocusedColumn.ColumnEdit != null && view.FocusedColumn.ColumnEdit.EditorTypeName == "TextEdit"))
                    {
                        if (!view.IsEditorFocused) //셀 에디트 모드인지 확인
                        {
                            string fieldName = view.FocusedColumn.FieldName;
                            if (FocusedColumn.OptionsColumn.AllowEdit == true) //Edit Allow인지 확인 (그리드 디자인단)
                            {
                                if (disabledEditingColumns != null) //Edit Disable인지 확인 (조회후 코드에서 설정)
                                {
                                    if (!disabledEditingColumns.Contains(fieldName))
                                    {
                                        view.FocusedColumn = FocusedColumn;
                                        view.FocusedRowHandle = view.FocusedRowHandle;
                                        view.SetFocusedRowCellValue(FocusedColumn, null);
                                        view.ShowEditor();
                                    }
                                }
                                else
                                {
                                    view.FocusedColumn = FocusedColumn;
                                    view.FocusedRowHandle = view.FocusedRowHandle;
                                    view.SetFocusedRowCellValue(FocusedColumn, null);
                                    view.ShowEditor();
                                }
                            }
                            e.Handled = true;
                        }
                    }
                    #endregion
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                _isCanceled = true;
            }

        }

        void MoveFocus(GridView view, KeyEventArgs e)
        {
            try
            {
                view.PostEditor();
                view.GetFocusedDataRow().EndEdit();
                view.UpdateCurrentRow();

                if (view.FocusedColumn == view.VisibleColumns.Last())
                {
                    if (view.IsLastRow)
                    {
                        CheckAddNewRow();
                    }
                    else
                    {
                        view.FocusedRowHandle += 1;
                        view.FocusedColumn = view.VisibleColumns.First();
                    }
                }
                else
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        void MoveFocusPre(GridView view, KeyEventArgs e)
        {
            GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs ci1 = viewInfo.ColumnsInfo.FirstColumnInfo;
            GridColumnInfoArgs ci2 = viewInfo.ColumnsInfo.LastColumnInfo;

            if (ci1.Column == view.FocusedColumn)
            {
                view.UnselectRow(view.FocusedRowHandle);
                view.FocusedColumn = ci2.Column;
                view.FocusedRowHandle = view.FocusedRowHandle - 1;
                e.Handled = true;
            }

            //try
            //{
            //    view.PostEditor();
            //    view.GetFocusedDataRow().EndEdit();
            //    view.UpdateCurrentRow();

            //    if (view.FocusedColumn == view.VisibleColumns.Last())
            //    {
            //        if (view.IsLastRow)
            //        {
            //            CheckAddNewRow();
            //        }
            //        else
            //        {
            //            view.FocusedRowHandle -= 1;
            //            view.FocusedColumn = view.VisibleColumns.First();
            //        }
            //    }
            //    else
            //    {
            //        e.Handled = true;
            //        e.SuppressKeyPress = true;
            //        view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex - 1];
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

        }

        private void AGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            GridView view = this.FocusedView as GridView;

            if (Control.ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                {
                    if (view.Appearance.FilterPanel.FontSizeDelta < 20)
                    {
                        view.Appearance.FilterPanel.FontSizeDelta++;
                        view.Appearance.Row.FontSizeDelta++;
                        view.Appearance.HeaderPanel.FontSizeDelta++;
                    }
                }
                else
                {
                    if (view.Appearance.FilterPanel.FontSizeDelta > -5)
                    {
                        view.Appearance.FilterPanel.FontSizeDelta--;
                        view.Appearance.Row.FontSizeDelta--;
                        view.Appearance.HeaderPanel.FontSizeDelta--;
                    }
                }

                //view.BestFitColumns();
            }
        }

        private void AGrid_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = this.FocusedView as GridView;

            if (e.Button == MouseButtons.Right && (ModifierKeys == (Keys.Control | Keys.Shift)))
            {
                DataTable dtGrid = new DataTable();
                DataTable dtData = new DataTable();

                dtData = this.DataSource as DataTable;

                dtGrid.Columns.Add("Name");
                dtGrid.Columns.Add("Field");
                dtGrid.Columns.Add("Type");
                dtGrid.Columns.Add("Vislble");
                dtGrid.Columns.Add("Editable");

                foreach (GridColumn item in view.Columns)
                {
                    DataRow newRow = dtGrid.NewRow();
                    newRow["Name"] = item.Caption;
                    newRow["Field"] = item.FieldName;
                    newRow["Type"] = item.ColumnType;
                    newRow["Vislble"] = item.Visible.ToString();
                    newRow["Editable"] = item.OptionsColumn.AllowEdit.ToString();

                    dtGrid.Rows.Add(newRow);
                }

                //GridInfo GridInfo = new GridInfo(dtGrid, dtData);
                //GridInfo.ShowDialog();
            }
        }

        #endregion

        #region Method 모음
        public void SetBinding(Control container, GridView aGridView, object[] EnableControlsIfAdded)
        {
            _binding = new SetControlBinding(aGridView, container, EnableControlsIfAdded);
        }

        public void SetBindingEvnetPossible(bool _isEvent)
        {
            if (_binding != null)
            {
                if (_isEvent)
                    _binding.InitControlEvent();
                else
                    _binding.InitControlEventDelete();
            }
        }

        public void SelectRow(int rowHandle)
        {
            _binding.SelectRow(rowHandle);
        }

        public void SetPopUp(string columnName, string popUpID, string[] columnNames, string[] returnColumnNames)
        {

        }

        public void DoRowUp(string OrderFieldName)
        {
            GridView view = this.FocusedView as GridView;

            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index <= 0) return;

            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index - 1);
            object val1 = row1[OrderFieldName];
            object val2 = row2[OrderFieldName];
            row1[OrderFieldName] = val2;
            row2[OrderFieldName] = val1;

            object[] row = row1.ItemArray;

            row1.ItemArray = row2.ItemArray;
            row2.ItemArray = row;

            view.FocusedRowHandle = index - 1;
        }

        public void DoRowDown(string OrderFieldName)
        {
            GridView view = this.FocusedView as GridView;

            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index >= view.DataRowCount - 1) return;

            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index + 1);
            object val1 = row1[OrderFieldName];
            object val2 = row2[OrderFieldName];

            row1[OrderFieldName] = val2;
            row2[OrderFieldName] = val1;

            object[] row = row1.ItemArray;

            row1.ItemArray = row2.ItemArray;
            row2.ItemArray = row;

            view.FocusedRowHandle = index + 1;
        }

        public void Insertrow(DataTable dt, int index)
        {
            dt.Rows.InsertAt(dt.NewRow(), index);
            Binding(dt);
        }

        public void Insertrow(DataTable dt, Hashtable ha_Filter)
        {
            DataRow I_Row = null;

            int Row_index = 0;

            I_Row = dt.NewRow();

            System.Text.StringBuilder Query = null;

            Query = new System.Text.StringBuilder();


            foreach (DictionaryEntry entry in ha_Filter)
            {
                I_Row[entry.Key.ToString()] =  entry.Value;

                Query.AppendFormat(" {0} = '{1}' AND", entry.Key.ToString(), entry.Value.ToString());
            }

            
            if (Query.Length > 0)
            {
                Query.Remove(Query.Length - 3, 3);
            }


            DataRow[] result = dt.Select(Query.ToString());

            if (result.Length > 0)
            {
                Row_index = dt.Rows.IndexOf(result[result.Length - 1]); 
            }

            dt.Rows.InsertAt(I_Row, Row_index);

            Binding(dt);
        }


        #region Datasource Getchange / Accecpt Change
        private DataTable _dt = new DataTable();
        /// <summary>
        /// 그리드에 있는 dataSource 의 상태를 가져옵니다.
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetChanges()
        {
            AGrid_MouseLeave(null, null);
            _dt = this.DataSource as DataTable;

            if (VerifyNullDelete != null) VerifyNullAutoDelete();

            if (VerifyNotNull != null) VerifyNullCheckColumn();

            DataTable dtChange = _dt.GetChanges();
            return dtChange;
        }

        /// <summary>
        /// 그리드컨트롤에 있는 Datasoruce AcceptChange
        /// </summary>
        public void AcceptChanges()
        {
            if (_dt != null)
                _dt.AcceptChanges();
        }
        #endregion Datasource Getchange / Accecpt Change

        #region Column Get/Set
        public object GetCol(string ColumnName)
        {
            GridView view = this.FocusedView as GridView;
            return view.GetFocusedDataRow() == null ? 0 : view.GetFocusedDataRow().Field<object>(ColumnName);
        }

        public object GetCol(int rowHandle, string ColumnName)
        {
            GridView view = this.FocusedView as GridView;
            return view.GetDataRow(rowHandle).Field<object>(ColumnName);
        }

        public void SetCol(string ColumnName, object value)
        {
            try
            {
                //이 방식으로 하면 컬럼이 없고, datatable에만 있는경우 set 불가
                //view.SetFocusedRowCellValue(ColumnName, value);

                GridView view = this.FocusedView as GridView;
                view.UpdateCurrentRow();

                DataView dv = view.DataSource as DataView;
                DataTable dt = dv.Table;

                dt.Rows[view.GetFocusedDataSourceRowIndex()][ColumnName] = value;
            }
            catch
            {
                throw;
            }
        }

        public void SetCol(GridColumn Column, object value)
        {
            GridView view = this.FocusedView as GridView;
            view.SetFocusedRowCellValue(Column, value);
        }

        public void SetCol(int rowHandle, string ColumnName, object value)
        {
            try
            {
                //이 방식으로 하면 컬럼이 없고, datatable에만 있는경우 set 불가
                //view.SetRowCellValue(rowHandle, ColumnName, value);

                GridView view = this.FocusedView as GridView;
                view.UpdateCurrentRow();

                DataView dv = view.DataSource as DataView;
                DataTable dt = dv.Table;

                dt.Rows[rowHandle][ColumnName] = value;
            }
            catch
            {
                throw;
            }
        }

        public void SetCol(int rowHandle, GridColumn Column, object value)
        {
            GridView view = this.FocusedView as GridView;
            view.SetRowCellValue(rowHandle, Column, value);
        }

        private void VerifyNullAutoDelete()
        {
            GridView view = this.FocusedView as GridView;

            for (int i = view.RowCount; i >= 0; --i)
            {
                for (int j = 0; j < VerifyNullDelete.Length; j++)
                {
                    if (GetCol(VerifyNullDelete[j]) == null || GetCol(VerifyNullDelete[j]).ToString() == string.Empty)
                    {
                        (DataSource as DataTable).Rows[i - 1].Delete();
                    }
                }
            }
        }

        private void VerifyNullCheckColumn()
        {
            GridView view = this.FocusedView as GridView;

            DataTable dt = this.DataSource as DataTable;
            DataTable dtChanges = dt.GetChanges();

            if (dtChanges == null) return;

            foreach (DataRow row in dtChanges.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                foreach (string item in this.VerifyNotNull)
                {
                    if (dtChanges.Columns[item].DataType != typeof(decimal))
                    {
                        if (GetString(row[item]) == string.Empty)
                        {
                            view.FocusedColumn = view.Columns[item];
                            view.ShowEditor();
                            throw new System.Exception(string.Format("[{0}]은 필수 입력 항목입니다.", view.Columns[item].Caption));
                        }
                    }

                    if (dtChanges.Columns[item].DataType == typeof(decimal))
                    {
                        if (GetDecimal(row[item]) == decimal.Zero)
                        {
                            view.FocusedColumn = view.Columns[item];
                            view.ShowEditor();
                            throw new System.Exception(string.Format("[{0}]은 필수 입력 항목입니다.", view.Columns[item].Caption));
                        }
                    }
                }
            }
        }
        #endregion Column Get/Set

        #region Summary
        public void Summary(string HeaderField, string HeaderFormat, string[] FieldName, string[] DisplayFormat)
        {
            GridView view = this.MainView as GridView;
            view.OptionsView.ShowFooter = true;
            view.CustomDrawFooterCell += View_CustomDrawFooterCell;

            view.Columns[HeaderField].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            view.Columns[HeaderField].SummaryItem.FieldName = HeaderField;
            view.Columns[HeaderField].SummaryItem.DisplayFormat = HeaderFormat;

            for (int i = 0; i < FieldName.Length; i++)
            {
                view.Columns[FieldName[i]].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                view.Columns[FieldName[i]].SummaryItem.FieldName = FieldName[i];
                view.Columns[FieldName[i]].SummaryItem.DisplayFormat = DisplayFormat[i];
            }
            
            SkinElement element = SkinManager.GetSkinElement(SkinProductId.Grid, DevExpress.LookAndFeel.UserLookAndFeel.Default, "FooterPanel");
            element.Color.BackColor = Color.LightGray;
            element.Color.BackColor2 = Color.LightGray;

            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
        }

        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            e.Appearance.DrawString(e.Cache, e.Info.DisplayText, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Handled = true;
        }
        #endregion

        #endregion

        #region Binding 
        /// <summary>
        /// Binding Grid 
        /// </summary>
        /// <param name="gridControl">aGrid control</param>
        /// <param name="dataSource">Prefer to use DataTable</param>
        public void Binding(object dataSource)
        {
            Binding(dataSource, false);
        }

        /// <summary>
        /// Binding Grid 
        /// </summary>
        /// <param name="gridControl">aGrid control</param>
        /// <param name="dataSource">Prefer to use DataTable</param>
        /// <param name="autoWidth">BestFit Width</param>

        public void Binding(object dataSource, bool autoWidth)
        {
            GridView view = this.MainView as GridView;

            this.BeginUpdate();
            this.DataSource = dataSource;

            if(autoWidth)
            {
                view.BestFitColumns();
            }
            
            this.EndUpdate();
        }

        #endregion

        //전체 디자인 적용을 위해 추가하였음
        //화면에서 아래 코드를 지우자
        //(Control).LookAndFeel.SkinName = "Office 2013";
        //(Control).LookAndFeel.UseDefaultLookAndFeel = false;
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    if (this.LookAndFeel.SkinName != "DevExpress Style")
        //    {
        //        this.LookAndFeel.SkinName = "DevExpress Style";
        //        this.LookAndFeel.UseDefaultLookAndFeel = true;
        //    }

        //}

        protected override void InitLayout()
        {
            //IME모드는 무조건 영문
            //this.ImeMode = ImeMode.Off;

            //Enter로 셀이동 & Delete키로 바로 삭제
            this.ProcessGridKey += AGrid_ProcessGridKey;
            this.DataSourceChanged += AGrid_DataSourceChanged;
            //그리드 확대 이벤트
            this.MouseWheel += AGrid_MouseWheel;
            this.MouseDown += AGrid_MouseDown;
            base.InitLayout();
        }

        private static string GetString(object val)
        {
            return GetString(val, false);
        }

        private static string GetString(object val, bool isUpper)
        {
            if (val == null || val == DBNull.Value)
                return "";

            string ret = string.Empty;
            if (isUpper)
                ret = val.ToString().ToUpper();
            else
                ret = val.ToString();

            if (ret == null)
                return "";

            return ret.Trim();
        }

        private static decimal GetDecimal(object val)
        {
            if (val == null || val == DBNull.Value)
                return 0;

            decimal ret = 0;
            try
            {
                ret = Convert.ToDecimal(val);
            }
            catch
            {
            }

            return ret;
        }

        private void InitializeComponent()
        {
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this;
            this.gridView1.Name = "gridView1";
            // 
            // aGrid
            // 
            this.MainView = this.gridView1;
            this.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
