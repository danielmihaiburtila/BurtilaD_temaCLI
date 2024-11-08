1. În OpenGL, ordinea de desenare a vertexurilor determină care față a poligonului este considerată vizibilă și poate fi fie în sens orar (clockwise - CW), fie în sens anti-orar (counter-clockwise - CCW). Implicit, OpenGL folosește ordinea anti-orară (CCW) pentru a desemna o față a poligonului ca fiind orientată către cameră (fața frontală).
   Această convenție este esențială pentru culling (eliminarea fețelor invizibile), deoarece OpenGL elimină automat fețele orientate invers, pe baza ordinii vertexurilor specificate.

ImediateMode.cs

private void DrawAxes() {

     //GL.LineWidth(3.0f);

     // Desenează axa Ox (cu roșu).
     GL.Begin(PrimitiveType.Lines);
     GL.Color3(Color.Red);
     GL.Vertex3(0, 0, 0);
     GL.Vertex3(XYZ_SIZE, 0, 0);
    /// GL.End();

     // Desenează axa Oy (cu galben).
     //GL.Begin(PrimitiveType.Lines);
     GL.Color3(Color.Yellow);
     GL.Vertex3(0, 0, 0);
     GL.Vertex3(0, XYZ_SIZE, 0); ;
     ///GL.End();

     // Desenează axa Oz (cu verde).
     //GL.Begin(PrimitiveType.Lines);
     GL.Color3(Color.Green);
     GL.Vertex3(0, 0, 0);
     GL.Vertex3(0, 0, XYZ_SIZE);
     GL.End();

}

2. Anti-aliasing este o tehnică utilizată în grafică digitală pentru a netezi marginile obiectelor și a reduce efectul de „dinți de fierăstrău” care apare din cauza pixelării. Funcționează prin amestecarea culorilor la marginea obiectelor, creând o tranziție mai graduală între margine și fundal, oferind astfel un aspect mai neted și mai natural. Există mai multe metode de anti-aliasing, precum MSAA (Multisample Anti-Aliasing) și FXAA (Fast Approximate Anti-Aliasing), fiecare având diferite avantaje în funcție de performanță și calitatea imaginii.

3. Comanda GL.LineWidth(float width) setează grosimea liniilor desenate în OpenGL, specificând lățimea acestora în pixeli. Astfel, orice linie desenată după apelarea acestei comenzi va avea grosimea indicată, ceea ce poate fi util pentru evidențierea sau accentuarea unor elemente grafice.

Comanda GL.PointSize(float size) setează dimensiunea punctelor desenate, specificând diametrul acestora în pixeli. Orice punct desenat după apelarea acestei comenzi va avea dimensiunea specificată, permițând astfel modificarea aspectului acestora pentru diverse efecte vizuale.

Ambele comenzi trebuie apelate în afara unei zone GL.Begin()/GL.End(). Dacă sunt apelate în interiorul unei astfel de secțiuni, nu vor avea efect, deoarece setările de stare OpenGL (precum lățimea liniilor sau dimensiunea punctelor) trebuie stabilite înainte de a începe desenarea.

4. LineLoop:

Conectează fiecare punct consecutiv cu o linie și, în final, leagă ultimul punct înapoi la primul. Rezultatul este un contur închis, util pentru desenarea poligoanelor complete (ex. un pătrat sau un hexagon).

LineStrip:

Conectează fiecare punct consecutiv cu o linie, dar nu închide forma, adică nu leagă ultimul punct cu primul. Este util pentru desenarea formelor deschise sau a poliliniilor.

TriangleFan:

Desenează triunghiuri având un punct central comun (primul punct specificat) și le conectează cu fiecare punct nou adăugat, formând o structură în formă de evantai. Este ideal pentru desenarea formelor circulare sau conice (ex. un disc).

TriangleStrip:

Desenează o serie de triunghiuri conectate, fiecare triunghi nou folosind ultimele două puncte ale celui anterior și un punct nou. Este eficient pentru a crea benzi sau panglici continue, utilizând un număr minim de puncte.

5. using OpenTK;
   using OpenTK.Graphics.OpenGL;
   using System;
   using System.Collections.Generic;
   using System.Data;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

namespace lab3
{
internal class ImmediateMode:GameWindow
{

        public ImmediateMode():base(800,600)
        {
            VSync = VSyncMode.On;


        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Green);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);



        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width / 2, Height);


        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }


    }

}

6.  Urmăriți aplicația „shapes.exe” din tutorii OpenGL Nate Robbins.
    De ce este importantă utilizarea de culori diferite (în gradient sau
    culori selectate per suprafață) în desenarea obiectelor 3D? Care este
    avantajul?

7.  Un gradient de culoare este o tranziție lină între culori diferite pe o suprafață. În OpenGL, se obține atribuind culori diferite fiecărui vârf al unei forme (de exemplu, un triunghi). OpenGL interpolează automat culorile între vârfuri, creând efectul de gradient. De exemplu, dacă desenăm un triunghi și setăm roșu pentru primul vârf, verde pentru al doilea și albastru pentru al treilea, OpenGL va amesteca aceste culori între vârfuri, rezultând o trecere graduală a culorilor pe suprafața triunghiului.

8&9

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

10. Dacă folosești culori diferite pentru fiecare vârf atunci când desenezi un triunghi sau o linie în modul strip, culorile se vor amesteca treptat între vârfuri, creând un efect de gradient. Aceasta înseamnă că forma va avea o trecere lină de culori de la un vârf la altul.
