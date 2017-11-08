using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PCB_Round_Robin_
{
    enum State { NEW, READY, RUN, BLOCK, TERMINATED };

    class Program
    {
        static void Main(string[] args)
        {
            Queue<Process> readyQueue = new Queue<Process>();
            Queue<Process> blockedQueue = new Queue<Process>();
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
                // readyQueue.Enqueue(p);
                processList.Add(p);

            }
            Console.WriteLine("Enter quantum size : ");
            int q = int.Parse(Console.ReadLine());

            bool interrupt = false;
            int time = 0;

            readyQueue.Enqueue(processList[0]);



            while (readyQueue.Count != 0)//(stop != true)
            {

                Process process = readyQueue.Dequeue();

                Console.WriteLine("press Eneter to continue , i for Interrupt , r for Return");
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Enter)
                {

                }
                else if (input.Key == ConsoleKey.I)
                {
                    Console.WriteLine("Inetrrupt...");
                    interrupt = true;
                    blockedQueue.Enqueue(process);
                    if (readyQueue.Count == 0)
                    {
                        Console.WriteLine("Ready Queue is Empty");
                    }
                    else
                    {
                        process = readyQueue.Dequeue();
                    }



                }
                else if (input.Key == ConsoleKey.R)
                {
                    if (blockedQueue.Count == 0)
                    {
                        Console.WriteLine("Blocked Queue is Empty");
                    }
                    else
                    {
                        readyQueue.Enqueue(blockedQueue.Dequeue());
                    }
                }

                Console.WriteLine();

                if (process.state == State.NEW)
                {
                    process.state = State.READY;

                }
                if (process.startTime == 0)
                {
                    process.startTime = time;
                }

                if (process.rem > q)
                {
                    Console.WriteLine("{0} quantums of process{1} are running ", q, process.processId);
                    process.rem -= q;
                    time += q;
                }
                else
                {

                    Console.WriteLine("{0} quantums of process {1} are running ", process.rem, process.processId);
                    time += process.rem;

                    process.rem = 0;

                    process.state = State.TERMINATED;
                    process.completionTime = time;




                }
                Console.WriteLine();
                // check if all are printed break loop



                List<Process> newProcess = getAllProcessAtThisTime(processList, time);
                foreach (var item in newProcess)
                {
                    item.state = State.READY;
                    readyQueue.Enqueue(item);
                }
                if (process.state == State.TERMINATED)
                {
                    completedProcess.Add(process);
                }
                else
                {
                    readyQueue.Enqueue(process);
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


        static void calculateAttributes(List<Process> list)
        {
            foreach (Process p in list)
            {
                p.tat = p.completionTime - p.arrivalTime;
                p.wt = p.tat - p.size;
            }
        }

    }
    
}
