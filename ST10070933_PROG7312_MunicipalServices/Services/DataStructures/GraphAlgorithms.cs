using Microsoft.AspNetCore.Mvc;

namespace ST10070933_PROG7312_MunicipalServices.Services.DataStructures
{
    public static class GraphAlgorithms
    {
        // Dijkstra’s algorithm for shortest path
        public static Dictionary<string, int> Dijkstra(Graph graph, string start)
        {
            var distances = new Dictionary<string, int>();
            var priorityQueue = new MinHeap<(int Distance, string Node)>();
            var visited = new HashSet<string>();

            foreach (var node in graph.GetGraph().Keys)
                distances[node] = int.MaxValue;

            distances[start] = 0;
            priorityQueue.Add((0, start));

            while (priorityQueue.Count > 0)
            {
                var (dist, node) = priorityQueue.Pop();
                if (visited.Contains(node)) continue;
                visited.Add(node);

                foreach (var (neighbor, weight) in graph.GetGraph()[node])
                {
                    int newDist = dist + weight;
                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        priorityQueue.Add((newDist, neighbor));
                    }
                }
            }

            return distances;
        }

        // Simple DFS traversal
        public static List<string> DepthFirstTraversal(Graph graph, string start)
        {
            var visited = new HashSet<string>();
            var result = new List<string>();
            DFSHelper(graph, start, visited, result);
            return result;
        }

        private static void DFSHelper(Graph graph, string node, HashSet<string> visited, List<string> result)
        {
            visited.Add(node);
            result.Add(node);

            foreach (var (neighbor, _) in graph.GetGraph()[node])
            {
                if (!visited.Contains(neighbor))
                    DFSHelper(graph, neighbor, visited, result);
            }
        }

        // Prim’s Algorithm for Minimum Spanning Tree (MST)
        public static List<(string, string, int)> MinimumSpanningTree(Graph graph)
        {
            var result = new List<(string, string, int)>();
            var nodes = graph.GetGraph().Keys.ToList();
            if (nodes.Count == 0) return result;

            var visited = new HashSet<string> { nodes[0] };
            var edges = new MinHeap<(int Weight, string From, string To)>();

            foreach (var (to, weight) in graph.GetGraph()[nodes[0]])
                edges.Add((weight, nodes[0], to));

            while (edges.Count > 0 && visited.Count < nodes.Count)
            {
                var (weight, from, to) = edges.Pop();
                if (visited.Contains(to)) continue;

                visited.Add(to);
                result.Add((from, to, weight));

                foreach (var (next, w) in graph.GetGraph()[to])
                    if (!visited.Contains(next))
                        edges.Add((w, to, next));
            }

            return result;
        }
    }
}