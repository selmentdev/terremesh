using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sanford.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terremesh.Remeshing.Quadric
{
    public struct Vertex
    {
        public Vector3 Position;
    }

    public struct Face
    {
        public int this[int index]
        {
            get
            {
                return GetVertex(index);
            }
            set
            {
                SetVertex(index, value);
            }
        }

        public int GetVertex(int index)
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
                default:
                    {
                        return m_Vertex3;
                    }
            }
        }

        public void SetVertex(int index, int vertex)
        {
            switch (index)
            {
                case 0:
                    {
                        m_Vertex1 = vertex;
                        break;
                    }
                case 1:
                    {
                        m_Vertex2 = vertex;
                        break;
                    }
                default:
                    {
                        m_Vertex3 = vertex;
                        break;
                    }
            }
        }

        public Plane Plane
        {
            get { return m_Plane; }
        }

        private Plane m_Plane;
        private int m_Vertex1;
        private int m_Vertex2;
        private int m_Vertex3;
    }

    public struct Split
    {
        public Vertex V1;
        public Vertex V2;
        public Vertex VF;
    }

    public struct Pair<T, U>
    {
        public T First;
        public U Second;
    }

    public struct VertexPair
    {
        public int First;
        public int Second;

        public bool Equals(VertexPair pair)
        {
            return this.First == pair.First && this.Second == pair.Second;
        }
    }

    public struct ErrorMetric
    {
        public ErrorMetric(Plane plane)
        {
            float aa = plane.Normal.X * plane.Normal.X;
            float bb = plane.Normal.Y * plane.Normal.Y;
            float cc = plane.Normal.Z * plane.Normal.Z;
            float dd = plane.D * plane.D;
            float ab = plane.Normal.X * plane.Normal.Y;
            float ac = plane.Normal.X * plane.Normal.Z;
            float ad = plane.Normal.X * plane.D;
            float bc = plane.Normal.Y * plane.Normal.Z;
            float bd = plane.Normal.Y * plane.D;
            float cd = plane.Normal.Z * plane.D;

            m_Matrix = new Matrix(
                aa, ab, ac, ad,
                ab, bb, bc, bd,
                ac, bc, cc, cd,
                ad, bd, cd, dd);
        }

        public double Evaluate(Vector3 point)
        {
            double value = m_Matrix.M11 * point.X * point.X +
                   2 * m_Matrix.M12 * point.X * point.Y +
                   2 * m_Matrix.M13 * point.X * point.Z +
                   2 * m_Matrix.M14 * point.X +
                   m_Matrix.M22 * point.Y * point.Y +
                   2 * m_Matrix.M23 * point.Y * point.Z +
                   2 * m_Matrix.M24 * point.Y +
                   m_Matrix.M33 * point.Z * point.Z +
                   2 * m_Matrix.M34 * point.Z
                   + m_Matrix.M44;
            return value;
        }

        public static ErrorMetric Empty
        {
            get { return m_Empty; }
        }

        private static readonly ErrorMetric m_Empty = new ErrorMetric() { m_Matrix = Matrix.Identity };

        public Matrix Matrix
        {
            get { return m_Matrix; }
            set { m_Matrix = value; }
        }

        public static ErrorMetric operator + (ErrorMetric e1, ErrorMetric e2)
        {
            ErrorMetric result = new ErrorMetric();

            result.m_Matrix = e1.m_Matrix + e2.m_Matrix;

            return result;
        }

        public static ErrorMetric operator -(ErrorMetric e1, ErrorMetric e2)
        {
            ErrorMetric result = new ErrorMetric();
            result.m_Matrix = e1.m_Matrix - e2.m_Matrix;
            return result;
        }

        private Matrix m_Matrix;
    }

    public class QuadricErrorMetric
    {

        public int FacesCount
        {
            get { return m_Faces.Count; }
        }

        public void Load(System.IO.Stream stream)
        {
            var reader = new MeshReader(stream);

            //reader.ReadMesh(
        }

        public void Save(ref Mesh mesh)
        {
        }

        private void Contract(int facesCount)
        {
            SelectValidPairs();

            int afterContract = m_Faces.Count - facesCount;

            while (m_Faces.Count > afterContract)
            {
                double minError = (double)int.MaxValue;

                //KeyValuePair<VertexPair, double> min;
                VertexPair minPair = new VertexPair() { First = 0, Second = 0 };

                foreach (KeyValuePair<VertexPair, double> e in m_Errors)
                {
                    if (e.Value < minError)
                    {
                        minError = e.Value;
                        minPair = e.Key;
                    }
                }

                Vector3 error;
                ComputeError(minPair.First, minPair.Second, out error);

                Split split = new Split();
                split.V1.Position = m_Vertices[minPair.First].Position;
                split.V2.Position = m_Vertices[minPair.Second].Position;
                split.VF.Position = error;
                m_Splits.PushBack(split);

                var vertex = m_Vertices[minPair.First];
                vertex.Position = error;

                m_Quadrics[minPair.First] = m_Quadrics[minPair.First] + m_Quadrics[minPair.Second];

                for (int i = m_Faces.Count; i != 0;)
                {
                    var face = m_Faces[i];

                    for (int j = 0; j < 3; ++j)
                    {
                        if (face[j] == minPair.Second)
                        {
                            if (face[0] == minPair.First || face[1] == minPair.First || face[2] == minPair.First)
                            {
                                m_Faces.Remove(face);
                            }
                            else
                            {
                                face[j] = minPair.First;
                            }
                            --i;
                            break;
                        }
                        else if (j == 2)
                        {
                            --i;
                        }
                    }
                }

                m_Vertices.Remove(minPair.Second);

                KeyValuePair<VertexPair, double> pair;

                for (int iter = m_Errors.Count; iter != 0;)
                {
                    pair = m_Errors.ElementAt(iter);

                    if (pair.Key.First == minPair.Second && pair.Key.Second != minPair.First)
                    {
                        m_Errors.Remove(m_Errors.ElementAt(iter).Key);

                        m_Errors.Add(
                            new VertexPair() { First = Math.Min(minPair.First, pair.Key.Second), Second = Math.Max(minPair.First, pair.Key.Second) },
                            0.0);
                        --iter;
                    }
                    else if (pair.Key.Second == minPair.Second && pair.Key.First != minPair.First)
                    {
                        m_Errors.Remove(m_Errors.ElementAt(iter).Key);

                        m_Errors.Add(
                            new VertexPair() { First = Math.Min(minPair.First, pair.Key.First), Second = Math.Max(minPair.First, pair.Key.First) },
                            0.0);
                        --iter;
                    }
                    else
                    {
                        --iter;
                    }
                }

                m_Errors.Remove(minPair);

                foreach (var e in m_Errors)
                {
                    var p = e.Key;
                    if (p.First == minPair.First)
                    {
                        m_Errors[p] = ComputeError(minPair.First, p.Second);
                    }

                    if (p.Second == minPair.First)
                    {
                        m_Errors[p] = ComputeError(minPair.First, p.First);
                    }
                }

            }
        }

        private void ComputeInitialQuadrics()
        {
            for (int i = 0; i < m_Vertices.Count; ++i)
            {
                m_Quadrics.Add(i, ErrorMetric.Empty);
            }

            for (int i = 0; i < m_Faces.Count; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    m_Quadrics[m_Faces.ElementAt(i).GetVertex(j)] += new ErrorMetric(m_Faces.ElementAt(i).Plane);
                }
            }
        }

        private void SelectValidPairs(double level = 0.12)
        {
            for (int i = 0; i < m_Faces.Count; ++i)
            {
                var face = m_Faces.ElementAt(i);

                VertexPair pair;
                pair.First = Math.Min(face[0], face[1]);
                pair.Second = Math.Max(face[0], face[1]);

                if (!m_Errors.ContainsKey(pair))
                {
                    m_Errors.Add(pair, ComputeError(pair.First, pair.Second));
                }

                pair.First = Math.Min(face[0], face[2]);
                pair.Second = Math.Max(face[0], face[2]);

                if (!m_Errors.ContainsKey(pair))
                {
                    m_Errors.Add(pair, ComputeError(pair.First, pair.Second));
                }

                pair.First = Math.Min(face[1], face[2]);
                pair.Second = Math.Max(face[1], face[2]);

                if (!m_Errors.ContainsKey(pair))
                {
                    m_Errors.Add(pair, ComputeError(pair.First, pair.Second));
                }
            }

            if (m_EnableVirtualPairs)
            {
                for (int i = 1; i < m_Vertices.Count; ++i)
                {
                    for (int j = i + 1; j < m_Vertices.Count; ++j)
                    {
                        if (Vector3.Distance(m_Vertices[i].Position, m_Vertices[j].Position) < level)
                        {
                            VertexPair pair;
                            pair.First = i;
                            pair.Second = j;
                            if (!m_Errors.ContainsKey(pair))
                            {
                                m_Errors.Add(pair, ComputeError(i, j));
                            }
                        }
                    }
                }
            }
        }

        private double ComputeError(int id1, int id2)
        {
            Vector3 unused;
            return ComputeError(id1, id2, out unused);
        }

        private double ComputeError(int id1, int id2, out Vector3 error)
        {
            ErrorMetric edge = ErrorMetric.Empty;
            ErrorMetric delta = ErrorMetric.Empty;

            Vector3 vertex = new Vector3();

            edge = m_Quadrics[id1] + m_Quadrics[id2];

            delta = edge;
            Matrix m = delta.Matrix;
            {
                m.M41 = 0.0f;
                m.M42 = 0.0f;
                m.M43 = 0.0f;
                m.M44 = 1.0f;
            }
            delta.Matrix = m;

            double minError = 0.0;

            if (Math.Abs(delta.Matrix.Determinant()) <= 1e-5)
            {
                Vector3 v1 = m_Vertices[id1].Position;
                Vector3 v2 = m_Vertices[id2].Position;
                Vector3 v3 = Vector3Extension.Mean(v1, v2);

                double e1 = edge.Evaluate(v1);
                double e2 = edge.Evaluate(v2);
                double e3 = edge.Evaluate(v3);

                minError = Math.Min(Math.Min(e1, e2), e3);

                if (minError == e1)
                {
                    vertex = v1;
                }
                else if (minError == e2)
                {
                    vertex = v2;
                }
                else if (minError == e3)
                {
                    vertex = v3;
                }
            }
            else
            {
                double det = (double)delta.Matrix.IndexedDeterminant(0, 1, 2, 4, 5, 6, 8, 9, 10);
                vertex.X = (float)(-1.0 / det * (delta.Matrix.IndexedDeterminant(1, 2, 3, 5, 6, 7, 9, 10, 11)));
                vertex.Y = (float)(1.0 / det * (delta.Matrix.IndexedDeterminant(0, 2, 3, 4, 6, 7, 8, 10, 11)));
                vertex.Z = (float)(-1.0 / det * (delta.Matrix.IndexedDeterminant(0, 1, 3, 4, 5, 7, 8, 9, 11)));
            }

            error = vertex;
            minError = edge.Evaluate(vertex);
            return minError;
        }

        public bool EnableVirtualPairs
        {
            get { return m_EnableVirtualPairs; }
            set { m_EnableVirtualPairs = value; }
        }

        private Dictionary<int, Vertex> m_Vertices = new Dictionary<int, Vertex>();
        private List<Face> m_Faces = new List<Face>();
        private Dictionary<int, ErrorMetric> m_Quadrics = new Dictionary<int, ErrorMetric>();
        private Dictionary<VertexPair, double> m_Errors = new Dictionary<VertexPair, double>();
        private Deque<Split> m_Splits = new Deque<Split>();
        private bool m_EnableVirtualPairs = false;
    }
}
