namespace DM_project;


public class Graph
{
    public int[,] GraphGenerator(int vertices, int density)
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

                var edgeProbability = random.Next(100);
                if (edgeProbability < density)
                {
                    graph[i, q] = random.Next(1, 1000);
                }
                else
                {
                    graph[i, q] = 0;
                }
            }
        }

        return graph;
    }
}