#pragma once
#include <memory>
#include <string>

class ASTNode
{
public:
    virtual ~ASTNode() = default;
    virtual void print(int indent = 0) const = 0;
};

class MonogramNode : public ASTNode
{
public:
    MonogramNode(const std::string& value);
    void print(int indent) const override;

private:
    std::string value;
};

class BigramNode : public ASTNode
{
public:
    BigramNode(const std::string& value);
    void print(int indent) const override;

private:
    std::string value;
};

class NumberNode : public ASTNode
{
public:
    NumberNode(const std::string& value);
    void print(int indent) const override;

private:
    std::string value;
};

class LetterNode : public ASTNode
{
public:
    LetterNode(const std::string& value);
    void print(int indent) const override;

private:
    std::string value;
};

class ControlNode : public ASTNode
{
public:
    ControlNode(const std::string& value);
    void print(int indent) const override;

private:
    std::string value;
};

class BlockNode : public ASTNode
{
public:
    BlockNode();
    void print(int indent) const override;
};
