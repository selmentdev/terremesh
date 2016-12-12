#pragma once
#ifndef _Terremesh_Remesh_MeshWriter_H__
#define _Terremesh_Remesh_MeshWriter_H__

#include "../Required.h"
#include "Mesh.h"
#include "../IProgressListener.h"

namespace Terremesh
{
namespace Remesh
{
	/// Implements .obj mesh writer.
	class MeshWriter
	{
	public:
		/// Creates instance of the MeshWriter class.
		///
		/// @param[in] stream
		///		The output stream.
		MeshWriter(std::ofstream& stream);

		/// Writes mesh into stream.
		///
		/// @param[in] mesh
		///		The mesh to write.
		/// @param[in] listener
		///		The progress listener.
		void Write(const Mesh& mesh, IProgressListener* listener);

	private:
		MeshWriter(const MeshWriter&);
		MeshWriter& operator = (const MeshWriter&);

		std::ofstream& m_Stream;
	};
}
}

#endif /* _Terremesh_Remesh_MeshWriter_H__ */