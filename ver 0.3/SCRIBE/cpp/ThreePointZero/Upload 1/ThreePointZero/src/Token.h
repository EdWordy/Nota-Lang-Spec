#pragma once
#include <string>

enum TokenType
{
    Unknown,
    Identifier,
    Keyword,
    Operator,
    Literal,
    Separator,
    Comment,
    EndOfLine,
    EndOfFile
};

class Token
{
    TokenType type;
    std::string value;
    
public:
    Token(TokenType type, const std::string& value) : type(type), value(value) {}
    
    const std::string& GetValue() const;
    TokenType GetType() const;
};
