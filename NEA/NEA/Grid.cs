using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;
using SharpDX.DXGI;


namespace NEA
{

    [Serializable]
    public class Grid 
    {

        public const int width = 9;
        public const int height = 6;
        private Cell[,] gridArray;
        public static Cell cell = new Cell();
        private bool fill = false;
        public static Texture curveTexture;
        public static Texture roadTexture;
        public static Texture grassTexture;
        public static Texture startingLine;

        public Cell[,] GridArray { get => gridArray; set => gridArray = value; }
        public bool Fill { get => fill; set => fill = value; }

        public Grid()
        {
            GridArray = new Cell[width, height];
            

            for (int i = 0; i < height; i++)
            {
                for (int z = 0; z < width; z++)
                {
                    
                    GridArray[z,i] = new Cell();
                }
            }
            
        }

        public void Draw(int cellxy , int gridStartingX, float scale, int startLine)
        {
            int cellx = 0;
            int celly = 0;
            
            Vector2 roadCenter = new Vector2(roadTexture.width/2,roadTexture.height/2);




            for (int y = 0; y < height; y++)
            {

                for (int i = 0; i < width; i++)
                {
                    cellx += cellxy;
                    Raylib.DrawRectangleLinesEx(new Rectangle( startLine + cellx, celly, cellxy, cellxy), 1, Raylib.WHITE);
                }
                cellx = 0;
                celly += cellxy;

            }

            for (int i = 0; i < height; i++)
            {
                for (int z = 0; z < width; z++)
                {
                    

                    if (GridArray[z, i].Road == 0 && Fill == true)
                    {
                        Raylib.DrawTexturePro(grassTexture, new Rectangle(0, 0, roadTexture.width, roadTexture.height), new Rectangle((z * cellxy + gridStartingX) + (roadCenter.X * scale), i * cellxy + (roadCenter.X * scale), roadTexture.width * scale, roadTexture.height * scale), roadCenter * scale, GridArray[z, i].Rotation * 90, Raylib.WHITE);
                    }
                    
                    
                    
                    else if (GridArray[z,i].Road == 1)
                    {

                        if (GridArray[z, i].StartingLine == true)
                        {
                            Raylib.DrawTexturePro(startingLine, new Rectangle(0, 0, roadTexture.width, roadTexture.height), new Rectangle((z * cellxy + gridStartingX) + (roadCenter.X * scale), i * cellxy + (roadCenter.X * scale), roadTexture.width * scale, roadTexture.height * scale), roadCenter * scale, GridArray[z, i].Rotation * 90, Raylib.WHITE);

                        }

                        else
                        {
                            // we use texture pro to set the origin of the rotation to the centre of the texutre

                            Raylib.DrawTexturePro(roadTexture, new Rectangle(0, 0, roadTexture.width, roadTexture.height), new Rectangle((z * cellxy + gridStartingX) + (roadCenter.X * scale), i * cellxy + (roadCenter.X * scale), roadTexture.width * scale, roadTexture.height * scale), roadCenter * scale, GridArray[z, i].Rotation * 90, Raylib.WHITE);

                        }




                    }
                    else if (GridArray[z,i].Road == 2)
                    {
                        Raylib.DrawTexturePro(curveTexture, new Rectangle(0, 0, curveTexture.width, curveTexture.height), new Rectangle((z * cellxy + gridStartingX) + (roadCenter.X * scale), i * cellxy + (roadCenter.X * scale), curveTexture.width * scale, curveTexture.height * scale), roadCenter * scale, GridArray[z, i].Rotation * 90, Raylib.WHITE);
                    }

                   
                }
                
            }
        }

        /// <summary>
        /// Checks which cell of the grid we pressed and changes it to a different map tile according to which button we pressed
        /// </summary>
        public void IsCellPicked()
        {


            int mouseX = 0; 
            int mouseY = 0;
            int cellxy = 112;
           
           
            // if the right mouse button is clicked a straight its placed, if the cell picked is already a straight it will be rotated 
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT) == true) 
            {
                
                mouseX = Raylib.GetMouseX();
                mouseY = Raylib.GetMouseY();

                if (mouseX >= 262 && mouseX <= 1270 && mouseY <= 672)
                {

                    // we find the element of the array corrispondent to this cordinates in our grid and we set to 1 to indicate that we drew a road in that cell
                    mouseX = (mouseX - 262) / cellxy;
                    mouseY = mouseY / cellxy;


                    if (GridArray[mouseX, mouseY].Road == 1)
                    {

                        if (GridArray[mouseX, mouseY].Rotation == 1)
                        {
                            GridArray[mouseX, mouseY].Rotation = 0;
                        }
                        else if (GridArray[mouseX, mouseY].Rotation == 0)
                        {
                            GridArray[mouseX, mouseY].Rotation = 1;
                        }

                    }
                    else
                    {
                      
                        GridArray[mouseX, mouseY].Road = 1;
                        GridArray[mouseX, mouseY].Rotation = 0;
                    }


                }
            }





            // if the left mouse button is clicked a curve its placed, if the cell picked is already a curve it will be rotated 
            else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) == true)
            {

                
                mouseX = Raylib.GetMouseX();
                mouseY = Raylib.GetMouseY();

                if (mouseX >= 262 && mouseX <= 1270 && mouseY <= 672)
                {

                    // we find the element of the array corrispondent to this cordinates in our grid and we set to 1 to indicate that we drew a curve in that cell
                    mouseX = (mouseX - 262) / cellxy;
                    mouseY = mouseY / cellxy;

                    if (GridArray[mouseX, mouseY].Road == 2)
                    {
                        if (GridArray[mouseX, mouseY].Rotation < 3)
                        {
                            GridArray[mouseX, mouseY].Rotation += 1;
                        }
                        else if (GridArray[mouseX, mouseY].Rotation == 3)
                        {
                            GridArray[mouseX, mouseY].Rotation = 0;
                        }
                    }
                    else
                    {
                        
                        GridArray[mouseX, mouseY].Road = 2;
                        GridArray[mouseX, mouseY].Rotation = 0;
                    }


                }
            }

            // if the middle mouse button its clicked, the tile in the cell where the mouse is when we press the button will be removed
            else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_MIDDLE) == true)
            {


                mouseX = Raylib.GetMouseX();
                mouseY = Raylib.GetMouseY();

                if (mouseX >= 262 && mouseX <= 1270 && mouseY <= 672)
                {

                    // we find the element of the array corrispondent to this cordinates in our grid and we set to 1 to indicate that we drew a curve in that cell
                    mouseX = (mouseX - 262) / cellxy;
                    mouseY = mouseY / cellxy;

                    GridArray[mouseX, mouseY].Road = 0;

                }
            }





        }

        // erase all the tiles in the grid 
        public void Erase()
        {
            for (int i = 0; i < height; i++)
            {
                for (int z = 0; z < width; z++)
                {

                    GridArray[z,i].Road = 0;

                }

            }
        }
        // fills the empty part of the track with grass tiles
        public void FillTrack()
        {

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) == true)
            {
                Fill = true;
            }

        }

        // sets the first straight tile placed as the starting line for the track
        public void SetStartingPoint()
        {
            int y = 0;
            int x = 0;
            bool set = false;
            while (y<6 && set != true)
            {
                x = 0;
                while (x<9 && set != true)
                {
                    if (GridArray[x,y].Road == 1)
                    {
                        GridArray[x,y].StartingLine = true;
                        set = true;
                    }

                    x++;
                }

                y++;
            }
            
        }
        public void Update()
        {

            IsCellPicked();
            FillTrack();
        }
    }
}
