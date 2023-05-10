using System;

namespace Antlr_language.ast.expression
{
    public class MapAccessNode : AbstractExpressionNode
    {

        private string? VariableName;
        private string? LayerName;
        private List<ExpressionNode> indexs = new List<ExpressionNode>();
        

        public override string CodeGen(int indentation)
        {
            string result;
            if (indexs.Count() != 0) {
                if (VariableName == null)
                    throw new NotImplementedException();
                result = VariableName + "." + LayerName + "[";
                foreach (ExpressionNode expr in indexs) {
                    result += expr.CodeGen(indentation) + ",";
                }
                //Remove the last ",".
                result = result.Substring(0, result.Length-2);
                result += "]";
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override Enums.Types getEvaluationType () {
            throw new NotImplementedException();
        }

    }
}
