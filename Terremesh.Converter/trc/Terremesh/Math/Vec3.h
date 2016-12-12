#pragma once
#ifndef _Terremesh_Math_Vec3_H__
#define _Terremesh_Math_Vec3_H__

#include "../Required.h"
#include <cmath>

namespace Terremesh
{
namespace Math
{
	/// Represents 3D vector.
	struct Vec3
	{
		/// Creates instance of the Vec3 struct.
		Vec3()
		{
		}

		/// Creates instance of the Vec3 struct.
		///
		/// @param[in] x
		///		The X component.
		/// @param[in] y
		///		The Y component.
		/// @param[in] z
		///		The Z component.
		Vec3(double x, double y, double z)
			: X(x), Y(y), Z(z)
		{
		}

		/// Normalizes vector.
		void Normalize()
		{
			double invLen = 1.0 / Length();
			X *= invLen;
			Y *= invLen;
			Z *= invLen;
		}

		/// Computes length of the vector.
		///
		/// @returns
		///		The length of the vector.
		double Length() const
		{
			return std::sqrt(LengthSquared());
		}

		/// Computes squared length of the vector.
		///
		/// @returns
		///		The squared length of the vector.
		double LengthSquared() const
		{
			return X*X + Y*Y + Z*Z;
		}

		/// Computes distance between two vectors.
		///
		/// @param[in] value1
		///		The source vector.
		/// @parma[in] value2
		///		The source vector.
		///
		/// @returns
		///		The distance between vectors.
		static double Distance(const Vec3& value1, const Vec3& value2)
		{
			Vec3 difference;
			Vec3::Subtract(difference, value1, value2);
			return difference.Length();
		}

		/// Computes center vector between two vectors.
		///
		/// @param[out] result
		///		The result vector.
		/// @param[in] value1
		///		The source vector.
		/// @param[in] value2
		///		The source vector.
		static void Center(Vec3& result, const Vec3& value1, const Vec3& value2)
		{
			result.X = 0.5f * (value1.X + value2.X);
			result.Y = 0.5f * (value1.Y + value2.Y);
			result.Z = 0.5f * (value1.Z + value2.Z);
		}

		bool operator == (const Vec3& vector) const
		{
			return X == vector.X &&
				Y == vector.Y &&
				Z == vector.Z;
		}

		bool operator != (const Vec3& vector) const
		{
			return ((*this) == vector) == false;
		}

		/// Adds two vectors.
		///
		/// @param[out] result
		///		The result vector.
		/// @param[in] value1
		///		The source vector.
		/// @param[in] value2
		///		The source vector.
		static void Add(Vec3& result, const Vec3& value1, const Vec3& value2)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		/// Subtracts two vectors.
		///
		/// @param[out] result
		///		The result vector.
		/// @param[in] value1
		///		The source vector.
		/// @param[in] value2
		///		The source vector.
		static void Subtract(Vec3& result, const Vec3& value1, const Vec3& value2)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

	public:
		/// The X component.
		double X;

		/// The Y component.
		double Y;

		/// The Z component.
		double Z;
	};
}
}

#endif /* _Terremesh_Math_Vec3_H__ */