#define TRACE_NANS
//#define DISABLE_MEAN_POINT
//#define DISABLE_QEM_COMPUTING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Terremesh.Remeshing
{
    /// <summary>
    /// Extends matrix class by adding new methods.
    /// </summary>
    internal static class MatrixExtension
    {
        /// <summary>
        /// Multiplies 4D vector by 4x4 matrix.
        /// </summary>
        /// <param name="v">The source vector.</param>
        /// <param name="m">The source matrix.</param>
        /// <param name="result">The result value.</param>
        /// <remarks>The result value is a scalar.</remarks>
        public static void Multiply(ref Vector4 v, ref Matrix m, out float result)
        {
            result =
                (v.X * m.M11 + v.Y * m.M12 + v.Z * m.M13 + v.W * m.M14) +
                (v.X * m.M21 + v.Y * m.M22 + v.Z * m.M23 + v.W * m.M24) +
                (v.X * m.M31 + v.Y * m.M32 + v.Z * m.M33 + v.W * m.M34) +
                (v.X * m.M41 + v.Y * m.M42 + v.Z * m.M43 + v.W * m.M44);
        }

        /// <summary>
        /// Multiplies 4D vector by 4x4 matrix.
        /// </summary>
        /// <param name="v">The source vector.</param>
        /// <param name="m">The source matrix.</param>
        /// <returns>The result value.</returns>
        /// <remarks>The result value is a scalar.</remarks>
        public static float Multiply(ref Vector4 v, ref Matrix m)
        {
            float result;
            Multiply(ref v, ref m, out result);
            return result;
        }

        /// <summary>
        /// Multiplies 3D vector by 3x3 matrix.
        /// </summary>
        /// <param name="v">The source vector.</param>
        /// <param name="m">The source matrix.</param>
        /// <param name="result">The result value.</param>
        /// <remarks>The result value is a scalar.</remarks>
        public static void Multiply(ref Vector3 v, ref Matrix m, out float result)
        {
            result =
                (v.X * m.M11 + v.Y * m.M12 + v.Z * m.M13) +
                (v.X * m.M21 + v.Y * m.M22 + v.Z * m.M23) +
                (v.X * m.M31 + v.Y * m.M32 + v.Z * m.M33);
        }

        /// <summary>
        /// Multiplies 3D vector by 3x3 matrix.
        /// </summary>
        /// <param name="v">The source vector.</param>
        /// <param name="m">The source matrix.</param>
        /// <returns>The result value.</returns>
        /// <remarks>The result value is a scalar.</remarks>
        public static float Multiply(ref Vector3 v, ref Matrix m)
        {
            float result;
            Multiply(ref v, ref m, out result);
            return result;
        }

        /// <summary>
        /// Multiplies 4x4 matrix by 4D vector.
        /// </summary>
        /// <param name="m">The source matrix.</param>
        /// <param name="v">The source vector.</param>
        /// <param name="result">The result vector.</param>
        /// <remarks>The result value is an vector.</remarks>
        public static void Multiply(ref Matrix m, ref Vector4 v, out Vector4 result)
        {
            result = new Vector4(
                m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z + m.M14 * v.W,
                m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z + m.M24 * v.W,
                m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z + m.M34 * v.W,
                m.M41 * v.X + m.M42 * v.Y + m.M43 * v.Z + m.M44 * v.W);
        }

        /// <summary>
        /// Multiplies 4x4 matrix by 4D vector.
        /// </summary>
        /// <param name="m">The source matrix.</param>
        /// <param name="v">The source vector.</param>
        /// <returns>The result vector.</returns>
        /// <remarks>The result value is an vector.</remarks>
        public static Vector4 Multiply(ref Matrix m, ref Vector4 v)
        {
            Vector4 result;
            Multiply(ref m, ref v, out result);
            return result;
        }

        /// <summary>
        /// Multiplies 3x3 matrix by 3D vector.
        /// </summary>
        /// <param name="m">The source matrix.</param>
        /// <param name="v">The source vector.</param>
        /// <param name="result">The result vector.</param>
        /// <remarks>The result value is an vector.</remarks>
        public static void Multiply(ref Matrix m, ref Vector3 v, out Vector3 result)
        {
            result = new Vector3(
                m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z,
                m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z,
                m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z);
        }

        /// <summary>
        /// Multiplies 3x3 matrix by 3D vector.
        /// </summary>
        /// <param name="m">The source matrix.</param>
        /// <param name="v">The source vector.</param>
        /// <returns>The result vector.</returns>
        /// <remarks>The result value is an vector.</remarks>
        public static Vector3 Multiply(ref Matrix m, ref Vector3 v)
        {
            Vector3 result;
            Multiply(ref m, ref v, out result);
            return result;
        }

        /// <summary>
        /// Inverts specified matrix and returns computed determinant value.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <param name="result">The inverted matrix</param>
        /// <param name="determinant">The matrix determinant before inversion.</param>
        public static void Invert(ref Matrix matrix, out Matrix result, out float determinant)
        {
            float det1 = matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;
            float det2 = matrix.M11 * matrix.M23 - matrix.M13 * matrix.M21;
            float det3 = matrix.M11 * matrix.M24 - matrix.M14 * matrix.M21;
            float det4 = matrix.M12 * matrix.M23 - matrix.M13 * matrix.M22;
            float det5 = matrix.M12 * matrix.M24 - matrix.M14 * matrix.M22;
            float det6 = matrix.M13 * matrix.M24 - matrix.M14 * matrix.M23;
            float det7 = matrix.M31 * matrix.M42 - matrix.M32 * matrix.M41;
            float det8 = matrix.M31 * matrix.M43 - matrix.M33 * matrix.M41;
            float det9 = matrix.M31 * matrix.M44 - matrix.M34 * matrix.M41;
            float det10 = matrix.M32 * matrix.M43 - matrix.M33 * matrix.M42;
            float det11 = matrix.M32 * matrix.M44 - matrix.M34 * matrix.M42;
            float det12 = matrix.M33 * matrix.M44 - matrix.M34 * matrix.M43;

            float detMatrix = (float)(det1 * det12 - det2 * det11 + det3 * det10 + det4 * det9 - det5 * det8 + det6 * det7);
            determinant = detMatrix;

            float invDetMatrix = 1.0f / detMatrix;

            Matrix ret;

            ret.M11 = (matrix.M22 * det12 - matrix.M23 * det11 + matrix.M24 * det10) * invDetMatrix;
            ret.M12 = (-matrix.M12 * det12 + matrix.M13 * det11 - matrix.M14 * det10) * invDetMatrix;
            ret.M13 = (matrix.M42 * det6 - matrix.M43 * det5 + matrix.M44 * det4) * invDetMatrix;
            ret.M14 = (-matrix.M32 * det6 + matrix.M33 * det5 - matrix.M34 * det4) * invDetMatrix;
            ret.M21 = (-matrix.M21 * det12 + matrix.M23 * det9 - matrix.M24 * det8) * invDetMatrix;
            ret.M22 = (matrix.M11 * det12 - matrix.M13 * det9 + matrix.M14 * det8) * invDetMatrix;
            ret.M23 = (-matrix.M41 * det6 + matrix.M43 * det3 - matrix.M44 * det2) * invDetMatrix;
            ret.M24 = (matrix.M31 * det6 - matrix.M33 * det3 + matrix.M34 * det2) * invDetMatrix;
            ret.M31 = (matrix.M21 * det11 - matrix.M22 * det9 + matrix.M24 * det7) * invDetMatrix;
            ret.M32 = (-matrix.M11 * det11 + matrix.M12 * det9 - matrix.M14 * det7) * invDetMatrix;
            ret.M33 = (matrix.M41 * det5 - matrix.M42 * det3 + matrix.M44 * det1) * invDetMatrix;
            ret.M34 = (-matrix.M31 * det5 + matrix.M32 * det3 - matrix.M34 * det1) * invDetMatrix;
            ret.M41 = (-matrix.M21 * det10 + matrix.M22 * det8 - matrix.M23 * det7) * invDetMatrix;
            ret.M42 = (matrix.M11 * det10 - matrix.M12 * det8 + matrix.M13 * det7) * invDetMatrix;
            ret.M43 = (-matrix.M41 * det4 + matrix.M42 * det2 - matrix.M43 * det1) * invDetMatrix;
            ret.M44 = (matrix.M31 * det4 - matrix.M32 * det2 + matrix.M33 * det1) * invDetMatrix;

            result = ret;
        }

        /// <summary>
        /// Inverts specified matrix and returns computed determinant value.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <param name="result">The inverted matrix</param>
        /// <returns>The matrix determinant.</returns>
        public static float Invert(ref Matrix matrix, out Matrix result)
        {
            float det;
            Invert(ref matrix, out result, out det);
            return det;
        }

        public static float IndexedDeterminant(this Matrix matrix, int m11, int m12, int m13, int m21, int m22, int m23, int m31, int m32, int m33)
        {
            float[] m = new float[16];
            m[0] = matrix.M11;
            m[1] = matrix.M12;
            m[2] = matrix.M13;
            m[3] = matrix.M14;
            m[4] = matrix.M21;
            m[5] = matrix.M22;
            m[6] = matrix.M23;
            m[7] = matrix.M24;
            m[8] = matrix.M31;
            m[9] = matrix.M32;
            m[10] = matrix.M33;
            m[11] = matrix.M34;
            m[12] = matrix.M41;
            m[13] = matrix.M42;
            m[14] = matrix.M43;
            m[15] = matrix.M44;
            return m[m11] * m[m22] * m[m33] + m[m13] * m[m21] * m[m32] + m[m12] * m[m23] * m[m31]
                - m[m13] * m[m22] * m[m31] - m[m11] * m[m23] * m[m32] - m[m12] * m[m21] * m[m33];
        }
    }

    /// <summary>
    /// Implements mesh remeshing method using  Quadric Error Metric method.
    /// </summary>
    public class QuadricErrorMetricMethod : IRemeshingMethod
    {
        #region Error Metric computation
        /// <summary>
        /// Implements error metric for each vertex, triangle and edge.
        /// </summary>
        public struct ErrorMetric
        {
            #region Constructors
            /// <summary>
            /// Creates instnace of the ErrorMetric class.
            /// </summary>
            /// <param name="triangle">The triangle.</param>
            public ErrorMetric(Triangle triangle)
            {
                Plane p = new Plane(
                    triangle.Vertex1.Position,
                    triangle.Vertex2.Position,
                    triangle.Vertex3.Position);


                float a2 = p.Normal.X * p.Normal.X;
                float b2 = p.Normal.Y * p.Normal.Y;
                float c2 = p.Normal.Z * p.Normal.Z;
                float d2 = p.D * p.D;
                float ab = p.Normal.X * p.Normal.Y;
                float ac = p.Normal.X * p.Normal.Z;
                float ad = p.Normal.X * p.D;
                float bc = p.Normal.Y * p.Normal.Z;
                float bd = p.Normal.Y * p.D;
                float cd = p.Normal.Z * p.D;

                m_Matrix = new Matrix(
                    a2, ab, ac, ad,
                    ab, b2, bc, bd,
                    ac, bc, c2, cd,
                    ad, bd, cd, d2
                    );
                m_Area = triangle.Area;
                m_ProposedPoint = new Vector3(100.0f, 100.0f, 100.0f);
            }
            #endregion

            #region Methods
#if false
            /// <summary>
            /// Computes new proposed position.
            /// </summary>
            /// <returns>The proposed position.</returns>
            public Vector3 ComputePosition()
            {
                return ComputeNewPosition(ref this);
            }


            /// <summary>
            /// Computes new proposed position using specified error metric.
            /// </summary>
            /// <param name="metric">The error metric.</param>
            /// <returns>The proposed position.</returns>
            public static Vector3 ComputeNewPosition(ref ErrorMetric metric)
            {
                // Replace bottom row:
                Matrix m = metric.m_Matrix;
                m.M41 = m.M42 = m.M43 = 0.0f;
                m.M44 = 1.0f;

                // Invert matrix.
                //Matrix.Transpose(ref m, out m);
                Matrix.Invert(ref m, out m);
                

                // Vector x Matrix
                Vector4 newPosition;
                MatrixExtension.Multiply(ref m, ref m_Wzero, out newPosition);

                return new Vector3(
                    newPosition.X,
                    newPosition.Y,
                    newPosition.Z);
            }
            private static Vector4 m_Wzero = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);

            public void PointConstraint(Vector3 point)
            {
                m_Matrix.M11 = 1.0f; m_Matrix.M12 = 0.0f; m_Matrix.M13 = 0.0f; m_Matrix.M14 = -point.X;
                m_Matrix.M21 = 0.0f; m_Matrix.M22 = 1.0f; m_Matrix.M23 = 0.0f; m_Matrix.M24 = -point.Y;
                m_Matrix.M31 = 0.0f; m_Matrix.M32 = 0.0f; m_Matrix.M33 = 1.0f; m_Matrix.M34 = -point.Z;

                m_Matrix.M41 = point.X;
                m_Matrix.M42 = point.Y;
                m_Matrix.M43 = point.Z;
                m_Matrix.M44 = point.X * point.X + point.Y * point.Y + point.Z * point.Z;
            }
#endif
            /// <summary>
            /// Tries to compute optimized contraction point.
            /// </summary>
            /// <param name="v">The proposed optimized contraction point.</param>
            /// <returns>true when successful, false otherwise.</returns>
            public bool Optimize(out Vector3 v)
            {
                Matrix Ainv;
                Matrix tensor = Tensor;
                float determinant;

                MatrixExtension.Invert(ref tensor, out Ainv, out determinant);

                if (Math.Abs(determinant) < 1e-5)
                {
                    v = Vector3.Zero;
                    return false;
                }

                Vector3 tmp = Vector;
                Vector3 result = MatrixExtension.Multiply(ref Ainv, ref tmp);
                v = -result;

                return true;
            }

            /// <summary>
            /// Tries to compute optimized contraction point using specified edge points.
            /// </summary>
            /// <param name="v1">The source edge point.</param>
            /// <param name="v2">The target edge point.</param>
            /// <param name="result">The proposed contraction point.</param>
            /// <returns>true when successful, false otherwise.</returns>
            public bool Optimize(ref Vector3 v1, ref Vector3 v2, out Vector3 result)
            {
                Vector3 difference = v1 - v2;
                Matrix A = Tensor;
                Vector3 vector = Vector;

                Vector3 Av2 = MatrixExtension.Multiply(ref A, ref v2);
                Vector3 Ad = MatrixExtension.Multiply(ref A, ref difference);

                double den = 2.0 * MatrixExtension.Multiply(ref difference, ref A);

                if (Math.Abs(den) < 1e-6)
                {
                    result = Vector3.Zero;
                    return false;
                }

                double a = (-2.0 * Vector3.Dot(vector, difference) - Vector3.Dot(v2, Ad)) / (2.0 * Vector3.Dot(difference, Ad));

                if (a < 0.0)
                {
                    a = 0.0;
                }
                else if (a > 1.0)
                {
                    a = 1.0;
                }

                result = Vector3.Multiply(difference, (float)a) + v2;
                return true;
            }

            /// <summary>
            /// Transforms error metric by matrix.
            /// </summary>
            /// <param name="matrix">The transform matrix.</param>
            public void Transform(Matrix matrix)
            {
                Matrix m = m_Matrix;
                Matrix pa = Matrix.Transpose(matrix);
                m = pa * m * pa;
                m_Matrix = m;
            }

            /// <summary>
            /// Transforms error metric by other error metric.
            /// </summary>
            /// <param name="m">The other error metric.</param>
            public void Transform(ref ErrorMetric m)
            {
                Matrix Q = m_Matrix;
                Matrix Pa = m.m_Matrix;

                Q = Pa * Q * Pa;

                m_Matrix = Q;
            }

            /// <summary>
            /// Adds two error metrics.
            /// </summary>
            /// <param name="value1">The source error metric.</param>
            /// <param name="value2">The source error metric.</param>
            /// <param name="result">The result error metric.</param>
            public static void Add(ref ErrorMetric value1, ref ErrorMetric value2, out ErrorMetric result)
            {
                result = new ErrorMetric();
                result.m_Area = value1.m_Area + value2.m_Area;
                result.m_Matrix = value1.m_Matrix + value2.m_Matrix;
            }

            /// <summary>
            /// Adds two error metrics.
            /// </summary>
            /// <param name="value1">The source error metric.</param>
            /// <param name="value2">The source error metric.</param>
            /// <returns>The result error metric.</returns>
            public static ErrorMetric Add(ref ErrorMetric value1, ref ErrorMetric value2)
            {
                ErrorMetric result;
                Add(ref value1, ref value2, out result);
                return result;
            }

            /// <summary>
            /// Subtracts two error metrics.
            /// </summary>
            /// <param name="value1">The source error metric.</param>
            /// <param name="value2">The source error metric.</param>
            /// <param name="result">The result error metric.</param>
            public static void Subtract(ref ErrorMetric value1, ref ErrorMetric value2, out ErrorMetric result)
            {
                result = new ErrorMetric();
                result.m_Area = value1.m_Area - value2.m_Area;
                result.m_Matrix = value1.m_Matrix - value2.m_Matrix;
            }

            /// <summary>
            /// Subtracts two error metrics.
            /// </summary>
            /// <param name="value1">The source error metric.</param>
            /// <param name="value2">The source error metric.</param>
            /// <returns>The result error metric.</returns>
            public static ErrorMetric Subtract(ref ErrorMetric value1, ref ErrorMetric value2)
            {
                ErrorMetric result;
                Subtract(ref value1, ref value2, out result);
                return result;
            }

            /// <summary>
            /// Evaluates point error value.
            /// </summary>
            /// <param name="point">The source point to evaluate.</param>
            /// <returns>The point error value.</returns>
            public float Evaluate(Vector3 point)
            {
                float value = m_Matrix.M11 * point.X * point.X +
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
            #endregion

            #region Properties
            /// <summary>
            /// Gets tensor matrix.
            /// </summary>
            public Matrix Tensor
            {
                get
                {
                    Matrix tensor = m_Matrix;
                    tensor.M14 = 0.0f;
                    tensor.M24 = 0.0f;
                    tensor.M34 = 0.0f;
                    tensor.M44 = 1.0f;
                    tensor.M43 = 0.0f;
                    tensor.M42 = 0.0f;
                    tensor.M41 = 0.0f;

                    return tensor;
                }
            }

            /// <summary>
            /// Gets or sets matrix.
            /// </summary>
            public Matrix Matrix
            {
                get { return m_Matrix; }
                set { m_Matrix = value; }
            }

            /// <summary>
            /// Gets vector from matrix.
            /// </summary>
            public Vector3 Vector
            {
                get { return new Vector3(m_Matrix.M14, m_Matrix.M24, m_Matrix.M34); }
            }

            /// <summary>
            /// Gets offset value.
            /// </summary>
            public float Offset
            {
                get { return m_Matrix.M44; }
            }

            /// <summary>
            /// Gets area value.
            /// </summary>
            public float Area
            {
                get { return m_Area; }
            }

            /// <summary>
            /// Gets empty error metric.
            /// </summary>
            public static ErrorMetric Empty
            {
                get { return m_Empty; }
            }

#if false
            public Vector3 NewPosition
            {
                get { return ComputeNewPosition(ref this); }
            }
#endif
            /// <summary>
            /// Gets proposed point.
            /// </summary>
            public Vector3 ProposedPoint
            {
                get { return m_ProposedPoint; }
                set { m_ProposedPoint = value; }
            }
#endregion

            #region Fields
            private static readonly ErrorMetric m_Empty = new ErrorMetric() { 
                m_Matrix = Matrix.Identity,
                m_Area = 0.0f
            };

            private Vector3 m_ProposedPoint;
            private Matrix m_Matrix;
            private float m_Area;
            #endregion
        }
        #endregion

        #region Enums
        /// <summary>
        /// Provides enumeration that represents weight computation policy.
        /// </summary>
        [Flags()]
        public enum WeightPolicy
        {
            /// <summary>
            /// Use average triangle area.
            /// </summary>
            AverageTriangleArea,

            /// <summary>
            /// Performs optimization.
            /// </summary>
            Optmized,

            /// <summary>
            /// Evaluate middle point on edge.
            /// </summary>
            MiddlePoint,
        }
        #endregion

        #region Methods
        public void Remesh(ref Mesh mesh, IProgressListener progress = null)
        {
            var triangles = mesh.Triangles;
            var vertices = mesh.Vertices;

            // Compute K matrices for initial triangles.
            foreach (var t in triangles)
            {
                t.UpdateGeometricData();
                t.Tag = new ErrorMetric(t);
            }

            // Compute Q for intiial vertices.
            foreach (var v in vertices)
            {
                ComputeErrorMetricForVertex(v);
            }

            // Compute initial edge QEM-s
            foreach (var t in triangles)
            {
                ComputeErrorMetricForEdges(t);
            }

            foreach (var t in triangles)
            {
                ComputeEdgeCost(t);
            }

            // Compute number of triangles after we stop
            int toRemove = (int)((m_Removed) * triangles.Count);
            int triangleLimit = triangles.Count - toRemove;

#if TRACE_NANS
            int nansCount = 0;
#endif
            int index = 0;
            while (triangleLimit < triangles.Count)
            {
                Vertex v1, v2;
                Triangle.Edge edge;
                if (SearchBestCandidate(ref mesh, out v1, out v2, out edge))
                {
                    if (edge != null)
                    {

                        ErrorMetric em = (ErrorMetric)edge.Tag;
                        Vector3 v = em.ProposedPoint;
#if false
                        if (v.IsNaN())
                        {
#if TRACE_NANS
                            ++nansCount;
#endif
                            v = Vector3Extension.Mean(v1.Position, v2.Position);
                        }
#endif
                        if (mesh.JoinVertices(v1, v2, v))
                        {
                            // V1, since v2 is removed from now.
                            UpdateVertexNeighbors(v1);
                        }

                        progress.OnProgress(index, toRemove);
                        index += 2;
                    }
                    else
                    {
                        Trace.WriteLine("If you see this message more than once per second, I can't find any matching edge");
                    }
                }

            }

#if TRACE_NANS
            Trace.WriteLine(string.Format("NaNs count: {0}", nansCount));
#endif

        }

        /// <summary>
        /// Optimizes edge target.
        /// </summary>
        /// <param name="v1">The source edge vertex.</param>
        /// <param name="v2">The target edge vertex.</param>
        /// <param name="edge">The edge additional params.</param>
        /// <param name="policy">The used policy.</param>
        private void OptimizeEdgeTarget(ref Vertex v1, ref Vertex v2, ref Triangle.Edge edge, WeightPolicy policy)
        {
            ErrorMetric Q = (ErrorMetric)edge.Tag;
         
            double min_cost = 0.0;
            Vector3 best;

            if (policy.HasFlag(WeightPolicy.Optmized) && Q.Optimize(out best))
            {
                best = Q.ProposedPoint;
                min_cost = Q.Evaluate(best);
            }
            else
            {
                double min1 = Q.Evaluate(v1.Position);
                double min2 = Q.Evaluate(v2.Position);

                if (min1 < min2)
                {
                    min_cost = min1;
                    best = v1.Position;
                }
                else
                {
                    min_cost = min2;
                    best = v2.Position;
                }

                if (policy.HasFlag(WeightPolicy.MiddlePoint))
                {
                    Vector3 middle = Vector3Extension.Mean(v1.Position, v2.Position);
                    double cost_middle = Q.Evaluate(middle);

                    if (cost_middle < min_cost)
                    {
                        min_cost = cost_middle;
                        best = middle;
                    }
                }
            }

            if (policy.HasFlag(WeightPolicy.AverageTriangleArea))
            {
                min_cost /= Q.Area;
            }

            edge.Cost = 1.0/min_cost;
            Q.ProposedPoint = best;
            edge.Tag = (object)Q;
        }

        /// <summary>
        /// Updates vertex neighborhood.
        /// </summary>
        /// <param name="vertex"></param>
        private void UpdateVertexNeighbors(Vertex vertex)
        {
            // Update vertex cost, according to near triangles.
            ComputeErrorMetricForVertex(vertex);

            // Update neighboor vertices
            foreach (var n in vertex.Neighbors)
            {
                ComputeErrorMetricForVertex(n);
            }

            // Compute partial costs for all edges in triangle
            foreach (var t in vertex.Triangles)
            {
                t.UpdateGeometricData();
                ComputeErrorMetricForEdges(t);
            }
#if true
            foreach (var n in vertex.Neighbors)
            {
                foreach (var t in n.Triangles)
                {
                    ComputeEdgeCost(t);
                }
            }
            // Compute final cost for triangle edges.
            foreach (var t in vertex.Triangles)
            {
                ComputeEdgeCost(t);
            }
#endif
        }

        /// <summary>
        /// Computes error metric for specified vertex.
        /// </summary>
        /// <param name="vertex">The source vertex.</param>
        private void ComputeErrorMetricForVertex(Vertex vertex)
        {
            ErrorMetric Q = ErrorMetric.Empty;

            foreach (var t in vertex.Triangles)
            {
                ErrorMetric K = (ErrorMetric)t.Tag;

                ErrorMetric.Add(ref Q, ref K, out Q);
            }

            vertex.Tag = (object)Q;
        }

        /// <summary>
        /// Compute error metric for edges in triangle.
        /// </summary>
        /// <param name="t">The triangle.</param>
        private void ComputeErrorMetricForEdges(Triangle t)
        {
            ErrorMetric V1 = (ErrorMetric)t.Vertex1.Tag;
            ErrorMetric V2 = (ErrorMetric)t.Vertex2.Tag;
            ErrorMetric V3 = (ErrorMetric)t.Vertex3.Tag;

            ErrorMetric E12 = ErrorMetric.Empty;
            ErrorMetric E23 = ErrorMetric.Empty;
            ErrorMetric E31 = ErrorMetric.Empty;

            ErrorMetric.Add(ref V1, ref V2, out E12);
            ErrorMetric.Add(ref V2, ref V3, out E23);
            ErrorMetric.Add(ref V3, ref V1, out E31);

            t.Edge12.Tag = E12;
            t.Edge23.Tag = E23;
            t.Edge31.Tag = E31;
        }

        /// <summary>
        /// Computes edge cost for triangle.
        /// </summary>
        /// <param name="t">The triangle.</param>
        private void ComputeEdgeCost(Triangle t)
        {
#if false
            Vector3 P1 = E12.ComputePosition();
            Vector3 P2 = E23.ComputePosition();
            Vector3 P3 = E31.ComputePosition();

#if !DISABLE_QEM_COMPUTING
            double C12 = E12.Evaluate(P1);
            double C23 = E23.Evaluate(P2);
            double C31 = E31.Evaluate(P3);

            if (double.IsNaN(C12))
            {
#if DISABLE_MEAN_POINT
                E12.ProposedPoint = t.Vertex1.Position;
#else
                E12.ProposedPoint = Vector3Extension.Mean(
                    t.Vertex1.Position,
                    t.Vertex2.Position);
#endif
                C12 = E12.Evaluate(E12.ProposedPoint);
            }
            else
            {
                E12.ProposedPoint = P1;
            }

            if (double.IsNaN(C23))
            {
#if DISABLE_MEAN_POINT
                E23.ProposedPoint = t.Vertex2.Position;
#else
                E23.ProposedPoint = Vector3Extension.Mean(
                    t.Vertex2.Position,
                    t.Vertex3.Position);
                C23 = E23.Evaluate(E23.ProposedPoint);
#endif
            }
            else
            {
                E23.ProposedPoint = P2;
            }

            if (double.IsNaN(C31))
            {
#if DISABLE_MEAN_POINT
                E31.ProposedPoint = t.Vertex3.Position;
#else
                E31.ProposedPoint = Vector3Extension.Mean(
                    t.Vertex3.Position,
                    t.Vertex1.Position);
                C31 = E31.Evaluate(E31.ProposedPoint);
#endif
            }
            else
            {
                E31.ProposedPoint = P3;
            }
#else
            double C12 = E12.Evaluate(t.Vertex1.Position);
            double C23 = E23.Evaluate(t.Vertex2.Position);
            double C31 = E31.Evaluate(t.Vertex3.Position);
#endif
            
            t.Edge12.Cost = C12;
            t.Edge23.Cost = C23;
            t.Edge31.Cost = C31;
#else
            WeightPolicy policy = (WeightPolicy.Optmized | WeightPolicy.AverageTriangleArea);
            OptimizeEdgeTarget(ref t.m_Vertex1, ref t.m_Vertex2, ref t.m_Edge12, policy);
            OptimizeEdgeTarget(ref t.m_Vertex2, ref t.m_Vertex3, ref t.m_Edge23, policy);
            OptimizeEdgeTarget(ref t.m_Vertex3, ref t.m_Vertex1, ref t.m_Edge31, policy);
#endif
            t.Cost = Math.Min(t.Edge12.Cost, Math.Min(t.Edge23.Cost, t.Edge31.Cost));
        }

        /// <summary>
        /// Tries to search best candidate for remeshing.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="v1">The source vertex.</param>
        /// <param name="v2">The source vertex.</param>
        /// <param name="edge">The edge additional informations.</param>
        /// <returns>true when successful, false otherwise.</returns>
        private bool SearchBestCandidate(ref Mesh mesh, out Vertex v1, out Vertex v2, out Triangle.Edge edge)
        {
            var min = mesh.Triangles.Min();

            if (min != null)
            {
                if (min.Edge12.Cost < min.Edge23.Cost)
                {
                    if (min.Edge12.Cost < min.Edge31.Cost)
                    {
                        v1 = min.Vertex1;
                        v2 = min.Vertex2;
                        edge = min.Edge12;
                    }
                    else
                    {
                        v1 = min.Vertex3;
                        v2 = min.Vertex1;
                        edge = min.Edge31;
                    }
                }
                else
                {
                    if (min.Edge23.Cost < min.Edge31.Cost)
                    {
                        v1 = min.Vertex2;
                        v2 = min.Vertex3;
                        edge = min.Edge23;
                    }
                    else
                    {
                        v1 = min.Vertex3;
                        v2 = min.Vertex1;
                        edge = min.Edge31;
                    }
                }

                return true;
            }
            v1 = null;
            v2 = null;
            edge = null;
            return false;
        }

        public void Reset()
        {
        }

        public override string ToString()
        {
            return "Quadric Error Metric Method";
        }
        #endregion

        #region Properties
        public int Removed
        {
            get { return m_Removed; }
            set { m_Removed = value; }
        }
        #endregion

        #region Fields
        private int m_Removed = 0;
        #endregion
    }
}
