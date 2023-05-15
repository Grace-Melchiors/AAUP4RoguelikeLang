using System.Threading;
using System.Linq.Expressions;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Antlr_language.ast.structure;
using Antlr_language.ast.statement;
using Antlr_language.ast.expression;
using Antlr_language.ast;

public class SymbolTable
{
    public SymbolTable()
    {
        InitialiseSymbolTable();
    }

    // A symbol table is associated with each scope
    // As opposed to one global table (Crafting a Compiler 8.2.2)
    public Stack<Dictionary<string, object>> SymbolTableStack = new Stack<Dictionary<string, object>>();

    public void InitialiseSymbolTable()
    {
        Dictionary<string, object> NewSymbolTable = new Dictionary<string, object>();
        SymbolTableStack.Push(NewSymbolTable);
    }

    public Dictionary<string, Enums.Types> IdentifierAndType = new Dictionary<string, Enums.Types>();


    // Opens the scope by pushing a symbol table to the stack.
    // Usage: When entering a new scope
    public void OpenScope()
    { // Public for now
        Dictionary<string, object> NewSymbolTable = new Dictionary<string, object>();
        SymbolTableStack.Push(NewSymbolTable);
        System.Console.WriteLine("Opening Scope");
    }

    // Closes the scope by popping symbol table from the stack.
    // Usage: When exitting a scope, as it removes all variables.

    public void CloseScope()
    { // Public for now
        SymbolTableStack.Pop();
        System.Console.WriteLine("Closing Scope");
    }

    // Iterates all elements of the stacks. Prints each to std.out.
    public void CheckStack()
    {
        int level = 0;
        int TotalLevels = SymbolTableStack.Count() - 1;
        System.Console.WriteLine("-----");
        System.Console.WriteLine("Top of stack");

        // Level 0 = top of stack
        while (level <= TotalLevels)
        {
            System.Console.WriteLine("|---------- Level {0} ----------|");


            foreach (var kvp in SymbolTableStack.ElementAt(level))
            {
                System.Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }


            if (level == TotalLevels)
            {
                System.Console.WriteLine("|-----------------------------|");
            }
            level++;
        }

        System.Console.WriteLine("Bottom of stack");
        System.Console.WriteLine("-----");
    }

    // Enters a new value into the symbol table located on top of the stack.
    public void EnterSymbol(string name, object obj)
    {
        // Retrieve the top of the stack.
        Dictionary<string, object> SymbolTableTopOfStack = SymbolTableStack.Peek();
        // Add symbol to the dictionary.
        SymbolTableTopOfStack.Add(name, obj);
    }

    // Retrieves a symbol in the stack of symbol tables by name.
    // Starts at the symbol table located at the top of the stack.
    // If name is not found, checks the next level of the stack until no more levels.
    public object RetrieveSymbol(string name)
    {
        int CurrentLevelInStack = 0;
        int LevelsInStack = SymbolTableStack.Count - 1;

        //System.Console.WriteLine("Total levels (including level 0): {0}", LevelsInStack + 1);

        while (CurrentLevelInStack <= LevelsInStack)
        {

            if (SymbolTableStack.ElementAt(CurrentLevelInStack).ContainsKey(name))
            {
                System.Console.WriteLine("Found symbol at level {0}", CurrentLevelInStack);
                return SymbolTableStack.ElementAt(CurrentLevelInStack)[name];
            }
            else
            {
                System.Console.WriteLine("Symbol not found, checking next level. Current level: {0}", CurrentLevelInStack);
                CurrentLevelInStack++;
            }

        }

        return null;
    }
}

    