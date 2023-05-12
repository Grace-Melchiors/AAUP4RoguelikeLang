using System.Globalization;
using Antlr_language.ast;

public class UndefinedVariableException: Exception {
    public UndefinedVariableException(string identifier): base($"UndefinedVariableError. {identifier} is not defined.")
    {
        this.identifier = identifier;

    }
    
    private string identifier;

    
}