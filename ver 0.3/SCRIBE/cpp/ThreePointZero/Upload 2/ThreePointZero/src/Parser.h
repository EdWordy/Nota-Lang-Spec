#pragma once
#include "AstNode.h"
#include "Lexer.h"
#include "Token.h"

class Parser
{
public:
    Lexer lexer;
    Token currentToken;
    
    Parser(Lexer lexer) : lexer(lexer), currentToken(lexer.scannedTokens.front()) {}

    AstStatement parseStatements();
    AstBlock parseBlock();
    AstNode parseNode();
    AstStatement parseStatement();
    AstRoot parse();
    void consume(TokenType type);
    void nextToken();
    
};
