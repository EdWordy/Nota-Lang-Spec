#pragma once
#include "Lexer.h"
#include "Token.h"

class Parser
{
public:
    Lexer lexer;
    Token currentToken;


    Parser(Lexer lexer) : lexer(lexer), currentToken(lexer.scannedTokens.front()) {}

    void Parse();
    
    
};
