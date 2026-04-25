using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base.ViewInfo;

namespace NF.A2P.Grid
{
    public class CustomGridViewInfoRegistrator :GridInfoRegistrator {
        public override string ViewName {
            get { return CustomGridView.CustomName; }
        }

        public override BaseView CreateView(GridControl grid) {
            return new CustomGridView(grid);
        }

        public override BaseViewInfo CreateViewInfo(BaseView view) {
            return new CustomGridViewInfo((GridView)view);
        }
    }
}
