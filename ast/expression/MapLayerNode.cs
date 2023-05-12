using System;
using System.Text;

namespace Antlr_language.ast.expression
{
    public class MapLayerNode : AbstractExpressionNode
    {
        
        private List<IndividualLayerNode> mapLayer;

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
            
            
            
            foreach (var layer in mapLayer) {
                result.Append(layer.CodeGen(indentation));
                result.Append(",");
            }
            result.Length--;
            return result.ToString();
        }

        public override Enums.Types getEvaluationType()
        {
            throw new NotImplementedException();
        }
    }
}