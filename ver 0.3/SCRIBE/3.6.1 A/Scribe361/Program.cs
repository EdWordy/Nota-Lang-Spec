﻿namespace Scribe361
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            // input
            //string input = "WELL WELL WELL\n\n\"Hello\"";
            string input = File.ReadAllText("input.txt");

            // setup
            Lexer lexer = new Lexer(input);
            Parser parser = new Parser(lexer);
            AstNode ast = parser.Parse();
            AstPrinter.Print(ast);
        }
    }

    // - - - - -
    
    // static helper methods 
    
    public static class AstPrinter
    {
        public static void Print(AstNode node)
        {
            PrintNode(node, 0);
        }

        private static void PrintNode(AstNode node, int indent)
        {
            string indentation = new string(' ', indent * 2);

            switch (node)
            {
                case AstNumber number:
                    Console.WriteLine($"{indentation}Number: {number.Value}");
                    break;
                case AstString str:
                    Console.WriteLine($"{indentation}String: {str.Value}");
                    break;
                case AstIdentifier identifier:
                    Console.WriteLine($"{indentation}Identifier: {identifier.Value}");
                    break;
                case AstOperator operatorNode:
                    Console.WriteLine($"{indentation}Operator: {operatorNode.Value}");
                    break;
                case AstKeyword keywordNode:
                    Console.WriteLine($"{indentation}Keyword: {keywordNode.Value}");
                    break;
                case AstStatement statement:
                    Console.WriteLine($"{indentation}Statement:");
                    foreach (AstNode n in statement.Nodes)
                    {
                        PrintNode(n, indent + 1);
                    }
                    break;
                case AstBlock block:
                    Console.Write($"{indentation}Block: \n");
                    foreach (AstNode statement in block.Statements)
                    {
                        PrintNode(statement, indent + 2);
                    }
                    Console.WriteLine();
                    break;
                case AstNodeList nodeList:
                    Console.WriteLine($"{indentation}Node List:");
                    foreach (AstBlock blockNode in nodeList.Blocks)
                    {
                        PrintNode(blockNode, indent + 1);
                    }
                    break;
                case AstIfStatement ifStatement:
                    Console.WriteLine($"{indentation}IfStatement:");
                    PrintNode(ifStatement.IfKeyword, indent + 1);
                    PrintNode(ifStatement.LeftIdentifier, indent + 1);
                    PrintNode(ifStatement.Operator, indent + 1);
                    PrintNode(ifStatement.RightIdentifier, indent + 1);
                    break;
                case AstReturnKeyword returnKeyword:
                    Console.WriteLine($"{indentation}ReturnKeyword: {returnKeyword.Value}");
                    break;
                case AstParametersList parametersList:
                    Console.WriteLine($"{indentation}ParametersList:");
                    foreach (AstNode param in parametersList.Parameters)
                    {
                        PrintNode(param, indent + 1);
                    }
                    break;
            }
        }
    }
}