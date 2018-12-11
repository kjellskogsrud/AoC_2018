using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2018.Days
{
    class Day5 : IDay
    {
        string[] puzzleInput;
        public Day5(string path, string title)
        {
            Title = title;
            puzzleInput = File.ReadAllLines(path);
        }

        public string Title { get; set; }

        public void SolvePart1()
        {
            Console.WriteLine("Reducing string...");
            int reducedString = ReactPolymer(puzzleInput[0]);
            //string reducedString = ReduceString("dabAcCaCBAcCcaDA");
            Console.WriteLine("Reduced to {0} chars", reducedString);
        }

        public void SolvePart2()
        {
            string alphabeth = "abcdefghijklmnopqrstuvwxyz";
            foreach (char c in alphabeth)
            {
                
                Console.WriteLine("Reducing without polymer: {0}, resulting polymer is {1} units.", c, ReactPolymer(puzzleInput[0].Replace(char.ToUpper(c),'1').Replace(char.ToLower(c),'1')));

            }
        }

        private int ReactPolymer(string polymer)
        {
            Stack<char> reactedPolymer = new Stack<char>();
            foreach (char c in polymer)
            {
                if (!char.IsNumber(c))
                {
                    if (reactedPolymer.Count != 0)
                    {
                        char lastChar = reactedPolymer.Peek();
                        if (char.ToLower(c) == char.ToLower(lastChar) && (char.IsLower(c) && char.IsUpper(lastChar) || char.IsUpper(c) && char.IsLower(lastChar)))
                        {
                            reactedPolymer.Pop();
                        }
                        else
                        {
                            reactedPolymer.Push(c);
                        }
                    }
                    else
                    {
                        reactedPolymer.Push(c);
                    }
                }
            }
            return reactedPolymer.Count();
            

        }
    }
}
