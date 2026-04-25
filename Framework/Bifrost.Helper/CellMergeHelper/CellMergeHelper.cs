using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Columns;

namespace Bifrost.Helper.CellMergeHelper
{
    public class CellMergeHelper
    {
        private List<MergedCell> _MergedCells = new List<MergedCell>();
        public List<MergedCell> MergedCells
        {
            get { return _MergedCells; }
        }

        aGridPainter painter;


        GridView _view;

        public CellMergeHelper(GridView view)
        {
            _view = view;
            view.CustomDrawCell += new RowCellCustomDrawEventHandler(view_CustomDrawCell);
            view.GridControl.Paint += new PaintEventHandler(GridControl_Paint);
            view.CellValueChanged += new CellValueChangedEventHandler(view_CellValueChanged);
            painter = new aGridPainter(view);
        }

        public MergedCell AddMergedCell(int rowHandle, GridColumn col1, GridColumn col2)
        {
            MergedCell cell = new MergedCell(rowHandle, col1, col2);
            _MergedCells.Add(cell);
            return cell;
        }
        public void AddMergedCell(int rowHandle, int col1, int col2, object value)
        {
            AddMergedCell(rowHandle, _view.Columns[col1], _view.Columns[col2]);
        }

        public void AddMergedCell(int rowHandle, GridColumn col1, GridColumn col2, object value)
        {
            MergedCell cell = AddMergedCell(rowHandle, col1, col2);
            SafeSetMergedCellValue(cell, value);
        }



        public void SafeSetMergedCellValue(MergedCell cell, object value)
        {
            if (cell != null)
            {
                SafeSetCellValue(cell.RowHandle, cell.Column1, value);
                SafeSetCellValue(cell.RowHandle, cell.Column2, value);
            }
        }

        public void SafeSetCellValue(int rowHandle, GridColumn column, object value)
        {
            if (_view.GetRowCellValue(rowHandle, column) != value)
                _view.SetRowCellValue(rowHandle, column, value);
        }

        private MergedCell GetMergedCell(int rowHandle, GridColumn column)
        {
            foreach (MergedCell cell in _MergedCells)
            {
                if (cell.RowHandle == rowHandle && (column == cell.Column1 || column == cell.Column2))
                    return cell;
            }
            return null;
        }

        private bool IsMergedCell(int rowHandle, GridColumn column)
        {
            return GetMergedCell(rowHandle, column) != null;
        }

        private void DrawMergedCells(PaintEventArgs e)
        {
            foreach (MergedCell cell in _MergedCells)
            {
                painter.DrawMergedCell(cell, e);
            }
        }


        void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            SafeSetMergedCellValue(GetMergedCell(e.RowHandle, e.Column), e.Value);
        }

        void GridControl_Paint(object sender, PaintEventArgs e)
        {
            DrawMergedCells(e);
        }

        void view_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (IsMergedCell(e.RowHandle, e.Column))
                e.Handled = !painter.IsCustomPainting;
        }

    }
}