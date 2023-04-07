#include "Parser.h"

#include <iostream>

void Parser::Parse()
{
    while (currentToken.GetType() != EndOfFile)
    {
        // set currentToken to the first token
        currentToken = lexer.scannedTokens.front();
        // erase the first token
        lexer.scannedTokens.erase(lexer.scannedTokens.begin());
        // print message
        std::cout << currentToken.GetValue() << " :" << (currentToken.GetType()) << "\n";
        
    }
}
