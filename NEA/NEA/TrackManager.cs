
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
    public static class TrackManager
    {
        public const string trackFileName = "Tracks.bin";

        /// <summary>
        /// Save tracks to a binary file
        /// </summary>
        /// <param name="trackList">tracklist loaded intO FILE</param>
        public static List<Track> SaveTracks(List<Track> trackList)
        {
            

            try
            {
                using (Stream stream = File.Open(trackFileName, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bformatter.Serialize(stream, trackList);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving tracks " + e.Message); 
                
            }
            
            return trackList;


        }
        /// <summary>
        /// //Access the binary file, reads it and associate the list of tracks stored to the variavle trackList
        /// </summary>
        /// <param name="trackList"></param>
        public static List<Track> LoadTracks(List<Track> trackList)
        {
            try
            {
                using (Stream stream = File.Open(trackFileName, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    trackList = (List<Track>)bformatter.Deserialize(stream);
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading tracks " + e.Message);
                
            }


            return trackList;
        }
    }
}
