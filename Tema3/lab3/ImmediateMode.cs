using OpenTK;
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
