using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.BandedGrid.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Drawing;
using DevExpress.Utils;
using DevExpress.XtraEditors.ViewInfo;

namespace Bifrost.Helper.CellMergeHelper
{
    public class aBandedGridPainter : BandedGridPainter
    {


        public aBandedGridPainter(BandedGridView view)
            : base(view)
        {

        }

        private bool _IsCustomPainting;
        public bool IsCustomPainting
        {
            get { return _IsCustomPainting; }
            set { _IsCustomPainting = value; }
        }

        public void DrawMergedCell(MergedCell cell, PaintEventArgs e)
        {
            try { 
                int delta = cell.Column1.VisibleIndex - cell.Column2.VisibleIndex;
                if (Math.Abs(delta) > 1)
                    return;
                BandedGridViewInfo vi = View.GetViewInfo() as BandedGridViewInfo;
                GridCellInfo gridCellInfo1 = vi.GetGridCellInfo(cell.RowHandle, cell.Column1);
                GridCellInfo gridCellInfo2 = vi.GetGridCellInfo(cell.RowHandle, cell.Column2);
                if (gridCellInfo1 == null || gridCellInfo2 == null)
                    return;
                Rectangle targetRect = Rectangle.Union(gridCellInfo1.Bounds, gridCellInfo2.Bounds);
                gridCellInfo1.Bounds = targetRect;
                gridCellInfo1.CellValueRect = targetRect;
                gridCellInfo2.Bounds = targetRect;
                gridCellInfo2.CellValueRect = targetRect;
                if (delta < 0)
                    gridCellInfo1 = gridCellInfo2;
                Rectangle bounds = gridCellInfo1.ViewInfo.Bounds;
                bounds.Width = targetRect.Width;
                bounds.Height = targetRect.Height;
                gridCellInfo1.ViewInfo.Bounds = bounds;
                gridCellInfo1.ViewInfo.CalcViewInfo(e.Graphics);
                IsCustomPainting = true;
                GraphicsCache cache = new GraphicsCache(e.Graphics);
                gridCellInfo1.Appearance.FillRectangle(cache, gridCellInfo1.Bounds);
                DrawRowCell(new BandedGridViewDrawArgs(cache, vi, vi.ViewRects.Bounds), gridCellInfo1);
                IsCustomPainting = false; ;
            }
            catch
            {
            }
        }

    }

}
