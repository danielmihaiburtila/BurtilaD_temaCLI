using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

class TriangleApp : GameWindow
{
    private Vector3[] triangleVertices;
    private Color4[] vertexColors = new Color4[3];
    private float cameraAngleX = 0.0f;
    private float cameraAngleY = 0.0f;
    private int previousMouseX, previousMouseY;
    private int selectedVertex = 0;

    public TriangleApp(int width, int height, string title)
        : base(width, height, GraphicsMode.Default, title)
    {
        triangleVertices = LoadTriangleVertices("TextFile1.txt");
        vertexColors[0] = new Color4(1.0f, 0.0f, 0.0f, 1.0f); // Inițial, roșu pentru vârful 1
        vertexColors[1] = new Color4(0.0f, 1.0f, 0.0f, 1.0f); // Inițial, verde pentru vârful 2
        vertexColors[2] = new Color4(0.0f, 0.0f, 1.0f, 1.0f); // Inițial, albastru pentru vârful 3
        previousMouseX = Mouse.GetState().X;
        previousMouseY = Mouse.GetState().Y;
    }

    private Vector3[] LoadTriangleVertices(string path)
    {
        string[] lines = File.ReadAllLines(path);
        Vector3[] vertices = new Vector3[3];
        for (int i = 0; i < 3; i++)
        {
            string[] parts = lines[i].Split(',');
            vertices[i] = new Vector3(
                float.Parse(parts[0]),
                float.Parse(parts[1]),
                float.Parse(parts[2])
            );
        }
        return vertices;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f); // Fundal negru
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Blend); // Activam blending-ul pentru transparenta
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha); // Setam functia de blending
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, Width, Height);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Width / (float)Height, 0.1f, 100.0f);
        GL.LoadMatrix(ref perspective);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        // Selectarea vârfului activ cu tastele 1, 2 și 3
        if (Keyboard.GetState().IsKeyDown(Key.Number1)) selectedVertex = 0;
        if (Keyboard.GetState().IsKeyDown(Key.Number2)) selectedVertex = 1;
        if (Keyboard.GetState().IsKeyDown(Key.Number3)) selectedVertex = 2;

        // Controlul culorii pentru vârful selectat
        if (Keyboard.GetState().IsKeyDown(Key.R)) vertexColors[selectedVertex].R = MathHelper.Clamp(vertexColors[selectedVertex].R + 0.01f, 0.0f, 1.0f);
        if (Keyboard.GetState().IsKeyDown(Key.G)) vertexColors[selectedVertex].G = MathHelper.Clamp(vertexColors[selectedVertex].G + 0.01f, 0.0f, 1.0f);
        if (Keyboard.GetState().IsKeyDown(Key.B)) vertexColors[selectedVertex].B = MathHelper.Clamp(vertexColors[selectedVertex].B + 0.01f, 0.0f, 1.0f);
        if (Keyboard.GetState().IsKeyDown(Key.T)) vertexColors[selectedVertex].A = MathHelper.Clamp(vertexColors[selectedVertex].A - 0.01f, 0.0f, 1.0f);
        if (Keyboard.GetState().IsKeyDown(Key.O)) vertexColors[selectedVertex].A = MathHelper.Clamp(vertexColors[selectedVertex].A + 0.01f, 0.0f, 1.0f);

        // Afișarea valorilor RGB în consolă la fiecare frame
        Console.Clear();
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Vârful {i + 1}: R = {vertexColors[i].R:F2}, G = {vertexColors[i].G:F2}, B = {vertexColors[i].B:F2}, A = {vertexColors[i].A:F2}");
        }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        if (Mouse.GetState().IsButtonDown(MouseButton.Left))
        {
            int deltaX = e.X - previousMouseX;
            int deltaY = e.Y - previousMouseY;

            cameraAngleX += deltaX * 0.1f;
            cameraAngleY += deltaY * 0.1f;

            previousMouseX = e.X;
            previousMouseY = e.Y;
        }
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();
        GL.Translate(0.0, 0.0, -5.0); // Mutam camera pe axa Z
        GL.Rotate(cameraAngleX, 0.0, 1.0, 0.0); // Rotim camera pe axa Y
        GL.Rotate(cameraAngleY, 1.0, 0.0, 0.0); // Rotim camera pe axa X

        GL.Begin(PrimitiveType.Triangles);
        for (int i = 0; i < 3; i++)
        {
            GL.Color4(vertexColors[i]);
            GL.Vertex3(triangleVertices[i]);
        }
        GL.End();

        SwapBuffers();
    }

    [STAThread]
    static void Main(string[] args)
    {
        using (TriangleApp app = new TriangleApp(800, 600, "OpenGL Triangle with Vertex Color Control"))
        {
            app.Run(60.0);
        }
    }
}

//Acest program desenează un triunghi cu un gradient de culoare în OpenGL, atribuind culori diferite fiecărui vârf și permitând utilizatorului să modifice dinamic valorile RGB și transparența pentru fiecare vârf folosind tastatura.
////Utilizatorul poate selecta un vârf și ajusta culorile pentru a observa efectul în timp real, iar programul afișează valorile actuale ale culorilor în consolă.
////Mișcarea camerei este controlată cu mouse-ul, permițând vizualizarea triunghiului din diferite unghiuri.