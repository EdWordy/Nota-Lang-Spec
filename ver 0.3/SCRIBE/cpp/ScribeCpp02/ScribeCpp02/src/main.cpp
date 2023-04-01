// main.cpp
#include "lexer.h"
#include "parser.h"
#include "ast_nodes.h"
#include <iostream>
#include <string>
#include <memory>

void printAST(const ASTNode& node)
{
    node.print();
}

int main()
{
    std::string input = "A+B=C\n\nC+D=E\r\n\r\ne+f=g\r\n\r\n{if x = y}";

    Lexer lexer(input);
    Parser parser(lexer);

    Token token;
    while ((token = lexer.nextToken()).type != TokenType::EndOfFile)
    {
        std::unique_ptr<ASTNode> astNode = parser.parse(token);
        
        if (astNode)
        {
            printAST(*astNode);
        }
    }

    return 0;
}
