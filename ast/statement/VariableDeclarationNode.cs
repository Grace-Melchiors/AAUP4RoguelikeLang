using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class VariableDeclarationNode : AbstractNode
    {
        public TypeNode Type {get; private set;}
        public string Identifier {get; private set;}
        public ExpressionNode? expression {get; private set;}
        //private List<int>? ArraySizes;
        
        public string GetIdentifier() {
            return Identifier;
        }
        public Enums.Types GetDataType() {
            return Type.GetDataType();
        }

        public TypeNode GetTypeNode() {
            return Type;
        }

        public ExpressionNode GetExpressionNode() {
            return expression;
        }

        //Normal declaration
        public VariableDeclarationNode(TypeNode type, string identifier, ExpressionNode? expression)
        {
            Type = type;
            this.Identifier = identifier;
            this.expression = expression;
            //ArraySizes = null;
        }
        //Array declaration
        //public VariableDeclarationNode(TypeNode type, string? identifier, ExpressionNode? expression, List<int> arraySizes): this(type, identifier, expression) {
        //    ArraySizes = arraySizes;
        //}

    }

    

}
