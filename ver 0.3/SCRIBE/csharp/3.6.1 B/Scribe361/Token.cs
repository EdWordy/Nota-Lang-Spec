using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe361
{
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
        CloseParenthesis,
        OpenBrace,
        CloseBrace,
        If,
        Keyword,
        ReturnKeyword,
        FuncKeyword,
        IntKeyword,
        StrKeyword,
        BoolKeyword,
        Semicolon,
        Comma
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



}
