public class Program
{
    public static void Main(string[] args)
    {
        //string input = "WELL WELL WELL\n\n\"Hello\"";
        string input = File.ReadAllText("input.txt");
        Lexer lexer = new Lexer(input);
        Parser parser = new Parser(lexer);
        AstNode ast = parser.Parse();
        AstPrinter.Print(ast);
    }
}

public enum TokenType
{
    EOF,
    NewLine,
    CarriageReturn,
    Identifier,
    Number,
    String,
    Operator,
    OpenParenthesis,
    CloseParenthesis
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"Token({Type}, {Value})";
    }
}

public class Lexer
{
    private string _input;
    private int _position;
    private char _currentChar;

    public Lexer(string input)
    {
        _input = input;
        _position = 0;
        _currentChar = input[_position];
    }

    private void Advance()
    {
        _position++;
        if (_position > _input.Length - 1)
        {
            _currentChar = '\0';
        }
        else
        {
            _currentChar = _input[_position];
        }
    }

    private void SkipWhitespace()
    {
        while (_currentChar != '\0' && char.IsWhiteSpace(_currentChar) && _currentChar != '\n')
        {
            Advance();
        }
    }

    private Token GetNumber()
    {
        string result = "";
        while (_currentChar != '\0' && char.IsDigit(_currentChar))
        {
            result += _currentChar;
            Advance();
        }
        return new Token(TokenType.Number, result);
    }

    private Token GetString()
    {
        string result = "";
        Advance(); // Consume opening quote
        while (_currentChar != '\0' && _currentChar != '\"')
        {
            result += _currentChar;
            Advance();
        }
        Advance(); // Consume closing quote
        return new Token(TokenType.String, result);
    }

    private Token GetIdentifier()
    {
        string result = "";
        while (_currentChar != '\0' && (char.IsLetterOrDigit(_currentChar) || _currentChar == '_'))
        {
            result += _currentChar;
            Advance();
        }
        return new Token(TokenType.Identifier, result);
    }

    private Token GetOperator()
    {
        string result = "";
        while (_currentChar != '\0' && "+-*/%^&|<>!~=.".Contains(_currentChar))
        {
            result += _currentChar;
            Advance();
        }
        return new Token(TokenType.Operator, result);
    }

    public Token GetNextToken()
    {
        while (_currentChar != '\0')
        {
            if (char.IsWhiteSpace(_currentChar))
            {
                if (_currentChar == '\n')
                {
                    Advance();
                    return new Token(TokenType.NewLine, "\n");
                }
                SkipWhitespace();
                continue;
            }

            if (char.IsDigit(_currentChar))
            {
                return GetNumber();
            }

            if (_currentChar == '\"')
            {
                return GetString();
            }

            if (_currentChar == '\r')
            {
                return new Token(TokenType.CarriageReturn, "\r");
            }

            if (char.IsLetter(_currentChar) || _currentChar == '_')
            {
                return GetIdentifier();
            }

            if ("+-*/%^&|<>!~=.".Contains(_currentChar))
            {
                return GetOperator();
            }

            if (_currentChar == '(')
            {
                Advance();
                return new Token(TokenType.OpenParenthesis, "(");
            }

            if (_currentChar == ')')
            {
                Advance();
                return new Token(TokenType.CloseParenthesis, ")");
            }
        }

        return new Token(TokenType.EOF, null);
    }
}

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

public class AstOperator : AstNode
{
    public string Value { get; }

    public AstOperator(string value)
    {
        Value = value;
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

public class Parser
{
    private Token _token;
    private Lexer _lexer;

    public Parser(Lexer lexer)
    {
        _lexer = lexer;
        _token = _lexer.GetNextToken();
    }

    private void Consume(TokenType type)
    {
        if (_token.Type == type)
        {
            _token = _lexer.GetNextToken();
        }
        else
        {
            throw new InvalidOperationException($"Unexpected token: {_token}");
        }
    }

    private AstNode ParseExpression()
    {
        AstNode node;

        if (_token.Type == TokenType.Number)
        {
            node = new AstNumber(_token.Value);
            Consume(TokenType.Number);
        }
        else if (_token.Type == TokenType.String)
        {
            node = new AstString(_token.Value);
            Consume(TokenType.String);
        }
        else if (_token.Type == TokenType.Identifier)
        {
            node = new AstIdentifier(_token.Value);
            Consume(TokenType.Identifier);
        }
        else if (_token.Type == TokenType.Operator)
        {
            node = new AstOperator(_token.Value);
            Consume(TokenType.Operator);
        }
        else
        {
            throw new InvalidOperationException($"Unexpected token: {_token}");
        }

        return node;
    }

    private AstNode ParseStatement()
    {
        AstNode statement = ParseExpression();

        // Consume newlines after a statement is parsed
        if (_token.Type == TokenType.NewLine)
        {
            Consume(TokenType.NewLine);
        }

        return statement;
    }

    public AstNode Parse()
    {
        List<AstBlock> blocks = new List<AstBlock>();
        AstBlock currentBlock = new AstBlock();

        while (_token.Type != TokenType.EOF)
        {

            if (_token.Type == TokenType.CarriageReturn)
            {
                blocks.Add(currentBlock);
                currentBlock = new AstBlock();
                Consume(TokenType.CarriageReturn);
                if (_token.Type == TokenType.NewLine)
                {
                    Consume(TokenType.NewLine);
                }
            }
            else if (_token.Type == TokenType.NewLine)
            {
                // If a newline followed by an optional carriage return and another newline is found, create a new block
                blocks.Add(currentBlock);
                currentBlock = new AstBlock();
                Consume(TokenType.NewLine);
                if (_token.Type == TokenType.NewLine)
                {
                    Consume(TokenType.NewLine);
                }
            }
            else
            {
                currentBlock.Add(ParseStatement());
            }
        }

        // Add the last block to the list
        if (currentBlock.Statements.Count > 0)
        {
            blocks.Add(currentBlock);
        }

        return new AstNodeList(blocks);
    }
}

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
        }
    }
}