#pragma once
#ifndef _Terremesh_Remesh_MeshReader_H__
#define _Terremesh_Remesh_MeshReader_H__

#include "../Required.h"
#include "../IProgressListener.h"
#include "Mesh.h"

namespace Terremesh
{
namespace Remesh
{
	/// Implements .obj file mesh reader.
	class MeshReader
	{
	public:
		/// Creates instance of the MeshReader class.
		///
		/// @param[in] stream
		///		The input stream.
		MeshReader(std::ifstream& stream);
		
		/// Reads mesh from stream.
		///
		/// @param[out] mesh
		///		The mesh.
		/// @param[in] listener
		///		The progress listener.
		void Read(Mesh& mesh, IProgressListener* listener);

	private:
		MeshReader(const MeshReader&);
		MeshReader& operator = (const MeshReader&);

		std::ifstream& m_Stream;
	};
}
}

#endif /* _Terremesh_Mesh_MeshReader_H__ */