using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe361
{
    public abstract class AstNode { }

    public class AstNumber : AstNode
    {
        public string Value { get; }

        public AstNumber(string value)
        {
            Value = value;
        }
    }

    public class AstString : AstNode
    {
        public string Value { get; }

        public AstString(string value)
        {
            Value = value;
        }
    }

    public class AstIdentifier : AstNode
    {
        public string Value { get; }

        public AstIdentifier(string value)
        {
            Value = value;
        }
    }

    public class AstKeyword : AstNode
    {
        public string Value { get; }

        public AstKeyword(string value)
        {
            Value = value;
        }
    }

    public class AstReturnKeyword : AstNode
    {
        public string Value { get; }

        public AstReturnKeyword(string value)
        {
            Value = value;
        }
    }

    public class AstOperator : AstNode
    {
        public string Value { get; }

        public AstOperator(string value)
        {
            Value = value;
        }
    }

    public class AstStatement : AstNode
    {
        public List<AstNode> Nodes { get; }

        public AstStatement(List<AstNode> nodes)
        {
            Nodes = nodes;
        }
    }

    public class AstBlock : AstNode
    {
        public List<AstNode> Statements { get; }

        public AstBlock()
        {
            Statements = new List<AstNode>();
        }

        public void Add(AstNode statement)
        {
            Statements.Add(statement);
        }
    }

    public class AstNodeList : AstNode
    {
        public List<AstBlock> Blocks { get; }

        public AstNodeList(List<AstBlock> blocks)
        {
            Blocks = blocks;
        }
    }

    public class AstParametersList : AstNode
    {
        public List<AstNode> Parameters { get; }

        public AstParametersList(List<AstNode> parameters)
        {
            Parameters = parameters;
        }
    }

    public class AstIfStatement : AstNode
    {
        public AstIdentifier IfKeyword { get; }
        public AstIdentifier LeftIdentifier { get; }
        public AstOperator Operator { get; }
        public AstIdentifier RightIdentifier { get; }

        public AstIfStatement(AstIdentifier ifKeyword, AstIdentifier leftIdentifier, AstOperator operatorNode, AstIdentifier rightIdentifier)
        {
            IfKeyword = ifKeyword;
            LeftIdentifier = leftIdentifier;
            Operator = operatorNode;
            RightIdentifier = rightIdentifier;
        }
    }
}
