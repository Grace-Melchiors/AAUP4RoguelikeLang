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

    }
}
