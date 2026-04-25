using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace Bifrost.Grid
{
    public class BandMultiSelectionHelper
    {
        private readonly string markFieldName = "CheckMark_selectedRow";
        protected BandedGridView _view;
        protected ArrayList _selectedRow;
        protected ArrayList _selectedRowHandle = new ArrayList();

        protected BandedGridColumn column;
        protected RepositoryItemCheckEdit edit;
        protected const int CheckboxIndent = 4;


        /// <summary>
        /// 생성자
        /// </summary>
        public BandMultiSelectionHelper()
        {
            _selectedRow = new ArrayList();
        }
        public BandMultiSelectionHelper(BandedGridView view) : this()
        {
            this.View = view;
        }
        public BandedGridView View
        {
            get { return _view; }
            set
            {
                if (_view != value)
                {
                    Detach();
                    Attach(value);
                }
            }
        }
        public string MarkFieldName
        {
            get { return this.markFieldName; }
        }

        private bool _isMultiSelect = false;
        public bool IsMultiSelect
        {
            get { return this._isMultiSelect; }
            private set { this._isMultiSelect = value; }
        }


        public GridColumn CheckMarkColumn { get { return column; } }
        public int SelectedCount { get { return _selectedRow.Count; } }
        public object GetSelectedRow(int index)
        {
            return _selectedRow[index];
        }
        public int GetSelectedIndex(object row)
        {
            foreach (object record in _selectedRow)
                if ((record as DataRowView).Row == (row as DataRowView).Row)
                    return _selectedRow.IndexOf(record);
            return _selectedRow.IndexOf(row);
        }

        public int GetSelectedRowHandle(int index)
        {
            return (int)_selectedRowHandle[index];
        }
        public void ClearSelection(BandedGridView view)
        {
            _selectedRow.Clear();
            _selectedRowHandle.Clear();
            Invalidate(view);
        }
        public void SelectAll(BandedGridView view)
        {
            _selectedRow.Clear();
            // fast (won't work if the grid is filtered)
            //if(_view.DataSource is ICollection)
            //	_selectedRow.AddRange(((ICollection)_view.DataSource));
            //else
            // slow:
            for (int i = 0; i < view.DataRowCount; i++)
            {
                _selectedRow.Add(view.GetRow(i));
                _selectedRowHandle.Add(view.GetRowHandle(i));
            }
            Invalidate(view);
        }
        public void SelectGroup(int rowHandle, bool select, BandedGridView view)
        {
            if (IsGroupRowSelected(rowHandle, view) && select) return;
            for (int i = 0; i < view.GetChildRowCount(rowHandle); i++)
            {
                int childRowHandle = view.GetChildRowHandle(rowHandle, i);
                if (view.IsGroupRow(childRowHandle))
                    SelectGroup(childRowHandle, select, view);
                else
                    SelectRow(childRowHandle, select, false, view);
            }
            Invalidate(view);
        }
        void SelectRow(int rowHandle, bool select, bool invalidate, BandedGridView view)
        {
            if (IsRowSelected(rowHandle, view) == select) return;
            object row = view.GetRow(rowHandle);
            if (select)
            {
                _selectedRow.Add(row);
                _selectedRowHandle.Add(rowHandle);
            }
            else
            {
                _selectedRow.Remove(row);
                _selectedRowHandle.Remove(rowHandle);
            }
            if (invalidate)
            {
                Invalidate(view);
            }
        }
        void SelectRow(object row, bool select, bool invalidate, BandedGridView view)
        {
            if (IsRowSelected(row, view) == select) return;
            if (select)
                _selectedRow.Add(row);
            else
                _selectedRow.Remove(row);
            if (invalidate)
            {
                Invalidate(view);
            }
        }
        public void SelectRow(int rowHandle, bool select, BandedGridView view)
        {
            SelectRow(rowHandle, select, true, view);
        }
        public void SelectRow(object row, bool select, BandedGridView view)
        {
            SelectRow(row, select, true, view);
        }
        public void InvertRow_selectedRow(int rowHandle, BandedGridView view)
        {
            if (view.IsDataRow(rowHandle))
            {
                SelectRow(rowHandle, !IsRowSelected(rowHandle, view), view);
            }
            if (view.IsGroupRow(rowHandle))
            {
                SelectGroup(rowHandle, !IsGroupRowSelected(rowHandle, view), view);
            }
        }
        public bool IsGroupRowSelected(int rowHandle, BandedGridView view)
        {
            for (int i = 0; i < view.GetChildRowCount(rowHandle); i++)
            {
                int row = _view.GetChildRowHandle(rowHandle, i);
                if (view.IsGroupRow(row))
                {
                    if (!IsGroupRowSelected(row, view)) return false;
                }
                else
                    if (!IsRowSelected(row, view)) return false;
            }
            return true;
        }
        public bool IsRowSelected(int rowHandle, BandedGridView view)
        {
            if (view.IsGroupRow(rowHandle))
                return IsGroupRowSelected(rowHandle, view);

            object row = view.GetRow(rowHandle);

            if (row == null) return false;
            return GetSelectedIndex(row) != -1;
        }
        public bool IsRowSelected(object row, BandedGridView view)
        {
            if (row == null) return false;

            return GetSelectedIndex(row) != -1;
        }
        protected virtual void Attach(BandedGridView view)
        {
            if (view == null) return;
            _selectedRow.Clear();
            this._view = view;

            edit = view.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;

            column = view.Columns.Add();
            column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.Visible = true;
            column.VisibleIndex = 0;
            column.FieldName = this.MarkFieldName;
            column.Caption = "Mark";
            column.OptionsColumn.ShowCaption = false;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSize = false;
            column.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            column.Width = GetCheckBoxWidth();
            column.ColumnEdit = edit;
            //NEW LINE
            view.Bands[0].Columns.Insert(0, column);
            view.Bands[0].Caption = "Mark";
            view.Bands[0].OptionsBand.ShowCaption = false;
            view.Bands[0].OptionsBand.AllowSize = false;
            view.Click += View_Click;
            view.CustomDrawBandHeader += View_CustomDrawBandHeader;
            view.CustomDrawGroupRow += View_CustomDrawGroupRow;
            view.CustomUnboundColumnData += View_CustomUnboundColumnData;
            view.RowStyle += View_RowStyle;
            view.KeyDown += View_KeyDown;
            view.KeyUp += View_KeyUp;
            view.FocusedRowChanged += View_FocusedRowChanged;
            view.MouseDown += View_MouseDown;
        }

        private bool _checkHeader = false;
        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            BandedGridView view = sender as BandedGridView;
            GridHitInfo info = view.CalcHitInfo(e.Location);

            if (info.Column != null && info.HitTest == GridHitTest.Column && info.Column.FieldName == "CheckMark_selectedRow")//BandedGridView.CheckBoxSelectorColumnName)
            {
                if (_checkHeader) _checkHeader = false;
                else _checkHeader = true;

                if (_checkHeader)
                {
                    view.BeginUpdate();

                    for (int i = 0; i < view.RowCount; i++)
                    {
                        this.SelectRow(i, true, false, view);
                    }

                    view.EndUpdate();
                }
            }
        }

        void View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            this._view.BeginUpdate();

            // 클릭시 현재 Row 선택되게 하는 루틴
            //bool is_selectedRow = this.IsRowSelected(row, view);
            //this.SelectRow(row, !is_selectedRow, false, view);

            if (this.IsMultiSelect == true)
            {
                int start = 0, end = 0;

                if (e.PrevFocusedRowHandle <= e.FocusedRowHandle)
                {
                    start = e.PrevFocusedRowHandle;
                    end = e.FocusedRowHandle;
                }
                else
                {
                    start = e.FocusedRowHandle;
                    end = e.PrevFocusedRowHandle;
                }

                for (int i = start; i <= end; i++)
                {
                    this.SelectRow(i, true, false, this._view);
                }

            }

            this._view.EndUpdate();
        }

     
        protected virtual void Detach()
        {
            if (_view == null) return;
            if (column != null)
                column.Dispose();
            if (edit != null)
            {
                _view.GridControl.RepositoryItems.Remove(edit);
                edit.Dispose();
            }

            _view.Click -= new EventHandler(View_Click);
            _view.CustomDrawBandHeader -= new BandHeaderCustomDrawEventHandler(View_CustomDrawBandHeader);
            _view.CustomDrawGroupRow -= new RowObjectCustomDrawEventHandler(View_CustomDrawGroupRow);
            _view.CustomUnboundColumnData -= new CustomColumnDataEventHandler(View_CustomUnboundColumnData);
            _view.KeyDown -= new KeyEventHandler(View_KeyDown);
            _view.RowStyle -= new RowStyleEventHandler(View_RowStyle);

            // 2012-04-05 ADDED BY KDJ
            _view.KeyUp -= new KeyEventHandler(View_KeyUp);
            _view.FocusedRowChanged -= new FocusedRowChangedEventHandler(View_FocusedRowChanged);

            _view = null;
        }

        protected int GetCheckBoxWidth()
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            int width = 0;
            GraphicsInfo.Default.AddGraphics(null);
            try
            {
                width = info.CalcBestFit(GraphicsInfo.Default.Graphics).Width;
            }
            finally
            {
                GraphicsInfo.Default.ReleaseGraphics();
            }
            return width + CheckboxIndent * 2;
        }
        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked)
        {
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo info;
            DevExpress.XtraEditors.Drawing.CheckEditPainter painter;
            DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs args;
            info = edit.CreateViewInfo() as DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo;
            painter = edit.CreatePainter() as DevExpress.XtraEditors.Drawing.CheckEditPainter;
            info.EditValue = Checked;
            info.Bounds = r;
            info.CalcViewInfo(g);
            args = new DevExpress.XtraEditors.Drawing.ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }
        void Invalidate(BandedGridView view)
        {
            view.CloseEditor();
            view.BeginUpdate();
            view.EndUpdate();
        }
        void View_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            BandedGridView view = sender as BandedGridView;
            if (e.Column.Caption == "Mark")
            {
                if (e.IsGetData)
                    //e.Value = IsRowSelected(view.GetDataSourceRowIndex(e.ListSourceRowIndex), view); //IsRowSelected((view.DataSource as IList)[e.ListSourceRowIndex],view);
                    e.Value = IsRowSelected((view.DataSource as IList)[e.ListSourceRowIndex], view);
                else
                    SelectRow((view.DataSource as IList)[e.ListSourceRowIndex], (bool)e.Value, view);
            }
        }
        void View_KeyDown(object sender, KeyEventArgs e)
        {
            BandedGridView view = sender as BandedGridView;

            if (e.KeyCode == Keys.ShiftKey && this._isMultiSelect == false)
            {
                this.IsMultiSelect = true;
            }

            if (view.FocusedColumn.Caption != "Mark" || e.KeyCode != Keys.Space) return;
            InvertRow_selectedRow(view.FocusedRowHandle, view);
        }

        void View_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey & this._isMultiSelect == true)
            {
                this._isMultiSelect = false;
            }
        }

        void View_Click(object sender, EventArgs e)
        {
            BandedGridView view = sender as BandedGridView;
            BandedGridHitInfo info;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            Point viewOffset = view.GetViewInfo().Bounds.Location;
            //pt.Offset(-viewOffset.X, -viewOffset.Y);
            info = view.CalcHitInfo(pt);
            if (info.Band != null && info.Band.Caption == "Mark")
            {
                if (info.InBandPanel)
                {
                    if (SelectedCount == view.DataRowCount)
                        ClearSelection(view);
                    else
                        SelectAll(view);
                }
                if (info.InRowCell)
                {
                    InvertRow_selectedRow(info.RowHandle, view);
                }
            }
            if (info.InRow && view.IsGroupRow(info.RowHandle) && info.HitTest != BandedGridHitTest.RowGroupButton)
            {
                InvertRow_selectedRow(info.RowHandle, view);
            }
        }
        void View_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {

            if (e.Band != null && e.Band.Caption == "Mark")
            {
                BandedGridView view = sender as BandedGridView;
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(e.Graphics, e.Bounds, SelectedCount == view.DataRowCount);
                e.Handled = true;
            }
        }
        void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            BandedGridView view = sender as BandedGridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;

            info.GroupText = "         " + info.GroupText.TrimStart();
            e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
            e.Painter.DrawObject(e.Info);

            Rectangle r = info.ButtonBounds;
            r.Offset(r.Width + CheckboxIndent * 2 - 1, 0);
            DrawCheckBox(e.Graphics, r, IsGroupRowSelected(e.RowHandle, view));
            e.Handled = true;
        }
        void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            //BandedGridView view = sender as BandedGridView;
            //if (IsRowSelected(e.RowHandle, view))
            //{
            //    e.Appearance.BackColor = SystemColors.Highlight;
            //    e.Appearance.ForeColor = SystemColors.HighlightText;
            //}
        }
    }
}
