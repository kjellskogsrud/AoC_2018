using AoC_2018.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int,IDay> listOfDays = new Dictionary<int,IDay>();


            //Make All the days, and add to the list of days
            Day1 day1 = new Day1("input/day1.txt", "Chronal Calibration");
            Day2 day2 = new Day2("input/day2.txt", "Inventory Management System");
            Day3 day3 = new Day3("input/day3.txt", "No Matter How You Slice It");
            Day4 day4 = new Day4("input/day4.txt", "Repose Record");
            Day5 day5 = new Day5("input/day5.txt", "Alchemical Reduction");
            Day6 day6 = new Day6("input/day6.txt", "Chronal Coordinates");

            // Add all days to the list
            listOfDays.Add(1, day1);
            listOfDays.Add(2, day2);
            listOfDays.Add(3, day3);
            listOfDays.Add(4, day4);
            listOfDays.Add(5, day5);
            listOfDays.Add(6, day6);
            
            // Infinite loop time!
            while(true)
            {
                // Start Fresh
                Console.Clear();

                // Present a list of days
                for (int i = 1; i <= listOfDays.Values.Count; i++)
                {
                    Console.WriteLine("--- Day {0}: {1} ---",i,listOfDays[i].Title);
                }
                Console.WriteLine("--- Day X: Exit the application day!"); // Special Exit day
                Console.WriteLine(); // Empty line for formating
                Console.WriteLine("Select a day by entering its number below:");
                Console.Write(">");

                string daySelectString = Console.ReadLine();

                // Some input handeling
                // Lets assume that what was entered was a number
                int enteredNumber = -1;
                int.TryParse(daySelectString, out enteredNumber);

                
                if(int.TryParse(daySelectString, out enteredNumber))
                {
                    // So it was a number, but is it a valid number?
                    if (enteredNumber > 0 && enteredNumber <= listOfDays.Count)
                    {
                        IDay selectedDay = listOfDays[enteredNumber];
                        // Clear the screen again
                        Console.Clear();

                        // Present Options
                        Console.WriteLine("------ Part One ------");
                        Console.WriteLine("--- Enter To begin ---");
                        Console.ReadLine();
                        Console.WriteLine("------ Starting ------");
                        try
                        {
                            selectedDay.SolvePart1();

                            Console.WriteLine(); // Empty line for formating
                            Console.WriteLine("------ Part Two ------");
                            Console.WriteLine("--- Enter To begin ---");
                            Console.ReadLine();
                            Console.WriteLine("------ Starting ------");

                            selectedDay.SolvePart2();

                            Console.WriteLine(); // Empty line for formating
                            Console.WriteLine("--- Day {0}: {1} --- Compleat!", enteredNumber, selectedDay.Title);
                            Console.WriteLine("Enter to return to list.");
                            Console.ReadLine();
                        }
                        catch (NotImplementedException)
                        {
                            Console.WriteLine("Looks like this solution is not ready yet");
                            Console.WriteLine("Enter to return to list.");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number: {0}. Enter to try again.", enteredNumber);
                        Console.ReadLine();
                    }
                }
                else
                {
                    // Probably not a number then
                    // the only valid non number option is a string that starts with X or x to exit the application
                    // Is it something we can turn into a char array?
                    if(daySelectString != null && daySelectString != "")
                    {
                        if (daySelectString.ToLower().ToCharArray()[0] == 'x')
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Did not understand {0}. Enter to try again.", daySelectString);
                            Console.ReadLine();
                        }

                    }
                    else
                    {
                        Console.WriteLine("You need to enter an option. Enter to try again.", daySelectString);
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
