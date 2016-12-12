using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.IO;


namespace Terremesh.Remesh.Compiler
{
    class Program
    {
        private class Listener : Remeshing.IProgressListener
        {
            public Listener(string caption)
            {
                m_Caption = caption;
            }

            public void OnProgress(long current, long total)
            {
                float ratio = (float)current / (float)total;
                ratio *= 100;
                if (m_Progress != (int)ratio)
                {
                    m_Progress = (int)ratio;
                    Console.WriteLine("{0} {1}%", m_Caption, m_Progress);
                }
            }

            void Remeshing.IProgressListener.OnComplete(string stage)
            {
                Console.WriteLine("{0} Completed", m_Caption);
            }


            void Remeshing.IProgressListener.OnStart(string stage)
            {
                Console.WriteLine("{0} Started", m_Caption);
            }

            private string m_Caption;
            private int m_Progress = 0;
        }

        static void Main(string[] args)
        {
            string inputPath = null;
            string outputPath = null;
            string methodName = null;
            double percentRemoved = 0.5;
            int targetTriangles = 0;
            bool showHelp = false;
            bool verbose = false;

            bool target = false;
            bool ratio = false;

            var options = new NDesk.Options.OptionSet()
            {
                {"i|input=", "The input file.", value => inputPath = value },
                {"o|output=", "The output file", value => outputPath = value },
                {"m|method=", "The used method.", value => methodName = value },
                {"h|help", "Shows help message and exit.", value => showHelp = value != null },
                {"r|ratio=", "The percentage of triangles to remove", value =>
                { 
                    percentRemoved = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat); ratio = true;
                } },
                {"t|target=", "The target number of triangles", value => { targetTriangles = Convert.ToInt32(value); target = true; } },
                {"v|verbose", "Verbose", value => verbose = value != null },
            };

            List<string> extra;

            Console.WriteLine("Terramesh Remesh Converter");
            Console.WriteLine("Grzybowski & Ruchwa (C) 2012");
            Console.WriteLine();


            try
            {
                extra = options.Parse(args);
            }

            catch (NDesk.Options.OptionException e)
            {
                Console.WriteLine("{0}", e.Message);
            }


            if (showHelp)
            {
                Console.WriteLine("Usage: trcm [OPTIONS]");
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            if (verbose)
            {
                Console.WriteLine(@"Verbose mode.");
            }


            try
            {
                if (inputPath != null &&
                    outputPath != null &&
                    methodName != null &&
                    (ratio || target))
                {
                    if (verbose)
                    {
                        Console.WriteLine(@"Converting '{0}' to '{1}' using '{2}' method", inputPath, outputPath, methodName);
                    }


                    var inFile = new FileStream(inputPath, FileMode.Open);
                    var outFile = new FileStream(outputPath, FileMode.Create);

                    var reader = new Remeshing.MeshReader(inFile);
                    var writer = new Remeshing.MeshWriter(outFile);

                    var verticesIn = new List<Remeshing.Vertex>();
                    var indicesIn = new List<int>();
                    var verticesOut = new List<Remeshing.Vertex>();
                    var indicesOut = new List<int>();

                    Remeshing.IRemeshingMethod method = null;

                    switch (methodName)
                    {
                        case "asem":
                            {

                                method = new Remeshing.AngleSumErrorMetricMethod();
                                break;
                            }
                        case "rem":
                            {
                                method = new Remeshing.RandomRemeshingMethod();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Unknown method name.");
                                return;
                            }
                    }

                    if (verbose)
                    {
                        Console.WriteLine("Using '{0}' method.", method.GetType().ToString());
                    }

                    reader.ReadMesh(out verticesIn, out indicesIn, new Listener("Reading"));

                    var mesh = new Remeshing.Mesh(ref verticesIn, ref indicesIn);

                    if (ratio)
                    {
                        method.Process(ref mesh, percentRemoved, new Listener("Remeshing"));
                    }
                    else
                    {
                        method.Process(ref mesh, targetTriangles, new Listener("Remeshing"));
                    }
                    mesh.Save(out verticesOut, out indicesOut);
                    writer.WriteMesh(ref verticesOut, ref indicesOut, new Listener("Writing"));
                }
                else
                {
                    Console.WriteLine("Not all required arguments specified. Please use: ./trc --help to show help message.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }

}
