namespace Scribe361
{
    /// <summary>
    /// The class which represents the lexical analyzer 
    /// which tokenizes input and produces a stream of tokens.
    /// </summary>

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

        private char Peek()
        {
            int peekPosition = _position + 1;
            if (peekPosition > _input.Length - 1)
            {
                return '\0';
            }
            else
            {
                return _input[peekPosition];
            }
        }

        private void SkipWhitespace()
        {
            while (_currentChar != '\0' && char.IsWhiteSpace(_currentChar) && _currentChar != '\n')
            {
                Advance();
            }
        }

        private void SkipComment()
        {
            if (_currentChar == '/' && Peek() == '/')
            {
                // Single-line comment
                while (_currentChar != '\0' && _currentChar != '\n' && _currentChar != '\r')
                {
                    Advance();
                }
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

                if (_currentChar == '/')
                {
                    char nextChar = Peek();
                    if (nextChar == '/' || nextChar == '*')
                    {
                        SkipComment();
                        continue;
                    }
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
                    string identifier = GetIdentifier().Value;

                    if (identifier == "if")
                    {
                        return new Token(TokenType.If, identifier);
                    }
                    if (identifier == "int")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }
                    if (identifier == "str")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }
                    if (identifier == "bool")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }
                    if (identifier == "return")
                    {
                        return new Token(TokenType.ReturnKeyword, identifier);
                    }
                    if (identifier == "void")
                    {
                        return new Token(TokenType.VoidKeyword, identifier);
                    }
                    if (identifier == "print")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }
                    if (identifier == "func")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }
                    if (identifier == "true")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }
                    if (identifier == "false")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }

                    return new Token(TokenType.Identifier, identifier);
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

                if (_currentChar == ';')
                {
                    Advance();
                    return new Token(TokenType.Semicolon, ";");
                }

                if (_currentChar == ',')
                {
                    Advance();
                    return new Token(TokenType.Comma, ",");
                }
            }

            return new Token(TokenType.EOF, null);
        }
    }
}
