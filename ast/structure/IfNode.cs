using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
	public class IfNode : AbstractNode
	{
        private ExpressionNode expression;
        private BlockNode block;
        private ElseIfNode? elseIfNode;

        public IfNode(ExpressionNode expression, BlockNode block, ElseIfNode? elseIfNode)
        {
            this.expression = expression;
            this.block = block;
            this.elseIfNode = elseIfNode;
        }

        public string CodeGen (int indentation)
		{
            if (expression == null)
                throw new NotImplementedException();
            if (block == null)
                throw new NotImplementedException();
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            
            if (elseIfNode != null) {
                result.Append(indent);
            }
            result.Append("if (");
            result.Append(expression.CodeGen(indentation));
            result.Append(")");
            result.Append(block.CodeGen(indentation));

            if (elseIfNode != null) {
                result.Append(indent);
                result.Append("else ");
                result.Append(elseIfNode.CodeGen(indentation));
            }
            return result.ToString();
        }

	}
}


