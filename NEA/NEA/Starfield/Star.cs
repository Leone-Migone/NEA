using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Starfield
{
    public class Star
    {
        private float x;
        private float y;
        private float z;

        private float previousZ;
      
        public Star()
        {
            // 3D starting position of the star

            x = Raylib.GetRandomValue(-Program.screenwidth, Program.screenwidth);
            y = Raylib.GetRandomValue(-Program.screenheight, Program.screenheight);
            z = Raylib.GetRandomValue(0,Program.screenwidth);
            previousZ = z;
        }
        public void Update()
        {
            z = z - StarField.Speed;

            //If the star goes beyond the screen and so z is less than 1 it resets its position
            if (z < 1)
            {
                x = Raylib.GetRandomValue(-Program.screenwidth, Program.screenwidth);
                y = Raylib.GetRandomValue(-Program.screenheight, Program.screenheight);
                z = Program.screenwidth;
                previousZ = z;
            }
        }
        public void Draw()
        {

            // Calculate the screen position of the star by mapping the values
            int sx = (int)Map(x/z,0,1,0, Program.screenwidth);
            int sy = (int)Map(y/z, 0, 1, 0, Program.screenheight);

            float size = Map(z, 0, Program.screenwidth, 14, 0);

            // Calculate the previous screen position of the star for drawing the trail effect
            int px = (int)Map(x / previousZ, 0, 1, 0, Program.screenwidth);
            int py = (int)Map(y / previousZ, 0, 1, 0, Program.screenheight);

            previousZ = z;
            // Generate a random color for the star
            Random random = new Random();
            int r = random.Next(0,255);
            int g = random.Next(0,255);
            int b = random.Next(0,255);
            int a = random.Next(0,255);
            Color color = new Color(r,g,b,a);
            Raylib.DrawLineEx(new Vector2( (int)px + Program.screenwidth / 2, (int)py + Program.screenheight / 2) , new Vector2((int)sx + Program.screenwidth / 2, (int)sy + Program.screenheight / 2),3.5f, color);
        }

        public static float Map(float value, float low1, float high1,float low2, float high2)
        {
            //normalize value to be between 0 and  1
            float normalizedValue = (value - low1) / (high1 - low1);
            //now we map the value to the new range from 0 to 1
            float mappedValue = low2 + (normalizedValue * (high2 - low2));

            return mappedValue;
        }
    }
}
