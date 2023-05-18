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

        public override string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            
            
            if (mapLayer.Count > 0) {
                foreach (var layer in mapLayer) {
                    result.Append(layer.CodeGen(indentation));
                    result.Append(",");
                }
                result.Length--;
            }
            return result.ToString();
        }
    }
}