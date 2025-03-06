using NEA.Screens;
using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
    public class TrackEditorScreen:IGameScreen
    {
        private IGameScreenManager m_gameScreen;
        private Grid grid = new Grid();
        private bool editMode;
        public string trackName = "";
        private bool errorMessage = false;
        private List<Waypoint> waypoints = new List<Waypoint>();
        private List<Checkpoint> checkpoints = new List<Checkpoint>();
        private List<Track> trackList = new List<Track>();

       
        public bool ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public List<Waypoint> Waypoints { get => waypoints; set => waypoints = value; }
        public List<Checkpoint> Checkpoints { get => checkpoints; set => checkpoints = value; }
        public List<Track> TrackList { get => trackList; set => trackList = value; }
        public Grid Grid { get => grid; set => grid = value; }

        public TrackEditorScreen(IGameScreenManager gameScreen, List<Track> tracks)
        {
            m_gameScreen = gameScreen;  
            TrackList = tracks; 
            
        }

        public void Dispose()
        {
          
        }

        public void Draw()
        {


            Gui.GuiTextInputBox(new Rectangle(25, 40, 200, 100), "","","",ref trackName);
          
            Cordinate cordinate;


            
           if (RayGui.GuiButton(new Rectangle(205,40,22,22),"X") == true)
           {
                m_gameScreen.PopScreen();
           }
           if (RayGui.GuiButton(new Rectangle(25, 100, 100, 35),"ERASE") == true)
           {
                Grid.Erase();
           }
           else if(RayGui.GuiButton(new Rectangle(125, 100, 100, 35),"SAVE") == true)
           {
                cordinate = firstStraightCell();
                if (cordinate.x != -1 && cordinate.y != -1)
                {
                    Traverse(cordinate);

                    

                    if (IsTrackValid() == true && IsTrackOnly() == true)
                    {
                        
                        
                        Grid.Fill = true;
                        Grid.SetStartingPoint();
                        Track newTrack = new Track(Grid, trackName, Waypoints);
                        TrackList.Add(newTrack);
                        TrackManager.SaveTracks(TrackList);
                        m_gameScreen.PopScreen();
                        TrackSelectionScreen.currentTrack++;
                    }   
                    else
                    {
                        ErrorMessage = true;
                        
                    }
                }
                else
                {
                    
                    ErrorMessage = true;
                }
                


           }

            if (ErrorMessage == true)
            {
                bool editmode = false;
                
                if (RayGui.GuiTextBox(new Rectangle(25,200 ,200, 100), "THE TRACK IS NOT VALID",25,editmode))
                {
                    Grid.Erase();
                    ErrorMessage = false;
                }
               
              
            }


           Grid.Draw(112,262,1, 150);

        }

        //Save track on the text file
       
        public void HandleInput()
        {
           
        }

      
        /// <summary>
        /// Checks if track created by the user is valid by checking that the road tiles use are compatible and dont overlap 
        /// </summary>
        /// <returns></returns>

    public bool IsTrackValid()
    {

            int validNeighb = 0;
            bool validCell = true;
            bool validLine = false;
            Cell upNeighb;
            Cell bottNeighb;
            Cell rightNeighb;
            Cell leftNeighb;
            Cell currNeighb;
            int i = 0;
            int z = 0;

            while (i < Grid.height && validCell != false )
            {
                validLine = false;

                while (z < Grid.width)
                {

                    currNeighb = Grid.GridArray[z, i];
                    upNeighb = new Cell();
                    bottNeighb = new Cell();
                    rightNeighb = new Cell();
                    leftNeighb = new Cell();
                    validNeighb = 0;
                   //straight works with current neighb


                    if (z != 0)
                    {
                        leftNeighb = Grid.GridArray[z - 1, i];
                    }
                    if (z != 8)
                    {
                        rightNeighb = Grid.GridArray[z + 1, i];
                    }
                    if (i != 0)
                    {
                        upNeighb = Grid.GridArray[z, i - 1];
                    }
                    if (i != 5)
                    {
                        bottNeighb = Grid.GridArray[z, i + 1];
                    }


                    if (currNeighb.Road != 0)
                    {
                        validLine = true;
                        if (currNeighb.Road == 1)
                        {

                            

                            if ((leftNeighb.Road == 1 && leftNeighb.Rotation == 0) || (leftNeighb.Road == 2 && currNeighb.Rotation == 0 && (leftNeighb.Rotation == 3 || leftNeighb.Rotation == 2)))
                            {

                                validNeighb++;
                            }
                            if ((rightNeighb.Road == 1 && rightNeighb.Rotation == 0) || (rightNeighb.Road == 2 && currNeighb.Rotation == 0 && (rightNeighb.Rotation == 0 || rightNeighb.Rotation == 1)))
                            {
                                validNeighb++;
                            }
                            if ((upNeighb.Road == 1 && upNeighb.Rotation == 1) || (upNeighb.Road == 2 && currNeighb.Rotation == 1 && (upNeighb.Rotation == 0 || upNeighb.Rotation == 3)))
                            {
                                validNeighb++;
                            }
                            if ((bottNeighb.Road == 1 && bottNeighb.Rotation == 1) || (bottNeighb.Road == 2 && currNeighb.Rotation == 1 && (bottNeighb.Rotation == 1 || bottNeighb.Rotation == 2)))
                            {
                                validNeighb++;
                            }


                        }
                        else if (currNeighb.Road == 2)
                        {
                            if (currNeighb.Rotation == 0)
                            {

                                if ((bottNeighb.Road == 1 && bottNeighb.Rotation == 1) || (bottNeighb.Road == 2 && (bottNeighb.Rotation == 1 || bottNeighb.Rotation == 2)))
                                {
                                    validNeighb++;
                                }
                                if ((leftNeighb.Road == 1 && leftNeighb.Rotation == 0) || (leftNeighb.Road == 2 && (leftNeighb.Rotation == 2 || leftNeighb.Rotation == 3)))
                                {
                                    validNeighb++;
                                }


                            }
                            if (currNeighb.Rotation == 1)
                            {

                                if ((leftNeighb.Road == 1 && leftNeighb.Rotation == 0) || (leftNeighb.Road == 2 && (leftNeighb.Rotation == 2 || leftNeighb.Rotation == 3)))
                                {
                                    validNeighb++;
                                }
                                if ((upNeighb.Road == 1 && upNeighb.Rotation == 1) || (upNeighb.Road == 2 && (upNeighb.Rotation == 0 || upNeighb.Rotation == 3)))
                                {
                                    validNeighb++;
                                }

                            }
                            if (currNeighb.Rotation == 2)
                            {

                                if ((upNeighb.Road == 1 && upNeighb.Rotation == 1) || (upNeighb.Road == 2 && (upNeighb.Rotation == 0 || upNeighb.Rotation == 3)))
                                {
                                    validNeighb++;
                                }
                                if ((rightNeighb.Road == 1 && rightNeighb.Rotation == 0) || (rightNeighb.Road == 2 && (rightNeighb.Rotation == 0 || rightNeighb.Rotation == 1)))
                                {
                                    validNeighb++;
                                }

                            }
                            if (currNeighb.Rotation == 3)
                            {

                                if ((bottNeighb.Road == 1 && bottNeighb.Rotation == 1) || (bottNeighb.Road == 2 && (bottNeighb.Rotation == 1 || bottNeighb.Rotation == 2)))
                                {
                                    validNeighb++;
                                }
                                if ((rightNeighb.Road == 1 && rightNeighb.Rotation == 0) || (rightNeighb.Road == 2 && (rightNeighb.Rotation == 0 || rightNeighb.Rotation == 1)))
                                {
                                    validNeighb++;
                                }

                            }
                        }

                        if (validNeighb != 2 && validLine == true)
                        {
                            
                            validCell = false;
                        }



                    }

                    z++;
                }
                z = 0; 
                i++;
            }

            return validCell;

        }

        //finds the first cell from where we start to traverse the graph
        public Cordinate firstStraightCell()
        {
            Cordinate cordinate;
            cordinate.x = -1;
            cordinate.y = -1;
            int x = 0;
            int y = 0;
            bool found = false;
            while (y < 6 && found == false) 
            {
                x = 0;

                while (x < 9 && found == false)
                {
                    if (Grid.GridArray[x, y].Road == 1)
                    {

                        cordinate.x = x;
                        cordinate.y = y;
                        found = true;
                    }

                    x++;
                }

                
                y++;
            }
            
            return cordinate;
        }
        /// <summary>
        /// Traverse the track recursevely as a graph and put as traversed the traversed cells  
        /// </summary>
        /// <param name="cordinate"></param>
        public void Traverse(Cordinate cordinate)
        {
            bool trackTrav = false;
            
            Cell currCell = Grid.GridArray[cordinate.x, cordinate.y];
            Cell upNeighb = null;
            Cell bottNeighb = null;
            Cell rightNeighb = null;
            Cell leftNeighb = null;
            Waypoint currWaipoint = new Waypoint();

            currWaipoint.x = cordinate.x;
            currWaipoint.y = cordinate.y;
           


            
            Waypoints.Add(currWaipoint);

            
                
                if (currCell.Road == 1 && currCell.Rotation == 0)
                {

                    if (cordinate.x > 0)
                    {
                        leftNeighb = Grid.GridArray[cordinate.x - 1, cordinate.y];
                    }
                    if (cordinate.x < 8)
                    {
                        rightNeighb = Grid.GridArray[cordinate.x + 1, cordinate.y];
                    }



                    currCell.Traversed = true;

                    if (rightNeighb != null == rightNeighb.Traversed == false)
                    {

                        Traverse(new Cordinate(cordinate.x + 1, cordinate.y));
                    }
                    else if (leftNeighb != null && leftNeighb.Traversed == false)
                    {

                        Traverse(new Cordinate(cordinate.x - 1, cordinate.y));
                    }
                    else
                    {
                        trackTrav = true;
                    }
                }

                if (currCell.Road == 1 && currCell.Rotation == 1)
                {
                    if (cordinate.y > 0)
                    {
                        upNeighb = Grid.GridArray[cordinate.x, cordinate.y - 1];
                    }
                    if (cordinate.y < 5)
                    {
                        bottNeighb = Grid.GridArray[cordinate.x, cordinate.y + 1];
                    }


                    currCell.Traversed = true;
                    if (upNeighb != null && upNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x, cordinate.y - 1));
                    }
                    else if (bottNeighb != null && bottNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x, cordinate.y + 1));
                    }
                    else
                    {
                        trackTrav = true;
                    }
                }

                if (currCell.Road == 2 && currCell.Rotation == 0)
                {

                    if (cordinate.x > 0)
                    {
                        leftNeighb = Grid.GridArray[cordinate.x - 1, cordinate.y];
                    }
                    if (cordinate.y < 5)
                    {
                        bottNeighb = Grid.GridArray[cordinate.x, cordinate.y + 1];
                    }

                    currCell.Traversed = true;
                    if (leftNeighb != null && leftNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x - 1, cordinate.y));
                    }
                    else if (bottNeighb != null && bottNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x, cordinate.y + 1));
                    }
                    else
                    {
                        trackTrav = true;
                    }
                }

                if (currCell.Road == 2 && currCell.Rotation == 1)
                {
                    if (cordinate.x > 0)
                    {
                        leftNeighb = Grid.GridArray[cordinate.x - 1, cordinate.y];
                    }
                    if (cordinate.y > 0)
                    {
                        upNeighb = Grid.GridArray[cordinate.x, cordinate.y - 1];
                    }

                    currCell.Traversed = true;
                    if (leftNeighb != null && leftNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x - 1, cordinate.y));
                    }
                    else if (upNeighb != null && upNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x, cordinate.y - 1));
                    }
                    else
                    {
                        trackTrav = true;
                    }
                }

                if (currCell.Road == 2 && currCell.Rotation == 2)
                {
                    if (cordinate.x < 8)
                    {
                        rightNeighb = Grid.GridArray[cordinate.x + 1, cordinate.y];
                    }

                    if (cordinate.y > 0)
                    {
                        upNeighb = Grid.GridArray[cordinate.x, cordinate.y - 1];
                    }
                    currCell.Traversed = true;
                    if (rightNeighb != null && rightNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x + 1, cordinate.y));
                    }
                    else if (upNeighb != null && upNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x, cordinate.y - 1));
                    }
                    else
                    {
                        trackTrav = true;
                    }
                }

                if (currCell.Road == 2 && currCell.Rotation == 3)
                {
                    if (cordinate.x < 8)
                    {
                        rightNeighb = Grid.GridArray[cordinate.x + 1, cordinate.y];
                    }

                    if (cordinate.y < 5)
                    {
                        bottNeighb = Grid.GridArray[cordinate.x, cordinate.y + 1];
                    }
                    currCell.Traversed = true;
                    if (rightNeighb != null && rightNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x + 1, cordinate.y));
                    }
                    else if (bottNeighb != null && bottNeighb.Traversed == false)
                    {
                        Traverse(new Cordinate(cordinate.x, cordinate.y + 1));
                    }
                    else
                    {
                        trackTrav = true;
                    }
                
                }


        }
        /// <summary>
        /// checks that the user created more than 1 valid track at the same time by going throught the array and checking all cell have been traversed
        /// </summary>
        /// <returns></returns>
        public bool IsTrackOnly()
        {
            int x = 0;  
            int y = 0;  
            bool valid = true;
            

            while (y < 6 && valid != false)
            {

                x = 0;
                while (x < 9)
                {
                    if (Grid.GridArray[x,y].Road != 0)
                    {
                        
                        if (Grid.GridArray[x, y].Traversed == false)
                        {
                            valid = false;
                        }
                        
                    }

                    x++;    
                }
                y++;
            }

            return valid;
        }
        
        public void Update()
        {
            Grid.Update();
        }

        
        public struct Cordinate
        {
            public int x;
            public int y;

            public Cordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

    }
}
