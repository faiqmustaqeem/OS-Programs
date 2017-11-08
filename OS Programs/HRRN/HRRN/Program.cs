using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HRRN
{
    enum State { NEW, READY, RUN, BLOCK, TERMINATED };

    class Program
    {
        static void Main(string[] args)
        {


            Queue<Process> readyQueue = new Queue<Process>();

            List<Process> processList = new List<Process>();
            List<Process> completedProcess = new List<Process>();
            Console.WriteLine("Enter number of process");
            int noOfProcess = int.Parse(Console.ReadLine());

            int[] at = new int[noOfProcess];
            for (int i = 0; i < noOfProcess; i++)
            {
                Console.WriteLine("Enter Arrival Time of process " + i.ToString());
                at[i] = int.Parse(Console.ReadLine());

            }

            Console.WriteLine("Enter size of each processes : ");
            int[] size = new int[noOfProcess];
            for (int i = 0; i < noOfProcess; i++)
            {
                Console.WriteLine("Enter size of process " + i.ToString());
                size[i] = int.Parse(Console.ReadLine());

            }

            for (int i = 0; i < noOfProcess; i++)
            {
                Process p = new Process(i, at[i], size[i], State.NEW);

                processList.Add(p);

            }
            //    processList = processList.OrderBy(x => x.size).ToList();






            int time = 0;




          

            while (completedProcess.Count != processList.Count)//(stop != true)
            {

                Process process = getHighestResponseRatioProcess(processList, time);

                Console.WriteLine("press Eneter to continue");
                var input = Console.ReadKey();
                
                if (input.Key == ConsoleKey.Enter)
                {

                }

                process.state = State.READY;

                process.startTime = time;

                Console.WriteLine("{0} quantums of process {1} are running ", process.size, process.processId);
                time += process.size;

                process.rem = 0;

                process.state = State.TERMINATED;
                process.completionTime = time;

                Console.WriteLine();


                completedProcess.Add(process);

               
               



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

        static Process getHighestResponseRatioProcess(List<Process> list , int time)
        {
            List<Process> arrivedProcesses = newArrivedProcessAtThisTime(list , time);
            int n = getNumberOfProcessArrivedAtThisTime(list, time);
            if (n == 1)
            {
                return arrivedProcesses[0];
            }
           List<double> RR =new List<double>();
            
            foreach (var item in arrivedProcesses)
            {
                RR.Add(ResponseRatio(item));
                
            }
           double HRR =  RR.Max();
           int indexOfHRR = RR.IndexOf(HRR);

          return  arrivedProcesses[indexOfHRR];

        }
        
        static double ResponseRatio(Process p)
        {
            return ((double)p.wt + (double)p.size) / (double)p.size;
        }
        static void calculateAttributes(List<Process> list)
        {
            foreach (Process p in list)
            {
                p.tat = p.completionTime - p.arrivalTime;
                p.wt = p.tat - p.size;
            }
        }
        static List<Process> newArrivedProcessAtThisTime(List<Process> list, int time)
        {
            List<Process> newList = new List<Process>();
            foreach (Process p in list)
            {
                if (p.arrivalTime <= time && p.state != State.TERMINATED)
                    newList.Add(p);
            }
            foreach (var item in newList)
            {
                item.completionTime = time + item.size;
                item.tat = item.completionTime - item.arrivalTime;
                item.wt = item.tat - item.size;
            }
            newList = newList.OrderBy(x => x.arrivalTime).ToList();
            return newList;
        }
        static int getNumberOfProcessArrivedAtThisTime(List<Process> list, int time)
        {
            List<Process> newList = new List<Process>();
            foreach (Process p in list)
            {
                if (p.arrivalTime <= time && p.state != State.TERMINATED)
                    newList.Add(p);
            }
            return newList.Count;
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
