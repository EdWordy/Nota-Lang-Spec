#include <iostream>
#include "Parser.h"
#include "AstNode.h"


AstStatement Parser::parseStatements()
{
    return parseStatement();
}

AstBlock Parser::parseBlock()
{
    // create a new block
    AstBlock block;
    // int var
    int newLineCount = 0;

    // loop
    while (newLineCount != 2)
    {
        // parse statement
        block.add(parseStatements());
        
        // new line check
        if (currentToken.GetType() == EndOfLine)
        {
            consume(EndOfLine);
            newLineCount++;
        }

        if (newLineCount == 2)
        {
            break;
        }
    }
    return block;
}

AstNode Parser::parseNode()
{
    AstNode node("");

    if (currentToken.GetType() == TokenType::Unknown)
    {
        std::cout << "ERROR: Unknown token found\n";
    }
    else
    {
        return AstNode(currentToken.GetValue());
    }
    // catch
    node.value = "?";
    return node;
}

AstStatement Parser::parseStatement()
{
    AstStatement statement;

    while (currentToken.GetType() != EndOfLine || currentToken.GetType() != EndOfFile)
    {
        // parse the node
        statement.add(parseNode());

        // next
        nextToken();
        
        if (currentToken.GetType() != EndOfLine || currentToken.GetType() != EndOfFile)
        {
            break;
        }
        
    }
    return statement;
}

AstRoot Parser::parse()
{
    AstRoot root;
    
    while (currentToken.GetType() != EndOfFile)
    {
        nextToken();
        // look for blocks
        root.add(parseBlock());

        if (currentToken.GetType() == EndOfFile)
        {
            break;
        }
        
    }
    return root;
}

void Parser::consume(TokenType type)
{
    if (currentToken.GetType() == type)
    {
        nextToken(); 
    }
    else
    {
        std::cout << "Unexpected token " << type << "\n"; 
     }
    
}

void Parser::nextToken()
{
    // set currentToken to the first token
    currentToken = lexer.scannedTokens.front();
    // pop off the first token
    lexer.scannedTokens.erase(lexer.scannedTokens.begin());
}
