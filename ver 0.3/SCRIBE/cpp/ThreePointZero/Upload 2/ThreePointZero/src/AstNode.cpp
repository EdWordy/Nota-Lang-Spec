#include "AstNode.h"

void AstStatement::add(AstNode node)
{
    nodeList.push_back(node);
}

void AstBlock::add(AstStatement statement)
{
    statementList.push_back(statement);
}

void AstRoot::add(AstBlock block)
{
    blocks.push_back(block);
}
