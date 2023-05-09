using System;

namespace Antlr_language.ast.expression
{
    public class ExpressionNode : AbstractNode
    {
        private ParenthesizedExpressionNode parenthesizedExpression;
        public string CodeGen()
        {
            if (parenthesizedExpression != null) {
                return parenthesizedExpression.CodeGen();
            }
            else {
                throw new NotImplementedException();
            }
        }

    }
}
