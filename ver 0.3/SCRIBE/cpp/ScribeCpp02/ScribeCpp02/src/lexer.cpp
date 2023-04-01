#include "lexer.h"

Lexer::Lexer(const std::string& input) : input(input), pos(0), firstBlock(true) {}

Token Lexer::nextToken()
{
    if (pos >= input.size())
    {
        return {TokenType::EndOfFile, ""};
    }

    if (firstBlock)
    {
        firstBlock = false;
        return {TokenType::Block, ""};
    }

    std::size_t blockEndPos = findBlockEnd(pos);
    if (blockEndPos != std::string::npos)
    {
        pos = blockEndPos;
        return {TokenType::Block, ""};
    }

    char current = input[pos];
    pos++;

    if (isLetter(current))
    {
        std::string tokenValue(1, current);
        return {TokenType::Letter, tokenValue};
    }

    if (isMonogram(current))
    {
        std::string tokenValue(1, current);
        return {TokenType::Monogram, tokenValue};
    }

    if (isBigramStart(current) && pos < input.size() && isBigramEnd(input[pos]))
    {
        std::string tokenValue(1, current);
        tokenValue += input[pos++];
        return {TokenType::Bigram, tokenValue};
    }

    if (isDigit(current))
    {
        std::string tokenValue(1, current);
        while (pos < input.size() && isDigit(input[pos]))
        {
            tokenValue += input[pos++];
        }
        return {TokenType::Number, tokenValue};
    }

    if (isStringStart(current))
    {
        std::string tokenValue = "";
        while (pos < input.size() && !isStringEnd(input[pos]))
        {
            tokenValue += input[pos++];
        }
        if (pos < input.size() && isStringEnd(input[pos]))
        {
            pos++; // Consume the closing string delimiter
        }
        return {TokenType::String, tokenValue};
    }

    if (isControlStart(current))
    {
        std::string tokenValue = "";
        while (pos < input.size() && !isControlEnd(input[pos]))
        {
            tokenValue += input[pos++];
        }
        if (pos < input.size() && isControlEnd(input[pos]))
        {
            pos++; // Consume the closing control delimiter
        }
        return {TokenType::Control, tokenValue};
    }

    return {TokenType::Unknown, std::string(1, current)};
}

std::size_t Lexer::findBlockEnd(std::size_t startPos)
{
    if (startPos + 1 >= input.size())
    {
        return std::string::npos;
    }

    if (isLineBreak(input[startPos]) && isLineBreak(input[startPos + 1]))
    {
        std::size_t newPos = startPos + 2;

        while (newPos + 1 < input.size() && isLineBreak(input[newPos]) && isLineBreak(input[newPos + 1]))
        {
            newPos++;
        }

        return newPos + 1;
    }

    return std::string::npos;
}

bool Lexer::isLineBreak(char ch) const
{
    return ch == '\n' || ch == '\r';
}

bool Lexer::isLetter(char ch) const
{
    return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9');
}

bool Lexer::isMonogram(char c) const
{
    return (ispunct(c) && c != '{' && c != '}' );
}


bool Lexer::isBigramStart(char ch) const
{
    return ch == '<' || ch == '>' || ch == '!' || ch == '=';
}

bool Lexer::isBigramEnd(char ch) const
{
    return ch == '=';
}

bool Lexer::isDigit(char ch) const
{
    return ch >= '0' && ch <= '9';
}

bool Lexer::isStringStart(char ch) const
{
    return ch == '"';
}

bool Lexer::isStringEnd(char ch) const
{
    return ch == '"';
}

bool Lexer::isControlStart(char ch) const
{
    return ch == '{';
}

bool Lexer::isControlEnd(char ch) const
{
    return ch == '}';
}
