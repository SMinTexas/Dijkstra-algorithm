using System;
using System.Collections.Generic;

namespace Dijkstras
{
    class Graph
    {
        readonly Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
        
        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }

        public object shortest_path(char start, char finish)
        {
            var previous = new Dictionary<char, char>();
            var distances = new Dictionary<char, int>();
            var nodes = new List<char>();

            List<char> path = new List<char>();

            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }

            path.Reverse();
            Console.WriteLine("Path: \t" + "A\t" + string.Join("\t", path));
            return path;
        }
    }

    public static class MainClass
    {
        public static void Main(string[] args)
        {
            char[] nodeList = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
            Graph g = new Graph();

            g.add_vertex('A', new Dictionary<char, int>() { { 'B', 2 }, { 'C', 6 } });
            g.add_vertex('B', new Dictionary<char, int>() { { 'A', 2 }, { 'D', 5 } });
            g.add_vertex('C', new Dictionary<char, int>() { { 'A', 6 }, { 'D', 8 } });
            g.add_vertex('D', new Dictionary<char, int>() { { 'B', 5 }, { 'C', 8 }, { 'E', 10 }, { 'F', 15 } });
            g.add_vertex('E', new Dictionary<char, int>() { { 'D', 10 }, { 'F', 6 }, { 'G', 2 } });
            g.add_vertex('F', new Dictionary<char, int>() { { 'E', 10 }, { 'F', 15 }, { 'G', 6 } });
            g.add_vertex('G', new Dictionary<char, int>() { { 'E', 2 }, { 'F', 6 } });

            Console.WriteLine("The source node is " + 'A');
            Console.WriteLine();
            foreach (var node in nodeList)
            {
                if (node != 'A')
                {
                    Console.WriteLine("The shortest path from A to " + node);
                    g.shortest_path('A', node);
                    Console.WriteLine();
                }
            }
        }
    }
}
