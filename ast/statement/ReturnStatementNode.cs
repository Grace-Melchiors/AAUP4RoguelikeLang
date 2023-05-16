using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class ReturnStatementNode : AbstractNode
    {
        public ExpressionNode? expression {get; private set;}

        public ReturnStatementNode(ExpressionNode? expression)
        {
            this.expression = expression;
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.Append("return ");
            if (expression != null) {
                result.Append(expression.CodeGen(indentation) + ";");
            } else {
                throw new NotImplementedException();
            }
            return result.ToString();
        }

    }
}
