using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class ChanceNode : AbstractNode
    {
        public List<ExpressionNode> weights {get;private set;}
        public List<BlockNode> blocks {get;private set;}

        public ChanceNode(List<ExpressionNode> weights, List<BlockNode> blocks)
        {
            this.weights = weights;
            this.blocks = blocks;
        }

    }
}
