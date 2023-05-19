using System;

namespace Antlr_language.ast.expression
{
    public class ConstantNode : AbstractExpressionNode
    {
        
        public bool? boolean{get; private set;} = null;
        public int? integer{get; private set;} = null;

        public ConstantNode(bool? boolean, int? integer)
        {
            this.boolean = boolean;
            this.integer = integer;
        }
    }
}
