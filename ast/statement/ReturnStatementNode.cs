using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class ReturnStatementNode : AbstractNode
    {
        public ExpressionNode? expression {get; private set;}

        public ReturnStatementNode(ExpressionNode? expression)
        {
            this.expression = expression;
        }

    }
}
