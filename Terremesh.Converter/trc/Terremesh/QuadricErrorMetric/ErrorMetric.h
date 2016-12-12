#pragma once
#ifndef _Terremesh_QuadricErrorMetric_ErrorMetric_H__
#define _Terremesh_QuadricErrorMetric_ErrorMetric_H__

#include "../Math/Matrix.h"
#include "../Math/Vec3.h"
#include "../Math/Plane.h"

namespace Terremesh
{
namespace QuadricErrorMetric
{
	/// Implements Quadric Error Metric.
	class ErrorMetric
	{
	public:
		/// Creates instance of the ErrorMetric class.
		ErrorMetric()
			: m_Matrix(0.0)
		{
		}

		/// Creates instance of the ErrrorMetric class.
		///
		/// @param[in] plane
		///		The plane.
		ErrorMetric(Math::Plane plane)
		{
			double aa = plane.Normal.X * plane.Normal.X;
			double bb = plane.Normal.Y * plane.Normal.Y;
			double cc = plane.Normal.Z * plane.Normal.Z;
			double dd = plane.D * plane.D;
			double ab = plane.Normal.X * plane.Normal.Y;
			double ac = plane.Normal.X * plane.Normal.Z;
			double ad = plane.Normal.X * plane.D;
			double bc = plane.Normal.Y * plane.Normal.Z;
			double bd = plane.Normal.Y * plane.D;
			double cd = plane.Normal.Z * plane.D;

			m_Matrix = Math::Matrix(
                aa, ab, ac, ad,
                ab, bb, bc, bd,
                ac, bc, cc, cd,
                ad, bd, cd, dd);
		}

		/// Evaluates error metric for specified point.
		///
		/// @param[in] point
		///		The point to evaluate.
		double Evaluate(const Math::Vec3& point)
		{
			return
				m_Matrix.m[0] * point.X * point.X +
                m_Matrix.m[1] * point.X * point.Y * 2.0 +
                m_Matrix.m[2] * point.X * point.Z * 2.0 +
                m_Matrix.m[3] * point.X * 2.0 +
                m_Matrix.m[5] * point.Y * point.Y +
                m_Matrix.m[6] * point.Y * point.Z * 2.0 +
                m_Matrix.m[7] * point.Y * 2.0 +
                m_Matrix.m[10] * point.Z * point.Z +
                m_Matrix.m[11] * point.Z * 2.0 +
				m_Matrix.m[15];
		}

		/// Gets matrix.
		///
		/// @return
		///		The matrix.
		const Math::Matrix& GetMatrix() const { return m_Matrix; }

		/// Sets matrix.
		///
		/// @param[in] value
		///		The matrix.
		void SetMatrix(const Math::Matrix& value) { m_Matrix = value; }

		/// Adds value1 to value2 and store into result.
		///
		/// @param[out] result
		///		The return error metric.
		/// @param[in] value1
		///		The source error metric.
		/// @param[in] value2
		///		The source error metric.
		static void Add(ErrorMetric& result, const ErrorMetric& value1, const ErrorMetric& value2)
		{
			Math::Matrix::Add(result.m_Matrix, value1.m_Matrix, value2.m_Matrix);
		}

		/// Subtracts value2 from value1 and store into result.
		///
		/// @param[out] result
		///		The return error metric.
		/// @param[in] value1
		///		The source error metric.
		/// @param[in] value2
		///		The source error metric.
		static void Subtract(ErrorMetric& result, const ErrorMetric& value1, const ErrorMetric& value2)
		{
			Math::Matrix::Subtract(result.m_Matrix, value1.m_Matrix, value2.m_Matrix);
		}

	private:
		/// The matrix.
		Math::Matrix m_Matrix;
	};
}
}

#endif /* _Terremesh_QuadricErrorMetric_ErrorMetric_H__ */