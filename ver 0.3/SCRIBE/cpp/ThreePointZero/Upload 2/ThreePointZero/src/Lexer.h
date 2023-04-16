#pragma once
#include <string>
#include <vector>
#include "Token.h"

class Lexer
{
    std::string input;
    char currentChar;

public:
    size_t pos;
    size_t lineNum;

    std::vector<Token> scannedTokens;
    
    Lexer(const std::string& input) : input(input), currentChar(), pos(0), lineNum(1) {}
    
    void advance();
    char peek(size_t currentPos);
    
    Token nextToken();
    void scan();

    Token handleLetters();
    Token handleDigits();
    Token handleSymbols();
};