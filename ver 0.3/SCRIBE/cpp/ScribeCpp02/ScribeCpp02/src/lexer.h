#pragma once

// lexer.h
#pragma once
#include <string>
#include <cctype>

enum class TokenType
{
    Unknown,
    Monogram,
    Bigram,
    Number,
    Letter,
    String,
    Control,
    Block,
    EndOfFile
};

struct Token
{
    TokenType type;
    std::string value;
};

class Lexer
{
public:
    Lexer(const std::string& input);
    Token nextToken();

private:
    const std::string input;
    std::size_t pos;
    bool firstBlock;

    bool isLineBreak(char c) const;
    bool isLetter(char c) const;
    bool isMonogram(char c) const;
    bool isBigramStart(char ch) const;
    bool isBigramEnd(char ch) const;
    bool isDigit(char ch) const;
    bool isStringStart(char ch) const;
    bool isStringEnd(char ch) const;
    bool isControlStart(char ch) const;
    bool isControlEnd(char ch) const;

    std::size_t findBlockEnd(std::size_t startPos);
};
