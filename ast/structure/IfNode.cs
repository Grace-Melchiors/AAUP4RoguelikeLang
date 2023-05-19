using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
	public class IfNode : AbstractNode
	{
        public ExpressionNode expression {get; private set;}
        public BlockNode block {get; private set;}
        public BlockNode? elseBlock {get; private set;}

        public IfNode(ExpressionNode expression, BlockNode block, BlockNode? elseBlock)
        {
            this.expression = expression;
            this.block = block;
            this.elseBlock = elseBlock;
        }

	}
}


