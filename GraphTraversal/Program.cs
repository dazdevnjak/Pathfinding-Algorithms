public class Node<T>
{
    public Node<T> parent;
    public List<Node<T>> children;
    public T value;

    public Node(T value, List<Node<T>> children = null)
    {
        this.value = value;
        this.parent = this;
        this.children = children ?? new List<Node<T>>();
    }
}

public static class GraphTraversal
{
    /**
     * Performs a recursive Depth-First Search (DFS) to find a path from the start node to the destination node in a tree.
     * This version correctly backtracks the visited list instead of copying it in each call.
     *
     * @param current The current node being visited.
     * @param destination The destination node we want to reach.
     * @param visited The list that keeps track of the path from root to current node.
     * @return The path as a list of nodes from the start to the destination, or null if no path is found.
     */
    public static List<Node<T>> DFSRecursiveAlgorithm<T>(Node<T> current, Node<T> destination, List<Node<T>> visited)
    {
        if (current == null) return null;

        visited.Add(current);
        Console.Write(current.value + " ");

        if (current == destination)
            return new List<Node<T>>(visited);

        if (current.children != null)
        {
            foreach (var child in current.children)
            {
                var result = DFSRecursiveAlgorithm(child, destination, visited);
                if (result != null)
                    return result;
            }
        }

        visited.RemoveAt(visited.Count - 1);
        return null;
    }
    /**
    * Performs an iterative Depth-First Search (DFS) using a stack to find a path from the current node to the destination node.
    * Traverses children from left to right (stack requires reverse push order).
    *
    * @param current The starting node.
    * @param destination The destination node to find.
    * @return A list of nodes representing the path from current to destination, or null if not found.
    */
    public static List<Node<T>> DFSIterativeAlgorithm<T>(Node<T> current, Node<T> destination)
    {
        if (current == null) return null;

        var stack = new Stack<(Node<T> node, List<Node<T>> path)>();
        var visited = new HashSet<Node<T>>();

        stack.Push((current, new List<Node<T>> { current }));

        while (stack.Count > 0)
        {
            var (node, path) = stack.Pop();

            if(visited.Contains(node)) continue;
            visited.Add(node);
            
            Console.Write(node.value + " ");

            if (node == destination) return path;

            if (node.children == null) continue;

            for (int i = node.children.Count - 1; i >= 0; i--)
            {
                var child = node.children[i];
                var newPath = new List<Node<T>>(path) { child };
                stack.Push((child, newPath));
            }
        }

        return null;
    }
    /**
    * Performs an iterative Breadth-First Search (BFS) on a tree structure to find a path 
    * from the start node to the destination node.
    *
    * This method explores nodes level by level using a queue and records the visited path 
    * to prevent revisiting the same nodes. Once the destination is found, it returns the 
    * path from the start node to the destination node.
    *
    * @param start The node to start the search from.
    * @param destination The node to search for.
    * @return A list representing the path from start to destination, or null if no path is found.
    */
    public static List<Node<T>> BFSIterativeAlgorithm<T>(Node<T> start, Node<T> destination)
    {
        if (start == null) return null;

        var queue = new Queue<(Node<T> node, List<Node<T>> path)>();
        var visited = new HashSet<Node<T>>();

        queue.Enqueue((start, new List<Node<T>> { start }));

        while (queue.Count > 0)
        {
            var (node, path) = queue.Dequeue();

            if (visited.Contains(node)) continue;
            visited.Add(node);

            Console.Write(node.value + " ");

            if (node == destination) return path;

            if (node.children == null) continue;

            for (int i = 0; i < node.children.Count; i++)
            {
                var child = node.children[i];
                var newPath = new List<Node<T>>(path) { child };
                queue.Enqueue((child, newPath));
            }
        }

        return null;
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Node<string> nodeA = new Node<string>("A");
        Node<string> nodeB = new Node<string>("B");
        Node<string> nodeC = new Node<string>("C");
        Node<string> nodeD = new Node<string>("D");
        Node<string> nodeE = new Node<string>("E");
        Node<string> nodeF = new Node<string>("F");
        Node<string> nodeG = new Node<string>("G");

        nodeA.children.AddRange(new[] { nodeB, nodeC, nodeD });
        nodeC.children.AddRange(new[] { nodeE, nodeF });
        nodeF.children.Add(nodeG);

        List<Node<string>> result1 = new List<Node<string>>();
        result1 = GraphTraversal.DFSRecursiveAlgorithm(nodeA, nodeF, result1);

        Console.Write("\npath with DFSRecursiveAlgorithm: ");
        foreach (var node in result1)
        {
            Console.Write(node.value + " ");
        }

        Console.WriteLine("\n--------------------");

        List<Node<string>> result2 = new List<Node<string>>();
        result2 = GraphTraversal.DFSIterativeAlgorithm(nodeA, nodeF);

        Console.Write("\npath with DFSIterativeAlgorithm: ");
        foreach (var node2 in result2)
        {
            Console.Write(node2.value + " ");
        }

        Console.WriteLine("\n--------------------");

        List<Node<string>> result3 = new List<Node<string>>();
        result3 = GraphTraversal.BFSIterativeAlgorithm(nodeA, nodeF);

        Console.Write("\npath with BFSIterativeAlgorithm: ");
        foreach (var node3 in result3)
        {
            Console.Write(node3.value + " ");
        }

        Console.WriteLine();
    }
}

