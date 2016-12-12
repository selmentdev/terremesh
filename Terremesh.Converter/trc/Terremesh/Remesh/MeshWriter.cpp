#include "MeshWriter.h"


namespace Terremesh
{
namespace Remesh
{
	MeshWriter::MeshWriter(std::ofstream& stream)
		: m_Stream(stream)
	{
	}

	void MeshWriter::Write(const Mesh& mesh, IProgressListener* listener)
	{
		if (listener != nullptr)
		{
			listener->OnStarted("Write");
		}

		auto& vertices = mesh.GetVertices();
		auto& triangles = mesh.GetTriangles();

		// Remap indices
		std::map<VertexId, VertexId> ids;

		VertexId id = 1;

		for (auto it = vertices.begin(); it != vertices.end(); ++it)
		{
			auto& v = it->second.Position;

			m_Stream << "v " << v.X << " " << v.Y << " " << v.Z << std::endl;
			ids.insert(std::make_pair(it->first, id));
			++id;
		}

		for (auto it = triangles.begin(); it != triangles.end(); ++it)
		{			
			auto& t = it->Vertices;

			VertexId t0 = ids[t[0]];
			VertexId t1 = ids[t[1]];
			VertexId t2 = ids[t[2]];

			m_Stream << "f " << t0 << " " << t1 << " " << t2 << std::endl;
		}

		if (listener != nullptr)
		{
			listener->OnCompleted("Write");
		}
	}
}
}