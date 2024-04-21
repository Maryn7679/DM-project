namespace DM_project;

public class PerfectMatching
{
    public static Dictionary<int, (int, int)> GetBetterMatching
        (Dictionary<int, (int, int)> pairs, Dictionary<int, Dictionary<int, int>> graph)
    {
        var betterPairs = pairs;
        for (var i = 0; i < pairs.Count * 2; i += 2)
        {
            for (var q = i; q < pairs.Count * 2; q += 2)
            {
                if (graph[i][i + 1] + graph[q][q + 1] < graph[i + 1][q] + graph[q + 1][i])
                {
                    betterPairs[i] = (q + 1, graph[i][q + 1]);
                    betterPairs[q] = (i + 1, graph[q][i + 1]);
                }
                else
                {
                    betterPairs[i] = (i + 1, graph[i][i + 1]);
                    betterPairs[q] = (q + 1, graph[q][q + 1]);
                }
            }
        }

        return betterPairs;
    }
    
    public static Dictionary<int, (int, int)> GetAnyPerfectMatching(Dictionary<int, Dictionary<int, int>> graph)
    {
        var matching = new Dictionary<int, (int, int)>();
        
        for (var i = 0; i < graph.Count; i += 2)
        {
            int distance;
            if (!graph[i].ContainsKey(i + 1))
            {
                distance = 100000;
            }
            else
            {
                distance = graph[i][i + 1];
            }
            matching[i] = (i + 1, distance);
        }

        return matching;
    }

    public static Dictionary<int, Dictionary<int, int>> GetSubgraph(List<int> vertices, Graph graph)
    {
        var listSubgraph = new Dictionary<int, Dictionary<int, int>>();
        
        foreach (var edge in graph.Edges())
        {
            if (vertices.Contains(edge.Destination) && vertices.Contains(edge.Source))
            {
                if (!listSubgraph.ContainsKey(edge.Source))
                {
                    listSubgraph[edge.Source] = new Dictionary<int, int>();
                }
                
                listSubgraph[edge.Source][edge.Destination] = edge.Weight;
            }
        }

        return listSubgraph;
    }
    
    public static List<int> GetOddVertices(Graph tree)
    {
        var listTree = GraphFunctions.ToListForm(tree.Edges());
        var oddVerticesList = new List<int>();

        for (var vertice = 0; vertice < tree.Vertices(); vertice++)
        {
            if (listTree[vertice].Count % 2 == 1)
            {
                oddVerticesList.Add(vertice);
            }
        }

        return oddVerticesList;
    }
}