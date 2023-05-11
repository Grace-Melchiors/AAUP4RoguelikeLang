using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Antlr_language.ast.structure;
using Antlr_language.ast.statement;
using Antlr_language.ast.expression;
using Antlr_language.ast;

public class SemanticAnalysis {
    public SemanticAnalysis() {
        InitialiseSymbolTable();
    }

    // A symbol table is associated with each scope
    // As opposed to one global table (Crafting a Compiler 8.2.2)
    public Stack<Dictionary<string, object>> SymbolTableStack = new Stack<Dictionary<string, object>>();

    public void InitialiseSymbolTable(){
        Dictionary<string, object> NewSymbolTable = new Dictionary<string, object>();
        SymbolTableStack.Push(NewSymbolTable);
    }
    
    public Dictionary<string, Enums.Types> IdentifierAndType = new Dictionary<string, Enums.Types>(); 


    // Opens the scope by pushing a symbol table to the stack.
    // Usage: When entering a new scope
    public void OpenScope() { // Public for now
        Dictionary<string, object> NewSymbolTable = new Dictionary<string, object>();
        SymbolTableStack.Push(NewSymbolTable);
        System.Console.WriteLine("Opening Scope");
    }

    // Closes the scope by popping symbol table from the stack.
    // Usage: When exitting a scope, as it removes all variables.

    public void CloseScope() { // Public for now
        SymbolTableStack.Pop();
        System.Console.WriteLine("Closing Scope");
    }

    // Iterates all elements of the stacks. Prints each to std.out.
    public void CheckStack() {
        int level = 0;
        int TotalLevels = SymbolTableStack.Count() - 1;
        System.Console.WriteLine("-----");
        System.Console.WriteLine("Top of stack");

        // Level 0 = top of stack
        while(level <= TotalLevels) {
            System.Console.WriteLine("|---------- Level {0} ----------|");


            foreach(var kvp in SymbolTableStack.ElementAt(level)) {
                System.Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }


            if(level == TotalLevels) {
                System.Console.WriteLine("|-----------------------------|");
            }
            level++;
        }

        System.Console.WriteLine("Bottom of stack");
        System.Console.WriteLine("-----");
    }

    // Enters a new value into the symbol table located on top of the stack.
    public void EnterSymbol(string name, object obj) {
        // Retrieve the top of the stack.
        Dictionary<string, object> SymbolTableTopOfStack = SymbolTableStack.Peek();
        // Add symbol to the dictionary.
        SymbolTableTopOfStack.Add(name, obj);
    }

    // Retrieves a symbol in the stack of symbol tables by name.
    // Starts at the symbol table located at the top of the stack.
    // If name is not found, checks the next level of the stack until no more levels.
    public object RetrieveSymbol(string name) {
        int CurrentLevelInStack = 0;
        int LevelsInStack = SymbolTableStack.Count - 1;

        //System.Console.WriteLine("Total levels (including level 0): {0}", LevelsInStack + 1);

        while(CurrentLevelInStack <= LevelsInStack) {

            if (SymbolTableStack.ElementAt(CurrentLevelInStack).ContainsKey(name))
            {
                System.Console.WriteLine("Found symbol at level {0}", CurrentLevelInStack);
                return SymbolTableStack.ElementAt(CurrentLevelInStack)[name];
            }
            else {
                System.Console.WriteLine("Symbol not found, checking next level. Current level: {0}", CurrentLevelInStack);
                CurrentLevelInStack++;
            }

        }

        return null;
    }
    
    public void VisitProgram(ProgramNode programNode) {
        OpenScope();
        VisitLines(programNode.retrieveLineNodes());
        
        CloseScope();
    
    }
    
    public void VisitLines(List<LineNode> lineNodes) {
        foreach(LineNode lineNode in lineNodes) {
            VisitLine(lineNode);

        }
    }
    
    public void VisitLine(LineNode lineNode) {
        if(lineNode.IsStatement()) {
            StatementNode statementNode = lineNode.GetStatementNode();
            VisitStatement(statementNode);

        }  else {
            FunctionDeclarationNode functionDeclarationNode = lineNode.GetFunctionDeclarationNode();
            throw new NotImplementedException("Function declarations not yet supported");
        }
        
    }

