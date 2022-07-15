using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SmartRace.Utils;
using SmartRace.Maths;

namespace SmartRace.Core
{
    class Track
    {
        public float Width { get; set; }

        private List<TrackPiece> TrackPieces { get; set; } = new List<TrackPiece>();

        public Track(float width)
        {
            Width = width;
        }

        public void AddPiece(TrackPiece trackPiece)
        {
            TrackPieces.Add(trackPiece);
        }

        public void TryAddPoint(Vector2 point)
        {
            TrackPiece.TrackPieceType type = TrackPiece.TrackPieceType.Bezier;

            if (TrackPieces.Count == 0)
                TrackPieces.Add(new TrackPiece(type));

            TrackPiece currentPiece = TrackPieces.Last();

            if (currentPiece.IsComplete)
                TrackPieces.Add(new TrackPiece(type));

            currentPiece = TrackPieces.Last();

            Vector2 actualPoint = TrackPieces.Count > 1 ? GetActualPointToBeAdded(currentPiece, point) : point;

            currentPiece.AddPoint(actualPoint);

            if (currentPiece.IsComplete)
            {
                TrackPiece nextPiece = new TrackPiece(type);
                nextPiece.AddPoint(actualPoint);
                TrackPieces.Add(nextPiece);
            }
        }

        public Vector2 GetActualPointToBeAdded(TrackPiece currentTrackPiece, Vector2 point)
        {
            TrackPiece previousTrackPiece = TrackPieces[TrackPieces.IndexOf(currentTrackPiece) - 1];

            switch (currentTrackPiece.GetTrackPieceType())
            {
                case TrackPiece.TrackPieceType.Bezier:
                    if (currentTrackPiece.ReferencePoints.Count != 1)
                        return point;

                    return Geometry.ClosestPointOnStraight(point, previousTrackPiece.ReferencePoints[3], previousTrackPiece.ReferencePoints[2]); ;
                default:
                    return point;
            }
        }

        public void Draw(Graphics g, Vector2 offset, float scale, bool showGuides = false)
        {
            foreach (TrackPiece trackPiece in TrackPieces)
                trackPiece.Draw(g, offset, Width, scale, showGuides);
        }

        public void DrawPreviewConstrainedPoint(Graphics g, Camera camera, Vector2 mousePosition)
        {
            if (TrackPieces.Count < 2)
                return;

            TrackPiece currentTrackPiece = TrackPieces.Last();

            TrackPiece previousTrackPiece = TrackPieces[TrackPieces.IndexOf(currentTrackPiece) - 1];

            using (Brush brush = new SolidBrush(Color.Red))
                switch (currentTrackPiece.GetTrackPieceType())
                {

                    case TrackPiece.TrackPieceType.Bezier:
                        if (currentTrackPiece.ReferencePoints.Count == 1)
                        {
                            Vector2 closestPointInStraight = Geometry.ClosestPointOnStraight(mousePosition, previousTrackPiece.ReferencePoints[3], previousTrackPiece.ReferencePoints[2]);
                            Vector2 projectedPointOnCanvas = camera.ConvertGamePositionToCanvasPosition(closestPointInStraight);
                            g.FillCircle(brush, projectedPointOnCanvas.X, projectedPointOnCanvas.Y, 5 * camera.Scale);
                        }
                        break;
                }
        }
    }
}


//public class Track
//{
//    // Build mode for the track editor
//    enum BuildMode
//    {
//        FreeDraw,
//        Spline,
//        Predefined
//    }

//    // Width of the track
//    double width;

//    // All the pieces of the track
//    public List<TrackPiece> TrackPieces = new List<TrackPiece>();

//    // Drawing points reference
//    public PointF TopLeft;

//    public Track(double width)
//    {
//        this.width = width;
//    }


//    public void Add(TrackPiece piece)
//    {
//        TrackPieces.Add(piece);
//    }

//    public void Draw(PaintEventArgs e, PointF offset)
//    {
//        foreach (TrackPiece piece in TrackPieces)
//            piece.Draw(e, offset, width);
//    }

//    public void Draw(Graphics g, PointF offset)
//    {
//        foreach (TrackPiece piece in TrackPieces)
//            piece.Draw(g, offset, width);
//    }


//    public PointF Size()
//    {
//        float maxX, maxY, minX, minY;
//        maxX = 0; maxY = 0; minX = 0; minY = 0;

//        foreach (TrackPiece piece in TrackPieces)
//        {
//            switch (piece.type)
//            {
//                case TrackPiece.PieceType.Bezier:
//                    foreach (PointF Point in piece.points)
//                    {
//                        if (Point.X > maxX)
//                            maxX = Point.X;
//                        if (Point.X < minX)
//                            minX = Point.X;
//                        if (Point.Y > maxY)
//                            maxY = Point.Y;
//                        if (Point.Y < minY)
//                            minY = Point.Y;
//                    }
//                    break;
//            }
//        }

//        return new PointF(maxX - minX + 4 * (float)width, maxY - minY + 4 * (float)width);
//    }

//    public PointF GetTopLeft()
//    {
//        float minX, minY;
//        minX = 0; minY = 0;

//        foreach (TrackPiece piece in TrackPieces)
//        {
//            switch (piece.type)
//            {
//                case TrackPiece.PieceType.Bezier:
//                    foreach (PointF Point in piece.points)
//                    {
//                        if (Point.X < minX)
//                            minX = Point.X;
//                        if (Point.Y < minY)
//                            minY = Point.Y;
//                    }
//                    break;
//            }
//        }

//        TopLeft = new PointF(minX - 2 * (float)width, minY - 2 * (float)width);
//        return TopLeft;
//    }
//}

