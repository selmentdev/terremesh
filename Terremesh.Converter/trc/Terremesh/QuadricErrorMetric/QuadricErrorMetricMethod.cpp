#include "QuadricErrorMetricMethod.h"

#include "../Math/Matrix.h"
#include "../Math/Vec3.h"

namespace Terremesh
{
namespace QuadricErrorMetric
{
	void QuadricErrorMetricMethod::Process(Remesh::Mesh& mesh, double targetRatio, IProgressListener* listener)
	{
		int totalTriangles = mesh.GetTriangles().size();
		int trianglesLimit = (int)(targetRatio * totalTriangles);

		Process(mesh, trianglesLimit, listener);
	}

	void QuadricErrorMetricMethod::Process(Remesh::Mesh& mesh, int targetTriangles, IProgressListener* listener)
	{
		m_Vertices = mesh.GetVertices();
		m_Triangles = mesh.GetTriangles();

		Initialize(listener);
		Remesh(targetTriangles, listener);

		mesh.SetVertices(m_Vertices);
		mesh.SetTriangles(m_Triangles);
	}

	void QuadricErrorMetricMethod::Initialize(IProgressListener* listener)
	{
		if (listener != nullptr)
		{
			listener->OnStarted("Initialize quadrics");
		}

		// Initialize error metrics for each vertex available.
		for (auto i = 1; i <= (int)m_Vertices.size(); ++i)
		{
			// By empty error metric.
			m_ErrorMetrics.insert(
				std::make_pair(
					(int)i,
					ErrorMetric()
				)
			);
		}

		// For each triangle compute plane metric and add it to neighbor vertex
		for (auto it = m_Triangles.begin(); it != m_Triangles.end(); ++it)
		{
			for (auto vertex = 0; vertex < 3; ++vertex)
			{
				auto& vertexMetric = m_ErrorMetrics[it->Vertices[vertex]];
				auto& planeMetric = ErrorMetric(it->Plane);

				// Adding error metrics.
				ErrorMetric::Add(vertexMetric, vertexMetric, planeMetric);
			}
		}

		if (listener != nullptr)
		{
			listener->OnCompleted("Initialize quadrics");
		}
	}

	void QuadricErrorMetricMethod::SelectValidPairs(double treshold, IProgressListener* listener)
	{
		if (listener != nullptr)
		{
			listener->OnStarted("Selecting pairs");
		}

		// For each triangle
		for (auto it = m_Triangles.begin(); it != m_Triangles.end(); ++it)
		{
			// Compute edge costs (edge 01)
			VertexPair pair;
			pair.first = std::min(it->Vertices[0], it->Vertices[1]);
			pair.second = std::max(it->Vertices[0], it->Vertices[1]);

			if (m_Edges.find(pair) == m_Edges.end())
			{
				m_Edges.insert(std::make_pair(pair, ComputeError(pair)));
			}

			// Compute edge costs (edge 12)
			pair.first = std::min(it->Vertices[1], it->Vertices[2]);
			pair.second = std::max(it->Vertices[1], it->Vertices[2]);

			if (m_Edges.find(pair) == m_Edges.end())
			{
				m_Edges.insert(std::make_pair(pair, ComputeError(pair)));
			}

			// Compute edge costs (edge 20)
			pair.first = std::min(it->Vertices[2], it->Vertices[0]);
			pair.second = std::max(it->Vertices[2], it->Vertices[0]);

			if (m_Edges.find(pair) == m_Edges.end())
			{
				m_Edges.insert(std::make_pair(pair, ComputeError(pair)));
			}
		}

		if (listener != nullptr)
		{
			listener->OnCompleted("Selecting pairs");
		}

		// Check if virtual pairs are enabled 
		/// @bug Not working as expected.
		if (m_EnableVirtualPairs)
		{
			if (listener != nullptr)
			{
				listener->OnStarted("Selecting virtual pairs");
			}

			// Search for vertex pairs with distance lesser than treshold
			for (auto i = 1; i < (int)m_Vertices.size(); ++i)
			{
				for (auto j = i + 1; j < (int)m_Vertices.size(); ++j)
				{
					if (Math::Vec3::Distance(m_Vertices[i].Position, m_Vertices[j].Position) < treshold)
					{
						VertexPair pair(i, j);
						m_Edges.insert(std::make_pair(pair, ComputeError(pair)));
					}
				}
			}

			if (listener != nullptr)
			{
				listener->OnCompleted("Selecting virtual pairs");
			}
		}
	}

