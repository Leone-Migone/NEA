using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA
{
    public class CheckpointManager
    {
        private Queue<Checkpoint> p1Queue = new Queue<Checkpoint>();
        private Queue<Checkpoint> p2Queue = new Queue<Checkpoint>();
        private List<Checkpoint> checkpList;

        private int p1Lapsleft;
        private int p2Lapsleft;

        public Queue<Checkpoint> P2Queue { get => p2Queue; set => p2Queue = value; }
        public Queue<Checkpoint> P1Queue { get => p1Queue; set => p1Queue = value; }
        public List<Checkpoint> CheckpList { get => checkpList; set => checkpList = value; }
        public int P1Lapsleft { get => p1Lapsleft; set => p1Lapsleft = value; }
        public int P2Lapsleft { get => p2Lapsleft; set => p2Lapsleft = value; }

        
        public CheckpointManager(List<Checkpoint> checkps, int lapsNumber)
        {
            P1Lapsleft = lapsNumber;
            P2Lapsleft = lapsNumber;
            CheckpList = checkps;
            for (int i = 0; i < CheckpList.Count(); i++)
            {
                P1Queue.Enqueue(CheckpList.ElementAt(i));
                P2Queue.Enqueue(CheckpList.ElementAt(i));
            }
            // queue another time the first cell since has to be trigerred at the start and when we arrive
            P1Queue.Enqueue(CheckpList.ElementAt(0));
            P2Queue.Enqueue(CheckpList.ElementAt(0));

        }

        /// <summary>
        /// Check that when the first player crosses the starting line, all the checkpoints have been passed and in case decreases the laps left
        /// </summary>
        /// <returns></returns>
        public bool p1IsLapValid()
        {
            bool IsValid = false;
            if (P1Queue.Count == 0)
            {
                IsValid = true;
                P1Lapsleft--;
                for (int i = 0; i < CheckpList.Count(); i++)
                {
                    P1Queue.Enqueue(CheckpList.ElementAt(i));
                }

            }

            return IsValid;
        }

        /// <summary>
        /// Check that when the second player crosses the starting line, all the checkpoints have been passed and in case decreases the laps left
        /// </summary>
        /// <returns></returns>
        public bool p2IsLapValid()
        {
            bool IsValid = false;
            if (P2Queue.Count == 0)
            {
                P2Lapsleft--;
                IsValid = true;
                for (int i = 0; i < CheckpList.Count(); i++)
                {
                    P2Queue.Enqueue(CheckpList.ElementAt(i));
                }

            }

            return IsValid;
        }


        /// <summary>
        /// deques every checkpoint when its passed by the first player
        /// </summary>
        public void p1CheckpointPassed()
        {

            P1Queue.Dequeue();

            
        
        }
        /// <summary>
        /// deques every checkpoint when its passed by the second player
        /// </summary>
        public void p2CheckpointPassed()
        {

            P2Queue.Dequeue();


        }

    }
}
