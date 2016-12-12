using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terremesh.Mesh
{
    public interface IProgressListener
    {
        void OnComplete(string stage);
        void OnStarted(string stage);
        void OnStep(long current, long total);
    }
}
