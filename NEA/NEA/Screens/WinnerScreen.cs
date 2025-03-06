using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Screens
{
    public class WinnerScreen : IGameScreen
    {
        private string winner;
        private IGameScreenManager m_screenManager;
        public WinnerScreen(string Winner, IGameScreenManager m_screenManager1)
        {
            winner = Winner;
            m_screenManager = m_screenManager1;
            Raylib.ToggleFullscreen();
        }

        public void Dispose()
        {
            
        }

        public void Draw()
        {
            Raylib.DrawRectangle(Program.screenwidth / 2 - 210, Program.screenheight / 2 - 100, 405, 100,Raylib.LIGHTGRAY);
            Raylib.DrawText(winner + " WON", Program.screenwidth / 2 - 200, Program.screenheight / 2 - 100, 100, Raylib.BLACK);
            if (RayGui.GuiTextBox(new Rectangle(Program.screenwidth / 2 - 210, Program.screenheight / 2 - 100, 410, 100),"", 700, false) != false)
            {
                m_screenManager.PopScreen();
                m_screenManager.PopScreen();
                m_screenManager.PopScreen();

            }
           
        }

        public void HandleInput()
        {

        }

        public void Update()
        {

        }
    }
}
