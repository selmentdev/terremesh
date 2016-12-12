using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terremesh.Mesh
{
    public interface IRemeshingMethod
    {
        void Process(ref Mesh mesh, double targetRatio, IProgressListener listener = null);
        void Process(ref Mesh mesh, int targetTriangles, IProgressListener listener = null);
    }
}
