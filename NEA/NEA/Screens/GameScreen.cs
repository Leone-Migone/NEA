
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;
using System.Numerics;
using System.Threading;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Raylib_CsLo.InternalHelpers;
using System.Timers;
using System.Collections;
using System.ComponentModel;
using Microsoft.Toolkit.HighPerformance;

namespace NEA.Screens
{
    public class GameScreen : IGameScreen
    {

        private IGameScreenManager m_screenManager;
        private Track gameTrack;
        private PlayerCar gameCar;
        private Rectangle rect2;
        private CarAI p2Bot;
        private PlayerCar p2Car;
        private bool playCPU;
        private Vector2 p2position;
        private CheckpointManager checkpointManager;
        public List<Checkpoint> checkpointList;
        // private CarAI botCar;
        private Vector2 gridPosition;
        private Camera2D p1camera;
        private Camera2D p2camera;
        private Rectangle startcellRect;
 
        public RenderTexture screenCamera1 = Raylib.LoadRenderTexture(1920 / 2, 1080);
        public RenderTexture screenCamera2 = Raylib.LoadRenderTexture(1920 / 2, 1080);
        private Rectangle splitScreenRect;
        static double startingTime;
        private double p1startingTime;
        private double p2startingTime;
        private double p1currentTime;
        private double p2currentTime;
        private double p1bestLap = -1;
        private double p2bestLap = -1;

        public const int TrackXOffset = 180;
        public const float cellScale = 1.60714286f;
        public const int scaledCellWidth = 180;
        private int laps = 5;
        private int p1lapsdone = 0;
        private int p2lapsdone = 0;

        public int Laps { get => laps; }
        public int P1lapsdone { get => p1lapsdone; set => p1lapsdone = value; }
        public int P2lapsdone { get => p2lapsdone; set => p2lapsdone = value; }


        public GameScreen(Track track, PlayerCar vehicle, PlayerCar p2Vehicle, IGameScreenManager gameScreenManager, CarAI botCar, bool playBOT)
        {

            m_screenManager = gameScreenManager;
            gameTrack = track;
            gameCar = vehicle;
            playCPU = playBOT;
            p2Bot = botCar;
            p2Car = p2Vehicle;
            startingTime = Raylib.GetTime();



            p1startingTime = Raylib.GetTime();
            p2startingTime = Raylib.GetTime();

            if (playCPU)
            {
                p2position = new Vector2(p2Bot.Position.X, p2Bot.Position.Y);
            }
            else
            {
                p2position = new Vector2(p2Car.Position1.X, p2Car.Position1.Y);
            }

            p2camera.target = p2position;

            p1camera.target = new Vector2(gameCar.Position1.X, gameCar.Position1.Y);




            checkpointList = gameTrack.GenerateCheckpoints();
            checkpointManager = new CheckpointManager(checkpointList, Laps);

            startcellRect = checkpointList.ElementAt(1).CheckRect;

            p1camera.zoom = 3.5f;
            p2camera.zoom = 3.5f;





            splitScreenRect = new Rectangle(0.0f, 0.0f, (float)screenCamera1.texture.width, (float)-screenCamera1.texture.height);





            Raylib.SetWindowSize(1920, 1080);
            Raylib.ToggleFullscreen();

        }

        public void Dispose()
        {

        }

