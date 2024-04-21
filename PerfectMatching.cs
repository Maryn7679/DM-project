namespace DM_project;

public class PerfectMatching
{
    public static List<int> GetOddVertices(List<Edge> tree)
    {
        var listTree = GraphFunctions.ToListForm(tree);
        var oddVerticesList = new List<int>();

        foreach (var vertice in listTree.Keys)
        {
            if (listTree[vertice].Count % 2 == 1)
            {
                oddVerticesList.Add(vertice);
            }
        }

        return oddVerticesList;
    }
}