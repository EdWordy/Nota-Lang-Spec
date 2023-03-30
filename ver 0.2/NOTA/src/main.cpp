#include <fstream>
#include <iostream>
#include "SCRIBE/Parser.h"

int main(char argv[])
{
    // if the file name isn't null then
    if (argv != NULL)
    {
        // TODO: Finish code here for command line args by filename
        
        // Create a text string, which is used to output the text file
        //std::string code;

        // Read from the text file
        std::ifstream MyReadFile(argv);
        
        // Example value
        std::string code = "(5 * 10) + 4";
        
        // Parse
        Parser parser(code);
    
        // Create the AST
        Node *ast = parser.parse();

        // print
        std::ofstream MyWriteFile("Output.txt");

        MyWriteFile << ast->eval() << std::endl;
        std::cout << "Result: " << ast->eval() << std::endl;
        std::cout << code;
        

        // cleanup
        delete ast;
        MyReadFile.close();
    
        return 0;
    }
}