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
            result += Type.CodeGen(indentation);
            result += identifier;
            if (expression != null) {
                result += " = " + expression.CodeGen(indentation);
            } else {
                if (Type.GetNodeType() == Enums.Types.INTEGER) {
                    result += " = " + Constants.INTEGER_DEFAULT;
                } else if (Type.GetNodeType() == Enums.Types.BOOL) {
                    result += " = " + Constants.BOOL_DEFAULT;
                } else if (Type.GetNodeType() == Enums.Types.MAP) {
                    result += " = " + Constants.MAP_DEFAULT;
                }
                
            }

            return result;
        }

    }
}
