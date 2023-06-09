﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe361
{
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
                    string identifier = GetIdentifier().Value;

                    if (identifier == "if")
                    {
                        return new Token(TokenType.If, identifier);
                    }
                    if (identifier == "int")
                    {
                        return new Token(TokenType.IntKeyword, identifier);
                    }
                    if (identifier == "return")
                    {
                        return new Token(TokenType.ReturnKeyword, identifier);
                    }
                    if (identifier == "print")
                    {
                        return new Token(TokenType.Keyword, identifier);
                    }
                    if (identifier == "func")
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

                if (_currentChar == '{')
                {
                    Advance();
                    return new Token(TokenType.OpenBrace, "{");
                }

                if (_currentChar == '}')
                {
                    Advance();
                    return new Token(TokenType.CloseBrace, "}");
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
