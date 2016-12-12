using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Terremesh.Mesh.XnaFramework
{
    public static class Vector3Extension
    {
        public static bool IsNaN(this Vector3 vector)
        {
            return float.IsNaN(vector.X) || float.IsNaN(vector.Y) || float.IsNaN(vector.Z);
        }

        public static Vector3 Mean(Vector3 vector1, Vector3 vector2)
        {
            Vector3 result = vector1 + vector2;
            Vector3.Multiply(ref result, 0.5f, out result);
            return result;
        }
    }

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
}
