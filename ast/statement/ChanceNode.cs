using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class ChanceNode : AbstractNode
    {
        private List<ExpressionNode> weights;
        private List<BlockNode> blocks;

        public ChanceNode(List<ExpressionNode> weights, List<BlockNode> blocks)
        {
            this.weights = weights;
            this.blocks = blocks;
        }

        public string CodeGen(int indentation)
        {
            StringBuilder result = new StringBuilder();
            string indent = "";
            for (int i = 0; i < indentation; i++)
                indent +="\t";

            
            result.Append(indent);
            result.AppendLine("{");
            indent +="\t";
            result.Append(indent);
            result.AppendLine("int _sum = 0;");
            foreach (var weight in weights) {
                result.Append(indent);
                result.AppendLine("_sum += " + weight.CodeGen(indentation + 1) + ";");
            }
            
            result.Append(indent);
            result.AppendLine("int _chance = rng.Next(0,_sum);");
            for (int i = 0; i < weights.Count; i++) {
                var weight = weights[i];
                var block = blocks[i];
                result.Append(indent);
                result.Append("if (_chance <" + weight.CodeGen(indentation + 1) + ")");
                //result.Append(indent);
                result.Append(block.CodeGen(indentation +1));
                result.Append(indent);
                result.AppendLine("else {_chance -= " + weight.CodeGen(indentation +1) + ";}");
            }
            indent = "";
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.AppendLine("}");

            return result.ToString();
        }

    }
}
