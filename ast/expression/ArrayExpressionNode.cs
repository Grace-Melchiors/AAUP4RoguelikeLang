using System;
using System.Text;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.expression
{
    public class ArrayExpressionNode : AbstractExpressionNode
    {

        public List<ExpressionNode> expressions {get; private set;}

        public ArrayExpressionNode(List<ExpressionNode> expressions)
        {
            this.expressions = expressions;
        }

        //int[] i = {1,2,3};

        public override string CodeGen(int indentation)
        {
            string result = "";
            if (type == null)
                throw new NotImplementedException("Mising type information, Need to write new {type} [size, size]");
            result += "new " + type.CodeGen(indentation, true) + "{";
            foreach (var expression in expressions)
                result += expression.CodeGen(indentation) + ",";
            result = result.Substring(0, result.Length-1);
            result += "}";

            return result;
        }
        
        public List<ExpressionNode> GetExpressions() {
            return expressions;
        }

    }
}
