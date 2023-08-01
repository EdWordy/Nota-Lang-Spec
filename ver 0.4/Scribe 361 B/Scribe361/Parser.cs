namespace Scribe361
{
    /// <summary>
    ///  A simple class representing the parser which takes 
    ///  an input of tokens from the Lexer 
    ///  and turns them into an AST.
    /// </summary>

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

        private AstNode ParseToken()
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
                SemanticAnalyzer.DefineIdentifier(_token.Value);
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
            else if (_token.Type == TokenType.ReturnKeyword)
            {
                node = new AstReturnKeyword(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.ReturnKeyword);
            }
            else if (_token.Type == TokenType.VoidKeyword)
            {
                node = new AstVoidKeyword(_token.Value);
                node.Value = _token.Value;
                Consume(TokenType.VoidKeyword);
            }
            else
            {
                throw new InvalidOperationException($"Unexpected token: {_token}");
            }

            return node;
        }

        private AstNode ParseForDeclaration() // e.g int i from 0 to 10
        {
            AstNode keyword = ParseToken();         
            AstNode identifier = ParseToken();      
            AstNode fromkeyword = ParseToken();     
            AstNode lowerindex = ParseToken();      
            AstNode tokeyword = ParseToken();       
            AstNode upperindex = ParseToken();      
            AstBlock statementbody = ParseForBody();

            return new AstForStatement(keyword, identifier, fromkeyword, lowerindex, tokeyword, upperindex, statementbody);
        }

        private AstBlock ParseForBody()
        {
            AstBlock block = new AstBlock();

            return block;
        }

        private AstNode ParseVariableDeclaration()
        {
            AstNode dataType = ParseToken(); // Parse data type keyword
            AstNode identifier = ParseToken(); // Parse identifier
            AstNode assignmentExpression = null;

            if (_token.Type == TokenType.Operator && _token.Value == "=")
            {
                Consume(TokenType.Operator); // Consume '='
                assignmentExpression = ParseToken();
            }

            return new AstVariableDeclaration(dataType, identifier, assignmentExpression);
        }

        private AstNode ParseFuncDeclaration()
        {
            Consume(TokenType.Keyword); // Consume 'func' keyword

            AstNode returnType = ParseToken(); // Parse return type keyword

            AstNode funcName = ParseToken(); // Parse function name identifier

            List<AstNode> parameters = new List<AstNode>();

            if (_token.Type == TokenType.OpenParenthesis)
            {
                Consume(TokenType.OpenParenthesis);

                while (_token.Type != TokenType.CloseParenthesis && _token.Type != TokenType.EOF)
                {
                    
                    AstNode paramType = ParseToken(); // Parse parameter type keyword
                    AstNode paramName = ParseToken(); // Parse parameter name identifier

                    parameters.Add(new AstStatement(new List<AstNode> { paramType, paramName }));

                    if (_token.Type == TokenType.Comma)
                    {
                        Consume(TokenType.Comma);
                    }
                }
                Consume(TokenType.CloseParenthesis);
            }

            if (_token.Type == TokenType.NewLine)
                Consume(TokenType.NewLine);
            if (_token.Type == TokenType.CarriageReturn)
                Consume(TokenType.CarriageReturn);

            AstBlock block = ParseFunctionBody();

            return new AstFunctionDeclaration(returnType, funcName, parameters, block);
        }

        private AstBlock ParseFunctionBody()
        {
            AstBlock block = new AstBlock();

            while (_token.Type != TokenType.EOF)
            {
                // Ignore single line comments
                if (_token.Type == TokenType.Comment)
                {
                    Consume(TokenType.Comment);
                    continue;
                }

                if (_token.Type == TokenType.CarriageReturn)
                {
                    Consume(TokenType.CarriageReturn);
                    Consume(TokenType.NewLine);
                }

                if (_token.Type == TokenType.NewLine)
                {
                    Consume(TokenType.NewLine);
                }
                else
                {
                    block.Add(ParseStatement());
                }

                if (_token.Type == TokenType.ReturnKeyword)
                {
                    block.Add(ParseStatement());
                    break;
                }
            }

            return block;
        }

        private AstNode ParseStatement()
        {
            List<AstNode> nodes = new List<AstNode>();

            // Ignore single line comments
            if (_token.Type == TokenType.Comment)
            {
                Consume(TokenType.Comment);
            }

            if (_token.Type == TokenType.Keyword && _token.Value == "int" || _token.Value == "str" || _token.Value == "bool")
            {
                return ParseVariableDeclaration();
            }

            if (_token.Type == TokenType.Keyword && _token.Value == "for")
            {
                return ParseForDeclaration();
            }

            if (_token.Type == TokenType.Keyword && _token.Value == "func")
            {
                return ParseFuncDeclaration();
            }

            if (_token.Type == TokenType.If && _token.Value.ToLower() == "if")
            {
                AstKeyword ifKeyword = new AstKeyword(_token.Value);
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
                while (_token.Type != TokenType.NewLine && _token.Type != TokenType.EOF && _token.Type != TokenType.CarriageReturn)
                {
                    nodes.Add(ParseToken());
                }
            }

            if (nodes.Count > 0)
            {
                return new AstStatement(nodes);
            }
            else
            {
                return null;
            }
        }

        public AstNode Parse()
        {
            List<AstBlock> blocks = new List<AstBlock>();

            AstBlock block = new AstBlock();

            while (_token.Type != TokenType.EOF)
            {
                AstNode node = ParseStatement();

                // Ignore single line comments
                if (_token.Type == TokenType.Comment)
                {
                    Consume(TokenType.Comment);
                    continue;
                }

                // add the node to the block
                if (node != null && node.Value != string.Empty) 
                {
                    block.Add(node);
                }

                // break if EOF
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

                if (newlinesCount == 2)
                {
                    if (block != null)
                    {
                        blocks.Add(block);
                    }    
                    block = new AstBlock(); // Start a new block after two consecutive newlines
                }
            }

            if (block != null && block.Statements != null)
            {
                blocks.Add(block); // Add the last block
            }

            return new AstNodeList(blocks);
        }
    }
}
    
