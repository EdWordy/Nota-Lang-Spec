#include <iostream>
#include <regex>
#include "Lexer.h"

void Lexer::advance()
{
    if (pos > input.length() + 1)
    {
        currentChar = '\0';
    }
    else
    {
        currentChar = input[pos++];
    }
}

char Lexer::peek(size_t currentPos)
{
    if (currentPos > input.length() + 1)
    {
        return '\0';
    }
    else
    {
        return input[currentPos];
    }
}

Token Lexer::handleLetters()
{
    std::string str;
    
    // its either a keyword or an identifier
    while (!iscntrl(currentChar) || !isblank(currentChar))
    {
        str += currentChar;
        advance();
                
        if (iscntrl(currentChar) || isblank(currentChar))
        {
            pos--;
            break;
        }
    }

    // keyword check
    if (str == "func" || str == "return"|| str == "print"|| str == "int"|| str == "str"|| str == "bool" || str == "true" || str == "false")
    {
        return Token {Keyword, str};
    }
            
    // else its an identifier
    return Token {Identifier, str};
}

Token Lexer::handleDigits()
{
    std::string str;
    
    // if its a digit its probably a number
    while (!iscntrl(currentChar) || !isblank(currentChar))
    {
        str += currentChar;
        advance();

        if (iscntrl(currentChar) || isblank(currentChar))
        {
            pos--;
            break;
        }
    }
            
    return Token {Literal, str};
}

Token Lexer::handleSymbols()
{
    std::string str;
    
    // if its punct its either one of these cases:
    // a) operator such as + = - ... ie operator
    // b) delimiter such as ( ) , ie separator
    // c) delimited string with " ie literals
    // d) a comment

    if (currentChar == '/') // d comments
        {
        while (!iscntrl(currentChar))
        {
            str += currentChar;
            advance();

            if (iscntrl(currentChar))
            {
                pos--;
                break;
            }
        }
        return Token {Comment, str};
        }

    if (currentChar == '(' || currentChar == ')') // b separator
        {
        str += currentChar;
        return Token {Separator, str};
        }

    if (currentChar == '\"') // c string literal
        {
        advance();
        while (currentChar != '\"')
        {
            str += currentChar;
            advance();
        }
        return Token {Literal, str};
            
        }

    switch (currentChar) // a operators
    {
    case '+':
        return Token {Operator, "+"};
    case '-':
        return Token {Operator, "-"};
    case '=':
        if (peek(pos) == '=')
        {
            advance();
            return Token {Operator, "=="};
        }
        return Token {Operator, "="};
    case '/':
        return Token {Operator, "/"};
    case '*':
        return Token {Operator, "*"};
    case '!':
        if (peek(pos) == '=')
        {
            advance();
            return Token {Operator, "!="};
        }
        return Token {Operator, "!"};
    default:
        std::cout << "unexpected char: [" << currentChar << "] at " << pos << ":" << lineNum;
    }

    // catch-all
    return Token {Unknown, "?"};
}

// ----------------------------------

Token Lexer::nextToken()
{
    // move to next char
    advance();

    // advance its if its a space
    if (currentChar == ' ')
    {
        advance();
    }

    // early check if its a new line
    if (iscntrl(currentChar))
    {
        lineNum++;
        return Token { EndOfLine, "\\n" };
    }

    // early check if its end of file
    if (currentChar == '\0') 
    {
        return Token {EndOfFile, "\0" };
    }

    // if its not a letter or digit, then its a symbol
    if (isalnum(currentChar))
    {
        if (isalpha(currentChar))
        {
            return handleLetters();
        }
        else if (isdigit(currentChar))
        {
            return handleDigits();
        }
    }
    else if (ispunct(currentChar))
    {
        return handleSymbols();   
    }
    
    // catch-all
    return Token {Unknown, "?"};
}

void Lexer::scan()
{
    // scan tokens
    while (pos != input.length() + 1)
    {
        scannedTokens.push_back(nextToken());
    }

    // add the last token
    Token EOFtoken {EndOfFile, "\0"};
    scannedTokens.push_back(EOFtoken);
}
