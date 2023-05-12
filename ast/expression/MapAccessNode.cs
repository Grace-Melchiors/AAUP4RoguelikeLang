using System;

namespace Antlr_language.ast.expression
{
    public class MapAccessNode : AbstractExpressionNode
    {

        private Factor2Node factor2;
        private string IDENTIFIER;
        private ArrayDimensionsNode arrayDimensions;

        public MapAccessNode(Factor2Node factor2, string iDENTIFIER, ArrayDimensionsNode arrayDimensions)
        {
            this.factor2 = factor2;
            IDENTIFIER = iDENTIFIER;
            this.arrayDimensions = arrayDimensions;
        }

        public override string CodeGen(int indentation)
        {
            throw new NotImplementedException();
        }
        public override Enums.Types getEvaluationType () {
            throw new NotImplementedException();
        }

    }
}
