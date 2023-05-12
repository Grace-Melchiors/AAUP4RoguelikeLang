using System;

namespace Antlr_language.ast.expression
{
    public class ArrayExpressionNode : AbstractExpressionNode
    {

        private List<ExpressionNode> expressions;

        public ArrayExpressionNode(List<ExpressionNode> expressions)
        {
            this.expressions = expressions;
        }

        //int[] i = {1,2,3};

        public override string CodeGen(int indentation)
        {
            string result = "";
            throw new NotImplementedException("Need to write new {type} [size, size]");
            result += "{";
            foreach (var expression in expressions)
                result += expression.CodeGen(indentation) + ",";
            result = result.Substring(0, result.Length-1);
            result += "}";

            return result;
        }
        public override Enums.Types getEvaluationType () {
            throw new NotImplementedException();
        }

    }
}
