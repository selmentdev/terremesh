#pragma once
#ifndef _Terremesh_Remesh_Vertex_H__
#define _Terremesh_Remesh_Vertex_H__

#include "../Math/Vec3.h"

namespace Terremesh
{
namespace Remesh
{
	/// Specifies vertex structure.
	struct Vertex
	{
	public:
		/// Creates instance of the Vertex structure.
		Vertex()
			: Position(0.0, 0.0, 0.0)
		{
		}

		/// Creates instance of the Vertex structure.
		///
		/// @param[in] position
		///		The vertex position.
		Vertex(const Math::Vec3& position)
			: Position(position)
		{
		}

		/// The vertex position.
		Math::Vec3 Position;
	};
	
	/// The vertex identifier.
	typedef int VertexId;
}
}

#endif /* _Terremesh_Remesh_Vertex_H__ */