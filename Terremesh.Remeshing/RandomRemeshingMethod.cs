using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace Terremesh.Remeshing
{
    /// <summary>
    /// Implements random mesh remeshing method.
    /// </summary>
    public class RandomRemeshingMethod : IRemeshingMethod
    {
        #region Methods

        public void Process(ref Mesh mesh, double ratio, IProgressListener listener = null)
        {
            Process(ref mesh, (int)(mesh.Triangles.Count * (1.0 - ratio)), listener);
        }

        public void Process(ref Mesh mesh, int toRemove, IProgressListener listener = null)
        {
            Remesh(ref mesh, toRemove, listener);
        }

        private void Remesh(ref Mesh mesh, int toRemove, IProgressListener progress = null)
        {
            if (!(toRemove > mesh.Triangles.Count && mesh.Triangles.Count > 10))
            {
                throw new ArgumentOutOfRangeException("toRemove", toRemove, "Invalid triangle count");
            }

            var vertices = mesh.Vertices;

            progress.OnStart("Started remeshing");

            int totalVertices = mesh.Vertices.Count;

            Debug.Assert(toRemove > 0);

            Random random = new Random();

            while (toRemove > 0)
            {
                //vertices.ran

                var element = vertices.ElementAt(random.Next(0, vertices.Count - 1));
                mesh.JoinToNearestByDistance(element);
                toRemove -= 2;

                progress.OnProgress(toRemove, totalVertices);
            }

            progress.OnComplete("Ended remeshing");
        }

        public void Reset()
        {
            m_Removed = 0;
        }

        public override string ToString()
        {
            return "Random Remeshing Method";
        }
        #endregion

        #region Properties
        public int Removed
        {
            get { return m_Removed; }
            set { m_Removed = value; }
        }
        #endregion

        #region Fields
        private int m_Removed = 0;
        #endregion
    }
}
