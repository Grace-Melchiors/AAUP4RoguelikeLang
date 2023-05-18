using System;
using System.Text;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.expression
{
    public class MapAccessNode : AbstractExpressionNode
    {

        public Factor2Node factor2 {get; private set;}
        public string IDENTIFIER {get; private set;}
        public ArrayDimensionsNode? arrayDimensions {get; private set;}
        public TypeNode? layerType;

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

    }
}
