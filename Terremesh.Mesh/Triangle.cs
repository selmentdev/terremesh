using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Terremesh.Mesh
{
    public class Triangle
    {
        public Triangle(int v1, int v2, int v3)
        {
            m_Vertex1 = v1;
            m_Vertex2 = v2;
            m_Vertex3 = v3;
        }

        public Plane Plane
        {
            get { return m_Plane; }
            set { m_Plane = value; }
        }

        public int this[int index]
        {
            get { return GetVertex(index); }
            set { SetVertex(index, value); }
        }

        public bool HasVertex(int vertexId)
        {
            return (m_Vertex1 == vertexId) || (m_Vertex2 == vertexId) || (m_Vertex3 == vertexId);
        }

        private void SetVertex(int index, int vertexId)
        {
            switch (index)
            {
                case 0:
                    {
                        m_Vertex1 = vertexId;
                        break;
                    }
                case 1:
                    {
                        m_Vertex2 = vertexId;
                        break;
                    }
                case 2:
                    {
                        m_Vertex3 = vertexId;
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException("index");
                    }
            }
        }

        private int GetVertex(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        return m_Vertex1;
                    }
                case 1:
                    {
                        return m_Vertex2;
                    }
                case 2:
                    {
                        return m_Vertex3;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException("index");
                    }
            }
        }

        private Plane m_Plane;
        private int m_Vertex1;
        private int m_Vertex2;
        private int m_Vertex3;
    }
}
