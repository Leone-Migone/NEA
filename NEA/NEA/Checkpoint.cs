using System;
using System.Collections.Generic;

using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;

namespace NEA
{
    [Serializable]
    public class Checkpoint
    {
        //rectangle for collision area of checkpoint
        private Rectangle checkRect = new Rectangle();


        public Checkpoint(Rectangle checkRect)
        {
            this.CheckRect = checkRect;
           
        }

        public Rectangle CheckRect { get => checkRect; set => checkRect = value; }
    }
}
