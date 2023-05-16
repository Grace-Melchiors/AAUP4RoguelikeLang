using System;
using System.Text;


namespace Antlr_language.ast.expression
{
    public class ArrayDimensionsNode : AbstractExpressionNode
    {
        public List<ExpressionNode> expressions {get; private set;}

        public ArrayDimensionsNode(List<ExpressionNode> expressions)
        {
            this.expressions = expressions;
        }

        public override string CodeGen(int indentation)
        {
           string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append("["); 
            foreach (ExpressionNode size in expressions) {
                result.Append(size.CodeGen(indentation) + ",");
            }
            result.Length--;
            result.Append("]");

            return result.ToString();
        }

        public override Enums.Types getEvaluationType()
        {
            throw new NotImplementedException();
        }
    }
}