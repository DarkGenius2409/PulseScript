﻿using psc.CodeAnalysis;

namespace psc
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showTree = false;
            while (true)
            {
                Console.Write("psc -> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;
                if (line == "#toggleTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing Parser Trees." : "Not Showing Parser Trees");
                    continue;
                }
                else if (line == "#clr")
                {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                var color = Console.ForegroundColor;
                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = color;
                }

                if (!syntaxTree.Diagnostics.Any())
                {
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var diagnostic in syntaxTree.Diagnostics)
                    {
                        Console.WriteLine(diagnostic);
                    }
                    Console.ForegroundColor = color;
                }
            }
        }
        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            // ├──
            //│

            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Type);
            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write("  ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            // indent += "    ";
            indent += isLast ? "    " : "│    ";

            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren()) { PrettyPrint(child, indent, child == lastChild); }
        }
    }
}