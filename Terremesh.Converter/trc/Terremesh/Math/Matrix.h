#pragma once
#ifndef _Terremesh_Math_Matrix_H__
#define _Terremesh_Math_Matrix_H__

#include "../Required.h"
#include "Vec3.h"

namespace Terremesh
{
namespace Math
{
	/// Implements matrix
	class Matrix
	{
	public:
		/// Creates instance of the Matrix class.
		Matrix()
		{
			for (int i = 0; i < 16; ++i)
			{
				m[i] = 0.0;
			}
		}

		/// Creates instance of the Matrix class.
		///
		/// @param[in] value
		///		The fill value.
		Matrix(double value)
		{
			for (int i = 0; i < 16; ++i)
			{
				m[i] = value;
			}
		}

		/// Creates instance of the Matrix class.
		///
		/// @param[in] m11
		///		The matrix component.
		/// @param[in] m12
		///		The matrix component.
		/// @param[in] m13
		///		The matrix component.
		/// @param[in] m14
		///		The matrix component.
		/// @param[in] m21
		///		The matrix component.
		/// @param[in] m22
		///		The matrix component.
		/// @param[in] m23
		///		The matrix component.
		/// @param[in] m24
		///		The matrix component.
		/// @param[in] m31
		///		The matrix component.
		/// @param[in] m32
		///		The matrix component.
		/// @param[in] m33
		///		The matrix component.
		/// @param[in] m34
		///		The matrix component.
		/// @param[in] m41
		///		The matrix component.
		/// @param[in] m42
		///		The matrix component.
		/// @param[in] m43
		///		The matrix component.
		/// @param[in] m44
		///		The matrix component.
		Matrix(double m11, double m12, double m13, double m14,
			double m21, double m22, double m23, double m24,
			double m31, double m32, double m33, double m34,
			double m41, double m42, double m43, double m44)
		{
			m[ 0] = m11; m[ 1] = m12; m[ 2] = m13; m[ 3] = m14;
			m[ 4] = m21; m[ 5] = m22; m[ 6] = m23; m[ 7] = m24;
			m[ 8] = m31; m[ 9] = m32; m[10] = m33; m[11] = m34;
			m[12] = m41; m[13] = m42; m[14] = m43; m[15] = m44;
		}

		double& operator() (int row, int col)
		{
			return m[row * 4 + col];
		}

		const double& operator() (int row, int col) const
		{
			return m[row * 4 + col];
		}

		double& operator[] (int index)
		{
			return m[index];
		}

		const double& operator[] (int index) const
		{
			return m[index];
		}

		/// Computes 3x3 submatrix matrix determinant.
		///
		/// @returns
		///		The matrix determinant.
		double Determinant() const
		{
			return Matrix::Determinant(*this, 0, 1, 2, 4, 5, 6, 8, 9, 10);
		}

		/// Gets vector.
		///
		/// @return
		///		The vector.
		Vec3 GetVector() const
		{
			Vec3 result;

			double det = this->Determinant();

			result.X = -1.0 / det * (Matrix::Determinant(*this, 1, 2, 3, 5, 6, 7, 9, 10, 11));
			result.Y = 1.0 / det * (Matrix::Determinant(*this, 0, 2, 3, 4, 6, 7, 8, 10, 11));
			result.Z = -1.0 / det * (Matrix::Determinant(*this, 0, 1, 3, 4, 5, 7, 8, 9, 11));

			return result;
		}

		/// Determines whether matrix is symmetric.
		///
		/// @retval true when succesful.
		/// @retval false otherwise.
		bool IsSymetric() const
		{
			return m[1] == m[4] && m[2] == m[8] && m[6] == m[9] &&
				m[3] == m[12] && m[7] == m[13] && m[11] == m[14];
		}

		/// Computes submatrix determinant.
		///
		/// @param[in] matrix
		///		The matrix.
		/// @param[in] i11
		///		The component index.
		/// @param[in] i12
		///		The component index.
		/// @param[in] i13
		///		The component index.
		/// @param[in] i21
		///		The component index.
		/// @param[in] i22
		///		The component index.
		/// @param[in] i23
		///		The component index.
		/// @param[in] i31
		///		The component index.
		/// @param[in] i32
		///		The component index.
		/// @param[in] i33
		///		The component index.
		static double Determinant(const Matrix& matrix,
			size_t i11, size_t i12, size_t i13,
			size_t i21, size_t i22, size_t i23,
			size_t i31, size_t i32, size_t i33)
		{
			return 
				matrix.m[i11]*matrix.m[i22]*matrix.m[i33] + matrix.m[i13]*matrix.m[i21]*matrix.m[i32] + matrix.m[i12]*matrix.m[i23]*matrix.m[i31] -
				matrix.m[i13]*matrix.m[i22]*matrix.m[i31] - matrix.m[i11]*matrix.m[i23]*matrix.m[i32] - matrix.m[i12]*matrix.m[i21]*matrix.m[i33];
		}

