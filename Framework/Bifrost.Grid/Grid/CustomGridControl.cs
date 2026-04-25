using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;

namespace NF.A2P.Grid {
    public class CustomGridControl :GridControl {
        protected override void RegisterAvailableViewsCore(InfoCollection collection) {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridViewInfoRegistrator());
        }
    }
}
