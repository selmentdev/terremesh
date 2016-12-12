#pragma once
#ifndef _Terremesh_QuadricErrorMetric_QuadricErrorMetricMethod_H__
#define _Terremesh_QuadricErrorMetric_QuadricErrorMetricMethod_H__

#include "../Required.h"

#include "../IProgressListener.h"
#include "../IRemeshingMethod.h"
#include "../Remesh/Mesh.h"
#include "ErrorMetric.h"

namespace Terremesh
{
namespace QuadricErrorMetric
{
	/// Implementation of Quadric Error Metric method.
	class QuadricErrorMetricMethod
		: public IRemeshingMethod
		{
	public:
		/// Creates instance of the QuadricErrorMetricMethod class.
		QuadricErrorMetricMethod()
		{
			m_EnableVirtualPairs = false;
		}
		
		virtual void Process(Remesh::Mesh& mesh, double targetRatio, IProgressListener* listener);
		virtual void Process(Remesh::Mesh& mesh, int targetTriangles, IProgressListener* listener);

		/// Gets value indicating whether method is using virtual pairs.
		///
		/// @retval true when successful.
		/// @retval false otherwise.
		bool GetEnableVirtualPairs() const { return m_EnableVirtualPairs; }

		/// Sets value indicating whether method is using virtual pairs.
		///
		/// @param[in] value
		///		The value.
		void SetEnableVirtualPairs(bool value) { m_EnableVirtualPairs = value; }

	private:
		/// The ertex pair type.
		typedef std::pair<Remesh::VertexId, Remesh::VertexId> VertexPair;

		/// The error metric container type.
		typedef std::map<Remesh::VertexId, ErrorMetric> ErrorMetricContainer;

		/// The edge error container type.
		typedef std::map<VertexPair, double> EdgeErrorContainer;

		/// Triangle container.
		Remesh::Mesh::TriangleContainer m_Triangles;

		/// Vertex container.
		Remesh::Mesh::VertexContainer m_Vertices;

		/// Error metrics container.
		ErrorMetricContainer m_ErrorMetrics;

		/// Edge error container.
		EdgeErrorContainer m_Edges;

		/// Virtual pairs.
		bool m_EnableVirtualPairs;
		
	private:
		/// Initializes mesh for remeshing.
		///
		/// @param[in] listener
		///		The progress listener.
		void Initialize(IProgressListener* listener);

		/// Selects valid pairs.
		///
		/// @param[in] treshold
		///		The treshold for virtual pairs.
		/// @param[in] listener
		///		The progress listener.
		void SelectValidPairs(double treshold = 0.10, IProgressListener* listener = nullptr);

		/// Remeshes mesh.
		///
		/// @param[in] targetTriangles
		///		The number of target mesh triangles.
		/// @param[in] listener
		///		The progress listener.
		void Remesh(int targetTriangles, IProgressListener* listener);

		/// Computes error for vertices pair.
		///
		/// @param[in] pair
		///		The vertex pair.
		/// @param[out] error
		///		The error point.
		///
		/// @return
		///		The error value.
		double ComputeError(const VertexPair& pair, Math::Vec3& error)
		{
			return ComputeError(pair.first, pair.second, error);
		}

		/// Computes error for vertices pair.
		///
		/// @param[in] pair
		///		The vertex pair.
		///
		/// @return
		///		The error value.
		double ComputeError(const VertexPair& pair)
		{
			Math::Vec3 error;
			return ComputeError(pair, error);
		}

		/// Computes error for vertices pair.
		///
		/// @param[in] id1
		///		The vertex ID.
		/// @param[in] id2
		///		The vertex ID.
		/// @param[out] error
		///		The error point.
		///
		/// @returns
		///		The error value.
		double ComputeError(Remesh::VertexId id1, Remesh::VertexId id2, Math::Vec3& error);

		/// Computes error for vertices pair.
		///
		/// @param[in] id1
		///		The vertex ID.
		/// @param[in] id2
		///		The vertex ID.
		///
		/// @returns
		///		The error value.
		double ComputeError(Remesh::VertexId id1, Remesh::VertexId id2)
		{
			Math::Vec3 error;
			return ComputeError(id1, id2, error);
		}
	};
}
}

#endif /* _Terremesh_QuadricErrorMetric_Method_H__ */
