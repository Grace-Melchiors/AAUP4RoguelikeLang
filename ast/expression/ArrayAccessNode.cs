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


        public override string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";

            result.Append(factor2.CodeGen(indentation));
            result.Append(indicies.CodeGen(indentation));

            return result.ToString();
        }
        /*public override string CodeGen(int indentation)
        {
            string result;
            if (indexs.Count() != 0) {
                if (VariableName == null)
                    throw new NotImplementedException();
                result = VariableName + "[";
                foreach (ExpressionNode expr in indexs) {
                    result += expr.CodeGen(indentation) + ",";
                }
                //Remove the last ",".
                result = result.Substring(0, result.Length-1);
                result += "]";
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override Enums.Types getEvaluationType () {
            return type;
        }*/

    }
}
