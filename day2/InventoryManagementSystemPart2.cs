using System;
using System.Collections.Generic;
using System.IO;

/*
    --- Day 2: Inventory Management System ---
    ...
    --- Part Two ---

    Confident that your list of box IDs is complete, you're ready to find the boxes full of prototype fabric.
   
    The boxes will have IDs which differ by exactly one character at the same position in both strings. 
    For example, given the following box IDs:

    abcde
    fghij
    klmno
    pqrst
    fguij
    axcye
    wvxyz

    The IDs abcde and axcye are close, but they differ by two characters (the second and fourth). However, 
    the IDs fghij and fguij differ by exactly one character, the third (h and u). Those must be the correct boxes.

    What letters are common between the two correct box IDs? (In the example above, this is found by removing 
    the differing character from either ID, producing fgij.)

 */

namespace AdventOfCodeDay2
{
    class AdventOfCodeDay2Part2
    {
        static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            
            // Some good old (On^2), small input anyways :-)
            foreach (var line in input)
            {
                foreach (var line2 in input)
                {
                    var c1 = line.ToCharArray();
                    var c2 = line2.ToCharArray();

                    var len = c1.Length;
                    var pairs = new List<char>();

                    // Count how many chars match in the Ids
                    for (int i = 0; i < len; i++)
                    {
                        if (c1[i] == c2[i])
                        {
                            pairs.Add(c1[i]);
                        }
                    }
                    
                    // If all but 1 letter match, it must be the result
                    if (pairs.Count == len - 1)
                    {
                        Console.Write("Result is: {0}\n", new string(pairs.ToArray()));
                        Console.ReadLine();
                        return;
                    }
                }
            }

            Console.Write("Finished processing, failed to find result.");
            Console.ReadLine();
        }
    }
}
