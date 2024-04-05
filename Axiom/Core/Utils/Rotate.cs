using System;
using System.Collections.Generic;
using System.Drawing;

namespace Axiom.Core.Utils
{
    public class Rotate
    {
        public static List<PointF> Points(List<PointF> points, PointF center, double angle)
        {
            // Convert to radians
            angle = angle * (Math.PI / 180);

            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            for (int i = 0; i < points.Count; i++)
            {
                float x = points[i].X;
                float y = points[i].Y;

                points[i] = new PointF
                {
                    X = cos * (x - center.X) - sin * (y - center.Y) + center.X,
                    Y = sin * (x - center.X) + cos * (y - center.Y) + center.Y
                };
            }

            return points;
        }

        public static List<PointF> Points(List<PointF> points, double angle)
        {
            // Convert to radians
            angle = angle * (Math.PI / 180);

            var rMatrix = GetRotationMatrix(points[0], angle);
            var rotatedPoints = new List<PointF>();
            var pMatrix = new double[3];

            foreach (var p in points)
            {
                // Assemble the points matrix
                pMatrix[0] = p.X;
                pMatrix[1] = p.Y;
                pMatrix[2] = 1;

                // Multiply the point matrix by the rotation matrix
                var product = VectorProduct(rMatrix, pMatrix);
                float x = (float)product[0];
                float y = (float)product[1];

                // Add the rotated point to the list
                rotatedPoints.Add(new PointF(x, y));
            }

            return rotatedPoints;
        }

        public static PointF Point(PointF point, double angle)
        {
            // Convert to radians
            angle = angle * (Math.PI / 180);

            var rMatrix = GetRotationMatrix(point, angle);
            var pMatrix = new double[3];

            // Assemble the points matrix
            pMatrix[0] = point.X;
            pMatrix[1] = point.Y;
            pMatrix[2] = 1;

            // Multiply the point matrix by the rotation matrix
            var product = VectorProduct(rMatrix, pMatrix);
            float x = (float)product[0];
            float y = (float)product[1];

            return new PointF(x, y);
        }

        // Matrix for rotation

        private static double[][] GetRotationMatrix(PointF origin, double angle)
        {
            var c = Math.Cos(angle);
            var s = Math.Sin(angle);

            // Form the rotation matrix
            var matrix = new double[3][];
            matrix[0] = new double[3] { c, -s, -(origin.X * c) + (origin.Y * s) + origin.X };
            matrix[1] = new double[3] { s, c, -(origin.X * s) - (origin.Y * c) + origin.Y };
            matrix[2] = new double[3] { 0, 0, 1 };

            return matrix;
        }

        public static double[] VectorProduct(double[][] matrix, double[] vector)
        {
            // Result of multiplying an n x m matrix by a m x 1
            // Column vector (yielding an n x 1 column vector)
            int mCols = matrix[0].Length;
            int mRows = matrix.Length;
            int vRows = vector.Length;

            if (mCols != vRows)
            {
                throw new Exception("Non-conformable matrix and vector");
            }

            double[] result = new double[mRows];

            for (int i = 0; i < mRows; ++i)
            {
                for (int j = 0; j < mCols; ++j)
                {
                    result[i] += matrix[i][j] * vector[j];
                }
            }

            return result;
        }


    }
}
