using Raylib_CsLo;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Screens
{
    public class CarSelectionScreen : IGameScreen
    {
        private IGameScreenManager m_gameScreen;
        public List<PlayerCar> p1_vehicles = new List<PlayerCar>();
        public List<PlayerCar> p2_vehicles = new List<PlayerCar>();
        bool playCPU = false;
        private int p1currentVehicle = 0;
        private int p2currentVehicle = 0;
        private Track track;
        private PlayerCar p1vehicle1;
        private PlayerCar p1vehicle2;
        private PlayerCar p1vehicle3;

        private PlayerCar p2vehicle1;
        private PlayerCar p2vehicle2;
        private PlayerCar p2vehicle3;
        private CarAI botCar;

        public int P1currentVehicle { get => p1currentVehicle; set => p1currentVehicle = value; }
        public int P2currentVehicle { get => p2currentVehicle; set => p2currentVehicle = value; }
        public Track Track { get => track; set => track = value; }
        public PlayerCar P1vehicle1 { get => p1vehicle1; set => p1vehicle1 = value; }
        public PlayerCar P1vehicle2 { get => p1vehicle2; set => p1vehicle2 = value; }
        public PlayerCar P1vehicle3 { get => p1vehicle3; set => p1vehicle3 = value; }
        public PlayerCar P2vehicle1 { get => p2vehicle1; set => p2vehicle1 = value; }
        public PlayerCar P2vehicle2 { get => p2vehicle2; set => p2vehicle2 = value; }
        public PlayerCar P2vehicle3 { get => p2vehicle3; set => p2vehicle3 = value; }
        public CarAI BotCar { get => botCar; set => botCar = value; }

        public CarSelectionScreen(IGameScreenManager gameScreenManager,Track currentTrack,Vector2 carstartingPos)
        {
            P1vehicle1 = new PlayerCar(KeyboardKey.KEY_W, KeyboardKey.KEY_S, KeyboardKey.KEY_A, KeyboardKey.KEY_D,KeyboardKey.KEY_LEFT_SHIFT, new Vector2(carstartingPos.X * 180 + 180 + 90, carstartingPos.Y*180 + 90 - 10), Program.carTexture);
            P1vehicle2 = new PlayerCar(KeyboardKey.KEY_W, KeyboardKey.KEY_S, KeyboardKey.KEY_A, KeyboardKey.KEY_D, KeyboardKey.KEY_LEFT_SHIFT, new Vector2(carstartingPos.X * 180 + 180 + 90, carstartingPos.Y * 180 + 90 - 10), Program.carTexture2);
            P1vehicle3 = new PlayerCar(KeyboardKey.KEY_W, KeyboardKey.KEY_S, KeyboardKey.KEY_A, KeyboardKey.KEY_D, KeyboardKey.KEY_LEFT_SHIFT, new Vector2(carstartingPos.X * 180 + 180 + 90, carstartingPos.Y * 180 + 90 - 10), Program.carTexture3);

            P2vehicle1 = new PlayerCar(KeyboardKey.KEY_UP, KeyboardKey.KEY_DOWN, KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT, KeyboardKey.KEY_RIGHT_CONTROL, new Vector2(carstartingPos.X * 180 + 180 + 90, carstartingPos.Y * 180 + 90 + 20), Program.carTexture);
            P2vehicle2 = new PlayerCar(KeyboardKey.KEY_UP, KeyboardKey.KEY_DOWN, KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT, KeyboardKey.KEY_RIGHT_CONTROL, new Vector2(carstartingPos.X * 180 + 180 + 90, carstartingPos.Y * 180 + 90 + 20), Program.carTexture2);
            P2vehicle3 = new PlayerCar(KeyboardKey.KEY_UP, KeyboardKey.KEY_DOWN, KeyboardKey.KEY_LEFT, KeyboardKey.KEY_RIGHT, KeyboardKey.KEY_RIGHT_CONTROL, new Vector2(carstartingPos.X * 180 + 180 + 90, carstartingPos.Y * 180 + 90 + 20), Program.carTexture3);

            BotCar = new CarAI(new Vector2(carstartingPos.X , carstartingPos.Y ), Program.carTexture3, currentTrack.Waypoints);
            Track = currentTrack;
            m_gameScreen = gameScreenManager;

            p1_vehicles.Add(P1vehicle1);
            p1_vehicles.Add(P1vehicle2);
            p1_vehicles.Add(P1vehicle3);

            p2_vehicles.Add(P2vehicle1);
            p2_vehicles.Add(P2vehicle2);
            p2_vehicles.Add(P2vehicle3);
        }

        public void Dispose()
        {
            
        }

        public void Draw()
        {
            //CAR SELECTION MENU
            Raylib.DrawTextureEx(Program.selectionP1,new Vector2( 1280 / 2 - (45 * 3 / 2) - 294,150),0f,2f,Raylib.WHITE);
            

            Raylib.DrawTextureEx(p1_vehicles[P1currentVehicle].CarTexture,new Vector2(1280/2 - (45*3/2) - 300 ,720/2 - (86*3/2)),0,3,Raylib.WHITE);
            Raylib.DrawTextureEx(Program.WASD,new Vector2(1280 / 2 - (45 * 3 / 2) - 300 -10, 720 / 2 - (86 * 3 / 2) - 200),0,1,Raylib.WHITE);
            if (P2currentVehicle != p2_vehicles.Count())
            {
                Raylib.DrawTextureEx(Program.Arrows, new Vector2(1280 / 2 - (45 * 3 / 2) + 300 - 10, 720 / 2 - (86 * 3 / 2) - 200), 0, 1, Raylib.WHITE);
            }
           

            //Player 1 Menu
            if (RayGui.GuiButton(new Rectangle(Program.screenwidth/2 - 125 - 300,Program.screenheight/2 , 25, 25), "<") )
            {
                if (P1currentVehicle != 0)
                {
                    P1currentVehicle --; 
                }
                else
                {
                    P1currentVehicle = p1_vehicles.Count - 1;
                }
               
            }

            if (RayGui.GuiButton(new Rectangle(Program.screenwidth / 2 + 110 - 300,Program.screenheight / 2 , 25, 25), ">"))
            {
                
                
                if ( P1currentVehicle == p1_vehicles.Count - 1)
                {
                    P1currentVehicle = 0;
                }

                else
                {
                   P1currentVehicle ++;
                }
                
            }
            // player 2 menu

            if (P2currentVehicle == p2_vehicles.Count )
            {
                playCPU = true;
                Raylib.DrawTextureEx(Program.selectionCPU, new Vector2(1280 / 2 - (45 * 3 / 2) + 305, 150), 0f, 2f, Raylib.WHITE);
                Raylib.DrawTextureEx(BotCar.CarTexture, new Vector2(1280 / 2 - (45 * 3 / 2) + 300, 720 / 2 - (86 * 3 / 2)), 0, 3, Raylib.WHITE);

            }
            else
            {
                playCPU = false;
                Raylib.DrawTextureEx(Program.selectionP2, new Vector2(1280 / 2 - (45 * 3 / 2) + 305, 150), 0f, 2f, Raylib.WHITE);
                Raylib.DrawTextureEx(p2_vehicles[P2currentVehicle].CarTexture, new Vector2(1280 / 2 - (45 * 3 / 2) + 300, 720 / 2 - (86 * 3 / 2)), 0, 3, Raylib.WHITE);
            }
            
         

            if (RayGui.GuiButton(new Rectangle(Program.screenwidth / 2 - 125 + 300, Program.screenheight / 2, 25, 25), "<"))
            {
                if (P2currentVehicle != 0)
                {
                    P2currentVehicle--;
                }
                else
                {
                    P2currentVehicle = p2_vehicles.Count;
                }

            }

            if (RayGui.GuiButton(new Rectangle(Program.screenwidth / 2 + 110 + 300, Program.screenheight / 2, 25, 25), ">"))
            {


                if (P2currentVehicle == p2_vehicles.Count)
                {
                    P2currentVehicle = 0;
                }

                else
                {
                    P2currentVehicle++;
                }

            }







            if (RayGui.GuiButton(new Rectangle(1280/2 - 100,650, 200,75),"SELECT CARS"))
            {
                if (playCPU)
                {
                    m_gameScreen.PushScreen(new GameScreen(Track, p1_vehicles[P1currentVehicle], p2_vehicles[P2currentVehicle - 1], m_gameScreen, BotCar, playCPU));

                }
                else
                {
                    m_gameScreen.PushScreen(new GameScreen(Track, p1_vehicles[P1currentVehicle], p2_vehicles[P2currentVehicle], m_gameScreen, BotCar, playCPU));
                }
                
            }
        }

        public void HandleInput()
        {
            if (RayGui.GuiButton(new Rectangle(Program.screenwidth -50,25,25,25),"x") == true)
            {
                m_gameScreen.PopScreen();
            }
        }

        
        public void Update()
        {
           
        }
    }
}
