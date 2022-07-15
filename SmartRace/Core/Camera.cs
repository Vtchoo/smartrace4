using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartRace.Core
{
    class Camera
    {
        public Vector2 Position { get; set; }

        public Vector2 MousePosition { get; set; }

        public float Scale { get; set; } = 1f;

        private readonly List<ViewportHandler> viewportHandlers = new();

        public Camera(Control viewport)
        {
            viewportHandlers.Add(new ViewportHandler(viewport));
        }

        public Camera(List<ViewportHandler> viewports)
        {
            this.viewportHandlers = viewports;
        }

        public void AddViewport(Control viewport)
        {
            viewportHandlers.Add(new ViewportHandler(viewport));
        }

        public void RemoveViewport(Control viewport)
        {
            viewportHandlers.RemoveAll(viewportInfo => viewportInfo.Viewport == viewport);
        }


        public PaintEventHandler AddOnRender(PaintEventHandler handler)
        {
            viewportHandlers.ForEach(viewportHandler =>
                viewportHandler.Viewport.Paint += handler
            );
            return handler;
        }

        public void RemoveOnRender(PaintEventHandler handler)
        {
            viewportHandlers.ForEach(viewportHandler =>
                viewportHandler.Viewport.Paint -= handler
            );
        }


        public Vector2 ConvertCanvasPositionToGamePosition(Vector2 canvasPosition)
        {
            return new Vector2((canvasPosition.X - Position.X) * Scale, (canvasPosition.Y - Position.Y) * Scale);
        }

        public Vector2 ConvertGamePositionToCanvasPosition(Vector2 gamePosition)
        {
            return new Vector2((gamePosition.X + Position.X) / Scale, (gamePosition.Y + Position.Y) / Scale);
        }
    }

    class ViewportHandler
    {
        public float Scale = 1f;

        public Control Viewport { get; private set; }

        public ViewportHandler(Control viewport, float scale = 1f)
        {
            Viewport = viewport;
            Scale = scale;
        }
    }
}
