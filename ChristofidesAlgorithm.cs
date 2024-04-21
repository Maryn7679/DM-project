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
}
