#include <fstream>
#include <iostream>
#include <sstream>
#include <string>

#include "Lexer.h"
#include "Parser.h"

int main(char* argv[])
{
    // get the input
    // a
    // std::string inputFile = argv[1];
    // b
    std::string inputFile = "E:/Dev/source/repos/cpp/Scribe/ThreePointZero/x64/Debug/input.txt";

    // feed it to a file
    std::ifstream file(inputFile);

    // check if its open
    if (file.is_open())
    {
        // Create a stringstream to hold the contents of the file
        std::stringstream file_content;

        // Read the entire file into the stringstream
        file_content << file.rdbuf();

        // close the file
        file.close();

        // Convert the stringstream to a string
        std::string string = file_content.str();

        // std::cout << string << "\n\n";

        // lexing
        Lexer lexer(string);
        lexer.scan();

        //for (Token T : lexer.scannedTokens)
        //{
        //    std::cout << T.GetValue() << ": " << T.GetType() << "\n";
        //}
        
        // parsing
        Parser parser(lexer);
        parser.Parse();
        
        // semantic analysis
    }
    else
    {
        std::cerr << "Unable to open file: " << inputFile << std::endl;
        return 1;
    }
    return 0;
}
