using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SRTF
{
    enum State { NEW, READY, RUN, BLOCK, TERMINATED };

    class Program
    {
        static void Main(string[] args)
        {


            

            List<Process> processList = new List<Process>();
            List<Process> completedProcess = new List<Process>();
            Console.WriteLine("Enter number of process");
            int noOfProcess = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter size of each processes : ");
            int[] size = new int[noOfProcess];
            for (int i = 0; i < noOfProcess; i++)
            {
                Console.WriteLine("Enter size of process " + i.ToString());
                size[i] = int.Parse(Console.ReadLine());

            }

            for (int i = 0; i < noOfProcess; i++)
            {
                Process p = new Process(i, i, size[i], State.NEW);

                processList.Add(p);

            }
   




            int time = 0;




            Process process;

            while (processList.Count != completedProcess.Count)//(stop != true)
            {


                process = getNewShortestRemainingAtTime(processList, time); 

     

                Console.WriteLine("press Eneter to continue");
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Enter)
                {

                }
                if (process.state == State.NEW)
                {
                    process.startTime = time;
                    process.state = State.READY;
                }



                int n = 1;

                Console.WriteLine("{0} quantums of process {1} are running ", n, process.processId);

                time += n;

                process.rem -= n;
                if (process.rem == 0)
                {
                    process.state = State.TERMINATED;
                    process.completionTime = time;
                    completedProcess.Add(process);
                }
                Console.WriteLine();


                



            }


            

            completedProcess = completedProcess.OrderBy(x => x.processId).ToList();
            calculateAttributes(completedProcess);

            Console.WriteLine("P.NO\tAT\tET\tST\tCT\tTAT\tWT");
            foreach (Process p in completedProcess)
            {
                Console.WriteLine(p.processId + "\t" + p.arrivalTime + "\t" + p.size + "\t" + p.startTime + "\t" + p.completionTime + "\t" + p.tat + "\t" + p.wt);
            }

            Console.ReadKey();

        }


        static void calculateAttributes(List<Process> list)
        {
            foreach (Process p in list)
            {
                p.tat = p.completionTime - p.arrivalTime;
                p.wt = p.tat - p.size;
            }
        }
        static Process getNewShortestRemainingAtTime(List<Process> list, int time)
        {
            List<Process> newList = new List<Process>();
            foreach (Process p in list)
            {
                if (p.arrivalTime <= time && p.state != State.TERMINATED )
                    newList.Add(p);
            }
            newList = newList.OrderBy(x => x.rem).ToList();
            if (newList.Count != 0)
                return newList[0];
            else return null;
        }

    }
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
