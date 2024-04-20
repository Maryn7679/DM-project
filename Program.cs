namespace DM_project;

internal static class Program
{
    public static void Main()
    {
        var randomGraph = Graph.GraphGenerator(5, 50);
        for (var a = 0; a < randomGraph.GetLength(0); a++)
        {
            var b = "";
            for (var c = 0; c < randomGraph.GetLength(1); c++)
            {
                b += randomGraph[a, c].ToString() + ", ";
            }
            Console.WriteLine(b);
        }
    }
}