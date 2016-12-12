using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Terremesh.Remeshing
{
    /// <summary>
    /// Implements remeshing method using Angle Sum error metric.
    /// </summary>
    public class AngleSumErrorMetricMethod : IRemeshingMethod
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

        /// <summary>
        /// Remeshes specified mesh.
        /// </summary>
        /// <param name="mesh">The mesh to remesh</param>
        /// <param name="toRemove">The number of triangles to remove.</param>
        /// <param name="progress">The progress listener.</param>
        private void Remesh(ref Mesh mesh, int toRemove, IProgressListener progress = null)
        {
            if (!(toRemove > mesh.Triangles.Count && mesh.Triangles.Count > 10))
            {
                throw new ArgumentOutOfRangeException("toRemove", toRemove, "Invalid triangle count");
            }

            progress.OnStart("Remeshing using AngleSum Error Metric");

            var triangles = mesh.Triangles;
            var vertices = mesh.Vertices;

            if (triangles.Count <= toRemove)
            {
                progress.OnComplete("Failed. Too many triangles to remove");
            }

            //int toRemove = (int)((m_Removed) * triangles.Count);
            int triangleLimit = triangles.Count - toRemove;

            foreach (var v in vertices)
            {
                UpdateVertexCost(v);
            }

#if false   // Adding mesh border penalty.
            foreach (var v in vertices)
            {
                // Border vertices has no more that 5 neighbors
                if (v.Neighbors.Count <= 4)
                {
                    v.Cost += 1.0;
                }
            }
#endif

            int index = 0;

            while (triangles.Count > triangleLimit)
            {
                var min = mesh.Vertices.Min();
                if (mesh.JoinToNearestByCost(min, Mesh.JoinPositionType.Source))
                {
                    UpdateVertexCost(min);
                    UpdateVertexNeighborsCost(min);

                    progress.OnProgress(vertices.Count, triangles.Count);
                    index += 2;
                }
            }

            progress.OnComplete("End of remeshing using AngleSum Error Metric");
        }

        /// <summary>
        /// Tries to search best candidate for remeshing.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="minimal">The minimal erase cost vertex.</param>
        /// <param name="toJoin">The vertex proposed to removal.</param>
        /// <returns>true when successful, false otherwise.</returns>
        public bool SearchBestCandidate(ref Mesh mesh, out Vertex minimal, out Vertex toJoin)
        {
            Vertex vertex = mesh.Vertices.Min();
            Vertex neighbor = null;

            float distance = float.MaxValue;

            foreach (var v in vertex.Neighbors)
            {
                float current = Vector3.DistanceSquared(vertex.Position, v.Position);
                if (current < distance)
                {
                    distance = current;
                    neighbor = v;
                }
            }

            minimal = vertex;
            toJoin = neighbor;

            return neighbor != null;
        }

        /// <summary>
        /// Computes vertex cost.
        /// </summary>
        /// <param name="vertex">The source vertex.</param>
        /// <returns>The vertex removal cost.</returns>
        public double ComputeVertexCost(Vertex vertex)
        {
            double angle = 0.0;

            var triangles = vertex.Triangles;

            foreach (var t in triangles)
            {
                angle += t.ComputeCornerAngle(vertex);
            }

#if false
            double normalized = (double)(angle / (Math.PI * 2.0));

            return Math.Abs(normalized - 1.0);
#else
            double normalized = (double)(angle / (Math.PI * 2.0));

            return Math.Abs(normalized - 1.0);
#endif
        }

        /// <summary>
        /// Updates vertex cost.
        /// </summary>
        /// <param name="vertex">The source vertex.</param>
        public void UpdateVertexCost(Vertex vertex)
        {
            vertex.Cost = ComputeVertexCost(vertex);
        }

        /// <summary>
        /// Updates costs of neighbour vertices.
        /// </summary>
        /// <param name="vertex">The source vertex.</param>
        public void UpdateVertexNeighborsCost(Vertex vertex)
        {
            foreach (var v in vertex.Neighbors)
            {
                UpdateVertexCost(v);
            }
        }

        /// <summary>
        /// Resets method to defaults.
        /// </summary>
        public void Reset()
        {
        }

        public override string ToString()
        {
            return "AngleSum Error Metric";
        }
        #endregion
    }
}
