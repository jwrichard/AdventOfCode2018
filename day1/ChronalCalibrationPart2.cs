using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

/*
    --- Day 1: Chronal Calibration ---
    ...
    --- Part Two ---
    You notice that the device repeats the same frequency change list over and over.
    To calibrate the device, you need to find the first frequency it reaches twice.

    For example, using the same list of changes above (+1, -2, +3, +1), the device would loop as follows:
   
    Current frequency  0, change of +1; resulting frequency  1.
    Current frequency  1, change of -2; resulting frequency -1.
    Current frequency -1, change of +3; resulting frequency  2.
    Current frequency  2, change of +1; resulting frequency  3.
    (At this point, the device continues from the start of the list.)
    Current frequency  3, change of +1; resulting frequency  4.
    Current frequency  4, change of -2; resulting frequency  2, which has already been seen.

    In this example, the first frequency reached twice is 2. Note that your device might need to
    repeat its list of frequency changes many times before a duplicate frequency is found, and that
    duplicates might be found while in the middle of processing the list.

    What is the first frequency your device reaches twice?
 */

namespace AdventOfCodeDay1
{
    class AdventOfCodeDay1Part2
    {
        static void Main()
        {
            // Start timer
            var timer = new Stopwatch();
            timer.Start();

            // Open file and parse as int list
            var numInput = File.ReadAllLines("input.txt").Select(s => int.Parse(s)).ToArray();
            var sumsSeen = new Dictionary<int, bool> { { 0, true } };
            var counter = 0;
            
            var foundResult = false;
            while (!foundResult)
            {
                foreach (var number in numInput)
                {
                    counter += number;
                    if (sumsSeen.ContainsKey(counter))
                    {
                        Console.Write("First duplicate frequency is: {0}\n", counter);
                        foundResult = true;
                        break;
                    }

                    sumsSeen.Add(counter, true);
                }
            }

            // End timer and print results + speed
            timer.Stop();
            var ms = timer.ElapsedMilliseconds;

            Console.Write("Done processing... total ms: {0}\n", ms);
            Console.ReadLine();
        }
    }
}
