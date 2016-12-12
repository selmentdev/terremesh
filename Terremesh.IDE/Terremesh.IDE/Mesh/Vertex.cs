using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Terremesh.IDE.Mesh
{
    /// <summary>
    /// Represents vertex.
    /// </summary>
    class Vertex
    {
        /// <summary>
        /// Create a new vertex.
        /// </summary>
        public Vertex()
        {
        }

        /// <summary>
        /// Create a new vertex using position.
        /// </summary>
        /// <param name="position">The vertex position.</param>
        public Vertex(Vector3 position)
        {
            m_Position = position;
        }

        #region Properties
        /// <summary>
        /// Vertex position.
        /// </summary>
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
        #endregion

        private Vector3 m_Position;
        private Vector3 m_Normal;
    }
}