        public void Draw()
        {
         
            //draws split screen cars and HUD

            Raylib.BeginTextureMode(screenCamera1);
            Raylib.ClearBackground(Raylib.GREEN);
            Raylib.BeginMode2D(p1camera);
            gameTrack.TrackGrid.Draw(TrackXOffset, (int)((float)Cell.width * cellScale), cellScale, 0);
            gameCar.Draw();

            if (playCPU)
            {
                p2Bot.Draw();
            }
            else
            {
                p2Car.Draw();
            }
            Raylib.EndMode2D();

            Raylib.EndTextureMode();


            Raylib.BeginTextureMode(screenCamera2);
            Raylib.ClearBackground(Raylib.GREEN);
            Raylib.BeginMode2D(p2camera);

            gameTrack.TrackGrid.Draw(TrackXOffset, (int)((float)Cell.width * cellScale), cellScale, 0);
            gameCar.Draw();
            // Initialize AI position or second player position based on user choice in the previous screen
            if (playCPU)
            {
                p2Bot.Draw();
            }
            else
            {
                p2Car.Draw();
            }

            Raylib.EndMode2D();

            Raylib.EndTextureMode();


            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            Raylib.DrawTextureRec(screenCamera1.texture, splitScreenRect, new Vector2(0, 0), Raylib.WHITE);
            Raylib.DrawTextureRec(screenCamera2.texture, splitScreenRect, new Vector2(1980 / 2 - 25, 0), Raylib.WHITE);

            Raylib.DrawText("BEST LAP:" + MathF.Round((float)p2bestLap, 1), 1980 - 285, 125, 30, Raylib.BLACK);
            Raylib.DrawText("BEST LAP:" + MathF.Round((float)p1bestLap, 1), 1980 / 2 - 260, 125, 30, Raylib.BLACK);

            Raylib.DrawTexture(Program.Laps, 1980 / 2 - 250, 75, Raylib.WHITE);
            Raylib.DrawText(P1lapsdone + "/" + Laps, 1980 / 2 - 250 + 85, 82, 30, Raylib.BLACK);
            Raylib.DrawTexture(Program.Laps, 1980 - 275, 75, Raylib.WHITE);
            Raylib.DrawText(P2lapsdone + "/" + Laps, 1980 - 250 + 85 - 25, 82, 30, Raylib.BLACK);

            Raylib.DrawTexture(Program.lapTime, 1980 / 2 - 250, 25, Raylib.WHITE);
            Raylib.DrawText(Math.Round(p1currentTime, 2).ToString(), 1980 / 2 - 250 + 97, 32, 30, Raylib.BLACK);
            Raylib.DrawTexture(Program.lapTime, 1980 - 275, 25, Raylib.WHITE);
            Raylib.DrawText(Math.Round(p2currentTime, 2).ToString(), 1980 - 275 + 97, 32, 30, Raylib.BLACK);

            Raylib.DrawRectangle(1980 / 4 - 120, 950, (int)gameCar.Boost * 3, 75, Raylib.YELLOW);
            Raylib.DrawTextureEx(Program.boostFrame, new Vector2(1980 / 4 - 200, 700), 0, 4, Raylib.WHITE);
            if (playCPU != true)
            {
                Raylib.DrawRectangle(1980 / 2 + 1980 / 4 - 120, 950, (int)p2Car.Boost * 3, 75, Raylib.YELLOW);
                Raylib.DrawTextureEx(Program.boostFrame, new Vector2(1980 / 2 + 1980 / 4 - 200, 700), 0, 4, Raylib.WHITE);
            }









        }

