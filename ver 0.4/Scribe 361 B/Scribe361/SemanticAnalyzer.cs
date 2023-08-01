using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe361
{
    public class SemanticAnalyzer
    {

        private SymbolTable _symbolTable = new SymbolTable();

        public static HashSet<string> DefinedIdentifiers { get; set; } = new HashSet<string>();

        public static HashSet<string> DefinedOperators { get; set; } = new HashSet<string>() 
        { 
            "!", 
            "+", 
            "-", 
            "=", 
            "*", 
            "/", 
            "==" 
        };

        public void Analyze(AstNode node)
        {
            AnalyzeNode(node);
        }

        private void AnalyzeNode(AstNode node)
        {
            switch (node)
            {
                case AstIdentifier identifier:
                    if (!DefinedIdentifiers.Contains(identifier.Value))
                    {
                        throw new InvalidOperationException($"Undefined identifier: {identifier.Value}");
                    }
                    break;
                case AstOperator op:
                    if (!DefinedOperators.Contains(op.Value))
                    {
                        throw new InvalidOperationException($"Undefined identifier: {op.Value}");
                    }
                    break;
                case AstIfStatement ifStatement:
                    AnalyzeNode(ifStatement.IfKeyword);
                    AnalyzeNode(ifStatement.LeftIdentifier);
                    AnalyzeNode(ifStatement.Operator);
                    AnalyzeNode(ifStatement.RightIdentifier);
                    break;
                case AstStatement statement:
                    foreach (AstNode nodeElement in statement.Nodes)
                    {
                        AnalyzeNode(nodeElement);
                    }
                    break;
                case AstBlock block:
                    foreach (AstNode statement in block.Statements)
                    {
                        AnalyzeNode(statement);
                    }
                    break;
                case AstNodeList nodeList:
                    foreach (AstBlock blockNode in nodeList.Blocks)
                    {
                        AnalyzeNode(blockNode);
                    }
                    break;
            }
        }

        public static void DefineIdentifier(string identifier)
        {
            DefinedIdentifiers.Add(identifier);
        }
    }
}
