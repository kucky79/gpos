using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace NF.A2P.Grid
{
    public class CustomGridViewInfo : GridViewInfo {
        public CustomGridViewInfo(GridView gridView) : base(gridView) { }

        public override void CalcRects(Rectangle bounds, bool partital) {
            base.CalcRects(bounds, partital);
            //ViewRects.IndicatorWidth *= View.RowCount;
            if (NewIndicatorWidth < ViewRects.IndicatorWidth)
            {
                NewIndicatorWidth = ViewRects.IndicatorWidth;
            }
            ViewRects.IndicatorWidth = NewIndicatorWidth;
        }
        public int NewIndicatorWidth { get; set; }
    }
}
