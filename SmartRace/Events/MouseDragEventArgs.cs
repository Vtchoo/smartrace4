using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartRace.Events
{
    internal class MouseDragEventArgs : MouseEventArgs
    {
        public Point PreviousPoint { get; private set; }
        public Size PositionDelta { get { return new Size(X, Y) - new Size(PreviousPoint.X, PreviousPoint.Y); } }
        //public Point PositionDelta { get { return Point.Subtract(new Point(X, Y), new Size(PreviousPoint.X, PreviousPoint.Y)); } }

        public MouseDragEventArgs(MouseButtons button, int clicks, int x, int y, int delta, Point previousPoint) : base(button, clicks, x, y, delta)
        {
            PreviousPoint = previousPoint;
        }
    }
}
