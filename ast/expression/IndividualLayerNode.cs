using System;
using System.Text;
using Antlr_language.ast.structure;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.expression
{
    public class IndividualLayerNode : AbstractExpressionNode
    {
        public TypeNode LayerType {get; private set;}
        public string IDENTIFIER {get; private set;}
        public ExpressionNode? Expression {get; private set;}

        public IndividualLayerNode(TypeNode type, string iDENTIFIER, ExpressionNode? expression)
        {
            this.LayerType = type;
            IDENTIFIER = iDENTIFIER;
            this.Expression = expression;
        }
        public Enums.Types GetNodeType () {
            return LayerType.GetNodeType();
        }
    }
}