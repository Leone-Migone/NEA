using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA.Screens
{
    public interface IGameScreen
    {

        void Update();
        void Draw();
        void Dispose();
        void HandleInput();

    }

    public interface IGameScreenManager
    {
        
        void PushScreen(IGameScreen screen);
        void PopScreen();


        void Update();
        void Draw();
        void HandleInput();
       

    }
}
