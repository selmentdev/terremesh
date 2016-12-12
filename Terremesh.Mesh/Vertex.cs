using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Terremesh.Mesh
{
    public class Vertex
    {
        public Vertex(Vector3 position)
            : this(position, Vector3.Zero)
        {
        }

        public Vertex(Vector3 position, Vector3 normal)
        {
            m_Position = position;
            m_Normal = normal;
        }

        private Vector3 m_Position;
        private Vector3 m_Normal;

        public Vector3 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Vector3 Normal
        {
            get { return m_Normal; }
            set { m_Normal = value; }
        }
    }
}
