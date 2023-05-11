using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
	public class ElseIfNode : AbstractNode
	{
        private IfNode? ifNode;
        private BlockNode? block;

        public ElseIfNode(IfNode? ifNode, BlockNode? block)
        {
            this.ifNode = ifNode;
            this.block = block;
        }

        public string CodeGen (int indentation)
		{
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";

            if (ifNode != null) {
                result.Append(ifNode.CodeGen(indentation));
            } else if (block != null) {
                result.Append(block.CodeGen(indentation));
            } else {
                throw new NotImplementedException();
            }
            return result.ToString();
        }

	}
}


