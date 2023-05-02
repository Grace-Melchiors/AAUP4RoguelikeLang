using System;

namespace Antlr_language.ast.expression
{
    public class ExpressionNode : AbstractNode
    {
        private ParenthesizedExpressionNode parenthesizedExpression;
        private ArithmeticExpressionNode arithmeticExpression;
        private LogicalExpressionNode logicalExpression;
        public string CodeGen()
        {
            if (parenthesizedExpression != null) {
                return parenthesizedExpression.CodeGen();
            }
            else if (arithmeticExpression != null) {
                return arithmeticExpression.CodeGen();
            }
            else if (logicalExpression != null) {
                return logicalExpression.CodeGen();
            }
            else {
                throw new NotImplementedException();
            }
        }

    }
}
