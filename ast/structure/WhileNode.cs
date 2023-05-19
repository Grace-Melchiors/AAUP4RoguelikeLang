using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class WhileNode : AbstractNode
    {
        public ExpressionNode? expression {get; private set;}
        public BlockNode? block {get; private set;}

        public WhileNode(ExpressionNode? expression, BlockNode? block)
        {
            this.expression = expression;
            this.block = block;
        }

    }
}
