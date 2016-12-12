#pragma once
#ifndef _Terremesh_Math_Plane_H__
#define _Terremesh_Math_Plane_H__

#include "../Required.h"
#include "Vec3.h"

namespace Terremesh
{
namespace Math
{
	/// Represents plane.
	class Plane
	{
	public:
		/// Creates instance of the Plane struct.
		Plane()
			: Normal(0.0, 0.0, 1.0)
			, D(0.0)
		{
		}

		/// Creates instance of the Plane struct.
		///
		/// @param[in] pos1
		///		The source vector.
		/// @param[in] pos2
		///		The source vector.
		/// @param[in] pos3
		///		The source vector.
		Plane(const Vec3& pos1, const Vec3& pos2, const Vec3& pos3)
		{
			Normal.X = (pos2.Y - pos1.Y) * (pos3.Z - pos1.Z) - (pos2.Z - pos1.Z) * (pos3.Y - pos1.Y);
			Normal.Y = (pos2.Z - pos1.Z) * (pos3.X - pos1.X) - (pos2.X - pos1.X) * (pos3.Z - pos1.Z);
			Normal.Z = (pos2.X - pos1.X) * (pos3.Y - pos1.Y) - (pos2.Y - pos1.Y) * (pos3.X - pos1.X);
			Normal.Normalize();

			D = - (Normal.X * pos1.X + Normal.Y * pos1.Y + Normal.Z * pos1.Z);
		}

	public:
		/// Plane normal vector.
		Vec3 Normal;

		/// Negative plane distance from coordinate system origin.
		double D;
	};
}
}

#endif /* _Terremesh_Math_Plane_H__ */
