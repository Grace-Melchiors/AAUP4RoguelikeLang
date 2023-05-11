using System.Globalization;
using Antlr_language.ast;

public class VariableAlreadyDefinedException: Exception {
    public VariableAlreadyDefinedException(string identifier): base($"VariableAlreadyDefinedException. {identifier} is already defined.")
    {
        this.identifier = identifier;

    }
    
    private string identifier;

    
}