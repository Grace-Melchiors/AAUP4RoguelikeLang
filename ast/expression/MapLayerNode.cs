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
            
            List<IndividualLayerNode> intLayers = new List<IndividualLayerNode>();
            List<IndividualLayerNode> boolLayers = new List<IndividualLayerNode>();
            foreach (IndividualLayerNode layer in mapLayer) {
                if (layer.GetNodeType() == Enums.Types.INTEGER) {
                    intLayers.Add(layer);
                } else if (layer.GetNodeType() == Enums.Types.INTEGER) {
                    boolLayers.Add(layer);
                } else {
                    throw new NotSupportedException("Layers of type: " + layer.GetNodeType() + " are not supported.");
                }
            }
            result.Append("new Dictionary<string, int[][]> {");
            foreach (var layer in intLayers) {
                layer.CodeGen(indentation);
            }
            result.Length--;
            result.Append("}, Dictionary<string, bool[][]> {");
            foreach (var layer in boolLayers) {
                layer.CodeGen(indentation);
            }
            result.Length--;
            result.Append("}");
            return result.ToString();
        }

        public override Enums.Types getEvaluationType()
        {
            throw new NotImplementedException();
        }
    }
}