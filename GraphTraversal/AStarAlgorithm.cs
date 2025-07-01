
/**
 * Represents a node in a graph used for the A* search algorithm.
 *
 * @param <T> The type of the value stored in the node.
 */
public class NodeA<T>
{
    public NodeA<T> parent;
    public List<(NodeA<T> child, int cost)> children; // child node and path cost (G)
    public T value; // e.g., "A", "B", "C"
    public int heuristics; // heuristic value (H)

    public NodeA(T value, int heuristics)
    {
        this.value = value;
        this.heuristics = heuristics;
        this.parent = null;
        this.children = new List<(NodeA<T>, int)>();
    }
}

/**
 * Represents a record used in the A* algorithm to track node cost and estimated total cost.
 *
 * @param <T> The type of the value in the node.
 */
class NodeRecord<T>
{
    public NodeA<T> node;
    public int g; // total cost from the start node
    public int f => g + node.heuristics; // evaluation function f(n) = g(n) + h(n)
    
    public NodeRecord(NodeA<T> node, int g)
    {
        this.node = node;
        this.g = g;
    }
}

public class AStarAlgorithm
{
    /**
     * Executes the A* pathfinding algorithm.
     *
     * @param start The start node.
     * @param destination The destination node.
     * @param <T> The type of the node's value.
     * @return A Pair containing the list representing the path from start to destination, and the total cost.
     */
    public static (List<NodeA<T>>, int) AStar<T>(NodeA<T> start, NodeA<T> destination)
    {
        var open = new List<NodeRecord<T>>();
        var closed = new HashSet<NodeA<T>>();

        open.Add(new NodeRecord<T>(start, 0));
        start.parent = null;

        while (open.Count > 0)
        {
            var current = open
                .OrderBy(x => x.f)
                .ThenBy(x => x.g)
                .First();

            open.Remove(current);

            if (current.node.Equals(destination))
            {
                var path = new List<NodeA<T>>();
                var node = current.node;
                while (node != null)
                {
                    path.Add(node);
                    node = node.parent;
                }
                path.Reverse();
                return (path, current.f);
            }

            closed.Add(current.node);

            foreach (var (child, cost) in current.node.children)
            {
                if (closed.Contains(child)) continue;

                int tentativeG = current.g + cost;

                var existing = open.FirstOrDefault(nr => nr.node.Equals(child));
                if (existing == null)
                {
                    child.parent = current.node;
                    open.Add(new NodeRecord<T>(child, tentativeG));
                }
                else if (tentativeG < existing.g)
                {
                    existing.g = tentativeG;
                    child.parent = current.node;
                }
            }
        }
        return (new List<NodeA<T>>(), -1);
    }
}

