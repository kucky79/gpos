using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jarvis.Util
{
    public static class ScreenUtil
    {
        public static void SetValidPosition(Control control)
        {
            Rectangle workingArea = GetCurrentScreenBound();

            Point location = new Point();
            location = control.Location;

            if (control.Location.X < workingArea.Left) location.X = workingArea.Left;
            if (control.Location.Y < workingArea.Top) location.Y = workingArea.Top;
            if (control.Location.X > workingArea.Right) location.X = workingArea.Right - control.Width;
            if (control.Location.Y > workingArea.Bottom) location.Y = workingArea.Bottom - control.Height;

            control.Location = location;

        }

        private static Rectangle GetCurrentScreenBound()
        {
            Rectangle workingArea = new Rectangle();
            workingArea.X = int.MaxValue;
            workingArea.Y = int.MaxValue;
            workingArea.Width = 0;
            workingArea.Height = int.MaxValue;

            Screen[] screenList = Screen.AllScreens;

            foreach (Screen screen in screenList)
            {
                if (screen.WorkingArea.X <= workingArea.X) workingArea.X = screen.WorkingArea.X;
                if (screen.WorkingArea.Y <= workingArea.Y) workingArea.Y = screen.WorkingArea.Y;
                if (screen.WorkingArea.Height <= workingArea.Height) workingArea.Height = screen.WorkingArea.Height;

                workingArea.Width += screen.WorkingArea.Width;
            }

            return workingArea;
        }
    }
}
