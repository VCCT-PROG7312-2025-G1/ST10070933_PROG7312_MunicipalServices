using Microsoft.AspNetCore.Mvc;

namespace ST10070933_PROG7312_MunicipalServices.Services.DataStructures
{
    // This datastructure is used to model department locations and calculate optimal routing paths.
    public class Graph
{
    private readonly Dictionary<string, List<(string Destination, int Weight)>> _adjacencyList = new();

    public void AddNode(string node)
    {
        if (!_adjacencyList.ContainsKey(node))
            _adjacencyList[node] = new List<(string, int)>();
    }

    public void AddEdge(string source, string destination, int weight)
    {
        AddNode(source);
        AddNode(destination);
        _adjacencyList[source].Add((destination, weight));
        _adjacencyList[destination].Add((source, weight)); 
    }

    public Dictionary<string, List<(string, int)>> GetGraph() => _adjacencyList;
}
}
