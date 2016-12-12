using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
namespace Terremesh.IDE.Rendering
{
    /// <summary>
    /// 3D renderer used to render files loaded from file.
    /// </summary>
    class Renderer : Microsoft.Xna.Framework.Game
    {
        #region Fields
        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;
        private SpriteFont m_Font;
        private RenderDocument m_RenderDoc;
        private BasicEffect m_BasicEffect;
        private BasicEffect m_WireframeEffect;
        private IndexBuffer m_IndexBuffer;
        private VertexBuffer m_VertexBuffer;
        private Camera m_Camera = new Camera();
        private RenderConfig m_Config;
        private DepthStencilState m_DepthStencilState = new DepthStencilState();
        private DepthStencilState m_WireframeDepthStencilState = new DepthStencilState();
        private RasterizerState m_RasterizerState_Wireframe = new RasterizerState();
        private RasterizerState m_RasterizerState_Solid = new RasterizerState();
        #endregion

        #region Inner types
        /// <summary>
        /// Provides structure used to configure renderer at runtime.
        /// </summary>
        public class RenderConfig
        {
            #region Converts
            /// <summary>
            /// Converts color from XNA to System.Drawing.
            /// </summary>
            /// <param name="color">The color to convert.</param>
            /// <returns>The converted color.</returns>
            internal static System.Drawing.Color ConvertToDrawingColor(Microsoft.Xna.Framework.Color color)
            {
                return System.Drawing.Color.FromArgb(
                        color.A,
                        color.R,
                        color.G,
                        color.B);
            }

            /// <summary>
            /// Converts color from System.Drawing to XNA.
            /// </summary>
            /// <param name="color">The color to convert.</param>
            /// <returns>The converted color.</returns>
            internal static Microsoft.Xna.Framework.Color ConvertToXnaColor(System.Drawing.Color color)
            {
                return Microsoft.Xna.Framework.Color.FromNonPremultiplied(
                    color.R,
                    color.G,
                    color.B,
                    color.A);
            }
            #endregion

            #region Constructors
            /// <summary>
            /// Creates instance of the RenderConfig class.
            /// </summary>
            /// <param name="parent">The renderer.</param>
            public RenderConfig(Renderer parent)
            {
                m_Parent = parent;
                m_SunDirection.Normalize();
            }
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets value indicating whether model should be rendered in wireframe.
            /// </summary>
            [Category("Renderer"), Description("Determines whether ground should be rendered using wireframe filling mode.")]
            public bool Wireframe
            {
                get { return m_Wireframe; }
                set { m_Wireframe = value; }
            }

            /// <summary>
            /// Gets or sets value indicating ambient light color.
            /// </summary>
            [Category("Environment"), Description("Ambient light color")]
            public System.Drawing.Color AmbientLight
            {
                get { return ConvertToDrawingColor(m_AmbientLightColor); }
                set { m_AmbientLightColor = ConvertToXnaColor(value); }
            }
            
            /// <summary>
            /// Gets or sets value indicating diffuse light color.
            /// </summary>
            [Category("Environment"), Description("Diffuse color")]
            public System.Drawing.Color DiffuseColor
            {
                get { return ConvertToDrawingColor(m_DiffuseColor); }
                set { m_DiffuseColor = ConvertToXnaColor(value); }
            }

            /// <summary>
            /// Gets or sets value indicating background color.
            /// </summary>
            [Category("Environment"), Description("Background color")]
            public System.Drawing.Color BackgroundColor
            {
                get { return ConvertToDrawingColor(m_BackgroundColor); }
                set { m_BackgroundColor = ConvertToXnaColor(value); }
            }

            /// <summary>
            /// Gets or sets value indicating sun direction vector.
            /// </summary>
            [Category("Sun"), Description("Sun direction vector")]
            public Vector3 SunDirection
            {
                get { return m_SunDirection; }
                set
                {
                    m_SunDirection = value;
                    m_SunDirection.Normalize();
                }
            }

            /// <summary>
            /// Gets or sets value indicating sun diffuse color.
            /// </summary>
            [Category("Sun"), Description("Sun diffuse color")]
            public System.Drawing.Color SunDiffuseColor
            {
                get { return ConvertToDrawingColor(m_SunDiffuseColor); }
                set { m_SunDiffuseColor = ConvertToXnaColor(value); }
            }

