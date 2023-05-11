using System.Globalization;
using Antlr_language.ast;

public class UndefinedTypeException: Exception {
    public UndefinedTypeException(string message): base($"UndefinedTypeException. {message}.")
    {
        this.message = message;

    }

    
    private string message;
    
}