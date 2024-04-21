namespace DM_project;

public class Graph()
{
    private int _vertices;
    private List<Edge> _edges = [];

    public Graph(int vertices) : this()
    {
        _vertices = vertices;
    }
    
    public int Vertices()
    {
        return _vertices;
    }

    public List<Edge> Edges()
    {
        return _edges;
    }
    
    public void AddEdge(int source, int destination, int weight)
    {
        _edges.Add(new Edge(source, destination, weight));
    }
    
    public int Find(int[] parent, int vertex)
    {
        if (parent[vertex] != vertex)
            parent[vertex] = Find(parent, parent[vertex]);
        return parent[vertex];
    }

    public void Union(int[] parent, int[] rank, int x, int y)
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
}
