using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

sealed class GameWindow : OpenTK.GameWindow
{
    public const string WINDOW_TITLE = "6502sharp - NES Emulator";

    public GameWindow()
        : base(256, 240, GraphicsMode.Default, WINDOW_TITLE)
    {
        Console.WriteLine("Renderer: " + GL.GetString(StringName.Renderer));
        Console.WriteLine("OpenGL Version: " + GL.GetString(StringName.Version));
    }

    protected override void OnResize(EventArgs e)
    {
        GL.Viewport(0, 0, this.Width, this.Height);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        GL.ClearColor(Color4.Black);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.Begin(BeginMode.Triangles);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex2(0.0f, 1.0f);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex2(0.87f, -0.5f);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex2(-0.87f, -0.5f);
        GL.End();

        this.SwapBuffers();
    }
}