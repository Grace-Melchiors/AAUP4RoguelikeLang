using System;
using System.Text;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.expression
{
    public class ArrayExpressionNode : AbstractExpressionNode
    {

        public List<ExpressionNode> expressions {get; private set;}

        public ArrayExpressionNode(List<ExpressionNode> expressions)
        {
            this.expressions = expressions;
        }
        
        public List<ExpressionNode> GetExpressions() {
            return expressions;
        }

    }
}
