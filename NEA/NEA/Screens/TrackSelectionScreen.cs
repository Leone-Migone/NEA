using Raylib_CsLo;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace NEA.Screens
{
    public class TrackSelectionScreen : IGameScreen
    {
        private IGameScreenManager m_screenManager;
        public static Grid grid = new Grid();
        private List<Track> trackList = new List<Track>();
        public static int currentTrack = 0;

        public List<Track> TrackList { get => trackList; set => trackList = value; }
       

        public TrackSelectionScreen(IGameScreenManager gameScreenManager)
        {
            m_screenManager = gameScreenManager;
            //reading list of tracks
            TrackList = TrackManager.LoadTracks(TrackList);

        }

        public void Dispose()
        {
            
        }

        public void Draw()
        {
            try
            {
                //show different track on screen 
                if (File.Exists(TrackManager.trackFileName))
                {
                    if (TrackList.Count != 0)
                    {
                        Raylib.DrawText("TRACK: " + TrackList.ElementAt(currentTrack).TrackName.ToUpper(), 725, 220, 20, Raylib.LIME);
                        DrawTrackPreview();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            
            
           

        }

        /// <summary>
        /// Handles the input of the button pressed
        /// </summary>
        public void HandleInput()
        {
            if (RayGui.GuiButton(new Rectangle(75, 100, 200, 75), "SELECT TRACK") == true)
            {
                if (TrackList.Count > 0)
                {
                    Vector2 carPosition = CarStartingGridPos(); 
                    m_screenManager.PushScreen(new CarSelectionScreen(m_screenManager, TrackList[currentTrack],carPosition));
                }
                else
                {
                    m_screenManager.PushScreen(new TrackEditorScreen(m_screenManager,TrackList));
                }
                    
            }
            if (RayGui.GuiButton(new Rectangle(75, 250, 200, 75), "TRACK EDITOR") == true)
            {
                m_screenManager.PushScreen(new TrackEditorScreen(m_screenManager,TrackList));
            }

            if (RayGui.GuiButton(new Rectangle(75, 400, 200, 75), "DELETE TRACK") == true)
            {
                try
                {
                    if (TrackList.Count > 0)
                    {
                        TrackList.RemoveAt(currentTrack);
                        if (currentTrack == 0)
                        {

                        }
                        else
                        {
                            currentTrack--;
                        }
                        
                        TrackManager.SaveTracks(TrackList);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message); 
                }

               
                
                
            }
            //buttons to go through the the different tracks

            if (RayGui.GuiButton(new Rectangle(585,300,25,25),"<"))
            {
                if (currentTrack != 0)
                {
                    currentTrack --;
                }
                else if (TrackList.Count != 0)
                {

                    currentTrack = TrackList.Count -1;
                }
            }

            if (RayGui.GuiButton(new Rectangle(1100, 300, 25, 25), ">"))
            {
                if (currentTrack != TrackList.Count - 1)
                {
                    currentTrack ++;
                }

                else 
                {
                    currentTrack =0;
                }
            }
        }

        
       
        /// <summary>
        /// draws the track in smaller size to give the user a preview of the track before selecting it
        /// </summary>
        public void DrawTrackPreview()
        {
            

            Cell[,] trackGrid = TrackList.ElementAt(currentTrack).TrackGrid.GridArray;


            Vector2 roadCenter = new Vector2(Grid.roadTexture.width / 2, Grid.roadTexture.height / 2);

            int cellx = 0;
            int celly = 0;
          


            //We draw the preview of the tracks in a scaled grid, using Raylib.DrawtexturePro to scale it to the 25%

            for (int i = 0; i < Grid.height; i++)
            {
                for (int z = 0; z < Grid.width; z++)
                {

                    if (trackGrid[z, i].Road == 0)
                    {
                        Raylib.DrawTexturePro(Grid.grassTexture, new Rectangle(0, 0, Grid.roadTexture.width, Grid.roadTexture.height), new Rectangle((z * (112 / 4)) + roadCenter.X + ((500 + 1100) / 2) - Grid.roadTexture.width, i * (112 / 4) + roadCenter.X + (Program.screenheight / 2 - Grid.roadTexture.height * (18/10)) - 50, Grid.roadTexture.width / 4, Grid.roadTexture.height / 4), roadCenter/4, trackGrid[z, i].Rotation * 90, Raylib.WHITE);
                        
                    }

                    if (trackGrid[z, i].Road == 1)
                    {

                       
                        Raylib.DrawTexturePro(Grid.roadTexture, new Rectangle(0, 0, Grid.roadTexture.width , Grid.roadTexture.height ), new Rectangle((z * (112 / 4)) + roadCenter.X + ((500 + 1100) / 2) - Grid.roadTexture.width, i * (112 / 4) + roadCenter.X + (Program.screenheight / 2 - Grid.roadTexture.height * (18/10)) - 50, Grid.roadTexture.width / 4, Grid.roadTexture.height / 4), roadCenter/4, trackGrid[z, i].Rotation * 90 , Raylib.WHITE);

                    }
                    else if (trackGrid[z, i].Road == 2)
                    {
                        Raylib.DrawTexturePro(Grid.curveTexture, new Rectangle(0, 0, Grid.curveTexture.width , Grid.curveTexture.height ), new Rectangle((z * (112 / 4)) + roadCenter.X + ((500 + 1100) / 2) - Grid.roadTexture.width, i * (112 / 4) + roadCenter.X + (Program.screenheight / 2 - Grid.roadTexture.height * (18 / 10)) - 50, Grid.curveTexture.width / 4, Grid.curveTexture.height / 4), roadCenter / 4, trackGrid[z, i].Rotation * 90, Raylib.WHITE);
                    }


                }
                



            }

        }   


        /// <summary>
        /// Calculate the starting line of the track and returns the cell position in the grid
        /// </summary>
        /// <returns></returns>
        public Vector2 CarStartingGridPos()
        {
            bool found = false;
            Cell[,] trackGrid = TrackList.ElementAt(currentTrack).TrackGrid.GridArray;
            int x = 0;
            int y = 0;
            Vector2 carPos = Vector2.Zero;

            while (y < 6 && found != true)
            {
                x = 0;
                while (x < 9 && found != true)
                {
                    if (trackGrid[x, y].StartingLine == true)
                    {
                        found = true;
                        carPos = new Vector2(x, y);
                    }

                    
                  
                    

                     x++;
                }

                y++;
            }

            return carPos;
        }

        public void Update()
        {
           
        }
    }
}
