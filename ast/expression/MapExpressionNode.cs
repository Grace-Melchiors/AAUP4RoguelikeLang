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
    }
}