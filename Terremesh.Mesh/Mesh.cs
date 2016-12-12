using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Terremesh.Mesh
{
    public class MeshReader
    {
        public MeshReader(Stream input)
        {
            m_Reader = new StreamReader(input);
        }

        public virtual void Read(out Mesh mesh, IProgressListener listener = null)
        {
            mesh = new Mesh();

            Regex r = new Regex(@" |//");
            NumberStyles numberStyle = NumberStyles.Float;
            IFormatProvider numberFormatProvider = CultureInfo.InvariantCulture;

            if (listener != null)
            {
                listener.OnStarted("Load mesh");
            }

            int indicesCount = 0;

            while (m_Reader.Peek() >= 0)
            {
                string line = m_Reader.ReadLine();

                String[] elements = r.Split(line);

                // List<Vertex> normals;
                switch (elements[0])
                {
                    case "v":
                        {
                            float x, y, z;
                            if (
                                float.TryParse(elements[1], numberStyle, numberFormatProvider, out x) &&
                                float.TryParse(elements[2], numberStyle, numberFormatProvider, out y) &&
                                float.TryParse(elements[3], numberStyle, numberFormatProvider, out z)
                                )
                            {
                                mesh.Vertices.Add(++indicesCount, new Vertex(new Vector3(x, y, z)));
                            }
                            else
                            {
                                Trace.WriteLine(line);
                            }
                            break;
                        }
                    case "f":
                        {
                            int f1, f2, f3;
                            int f1Index = 1;
                            int f2Index = 2;
                            int f3Index = 3;

                            switch (elements.Length)
                            {
                                case 7:
                                    {
                                        //normal indexes in elements[2],elements[4] and elements[6] are ignored;
                                        f1Index = 1;
                                        f2Index = 3;
                                        f3Index = 5;

                                        goto case 4;
                                    }
                                case 4:
                                    {
                                        if (
                                            int.TryParse(elements[f1Index], numberStyle, numberFormatProvider, out f1) &&
                                            int.TryParse(elements[f2Index], numberStyle, numberFormatProvider, out f2) &&
                                            int.TryParse(elements[f3Index], numberStyle, numberFormatProvider, out f3)
                                            )
                                        {
                                            mesh.Faces.Add(new Triangle(f1, f2, f3));
                                        }
                                        else
                                        {
                                            Trace.WriteLine(line);
                                            throw new NotImplementedException("Only triangles are Implemented. Faces in file are not a triangles.That is bad:(");
                                        }
                                    }
                                    break;
                                default:
                                    Trace.WriteLine("Invalid number of components");
                                    break;
                            }
                            break;
                        }
                    case "vn":
                        {
                            break;
                        }
                    default:
                        Trace.WriteLine("Unknown obj specifier.");
                        break;

                }

                if (listener != null)
                {
                    listener.OnStep(
                        m_Reader.BaseStream.Position,
                        m_Reader.BaseStream.Length);
                }
            }

            if (listener != null)
            {
                listener.OnComplete("Mesh loaded successfully");
            }

            mesh.Invalidate(listener, Mesh.InvalidateFlags.Faces);
        }

        private StreamReader m_Reader;
    }

    public class MeshWriter
    {
        public MeshWriter(Stream outputStream)
        {
            m_Writer = new StreamWriter(outputStream);
        }

        public void WriteMesh(ref Mesh mesh, IProgressListener listener = null)
        {
            long current = 0;
            long total = mesh.Vertices.Count + mesh.Faces.Count;

            listener.OnStarted("Mesh write");

            foreach (var vertex in mesh.Vertices)
            {
                m_Writer.WriteLine(string.Format(
                    CultureInfo.InvariantCulture,
                    "v {0} {1} {2}",
                    vertex.Value.Position.X.ToString("0.000000", CultureInfo.InvariantCulture),
                    vertex.Value.Position.Y.ToString("0.000000", CultureInfo.InvariantCulture),
                    vertex.Value.Position.Z.ToString("0.000000", CultureInfo.InvariantCulture)));

                ++current;

                if (listener != null)
                {
                    listener.OnStep(current, total);
                }
            }

            int count = 0;
            foreach (var face in mesh.Faces)
            {
                m_Writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "f {0} {1} {2}",
                    face[0], face[1], face[2]));

                ++count;
                ++current;

                if (listener != null)
                {
                    listener.OnStep(current, total);
                }
            }

            if (listener != null)
            {
                listener.OnComplete("Mesh write");
            }

            m_Writer.Close();
        }

        private StreamWriter m_Writer;
    }

    public class Mesh
    {
        [Flags()]
        public enum InvalidateFlags
        {
            Faces,
            Vertices,
            All = Faces | Vertices,
        }

        /// <summary>
        /// Updates vertex normals and face planes.
        /// </summary>
        public void Invalidate(IProgressListener listener = null, InvalidateFlags invalidate = InvalidateFlags.All)
        {
            if (invalidate.HasFlag(InvalidateFlags.Faces))
            {
                if (listener != null)
                {
                    listener.OnStarted("Invalidating mesh faces");
                }

                foreach (var face in m_Faces)
                {
                    var v0 = m_Vertices[face[0]].Position;
                    var v1 = m_Vertices[face[1]].Position;
                    var v2 = m_Vertices[face[2]].Position;
                    face.Plane = new Plane(v0, v1, v2);

                }

                if (listener != null)
                {
                    listener.OnComplete("Invalidating mesh faces");
                }
            }

            if (invalidate.HasFlag(InvalidateFlags.Vertices))
            {
                if (listener != null)
                {
                    listener.OnStarted("Invalidating mesh vertices");
                }

                int i = 0;
                foreach (var vertex in m_Vertices)
                {
                    foreach (var face in m_Faces)
                    {
                        if (face.HasVertex(vertex.Key))
                        {
                            vertex.Value.Normal += face.Plane.Normal;
                        }
                    }
                    if (listener != null)
                    {
                        listener.OnStep(++i, m_Vertices.Count);
                    }
                    vertex.Value.Normal.Normalize();
                }
                if (listener != null)
                {
                    listener.OnComplete("Invalidating mesh vertices");
                }
            }
        }

        public Dictionary<int, Vertex> Vertices
        {
            get { return m_Vertices; }
            set { m_Vertices = value; }
        }

        public List<Triangle> Faces
        {
            get { return m_Faces; }
            set { m_Faces = value; }
        }

        private Dictionary<int, Vertex> m_Vertices = new Dictionary<int, Vertex>();
        private List<Triangle> m_Faces = new List<Triangle>();
    }
}
