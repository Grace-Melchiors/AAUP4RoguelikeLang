using System;
using System.Text;

namespace Antlr_language.ast.expression
{
    public class ArrayAccessNode : AbstractExpressionNode
    {

        public Factor2Node factor2 {get; private set;}
        public ArrayDimensionsNode indicies {get; private set;}

        public ArrayAccessNode(Factor2Node factor2, ArrayDimensionsNode indicies)
        {
            this.factor2 = factor2;
            this.indicies = indicies;
        }

    }
}
