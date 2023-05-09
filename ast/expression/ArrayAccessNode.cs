using System;

namespace Antlr_language.ast.expression
{
    public class ArrayAccessNode : AbstractNode
    {

        private Enums.Types type;
        private string VariableName;
        private ArithmeticExpressionNode index;
        

        public string CodeGen()
        {
            string result = VariableName + "[" + index.CodeGen() + "]";
            return result;
        }

    }
}
