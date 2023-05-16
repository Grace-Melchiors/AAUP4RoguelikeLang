using System;
using System.Text;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.expression
{
    public class MapAccessNode : AbstractExpressionNode
    {

        private Factor2Node factor2;
        private string IDENTIFIER;
        private ArrayDimensionsNode? arrayDimensions;
        public TypeNode layerType = new TypeNode(Enums.Types.INTEGER, null);

        public MapAccessNode(Factor2Node factor2, string iDENTIFIER, ArrayDimensionsNode arrayDimensions)
        {
            this.factor2 = factor2;
            IDENTIFIER = iDENTIFIER;
            this.arrayDimensions = arrayDimensions;
        }

        public override string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append("(" + layerType.CodeGen(indentation) + ")");
            result.Append(factor2.CodeGen(indentation) + ".layers");
            result.Append("[\"" + IDENTIFIER + "\"]");
            result.Append(".LayerValue");
            if (arrayDimensions != null)
                result.Append(arrayDimensions.CodeGen(indentation));

            return result.ToString();
        }
        public override Enums.Types getEvaluationType () {
            throw new NotImplementedException();
        }

    }
}
