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
            result += Type.CodeGen(indentation);
            result += identifier;
            if (expression != null) {
                result += " = " + expression.CodeGen(indentation);
            }

            return result;
        }

    }
}
