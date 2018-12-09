using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
   --- Day 8: Memory Maneuver ---

   ...

   --- Part Two ---
   The second check is slightly more complicated: you need to find the value of the root node (A in the example above).
   
   The value of a node depends on whether it has child nodes.
   
   If a node has no child nodes, its value is the sum of its metadata entries. So, the value of node B is 10+11+12=33, 
   and the value of node D is 99.
   
   However, if a node does have child nodes, the metadata entries become indexes which refer to those child nodes. 
   A metadata entry of 1 refers to the first child node, 2 to the second, 3 to the third, and so on. The value of 
   this node is the sum of the values of the child nodes referenced by the metadata entries. If a referenced child 
   node does not exist, that reference is skipped. A child node can be referenced multiple time and counts each time
   it is referenced. A metadata entry of 0 does not refer to any child node.

    What is the value of the root node?

 */

namespace AdventOfCodeDay8
{
    class AdventOfCodeDay8Part2
    {
        static void Main()
        {
            var input = File.ReadAllText("input.txt");
            var tokens = new Queue<int>(input.Split(' ').Select(s => int.Parse(s)));
            var head = new Node(tokens);

            Console.Write("Sum of metadata entries: {0}\n", head.GetSumOfMetadata());
            Console.Write("Value of root: {0}\n", head.GetValue());
            Console.ReadLine();
        }
    }

    class Node
    {
        private int ChildrenEntries { get; }
        private int MetadataEntries { get; }
        private int MetadataSum { get; }

        private List<Node> Children { get; }

        private List<int> Metadata { get; }

        public Node(Queue<int> queue)
        {
            Children = new List<Node>();
            Metadata = new List<int>();
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
                var item = queue.Dequeue();
                MetadataSum += item;
                Metadata.Add(item);
                remainingMetadata--;
            }
        }

        public int GetSumOfMetadata()
        {
            return MetadataSum + Children.Sum(c => c.GetSumOfMetadata());
        }

        public int GetValue()
        {
            return !Children.Any() 
                ? MetadataSum 
                : Metadata.Sum(m => Children.ElementAtOrDefault(m - 1) != null ? Children[m - 1].GetValue() : 0);
        }
    }
}
