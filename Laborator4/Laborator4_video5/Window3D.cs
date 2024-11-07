using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator4_video5
    ///<summary>
    ///The graphic window. Contains the canvas (viewport to be draw).
    /// </summary>
{
    internal class Window3D : GameWindow
    {
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
        private readonly Randomizer rando;
        private readonly Axes ax;
        private readonly Grid grid;
        private readonly Camera3DIsometric cam;

        private List<Objectoid> rainOfObjects;
        private bool GRAVITY = true;


        //DEFAULTS
        private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);

        /// <summary>
        /// Parametrised constructor. Invokes the anti-aliasing engine. All inits are placed here, for convenince.
        /// </summary>
        /// 

        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            //inits

            rando = new Randomizer();
            ax = new Axes();
            grid = new Grid();
            cam = new Camera3DIsometric();

            rainOfObjects = new List<Objectoid>();
            

            DisplayHelp();

        }

        /// <summary>
        /// OnLoad method. Part of the control loop pf the OpenTK API. Executed only once.
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }



        /// <summary>
        /// Onresize method. Part of the control lop of the OpenTK API. Executed at least once. (after OnLoad()).
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //set background

            GL.ClearColor(DEFAULT_BKG_COLOR);

            //set viewport

            GL.Viewport(0, 0, this.Width, this.Height);

            //set perspective

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 512);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref  perspective);

            //set the eye
            cam.SetCamera();

        }

        

        /// <summary>
        /// OPUpdateFrame() method. Part of the control loop of the OpenTK API. Executed periodically, with a frequency determined when launching
        /// the graphics window (<see cref="GameWindow.Run(double, double)"/>). in this case should be 30.00 (if unmodified).
        /// 
        /// All logic should reside here!
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //LOGIC CODE

            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();
            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
                
            }
            if (currentKeyboard[Key.Escape])
            {
                Exit();

            }
            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                GL.ClearColor(DEFAULT_BKG_COLOR);
                ax.Show(); 
                grid.Show();

            }
            if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
            {
                ax.ToggleVisibility();

            }
            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                GL.ClearColor(rando.RandomColor());

            }
            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                grid.ToggleVisibility();

            }

            //camera control (isometric mode)

            if (currentKeyboard[Key.W])
            {
                cam.MoveForward();
            }
            if (currentKeyboard[Key.S])
            {
                cam.MoveBackward();
            }
            if (currentKeyboard[Key.A])
            {
                cam.MoveLeft();
            }
            
            if (currentKeyboard[Key.D])
            {
                cam.MoveRight();


            }
            if (previousKeyboard[Key.Q])
            {
                cam.MoveUp();

            }
            if (previousKeyboard[Key.E])
            {
                cam.MoveDown();

            }

            //object spawn
            if (currentMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                rainOfObjects.Add(new Objectoid(GRAVITY));
            }
            //object spam cleanup
            if (currentMouse[MouseButton.Right] && !previousMouse[MouseButton.Right])
            {
                rainOfObjects.Clear();
            }

            //switch gravity

            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                GRAVITY = !GRAVITY;
            }
            //update falling logic
         
            foreach (Objectoid obj in rainOfObjects)
            {
                obj.UpdatePosition(GRAVITY);
            }


            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;

            //END LOGIC CODE
        }



        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            //RENDER CODE

            grid.Draw();
            ax.Draw();

            foreach (Objectoid obj in rainOfObjects)
            {
                obj.Draw();

            }

           

            //END RENDER CODE

            SwapBuffers();
        }
        


























        public void DisplayHelp()
        {
            Console.WriteLine("=== MENIU ====");
            Console.WriteLine("H. meniu");
            Console.WriteLine("ESC. parasire aplicatie");
            Console.WriteLine("K. schimbare vizibilitate sistem de axe");
            Console.WriteLine("R. reseteaza scena la valori implicite");
            Console.WriteLine("B. schimbare culoare de fundal");
            Console.WriteLine("V. schimbare vizibilitate grid");
            Console.WriteLine("(W,A,S,D,Q,E). deplasare camera (izometric)");
            Console.WriteLine("G. manipuleaza gravitatia");
            Console.WriteLine("(Mouse click stanga) - genereaza un nou obiect la o inaltime aleatoare ");
            Console.WriteLine("(Mouse click dreapta) - curata lista de obiecte");




        }




    }
}
