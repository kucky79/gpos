using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bifrost.Win.Controls
{
    public class aScrollablePanelHelper
    {
        readonly aScrollablePanel _scrollableControl;

        public aScrollablePanelHelper(aScrollablePanel scrollableControl) { _scrollableControl = scrollableControl; }

        public void EnableScrollOnMouseWheel() { _scrollableControl.VisibleChanged += OnVisibleChanged; }

        void OnVisibleChanged(object sender, EventArgs e)
        {
            _scrollableControl.Select();
            UnsubscribeFromMouseWheel(_scrollableControl.Controls);
            SubscribeToMouseWheel(_scrollableControl.Controls);
        }

        public void SubscribeToMouseWheel(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.MouseWheel += OnMouseWheel;
                SubscribeToMouseWheel(ctrl.Controls);
            }
        }

        public void UnsubscribeFromMouseWheel(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.MouseWheel -= OnMouseWheel;
                UnsubscribeFromMouseWheel(ctrl.Controls);
            }
        }

        void OnMouseWheel(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            var scrollValue = _scrollableControl.VerticalScroll.Value;
            var largeChange = _scrollableControl.VerticalScroll.LargeChange;
            if (e.Delta < 0)
                _scrollableControl.VerticalScroll.Value += _scrollableControl.VerticalScroll.LargeChange;
            else
                if (scrollValue < largeChange) { _scrollableControl.VerticalScroll.Value = 0; }
            else { _scrollableControl.VerticalScroll.Value -= largeChange; }
        }

        public void DisableScrollOnMouseWheel()
        {
            _scrollableControl.VisibleChanged -= OnVisibleChanged;
            UnsubscribeFromMouseWheel(_scrollableControl.Controls);
        }

    }
}