		/// Adds two matrices.
		///
		/// @param[out] result
		///		The result matrix.
		/// @param[in] value1
		///		The source matrix.
		/// @param[in] value2
		///		The source matrix.
		static void Add(Matrix& result, const Matrix& value1, const Matrix& value2)
		{
			result.m[ 0] = value1.m[ 0] + value2.m[ 0];
			result.m[ 1] = value1.m[ 1] + value2.m[ 1];
			result.m[ 2] = value1.m[ 2] + value2.m[ 2];
			result.m[ 3] = value1.m[ 3] + value2.m[ 3];
			result.m[ 4] = value1.m[ 4] + value2.m[ 4];
			result.m[ 5] = value1.m[ 5] + value2.m[ 5];
			result.m[ 6] = value1.m[ 6] + value2.m[ 6];
			result.m[ 7] = value1.m[ 7] + value2.m[ 7];
			result.m[ 8] = value1.m[ 8] + value2.m[ 8];
			result.m[ 9] = value1.m[ 9] + value2.m[ 9];
			result.m[10] = value1.m[10] + value2.m[10];
			result.m[11] = value1.m[11] + value2.m[11];
			result.m[12] = value1.m[12] + value2.m[12];
			result.m[13] = value1.m[13] + value2.m[13];
			result.m[14] = value1.m[14] + value2.m[14];
			result.m[15] = value1.m[15] + value2.m[15];
		}

		/// Subtracts two matrices.
		///
		/// @param[out] result
		///		The result matrix.
		/// @param[in] value1
		///		The source matrix.
		/// @param[in] value2
		///		The source matrix.
		static void Subtract(Matrix& result, const Matrix& value1, const Matrix& value2)
		{
			result.m[ 0] = value1.m[ 0] - value2.m[ 0];
			result.m[ 1] = value1.m[ 1] - value2.m[ 1];
			result.m[ 2] = value1.m[ 2] - value2.m[ 2];
			result.m[ 3] = value1.m[ 3] - value2.m[ 3];
			result.m[ 4] = value1.m[ 4] - value2.m[ 4];
			result.m[ 5] = value1.m[ 5] - value2.m[ 5];
			result.m[ 6] = value1.m[ 6] - value2.m[ 6];
			result.m[ 7] = value1.m[ 7] - value2.m[ 7];
			result.m[ 8] = value1.m[ 8] - value2.m[ 8];
			result.m[ 9] = value1.m[ 9] - value2.m[ 9];
			result.m[10] = value1.m[10] - value2.m[10];
			result.m[11] = value1.m[11] - value2.m[11];
			result.m[12] = value1.m[12] - value2.m[12];
			result.m[13] = value1.m[13] - value2.m[13];
			result.m[14] = value1.m[14] - value2.m[14];
			result.m[15] = value1.m[15] - value2.m[15];
		}

		const Matrix operator + (const Matrix& matrix) const
		{
			Matrix result;
			Matrix::Add(result, *this, matrix);
			return matrix;
		}

		const Matrix operator - (const Matrix& matrix) const
		{
			Matrix result;
			Matrix::Subtract(result, *this, matrix);
			return matrix;
		}

		Matrix& operator += (const Matrix& matrix)
		{
			Matrix::Add(*this, *this, matrix);
			return (*this);
		}

		Matrix& operator -= (const Matrix& matrix)
		{
			Matrix::Subtract(*this, *this, matrix);
			return (*this);
		}

	public:
		union
		{
			/// Matrix components stored as an array.
			double m[16];

			/// Matrix components stored as a structure.
			struct 
			{
				double M11;
				double M12;
				double M13;
				double M14;
				double M21;
				double M22;
				double M23;
				double M24;
				double M31;
				double M32;
				double M33;
				double M34;
				double M41;
				double M42;
				double M43;
				double M44;
			};
		};
	};
}
}

#endif /* _Terremesh_Math_Matrix_H__ */