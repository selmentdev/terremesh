using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Terremesh.Remeshing
{
    public static class Vector3Extension
    {
        public static bool IsNaN(this Vector3 vector)
        {
            return float.IsNaN(vector.X) || float.IsNaN(vector.Y) || float.IsNaN(vector.Z);
        }

        public static Vector3 Mean(Vector3 vector1, Vector3 vector2)
        {
            Vector3 result = vector1 + vector2;
            Vector3.Multiply(ref result, 0.5f, out result);
            return result;
        }
    }

    /// <summary>
    /// Represents vertex.
    /// </summary>
    public class Vertex : IComparable<Vertex>
    {
        /// <summary>
        /// Create a new vertex.
        /// </summary>
        public Vertex()
        {
            m_Neighbors = new HashSet<Vertex>();
            m_Triangles = new HashSet<Triangle>();
        }

        /// <summary>
        /// Create a new vertex using position.
        /// </summary>
        /// <param name="position">The vertex position.</param>
        public Vertex(Vector3 position)
        {
            m_Neighbors = new HashSet<Vertex>();
            m_Triangles = new HashSet<Triangle>();
            m_Position = position;
        }

        /// <summary>
        /// Normalizes the current vertex.
        /// </summary>
        internal void Normalize()
        {
            Vector3 normal = Vector3.Zero;

            if (m_Triangles.Count != 0)
            {
                Vector3 v;

                foreach (Triangle t in m_Triangles)
                {
                    v = t.Normal;
                    Vector3.Add(ref normal, ref v, out normal);
                }
            }
            Vector3.Normalize(ref normal, out m_Normal);
        }

        /// <summary>
        /// Adds neighbor vertex.
        /// </summary>
        /// <param name="vertex">The vertex to add.</param>
        public void AddNeighbor(Vertex vertex)
        {
            if (this != vertex)
            {
                m_Neighbors.Add(vertex);

                if (!vertex.Contains(this))
                {
                    vertex.AddNeighbor(this);
                }

                Normalize();
            }
        }

        /// <summary>
        /// Removes vertex from neighbors.
        /// </summary>
        /// <param name="vertex">The vertex to remove</param>
        /// <returns>true when successful, false otherwise.</returns>
        public bool RemoveNeighbor(Vertex vertex)
        {
            if (Contains(vertex))
            {
                m_Neighbors.Remove(vertex);

                if (vertex.Contains(this))
                {
                    vertex.RemoveNeighbor(this);
                }

                var triangles = GetSharedTriangles(vertex);

                foreach (Triangle t in triangles)
                {
                    RemoveTriangle(t);
                }

                if (m_Neighbors.Count != 0)
                {
                    Normalize();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds triangle.
        /// </summary>
        /// <param name="triangle">The triangle to add.</param>
        public void AddTriangle(Triangle triangle)
        {
            if (triangle.Contains(this))
            {
                m_Triangles.Add(triangle);

                Vertex v1 = triangle.Vertex1;
                Vertex v2 = triangle.Vertex2;
                Vertex v3 = triangle.Vertex3;

                v1.AddNeighbor(v2);
                v1.AddNeighbor(v3);
                v2.AddNeighbor(v1);
                v2.AddNeighbor(v3);
                v3.AddNeighbor(v1);
                v3.AddNeighbor(v2);

                if (!v1.Contains(triangle))
                {
                    v1.AddTriangle(triangle);
                }
                if (!v2.Contains(triangle))
                {
                    v2.AddTriangle(triangle);
                }
                if (!v3.Contains(triangle))
                {
                    v3.AddTriangle(triangle);
                }
            }
        }

        /// <summary>
        /// Removes triangle.
        /// </summary>
        /// <param name="triangle">The triangle to remove</param>
        /// <returns>true when successful, false otherwise</returns>
        public bool RemoveTriangle(Triangle triangle)
        {
            if (m_Triangles.Contains(triangle))
            {
                m_Triangles.Remove(triangle);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Replaces two vertices.
        /// </summary>
        /// <param name="previous">The vertex to remove.</param>
        /// <param name="vertex">The vertex to replace</param>
        /// <returns>true when successful, false otherwise</returns>
        public bool Replace(Vertex previous, Vertex vertex)
        {
            if (RemoveNeighbor(previous))
            {
                AddNeighbor(vertex);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether vertex contains another vertex.
        /// </summary>
        /// <param name="vertex">The vertex to check</param>
        /// <returns>true when successful, false otherwise</returns>
        public bool Contains(Vertex vertex)
        {
            return m_Neighbors.Contains(vertex);
        }

        /// <summary>
        /// Determines whether vertex contains another triangle.
        /// </summary>
        /// <param name="triangle">The triangle to check.</param>
        /// <returns>true when successful, false otherwise</returns>
        public bool Contains(Triangle triangle)
        {
            return m_Triangles.Contains(triangle);
        }

        /// <summary>
        /// Gets list of the shared triangles.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<Triangle> GetSharedTriangles(Vertex vertex)
        {
            List<Triangle> triangles = new List<Triangle>();
            foreach (Triangle t in m_Triangles)
            {
                if (t.Contains(vertex))
                {
                    triangles.Add(t);
                }
            }

            HashSet<Triangle> list = vertex.Triangles;
            foreach (Triangle t in list)
            {
                if (t.Contains(this))
                {
                    triangles.Add(t);
                }
            }

            return triangles;
        }

#region Properties
        /// <summary>
        /// Vertex position.
        /// </summary>
        [Category("Structure"), Description("Vertex position")]
        public Vector3 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        /// <summary>
        /// Vertex normal.
        /// </summary>
        [Category("Structure"), Description("Vertex normal vector")]
        public Vector3 Normal
        {
            get
            {
                return m_Normal;
            }
            set
            {
                m_Normal = value;
            }
        }

        /// <summary>
        /// Index
        /// </summary>
        [Category("Structure"), Description("Vertex index")]
        public int Index
        {
            get
            {
                return m_Index;
            }
            set
            {
                m_Index = value;
            }
        }

        /// <summary>
        /// Tag
        /// </summary>
        [Category("Remeshing"), Description("Additional tag object, used to tag vertices")]
        public object Tag
        {
            get
            {
                return m_Tag;
            }
            set
            {
                m_Tag = value;
            }
        }
        
        /// <summary>
        /// Adjacent triangles.
        /// </summary>
        [Browsable(false)]
        public HashSet<Triangle> Triangles
        {
            get
            {
                return m_Triangles;
            }
        }

        /// <summary>
        /// Neighbour vertices.
        /// </summary>
        [Browsable(false)]
        public HashSet<Vertex> Neighbors
        {
            //TODO: better make copy?
            get
            {
                return m_Neighbors;
            }
        }

        /// <summary>
        /// Removal cost.
        /// </summary>
        [Category("Remeshing"), Description("The cost of vertex removal")]
        public double Cost
        {
            get
            {
                return m_Cost;
            }
            set
            {
                m_Cost = value;
            }
        }
#endregion

        int IComparable<Vertex>.CompareTo(Vertex vertex)
        {
            if (m_Cost < vertex.m_Cost)
            {
                return -1;
            }
            return 0;
        }

        private Vector3 m_Position;
        private Vector3 m_Normal;
        private int m_Index;
        private object m_Tag;
        private HashSet<Triangle> m_Triangles;
        private HashSet<Vertex> m_Neighbors;
        private double m_Cost;
    }
}