        //Checks when a lap is completed by each of the two players
        private void LapCollisions()
        {

            //Checks if the first player is playing a bot or another user to decide which rectangle to use for collisions 
            if (playCPU)
            {
                rect2 = p2Bot.CarRect;
            }
            else
            {
                rect2 = p2Car.PlayerRect;
            }
            //checks for checkpoint collisions
            if (checkpointManager.P1Queue.Count() != 0 && Raylib.CheckCollisionRecs(gameCar.PlayerRect, checkpointManager.P1Queue.First().CheckRect))
            {

                checkpointManager.p1CheckpointPassed();


            }
            if (checkpointManager.P2Queue.Count() != 0 && Raylib.CheckCollisionRecs(rect2, checkpointManager.P2Queue.First().CheckRect))
            {


                checkpointManager.p2CheckpointPassed();


            }

            //checks if the lap is completed and if all the laps are completed by one the user shows the winner screen
            if (P2lapsdone == Laps && Raylib.CheckCollisionRecs(rect2, startcellRect) && checkpointManager.p2IsLapValid() == true)
            {
                //Raylib.ToggleFullscreen();
                m_screenManager.PushScreen(new WinnerScreen("P2", m_screenManager));

                Raylib.SetWindowSize(1280, 720);
            }
            else if (P1lapsdone == Laps && Raylib.CheckCollisionRecs(gameCar.PlayerRect, startcellRect) && checkpointManager.p1IsLapValid() == true)
            {
                m_screenManager.PushScreen(new WinnerScreen("P1", m_screenManager));

                //Raylib.ToggleFullscreen();
                Raylib.SetWindowSize(1280, 720);
            }
            else if (P2lapsdone != Laps && Raylib.CheckCollisionRecs(rect2, startcellRect) && checkpointManager.p2IsLapValid() == true)
            {
                P2lapsdone++;
                if (p2currentTime <= p2bestLap || p2bestLap < 0)
                {
                    p2bestLap = p2currentTime;
                }
                p2startingTime = p1currentTime + p1startingTime;

            }

            else if (P1lapsdone != Laps && Raylib.CheckCollisionRecs(gameCar.PlayerRect, startcellRect) && checkpointManager.p1IsLapValid() == true)
            {
                P1lapsdone++;
                if (p1currentTime <= p1bestLap || p1bestLap < 0)
                {
                    p1bestLap = p1currentTime;
                }
                p1startingTime = p1currentTime + p1startingTime;
            }

        }
        /// <summary>
        /// Collision between the two cars
        /// </summary>
        private void Collision()
        {
            //Checks if the first player is playing a bot or another user to decide which rectangle to use for collisions 
            if (playCPU)
            {
                rect2 = p2Bot.CarRect;
            }
            else
            {
                rect2 = p2Car.PlayerRect;
            }

            if (Raylib.CheckCollisionRecs(gameCar.PlayerRect, rect2))
            {

                Vector2 p1Origin = new Vector2(gameCar.PlayerRect.x + gameCar.PlayerRect.width / 2, gameCar.PlayerRect.y + gameCar.PlayerRect.height / 2);
                Vector2 p2Origin = new Vector2(rect2.x + rect2.width / 2, rect2.y + rect2.height / 2);
                Vector2 originVec = Vector2.Normalize(p1Origin - p2Origin);
                gameCar.CarVelocity += originVec;
                p2Car.CarVelocity -= originVec;

            }


        }
        /// <summary>
        /// Collision between cars and the Map boundaries
        /// </summary>
        public void MapLimits()
        {

            Vector2 p1Origin = new Vector2(gameCar.PlayerRect.x + gameCar.PlayerRect.width / 2, gameCar.PlayerRect.y + gameCar.PlayerRect.height / 2);
            Vector2 p2Origin = new Vector2(p2Car.PlayerRect.x + p2Car.PlayerRect.width / 2, p2Car.PlayerRect.y + p2Car.PlayerRect.height / 2);

            Vector2.Normalize(p1Origin);

            

            if (Raylib.CheckCollisionRecs(gameCar.PlayerRect, new Rectangle(0, 0, 190, 1080)) == true)
            {

                gameCar.CarVelocity = new Vector2(gameCar.CarVelocity.X + 3, gameCar.CarVelocity.Y);
            }
            if (Raylib.CheckCollisionRecs(gameCar.PlayerRect, new Rectangle(1790, 0, 120, 1080)) == true)
            {
                gameCar.CarVelocity = new Vector2(gameCar.CarVelocity.X - 3, gameCar.CarVelocity.Y);

            }

            if (Raylib.CheckCollisionRecs(p2Car.PlayerRect, new Rectangle(0, 0, 190, 1080)) == true)
            {

                p2Car.CarVelocity = new Vector2(p2Car.CarVelocity.X + 3, p2Car.CarVelocity.Y);

            }
            if (Raylib.CheckCollisionRecs(p2Car.PlayerRect, new Rectangle(1790, 0, 120, 1080)) == true)
            {
                p2Car.CarVelocity = new Vector2(p2Car.CarVelocity.X - 3, p2Car.CarVelocity.Y);
            }


            if (Raylib.CheckCollisionRecs(gameCar.PlayerRect, new Rectangle(0, 0, 1920, 10)) == true)
            {
                gameCar.CarVelocity = new Vector2(gameCar.CarVelocity.X, gameCar.CarVelocity.Y + 3);
            }
            if (Raylib.CheckCollisionRecs(gameCar.PlayerRect, new Rectangle(0, 1080 - 10, 1920, 190)) == true)
            {
                gameCar.CarVelocity = new Vector2(gameCar.CarVelocity.X, gameCar.CarVelocity.Y - 3);
            }


            if (Raylib.CheckCollisionRecs(p2Car.PlayerRect, new Rectangle(0, 0, 1920, 10)) == true)
            {
                p2Car.CarVelocity = new Vector2(p2Car.CarVelocity.X, p2Car.CarVelocity.Y + 3);
            }
            if (Raylib.CheckCollisionRecs(p2Car.PlayerRect, new Rectangle(0, 1920 - 190, 1920, 190)) == true)
            {
                p2Car.CarVelocity = new Vector2(p2Car.CarVelocity.X, p2Car.CarVelocity.Y - 3);
            }


        }
        public void HandleInput()
        {

        }




