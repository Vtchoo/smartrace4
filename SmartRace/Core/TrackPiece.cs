using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartRace.Utils;

namespace SmartRace.Core
{
    class TrackPiece
    {
        public enum TrackPieceType
        {
            Bezier,
            Arc,
            Straight
        }

        public static readonly Dictionary<TrackPieceType, int> TotalReferencePointsPerType = new Dictionary<TrackPieceType, int>()
        {
            { TrackPieceType.Arc, 3 },
            { TrackPieceType.Bezier, 4 },
            { TrackPieceType.Straight, 2 }
        };

        private TrackPieceType Type { get; set; }

        public List<Vector2> ReferencePoints { get; set; } = new List<Vector2>();

        public bool IsComplete { get => ReferencePoints.Count == TotalReferencePointsPerType[Type]; }

        public TrackPiece(TrackPieceType type)
        {
            Type = type;
        }

        public TrackPiece(TrackPieceType type, List<Vector2> points)
        {
            Type = type;
            ReferencePoints.AddRange(points);
        }

        public void MovePointTo(int index, float newX, float newY)
        {
            Vector2 point = ReferencePoints[index];
            point.X = newX;
            point.Y = newY;
            ReferencePoints[index] = point;
        }

        public void MovePoint(int index, float deltaX, float deltaY)
        {
            ReferencePoints[index] = Vector2.Add(ReferencePoints[index], new Vector2(deltaX, deltaY));
        }

        public void AddPoint(Vector2 newPoint)
        {
            ReferencePoints.Add(newPoint);
        }

        public TrackPieceType GetTrackPieceType()
        {
            return Type;
        }

        public void Draw(Graphics g, Vector2 offset, float width, float scale, bool showGuides = false)
        {
            PointF[] points = ReferencePoints
                .Select(v => new PointF(v.X + offset.X, v.Y + offset.Y))
                .ToArray();

            using (Pen pen = new Pen(Color.Black, width))
            {
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                switch (Type)
                {
                    case TrackPieceType.Arc:

                        break;
                    case TrackPieceType.Bezier:
                        if (points.Length >= 4)
                            g.DrawBeziers(pen, points);
                        break;
                    case TrackPieceType.Straight:
                        if (points.Length >= 2)
                            g.DrawLine(pen, points[0], points[1]);
                        break;
                    default:

                        break;
                }
            }

            if (showGuides)
                foreach (PointF referencePoint in points)
                    using (Brush brush = new SolidBrush(Color.Red))
                        g.FillCircle(brush, referencePoint.X, referencePoint.Y, 5);
        }
    }
}
