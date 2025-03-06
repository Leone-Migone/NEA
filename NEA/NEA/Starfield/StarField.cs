using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;

namespace NEA.Starfield
{

    public class StarField
    {
        private static float speed;
        //array of stars that compone the starfield
        private Star[] stars = new Star[1000];

        public static float Speed { get => speed; set => speed = value; }

        public StarField()
        {
            Setup();
        }
        public void Setup()
        {
            //initialize all stars in the array
            for (int i = 0; i < stars.Length; i++)
            {
                 stars[i] = new Star();
                
               
            }
        }
        //draw starfield
        public void Draw()
        {
            // update star movement speed according to mouse x position
            Speed = Star.Map(Raylib.GetMouseX(),0,Program.screenwidth,0,55);
            for (int i = 0; i < stars.Length; i++)
            {
                
                stars[i].Update();

                stars[i].Draw();
            }
        }



    }
    

}
