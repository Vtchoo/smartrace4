using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartRace.Maths
{
    static class Geometry
    {
        /// <summary>
        /// Returns the angle between two vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Angle(Vector2 a, Vector2 b)
        {
            return Math.Acos(Vector2.Dot(a, b) / (a.Length() * b.Length()));
        }

        /// <summary>
        /// Returns the angle between two vectors
        /// </summary>
        /// <param name="startA"></param>
        /// <param name="endA"></param>
        /// <param name="startB"></param>
        /// <param name="endB"></param>
        /// <returns></returns>
        public static double Angle(Vector2 startA, Vector2 endA, Vector2 startB, Vector2 endB)
        {
            return Math.Acos(Vector2.Dot(Vector2.Subtract(endA, startA), Vector2.Subtract(endB, startB)) / (Vector2.Distance(startA, endA) * Vector2.Distance(startB, endB)));
        }


        /// <summary>
        /// Returns the distance of a point from a straight line
        /// </summary>
        /// <param name="point"></param>
        /// <param name="StrPoint1"></param>
        /// <param name="StrPoint2"></param>
        /// <returns></returns>
        public static double DistPointStraight(Vector2 point, Vector2 StrPoint1, Vector2 StrPoint2)
        {
            return Vector2.Distance(point, StrPoint1) * Math.Cos(Math.PI / 2 - Angle(StrPoint1, point, StrPoint1, StrPoint2));
        }

        public static Vector2 ClosestPointOnStraight(Vector2 point, Vector2 StrPoint1, Vector2 StrPoint2)
        {
            return Vector2.Add(StrPoint1, Vector2.Multiply(Vector2.Normalize(Vector2.Subtract(StrPoint2, StrPoint1)), -Vector2.Dot(Vector2.Subtract(StrPoint1, point), Vector2.Subtract(StrPoint2, StrPoint1)) / Vector2.Distance(StrPoint1, StrPoint2)));
        }


        public static double Atan2(Vector2 a, Vector2 b)
        {
            return Math.Atan2(a.Y - b.Y, a.X - b.X);
        }
    }
}