    public void VisitStatement(StatementNode statementNode) {
        if(statementNode.varDecl != null) {
            VisitVariableDeclaration(statementNode.varDecl);
        }
    }
    

    public void VisitVariableDeclaration(VariableDeclarationNode variableDeclarationNode) {
        string identifier = variableDeclarationNode.GetIdentifier();
        Enums.Types typeLHS = variableDeclarationNode.GetDataType();
        //IdentifierAndType.Add(identifier, typeLHS);
        
        if(RetrieveSymbol(identifier) == null) {
            ExpressionNode expressionNode = variableDeclarationNode.GetExpressionNode();
            if(expressionNode != null) {
               Enums.Types typeRHS = VisitExpression(expressionNode);
               
               bool MatchingTypes = typeLHS == typeRHS;
               
               if(MatchingTypes) {
                EnterSymbol(variableDeclarationNode.GetIdentifier(), variableDeclarationNode);
               }
               else {
                throw new TypeMismatchException(identifier, typeLHS, typeRHS);

               }
               
            }
        } else {
            throw new VariableAlreadyDefinedException(identifier);
        }
        

        
    }
    
    public Enums.Types VisitExpression(ExpressionNode expressionNode) {
        // If expression has a variable
        if(expressionNode.GetVariableName() != null) {
        //if(expressionNode.GetFactorNode != null && RetrieveSymbol(expressionNode.GetFactorNode().) != null) {
            AbstractNode symbol = (AbstractNode) RetrieveSymbol(expressionNode.GetVariableName());
            if(symbol != null) {
                //return GetDataTypeFromName(expressionNode.GetVariableName());
                System.Console.WriteLine("Not null");
                return ((VariableDeclarationNode) symbol).GetDataType();

            }
            throw new Exception("Variable not defined!");
        }

        Tuple<ExpressionNode, ExpressionNode> expressionNodes = expressionNode.GetExpressionNodes();
        if(expressionNode.GetFactorNode() != null && expressionNodes.Item1 == null && expressionNodes.Item2 == null) {
            System.Console.WriteLine("this is a factor");
            Enums.Types dataType = VisitFactor(expressionNode.GetFactorNode());
            return dataType;
        } 
        
        

        else if (expressionNode.GetNumber() != null) {
            System.Console.WriteLine("Trying GetNumber");
            return GetDataTypeFromLiteral(expressionNode.GetNumber());

        }
        
        else if(expressionNodes.Item2 != null) {
            Console.WriteLine("Revisiting VisitExpression on Item1");
            Enums.Types? dataType1 = VisitExpression(expressionNodes.Item1);

            Console.WriteLine("Revisiting VisitExpression on Item2");
            Enums.Types? dataType2 = VisitExpression(expressionNodes.Item2);

            if(dataType1 != null && dataType2 != null && dataType1 != dataType2) {
                throw new TypeMismatchException((Enums.Types) dataType1, (Enums.Types) dataType2);
            }
            else if(dataType1 != null) {
                return (Enums.Types) dataType1;
            }
            

        }
        else {
            return VisitExpression(expressionNodes.Item1);
        }
        
        throw new NotImplementedException("Type not accepted!");
    }
    
    public Enums.Types VisitFactor(FactorNode factorNode) {
        Enums.Types dataType = factorNode.getEvaluationType();
        return dataType;
    }
    
    private Enums.Types GetDataTypeFromLiteral(string literal) {
        if (literal.Equals("true") || literal.Equals("false")) {
            return Enums.Types.BOOL;
        } else {
            return Enums.Types.INTEGER;
        }
    }
    
    private Enums.Types GetDataTypeFromName(string name) {
        Enums.Types? type = null;
        AbstractNode abstractNode = (AbstractNode) RetrieveSymbol(name);
        
        if(abstractNode is StatementNode) {
            StatementNode statementNode = (StatementNode) abstractNode;
            if(statementNode.varDecl != null) {
                type = statementNode.varDecl.GetDataType();
            }
        } else if(abstractNode is FunctionDeclarationNode) {
            throw new NotImplementedException("Not supported");

        }
        
        if(type != null) {
            return (Enums.Types) type;
        }
        throw new VariableAlreadyDefinedException(name);
    }
    

}