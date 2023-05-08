using System.Globalization;
using System.Collections.Generic;
public class Semantics {
    public Semantics() {
        InitialiseSymbolTable();
    }

    public Dictionary<string, object> NewSymbolTable = new Dictionary<string, object>();
    // A symbol table is associated with each scope
    // As opposed to one global table (Crafting a Compiler 8.2.2)
    public Stack<Dictionary<string, object>> SymbolTableStack = new Stack<Dictionary<string, object>>();
    
    public void InitialiseSymbolTable(){
        SymbolTableStack.Push(NewSymbolTable);
    }
    
    
    // Opens the scope by pushing a symbol table to the stack.
    // Usage: When entering a new scope
    private void OpenScope() {
        Dictionary<string, object> NewSymbolTable = new Dictionary<string, object>();
        SymbolTableStack.Push(NewSymbolTable);
        System.Console.WriteLine("Opening Scope");

        
    }
    
    // Closes the scope by popping symbol table from the stack. 
    // Usage: When exitting a scope, as it removes all variables.

    private void CloseScope() {
        SymbolTableStack.Pop();
        System.Console.WriteLine("Closing Scope");
    }
    
    // Iterates all elements of the stacks. Prints each to std.out.
    public void CheckStack() {
        foreach(var dct in SymbolTableStack) {
            foreach(var kvp in dct) {
                System.Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
        }
    }
    
    // Enters a new value into the symbol table on top of the stack.
    public void EnterSymbol(String name, object obj) {
        // Retrieve the top of the stack.
        Dictionary<string, object> SymbolTableTopOfStack = SymbolTableStack.Peek();
        
        // Add symbol to the dictionary.
        SymbolTableTopOfStack.Add(name, obj);

        
    }
}