using System;
using Antlr_language.ast.expression;
using Antlr_language.ast;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class VariableDeclarationNode : AbstractNode
    {
        private TypeNode Type;
        private string? identifier;
        private ExpressionNode? expression;
        private List<int>? ArraySizes;
        
        public string GetIdentifier() {
            return identifier;
        }
        public Enums.Types GetDataType() {
            return Type.GetDataType();
        }

        public ExpressionNode GetExpressionNode() {
            return expression;
        }

        //Normal declaration
        public VariableDeclarationNode(TypeNode type, string? identifier, ExpressionNode? expression)
        {
            Type = type;
            this.identifier = identifier;
            this.expression = expression;
            ArraySizes = null;
        }
        //Array declaration
        public VariableDeclarationNode(TypeNode type, string? identifier, ExpressionNode? expression, List<int> arraySizes): this(type, identifier, expression) {
            ArraySizes = arraySizes;
        }

        public string CodeGen(int indentation)
        {
            string result = "";
            result += Type.CodeGen(indentation) + " ";
            result += identifier;
            if (expression != null) {
                result += " = " + expression.CodeGen(indentation);
            } else {
                if (Type.GetNodeType() == Enums.Types.INTEGER) {
                    if (ArraySizes != null) {
                        result += " = new " + Type.CodeGen(indentation) + "[";
                        foreach (var size in ArraySizes)
                            result += size + ",";
                        result = result.Substring(0, result.Length-1);
                        result += "]";
                    } else {
                        result += " = " + Constants.INTEGER_DEFAULT;
                    }
                } else if (Type.GetNodeType() == Enums.Types.BOOL) {
                    if (ArraySizes != null) {
                        result += " = new " + Type.CodeGen(indentation) + "[";
                        foreach (var size in ArraySizes)
                            result += size + ",";
                        result = result.Substring(0, result.Length-1);
                        result += "]";
                    } else {
                        result += " = " + Constants.BOOL_DEFAULT;
                    }
                    
                } else {
                    throw new NotImplementedException();
                }
                
            }

            return result;
        }

    }

    

}
