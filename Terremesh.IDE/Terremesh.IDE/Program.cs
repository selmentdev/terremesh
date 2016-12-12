using System;
using System.Windows.Forms;

namespace Terremesh.IDE
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var mainForm = new Forms.MainForm();
            // Hack: exit from Xna embedded window renderer.
            mainForm.Disposed += (object sender, EventArgs e) =>
                {
                    mainForm.RenderDocument.Renderer.Exit();
                };
            mainForm.Show();
            mainForm.RenderDocument.Renderer.Run();
        }
    }
#endif
}