	double QuadricErrorMetricMethod::ComputeError(Remesh::VertexId id1, Remesh::VertexId id2, Math::Vec3& error)
	{
		ErrorMetric edge;

		Math::Vec3 vertex;

		// Get metrics for involved vertices
		auto e1 = m_ErrorMetrics[id1];
		auto e2 = m_ErrorMetrics[id2];

		// Add and assume they represent edge error metric
		ErrorMetric::Add(edge, e1, e2);

		// Log when they aren't symmetric
		if (!edge.GetMatrix().IsSymetric())
		{
			std::cerr << "The result matrix is not symmetric" << std::endl;
		}

		// Compute delta edge
		ErrorMetric delta = edge;
		{
			Math::Matrix m = delta.GetMatrix();
			{
				m.M41 = 0.0;
				m.M42 = 0.0;
				m.M43 = 0.0;
				m.M44 = 1.0;
			}
			delta.SetMatrix(m);
		}

		double minError = 0.0;

		// If matrix is not invertible
		if (std::abs(delta.GetMatrix().Determinant()) <= 1e-5)
		{
			// Take two vertices and center between them
			Math::Vec3 v1 = m_Vertices[id1].Position;
			Math::Vec3 v2 = m_Vertices[id2].Position;
			Math::Vec3 v3;
			Math::Vec3::Center(v3, v1, v2);
			
			// And evaluate costs
			double e1 = edge.Evaluate(v1);
			double e2 = edge.Evaluate(v2);
			double e3 = edge.Evaluate(v3);

			// And choose wisely
			minError = std::min(std::min(e1, e2), e3);

			if (minError == e1)
			{
				vertex = v1;
			}
			else if (minError == e2)
			{
				vertex = v2;
			}
			else if (minError == e3)
			{
				vertex = v3;
			}
		}
		else
		{
			// Otherwise compute vertex from matrix.
			vertex = delta.GetMatrix().GetVector();
		}

		error = vertex;
		minError = edge.Evaluate(vertex);
		return minError;
	}

	void QuadricErrorMetricMethod::Remesh(int targetTriangles, IProgressListener* listener)
	{
		SelectValidPairs(0.1, listener);

		if (listener != nullptr)
		{
			listener->OnStarted("Remesh");
		}

		// Compute total and remaining triangles count.
		int totalTriangles = m_Triangles.size();
		int remainingTriangles = m_Triangles.size() - targetTriangles;


		Math::Vec3 error;

		// Until we don't reached remaining triangles count.
		while ((int)m_Triangles.size() > remainingTriangles)
		{
			if (listener != nullptr)
			{
				listener->OnStep(
					targetTriangles - (m_Triangles.size() - remainingTriangles),
					targetTriangles);
			}
			
			double minError = (double)std::numeric_limits<int>::max();

			EdgeErrorContainer::iterator itMinError;

			// Find cheapest edge
			for (auto it = m_Edges.begin(); it != m_Edges.end(); ++it)
			{
				if (it->second < minError)
				{
					minError = it->second;
					itMinError = it;
				}
			}

			// Compute error for pair
			VertexPair pairMinError = itMinError->first;

			ComputeError(pairMinError, error);

			m_Vertices[pairMinError.first].Position = error;
			
			// Compute error metric
			ErrorMetric::Add(
				m_ErrorMetrics[pairMinError.first],
				m_ErrorMetrics[pairMinError.first],
				m_ErrorMetrics[pairMinError.second]);

			// And for each triangle
			for (auto it = m_Triangles.begin(); it != m_Triangles.end();)
			{
				// And for each vertex in triangle
				for (auto j = 0; j < 3; ++j)
				{
					if (it->Vertices[j] == pairMinError.second)
					{
						if (
							(it->Vertices[0] == pairMinError.first) ||
							(it->Vertices[1] == pairMinError.first) ||
							(it->Vertices[2] == pairMinError.first))
						{
							// Erase
							it = m_Triangles.erase(it);
						}
						else
						{
							// Or not
							it->Vertices[j] = pairMinError.first;
							++it;
						}
						break;
					}
					else if (j == 2)
					{
						++it;
					}
				}
			}

			// And erase second vertex - it's merged now with first
			m_Vertices.erase(pairMinError.second);

			// Update involved edges costs - set as 0
			for (auto it = m_Edges.begin(); it != m_Edges.end(); /*++it*/)
			{
				auto pair = it->first;

				if ((pair.first == pairMinError.second) && (pair.second != pairMinError.first))
				{
					it = m_Edges.erase(it);

					m_Edges.insert(std::make_pair(
						VertexPair(
							std::min(pairMinError.first, pair.second),
							std::max(pairMinError.first, pair.second)
						),
						0.0
					));
				}
				else if ((pair.second == pairMinError.second) && (pair.first != pairMinError.first))
				{
					it = m_Edges.erase(it);

					m_Edges.insert(std::make_pair(
						VertexPair(
							std::min(pairMinError.first, pair.first),
							std::max(pairMinError.first, pair.first)
						),
						0.0
					));
				}
				else
				{
					++it;
				}
			}

			m_Edges.erase(itMinError);

			// Recompute all involved edges costs.
			for (auto it = m_Edges.begin(); it != m_Edges.end(); ++it)
			{
				auto pair = it->first;

				if (pair.first == pairMinError.first)
				{
					it->second = ComputeError(pairMinError.first, pair.second);
				}

				if (pair.second == pairMinError.first)
				{
					it->second = ComputeError(pairMinError.first, pair.first);
				}
			}
		}

		if (listener != nullptr)
		{
			listener->OnCompleted("Remesh");
		}
	}
}
}