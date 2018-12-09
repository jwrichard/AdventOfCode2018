using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing;

/*
    --- Day 3: No Matter How You Slice It ---
    ...

    --- Part Two ---

    Amidst the chaos, you notice that exactly one claim doesn't overlap by even a single 
    square inch of fabric with any other claim. If you can somehow draw attention to it, 
    maybe the Elves will be able to make Santa's suit after all!

    For example, in the claims above, only claim 3 is intact after all claims are made.

    What is the ID of the only claim that doesn't overlap?

 */

namespace AdventOfCodeDay3
{
    class AdventOfCodeDay3Part2
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

            // Now find the claim that has no overlaps
            foreach (var claim in claims)
            {
                var overlaps = false;
                foreach (var point in claim.CoveredCoordinates)
                {
                    if (pointCount[point] > 1)
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    Console.Write("Claim with no overlaps: {0}", claim);
                    Console.ReadLine();
                    return;
                }
            }

            Console.Write("Failed to find claim with no overlaps");
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
