using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe361
{
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
                node.Value = _token.Value;
                Consume(TokenType.Number);
            }
            else if (_token.Type == TokenType.String)
            {
                node = new AstString(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.String);
            }
            else if (_token.Type == TokenType.Identifier)
            {
                node = new AstIdentifier(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.Identifier);
            }
            else if (_token.Type == TokenType.Operator)
            {
                node = new AstOperator(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.Operator);
            }
            else if (_token.Type == TokenType.Keyword)
            {
                node = new AstKeyword(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.Keyword);
            }
            else if (_token.Type == TokenType.IntKeyword)
            {
                node = new AstKeyword(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.IntKeyword);
            }
            else if (_token.Type == TokenType.ReturnKeyword)
            {
                node = new AstReturnKeyword(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.ReturnKeyword);
            }
            else
            {
                throw new InvalidOperationException($"Unexpected token: {_token}");
            }

            return node;
        }

        private AstBlock ParseBlock()
        {
            AstBlock block = new AstBlock();

            while (_token.Type != TokenType.NewLine && _token.Type != TokenType.EOF)
            {
                block.Add(ParseStatement());
            }

            return block;
        }

        private AstNode ParseVariableDeclaration()
        {
            AstNode dataType = ParseExpression(); // Parse data type keyword
            AstNode identifier = ParseExpression(); // Parse identifier
            AstNode assignmentExpression = null;

            if (_token.Type == TokenType.Operator && _token.Value == "=")
            {
                Consume(TokenType.Operator); // Consume '='
                assignmentExpression = ParseExpression();
            }

            return new AstVariableDeclaration(dataType, identifier, assignmentExpression);
        }


        private AstNode ParseFuncDeclaration()
        {
            Consume(TokenType.Keyword); // Consume 'func' keyword

            AstNode returnType = ParseExpression(); // Parse return type keyword

            AstNode funcName = ParseExpression(); // Parse function name identifier

            List<AstNode> parameters = new List<AstNode>();

            if (_token.Type == TokenType.OpenParenthesis)
            {
                Consume(TokenType.OpenParenthesis);

                while (_token.Type != TokenType.CloseParenthesis && _token.Type != TokenType.EOF)
                {
                    AstNode paramType = ParseExpression(); // Parse parameter type keyword
                    AstNode paramName = ParseExpression(); // Parse parameter name identifier

                    parameters.Add(new AstStatement(new List<AstNode> { paramType, paramName }));

                    if (_token.Type == TokenType.Comma)
                    {
                        Consume(TokenType.Comma);
                    }
                }

                Consume(TokenType.CloseParenthesis);
            }

            return new AstStatement(new List<AstNode> { new AstKeyword("func"), returnType, funcName, new AstParametersList(parameters) });
        }

        private AstNode ParseStatement()
        {
            List<AstNode> nodes = new List<AstNode>();

            if (_token.Type == TokenType.Keyword && _token.Value == "func")
            {
                return ParseFuncDeclaration();
            }

            if (_token.Type == TokenType.Keyword && _token.Value == "int" || _token.Value == "str" || _token.Value == "bool")
            {
                return ParseVariableDeclaration();
            }

            if (_token.Type == TokenType.If && _token.Value.ToLower() == "if")
            {
                AstIdentifier ifKeyword = new AstIdentifier(_token.Value);
                Consume(TokenType.If);

                Consume(TokenType.OpenParenthesis);

                AstIdentifier leftIdentifier = new AstIdentifier(_token.Value);
                Consume(TokenType.Identifier);

                AstOperator operatorNode = new AstOperator(_token.Value);
                Consume(TokenType.Operator);

                AstIdentifier rightIdentifier = new AstIdentifier(_token.Value);
                Consume(TokenType.Identifier);

                Consume(TokenType.CloseParenthesis);

                AstIfStatement ifStatement = new AstIfStatement(ifKeyword, leftIdentifier, operatorNode, rightIdentifier);
                nodes.Add(ifStatement);
            }
            else
            {
                while (_token.Type != TokenType.NewLine && _token.Type != TokenType.EOF && _token.Type != TokenType.CloseBrace)
                {
                    nodes.Add(ParseExpression());
                }
            }

            return new AstStatement(nodes);
        }

        public AstNode Parse()
        {
            List<AstBlock> blocks = new List<AstBlock>();
            AstBlock block = new AstBlock();

            while (_token.Type != TokenType.EOF)
            {
                block.Add(ParseStatement());

                if (_token.Type == TokenType.EOF)
                {
                    break;
                }

                int newlinesCount = 0;

                while (_token.Type == TokenType.NewLine || _token.Type == TokenType.CarriageReturn)
                {
                    if (_token.Type == TokenType.NewLine)
                        Consume(TokenType.NewLine);
                    if (_token.Type == TokenType.CarriageReturn)
                        Consume(TokenType.CarriageReturn);

                    newlinesCount++;
                }

                if (newlinesCount >= 2)
                {
                    blocks.Add(block);
                    block = new AstBlock(); // Start a new block after two consecutive newlines
                }
            }

            blocks.Add(block); // Add the last block

            return new AstNodeList(blocks);
        }
    }
}
