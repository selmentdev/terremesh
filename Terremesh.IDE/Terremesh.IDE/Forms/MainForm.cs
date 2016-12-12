using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace Terremesh.IDE.Forms
{
    /// <summary>
    /// Main form
    /// </summary>
    partial class MainForm : Form, IProgressListener
    {
        private Forms.PropertiesPane m_PropertiesPane = new PropertiesPane();
        private Forms.OutputPane m_OutputPane = new OutputPane();
        private Forms.OptionsDocument m_OptionsDoc = null;
        private Forms.StartPage m_StartPage = null;
        private Rendering.RenderDocument m_RenderDocument = new Rendering.RenderDocument();


        private List<Mesh.Vertex> m_Vertices = null;
        private List<int> m_Indices = null;
        private bool m_Loaded = false;
        private string m_File;
        private string m_TempFile;

        public Rendering.RenderDocument RenderDocument
        {
            get { return m_RenderDocument; }
        }

        /// <summary>
        /// Describe single remeshing method.
        /// </summary>
        private class RemeshingMethod
        {
            /// <summary>
            /// Path to exe tool.
            /// </summary>
            public string Path;

            /// <summary>
            /// Name of the tool.
            /// </summary>
            public string Name;
            
            /// <summary>
            /// Caption showed in selection.
            /// </summary>
            public string Caption;

            public override string ToString()
            {
                return Caption;
            }
        }

        /// <summary>
        /// Creates a new instance of MainForm.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            /// <todo>Hardcoded</todo>
            tcbxRemeshingMethod.Items.Add(new RemeshingMethod() { Caption = "Quadric Error Metric", Path = "..\\tools\\trc.exe", Name = "qem" });
            tcbxRemeshingMethod.Items.Add(new RemeshingMethod() { Caption = "Angle Sum Error Metric", Path = "..\\tools\\trcm.exe", Name = "asem" });
            tcbxRemeshingMethod.Items.Add(new RemeshingMethod() { Caption = "Random Error Metric", Path = "..\\tools\\trcm.exe", Name = "rem" });
            tcbxRemeshingMethod.SelectedIndex = 0;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Showing About document...");

            Forms.AboutBox dlg = new AboutBox();
            dlg.Show(dockPanel1);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Showing Properties...");
            
            m_PropertiesPane.Show(this.dockPanel1);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Showing Options Document...");
            if (m_OptionsDoc == null)
            {
                m_OptionsDoc = new OptionsDocument();
            }
            m_OptionsDoc.Show(this.dockPanel1);
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_OutputPane.Show(this.dockPanel1);
        }

        private void testOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Test! -- this is some shorty longy line");
        }

        /// <summary>
        /// Gets IDockContent for the specified name.
        /// </summary>
        /// <param name="name">The name of the pane to load.</param>
        /// <returns>The dock pane.</returns>
        public IDockContent Persist(string name)
        {
            IDockContent pane = null;
            Trace.WriteLine(String.Format("Loading: \"{0}\"", name));

            switch (name)
            {
                case "Terremesh.IDE.Forms.OutputPane":
                    {
                        pane = m_OutputPane;
                        break;
                    }
                case "Terremesh.IDE.Forms.PropertiesPane":
                    {
                        pane = m_PropertiesPane;
                        break;
                    }
                case "Terremesh.IDE.Rendering.RenderDocument":
                    {
                        pane = m_RenderDocument;
                        break;
                    }
                /*case "Terremesh.IDE.Forms.SceneExplorer":
                    {
                        pane = m_SceneExplorer;
                        break;
                    }*/
                case "Terremesh.IDE.Forms.AboutBox":
                    {
                        pane = new Forms.AboutBox();
                        break;
                    }
                case "Terremesh.IDE.Forms.OptionsDocument":
                    {
                        if (m_OptionsDoc == null)
                        {
                            m_OptionsDoc = new OptionsDocument();
                        }
                        pane = m_OptionsDoc;
                        break;
                    }
                case "Terremesh.IDE.Forms.StartPage":
                    {
                        StartPage.ShowOnStartup = true;
                        if (m_StartPage == null)
                        {
                            m_StartPage = new StartPage();
                        }
                        pane = m_StartPage;
                        break;
                    }
                default:
                    {
                        Trace.WriteLine(String.Format("Unknown pane to load: \"{0}\"", name));
                        break;
                    }
            }
            return pane;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = "Terremesh";

            Trace.WriteLine("Loading default layout");

            if (System.IO.File.Exists("layout.xml"))
            {
                dockPanel1.LoadFromXml("layout.xml", this.Persist);
            }
            else
            {
                m_PropertiesPane.Show(dockPanel1, DockState.DockRight);
                m_OutputPane.Show(dockPanel1, DockState.DockBottom);
            }

            m_PropertiesPane.RegisterObject(m_RenderDocument.Renderer.Camera);
            m_PropertiesPane.RegisterObject(m_RenderDocument.Renderer.Config);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dockPanel1.SaveAsXml("layout.xml");
        }

        private void startPageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void OnShowProperties(object sender, EventArgs e)
        {
            
        }

        private void actShowProperties(object sender, EventArgs e)
        {

        }

        private void actViewShowProperties(object sender, EventArgs e)
        {
            m_PropertiesPane.Show(dockPanel1);
        }

        private void actViewShowOutput(object sender, EventArgs e)
        {
            m_OutputPane.Show(dockPanel1);
        }

        private void actViewShowStartPage(object sender, EventArgs e)
        {
            if (m_StartPage == null)
            {
                m_StartPage = new StartPage();
            }
            m_StartPage.Show(dockPanel1);
        }

        private void rendererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_RenderDocument.Show(this.dockPanel1, DockState.Document);
        }

      
        private void remeshToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgOpenMesh.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (System.IO.File.Exists(dlgOpenMesh.FileName))
                {
                    m_File = dlgOpenMesh.FileName;
                    m_Loaded = true;
                    m_TempFile = Path.GetTempFileName();

                    LoadModel(m_File);
                }
            }
            else
            {
            }
        }

        private void LoadModel(string filename)
        {
            Text = "Terremesh - [" + filename + "]";
            using (var file =
            new BufferedStream(
                new FileStream(filename,
                    FileMode.Open),
                    128 * 1024))
            {
                Mesh.MeshReader reader = new Mesh.MeshReader(file);

                m_Vertices = new List<Mesh.Vertex>();
                m_Indices = new List<int>();

                reader.ReadMesh(out m_Vertices, out m_Indices, this);
                m_RenderDocument.Renderer.LoadModel(ref m_Vertices, ref m_Indices);

                lblTrianglesVertices.Text = string.Format("Triangles: {0}, Vertices: {1}", m_Indices.Count / 3, m_Vertices.Count);
            }
        }

        #region Remeshing
        private int m_ToRemove = 1;
        private int m_Progress = 0;
        #endregion

        private void ttbxVerticesCount_TextChanged(object sender, EventArgs e)
        {
            int toRemove;
            if (int.TryParse(sender.ToString(), out toRemove))
            {
                m_ToRemove = toRemove;
            }
            else
            {
                //MessageBox.Show("Please enter integer number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ttbxVerticesCount.Text = "";
            }
        }

        private void tcbxRemeshingMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tbtnRemoveVertices_Click(object sender, EventArgs e)
        {
        }

        void IProgressListener.OnComplete(string stage)
        {
            //pgrProgress.Value = 0;
            lblStage.Text = stage;
        }

        void IProgressListener.OnStart(string stage)
        {
            pgrProgress.Value = 0;
            lblStage.Text = stage;
        }

        void IProgressListener.OnProgress(long current, long total)
        {
            float ratio = (float)current / (float)total;
            ratio *= 1000;

            int progress = (int)ratio;

            if (m_Progress != progress)
            {
                m_Progress = progress;

                if (lblStage != null)
                {
                    lblProgress.Text = string.Format("{0}/{1}", current, total);
                }

                if (pgrProgress != null)
                {
                    ///BUG: Check for Handle
                    pgrProgress.Maximum = 1000;

                    int value = (int)ratio;
                    pgrProgress.Value = value > 1000 ? 1000 : value;
                }
            }
            Application.DoEvents();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Loaded == false)
            {
                MessageBox.Show("Please load mesh before save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dlgSaveMesh.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_File = dlgSaveMesh.FileName;
                m_Loaded = true;

                SaveModel(m_File);
            }
        }

        private void SaveModel(string filename)
        {
            using (var file =
                new BufferedStream(
                    new FileStream(filename,
                        FileMode.CreateNew),
                        128 * 1024))
            {
                var writer = new Mesh.MeshWriter(file);

                writer.WriteMesh(ref m_Vertices, ref m_Indices, this);
            }
        }

        private void clearWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_Vertices = null;
            m_Indices = null;

            m_Loaded = false;
            m_RenderDocument.Renderer.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data);
        }

        private void toolExit(object sender, EventArgs e)
        {
            Trace.WriteLine("Exit: " + sender);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            RemeshingMethod selectedItem = (RemeshingMethod)tcbxRemeshingMethod.SelectedItem;

            if (selectedItem != null &&
                !string.IsNullOrEmpty(m_File) &&
                !string.IsNullOrEmpty(m_TempFile) &&
                !string.IsNullOrEmpty(ttbxRemovePercent.Text))
            {
                double ratio;
                if (!double.TryParse(ttbxRemovePercent.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out ratio))
                {
                    return;
                }

                string toolPath = Environment.CurrentDirectory + "\\" + selectedItem.Path;
                string fileName = Path.GetFullPath(Path.GetDirectoryName(toolPath)) + "\\" + Path.GetFileName(toolPath);
                string args = " --input=" + m_File + " --output=" + m_TempFile + " --method=" + selectedItem.Name + " --ratio=" + ttbxRemovePercent.Text;

                ProcessAndLoad(fileName, args);
            }
        }

        private void ProcessAndLoad(string fileName, string args)
        {
            try
            {
                using (var tool = new Process())
                {
                    Trace.WriteLine(fileName + args);
                    tool.StartInfo.CreateNoWindow = true;
                    tool.StartInfo.FileName = fileName;
                    tool.StartInfo.Arguments = args;
                    tool.StartInfo.UseShellExecute = false;
                    tool.StartInfo.RedirectStandardOutput = true;

                    tool.Start();

                    while (!tool.StandardOutput.EndOfStream)
                    {
                        Trace.WriteLine(tool.StandardOutput.ReadLine());
                        Application.DoEvents();
                    }

                    LoadModel(m_TempFile);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace.ToString());
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RemeshingMethod selectedItem = (RemeshingMethod)tcbxRemeshingMethod.SelectedItem;

            if (selectedItem != null &&
                !string.IsNullOrEmpty(m_File) &&
                !string.IsNullOrEmpty(m_TempFile) &&
                !string.IsNullOrEmpty(ttbxTriangleCount.Text))
            {
                int target;
                if (!int.TryParse(ttbxTriangleCount.Text, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out target))
                {
                    return;
                }

                string toolPath = Environment.CurrentDirectory + "\\" + selectedItem.Path;
                string fileName = Path.GetFullPath(Path.GetDirectoryName(toolPath)) + "\\" + Path.GetFileName(toolPath);
                string args = " --input=" + m_File + " --output=" + m_TempFile + " --method=" + selectedItem.Name + " --target=" + ttbxTriangleCount.Text;

                ProcessAndLoad(fileName, args);
            }
        }

        private void lblStage_Click(object sender, EventArgs e)
        {

        }

    }
}
