using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
   public class CarAI
   {
        // List of waypoints that define the car's path
        private List<Waypoint> waypoints;
        //current waypoint index that the car is targeting
        private int currentWaypoint = 0;

        private Vector2 waypVector;
        Vector2 distVector;
        //car properties
        private Rectangle carRect;
        
        private float maxForce;
        private float maxSpeed;
        private Vector2 velocity;
        private Vector2 acceleration = Vector2.Zero;
        private Vector2 steer = Vector2.Zero;
        private Vector2 position = Vector2.Zero;
        private Vector2 carCenter;
        private Texture carTexture;
        private float carRotation;

        public Rectangle CarRect { get => carRect; set => carRect = value; }
        public float MaxForce { get => maxForce; set => maxForce = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Vector2 Acceleration { get => acceleration; set => acceleration = value; }
        public Vector2 Steer { get => steer; set => steer = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Vector2 CarCenter { get => carCenter; set => carCenter = value; }
        public Texture CarTexture { get => carTexture; set => carTexture = value; }
        public float CarRotation { get => carRotation; set => carRotation = value; }

        public CarAI(Vector2 startPosition, Texture texture , List<Waypoint> waypoints) //: base(startPosition, texture)
        {
            position.X = startPosition.X * 180 + 270;
            position.Y = startPosition.Y * 180 + 90 + 10;
            CarTexture = texture;
            CarCenter = new Vector2(CarTexture.width / 6, CarTexture.height / 6);
            this.waypoints = waypoints;
            MaxSpeed = 4f;
            MaxForce = 3.0f;
          
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            CarRect = new Rectangle();
           
        }
        public void Draw()
        {
            
            CarRotation = (float)(Math.Atan2(Velocity.Y, Velocity.X)/MathF.PI)*180 + 90;
            
            

            // Calculate the drawing position
            Vector2 drawCarPos = new Vector2(Position.X - CarCenter.X, Position.Y - CarCenter.Y);

            // Draw the car using DrawTexturePro

            Raylib.DrawTexturePro(CarTexture, new Rectangle(0, 0, CarTexture.width, CarTexture.height), new Rectangle(drawCarPos.X, drawCarPos.Y, CarTexture.width / 3, CarTexture.height / 3), CarCenter, CarRotation, Raylib.WHITE);

            

        }

        /// <summary>
        /// Movement system for the cpu car
        /// </summary>
        public void Move()
        {

            //calcualtes next waypoint position
            int nextWaypointX = waypoints.ElementAt((currentWaypoint + 1) % waypoints.Count ).x * 180 + 180+ 90;
            int nextWaypointY = waypoints.ElementAt((currentWaypoint + 1) % waypoints.Count).y * 180 + 90;


            Rectangle temp = new Rectangle(nextWaypointX, nextWaypointY, 180, 180);

            CarRect = new (Position.X, Position.Y, CarTexture.width / 3, CarTexture.height / 3);
            
            waypVector = new Vector2(nextWaypointX,nextWaypointY);
            Seek(waypVector);

            if (Raylib.CheckCollisionCircleRec(new Vector2 (nextWaypointX, nextWaypointY),10 , CarRect) == true)
            {
                currentWaypoint = (currentWaypoint + 1)% waypoints.Count;
            }

            
            //NextVector();

           
        }

        public void ApplyForce(Vector2 force)
        {
            Acceleration += force;
        }
        /// <summary>
        /// switches to the vector guiding to the next checkpoint
        /// </summary>
        public void NextVector()
        {
            
            distVector = new Vector2((waypoints.ElementAt((currentWaypoint + 1) % waypoints.Count).x * 180 + 90 + 180) - Position.X, (waypoints.ElementAt((currentWaypoint + 1) % waypoints.Count).y * 180 + 90) - Position.Y);
        }
        /// <summary>
        /// calculate the vector that the cpu car will do to reach the next waypoint
        /// </summary>
        /// <param name="target"></param>
        public void Seek(Vector2 target)
        {
            Vector2 desired = Vector2.Subtract(target,Position);
            desired = Vector2.Normalize(desired) * MaxSpeed;
            Steer = Vector2.Subtract(desired, Velocity);
            Steer = Vector2.Clamp(Steer, Vector2.Negate(new Vector2(MaxForce, MaxForce)), new Vector2(MaxForce, MaxForce));
            ApplyForce(Steer);
        }

        public void Update()
        {
             Move();
             Velocity += Acceleration;
             Velocity = Vector2.Clamp(Velocity,Vector2.Negate(new Vector2(MaxSpeed,MaxSpeed)), new Vector2(MaxSpeed, MaxSpeed));
             Position += Velocity;
             Acceleration *= 0;
            //calculates the rotation of the rectangle associated to the car based on the car rotation, so we can be more accurate with collision
            int moddedrotation = (int)CarRotation % 360;
            if (moddedrotation > 360)
            {
                moddedrotation = moddedrotation % 360;
            }
            if (moddedrotation < 45 || moddedrotation > 135 && moddedrotation < 225 || moddedrotation > 315 )
            {
                CarRect = new Rectangle(Position.X - CarCenter.X - CarTexture.width / 6, Position.Y - CarCenter.Y - CarTexture.height / 6, CarTexture.width / 3, CarTexture.height / 3);

            }
            else
            {
                CarRect = new Rectangle(Position.X - CarCenter.X - CarTexture.height / 6, Position.Y - CarCenter.Y - CarTexture.width / 6, CarTexture.height / 3, CarTexture.width / 3);

            }





        }
    }
}
