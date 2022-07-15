using SmartRace.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartRace.Listeners
{
    internal class MouseDragEventListener
    {
        private readonly Control Control;

        private Point PreviousPoint;
        private Action<MouseDragEventArgs> Handler;

        public MouseDragEventListener(Control control, Action<MouseDragEventArgs> handler)
        {
            Control = control;
            control.MouseMove += MouseMove;

            Handler = handler;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location == PreviousPoint)
                return;

            if (PreviousPoint != null)
                Handler.Invoke(new MouseDragEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta, PreviousPoint));

            PreviousPoint = e.Location;
            return;
        }

        public void Remove()
        {
            Control.MouseMove -= MouseMove;
        }
    }
}