        public void Update()
        {




            if (playCPU)
            {
                p2position = new Vector2(p2Bot.Position.X, p2Bot.Position.Y);
            }
            else
            {
                p2position = new Vector2(p2Car.Position1.X, p2Car.Position1.Y);
            }

            p1camera.target = new Vector2(gameCar.Position1.X, gameCar.Position1.Y);
            p1camera.offset = new Vector2(1920 / 4, 1080 / 2);
            p2camera.target = p2position;
            p2camera.offset = new Vector2(1920 / 4, 1080 / 2);

            gameCar.Update();
            if (playCPU)
            {
                p2Bot.Update();
            }
            else
            {
                p2Car.Update();
            }

            Collision();
            LapCollisions();
            p1currentTime = Raylib.GetTime() - p1startingTime;
            p2currentTime = Raylib.GetTime() - p2startingTime;
            MapLimits();
            p1GroundCollision();
            p2GroundCollision();

        }



        /// <summary>
        /// Calculates the corrispondent cell in the grid from the position in the screen, and returns it
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 PosToGrid(Vector2 position)
        {

            position.X = (int)((position.X - 180) / 180);
            position.Y = (int)((position.Y) / 180);
            return position;
        }

       

        /// <summary>
        /// Checks if player 1 goes out of the road and apply braking in case
        /// </summary>
        public void p1GroundCollision()
        {
            Vector2 temp;
            temp = PosToGrid(gameCar.Position1);
            if (gameTrack.TrackGrid.GridArray[(int)temp.X, (int)temp.Y].Road == 0)
            {
                gameCar.CarVelocity = new Vector2(gameCar.CarVelocity.X * (1.0f - gameCar.CarBrake), gameCar.CarVelocity.Y * (1.0f - gameCar.CarBrake));
            }

        }

        /// <summary>
        /// Checks if player 2 goes out of the track and in case apply braking
        /// </summary>
        public void p2GroundCollision()
        {
            Vector2 temp;
            temp = PosToGrid(p2Car.Position1);
            if (gameTrack.TrackGrid.GridArray[(int)temp.X, (int)temp.Y].Road == 0)
            {


                p2Car.CarVelocity = new Vector2(p2Car.CarVelocity.X * (1.0f - p2Car.CarBrake), p2Car.CarVelocity.Y * (1.0f - p2Car.CarBrake));
            }

        }

      


    }
    
}
