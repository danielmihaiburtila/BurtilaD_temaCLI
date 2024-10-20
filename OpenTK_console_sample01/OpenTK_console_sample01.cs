using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_console_sample01
{
    class SimpleWindow : GameWindow
    {
        // Variabile pentru poziția triunghiului
        float trianglePosX = 0.0f;
        float trianglePosY = 0.0f;

        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            MouseMove += Mouse_Move;
        }

        // Controlul prin tastatură: tastele Left și Right
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;

            // Mutare stânga/dreapta a triunghiului
            if (e.Key == Key.Left)
                trianglePosX -= 0.1f;
            if (e.Key == Key.Right)
                trianglePosX += 0.1f;
        }

        // Controlul poziției triunghiului prin mouse
        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            // Convertim coordonatele mouse-ului la sistemul de coordonate OpenGL (-1.0, 1.0)
            trianglePosX = (e.X - (Width / 2.0f)) / (Width / 2.0f);
            trianglePosY = ((Height / 2.0f) - e.Y) / (Height / 2.0f);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-5.0, 5.0, -5.0, 1.0, 0.0, 4.0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Logică de actualizare aici, dacă e necesar
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Randare triunghi
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.MidnightBlue);
            GL.Vertex2(-1.0f + trianglePosX, 1.0f + trianglePosY);  // Poziție ajustată
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(0.0f + trianglePosX, -1.0f + trianglePosY);  // Poziție ajustată
            GL.Color3(Color.Ivory);
            GL.Vertex2(1.0f + trianglePosX, 1.0f + trianglePosY);   // Poziție ajustată

            GL.End();

            SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
