#pragma once
#ifndef _Terremesh_Remesh_Triangle_H__
#define _Terremesh_Remesh_Triangle_H__

#include "Vertex.h"
#include "../Math/Plane.h"

namespace Terremesh
{
namespace Remesh
{
	/// Specifies a triangle class.
	class Triangle
	{
	public:
		/// Creates instance of the Triangle class.
		Triangle()
		{
		}

		/// Create instance of the Triangle class using provided vertex identifiers.
		///
		/// @param[in] id1
		///		The vertex ID.
		/// @param[in] id2
		///		The vertex ID.
		/// @param[in] id3
		///		The vertex ID.
		Triangle(VertexId id1, VertexId id2, VertexId id3)
		{
			Vertices[0] = id1;
			Vertices[1] = id2;
			Vertices[2] = id3;
		}

		/// Determines whether triangle has specified vertex by ID.
		///
		/// @param[in] id
		///		The vertex ID.
		///
		/// @retval true when successful.
		/// @retval false otherwise.
		bool HasVertex(VertexId id)
		{
			return Vertices[0] == id || Vertices[1] == id || Vertices[2] == id;
		}

		/// Triangle plane.
		Math::Plane Plane;

		/// Vertices
		VertexId Vertices[3];
	};
}
}

#endif /* _Terremesh_Remesh_Triangle_H__ */