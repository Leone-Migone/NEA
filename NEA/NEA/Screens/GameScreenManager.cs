using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NEA.Screens.GameScreenManager;

namespace NEA.Screens
{

    /// <summary>
    /// This class manages the stack of game screens for the application.
    /// </summary>
    public class GameScreenManager : IGameScreenManager
    {
        
        private readonly List<IGameScreen> m_gameScreens = new List<IGameScreen>();

        public GameScreenManager()
        {
            
        }



        //Pushes a new game screen onto the top of the stack
        public void PushScreen(IGameScreen screen)
        {

            m_gameScreens.Add(screen);

        }

        //Checks if the game screen list is empty
        private bool IsScreenListEmpty
        {
            get { return m_gameScreens.Count <= 0; }
        }
        //returns the current game screen 
        private IGameScreen GetCurrentScreen()
        {
            return m_gameScreens[m_gameScreens.Count - 1];
        }

        //Removes all screens 
        private void RemoveAllScreens()
        {
            while (IsScreenListEmpty != true)
            {
                RemoveCurrentScreen();
            }
        }
        //removes current screen 
        private void RemoveCurrentScreen()
        {
            var screen = GetCurrentScreen();
            screen.Dispose();
            m_gameScreens.Remove(screen);
        }
        // pops the current screen off the stack
        public void PopScreen()
        {
            if (!IsScreenListEmpty)
            {
                RemoveCurrentScreen();

            }
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

            }
        }

        public void HandleInput()
        {
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                screen.HandleInput();

            }
        }
        //Update current screnn
        public void Update()
        {
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                screen.Update();

            }
        }
        //draws current screen
        public void Draw()
        {
            if (!IsScreenListEmpty)
            {
                var screen = GetCurrentScreen();

                screen.Draw();

            }
        }

        public void Dispose()
        {
            RemoveAllScreens();
        }


    }



}
