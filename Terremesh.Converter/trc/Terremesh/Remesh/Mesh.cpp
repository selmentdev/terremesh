#include "Mesh.h"

namespace Terremesh
{
namespace Remesh
{
	void Mesh::InvalidatePlanes()
	{
		for (auto it = m_Triangles.begin(); it != m_Triangles.end(); ++it)
		{
			assert(m_Vertices.find(it->Vertices[0]) != m_Vertices.end());
			assert(m_Vertices.find(it->Vertices[1]) != m_Vertices.end());
			assert(m_Vertices.find(it->Vertices[2]) != m_Vertices.end());

			it->Plane = Math::Plane(
				m_Vertices[it->Vertices[0]].Position,
				m_Vertices[it->Vertices[1]].Position,
				m_Vertices[it->Vertices[2]].Position);
		}
	}
}
}