            [Category("Renderer"), Description("Determines wireframe rendering color")]
            public System.Drawing.Color WireframeColor
            {
                get { return ConvertToDrawingColor(m_WireframeColor); }
                set { m_WireframeColor = ConvertToXnaColor(value); }
            }
            #endregion

            #region Methods
            /// <summary>
            /// Converts render config to string.
            /// </summary>
            /// <returns>The string representing RenderConfig instance.</returns>
            public override string ToString()
            {
                return "Renderer Configuration";
            }
            #endregion

            #region Fields
            private bool m_Wireframe = true;
            private Renderer m_Parent;
            internal Color m_AmbientLightColor = Color.WhiteSmoke;
            internal Color m_DiffuseColor = Color.ForestGreen;

            internal Vector3 m_SunDirection = new Vector3(-1.0f, -1.0f, -1.0f);
            internal Color m_SunDiffuseColor = Color.White;
            internal Color m_BackgroundColor = Color.CornflowerBlue;
            internal Color m_WireframeColor = Color.White;
            #endregion
        }

        /// <summary>
        /// Vertex format used to store mesh data.
        /// </summary>
        public struct VertexPositionNormalColor
        {
            /// <summary>
            /// Vertex position.
            /// </summary>
            public Vector3 Position;
            
            /// <summary>
            /// Vertex normal vector.
            /// </summary>
            public Vector3 Normal;

            /// <summary>
            /// Vertex color.
            /// </summary>
            public Color Color;

            /// <summary>
            /// Vertex declaration.
            /// </summary>
            public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]{
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                new VertexElement(sizeof(float) * 6, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            });
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets renderer configuration.
        /// </summary>
        public RenderConfig Config
        {
            get { return m_Config; }
            set { m_Config = value; }
        }

