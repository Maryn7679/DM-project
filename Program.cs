﻿namespace DM_project;

internal static class Program
{
    public static void Main()
    {
        // Перевірка, що функції працюють. Потім видалимо :)
        var randomGraph = GraphFunctions.GraphGenerator(5, 90);
        PrintGraph(randomGraph);
        Console.WriteLine();
        var graph1 = GraphFunctions.ToListForm(randomGraph);
        var graph2 = GraphFunctions.ToMatrixForm(graph1);
        PrintGraph(graph2);
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