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
            for (int i = 0; i < weights.Count; i++) {
                var weight = weights[i];
                result.Append(indent);
                result.AppendLine("_sum += " + weight.CodeGen(indentation + 1) + ";");
                result.Append(indent);
                result.AppendLine("int _sum"+i+" = " + weight.CodeGen(indentation + 1) + ";");
            }
            
            result.Append(indent);
            result.AppendLine("int _chance = rng.getRand(0,_sum);");
            for (int i = 0; i < weights.Count; i++) {
                var weight = weights[i];
                var block = blocks[i];
                result.Append(indent);
                result.Append("if (_chance <_sum"+i+ ")");
                //result.Append(indent);
                result.Append(block.CodeGen(indentation +1));
                result.Append(indent);
                result.AppendLine("_chance = _chance<_sum"+i+"?_sum:_chance-_sum"+i+";");
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
