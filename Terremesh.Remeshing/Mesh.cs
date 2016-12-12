using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;

namespace Terremesh.Remeshing
{
    /// <summary>
    /// Mesh class
    /// </summary>
    public class Mesh
    {
        #region Enumerations
        /// <summary>
        /// Provides enumeration which represents type of the joining positions.
        /// </summary>
        public enum JoinPositionType
        {
            /// <summary>
            /// Join to the source position.
            /// </summary>
            Source,

            /// <summary>
            /// Join to the target position.
            /// </summary>
            Target,

            /// <summary>
            /// Join to the middle position.
            /// </summary>
            Middle,
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a instance of the Mesh class.
        /// </summary>
        /// <param name="vertices">The array of vertices</param>
        /// <param name="indices">The array of indices</param>
        public Mesh(ref List<Vertex> vertices, ref List<int> indices)
        {
            Load(ref vertices, ref indices);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads mesh from vertices and indices.
        /// </summary>
        /// <param name="vertices">The array of vertices</param>
        /// <param name="indices">The array of indices</param>
        public void Load(ref List<Vertex> vertices, ref List<int> indices)
        {
            m_Triangles = new HashSet<Triangle>();
            m_Vertices = new HashSet<Vertex>();

            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }

            for (int i = 0; i < indices.Count; i += 3)
            {
                AddTriangle(
                    new Triangle(
                        vertices[indices[i + 0]],
                        vertices[indices[i + 1]],
                        vertices[indices[i + 2]]));
            }
        }

        /// <summary>
        /// Saves mesh into vertices and indices arrays.
        /// </summary>
        /// <param name="vertices">The array of vertices.</param>
        /// <param name="indices">The array of indices</param>
        public void Save(out List<Vertex> vertices, out List<int> indices)
        {
            RetagVertices();
            vertices = new List<Vertex>();
            indices = new List<int>();

            foreach (var vertex in m_Vertices)
            {
                vertices.Add(vertex);
            }

            foreach (var triangle in m_Triangles)
            {
                indices.Add(triangle.Vertex1.Index);
                indices.Add(triangle.Vertex2.Index);
                indices.Add(triangle.Vertex3.Index);
            }
        }

        /// <summary>
        /// Normalizes all vertices normals.
        /// </summary>
        public void NormalizeNormals()
        {
            foreach (var vertex in m_Vertices)
            {
                vertex.Normalize();
            }
        }

        /// <summary>
        /// Retags vertices.
        /// </summary>
        public void RetagVertices()
        {
            int current = 0;
            foreach (var vertex in m_Vertices)
            {
                vertex.Index = current;
                ++current;
            }
        }

        /// <summary>
        /// Adds triangle.
        /// </summary>
        /// <param name="triangle">The triangle to add.</param>
        internal void AddTriangle(Triangle triangle)
        {
            m_Triangles.Add(triangle);
        }

        /// <summary>
        /// Removes triangle.
        /// </summary>
        /// <param name="triangle">The triangle to remove.</param>
        /// <returns>true when successful, false otherwise.</returns>
        internal bool RemoveTriangle(Triangle triangle)
        {
            return m_Triangles.Remove(triangle);
        }

        /// <summary>
        /// Adds vertex.
        /// </summary>
        /// <param name="vertex">The vertex to add.</param>
        internal void AddVertex(Vertex vertex)
        {
            m_Vertices.Add(vertex);
        }

        /// <summary>
        /// Removes vertex.
        /// </summary>
        /// <param name="vertex">The vertex to remove.</param>
        /// <returns>true when successful, false otherwise.</returns>
        internal bool RemoveVertex(Vertex vertex)
        {
            return m_Vertices.Remove(vertex);  
        }

        /// <summary>
        /// Joins vertex to the cheapest neighbour.
        /// </summary>
        /// <param name="vertex">The vertex to join.</param>
        /// <param name="type">The type of position join method.</param>
        /// <returns></returns>
        public bool JoinToNearestByCost(Vertex vertex, JoinPositionType type = JoinPositionType.Middle)
        {
            Debug.Assert(vertex != null);

            Vertex nearest = vertex.Neighbors.Min();

            if (nearest != null)
            {
                Vector3 position;
                switch (type)
                {
                    case JoinPositionType.Source:
                        {
                            position = vertex.Position;
                            break;
                        }
                    case JoinPositionType.Target:
                        {
                            position = nearest.Position;
                            break;
                        }
                    default:
                    case JoinPositionType.Middle:
                        {
                            position = Vector3Extension.Mean(vertex.Position, nearest.Position);
                            break;
                        }
                }

                return JoinVertices(vertex, nearest, position);
            }
            return false;
        }

        /// <summary>
        /// Joins vertex to the closest neighbour.
        /// </summary>
        /// <param name="vertex">The vertex to join.</param>
        /// /// <param name="type">The type of position join method.</param>
        /// <returns>true when successful, false otherwise.</returns>
        public bool JoinToNearestByDistance(Vertex vertex, JoinPositionType type = JoinPositionType.Middle)
        {
            Debug.Assert(vertex != null);

            Vertex nearest = null;
            float minDistance = float.MaxValue;

            var neighbors = vertex.Neighbors.ToList();

            foreach (var neighbor in neighbors)
            {
                float distance = Vector3.Distance(vertex.Position, neighbor.Position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = neighbor;
                }
            }

            if (nearest != null)
            {
                Vector3 position;
                switch (type)
                {
                    case JoinPositionType.Source:
                        {
                            position = vertex.Position;
                            break;
                        }
                    case JoinPositionType.Target:
                        {
                            position = nearest.Position;
                            break;
                        }
                    default:
                    case JoinPositionType.Middle:
                        {
                            position = Vector3Extension.Mean(vertex.Position, nearest.Position);
                            break;
                        }
                }

                return JoinVertices(vertex, nearest, position);
            }
            return false;
        }


