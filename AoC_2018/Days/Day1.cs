using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC_2018.Days
{
    public class Day1 : IDay
    {
        public string Title { get; set; }

        public string Part1Solution;
        public string Part2Solution;

        public string[] puzzleInput;
        int currentFrequenzy;
        List<int> foundFrequenzies;
        Part2Monitor myMonitor = new Part2Monitor();

       class Part2Monitor
        {
            public int currentFrequenzy;
            bool monitor;
            public void Start()
            {
                monitor = true;
                while (monitor)
                {
                    Console.Clear();
                    Console.WriteLine("Searching for duplicates: {0}", currentFrequenzy);
                    Thread.Sleep(500);
                }
            }
            public void Stop()
            {
                monitor = false;
            }
        }

        public Day1(string path, string title)
        {
            this.puzzleInput = File.ReadAllLines(path);
            foundFrequenzies = new List<int>();
            this.Title = title;
        }
        public Day1(string[] puzzleInput, string title)
        {
            this.puzzleInput = puzzleInput;
            foundFrequenzies = new List<int>();
            this.Title = title;
        }

        public void SolvePart1()
        {
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                int frequenzyChange;
                int.TryParse(puzzleInput[i], out frequenzyChange);
                currentFrequenzy = currentFrequenzy + frequenzyChange;
            }
            Console.WriteLine("Final Frequency: {0}",currentFrequenzy);
            Part1Solution = currentFrequenzy.ToString();     
        }

        public void SolvePart2()
        {
            Thread MonitoringThread = new Thread(new ThreadStart(myMonitor.Start));
            // reset the frequenzy to 0
            currentFrequenzy = 0;
            bool foundDuplicateFrequency = false;
            MonitoringThread.Start();
            while(!foundDuplicateFrequency)
            {
                for (int i = 0; i < puzzleInput.Length; i++)
                {
                    myMonitor.currentFrequenzy = currentFrequenzy;
                    if(foundFrequenzies.Contains(currentFrequenzy))
                    {
                        foundDuplicateFrequency = true;
                        break;
                    }
                    else
                    {
                        foundFrequenzies.Add(currentFrequenzy);
                        int frequenzyChange;
                        int.TryParse(puzzleInput[i], out frequenzyChange);
                        currentFrequenzy = currentFrequenzy + frequenzyChange;
                    }
                }
            }
            Part2Solution = currentFrequenzy.ToString();
            myMonitor.Stop();
            Console.Clear();
            Console.WriteLine("First duplicate frequenzy found: {0}",currentFrequenzy);
        }
    }
}
