﻿namespace DM_project;

internal static class Program
{
    public static void Main()
    {
        var randomGraph = GraphFunctions.GraphGenerator(10, 15);
        var randomGraph2 = GraphFunctions.GraphGenerator(10, 20);
        var adjacencyList = GraphFunctions.ToListForm(randomGraph);
        var adjacencyList2 = GraphFunctions.ToListForm(randomGraph2);
        var newgraph = GraphFunctions.AddGraphs(adjacencyList, adjacencyList2);
        

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
            }
            Console.WriteLine();
            
        }
        List<Edge> minimalSpanningTree = kruskal.FindMinimalSpanningTree();

        Console.WriteLine("Minimal Spanning Tree:");
        foreach (var edge in minimalSpanningTree)
        {
            Console.WriteLine($"{edge.Source} -- {edge.Destination} : {edge.Weight}");
        }
        
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
}