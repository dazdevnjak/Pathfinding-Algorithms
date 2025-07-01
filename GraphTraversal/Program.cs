
using System.Collections.Generic;
using System.Xml.Linq;

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
        result1 = BfsDfsAlgorithms.DFSRecursiveAlgorithm(nodeA, nodeF, result1);

        Console.Write("\npath with DFSRecursiveAlgorithm: ");
        foreach (var node in result1)
        {
            Console.Write(node.value + " ");
        }

        Console.WriteLine("\n--------------------");

        List<Node<string>> result2 = new List<Node<string>>();
        result2 = BfsDfsAlgorithms.DFSIterativeAlgorithm(nodeA, nodeF);

        Console.Write("\npath with DFSIterativeAlgorithm: ");
        foreach (var node2 in result2)
        {
            Console.Write(node2.value + " ");
        }

        Console.WriteLine("\n--------------------");

        List<Node<string>> result3 = new List<Node<string>>();
        result3 = BfsDfsAlgorithms.BFSIterativeAlgorithm(nodeA, nodeF);

        Console.Write("\npath with BFSIterativeAlgorithm: ");
        foreach (var node3 in result3)
        {
            Console.Write(node3.value + " ");
        }

        Console.WriteLine("\n--------------------");

        NodeA<string> nodeAS = new NodeA<string>("S", 5);
        NodeA<string> nodeAA = new NodeA<string>("A", 7);
        NodeA<string> nodeAB = new NodeA<string>("B", 3);
        NodeA<string> nodeAD = new NodeA<string>("D", 6);
        NodeA<string> nodeAC = new NodeA<string>("C", 4);
        NodeA<string> nodeAF = new NodeA<string>("F", 6);
        NodeA<string> nodeAE = new NodeA<string>("E", 5);
        NodeA<string> nodeAG = new NodeA<string>("Goal", 0);

        nodeAS.children.AddRange(new[] { (nodeAA, 5), (nodeAB, 9), (nodeAD, 6) });
        nodeAA.children.AddRange(new[] { (nodeAB, 3) });
        nodeAB.children.AddRange(new[] { (nodeAA, 2), (nodeAC, 4) });
        nodeAC.children.AddRange(new[] { (nodeAS, 6), (nodeAG, 5), (nodeAF, 7) });
        nodeAD.children.AddRange(new[] { (nodeAS, 1), (nodeAC, 2), (nodeAE, 5) });
        nodeAF.children.AddRange(new[] { (nodeAD, 2) });

        (List<NodeA<string>>, int) result4 = AStarAlgorithm.AStar(nodeAS, nodeAG);

        Console.Write("path with A*: ");

        var (node4, cost) = result4;
        foreach (var n in node4)
        {
            Console.Write(n.value + " ");
        }
        Console.WriteLine("\ncost is: " + cost);
    }
}

