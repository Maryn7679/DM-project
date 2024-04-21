//Kruskal
namespace DM_project;

public class ChristofidesAlgorithm
{
    public static Dictionary<int, (int, int)> GetPerfectMatching(List<Edge> tree, Graph graph)
    {
        var oddVertices = PerfectMatching.GetOddVertices(tree);
        var subgraph = PerfectMatching.GetSubgraph(oddVertices, graph);
        var anyMatching = PerfectMatching.GetAnyPerfectMatching(subgraph);
        //var listGraph = GraphFunctions.ToListForm(graph.Edges());
        //var perfectMatching = PerfectMatching.GetBetterMatching(anyMatching, listGraph);

        return anyMatching;
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
}
