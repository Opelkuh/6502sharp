using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace NES.Rendering
{
    class GameWindow : OpenTK.GameWindow
    {
        #region Constants
        public const string WINDOW_TITLE = "6502sharp - NES Emulator";

        private static float[] VERTICIES =
        {
            -1, -1,
             1, -1,
            -1,  1,
             1,  1,
        };

        private static ushort[] INDICIES =
        {
            0, 1, 2,
            2, 3, 1
        };

        private static string VERTEX_SHADER_PATH = @"src/Rendering/Shaders/vertex.glsl";
        private static string FRAGMENT_SHADER_PATH = @"src/Rendering/Shaders/fragment.glsl";
        #endregion

        #region OpenGL Variables
        private int VAO;
        private int VBO;
        private int EBO;
        private int shaderProgram;

        #endregion

        public GameWindow() : base(
            256, 240,
            GraphicsMode.Default,
            WINDOW_TITLE,
            GameWindowFlags.Default,
            DisplayDevice.Default,
            3, 3,
            GraphicsContextFlags.Debug
        )
        {
            Console.WriteLine("Renderer: " + GL.GetString(StringName.Renderer));
            Console.WriteLine("OpenGL Version: " + GL.GetString(StringName.Version));
        }

        protected override void OnLoad(EventArgs e)
        {
            // VAO
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // VBO
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, VERTICIES.Length * sizeof(float), VERTICIES, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // EBO
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, INDICIES.Length * sizeof(ushort), INDICIES, BufferUsageHint.StaticDraw);

            // shaders
            Shader vertex = new Shader(ShaderType.VertexShader, VERTEX_SHADER_PATH);
            Shader fragment = new Shader(ShaderType.FragmentShader, FRAGMENT_SHADER_PATH);

            shaderProgram = new ShaderProgram()
                .Add(vertex)
                .Add(fragment)
                .Finish();

            // unbind VAO
            GL.BindVertexArray(0);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Clear
            GL.ClearColor(Color4.DeepPink);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Bind
            GL.UseProgram(shaderProgram);
            GL.BindVertexArray(VAO);

            // Draw
            GL.DrawElements(PrimitiveType.Triangles, INDICIES.Length, DrawElementsType.UnsignedShort, 0);

            SwapBuffers();
        }
    }
}