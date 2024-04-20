namespace DM_project;

public abstract class Graph
{
    public static int[,] GraphGenerator(int vertices, int density)
    {
        var graph = new int[vertices, vertices];
        var random = new Random();
        for (var i = 0; i < vertices; i++)
        {
            for (var q = 0; q < vertices; q++)
            {
                if (i == q)
                {
                    graph[i, q] = 0;
                    continue;
                }

                if (i > q)
                {
                    graph[i, q] = graph[q, i];
                    continue;
                }

                var edgeProbability = random.Next(100);
                if (edgeProbability < density)
                {
                    graph[i, q] = random.Next(1, 100);
                }
                else
                {
                    graph[i, q] = 0;
                }
            }
        }

        return graph;
    }

    public static (int, Dictionary<(int, int), int>) ToExplicitForm(int[,] matrixGraph)
    {
        var verticesAmount = matrixGraph.GetLength(0);
        var edges = new Dictionary<(int, int), int>();

        for (var i = 0; i < verticesAmount; i++)
        {
            for (var q = i + 1; q < verticesAmount; q++)
            {
                if (matrixGraph[i, q] != 0)
                {
                    edges[(i, q)] = matrixGraph[i, q];
                }
            }
        }

        return (verticesAmount, edges);
    }
}