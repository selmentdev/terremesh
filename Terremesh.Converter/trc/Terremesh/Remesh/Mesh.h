#pragma once
#ifndef _Terremesh_Resesh_Mesh_H__
#define _Terremesh_Resesh_Mesh_H__

#include "../Required.h"

#include "Vertex.h"
#include "Triangle.h"

namespace Terremesh
{
namespace Remesh
{
	/// Implements mesh class.
	class Mesh
	{
	public:
		/// Vertex container type.
		typedef std::map<int, Vertex> VertexContainer;

		/// Triangle container type.
		typedef std::deque<Triangle> TriangleContainer;

		/// Gets triangle container.
		///
		/// @return
		///		The triangle container.
		const TriangleContainer& GetTriangles() const { return m_Triangles; }

		/// Sets triangle container.
		///
		/// @param[in] triangles
		///		The triangles container.
		void SetTriangles(const TriangleContainer& triangles) { m_Triangles = triangles; }

		/// Gets vertex container.
		///
		/// @return
		///		The vertex container.
		const VertexContainer& GetVertices() const { return m_Vertices; }

		/// Sets vertex container.
		///
		/// @param[in] vertices
		///		The vertex container.
		void SetVertices(const VertexContainer& vertices) { m_Vertices = vertices; }

		/// Invalidate all planes.
		///
		/// @remarks
		///		Recompute all normals and planes for triangles.
		void InvalidatePlanes();

	private:
		/// Triangles container.
		TriangleContainer m_Triangles;

		/// Vertices container.
		VertexContainer m_Vertices;
	};
}
}

#endif /* _Terremesh_Resesh_Mesh_H__ */