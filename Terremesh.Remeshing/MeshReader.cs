using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Terremesh.Remeshing
{
    /// <summary>
    /// Mesh reader class.
    /// </summary>
    public class MeshReader
    {
        #region Fields
        private StreamReader inputReader;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates instance of the MeshReader class.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public MeshReader(Stream input)
        {
            inputReader = new StreamReader(input);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads raw mesh.
        /// </summary>
        /// <param name="vertices">The list of the vertices.</param>
        /// <param name="indices">The list of the indices.</param>
        /// <param name="progress">The optional progress listener.</param>
        public void ReadMesh(out List<Vertex> vertices, out List<int> indices, IProgressListener progress = null)
        {
            vertices = new List<Vertex>();
            indices = new List<int>();

            Regex r = new Regex(@" |//");
            NumberStyles numberStyle = NumberStyles.Float;
            IFormatProvider numberFormatProvider = CultureInfo.InvariantCulture;

            progress.OnStart("Loading mesh started...");

            while (inputReader.Peek() >= 0)
            {
                string line = inputReader.ReadLine();

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
                                Vertex w = new Vertex(new Vector3(x, y, z));
                                vertices.Add(w);
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
                                            indices.Add(f1 -1);
                                            indices.Add(f2 -1);
                                            indices.Add(f3 -1);
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

                if (progress != null)
                {
                    progress.OnProgress(
                        inputReader.BaseStream.Position,
                        inputReader.BaseStream.Length);
                }
            }
            
            if (progress != null)
            {
                progress.OnComplete("Mesh loaded successfully");
            }
        }
        #endregion
    }
}
