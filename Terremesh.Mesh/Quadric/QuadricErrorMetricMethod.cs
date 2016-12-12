using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terremesh.Mesh.XnaFramework;

namespace Terremesh.Mesh.Quadric
{
    public class QuadricErrorMetricMethod : IRemeshingMethod
    {
        public void Process(ref Mesh mesh, double targetRatio, IProgressListener listener = null)
        {
            if ((targetRatio <= 0.0) || (targetRatio > 1.0))
            {
                throw new ArgumentOutOfRangeException("targetRatio");
            }

            int targetTriangles = (int)(mesh.Faces.Count * targetRatio);

            Process(ref mesh, targetTriangles, listener);
        }

        public void Process(ref Mesh mesh, int targetTriangles, IProgressListener listener = null)
        {
            if ((targetTriangles <= 0) || (targetTriangles > mesh.Faces.Count))
            {
                throw new ArgumentOutOfRangeException("targetTriangles");
            }

            Clear();

            m_Vertices = mesh.Vertices;
            m_Faces = mesh.Faces;

            ComputeInitialQuadrics();
            Contract(targetTriangles, listener);

            mesh.Vertices = m_Vertices;
            mesh.Faces = m_Faces;
        }

        private void Clear()
        {
            m_Vertices = null;
            m_Faces = null;
            m_Errors.Clear();
            m_Quadrics.Clear();
            m_Splits.Clear();
        }

        private void Contract(int targetTriangles, IProgressListener listener = null)
        {
            SelectValidPairs();

            if (listener != null)
            {
                listener.OnStarted("Compacting");
            }

            int totalTriangles = m_Faces.Count - targetTriangles;
            int currentTriangle = 0;
            while (m_Faces.Count > targetTriangles)
            {
                if (listener != null)
                {
                    listener.OnStep(currentTriangle, totalTriangles);
                }
                ++currentTriangle;
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

#if false
                VertexSplit split = new VertexSplit();

                //var fv

                split.First.Position = m_Vertices[minPair.First].Position;
                split.Second.Position = m_Vertices[minPair.Second].Position;
                split.Target.Position = error;
                m_Splits.Add(split);
#endif
                var vertex = m_Vertices[minPair.First];
                vertex.Position = error;

                m_Quadrics[minPair.First] = m_Quadrics[minPair.First] + m_Quadrics[minPair.Second];

                for (int i = m_Faces.Count - 1; i != 0; )
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

#if false
                for (int iter = m_Errors.Count - 1; iter != 0; )
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
#else
                for (int it = 0; it < m_Errors.Count; ++it)
                {
                    pair = m_Errors.ElementAt(it);

                    if (pair.Key.First == minPair.Second && pair.Key.Second != minPair.First)
                    {
                        m_Errors.Remove(m_Errors.ElementAt(it).Key);

                        var key = new VertexPair()
                        {
                            First = Math.Min(minPair.First, pair.Key.Second),
                            Second = Math.Max(minPair.First, pair.Key.Second)
                        };

                        if (!m_Errors.ContainsKey(key))
                        {
                            m_Errors.Add(
                                key,
                                0.0);
                        }
                    }
                    else if (pair.Key.Second == minPair.Second && pair.Key.First != minPair.First)
                    {
                        m_Errors.Remove(m_Errors.ElementAt(it).Key);

                        var key = new VertexPair()
                        {
                            First = Math.Min(minPair.First, pair.Key.First),
                            Second = Math.Max(minPair.First, pair.Key.First)
                        };

                        if (!m_Errors.ContainsKey(key))
                        {
                            m_Errors.Add(
                                key,
                                0.0);
                        }
                    }
                }
#endif
                m_Errors.Remove(minPair);

                for (int it = 0; it < m_Errors.Count; ++it)
                {
                    var key = m_Errors.ElementAt(it).Key;

                    if (key.First == minPair.First)
                    {
                        m_Errors[key] = ComputeError(minPair.First, key.Second);
                    }

                    if (key.Second == minPair.First)
                    {
                        m_Errors[key] = ComputeError(minPair.First, key.First);
                    }
                }
                /*foreach (var e in m_Errors)
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
                }*/

            }
            if (listener != null)
            {
                listener.OnComplete("Compacting");
            }
        }

        private void ComputeInitialQuadrics()
        {
            for (int i = 0; i < m_Vertices.Count; ++i)
            {
                m_Quadrics.Add(i + 1, ErrorMetric.Empty);
            }

            for (int i = 0; i < m_Faces.Count; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    var face = m_Faces.ElementAt(i);
                    var index = face[j];

#if DEBUG
                    if (!m_Quadrics.ContainsKey(index))
                    {
                        System.Diagnostics.Debug.WriteLine("Failed {0}", index);
                    }
#endif
                    m_Quadrics[index] += new ErrorMetric(face.Plane);
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

        private Dictionary<int, Vertex> m_Vertices = null;
        private List<Triangle> m_Faces = null;
        private Dictionary<int, ErrorMetric> m_Quadrics = new Dictionary<int, ErrorMetric>();
        private Dictionary<VertexPair, double> m_Errors = new Dictionary<VertexPair, double>();
        private List<VertexSplit> m_Splits = new List<VertexSplit>();
        private bool m_EnableVirtualPairs = false;
    }
}
