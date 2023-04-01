#include "parser.h"

Parser::Parser(Lexer& lexer) : lexer(lexer) {}

std::unique_ptr<ASTNode> Parser::parse(const Token& token)
{
    switch (token.type)
    {
    case TokenType::Monogram:
        return std::make_unique<MonogramNode>(token.value);
    case TokenType::Bigram:
        return std::make_unique<BigramNode>(token.value);
    case TokenType::Number:
        return std::make_unique<NumberNode>(token.value);
    case TokenType::Letter:
        return std::make_unique<LetterNode>(token.value);
    case TokenType::Control:
        return std::make_unique<ControlNode>(token.value);
    case TokenType::Block:
        return std::make_unique<BlockNode>();
    default:
        return nullptr;
    }
}
