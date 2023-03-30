#pragma once

// The base node class for NOTAs AST.
class Node
{
public:
    virtual ~Node() {}
    virtual int eval() = 0;
};

class NumNode : public Node
{
    int value_;

public:
    NumNode(int value);
    int eval() override;
};

class MathOpNode : public Node
{
    char op_;
    Node *left_, *right_;

public:
    MathOpNode(char op_, Node *left_, Node *right_);
    ~MathOpNode();
    int eval() override;
};

// 

class BigramOpNode : public Node
{
    char op1_;
    char op2_;
    Node *left_;
    Node *right_;
    
public:
    BigramOpNode(char op1_, char op2_, Node *left_, Node *right_);
    ~BigramOpNode();
    int eval() override;
    
};
