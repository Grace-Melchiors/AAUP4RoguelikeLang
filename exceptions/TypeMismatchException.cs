using System.Globalization;
using Antlr_language.ast;

public class TypeMismatchException: Exception {
    public TypeMismatchException(string identifier, Enums.Types typeLHS, Enums.Types typeRHS): base($"TypeMismatchException. Types for {identifier} are mismatched. Type {typeRHS} is not assignable to type {typeLHS}.")
    {
        this.identifier = identifier;
        this.typeLHS = typeLHS;
        this.typeRHS = typeRHS;

    }

    public TypeMismatchException(Enums.Types typeLHS, Enums.Types typeRHS): base($"TypeMismatchException. Type {typeRHS} is not assignable to type {typeLHS}.")
    {
        this.typeLHS = typeLHS;
        this.typeRHS = typeRHS;

    }
    
    private string? identifier;
    private Enums.Types typeLHS;
    private Enums.Types typeRHS;

    
}