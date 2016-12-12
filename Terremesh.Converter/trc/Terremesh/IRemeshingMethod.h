#pragma once
#ifndef _Terremesh_IRemeshingMethod_H__
#define _Terremesh_IRemeshingMethod_H__

#include "Remesh/Mesh.h"

namespace Terremesh
{
	/// Provides interface for remeshing method.
	struct IRemeshingMethod
	{
		virtual ~IRemeshingMethod() {}

		/// Processes mesh using triangles ratio.
		///
		/// @param[in,out] mesh	
		///		The mesh to process.
		/// @param[in] targetRatio
		///		The removed triangle ratio.
		/// @param[in] listener
		///		The progress listener.
		virtual void Process(Remesh::Mesh& mesh, double targetRatio, IProgressListener* listener) = 0;

		/// Processes mesh using triangles count.
		///
		/// @param[in,out] mesh
		///		The mesh to process.
		/// @param[in] targetTriangles
		///		The number of target triangles.
		/// @param[in] listener
		///		The progress listener.
		virtual void Process(Remesh::Mesh& mesh, int targetTriangles, IProgressListener* listener) = 0;
	};
}

#endif /* _Terremesh_IRemeshingMethod_H__ */