using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Terremesh.Remeshing
{
    /// <summary>
    /// Represents triangle.
    /// </summary>
    public class Triangle : IComparable<Triangle>
    {
        /// <summary>
        /// Represents edge data.
        /// </summary>
        public class Edge
        {
            #region Properties
            /// <summary>
            /// Gets or sets edge removal cost.
            /// </summary>
            public double Cost
            {
                get { return m_Cost; }
                set { m_Cost = value; }
            }

            /// <summary>
            /// Gets or sets dditional tag.
            /// </summary>
            public object Tag
            {
                get { return m_Tag; }
                set { m_Tag = value; }
            }
            #endregion

            #region Fields
            private double m_Cost;
            private object m_Tag;
            #endregion
        }

        #region Constructors.
        /// <summary>
        /// Creates a new instance of the Triangle class.
        /// </summary>
        /// <param name="v1">The source vertex.</param>
        /// <param name="v2">The source vertex.</param>
        /// <param name="v3">The source vertex.</param>
        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;

            Vector3 pos1 = v1.Position;
            Vector3 pos2 = v2.Position;
            Vector3 pos3 = v3.Position;

            m_Normal = ComputeNormal(pos1, pos2, pos3);
            m_Center = ComputeCenter(pos1, pos2, pos3);
            m_Area = ComputeArea(pos1, pos2, pos3);

            Vertex1.AddTriangle(this);
            Vertex2.AddTriangle(this);
            Vertex3.AddTriangle(this);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Computes center of the triangle represented by three vertices.
        /// </summary>
        /// <param name="v1">The source position.</param>
        /// <param name="v2">The source position.</param>
        /// <param name="v3">The source position.</param>
        /// <returns>The center position.</returns>
        public static Vector3 ComputeCenter(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 result = v1 + v2 + v3;
            result *= 1.0f / 3.0f;
            return result;
        }

        /// <summary>
        /// Computes normal of the triangle.
        /// </summary>
        /// <param name="v1">The source position.</param>
        /// <param name="v2">The source position.</param>
        /// <param name="v3">The source position.</param>
        /// <returns>The computed normal.</returns>
        public static Vector3 ComputeNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 diff1 = v2 - v1;
            Vector3 diff2 = v3 - v2;
            Vector3 cross;
            Vector3.Cross(ref diff1, ref diff2, out cross);

            return Vector3.Normalize(cross);
        }

        /// <summary>
        /// Computes triangle area.
        /// </summary>
        /// <param name="v1">The source position.</param>
        /// <param name="v2">The source position.</param>
        /// <param name="v3">The source position.</param>
        /// <returns>The triangle area.</returns>
        public static float ComputeArea(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            var diff1 = v2 - v1;
            var diff2 = v3 - v1;
            var cross = Vector3.Cross(diff1, diff2);
            return cross.Length() * 0.5f;
        }

        /// <summary>
        /// Computes triangle perimeter.
        /// </summary>
        /// <param name="v1">The source position.</param>
        /// <param name="v2">The source position.</param>
        /// <param name="v3">The source position.</param>
        /// <returns>The triangle perimeter.</returns>
        public static float ComputePerimeter(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            var d1 = v1 - v2;
            var d2 = v2 - v3;
            var d3 = v3 - v1;

            return d1.Length() + d2.Length() + d3.Length();
        }

        /// <summary>
        /// Updates geometric data.
        /// </summary>
        public void UpdateGeometricData()
        {
            m_Area = ComputeArea(m_Vertex1.Position, m_Vertex2.Position, m_Vertex3.Position);
            m_Perimeter = ComputePerimeter(m_Vertex1.Position, m_Vertex2.Position, m_Vertex3.Position);
        }

        /// <summary>
        /// Computes angle for specifie
        /// </summary>
        /// <param name="vertex">The triangle corner vertex.</param>
        /// <returns>The angle in the triangle corner.</returns>
        public float ComputeCornerAngle(Vertex vertex)
        {
            Vector3 a = Vector3.UnitZ;
            Vector3 b = Vector3.UnitX;
            if (m_Vertex1 == vertex)
            {
                a = m_Vertex2.Position - m_Vertex1.Position;
                b = m_Vertex3.Position - m_Vertex1.Position;
            }
            else if (m_Vertex2 == vertex)
            {
                a = m_Vertex1.Position - m_Vertex2.Position;
                b = m_Vertex3.Position - m_Vertex2.Position;
            }
            else if (m_Vertex3 == vertex)
            {
                a = m_Vertex1.Position - m_Vertex3.Position;
                b = m_Vertex2.Position - m_Vertex3.Position;
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "Failed to compute angle");
            }

            float cosAngle = (Vector3.Dot(a, b)) / (a.Length() * b.Length());

            return (float)Math.Acos((double)cosAngle);
        }

        /// <summary>
        /// Determines whether triangle contains specified vertex.
        /// </summary>
        /// <param name="vertex">The vertex to check.</param>
        /// <returns>True when successful, false otherwise</returns>
        public bool Contains(Vertex vertex)
        {
            if (vertex == m_Vertex1)
            {
                return true;
            }
            if (vertex == m_Vertex2)
            {
                return true;
            }
            if (vertex == m_Vertex3)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Replaces two vertices.
        /// </summary>
        /// <param name="previousVertex">The vertex to replace.</param>
        /// <param name="newVertex">The new vertex.</param>
        /// <returns>true when vertex was replaced, false otherwise.</returns>
        public bool Replace(Vertex previousVertex, Vertex newVertex)
        {
            if (Contains(previousVertex) && Contains(newVertex))
            {
                return false;
            }

            if (Vertex1 == previousVertex && Vertex2 == previousVertex)
            {
                Vertex1.RemoveTriangle(this);
                return false;
            }
            if (Vertex2 == previousVertex && Vertex3 == previousVertex)
            {
                Vertex1.RemoveTriangle(this);
                return false;
            }
            if (Vertex1 == previousVertex && Vertex3 == previousVertex)
            {
                Vertex1.RemoveTriangle(this);
                return false;
            }

            if (Vertex1 == previousVertex)
            {
                Vertex1 = newVertex;
                Vertex1.AddTriangle(this);
            }
            else if (Vertex2 == previousVertex)
            {
                Vertex2 = newVertex;
                Vertex2.AddTriangle(this);
            }
            else if (Vertex3 == previousVertex)
            {
                Vertex3 = newVertex;
                Vertex3.AddTriangle(this);
            }
            else
            {
                return false;
            }

            Vertex1.Replace(previousVertex, newVertex);
            Vertex2.Replace(previousVertex, newVertex);
            Vertex3.Replace(previousVertex, newVertex);

            m_ReplacedBefore = true;
            return true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets normal vector.
        /// </summary>
        public Vector3 Normal
        {
            get { return m_Normal; }
            set { m_Normal = value; }
        }

        /// <summary>
        /// Gets center vector.
        /// </summary>
        public Vector3 Center
        {
            get { return m_Center; }
            set { m_Center = value; }
        }

        /// <summary>
        /// Gets triangle area.
        /// </summary>
        public float Area
        {
            get { return m_Area; }
        }

        /// <summary>
        /// Gets triangle perimeter value.
        /// </summary>
        public float Perimeter
        {
            get { return m_Perimeter; }
        }

        /// <summary>
        /// Gets first vertex.
        /// </summary>
        public Vertex Vertex1
        {
            get { return m_Vertex1; }
            set { m_Vertex1 = value; }
        }

        /// <summary>
        /// Gets second vertex.
        /// </summary>
        public Vertex Vertex2
        {
            get { return m_Vertex2; }
            set { m_Vertex2 = value; }
        }

        /// <summary>
        /// Gets third vertex.
        /// </summary>
        public Vertex Vertex3
        {
            get { return m_Vertex3; }
            set { m_Vertex3 = value; }
        }

        /// <summary>
        /// Plane.
        /// </summary>
        public Plane Plane
        {
            get
            {
                float w;
                Vector3 normal = Normal;
                Vector3 center = Center;
                Vector3.Dot(ref center, ref normal, out w);

                return new Plane(Normal, w);
            }
            set { Normal = value.Normal; }
        }

        /// <summary>
        /// Gets value indicating whether triangle was replaced before.
        /// </summary>
        public bool ReplacedBefore
        {
            get { return m_ReplacedBefore; }
        }

        /// <summary>
        /// Gets edge data between Vertex1 and Vertex2.
        /// </summary>
        public Edge Edge12
        {
            get { return m_Edge12; }
        }

        /// <summary>
        /// Gets edge data between Vertex2 and Vertex3.
        /// </summary>
        public Edge Edge23
        {
            get { return m_Edge23; }
        }

        /// <summary>
        /// Gets edge data between Vertex3 and Vertex1.
        /// </summary>
        public Edge Edge31
        {
            get { return m_Edge31; }
        }

        /// <summary>
        /// Gets or sets additional tag object.
        /// </summary>
        public object Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }

        /// <summary>
        /// Gets or sets edge removal cost.
        /// </summary>
        public double Cost
        {
            get { return m_Cost; }
            set { m_Cost = value; }
        }
        #endregion

        #region IComparable<> implementation
        int IComparable<Triangle>.CompareTo(Triangle t)
        {
            return Comparer<double>.Default.Compare(m_Cost, t.m_Cost);
        }
        #endregion

        #region Fields
        private Vector3 m_Normal;
        private Vector3 m_Center;
        private float m_Area = 0.0f;
        private float m_Perimeter = 0.0f;
        internal Vertex m_Vertex1;
        internal Vertex m_Vertex2;
        internal Vertex m_Vertex3;
        private double m_Cost = 0.0;
        internal Edge m_Edge12 = new Edge();
        internal Edge m_Edge23 = new Edge();
        internal Edge m_Edge31 = new Edge();
        private bool m_ReplacedBefore = false;
        private object m_Tag = null;
        #endregion
    }
}
