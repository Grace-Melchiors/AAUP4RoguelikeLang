using System.Globalization;
using Antlr_language.ast;

public class TypeMismatchException: Exception {
    public TypeMismatchException(string identifier, Enums.Types typeLHS, Enums.Types typeRHS): base($"TypeMismatchException. Types for {identifier} are mismatched. Type {typeLHS} is not assignable to type {typeRHS}.")
    {
        this.identifier = identifier;
        this.typeLHS = typeLHS;
        this.typeRHS = typeRHS;

    }

    public TypeMismatchException(Enums.Types typeLHS, Enums.Types typeRHS): base($"TypeMismatchException. Type {typeLHS} is not assignable to type {typeRHS}.")
    {
        this.typeLHS = typeLHS;
        this.typeRHS = typeRHS;

    }
    
    private string? identifier;
    private Enums.Types typeLHS;
    private Enums.Types typeRHS;

    
}