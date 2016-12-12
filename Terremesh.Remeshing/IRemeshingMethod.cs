using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terremesh.Remeshing
{
    /// <summary>
    /// Remeshing method interface.
    /// </summary>
    public interface IRemeshingMethod
    {
        #region Methods

        /// <summary>
        /// Processes specified mesh.
        /// </summary>
        /// <param name="mesh">The mesh to process.</param>
        /// <param name="ratio">The removed triangles to all triangles ratio.</param>
        /// <param name="listener">The progress listener.</param>
        void Process(ref Mesh mesh, double ratio, IProgressListener listener = null);

        /// <summary>
        /// Processes specified mesh.
        /// </summary>
        /// <param name="mesh">The mesh to process.</param>
        /// <param name="toRemove">The number of triangles to remove.</param>
        /// <param name="listener">The progress listener.</param>
        void Process(ref Mesh mesh, int toRemove, IProgressListener listener = null);
        
        /// <summary>
        /// Resets remeshing method parameters.
        /// </summary>
        void Reset();

        string ToString();
        #endregion
    }
}
