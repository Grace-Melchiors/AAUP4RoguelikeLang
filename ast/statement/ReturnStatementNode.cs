using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class ReturnStatementNode : AbstractNode
    {
        private ExpressionNode? expression;

        public ReturnStatementNode(ExpressionNode? expression)
        {
            this.expression = expression;
        }

        public string CodeGen(int indentation)
        {
            string result = "return ";
            if (expression != null) {
                result += expression.CodeGen(indentation) + ";";
            } else {
                throw new NotImplementedException();
            }
            return result;
        }

    }
}
