using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terremesh.Mesh
{
    public struct VertexPair
    {
        public int First;
        public int Second;

        public bool Equals(VertexPair pair)
        {
            return First == pair.First &&
                Second == pair.Second;
        }
    }
}
