#pragma once
#ifndef _Terremesh_Remesh_BinaryMeshReader_H__
#define _Terremesh_Remesh_BinaryMeshReader_H__

#include "../Required.h"
#include "../IProgressListener.h"
#include "Mesh.h"


namespace Terremesh
{
namespace Remesh
{
	/// @todo
	///		Unimplemented.
	class BinaryMeshReader
	{
	public:
		BinaryMeshReader(std::ifstream& stream);
		
		void Read(Mesh& mesh, IProgressListener* listener);

	private:
		BinaryMeshReader(const BinaryMeshReader&);
		BinaryMeshReader& operator = (const BinaryMeshReader&);

		std::ifstream& m_Stream;
	};
}
}

#endif /* _Terremesh_Remesh_BinaryMeshReader_H__ */