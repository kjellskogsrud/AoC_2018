using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2018.Days
{
    public class Day3 : IDay
    {
        int[,] cloth = new int[1000, 1000];
        List<Claim> claims;


        string[] puzzleInput;
        public Day3(string path, string title)
        {
            this.Title = title;
            this.puzzleInput = File.ReadAllLines(path);

            claims = new List<Claim>();

            // Init the array with 0
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    cloth[i, j] = 0;
                }
            }
        }

        public string Title { get; set; }

        public void SolvePart1()
        {
            
            /*
                #1 @ 108,350: 22x29
                #2 @ 370,638: 13x12
                #3 @ 242,156: 26x23
                #4 @ 638,540: 14x27
            */
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                // Read out the raw values
                string startingCoordinates = puzzleInput[i].Split(' ')[2];
                string patchSize = puzzleInput[i].Split(' ')[3];

                // Split the coords
                int startX;
                int startY;

                int.TryParse(startingCoordinates.Split(',')[0], out startX);
                int.TryParse(startingCoordinates.Split(',')[1].Replace(":", ""), out startY);

                // Split the size
                int sizeX;
                int sizeY;

                int.TryParse(patchSize.Split('x')[0], out sizeX);
                int.TryParse(patchSize.Split('x')[1], out sizeY);

                Claim thisClaim = new Claim();
                thisClaim.startX = startX;
                thisClaim.startY = startY;
                thisClaim.sizeX = sizeX;
                thisClaim.sizeY = sizeY;
                thisClaim.ClaimID = puzzleInput[i].Split(' ')[0];

                claims.Add(thisClaim);

                for (int x = startX; x < startX+sizeX; x++)
                {
                    for (int y = startY; y < startY+sizeY; y++)
                    {
                        cloth[x, y]++;
                    }
                }
            }

            // Count the cloths where the value is more then 1.
            int countOverlaps = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if(cloth[i,j] > 1)
                    {
                        countOverlaps++;
                    }
                }
            }
            Console.WriteLine("Count is {0}",countOverlaps);
        }

        public void SolvePart2()
        {
            foreach(Claim c in claims)
            {
                // Assume this claim is good.
                bool goodClaim = true;
                for (int x = c.startX; x < c.startX + c.sizeX; x++)
                {
                    for (int y = c.startY; y < c.startY + c.sizeY; y++)
                    {
                        if (cloth[x, y] > 1)
                            goodClaim = false;
                    }
                }
                if (goodClaim)
                {
                    Console.WriteLine("Good Claim: {0}", c.ClaimID);
                }
            }
        }

        class Claim
        {
            public int startX;
            public int startY;

            public int sizeX;
            public int sizeY;

            public string ClaimID;
        }
    }
}
