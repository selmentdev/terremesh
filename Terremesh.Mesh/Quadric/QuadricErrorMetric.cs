using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Terremesh.Mesh.Quadric
{
    internal struct ErrorMetric
    {
        public ErrorMetric(Plane plane)
        {
            float aa = plane.Normal.X * plane.Normal.X;
            float bb = plane.Normal.Y * plane.Normal.Y;
            float cc = plane.Normal.Z * plane.Normal.Z;
            float dd = plane.D * plane.D;
            float ab = plane.Normal.X * plane.Normal.Y;
            float ac = plane.Normal.X * plane.Normal.Z;
            float ad = plane.Normal.X * plane.D;
            float bc = plane.Normal.Y * plane.Normal.Z;
            float bd = plane.Normal.Y * plane.D;
            float cd = plane.Normal.Z * plane.D;

            m_Matrix = new Matrix(
                aa, ab, ac, ad,
                ab, bb, bc, bd,
                ac, bc, cc, cd,
                ad, bd, cd, dd);
        }

        public double Evaluate(Vector3 point)
        {
            double value = m_Matrix.M11 * point.X * point.X +
                   2 * m_Matrix.M12 * point.X * point.Y +
                   2 * m_Matrix.M13 * point.X * point.Z +
                   2 * m_Matrix.M14 * point.X +
                   m_Matrix.M22 * point.Y * point.Y +
                   2 * m_Matrix.M23 * point.Y * point.Z +
                   2 * m_Matrix.M24 * point.Y +
                   m_Matrix.M33 * point.Z * point.Z +
                   2 * m_Matrix.M34 * point.Z
                   + m_Matrix.M44;
            return value;
        }

        public static ErrorMetric Empty
        {
            get { return m_Empty; }
        }

        private static readonly ErrorMetric m_Empty = new ErrorMetric() { m_Matrix = Matrix.Identity };

        public Matrix Matrix
        {
            get { return m_Matrix; }
            set { m_Matrix = value; }
        }

        public static ErrorMetric operator + (ErrorMetric e1, ErrorMetric e2)
        {
            ErrorMetric result = new ErrorMetric();

            result.m_Matrix = e1.m_Matrix + e2.m_Matrix;

            return result;
        }

        public static ErrorMetric operator -(ErrorMetric e1, ErrorMetric e2)
        {
            ErrorMetric result = new ErrorMetric();
            result.m_Matrix = e1.m_Matrix - e2.m_Matrix;
            return result;
        }

        private Matrix m_Matrix;
    }
}
