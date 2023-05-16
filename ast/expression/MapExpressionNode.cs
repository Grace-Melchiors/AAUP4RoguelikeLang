using System;
using System.Text;

namespace Antlr_language.ast.expression
{
    public class MapExpressionNode : AbstractExpressionNode
    {
        public ArrayDimensionsNode arrayDimensions {get; private set;}
        public MapLayerNode mapLayer {get; private set;}
        //private size? No can't get the size at compile time.

        public MapExpressionNode(ArrayDimensionsNode arrayDimensions, MapLayerNode mapLayer)
        {
            this.arrayDimensions = arrayDimensions;
            this.mapLayer = mapLayer;
        }

        public override string CodeGen(int indentation)
        {
            //Map tempMap = new Map();
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";

            result.Append("new Map(" + arrayDimensions.CodeGen(indentation).Replace("[", "").Replace("]","") + ",");
            result.Append(mapLayer.CodeGen(indentation));
            result.Append(")");

            return result.ToString();
        }

        public override Enums.Types getEvaluationType()
        {
            throw new NotImplementedException();
        }
    }
}