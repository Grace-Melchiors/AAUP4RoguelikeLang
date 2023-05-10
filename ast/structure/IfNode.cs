using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
	public class IfNode : AbstractNode
	{
        private ExpressionNode? expression;
        private BlockNode? block;
        public string CodeGen (int indentation)
		{
            if (expression == null)
                throw new NotImplementedException();
            if (block == null)
                throw new NotImplementedException();
            //Maybe string builder here?
            string result = "if (";
            result += expression.CodeGen(indentation);
            result += ") ";
            result += block.CodeGen(indentation);
            return result;
        }

	}
}


