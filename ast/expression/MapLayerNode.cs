using System;
using System.Text;

namespace Antlr_language.ast.expression
{
    public class MapLayerNode : AbstractExpressionNode
    {
        
        public List<IndividualLayerNode> mapLayer {get; private set;}

        public MapLayerNode(List<IndividualLayerNode> mapLayer)
        {
            this.mapLayer = mapLayer;
        }
    }
}