namespace Scribe361
{
    /// <summary>
    /// The base class representing a node in the AST.
    /// </summary>

    public abstract class AstNode
    {
        public string Value { get; set; }
    }

    // basic data types

    public class AstNumber : AstNode
    {
        public AstNumber(string value)
        {
            Value = value;
        }
    }

    public class AstString : AstNode
    {
        public AstString(string value)
        {
            Value = value;
        }
    }

    // symbols

    public class AstIdentifier : AstNode
    {

        public AstIdentifier(string value)
        {
            Value = value;
        }
    }

    public class AstOperator : AstNode
    { 

        public AstOperator(string value)
        {
            Value = value;
        }
    }

    // Keywords

    public class AstKeyword : AstNode
    {
        public AstKeyword(string value)
        {
            Value = value;
        }
    }

    public class AstReturnKeyword : AstNode
    {
        public AstReturnKeyword(string value)
        {
            Value = value;
        }
    }

    public class AstVoidKeyword : AstNode
    {
        public AstVoidKeyword(string value)
        {
            Value = value;
        }
    }

    public class AstIntKeyword : AstNode
    {
        public AstIntKeyword(string value)
        {
            Value = value;
        }
    }

    public class AstStrKeyword : AstNode
    {
        public AstStrKeyword(string value)
        {
            Value = value;
        }
    }

    // units

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

    // control structs

    public class AstIfStatement : AstNode
    {
        public AstKeyword IfKeyword { get; }
        public AstIdentifier LeftIdentifier { get; }
        public AstOperator Operator { get; }
        public AstIdentifier RightIdentifier { get; }

        public AstIfStatement(AstKeyword ifKeyword, AstIdentifier leftIdentifier, AstOperator operatorNode, AstIdentifier rightIdentifier)
        {
            IfKeyword = ifKeyword;
            LeftIdentifier = leftIdentifier;
            Operator = operatorNode;
            RightIdentifier = rightIdentifier;
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

    // data

    public class AstVariableDeclaration : AstNode
    {
        public AstNode DataType { get; }
        public AstNode Identifier { get; }
        public AstNode AssignmentExpression { get; }

        public AstVariableDeclaration(AstNode dataType, AstNode identifier, AstNode assignmentExpression)
        {
            DataType = dataType;
            Identifier = identifier;
            AssignmentExpression = assignmentExpression;
        }
    }

    public class AstFunctionDeclaration : AstNode
    {
        public AstNode ReturnType { get; }
        public AstNode FunctionName { get; }
        public List<AstNode> Parameters { get; }
        public AstBlock FunctionBody { get; }

        public AstFunctionDeclaration(AstNode returnType, AstNode functionName, List<AstNode> parameters, AstBlock functionBody)
        {
            ReturnType = returnType;
            FunctionName = functionName;
            Parameters = parameters;
            FunctionBody = functionBody;
        }
    }
}
