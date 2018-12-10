using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2018.Days
{
    class Day4 : IDay
    {

        string[] puzzleInput;
        Dictionary<string,NightShift> nightShifts;
        Dictionary<string,Guard> allGuards;

        public Day4(string path, string title)
        {
            puzzleInput = File.ReadAllLines(path);
            this.Title = title;

            // Init list
            nightShifts = new Dictionary<string,NightShift>();
            allGuards = new Dictionary<string,Guard>();

        }

        /*
         *  [1518-11-01 00:00] Guard #10 begins shift
            [1518-11-01 00:05] falls asleep
            [1518-11-01 00:25] wakes up
            [1518-11-01 00:30] falls asleep
            [1518-11-01 00:55] wakes up
            [1518-11-01 23:58] Guard #99 begins shift
            [1518-11-02 00:40] falls asleep
            [1518-11-02 00:50] wakes up
        */


        public string Title { get; set; }

        public void SolvePart1()
        {
            // First lets parse the array input and add everything to the list
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                
                // Break the line into a word array
                string[] wordArray = puzzleInput[i].Split(' ');

                // The first two element makes the date and time.
                // [1518-11-01 00:00]
                int month = int.Parse(wordArray[0].Substring(6,2));
                int day = int.Parse(wordArray[0].Substring(9,2));
                int hour = int.Parse(wordArray[1].Substring(0, 2));
                int minute = int.Parse(wordArray[1].Substring(3, 2));

                // There is a cheeky gottcha in this where guards can start the shift on the day before
                // like this: [1518-11-01 23:58] Guard #99 begins shift
                // We can check for that by getting the hour component, and checking if it is differant from 0;
                if (hour != 0)
                    day++;

                // also it may not be nessasary but for sake of compleation we need to wrap the days in months
                // I did see a case where this will be required in my input, but the wrap around around for
                // the year is not required since every timestamp is from 1518, and there are no dates from month 12 in my input
                switch (month)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        if(day > 31)
                        {
                            day = 1;
                            month++;
                        }
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        if (day > 30)
                        {
                            day = 1;
                            month++;
                        }

                        break;
                    case 2:
                        if (day > 28)
                        {
                            day = 1;
                            month++;
                        }
                        break;
                    default:
                        break;
                }

                // now we have valid dates and times for things. the ways the problem is described we know that no only guard changes can happen
                // before midnight, and that we can safely assume that the guards only sleep between 00 and 59.
                // the important thing to know that will be in out dictionary of shifts is the date.
                // Referance to the nightshift we are lookin at:
                NightShift thisShift;

                string monthDayString = month.ToString().PadLeft(2,'0') + "-" + day.ToString().PadLeft(2, '0');

                // is this key her already?
                if (nightShifts.ContainsKey(monthDayString))
                {
                    thisShift = nightShifts[monthDayString];
                }
                // else make a new shift and add it.
                else
                {
                    thisShift = new NightShift(monthDayString);
                    nightShifts.Add(monthDayString, thisShift);
                }
                // now its time to add new information to the shift.
                // there are 3 possible things that can be added at this point (Guard, falls or wakes)
                // [1518-11-01 00:00] Guard #10 begins shift
                // [1518-11-01 00:05] falls asleep
                // [1518-11-01 00:25] wakes up
                switch (wordArray[2])
                {
                    case "Guard":
                        // this tells us about the guard for this shift
                        thisShift.GuardID = wordArray[3];
                        // does this guard exist in our list of guards?
                        if(!allGuards.ContainsKey(wordArray[3]))
                        {
                            allGuards.Add(wordArray[3], new Guard(wordArray[3]));
                        }
                        break;
                    case "falls":
                        thisShift.Sleeing[minute] = 1;
                        break;
                    case "wakes":
                        thisShift.Sleeing[minute] = 0;
                        break;
                    default:
                        break;
                }
            }

            // Now the entire list has been made and can be sorted
            var l = nightShifts.OrderBy(key => key.Key);
            var dic = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            nightShifts = dic;


            Console.Clear();
            Console.WriteLine("Date\tID\tMinute");
            Console.WriteLine("\t\t000000000011111111112222222222333333333344444444445555555555");
            Console.WriteLine("\t\t012345678901234567890123456789012345678901234567890123456789");
            
            foreach (var item in nightShifts)
            {
                
                // this list is sorted, but that matters little for this operation:
                // we added awake and sleep info when we read the input list, but only the point where the guard falls asleep, or wakes up.
                // so now we need to padd out the stages in between

                // this is where we store what happened last minute.
                int lastMinute = 0;

                // now we iterate
                for (int i = 0; i < item.Value.Sleeing.Length; i++)
                {
                    // is this element blank(-1)?
                    if (item.Value.Sleeing[i] == -1)   
                    {
                        // then write the status of the last minute
                        item.Value.Sleeing[i] = lastMinute;
                    }
                    // if this wasn't -1, then is is either 0(awake) or 1(sleeping).
                    else
                    {
                        // regardless of what is was, we don't want to update it, but we want to save this as the last thign 
                        // that has happend
                        lastMinute = item.Value.Sleeing[i];
                    }
                }


                // now we print the cool graph because we like cool things
                // we also take the oppertunity to count the sleeping minutes of each guard since we are itterating anyway
                Console.Write("{0}\t{1}\t", item.Value.Date,item.Value.GuardID);
                int shiftSleeing = 0;
                for (int i = 0; i < item.Value.Sleeing.Length; i++)
                {
                    if (item.Value.Sleeing[i] == 0)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write("#");
                        shiftSleeing++;
                    }
                }
                // add this sleep to the guards overall sleep.
                allGuards[item.Value.GuardID].sleepingHours = allGuards[item.Value.GuardID].sleepingHours + shiftSleeing;
                Console.Write("\n");

            }

            Console.WriteLine("There are {0} unique guards in this list",allGuards.Count); 
            Console.ReadLine();
            int sleepMax = 0;
            string guardID = ""; 
            foreach (Guard g in allGuards.Values)
            {
                if(g.sleepingHours > sleepMax)
                {
                    sleepMax = g.sleepingHours;
                    guardID = g.guardID;
                }
            }
            Console.WriteLine("{0} sleeps the most with a total of {1} minutes", guardID, sleepMax);
            Console.ReadLine();

            int[] sleepMinutes = new int[60];
            // now lets analyze ever minute where our guard sleeps, and count out when he sleeps most often
            for (int i = 0; i < sleepMinutes.Length; i++)
            {
                foreach (NightShift ns in nightShifts.Values)
                {
                    // is if the correct guard?
                    if (ns.GuardID == guardID)
                    {
                        sleepMinutes[i] = sleepMinutes[i] + ns.Sleeing[i];
                    }
                }

            }
            // now that sleep minutes contains a record of how often he sleeps for each minute we can find the max
            int maxSleeptMinute = 0;
            int maxSleeptMinuteID = 0;
            for (int i = 0; i < sleepMinutes.Length; i++)
            {
                if(sleepMinutes[i] > maxSleeptMinute)
                {
                    maxSleeptMinute = sleepMinutes[i];
                    maxSleeptMinuteID = i;
                }
            }
            Console.WriteLine("That Guard sleeps most often on minute {0}",maxSleeptMinuteID);
            
        }

        /// <summary>
        /// Simple class to hold information about nightshifts
        /// </summary>
        class NightShift
        {
            public string Date = "NoDate";
            public string GuardID = "NoID";

            // the sleep array indicates awake/sleeping, 0 = awake, and 1 = sleeping
            public int[] Sleeing = new int[60];

            public NightShift(string date)
            {
                Date = date;
                // when shifts are made, we -1 the array for sleeping
                for (int i = 0; i < Sleeing.Length; i++)
                {
                    Sleeing[i] = -1;
                }
            }
        }

        /// <summary>
        /// class for identifying holding information about unique guards.
        /// </summary>
        class Guard
        {
            public string guardID;
            public int sleepingHours;

            public Guard(string guardID)
            {
                this.guardID = guardID;
            }
        }


        public void SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
