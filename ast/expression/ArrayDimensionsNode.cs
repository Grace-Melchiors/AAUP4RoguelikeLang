using System;
using System.Text;


namespace Antlr_language.ast.expression
{
    public class ArrayDimensionsNode : AbstractExpressionNode
    {
        public List<ExpressionNode> expressions {get; private set;}

        public ArrayDimensionsNode(List<ExpressionNode> expressions)
        {
            this.expressions = expressions;
        }
    }
}