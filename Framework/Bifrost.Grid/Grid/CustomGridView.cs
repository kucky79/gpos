using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace NF.A2P.Grid
{
    public class CustomGridView :GridView {
        public CustomGridView(GridControl ownerGrid) : base(ownerGrid) { }
        public CustomGridView() : base() { }


        internal const string CustomName = "CustomGridView";
        protected override string ViewName {
            get { return CustomName; }
        }
    }
}
