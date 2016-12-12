using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Terremesh.IDE.Mesh
{
    /// <summary>
    /// Writes mesh to the specified output stream.
    /// </summary>
    class MeshWriter
    {
        #region Fields
        private StreamWriter m_Writer;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates instance of the MeshWriter class.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public MeshWriter(Stream output)
        {
            m_Writer = new StreamWriter(output);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Writes raw mesh to stream.
        /// </summary>
        /// <param name="vertices">The list of the vertices.</param>
        /// <param name="indices">The list of the indices.</param>
        /// <param name="progress">The optional progress listener.</param>
        public void WriteMesh(ref List<Vertex> vertices, ref List<int> indices, IProgressListener progress = null)
        {
            long current = 0;
            long total = vertices.Count + indices.Count;

            progress.OnStart("Mesh writing started...");

            foreach (var vertex in vertices)
            {
                m_Writer.WriteLine(string.Format(
                    CultureInfo.InvariantCulture,
                    "v {0} {1} {2}",
                    vertex.Position.X.ToString("0.000000", CultureInfo.InvariantCulture),
                    vertex.Position.Y.ToString("0.000000", CultureInfo.InvariantCulture),
                    vertex.Position.Z.ToString("0.000000", CultureInfo.InvariantCulture)));

                ++current;

                if (progress != null)
                {
                    progress.OnProgress(current, total);
                }
            }

            int count = 0;
            foreach (var index in indices)
            {
                switch (count % 3)
                {
                    case 0:
                        {
                            m_Writer.Write(string.Format(CultureInfo.InvariantCulture, "f {0} ", index + 1));
                            break;
                        }
                    case 1:
                        {
                            m_Writer.Write(string.Format(CultureInfo.InvariantCulture, "{0} ", index + 1));
                            break;
                        }
                    case 2:
                        {
                            m_Writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}", index + 1));
                            break;
                        }
                    default:
                        {
                            Trace.WriteLine("Not supported");
                            break;
                        }
                }

                ++count;
                ++current;

                if (progress != null)
                {
                    progress.OnProgress(current, total);
                }
            }

            if (progress != null)
            {
                progress.OnComplete("Mesh writing complete");
            }

            m_Writer.Close();
        }
        #endregion
    }
}
