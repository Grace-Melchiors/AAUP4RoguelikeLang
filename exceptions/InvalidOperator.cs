using System.Globalization;
using Antlr_language.ast;

public class InvalidOperator: Exception {
    public InvalidOperator(string message, Enums.Operators operators): base(message)
    {
        this.operators = operators;
    }
    public InvalidOperator(Enums.Operators operators): base($"Operator: {operators.ToString()} is not aplicable here.")
    {
        this.operators = operators;
    }
    private Enums.Operators operators;
}