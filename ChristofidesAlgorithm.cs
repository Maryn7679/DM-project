//Kruskal
namespace DM_project;

public class ChristofidesAlgorithm
{
    public static Dictionary<int, (int, int)> GetPerfectMatching(List<Edge> tree, Graph graph)
    {
        var oddVertices = PerfectMatching.GetOddVertices(tree);
        var subgraph = PerfectMatching.GetSubgraph(oddVertices, graph);
        var anyMatching = PerfectMatching.GetAnyPerfectMatching(subgraph);
        var listGraph = GraphFunctions.ToListForm(graph.Edges());
        var perfectMatching = PerfectMatching.GetBetterMatching(anyMatching, listGraph);

        return perfectMatching;
    }
    public static List<Edge> FindMinimalSpanningTree(Graph graph)
    {
        List<Edge> minimalSpanningTree = new List<Edge>();

        // Сортую ребра за вагою тут
        graph.Edges().Sort();

        int[] parent = new int[graph.Vertices()];
        int[] rank = new int[graph.Vertices()];

        for (int i = 0; i < graph.Vertices(); i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }

        int edgeIndex = 0;
        int treeEdgeCount = 0;

        while (treeEdgeCount < graph.Vertices() - 1)
        {
            Edge nextEdge = graph.Edges()[edgeIndex++];

            int sourceRoot = graph.Find(parent, nextEdge.Source);
            int destinationRoot = graph.Find(parent, nextEdge.Destination);

            if (sourceRoot != destinationRoot)
            {
                minimalSpanningTree.Add(nextEdge);
                graph.Union(parent, rank, sourceRoot, destinationRoot);
                treeEdgeCount++;
            }
        }

        return minimalSpanningTree;
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
