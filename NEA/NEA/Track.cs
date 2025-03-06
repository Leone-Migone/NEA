using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raylib_CsLo;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;

namespace NEA
{
    [Serializable()]
    public class Track
    {

        private Grid trackGrid;
        private string trackName;
        private List<Waypoint> waypoints;
        

        public Grid TrackGrid { get => trackGrid; set => trackGrid = value; }
        public string TrackName { get => trackName; set => trackName = value; }
        public List<Waypoint> Waypoints { get => waypoints; set => waypoints = value; }
        

        public Track(Grid grid, string name , List<Waypoint> waypoints)
        {

            TrackGrid = grid;
            TrackName = name;
            this.Waypoints = waypoints; 
           
            
            
        }
        /// <summary>
        /// Generates the checkpoint utilised during the race to validate each lap
        /// </summary>
        /// <returns></returns>
        public List<Checkpoint> GenerateCheckpoints()
        {
            List<Checkpoint> checkpoints = new List<Checkpoint>();
            Vector2 currWaip;
            Checkpoint temp;
            
            for (int i = 0; i < Waypoints.Count(); i++)
            {
                currWaip = new Vector2(Waypoints.ElementAt(i).x * 180 + 180, Waypoints.ElementAt(i).y * 180);
                temp = new Checkpoint(new Rectangle(currWaip.X,currWaip.Y,180,180));
                checkpoints.Add(temp);

            }

            return checkpoints;
        }
    }

}
