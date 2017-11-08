using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SJF
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
        //    processList = processList.OrderBy(x => x.size).ToList();

            
              
            

           
            int time = 0;




            readyQueue.Enqueue(processList[0]);

            while (readyQueue.Count != 0)//(stop != true)
            {

                Process process = readyQueue.Dequeue();

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

                    List<Process> newArrivedProcess = getAllProcessAtThisTime(processList, time);
                //
                    newArrivedProcess = newArrivedProcess.OrderBy(x => x.size).ToList();
                //
                    foreach (var item in newArrivedProcess)
                    {
                        item.state = State.READY;
                        readyQueue.Enqueue(item);
                    }
                    


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
        static List<Process> getAllProcessAtThisTime(List<Process> list, int time)
        {
            List<Process> newList = new List<Process>();
            foreach (Process p in list)
            {
                if (p.arrivalTime <= time && p.state == State.NEW)
                    newList.Add(p);
            }
            return newList;
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
