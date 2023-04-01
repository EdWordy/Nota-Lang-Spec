#include "ast_nodes.h"
#include <iostream>

MonogramNode::MonogramNode(const std::string& value) : value(value) {}

void MonogramNode::print(int indent) const
{
    std::cout << std::string(indent, ' ') << "Mono Op: " << value << std::endl;
}

BigramNode::BigramNode(const std::string& value) : value(value) {}

void BigramNode::print(int indent) const
{
    std::cout << std::string(indent, ' ') << "Bi Op: " << value << std::endl;
}

NumberNode::NumberNode(const std::string& value) : value(value) {}

void NumberNode::print(int indent) const
{
    std::cout << std::string(indent, ' ') << "Number: " << value << std::endl;
}

LetterNode::LetterNode(const std::string& value) : value(value)  {}

void LetterNode::print(int indent) const
{
    std::cout << std::string(indent, ' ') << "Letter: " << value << std::endl;
}

ControlNode::ControlNode(const std::string& value) : value(value) {}

void ControlNode::print(int indent) const
{
    std::cout << std::string(indent, ' ') << "ControlNode: " << value << std::endl;
}

BlockNode::BlockNode() {}

void BlockNode::print(int indent) const
{
    std::cout << std::string(indent, ' ') << "Block" << std::endl;
}
