namespace DM_project;
internal static class Program
{
    public static void Main()
    {
        var randomGraph = GraphFunctions.GraphGenerator(5, 90);
        var adjacencyList = GraphFunctions.ToListForm(randomGraph);
        var graph = new Graph(5);

        KruskalAlgorithm kruskal = new KruskalAlgorithm(5);

        foreach (var kvp in adjacencyList)
        {
            int vertex = kvp.Key; // ершина
            Console.Write($"Vertex {vertex}: ");
            foreach (var neighbor in kvp.Value)
            {
                int adjacentVertex = neighbor.Key; // сусідня
                int weightOfEdge = neighbor.Value; // вага
                Console.Write($"({adjacentVertex}, {weightOfEdge}) ");
                kruskal.AddEdge(vertex, adjacentVertex, weightOfEdge);
                graph.AddEdge(vertex, adjacentVertex, weightOfEdge);
            }
            Console.WriteLine();

        }
        
        List<Edge> minimalSpanningTree = kruskal.FindMinimalSpanningTree();;

        Console.WriteLine("Minimal Spanning Tree:");
        foreach (var edge in minimalSpanningTree)
        {
            Console.WriteLine($"{edge.Source} -- {edge.Destination} : {edge.Weight}");
        }

        var perfectMatching = ChristofidesAlgorithm.GetPerfectMatching(minimalSpanningTree, graph);
        foreach (var i in perfectMatching.Keys)
        {
            Console.WriteLine($"{i}: {perfectMatching[i]}");
        }
    }

    static void PrintGraph(int[,] graph)
        // Просто вивід матриці, щоб читати можна було
    {
        for (var a = 0; a < graph.GetLength(0); a++)
        {
            var b = "";
            for (var c = 0; c < graph.GetLength(1); c++)
            {
                b += graph[a, c].ToString() + ", ";
            }
            Console.WriteLine(b);
        }
    }
}