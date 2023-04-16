#include "Lexer.h"

void Lexer::advance()
{
    if (pos > input.length())
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
    if (currentPos < input.length())
    {
        size_t newPos = currentPos + 1;
        char c = input[newPos];
        return c;
    }
    else
    {
        return ' ';
    }
}

Token Lexer::nextToken()
{
    // move to next token
    advance();

    // for later; string builder
    std::string str;

    // advance its if its a space
    if (currentChar == ' ')
    {
        advance();
    }

    // early check if its a new line
    if (iscntrl(currentChar))
    {
        return Token(EndOfLine, "\n");
    }

    if (currentChar == '\0') // early check if its end of file
        {
        return Token(EndOfFile, "\0");
        }

    // if not its either or
    if (isalnum(currentChar))
    {
        if (isalpha(currentChar))
        {
            // its either a keyword or an identifier

            while (!iscntrl(currentChar) || !isblank(currentChar))
            {
                str += currentChar;
                advance();
                
                if (iscntrl(currentChar) || isblank(currentChar))
                {
                    break;
                }
            }

            if (str == "func" || str == "return")
            {
                return Token(Keyword, str);
            }
            
        
            return Token(Identifier, str);
            
        }
        else if (isdigit(currentChar))
        {
            // if its a digit its probably a number

            while (!iscntrl(currentChar) || !isblank(currentChar))
            {
                str += currentChar;
                advance();

                if (iscntrl(currentChar) || isblank(currentChar))
                {
                    break;
                }
            }
            
            return Token {Literal, str};
        }
    }
    else if (ispunct(currentChar))
    {
        // if its punct its either one of these several cases:
        // a) operator such as + = - ... ie operator
        // b) delimiter such as ( ) , ie separator
        // c) delimited string with " ie literals
        // d) a comment

        if (currentChar == '/')
        {
            while (!iscntrl(currentChar))
            {
                str += currentChar;
                advance();

                if (iscntrl(currentChar))
                {
                    break;
                }
            }
            return Token {Comment, str};
        }

        if (currentChar == '(' || currentChar == ')')
        {
            str += currentChar;
            return Token {Separator, str};
        }

        if (currentChar == '\"')
        {
            advance();
            while (currentChar != '\"')
            {
                str += currentChar;
                advance();
            }
            return Token {Literal, str};
            
        }

        switch (currentChar)
        {
        case '+':
            return Token {Operator, "+"};
        case '-':
            return Token {Operator, "-"};
        case '=':
            return Token {Operator, "="};
        case '/':
            return Token {Operator, "/"};
        case '*':
            return Token {Operator, "*"};
        case '!':
            return Token {Operator, "!"};
        }
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

