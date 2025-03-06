using NEA.Screens;
using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace NEA
{
    public class Vehicle 
    {




        private Texture carTexture;
        private Vector2 position1 = new Vector2(1280 / 2, 720 / 2);
        private Vector2 gridPos = new Vector2();


        private Vector2 carVelocity = new Vector2(0, 0);
        private float carRotation = 90f;
        private float carAccel = 0.12f;
        private float maxSpeed = 3.8f;
        private float carRotSpeed = 2.7f;
        private float friction = 0.025f;
        private float carBrake = 0.03f;

        private float boost = 0;
        private float maxboost = 100;

        public float speed { get; set; }
        public Vector2 Position { get => Position1;}
        public Texture CarTexture { get => carTexture; set => carTexture = value; }
        public Vector2 Position1 { get => position1; set => position1 = value; }
        public Vector2 GridPos { get => gridPos; set => gridPos = value; }
        public Vector2 CarVelocity { get => carVelocity; set => carVelocity = value; }
        public float CarRotation { get => carRotation; set => carRotation = value; }
        public float CarAccel { get => carAccel; set => carAccel = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
        public float CarRotSpeed { get => carRotSpeed; set => carRotSpeed = value; }
        public float Friction { get => friction; set => friction = value; }
        public float CarBrake { get => carBrake; set => carBrake = value; }
        public float Boost { get => boost; set => boost = value; }
        public float Maxboost { get => maxboost; set => maxboost = value; }


        public Vehicle(Vector2 startPosition, Texture texture)
        {

            CarTexture = texture;


        }

        public void Draw()
        {
            // divided by 4 since we scale down by 0.25
            Vector2 carCenter = new Vector2(CarTexture.width / 6, CarTexture.height / 6);

            // Calculate the drawing position
            Vector2 drawCarPos = new Vector2(Position.X - carCenter.X, Position.Y - carCenter.Y);

            // Draw the car using DrawTexturePro
            Raylib.DrawTexturePro(CarTexture, new Rectangle(0, 0, CarTexture.width, CarTexture.height), new Rectangle(drawCarPos.X, drawCarPos.Y, CarTexture.width/3, CarTexture.height/3), carCenter, CarRotation, Raylib.WHITE);



        }
        /// <summary>
        /// Movement system of each vehicle
        /// </summary>
        /// <param name="UP"></param>
        /// <param name="DOWN"></param>
        /// <param name="LEFT"></param>
        /// <param name="RIGHT"></param>
        /// <param name="BOOST"></param>
        public void Move(KeyboardKey UP,KeyboardKey DOWN,KeyboardKey LEFT, KeyboardKey RIGHT, KeyboardKey BOOST)
        {

            

            if (Raylib.IsKeyDown(RIGHT))
            {
                CarRotation += CarRotSpeed;
                if (Boost <= Maxboost)
                {
                    Boost = Boost + 0.2f;
                }
            }
            if (Raylib.IsKeyDown(LEFT))
            {
                CarRotation -= CarRotSpeed;
                if (Boost <= Maxboost)
                {
                    Boost = Boost +  0.2f;
                }
            }
            if (Raylib.IsKeyDown(UP))
            {
                // Calculate acceleration vector based on car rotation
                float accelerationX = (float)Math.Sin(CarRotation * (Math.PI / 180.0)) * CarAccel;
                float accelerationY = (float)Math.Cos(CarRotation * (Math.PI / 180.0)) * CarAccel;
                
                // Update car velocity
                carVelocity.X += accelerationX;
                carVelocity.Y -= accelerationY;

                float speed = RayMath.Vector2Length(CarVelocity);
                //imposing the max soeed
                if (speed > MaxSpeed)
                {
                    CarVelocity = RayMath.Vector2Normalize(CarVelocity);
                    carVelocity.X *= MaxSpeed;
                    carVelocity.Y *= MaxSpeed;
                }



            }
            else if (Raylib.IsKeyDown(DOWN))
            {
                // Apply braking
                carVelocity.X *= 1.0f - CarBrake;
                carVelocity.Y *= 1.0f - CarBrake;
            }
            //use boost
            if (Raylib.IsKeyDown(BOOST) && Boost > 0)
            {

                float boostTimer;
                float boostLenght = 5;
                //increase maxspeed while using the boost
                MaxSpeed = 7f;

                
                if (Boost > 0)
                {
                        CarAccel = CarAccel + 3f;
                        Boost = Boost - 2f;
                }
                    


            }
            else
            {
                
                while (CarAccel > 0.12f  || MaxSpeed > 3.7f)
                {
                    CarAccel -= 0.008f;
                    MaxSpeed -= 0.05f;
                }
                CarAccel = 0.12f;
                MaxSpeed = 3.7f;

            }
         


            // Apply friction
            carVelocity.X *= 1.0f - Friction;
            carVelocity.Y *= 1.0f - Friction;

            // Update car's position
            position1.X += CarVelocity.X;
            position1.Y += CarVelocity.Y;

           

        }
      


     






    }



}
