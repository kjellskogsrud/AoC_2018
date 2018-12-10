using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2018.Days
{
    public class Day2 : IDay
    {
        public string[] puzzleInput;
        public string Part1Solution;
        public string Part2Solution;

        boxID[] listOfBoxses;
        public Day2(string path, string title)
        {
            this.Title = title;
            puzzleInput = File.ReadAllLines(path);
            listOfBoxses = new boxID[puzzleInput.Length];
        }
        public Day2(string[] puzzleinput, string title)
        {
            this.Title = title;
            puzzleInput = puzzleinput;
            listOfBoxses = new boxID[puzzleInput.Length];
        }

        public string Title { get; set; }



        public void SolvePart1()
        {
            int twos = 0;
            int threes = 0;

            for (int i = 0; i < puzzleInput.Length; i++)
            {
                Dictionary<char, int> charCount = new Dictionary<char, int>();
                // make new box
                boxID thisBox = new boxID();
                thisBox.boxId = puzzleInput[i];
                char[] thisBoxId = puzzleInput[i].ToCharArray();

                for (int j = 0; j < thisBoxId.Length; j++)
                {
                    if(charCount.ContainsKey(thisBoxId[j]))
                    {
                        charCount[thisBoxId[j]]++;
                    }
                    else
                    {
                        charCount.Add(thisBoxId[j], 1);
                    }
                }
                if (charCount.ContainsValue(2))
                {
                    thisBox.twos = 1;
                    twos++;
                }
                if (charCount.ContainsValue(3))
                {
                    thisBox.threes = 1;
                    threes++;
                }
                listOfBoxses[i] = thisBox;
            }
            Part1Solution = (twos * threes).ToString();
            Console.WriteLine("Checksum calculation complet: {0} * {1} = {2}",twos,threes,twos*threes);

        }

        class boxID
        {
            public string boxId;
            public int twos;
            public int threes;
        }


        public void SolvePart2()
        {
            for (int i = 0; i < listOfBoxses.Length; i++)
            {
                int charmismatch= 0;
                char[] thisBoxID = listOfBoxses[i].boxId.ToCharArray();
                char[] commonID = new char[thisBoxID.Length];
                // We only look at forward.
                for (int j = i+1; j < listOfBoxses.Length; j++)
                {
                    charmismatch = 0;
                    char[] CompareBoxID = listOfBoxses[j].boxId.ToCharArray();

                    // looking at each char
                    for (int k = 0; k < thisBoxID.Length; k++)
                    {
                        if (thisBoxID[k] != CompareBoxID[k])
                        {
                            commonID[k] = '#';
                            charmismatch++;
                        }
                        else
                        {
                            commonID[k] = thisBoxID[k];
                        }

                        if (charmismatch > 1)
                            break;
                    }
                    if (charmismatch == 1)
                        break;
                }
                if (charmismatch == 1)
                {
                    Console.Write("Common BoxID is: ");
                    for (int j = 0; j < commonID.Length; j++)
                    {
                        Console.Write(commonID[j]);
                    }
                    Part2Solution = commonID.ToString();
                    break;
                }
            }
        }
    }
}
