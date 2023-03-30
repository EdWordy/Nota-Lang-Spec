#include "Node.h"
#include <iostream>

// The node that represents a number in NOTAs AST
NumNode::NumNode(int value) : value_(value) {}

int NumNode::eval()
{
    return value_;
}

// The node that represents a Math Operator in NOTAs AST
MathOpNode::MathOpNode(char op, Node *left, Node *right) : op_(op), left_(left), right_(right) {}

MathOpNode::~MathOpNode()
{
    delete left_;
    delete right_;
}

int MathOpNode::eval()
{
    int l = left_->eval();
    int r = right_->eval();

    std::cout << "L: " << l << "\n";
    std::cout << "R: " << r << "\n";
    
    switch (op_)
    {
        // Basic math operators
        case '+': return l + r;
        case '-': return l - r;
        case '*': return l * r;
        case '/': return l / r;
        case '%': return l % r;
        
        default: return 0;
    }
}

// The node that represents bigrammatic operators, which function as a single unit
BigramOpNode::BigramOpNode(char op1, char op2, Node *left, Node *right) : op1_(op1), op2_(op2), left_(left), right_(right) {}

BigramOpNode::~BigramOpNode()
{
    delete left_;
    delete right_;
}

int BigramOpNode::eval()
{
    char l = op1_;
    char r = op2_;

    // TODO: Finish this ...

    // Take the op1_ and op2_ operators and combine them into a single unit
    // Bigram gram = Bigram(op1_, op2_);
    
    // then treat it like the previous MathOpNode eval
    //
    
    return 0;
}
