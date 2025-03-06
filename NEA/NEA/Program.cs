using NEA;
using NEA.Screens;

using Raylib_CsLo;
using System.Numerics;
using System.Threading.Tasks;

namespace NEA
{
    public static class Program
    {

         
        public static Vehicle vehicle;   
        public static Grid grid;


        public static Texture carTexture;
        public static Texture carTexture2;
        public static Texture carTexture3;
        public static Texture selectionP1;
        public static Texture selectionP2;
        public static Texture selectionCPU;
        public static Color backgroundColor = Raylib.DARKGRAY;
        public static Texture boostFrame;
        public static Texture Bush1;
        public static Texture Bush2;
        public static Texture Bush3;
        public static Texture Tree;
        public static Texture lapTime;
        public static Texture Laps;
        public static Texture WASD;
        public static Texture Arrows;
        public static Texture CARZ;




        public static int screenwidth = 1280;
        public static int screenheight = 720;
       

        public static IGameScreenManager m_screenManager = new GameScreenManager();

      
        
        
        public static async Task Main(string[] args)
        {
            //initialize the screen
            Raylib.InitWindow(screenwidth, screenheight, "Hello, Raylib-CsLo");
            //Load all the game texture
            Grid.curveTexture = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Track/curve 1.png");
            Grid.roadTexture = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Track/straight 1.png");
            Grid.grassTexture = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Track/grass.png");
            Grid.startingLine = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Track/startingLine.png");
            carTexture = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Cars/BlackCar.png");
            carTexture2 = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Cars/SportCar.png");
            carTexture3 = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Cars/WhiteCar.png");
            selectionP1 = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/P1.png");
            selectionP2 = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/P2.png");
            selectionCPU = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/CPU.png");
            boostFrame = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Frame.png");
            Bush1 = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Bush1.png");
            Bush2 = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Bush2.png");
            Bush3 = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Bush3.png");
            Tree = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Tree.png");
            lapTime = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Laptime.png");
            Laps = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Laps.png");
            WASD = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/WASD.png");
            Arrows = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/Arrows.png");
            CARZ = Raylib.LoadTexture("/Users/39351/OneDrive - rendcomb.gloucs.sch.uk/Computing/NEA/NEA/bin/Debug/net6.0/Assets/CARZ.png");
           
            
            grid = new Grid();
            
            Raylib.SetTargetFPS(60);

            m_screenManager.PushScreen(new SplashScreen(m_screenManager));
            

            // Main game loop
            while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
            {
                
                Raylib.BeginDrawing();
              
                Raylib.ClearBackground(Raylib.DARKGRAY);
                Draw();

                Update();
                Raylib.EndDrawing();
            }
        
            
            Raylib.CloseWindow();
        }



        public static void Update()
        {
            m_screenManager.Update();
            m_screenManager.HandleInput();
         
            bool posoccupied = false;

          
      
            
            
        }

        public static void Draw()
        {
            m_screenManager.Draw();
          
        }
    }
}