using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2018.Days
{
    class Day6 : IDay
    {
        string[] puzzleInput;
        Square[,] grid;
        
        public Day6(string path, string title)
        {
            Title = title;
            //puzzleInput = File.ReadAllLines(path);
            puzzleInput = new string[] { "1,1", "6,1", "3,8", "4,3", "5,5", "9,8" };
        }

        public string Title { get; set; }

        public void SolvePart1()
        {
            // get the points
            int maxX = 0;
            int maxY = 0;
            Point[] points = new Point[puzzleInput.Length];
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                points[i].X = int.Parse(puzzleInput[i].Split('.')[0]);
                points[i].Y = int.Parse(puzzleInput[i].Split('.')[1]);

                points[i].Id = i.ToString();

                maxX = points[i].X > maxX ? points[i].X : maxX;
                maxY = points[i].Y > maxY ? points[i].Y : maxY;
            }
            // increase maxX and maxY to accomodate edge cases.
            maxX++;
            maxY++;

            // Then make a grid of square's
            grid = new Square[maxX, maxY];

            // populate the grid
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    Square newSquare = new Square(x, y);
                    grid[x, y] = newSquare;
                }
            }

            // run the grid again, this time calculating distance to other points
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    grid[x, y].DistanceToPoints(points);
                }
            }


        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        class Square
        {

            public int X { get; protected set; }
            public int Y { get; protected set; }

            List<Point> listOfPoints;

            public string ID = ".";

            public Square (int x, int y)
            {
                this.X = x;
                this.Y = y;
                listOfPoints = new List<Point>();
            }
            public void DistanceToPoints(Point[] points)
            {
                listOfPoints.Clear();
                for (int i = 0; i < points.Length; i++)
                {
                    /*
                    if()
                    points[i].distanceTo = 
                    listOfPoints.Add(points[i]);
                    */

                }
            }
            public string ClosestPointID()
            {




                return ".";
            }


            
        }
        struct Point
        {
            public int distanceTo;
            public string Id;
            public int X;
            public int Y;
        }
    }
}