        /// <summary>
        /// Gets or sets camera used to view scene.
        /// </summary>
        public Camera Camera
        {
            get { return m_Camera; }
            set { m_Camera = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates instance of the renderer.
        /// </summary>
        /// <param name="doc">The render document, to render content.</param>
        public Renderer(RenderDocument doc)
        {
            m_RenderDoc = doc;
            m_Config = new RenderConfig(this);

            var xnaWindow = (Form)Control.FromHandle(this.Window.Handle);
            xnaWindow.GotFocus += (object sender, EventArgs e) =>
            {
                (sender as Form).Visible = false;
                
            };

            m_RasterizerState_Solid.CullMode = CullMode.CullClockwiseFace;
            m_RasterizerState_Solid.FillMode = FillMode.Solid;
            m_RasterizerState_Wireframe.CullMode = CullMode.None;
            m_RasterizerState_Wireframe.FillMode = FillMode.WireFrame;
            m_RasterizerState_Wireframe.DepthBias = -0.000001f; 

            m_DepthStencilState.DepthBufferWriteEnable = true;
            m_DepthStencilState.DepthBufferEnable = true;
            m_DepthStencilState.DepthBufferFunction = CompareFunction.LessEqual;

            m_WireframeDepthStencilState.DepthBufferEnable = false;
            m_WireframeDepthStencilState.DepthBufferWriteEnable = false;
            m_WireframeDepthStencilState.DepthBufferFunction = CompareFunction.Always;
            

            m_Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = m_RenderDoc.ClientSize.Width,
                PreferredBackBufferHeight = m_RenderDoc.ClientSize.Height
            };

            m_Graphics.PreparingDeviceSettings += OnGraphicsPreparingDeviceSettings;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion

        #region Overriden methods from Xna.Framework.Game
        private void OnGraphicsPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = m_RenderDoc.RenderHandle;    
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            m_Font = Content.Load<SpriteFont>("DefaultFont");

            m_BasicEffect = new BasicEffect(GraphicsDevice);
            m_BasicEffect.VertexColorEnabled = false;
            m_BasicEffect.TextureEnabled = false;
            m_BasicEffect.LightingEnabled = true;
            m_BasicEffect.EnableDefaultLighting();
            m_BasicEffect.PreferPerPixelLighting = true;

            m_WireframeEffect = new BasicEffect(GraphicsDevice);
            m_BasicEffect.VertexColorEnabled = true;
            m_BasicEffect.TextureEnabled = false;
            m_BasicEffect.LightingEnabled = true;
            m_BasicEffect.EnableDefaultLighting();
            m_BasicEffect.PreferPerPixelLighting = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads model from vertices and indices list.
        /// </summary>
        /// <param name="vertices">The list of vertices.</param>
        /// <param name="indices">The list of indices.</param>
        public void LoadModel(ref List<Mesh.Vertex> vertices, ref List<int> indices)
        {
            VertexPositionNormalColor[] vvertices = new VertexPositionNormalColor[vertices.Count];

            int i = 0;
            foreach (var vertex in vertices)
            {
                vvertices[i].Position = vertex.Position;
                vvertices[i].Color = Color.DarkBlue;
                vvertices[i++].Normal = vertex.Normal;
            }

            int[] vindices = new int[indices.Count];
            i = 0;
            foreach (var index in indices)
            {
                vindices[i++] = index;
            }

            m_IndexBuffer = new IndexBuffer(GraphicsDevice, typeof(int), indices.Count, BufferUsage.WriteOnly);
            m_IndexBuffer.SetData(vindices);

            m_VertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionNormalColor.VertexDeclaration, vvertices.Length, BufferUsage.WriteOnly);
            m_VertexBuffer.SetData(vvertices);
        }

        public void Clear()
        {
            m_VertexBuffer = null;
            m_IndexBuffer = null;
        }

        /// <summary>
        /// Performs upda
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            m_Camera.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws everything
        /// </summary>
        /// <param name="gameTime">The time since last frame.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(m_Config.m_BackgroundColor);
            
            GraphicsDevice.DepthStencilState = m_DepthStencilState;

            if (m_VertexBuffer != null && m_IndexBuffer != null)
            {
                m_BasicEffect.World = Matrix.Identity;
                m_BasicEffect.View = Camera.View;
                m_BasicEffect.Projection = Camera.Projection;
                m_BasicEffect.DirectionalLight0.Direction = m_Config.m_SunDirection;
                m_BasicEffect.DirectionalLight0.DiffuseColor = m_Config.m_SunDiffuseColor.ToVector3();
                m_BasicEffect.DirectionalLight0.Enabled = true;
                m_BasicEffect.AmbientLightColor = m_Config.m_AmbientLightColor.ToVector3();
                m_BasicEffect.DiffuseColor = m_Config.m_DiffuseColor.ToVector3();

                GraphicsDevice.RasterizerState = m_RasterizerState_Solid;

                foreach (var pass in m_BasicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    GraphicsDevice.SetVertexBuffer(m_VertexBuffer);
                    GraphicsDevice.Indices = m_IndexBuffer;

                    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_VertexBuffer.VertexCount, 0, m_IndexBuffer.IndexCount / 3);
                }

                if (m_Config.Wireframe)
                {
                    GraphicsDevice.RasterizerState = m_RasterizerState_Wireframe;

                    m_WireframeEffect.World = Matrix.Identity;
                    m_WireframeEffect.View = Camera.View;
                    m_WireframeEffect.Projection = Camera.Projection;
                    

                    m_BasicEffect.DirectionalLight0.Direction = m_Config.m_SunDirection;
                    m_BasicEffect.DirectionalLight0.DiffuseColor = m_Config.m_SunDiffuseColor.ToVector3();
                    m_BasicEffect.DirectionalLight0.Enabled = true;
                    m_BasicEffect.AmbientLightColor = m_Config.m_AmbientLightColor.ToVector3();
                    m_WireframeEffect.DiffuseColor = m_Config.m_WireframeColor.ToVector3();

                    foreach (var pass in m_WireframeEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                        GraphicsDevice.SetVertexBuffer(m_VertexBuffer);
                        GraphicsDevice.Indices = m_IndexBuffer;

                        GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_VertexBuffer.VertexCount, 0, m_IndexBuffer.IndexCount / 3);
                    }
                }
            }

            m_SpriteBatch.Begin();
            m_SpriteBatch.DrawString(m_Font, string.Format("{0}\n{1:0.} FPS", gameTime.ElapsedGameTime.Milliseconds, 1000.0 / (double)gameTime.ElapsedGameTime.Milliseconds), new Vector2(0.0f, 0.0f), Color.AliceBlue);
            m_SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Handles resize event.
        /// </summary>
        public void Resize()
        {
            int width = m_RenderDoc.ClientSize.Width;
            int height = m_RenderDoc.ClientSize.Height;

            if ((width * height) != 0)
            {
                m_Camera.AspectRatio = (float)width / (float)height;
                m_Graphics.PreferredBackBufferWidth = width;
                m_Graphics.PreferredBackBufferHeight = height;
                m_Graphics.ApplyChanges();
            }
        }
        #endregion
    }
}