        /// <summary>
        /// Generates mesh info dump to the specified writer.
        /// </summary>
        /// <param name="writer">The target text writer.</param>
        public void Dump(TextWriter writer)
        {
            writer.WriteLine("Mesh dump stats");
            writer.WriteLine(" - {0} triangles", m_Triangles.Count);
            writer.WriteLine(" - {0} vertices", m_Vertices.Count);

            var triangles = new List<int>();
            var neighbors = new List<int>();
            foreach (var v in Vertices)
            {
                triangles.Add(v.Triangles.Count);
                neighbors.Add(v.Neighbors.Count);
            }

            writer.WriteLine(" - vertex triangles [min = {0}, avg = {1}, max = {2}]", triangles.Min(), triangles.Average(), triangles.Max());
            writer.WriteLine(" - vertex neighbors [min = {0}, avg = {1}, max = {2}]", neighbors.Min(), neighbors.Average(), neighbors.Max());
        }

        /// <summary>
        /// Collapses edge specified by vertex1 and vertex2 and moves into newPosition.
        /// </summary>
        /// <param name="vertex1">The source vertex.</param>
        /// <param name="vertex2">The source vertex.</param>
        /// <param name="newPosition">The position of joined vertex.</param>
        /// <returns>true when successful, false otherwise.</returns>
        /// <remarks>The vertex2 is removed and replaced with vertex1.</remarks>
        public bool JoinVertices(Vertex vertex1, Vertex vertex2, Vector3 newPosition)
        {
            Debug.Assert(vertex1 != null);
            Debug.Assert(vertex2 != null);

            var neighbors = vertex2.Neighbors.ToList();

            switch (neighbors.Count)
            {
                case 1:
                    {
                        var other = neighbors.ElementAt(0);
                        other.RemoveNeighbor(vertex2);
                        RemoveVertex(vertex2);
                        return true;
                    }
                case 2:
                    {
                        var first = neighbors.ElementAt(0);
                        var second = neighbors.ElementAt(1);

                        var oldTriangles = vertex2.Triangles;

                        foreach (var triangle in oldTriangles)
                        {
                            RemoveTriangle(triangle);
                        }

                        first.Replace(vertex2, second);
                        second.Replace(vertex2, first);

                        RemoveVertex(vertex2);
                        return true;
                    }
            }

            // Get neighbors of removed vertex
            //var neighbors = vertex2.Neighbors.ToList();
            // Get triangles of removed vertex
            var triangles = vertex2.Triangles.ToList();

            // Triangles that contains removed vertex
            var toRemove = new List<Triangle>();
            
            // Replace vertices and get triangles to remove.
            foreach (var t in triangles)
            {
                if (!t.Replace(vertex2, vertex1))
                {
                    toRemove.Add(t);
                }
            }

            // Notify neighbors that vertex2 passed away.
            foreach (var n in neighbors)
            {
                //n.Replace(vertex2, vertex1);
                n.RemoveNeighbor(vertex2);
                n.AddNeighbor(vertex1);

                foreach (var t in toRemove)
                {
                    n.RemoveTriangle(t);
                }
            }

            // Remove triangles
            foreach (var t in toRemove)
            {
                bool result = RemoveTriangle(t);
                Debug.Assert(result);
            }

            // And finally remove vertex.
            RemoveVertex(vertex2);

            return true;
        }

        /// <summary>
        /// Removes degenerated triangles.
        /// </summary>
        public void RemoveDegenerated()
        {
            var triangles = m_Triangles.ToList();

            foreach (var triangle in triangles)
            {
                if (Math.Abs(triangle.Area) <= m_DegeneratedEpsilon)
                {
                    Vector3 center = triangle.Center;
                    
                    // Join vertices of the triangle.
                    JoinVertices(triangle.Vertex1, triangle.Vertex2, center);
                    JoinVertices(triangle.Vertex1, triangle.Vertex3, center);
                    
                    // And remove degenrated triangle.
#if false
                    RemoveTriangle(triangle);
#endif
                }
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets vertices set.
        /// </summary>
        public HashSet<Vertex> Vertices
        {
            get { return m_Vertices; }
        }

        /// <summary>
        /// Gets triangles set.
        /// </summary>
        public HashSet<Triangle> Triangles
        {
            get { return m_Triangles; }
        }

        /// <summary>
        /// Gets or sets epsilon value used to collapse degenerated triangles.
        /// </summary>
        public double DegeneratedEpsilon
        {
            get { return m_DegeneratedEpsilon; }
            set { m_DegeneratedEpsilon = value; }
        }

        public bool Empty
        {
            get { return m_Triangles.Count == 0 || m_Vertices.Count == 0; }
        }
        #endregion

        #region Fields
        private HashSet<Vertex> m_Vertices;
        private HashSet<Triangle> m_Triangles;
        private double m_DegeneratedEpsilon = 1e-5;
        #endregion
    }
}
