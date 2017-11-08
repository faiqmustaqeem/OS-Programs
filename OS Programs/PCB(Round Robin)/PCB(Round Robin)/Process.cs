using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCB_Round_Robin_
{
    class Process
    {
        public int processId;
        public int size;
        public State state;
        public int rem;
        public int arrivalTime;
        public int startTime;
        public int completionTime;
        public int tat;
        public int wt;

        public Process(int processId, int arrivalTime, int size, State state)
        {
            this.processId = processId;
            this.arrivalTime = arrivalTime;
            this.size = size;
            this.state = state;
            rem = size;
        }




    }
}
