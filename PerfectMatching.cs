namespace DM_project;

public class PerfectMatching
{
    public static Dictionary<int, (int, int)> GetBetterMatching
        (Dictionary<int, (int, int)> pairs, Dictionary<int, Dictionary<int, int>> graph)
    {
        var betterPairs = pairs;
        var taken = new HashSet<HashSet<int>>();
        foreach (var i in pairs.Keys)
        {
            foreach (var q in pairs.Keys)
            {
                var currentPairs = new HashSet<int>
                {
                    i,
                    pairs[i].Item1,
                    q,
                    pairs[q].Item1
                };
                if (i == q || pairs[i].Item1 == q || pairs[q].Item1 == i || taken.Contains(currentPairs))
                {
                    continue;
                }

                var x = i;
                var y = betterPairs[i].Item1;
                var a = q;
                var b = betterPairs[q].Item1;
                
                if (HasEdge(graph[x],y) + HasEdge(graph[a],b) > HasEdge(graph[y],a) + HasEdge(graph[b],x))
                {
                    betterPairs[x] = (b, HasEdge(graph[x],b));
                    betterPairs[b] = (x, HasEdge(graph[x],b));
                    betterPairs[a] = (y, HasEdge(graph[q],y));
                    betterPairs[y] = (a, HasEdge(graph[q],y));
                }
                if (HasEdge(graph[x],y) + HasEdge(graph[a],b) > HasEdge(graph[y],b) + HasEdge(graph[a],x))
                {
                    betterPairs[x] = (a, HasEdge(graph[x],a));
                    betterPairs[a] = (x, HasEdge(graph[x],a));
                    betterPairs[b] = (y, HasEdge(graph[y],b));
                    betterPairs[y] = (b, HasEdge(graph[b],y));
                }
                
                //taken.Add(betterPairs[i].Item1);
                //taken.Add(i);

                int HasEdge(Dictionary<int, int> dictionary, int index)
                {
                    if (dictionary.ContainsKey(index))
                    {
                        return dictionary[index];
                    }

                    return 1000000;
                }
            }
        }

        return betterPairs;
    }
    
    public static Dictionary<int, (int, int)> GetAnyPerfectMatching(Dictionary<int, Dictionary<int, int>> graph)
    {
        var matching = new Dictionary<int, (int, int)>();
        var taken = new HashSet<int>();
        
        foreach (var i in graph.Keys)
        {
            
            foreach (var q in graph.Keys)
            {
                if (taken.Contains(i) || taken.Contains(q))
                {
                    continue;
                }
                
                if (i == q)
                {
                    continue;
                }
                
                if (graph[i].ContainsKey(q))
                {
                    matching[i] = (q, graph[i][q]);
                    matching[q] = (i, graph[i][q]);
                }
                
                taken.Add(q);
                taken.Add(i);
            }
            
            
        }

        return matching;
    }

    public static Dictionary<int, Dictionary<int, int>> GetSubgraph(List<int> vertices, Graph graph)
    {
        var listGraph = GraphFunctions.ToListForm(graph.Edges());
        var listSubgraph = new Dictionary<int, Dictionary<int, int>>();

        foreach (var vertice1 in vertices)
        {
            foreach (var vertice2 in vertices)
            {
                if (vertice1 == vertice2)
                {
                    continue;
                }

                if (!listSubgraph.ContainsKey(vertice1))
                {
                    listSubgraph[vertice1] = new Dictionary<int, int>();
                }
                
                
                if (!listGraph[vertice1].ContainsKey(vertice2))
                {
                    listSubgraph[vertice1][vertice2] = 1000000;
                }
                else
                {
                    listSubgraph[vertice1][vertice2] = listGraph[vertice1][vertice2];
                }
                
                
            }
        }
        
        /*foreach (var edge in graph.Edges())
        {
            if (vertices.Contains(edge.Destination))
            {
                if (!listSubgraph.ContainsKey(edge.Destination))
                {
                    listSubgraph[edge.Destination] = new Dictionary<int, int>();
                }
                listSubgraph[edge.Destination][edge.Source] = edge.Weight;
            }
            if (vertices.Contains(edge.Source))
            {
                if (!listSubgraph.ContainsKey(edge.Source))
                {
                    listSubgraph[edge.Source] = new Dictionary<int, int>();
                }
                listSubgraph[edge.Source][edge.Destination] = edge.Weight;
            }
        }*/

        foreach (var key in listSubgraph.Keys)
        {
            Console.WriteLine($"{key}");
        }
        return listSubgraph;
    }
    
    public static List<int> GetOddVertices(List<Edge> tree)
    {
        var listTree = GraphFunctions.ToListForm(tree);
        var oddVerticesList = new List<int>();

        for (var vertice = 0; vertice < listTree.Count; vertice++)
        {
            if (listTree[vertice].Count % 2 == 1)
            {
                oddVerticesList.Add(vertice);
            }
        }

        return oddVerticesList;
    }
}