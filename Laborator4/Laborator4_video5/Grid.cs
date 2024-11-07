using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator4_video5
{
    internal class Grid
    {
        private readonly Color colorisation;
        private bool visibility;


        //CONST

        private readonly Color GRIDCOLOR = Color.WhiteSmoke;
        private const int GRIDSTEP = 10;
        private const int UNITS = 50;
        private const int POINT_OFFSET = GRIDSTEP * UNITS;

        ///<summary>
        ///useful because otherwise the axes will be "drown" in overlaping grid lines...
        ///
        /// FIXED : the axes have line-width of 3, "drowning" the grid lines.
        /// </summary>
        /// 
        private const int MICRO_OFFSET = 0;

        public Grid()
        {
            colorisation = GRIDCOLOR;
            visibility = true;
        }

        public void Show()
        {
            visibility = true;
        }

        public void Hide()
        {
            visibility = false;
        }

        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public void Draw()
        {
            if (visibility)
            {

                GL.Begin(PrimitiveType.Lines);
                GL.Color3(colorisation);
                for (int i = -1 * GRIDSTEP * UNITS; i <= GRIDSTEP * UNITS; i += GRIDSTEP)
                {
                    //XZ plan darwing :parallel with Oz
                    GL.Vertex3(i + MICRO_OFFSET, 0, POINT_OFFSET);
                    GL.Vertex3(i + MICRO_OFFSET, 0, -1 * POINT_OFFSET);

                }
                for(int j=-1 * POINT_OFFSET;j<= POINT_OFFSET;j+=GRIDSTEP)
                {
                    GL.Vertex3(POINT_OFFSET, 0, j+MICRO_OFFSET);
                    GL.Vertex3(-1 * POINT_OFFSET, 0, j + MICRO_OFFSET);
                }
                GL.End();
            }
                
        }
    }
}
