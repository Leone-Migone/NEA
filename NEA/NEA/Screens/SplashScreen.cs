using NEA.Starfield;
using Raylib_CsLo;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Screens
{
    public class SplashScreen : IGameScreen
    {
        private StarField starField = new StarField();
        
        private IGameScreenManager m_screenManager;
        public SplashScreen(IGameScreenManager gameScreenManager)
        {
            m_screenManager = gameScreenManager;
        }

        public void Dispose()
        {

        }

        public void Draw()
        {
            Raylib.ClearBackground(Raylib.BLACK);
            //starfield animation
            starField.Draw();

            if (RayGui.GuiButton(new Rectangle(Program.screenwidth - 300,0, 300, 100), "PLAY") == true)
            {
                m_screenManager.PushScreen(new TrackSelectionScreen(m_screenManager));
            }
            //TITLE TEXTURE
            Raylib.DrawTexturePro(Program.CARZ,new Rectangle(0,0,Program.CARZ.width,Program.CARZ.height), new Rectangle(1280/2 -145, 720/2 - 100, Program.CARZ.width *4, Program.CARZ.height*4),new Vector2(0,0),0,Raylib.WHITE);
        }

        public void HandleInput()
        {

        }

        public void Update()
        {
            
        }
    }
}
