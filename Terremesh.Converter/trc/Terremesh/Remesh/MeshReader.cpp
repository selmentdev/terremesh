#include "MeshReader.h"
#include "Triangle.h"
#include "Vertex.h"

namespace Terremesh
{
namespace Remesh
{
	MeshReader::MeshReader(std::ifstream& stream)
		: m_Stream(stream)
	{
	}

	void MeshReader::Read(Mesh& mesh, IProgressListener* listener)
	{
		if (listener != nullptr)
		{
			listener->OnStarted("Read");
		}

		std::string keyword;
		
		Triangle triangle;
		Vertex vertex;

		Mesh::TriangleContainer triangles;
		Mesh::VertexContainer vertices;

		int verticesCount = 0;

		while (m_Stream.good() && (m_Stream >> keyword))
		{
			if (keyword == "v")
			{
				m_Stream >> vertex.Position.X >> vertex.Position.Y >> vertex.Position.Z;
				vertices.insert(std::make_pair(++verticesCount, vertex));

			}

			if (keyword == "f")
			{
				m_Stream >> triangle.Vertices[0] >> triangle.Vertices[1] >> triangle.Vertices[2];

				triangles.push_back(triangle);
			}
			
			// Ignore other stuff
		}
		// Ended parsing stuff

		mesh.SetTriangles(triangles);
		mesh.SetVertices(vertices);
		mesh.InvalidatePlanes();

		if (listener != nullptr)
		{
			listener->OnCompleted("Read");
		}
	}
}
}