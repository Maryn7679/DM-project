namespace DM_project;

public abstract class GraphFunctions
// Клас для збереження функцій  !!!Не структура даних!!!
// Якщо писати реальний клас для графів (що може і не знадобиться) - краще в іншому файлі
{
    public static int[,] GraphGenerator(int vertices, int density)
        // Створює матрицю рандомного неорієнтованого графа, який має реально можливі відстані
        // (відстань напряму завжди <= відстані через іншу точку)

        // vertices - скільки у графа буде вершин
        // density - щільність; приблизно який відсоток ребер з можливих буде побудовано
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
                        graphMatrix[i, q] = random.Next(1, 100);
                        // (1, 100) - діапазон можливих відстаней між ребрами. Можна змінювати, тільки не на від'ємні
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

    public static int[,] ToMatrixForm(Dictionary<int, Dictionary<int, int>> listGraph)
        // Приймає список суміжності графа; видає його матрицю
        // Список суміжності представлено двома вкладеними словниками; перший - вершина: їй суміжні,
        //                                                             другий - суміжна вершина: відстань до неї
    {
        var verticesAmount = listGraph.Keys.Count;
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

                if (listGraph[i].Keys.Contains(q))
                {
                    graphMatrix[i, q] = listGraph[i][q];
                }
            }
        }

        return graphMatrix;
    }

    public static Dictionary<int, Dictionary<int, int>> ToListForm(int[,] matrixGraph)
        // Приймає матрицю графа; видає його список суміжності
        // Список суміжності представлено аналогічно
    {
        var verticesAmount = matrixGraph.GetLength(0);
        var graph = new Dictionary<int, Dictionary<int, int>>();

        for (var i = 0; i < verticesAmount; i++)
        {
            var edges = new Dictionary<int, int>();
            for (var q = 0; q < verticesAmount; q++)
            {
                if (matrixGraph[i, q] != 0)
                {
                    edges[q] = matrixGraph[i, q];
                }
            }

            graph[i] = edges;
        }

        return graph;
    }

    public static Dictionary<int, Dictionary<int, int>> AddGraphs(Dictionary<int, Dictionary<int, int>> graph1,
        Dictionary<int, Dictionary<int, int>> graph2)
    {
        Dictionary<int, Dictionary<int, int>> newGraph = new Dictionary<int, Dictionary<int, int>>();
        
        foreach (var vertex1 in graph1)
        {
            int v1 = vertex1.Key;
            if (!newGraph.ContainsKey(v1))
                newGraph[v1] = new Dictionary<int, int>();
            
            foreach (var adj1 in vertex1.Value)
            {
                int adjVertex1 = adj1.Key;
                int weight1 = adj1.Value;
                newGraph[v1][adjVertex1] = weight1;
            }
            
            if (graph2.ContainsKey(v1))
            {
                foreach (var adj2 in graph2[v1])
                {
                    int adjVertex2 = adj2.Key;
                    int weight2 = adj2.Value;
                    
                    if (newGraph[v1].ContainsKey(adjVertex2))
                    {
                        newGraph[v1][adjVertex2] += weight2;
                    }
                    else
                    {
                        newGraph[v1][adjVertex2] = weight2;
                    }
                }
            }
        }

        return newGraph;
    }
}
    
    
    
