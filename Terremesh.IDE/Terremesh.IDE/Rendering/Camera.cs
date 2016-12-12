using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Terremesh.IDE.Rendering
{
    /// <summary>
    /// Provides enumeration that represents bit flags of camera movement.
    /// </summary>
    [Flags()]
    public enum CameraMovement
    {
        /// <summary>
        /// No move.
        /// </summary>
        None        = 0,

        /// <summary>
        /// Move forward.
        /// </summary>
        Forward     = 1 << 0,

        /// <summary>
        /// Move backward
        /// </summary>
        Backward    = 1 << 1,

        /// <summary>
        /// Move left.
        /// </summary>
        Left        = 1 << 2,

        /// <summary>
        /// Move right.
        /// </summary>
        Right       = 1 << 3,

        /// <summary>
        /// Move up.
        /// </summary>
        Up          = 1 << 4,

        /// <summary>
        /// Move down.
        /// </summary>
        Down        = 1 << 5,
    }
   
    /// <summary>
    /// Provides enumeration that represents camera movement speed.
    /// </summary>
    public enum CameraSpeed
    {
        /// <summary>
        /// The first level speed.
        /// </summary>
        Speed1,

        /// <summary>
        /// The second level speed.
        /// </summary>
        Speed2,

        /// <summary>
        /// The third level speed.
        /// </summary>
        Speed3,
        
        /// <summary>
        /// The fourth level speed.
        /// </summary>
        Speed4,
    }

    /// <summary>
    /// The camera class.
    /// </summary>
    class Camera
    {
        #region Constructors
        /// <summary>
        /// Creates instance of the Camera class.
        /// </summary>
        public Camera()
        {
            m_Position = new Vector3(1.35029054f, 0.926785946f, 3.523842f);
            m_Target = new Vector3(0.989467f, 0.6793819f, 2.62462187f);
            m_Up = Vector3.Up;

            m_Fov = 35.0f;
            m_AspectRatio = 1.0f;
            m_NearZ = 0.001f;
            m_FarZ = 1000.0f;

            UpdateProjection();
            UpdateView();
        }
        #endregion

        #region Camera state update
        /// <summary>
        /// Updates projection matrix.
        /// </summary>
        public void UpdateProjection()
        {
            if (m_Perspective)
            {
                m_Projection = Matrix.CreatePerspectiveFieldOfView(
                    MathHelper.ToRadians(m_Fov),
                    m_AspectRatio,
                    m_NearZ,
                    m_FarZ);
            }
            else
            {
                m_Projection = Matrix.CreateOrthographic(
                    m_AspectRatio,
                    1.0f,
                    m_NearZ,
                    m_FarZ);
            }
        }

        /// <summary>
        /// Updates view matrix.
        /// </summary>
        public void UpdateView()
        {
            m_View = Matrix.CreateLookAt(
                m_Position,
                m_Target,
                m_Up);
        }

        /// <summary>
        /// Updates camera placement according to movement set.
        /// </summary>
        public void Update()
        {
            Vector3 direction = Vector3.Zero;
            if (m_Movement.HasFlag(CameraMovement.Backward))
            {
                direction += Vector3.Backward;
            }
            if (m_Movement.HasFlag(CameraMovement.Forward))
            {
                direction += Vector3.Forward;
            }
            if (m_Movement.HasFlag(CameraMovement.Left))
            {
                direction += Vector3.Left;
            }
            if (m_Movement.HasFlag(CameraMovement.Right))
            {
                direction += Vector3.Right;
            }
            if (m_Movement.HasFlag(CameraMovement.Up))
            {
                direction += Vector3.Up;
            }
            if (m_Movement.HasFlag(CameraMovement.Down))
            {
                direction += Vector3.Down;
            }

            if (Math.Abs(direction.LengthSquared()) > 0.001f)
            {
                direction.Normalize();
                switch (m_Speed)
                {
                    case CameraSpeed.Speed2:
                        {
                            Vector3.Multiply(ref direction, 1.5f, out direction);
                            break;
                        }
                    case CameraSpeed.Speed3:
                        {
                            Vector3.Multiply(ref direction, 2.5f, out direction);
                            break;
                        }
                    case CameraSpeed.Speed4:
                        {
                            Vector3.Multiply(ref direction, 3.5f, out direction);
                            break;
                        }
                }

                this.Move(direction);
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets value representing camera position.
        /// </summary>
        [Category("Placement"), Description("Camera position.")]
        public Vector3 Position
        {
            get { return m_Position; }
            set { m_Position = value; UpdateView(); }
        }

        /// <summary>
        /// Gets or sets value representing camera up vector.
        /// </summary>
        [Category("Placement"), Description("Up vector.")]
        public Vector3 Up
        {
            get { return m_Up; }
            set { m_Up = value; UpdateView(); }
        }

        /// <summary>
        /// Gets or sets value representing camera target vector.
        /// </summary>
        [Category("Placement"), Description("Target vector.")]
        public Vector3 Target
        {
            get { return m_Target; }
            set { m_Target = value; UpdateView(); }
        }

        /// <summary>
        /// Gets or sets value representing angle field of view.
        /// </summary>
        [Category("Configuration"), Description("Field of view.")]
        public float FieldOfView
        {
            get { return m_Fov; }
            set { m_Fov = value; UpdateProjection(); }
        }

        /// <summary>
        /// Gets value representing camera aspect ratio.
        /// </summary>
        [Category("Configuration"), Description("Width to height ratio.")]
        public float AspectRatio
        {
            get { return m_AspectRatio; }
            set { m_AspectRatio = value; UpdateProjection(); }
        }

        /// <summary>
        /// Gets or sets value representing near z plane value.
        /// </summary>
        [Category("Configuration"), Description("Near Z value.")]
        public float NearZ
        {
            get { return m_NearZ; }
            set { m_NearZ = value; UpdateProjection(); }
        }

        /// <summary>
        /// Gets or sets value representing far z plane value.
        /// </summary>
        [Category("Configuration"), Description("Far Z value.")]
        public float FarZ
        {
            get { return m_FarZ; }
            set { m_FarZ = value; UpdateProjection(); }
        }

        /// <summary>
        /// Gets or sets value indicating whether perspective projection should
        /// be used instead of orthogonal projection.
        /// </summary>
        [Category("Configuration"), Description("Enables or disables perspective.")]
        public bool Perspective
        {
            get { return m_Perspective; }
            set { m_Perspective = value; UpdateProjection(); }
        }

        /// <summary>
        /// Gets or sets value indicating camera speed.
        /// </summary>
        [Category("Configuration"), Description("Determines camera speed")]
        public CameraSpeed Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        /// <summary>
        /// Gets value representing projection matrix.
        /// </summary>
        [Category("Matrices"), Description("Projection matrix.")]
        public Matrix Projection
        {
            get { return m_Projection; }
        }

        /// <summary>
        /// Gets value representing view matrix.
        /// </summary>
        [Category("Matrices"), Description("View matrix.")]
        public Matrix View
        {
            get { return m_View; }
        }

        /// <summary>
        /// Gets or sets value representing X rotation angle.
        /// </summary>
        [Category("Rotation"), Description("X rotation angle")]
        public float RotationX
        {
            get { return m_RotationX; }
            set { m_RotationX = value; }
        }

        /// <summary>
        /// Gets or sets value representing Y rotation angle.
        /// </summary>
        [Category("Rotation"), Description("Y rotation angle")]
        public float RotationY
        {
            get { return m_RotationY; }
            set { m_RotationY = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Rotates camera.
        /// </summary>
        /// <param name="xDelta">X delta</param>
        /// <param name="yDelta">Y delta</param>
        public void Rotate(float xDelta, float yDelta)
        {
            xDelta *= -0.005f;
            yDelta *= 0.005f;

            m_RotationX += xDelta;
            m_RotationY += yDelta;

            m_RotationX = MathHelper.WrapAngle(m_RotationX);
            m_RotationY = MathHelper.WrapAngle(m_RotationY);

            Vector3 x = Vector3.UnitZ;
            Quaternion r = Quaternion.CreateFromYawPitchRoll(m_RotationX, m_RotationY, 0.0f);
            Vector3.Transform(ref x, ref r, out m_Direction);

            m_Target = m_Position + m_Direction;

            UpdateView();
        }

        /// <summary>
        /// Move camera using Vector3 directions.
        /// </summary>
        /// <param name="delta">Delta direction vector.</param>
        public void Move(Vector3 delta)
        {
            Vector3 direction;
            Vector3.Multiply(ref delta, -0.15f, out delta);
            
            Quaternion r = Quaternion.CreateFromYawPitchRoll(m_RotationX, m_RotationY, 0.0f);
            Vector3.Transform(ref delta, ref r, out direction);

            m_Position += direction;
            m_Target = m_Position + m_Direction;

            UpdateView();
        }

        /// <summary>
        /// Enables specific movement flags.
        /// </summary>
        /// <param name="movement">The movement flags to enable</param>
        public void EnableMove(CameraMovement movement)
        {
            m_Movement |= movement;
        }

        /// <summary>
        /// Disables specific movement flags.
        /// </summary>
        /// <param name="movement">The movement flags to disable.</param>
        public void DisableMove(CameraMovement movement)
        {
            m_Movement &= (~movement);
        }

        /// <summary>
        /// Converts camera to string.
        /// </summary>
        /// <returns>The string value.</returns>
        public override string ToString()
        {
            return "Camera";
        }
        #endregion

        #region Fields
        private float m_Fov;
        private float m_AspectRatio;
        private float m_NearZ;
        private float m_FarZ;
        private float m_RotationX = -2.5f;
        private float m_RotationY = 0.5f;
        private Vector3 m_Target;
        private Vector3 m_Direction = Vector3.UnitX;
        private Vector3 m_Up;
        private Vector3 m_Position;
        private Matrix m_Projection;
        private Matrix m_View;
        private bool m_Perspective = true;
        private CameraSpeed m_Speed = CameraSpeed.Speed1;
        private CameraMovement m_Movement = CameraMovement.None;
        #endregion
    }
}
