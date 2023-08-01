using System;
using System.IO;
using System.Linq;

namespace Scribe361
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // input
            //string input = "WELL WELL WELL\n\n\"Hello\"";
            string input = File.ReadAllText("input.txt");

            // lexer
            Lexer lexer = new Lexer(input);

            // parser
            Parser parser = new Parser(lexer);
            AstNode ast = parser.Parse();

            // sexer
            SemanticAnalyzer analyzer = new SemanticAnalyzer(); 

            // print
            AstPrinter.Print(ast);
            analyzer.Analyze(ast);

            // print 2
            Console.WriteLine("SemAn Defined Identifiers:");
            foreach (string s in SemanticAnalyzer.DefinedIdentifiers)
            {
                Console.WriteLine("Identifier Found: " + s);
            }

        }
    }

    // - - - - - - - - - - -
    // static helper methods 
    // - - - - - - - - - - -

    /// <summary>
    /// A simple class to handle printing the AST to console.
    /// </summary>

    public static class AstPrinter
    {
        public static void Print(AstNode node)
        {
            PrintNode(node, 0);
        }

        private static void PrintNode(AstNode node, int indent)
        {
            string indentation = new string(' ', indent * 2);
            string indentation2 = new string(' ', indent * 3);

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
                case AstVariableDeclaration variableDeclaration:
                    Console.WriteLine($"{indentation}Variable:");
                    Console.WriteLine($"{indentation2}Type: {variableDeclaration.DataType.Value}");
                    Console.WriteLine($"{indentation2}Identifier: {variableDeclaration.Identifier.Value}");
                    if ( variableDeclaration.AssignmentExpression != null )
                        Console.WriteLine($"{indentation2}Value:  {variableDeclaration.AssignmentExpression.Value}");
                    break;
                case AstFunctionDeclaration functionDeclaration:
                    Console.WriteLine($"{indentation}ReturnType: {functionDeclaration.ReturnType.Value}");
                    Console.WriteLine($"{indentation}FuncName: {functionDeclaration.FunctionName.Value}");
                    foreach (AstNode param in functionDeclaration.Parameters)
                    {
                        PrintNode(param, indent + 2);
                    }
                    Console.WriteLine($"{indentation}FuncBody:");
                    foreach (AstNode funcNode in functionDeclaration.FunctionBody.Statements)
                    {
                        PrintNode(funcNode, indent + 2);
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