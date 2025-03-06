using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
    [Serializable]
    public class Cell
    {

        public static int width = 112;
        private int height = 112;
        private bool traversed = false;
        private int rotation = 0;
        private int road = 0;
        private bool startingLine = false;


        public int Height { get => height; set => height = value; }
        public bool Traversed { get => traversed; set => traversed = value; }
        public int Rotation { get => rotation; set => rotation = value; }
        public int Road { get => road; set => road = value; }
        public bool StartingLine { get => startingLine; set => startingLine = value; }

        public Cell()
        {
           
        }

        
        


    }
}
