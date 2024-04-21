namespace DM_project;

internal class Program
{
    public static void Main()
    {
        var vertices = 400;
        
        var randomGraph = GraphFunctions.GraphGenerator(vertices, 100);
        
        var watch = System.Diagnostics.Stopwatch.StartNew();
// the code that you want to measure comes here
        
        var adjacencyList = GraphFunctions.ToListForm(randomGraph);
        var graph = new Graph(vertices);
        
        //KruskalAlgorithm kruskal = new KruskalAlgorithm(5);
        
        foreach (var kvp in adjacencyList)
        {
            int vertex = kvp.Key; // ершина
            //Console.Write($"Vertex {vertex}: ");
            foreach (var neighbor in kvp.Value)
            {
                int adjacentVertex = neighbor.Key; // сусідня
                int weightOfEdge = neighbor.Value; // вага
                //Console.Write($"({adjacentVertex}, {weightOfEdge}) ");
                graph.AddEdge(vertex, adjacentVertex, weightOfEdge);
            }
            //Console.WriteLine();
            
        }
        List<Edge> minimalSpanningTree = ChristofidesAlgorithm.FindMinimalSpanningTree(graph);

        /*Console.WriteLine("Minimal Spanning Tree:");
        foreach (var edge in minimalSpanningTree)
        {
            Console.WriteLine($"{edge.Source} -- {edge.Destination} : {edge.Weight}");
        }*/

        var perfectMatching = ChristofidesAlgorithm.GetPerfectMatching(minimalSpanningTree, graph);
        /*foreach (var i in perfectMatching.Keys)
        {
            Console.WriteLine($"{i}: {perfectMatching[i]}");
        }*/

        var listTree = GraphFunctions.ToListForm(minimalSpanningTree);
        var listMatching = GraphFunctions.ToListForm(perfectMatching);
        var graphSum = ChristofidesAlgorithm.AddGraphs(listTree, listMatching);
        
        //PrintListGraph(graphSum);
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine($"time: {elapsedMs}");
        
        // Перевірка, що функції працюють. Потім видалимо :)
        //
        //PrintGraph(randomGraph);
        //Console.WriteLine();
        //var graph1 = GraphFunctions.ToListForm(randomGraph);
        //var graph2 = GraphFunctions.ToMatrixForm(graph1);
        //PrintGraph(graph2);
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

    static void PrintListGraph(Dictionary<int, Dictionary<int, int>> graph)
    {
        foreach (var key in graph.Keys)
        {
            foreach (var key2 in graph[key].Keys)
            {
                Console.WriteLine($"{key}: {key2} - {graph[key][key2]}");
            }
        }
    }
}