#include "parser.h"

Parser::Parser(const std::string &str) : input(str) {}

// gets the value
int Parser::getNumber()
{
    int value;
    input >> value;
    return value;
}

// gets the operator
char Parser::getOperator()
{
    char op;
    input >> op;
    return op;
}

// Parse the term in a given expression, as a tree
Node *Parser::parseTerm()
{
    if (input.peek() == '(')
    {
        input.get(); // Consume the opening parenthesis
        
        Node *node = parseExpression();

        if (input.peek() == ')')
        {
            input.get(); // Consume the closing parenthesis
        }
        else
        {
            // throw error: missing closing parenthesis
        }
        return node;
    }
    else
    {
        int value = getNumber();
        return new NumNode(value);
    }
}

// Parse an expression term by term, as a tree
Node *Parser::parseExpression()
{
    Node *left = parseTerm();

    while (input.peek() != EOF && input.peek() != ')' && input.peek() != '\n')
    {
        char op = getOperator();
        Node *right = parseTerm();
        left = new MathOpNode(op, left, right);
    }

    return left;
}

// General parse function; call when needed to parse the AST.
Node *Parser::parse()
{
    
    return parseExpression();
}