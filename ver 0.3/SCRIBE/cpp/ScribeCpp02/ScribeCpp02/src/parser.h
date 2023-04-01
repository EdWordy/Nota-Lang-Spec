#pragma once

#include "lexer.h"
#include "ast_nodes.h"
#include <memory>

class Parser
{
public:
    Parser(Lexer& lexer);

    std::unique_ptr<ASTNode> parse(const Token& token);

private:
    Lexer& lexer;
};
