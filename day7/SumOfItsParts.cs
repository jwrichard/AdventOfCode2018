using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
    --- Day 7: The Sum of Its Parts ---

    The instructions specify a series of steps and requirements about which steps must 
    be finished before others can begin (your puzzle input). Each step is designated by 
    a single letter. For example, suppose you have the following instructions:
   
        Step C must be finished before step A can begin.
        Step C must be finished before step F can begin.
        Step A must be finished before step B can begin.
        Step A must be finished before step D can begin.
        Step B must be finished before step E can begin.
        Step D must be finished before step E can begin.
        Step F must be finished before step E can begin.

    Your first goal is to determine the order in which the steps should be completed. If more than one step is ready, choose the step which is first alphabetically. In this example, the steps would be completed as follows:

    Only C is available, and so it is done first.
    Next, both A and F are available. A is first alphabetically, so it is done next.
    Then, even though F was available earlier, steps B and D are now also available, and B is the first alphabetically of the three.
    After that, only D and F are available. E is not available because only some of its prerequisites are complete. Therefore, D is completed next.
    F is the only choice, so it is done next.
    Finally, E is completed.
    So, in this example, the correct order is CABDFE.

    In what order should the steps in your instructions be completed?
 */

namespace AdventOfCodeDay7
{
    class AdventOfCodeDay7
    {
        static void Main()
        {
            // We know every letter is included so create each list ahead of time
            var steps = new Dictionary<string, List<string>>()
            {
                { "A", new List<string>() },
                { "B", new List<string>() },
                { "C", new List<string>() },
                { "D", new List<string>() },
                { "E", new List<string>() },
                { "F", new List<string>() },
                { "G", new List<string>() },
                { "H", new List<string>() },
                { "I", new List<string>() },
                { "J", new List<string>() },
                { "K", new List<string>() },
                { "L", new List<string>() },
                { "M", new List<string>() },
                { "N", new List<string>() },
                { "O", new List<string>() },
                { "P", new List<string>() },
                { "Q", new List<string>() },
                { "R", new List<string>() },
                { "S", new List<string>() },
                { "T", new List<string>() },
                { "U", new List<string>() },
                { "V", new List<string>() },
                { "W", new List<string>() },
                { "X", new List<string>() },
                { "Y", new List<string>() },
                { "Z", new List<string>() }
            };
            var result = string.Empty;
            var emptyKvp = new KeyValuePair<string, List<string>>();

            // Each line contains 1 letter and a letter that blocks it
            string[] lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var blockedByLetter = line.Substring(5, 1);
                var letter = line.Substring(36, 1);
                steps[letter].Add(blockedByLetter);
            }

            while (true)
            {
                // Debug: Print out each remaining letter and what its blocked by
                foreach (var kvp in steps)
                {
                    Console.Write("{0} is blocked by: {1}\n", kvp.Key, String.Join(String.Empty, kvp.Value.ToArray()));
                }

                // Select next letter by sorting by remaining letters by count of blocking letters
                var nextItem = steps.OrderBy(kvp => kvp.Value.Count).ThenBy(kvp => kvp.Key).FirstOrDefault();
                if (nextItem.Equals(emptyKvp))
                {
                    // Must be bad input file
                    Console.Write("Failed to find next item to use.\n");
                    break;
                }
                
                Console.Write("Found optimal item, removing key {0}\n", nextItem.Key);
                foreach (var kvp in steps)
                {
                    kvp.Value.RemoveAll(k => k == nextItem.Key);
                }

                result += nextItem.Key;
                steps.Remove(nextItem.Key);
            }
            
            Console.Write("Final order is: {0}\n", result);
            Console.ReadLine();
        }
    }
}
