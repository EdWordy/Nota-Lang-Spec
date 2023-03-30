#pragma once

#include <string>
#include <sstream>
#include "node.h"

class Parser {
    std::istringstream input;

    int getNumber();
    char getOperator();
    Node *parseExpression();
    Node *parseTerm();

public:
    Parser(const std::string &str);
    Node *parse();
};
