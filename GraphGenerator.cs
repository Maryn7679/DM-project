namespace DM_project;

public abstract class Graph
{
    public static int[,] GraphGenerator(int vertices, int density)
    {
        var graphMatrix = new int[vertices, vertices];
        var random = new Random();
        for (var i = 0; i < vertices; i++)
        {
            for (var q = 0; q < vertices; q++)
            {
                if (i == q)
                {
                    graphMatrix[i, q] = 0;
                    continue;
                }

                if (i > q)
                {
                    graphMatrix[i, q] = graphMatrix[q, i];
                    continue;
                }

                var edgeProbability = random.Next(100);
                if (edgeProbability < density)
                {
                    if (i == 0)
                    {
                        graphMatrix[i, q] = random.Next(0, 100);
                        continue;
                    }
                    
                    var maxValue = graphMatrix[i, 0] + graphMatrix[0, q];
                    var minValue = Math.Abs(graphMatrix[i, 0] - graphMatrix[0, q]);

                    for (var k = 1; k < i; k++)
                    {
                        if (graphMatrix[i, k] != 0 && graphMatrix[k, q] != 0)
                        {
                            maxValue = int.Min(graphMatrix[i, k] + graphMatrix[k, q], maxValue);
                            minValue = int.Max(Math.Abs(graphMatrix[i, k] - graphMatrix[k, q]), minValue);
                        }
                    }
                    
                    graphMatrix[i, q] = random.Next(minValue, maxValue);
                }
                else
                {
                    graphMatrix[i, q] = 0;
                }
            }
        }

        return graphMatrix;
    }

    public static int[,] ToMatrixForm((int, Dictionary<(int, int), int>) explicitGraph)
    {
        var verticesAmount = explicitGraph.Item1;
        var graphMatrix = new int[verticesAmount, verticesAmount];
        for (var i = 0; i < verticesAmount; i++)
        {
            for (var q = 0; q < verticesAmount; q++)
            {
                if (i == q)
                {
                    graphMatrix[i, q] = 0;
                    continue;
                }

                if (i > q)
                {
                    graphMatrix[i, q] = graphMatrix[q, i];
                    continue;
                }
                
                if (explicitGraph.Item2.ContainsKey((i, q)))
                {
                    graphMatrix[i, q] = explicitGraph.Item2[(i, q)];
                }
            }
        }

        return graphMatrix;
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