#pragma once
#include <string>
#include <vector>

class AstNode
{
public:
    std::string value;
    
    AstNode(std::string Value) : value(Value) {}
    AstNode() = default;
};

class AstStatement
{
public:
    std::vector<AstNode> nodeList;
    
    AstStatement(std::vector<AstNode> NodeList) : nodeList(NodeList) {}
    AstStatement() = default;

    void add(AstNode node);
};

class AstBlock
{
public:
    std::vector<AstStatement> statementList;
    
    AstBlock(std::vector<AstStatement> StatementList) : statementList(StatementList) {}
    AstBlock() = default;
    
    void add(AstStatement statement);
};

class AstRoot
{
public:
    std::vector<AstBlock> blocks;
    
    AstRoot(std::vector<AstBlock> Blocks) : blocks(Blocks) {}
    AstRoot() = default;

    void add(AstBlock block);
};