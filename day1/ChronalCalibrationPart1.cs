using System;
using System.IO;
using System.Linq;

/*
    --- Day 1: Chronal Calibration ---

    The device shows a sequence of changes in frequency (your puzzle input). A value 
    like +6 means the current frequency increases by 6; a value like -3 means the 
    current frequency decreases by 3.

    For example, if the device displays frequency changes of +1, -2, +3, +1, then starting 
    from a frequency of zero, the following changes would occur:

    Current frequency  0, change of +1; resulting frequency  1.
    Current frequency  1, change of -2; resulting frequency -1.
    Current frequency -1, change of +3; resulting frequency  2.
    Current frequency  2, change of +1; resulting frequency  3.
    In this example, the resulting frequency is 3.

    Starting with a frequency of zero, what is the resulting frequency after all of the
    changes in frequency have been applied?

 */

namespace AdventOfCodeDay1
{
    class AdventOfCodeDay1Part1
    {
        static void Main()
        {
            Console.Write("Resulting frequency: {0}\n", File.ReadAllLines("input.txt").Sum(s => int.Parse(s)));
            Console.ReadLine();
        }
    }
}
