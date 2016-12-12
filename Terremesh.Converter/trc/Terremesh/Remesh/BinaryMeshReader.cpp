#include "BinaryMeshReader.h"

namespace Terremesh
{
namespace Remesh
{
	BinaryMeshReader::BinaryMeshReader(std::ifstream& stream)
		: m_Stream(stream)
	{
	}

	void BinaryMeshReader::Read(Mesh& mesh, IProgressListener* listener)
	{
		if (listener != nullptr)
		{
			listener->OnStarted("Read");
		}

		m_Stream.close();

		while (m_Stream.good())
		{
			
		}

		if (listener != nullptr)
		{
			listener->OnCompleted("Read");
		}
	}
}
}