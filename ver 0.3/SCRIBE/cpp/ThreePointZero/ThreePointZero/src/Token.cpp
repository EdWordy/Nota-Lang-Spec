#include "Token.h"

const std::string& Token::GetValue() const
{
    return value;
}

TokenType Token::GetType() const
{
    return type;
}
