namespace Scribe361
{
    /// <summary>
    /// The enum representing different token types.
    /// </summary>

    public enum TokenType
    {
        EOF,
        NewLine,
        CarriageReturn,
        Identifier,
        Number,
        String,
        Operator,
        Comment,
        OpenParenthesis,
        CloseParenthesis,
        OpenBrace,
        CloseBrace,
        If,
        Keyword,
        ReturnKeyword,
        VoidKeyword,
        FuncKeyword,
        IntKeyword,
        StrKeyword,
        BoolKeyword,
        Semicolon,
        Comma
    }

    /// <summary>
    /// The class representing a token.
    /// </summary>

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
