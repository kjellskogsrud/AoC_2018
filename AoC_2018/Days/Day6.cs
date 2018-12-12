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
            puzzleInput = File.ReadAllLines(path);
            //puzzleInput = new string[] { "1,1", "6,1", "3,8", "4,3", "5,5", "9,8" };
        }

        public string Title { get; set; }

        public void SolvePart1()
        {
            // test thigns for the sample input
            // I visuallize things for the sample input, but not the actual input
            // so i want to name the inputs with chars.
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            

            // get the points
            int maxX = 0;
            int maxY = 0;
            Point[] points = new Point[puzzleInput.Length];
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                points[i] = new Point();
                points[i].X = int.Parse(puzzleInput[i].Split(',')[0]);
                points[i].Y = int.Parse(puzzleInput[i].Split(',')[1]);

                // for the sample input only use chars
                if (puzzleInput.Length == 6)
                {
                    points[i].Id = alpha.Substring(i,1).ToUpper();
                }
                else
                {
                    points[i].Id = i.ToString();
                }

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
            //printTheGrid(maxX,maxY,grid);

            // Again adding points
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    foreach (Point p in points)
                    {
                        if(p.X == x && p.Y == y)
                        {
                            grid[x, y].ID = p.Id;
                        }
                    }
                }
            }
            //printTheGrid(maxX, maxY, grid);

            // run the grid again, this time calculating distance to other points
            // and setting the closest point
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    grid[x, y].DistanceToPoints(points);
                    grid[x, y].ID = grid[x, y].ClosestPointID();
                }
            }
            //printTheGrid(maxX, maxY, grid);

            List<string> badPoints = new List<string>();
            // so now lets look at bad edge cases. 
            // topRows and bottomRow
            for (int y = 0; y < maxY; y++)
            {
                if(!badPoints.Contains(grid[0,y].ID.ToUpper()))
                {
                    badPoints.Add(grid[0, y].ID.ToUpper());
                }
                if (!badPoints.Contains(grid[maxX-1, y].ID.ToUpper()))
                {
                    badPoints.Add(grid[maxX-1, y].ID.ToUpper());
                }
            }

            // leftRow and rightRow

            for (int x = 0; x < maxX; x++)
            {
                if (!badPoints.Contains(grid[x, 0].ID.ToUpper()))
                {
                    badPoints.Add(grid[x, 0].ID.ToUpper());
                }
                if (!badPoints.Contains(grid[x, maxY-1].ID.ToUpper()))
                {
                    badPoints.Add(grid[x, maxY-1].ID.ToUpper());
                }
            }

            Dictionary<string,Point> finitePoints = new Dictionary<string,Point>();
            foreach (Point p in points)
            {
                // this is not a bad point (aka. Infinite)
                if(!badPoints.Contains(p.Id))
                {
                    Console.WriteLine("Finite point: {0}",p.Id);
                    // do we know of this point?
                    if (!finitePoints.ContainsKey(p.Id.ToLower()))
                    {
                        //if not add point
                        finitePoints.Add(p.Id.ToLower(),p);
                    }

                }
            }

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if(finitePoints.ContainsKey(grid[x, y].ID.ToLower()))
                    {
                        finitePoints[grid[x, y].ID.ToLower()].count++;
                    }
                }
            }

            // then order a list of finite points again
            foreach (Point p in finitePoints.Values.OrderBy(o => o.count).ToList())
            {
                Console.WriteLine("Point : {0}, at distance {1}",p.Id,p.count);
            }

            Console.ReadLine();
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
                    // make a new point
                    Point newPoint = new Point();
                    newPoint.Id = points[i].Id;
                    newPoint.X = points[i].X;
                    newPoint.Y = points[i].Y;

                    if (newPoint.X == this.X && newPoint.Y == this.Y)
                    {
                        newPoint.distanceTo = 0;  
                    }
                    else
                    {
                        newPoint.distanceTo = Math.Abs(points[i].X - X) + Math.Abs(points[i].Y - Y);
                    }
                    listOfPoints.Add(newPoint);
                }
            }
            public string ClosestPointID()
            {
                // The square can return its closest point.
                Stack<Point> orderedPoints = new Stack<Point>(listOfPoints.OrderByDescending(o => o.distanceTo).ToList());

                // Firstly is the closest point = 0?
                // is so we are a point so just return our ID
                if (orderedPoints.Peek().distanceTo == 0)
                    return orderedPoints.Peek().Id;

                // however if not; is the closest point equal distance from the second closest?
                Point closestPoint = orderedPoints.Pop();
                if (closestPoint.distanceTo == orderedPoints.Peek().distanceTo)
                    return ".";
                else
                    return closestPoint.Id.ToLower();
            }


            
        }
        class Point
        {
            public int distanceTo;
            public string Id;
            public int X;
            public int Y;
            public int count;
        }

        private void printTheGrid(int maxX, int maxY, Square[,] grid)
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    Console.Write(grid[x, y].ID);
                }
                Console.Write("\n");
            }
            Console.ReadLine();
        }
    }
}
