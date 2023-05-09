using System;

namespace Antlr_language.ast.expression
{
    public class ArithmeticExpressionNode : AbstractNode
    {

        private string Literal;
        private string VariableName;

        private ArrayAccessNode ArrayAccess;

        public string CodeGen()
        {
            if (Literal != null) {
                return Literal;
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
