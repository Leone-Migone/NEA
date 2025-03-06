using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
    public class PlayerCar : Vehicle
    {

        private KeyboardKey UP;
        private KeyboardKey DOWN;
        private KeyboardKey LEFT;
        private KeyboardKey RIGHT;
        private KeyboardKey BOOST;

        

        Vector2 carCenter;
        private Rectangle playerRect = new Rectangle();

        public Rectangle PlayerRect { get => playerRect; set => playerRect = value; }

        public PlayerCar(KeyboardKey UP,KeyboardKey DOWN, KeyboardKey LEFT, KeyboardKey RIGHT,KeyboardKey BOOST, Vector2 startPosition, Texture texture) : base(startPosition, texture)
        {
           
            this.UP = UP;
            this.DOWN = DOWN;
            this.LEFT = LEFT;   
            this.RIGHT = RIGHT;
            this.BOOST = BOOST;

            Position1 = new Vector2(startPosition.X,startPosition.Y);
            CarVelocity = new Vector2(0, 0);
            CarTexture = texture;
            carCenter = new Vector2(CarTexture.width / 6, CarTexture.height / 6);
            PlayerRect = new Rectangle(Position1.X, Position1.Y, texture.height / 3, texture.width / 3);


        }


       
    



       

        public void Update()
        {
            //calculates the rotation of the rectangle associated to the car based on the car rotation, so we can be more accurate with collision 
            int moddedrotation = (int)CarRotation % 360;
            if (moddedrotation > 360)
            {
                moddedrotation = moddedrotation % 360;
            }
            if (moddedrotation < 45 || moddedrotation > 135 && moddedrotation < 225 || moddedrotation > 315)
            {
                PlayerRect = new Rectangle(Position1.X - carCenter.X - CarTexture.width / 6, Position1.Y - carCenter.Y - CarTexture.height / 6, CarTexture.width / 3, CarTexture.height / 3);

            }
            else
            {
                PlayerRect = new Rectangle(Position1.X - carCenter.X - CarTexture.height / 6, Position1.Y - carCenter.Y - CarTexture.width / 6, CarTexture.height / 3, CarTexture.width / 3);

            }
            
            Move(UP,DOWN,LEFT,RIGHT,BOOST);
        }
        
        public void Draw()
        {
            
            
            // Calculate the drawing position
            Vector2 drawCarPos = new Vector2(Position.X - carCenter.X, Position.Y - carCenter.Y);

            // Draw the car using DrawTexturePro

            Raylib.DrawTexturePro(CarTexture, new Rectangle(0, 0, CarTexture.width, CarTexture.height), new Rectangle(drawCarPos.X, drawCarPos.Y, CarTexture.width / 3, CarTexture.height / 3), carCenter, CarRotation, Raylib.WHITE);


        }



        




    }
}
