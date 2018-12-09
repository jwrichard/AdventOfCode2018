using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
   --- Day 8: Memory Maneuver ---

   The tree is made up of nodes; a single, outermost node forms the tree's root, and 
   it contains all other nodes in the tree (or contains nodes that contain nodes, and so on).
   
   Specifically, a node consists of:
   
   A header, which is always exactly two numbers:
   The quantity of child nodes.
   The quantity of metadata entries.
   Zero or more child nodes (as specified in the header).
   One or more metadata entries (as specified in the header).
   Each child node is itself a node that has its own header, child nodes, and metadata. For example:

    2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2
    A----------------------------------
        B----------- C-----------
                         D-----

    In this example, each node of the tree is also marked with an underline starting with a letter for easier identification. In it, there are four nodes:

    A, which has 2 child nodes (B, C) and 3 metadata entries (1, 1, 2).
    B, which has 0 child nodes and 3 metadata entries (10, 11, 12).
    C, which has 1 child node (D) and 1 metadata entry (2).
    D, which has 0 child nodes and 1 metadata entry (99).
    The first check done on the license file is to simply add up all of the metadata entries. In this example, that sum is 1+1+2+10+11+12+2+99=138.

    What is the sum of all metadata entries on the given input file?

 */

namespace AdventOfCodeDay8
{
    class AdventOfCodeDay8
    {
        static void Main()
        {
            var input = File.ReadAllText("input.txt");
            var tokens = new Queue<int>(input.Split(' ').Select(s => int.Parse(s)));
            var head = new Node(tokens);

            Console.Write("Sum of metadata entries: {0}", head.GetSumOfMetadata());
            Console.ReadLine();
        }
    }

    class Node
    {
        private int ChildrenEntries { get; set; }
        private int MetadataEntries { get; set; }
        private int MetadataSum { get; set; }

        private List<Node> Children { get; set; }

        public Node(Queue<int> queue)
        {
            Children = new List<Node>();
            ChildrenEntries = queue.Dequeue();
            MetadataEntries = queue.Dequeue();

            var remainingChildNodes = ChildrenEntries;
            while (remainingChildNodes > 0)
            {
                Children.Add(new Node(queue));
                remainingChildNodes--;
            }

            var remainingMetadata = MetadataEntries;
            while (remainingMetadata > 0)
            {
                MetadataSum += queue.Dequeue();
                remainingMetadata--;
            }
        }

        public int GetSumOfMetadata()
        {
            return MetadataSum + Children.Sum(c => c.GetSumOfMetadata());
        }
    }
}
