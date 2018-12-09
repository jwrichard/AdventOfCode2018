using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing;

/*
    --- Day 3: No Matter How You Slice It ---

    Each Elf has made a claim about which area of fabric would be ideal for Santa's suit. All 
    claims have an ID and consist of a single rectangle with edges parallel to the edges of the 
    fabric. Each claim's rectangle is defined as follows:
   
    The number of inches between the left edge of the fabric and the left edge of the rectangle.
    The number of inches between the top edge of the fabric and the top edge of the rectangle.
    The width of the rectangle in inches.
    The height of the rectangle in inches.

    A claim like #123 @ 3,2: 5x4 means that claim ID 123 specifies a rectangle 3 inches from the 
    left edge, 2 inches from the top edge, 5 inches wide, and 4 inches tall. Visually, it claims 
    the square inches of fabric represented by # (and ignores the square inches of fabric 
    represented by .) in the diagram below:

    ...........
    ...........
    ...#####...
    ...#####...
    ...#####...
    ...#####...
    ...........
    ...........
    ...........

    The problem is that many of the claims overlap, causing two or more claims to cover part of the 
    same areas. For example, consider the following claims:

    #1 @ 1,3: 4x4
    #2 @ 3,1: 4x4
    #3 @ 5,5: 2x2
    Visually, these claim the following areas:

    ........
    ...2222.
    ...2222.
    .11XX22.
    .11XX22.
    .111133.
    .111133.
    ........
    The four square inches marked with X are claimed by both 1 and 2. (Claim 3, while adjacent to the
    others, does not overlap either of them.)

    If the Elves all proceed with their own plans, none of them will have enough fabric. 
    
    How many square inches of fabric are within two or more claims?

 */

namespace AdventOfCodeDay3
{
    class AdventOfCodeDay3Part1
    {
        static void Main()
        {
            // Read each line from file and parse into claim object
            var claims = File.ReadAllLines("input.txt").Select(i => new Claim(i)).ToArray();

            var pointCount = new Dictionary<Point, int>();
            var allPoints = claims.SelectMany(c => c.CoveredCoordinates);

            // Get a count of how many at each point
            foreach (var point in allPoints)
            {
                if (pointCount.ContainsKey(point))
                {
                    pointCount[point] = pointCount[point] + 1;
                }
                else
                {
                    pointCount.Add(point, 1);
                }
            }

            // Count how many points have 2 or more at that spot and we're done! :D
            Console.Write("Overlapping inches: {0}", pointCount.Count(kvp => kvp.Value > 1));
            Console.ReadLine();
        }
    }

    class Claim
    {
        private int Id { get; }
        private int LeftOffset { get; }
        private int TopOffset { get; }
        private int Width { get; }
        private int Height { get; }

        public List<Point> CoveredCoordinates { get; set; }

        public Claim(string input)
        {
            // Claim is of the pattern "#1 @ 1,3: 4x4", of varying number sizes
            string pattern = @"#(\d*)\s@\s(\d*),(\d*):\s(\d*)x(\d*)";
            Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            if (m.Success)
            {
                Id = int.Parse(m.Groups[1].ToString());
                LeftOffset = int.Parse(m.Groups[2].ToString());
                TopOffset = int.Parse(m.Groups[3].ToString());
                Width = int.Parse(m.Groups[4].ToString());
                Height = int.Parse(m.Groups[5].ToString());

                CoveredCoordinates = new List<Point>();

                for (int x = LeftOffset; x < LeftOffset + Width; x++)
                {
                    for (int y = TopOffset; y < TopOffset + Height; y++)
                    {
                        CoveredCoordinates.Add(new Point(x, y));
                    }
                }
            }
            else
            {
                throw new Exception("Input format doo-doo :(");
            }
        }

        public override string ToString()
        {
            return $"#{Id} @ {LeftOffset},{TopOffset}: {Width}x{Height}\n";
        }
    }
}
