# AAUP4RoguelikeLang
Centered around designing and implementing a programming language to create roguelike maps.

## Languages used:
C#, Antlr4

## How to use
### Packages
To utilize MapGenius you must have the following packages downloaded:

### How to run
The program is used by calling the compiler from a console with two arguments, the first being the path to the input file, and the second argument being the path to the output file, that is about to be created.
Additionally, two flags are available during compilation:
- debug: Skips the CST to AST transformation, AST decoration and Code Generation phases. Allows for execution of the program with generating an output file.
- verbose: Prints the contents of the symbol table repeatedly throughout the different the AST decoration.


## Credit
Repository for our AAU 2023 P4 project. Designed and developed by 
- Andreas Worm Holt
- Daniel Hilo Hansen
- Frederik Melchiors
- Karen Andersen
- Mikkel Daniel Bjørn
- Theis Randeris Mathiassen 

## License

[MIT](https://choosealicense.com/licenses/mit/)
