#include "AstPrinter.h"

#include <iostream>

void AstPrinter::printAst(AstRoot root)
{
    std::string indent = "  ";
    std::cout << "Root:\n";
    for (AstBlock block : root.blocks)
    {
        std::cout << indent << "Block:\n";
        for (AstStatement statement : block.statementList)
        {
            std::cout << indent << "  Statement:\n";
            for (AstNode node : statement.nodeList)
            {
                std::cout << indent << "  Node:" << node.value << "\n";
            }
            
        }
    }
}
