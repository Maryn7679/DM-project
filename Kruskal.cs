namespace DM_project;

public class Edge : IComparable<Edge>
{
    public int Source { get; }
    public int Destination { get; }
    public int Weight { get; }

    public Edge(int source, int destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }

    public int CompareTo(Edge other)
    {
        return Weight.CompareTo(other.Weight);
    }
}

public class KruskalAlgorithm
{
    private int vertices;
    private List<Edge> edges;

    public KruskalAlgorithm(int vertices)
    {
        this.vertices = vertices;
        edges = new List<Edge>();
    }

    public void AddEdge(int source, int destination, int weight)
    {
        edges.Add(new Edge(source, destination, weight));
    }

    private int Find(int[] parent, int vertex)
    {
        if (parent[vertex] != vertex)
            parent[vertex] = Find(parent, parent[vertex]);
        return parent[vertex];
    }

    private void Union(int[] parent, int[] rank, int x, int y)
    {
        int xRoot = Find(parent, x);
        int yRoot = Find(parent, y);

        if (rank[xRoot] < rank[yRoot])
            parent[xRoot] = yRoot;
        else if (rank[yRoot] < rank[xRoot])
            parent[yRoot] = xRoot;
        else
        {
            parent[xRoot] = yRoot;
            rank[yRoot]++;
        }
    }

    public List<Edge> FindMinimalSpanningTree()
    {
        List<Edge> minimalSpanningTree = new List<Edge>();

        // Сортую ребра за вагою тут
        edges.Sort();

        int[] parent = new int[vertices];
        int[] rank = new int[vertices];

        for (int i = 0; i < vertices; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }

        int edgeIndex = 0;
        int treeEdgeCount = 0;

        while (treeEdgeCount < vertices - 1)
        {
            Edge nextEdge = edges[edgeIndex++];

            int sourceRoot = Find(parent, nextEdge.Source);
            int destinationRoot = Find(parent, nextEdge.Destination);

            if (sourceRoot != destinationRoot)
            {
                minimalSpanningTree.Add(nextEdge);
                Union(parent, rank, sourceRoot, destinationRoot);
                treeEdgeCount++;
            }
        }

        return minimalSpanningTree;
    }
}