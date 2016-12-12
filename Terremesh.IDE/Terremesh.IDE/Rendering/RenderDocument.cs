using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Terremesh.IDE.Rendering
{
    /// <summary>
    /// Render Document
    /// </summary>
    partial class RenderDocument : Forms.DocumentBase
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of RenderDocument class.
        /// </summary>
        public RenderDocument()
        {
            m_Renderer = new Renderer(this);
            InitializeComponent();
        }
        #endregion

        #region Properties
        public IntPtr RenderHandle
        {
            get { return renderPanel.Handle; }
        }

        public Renderer Renderer
        {
            get { return m_Renderer; }
        }

        #endregion

        #region Remeshing Fields
        private Renderer m_Renderer;
        #endregion

        private bool m_MovingCamera = false;
        private System.Drawing.Point m_Previous;

        private void RenderDocument_Load(object sender, EventArgs e)
        {
            //m_Renderer.Run();
            m_Renderer.RunOneFrame();
        }

        protected override void OnResize(EventArgs e)
        {
            if (m_Renderer != null)
            {
                m_Renderer.Resize();
            }
            base.OnResize(e);
        }

        private void renderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                m_Previous = e.Location;
                m_MovingCamera = true;
            }
        }

        private void renderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                m_MovingCamera = false;
            }
        }

        private void renderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MovingCamera)
            {
#if true
                m_Renderer.Camera.Rotate(
                    e.Location.X - m_Previous.X,
                    e.Location.Y - m_Previous.Y
                    );
                m_Previous = e.Location;
#else
                m_Renderer.Camera.Rotate(
                    e.Location.X - m_Previous.X,
                    e.Location.Y - m_Previous.Y
                    );

                Cursor.Position = renderPanel.PointToScreen(m_Previous);
#endif
            }
        }

        private void renderPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void RenderDocument_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    {
                        Renderer.Camera.EnableMove(CameraMovement.Left);
                        break;
                    }
                case Keys.D:
                    {
                        Renderer.Camera.EnableMove(CameraMovement.Right);
                        break;
                    }
                case Keys.W:
                    {
                        Renderer.Camera.EnableMove(CameraMovement.Forward);
                        break;
                    }
                case Keys.S:
                    {
                        Renderer.Camera.EnableMove(CameraMovement.Backward);
                        break;
                    }
                case Keys.Q:
                    {
                        Renderer.Camera.EnableMove(CameraMovement.Up);
                        break;
                    }
                case Keys.E:
                    {
                        Renderer.Camera.EnableMove(CameraMovement.Down);
                        break;
                    }
            }
            if (e.Shift && e.Control)
            {
                Renderer.Camera.Speed = CameraSpeed.Speed4;
            }
            else if (e.Shift)
            {
                Renderer.Camera.Speed = CameraSpeed.Speed2;
            }
            else if (e.Control)
            {
                Renderer.Camera.Speed = CameraSpeed.Speed3;
            }
            else
            {
                Renderer.Camera.Speed = CameraSpeed.Speed1;
            }
        }

        private void RenderDocument_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    {
                        Renderer.Camera.DisableMove(CameraMovement.Left);
                        break;
                    }
                case Keys.D:
                    {
                        Renderer.Camera.DisableMove(CameraMovement.Right);
                        break;
                    }
                case Keys.W:
                    {
                        Renderer.Camera.DisableMove(CameraMovement.Forward);
                        break;
                    }
                case Keys.S:
                    {
                        Renderer.Camera.DisableMove(CameraMovement.Backward);
                        break;
                    }
                case Keys.Q:
                    {
                        Renderer.Camera.DisableMove(CameraMovement.Up);
                        break;
                    }
                case Keys.E:
                    {
                        Renderer.Camera.DisableMove(CameraMovement.Down);
                        break;
                    }
            }
        }

    }
}