using System;
using System.Text;
using Antlr_language.ast.expression;

namespace Antlr_language.ast.statement
{
    public class AssignmentNode : AbstractNode
    {
        private string IDENTIFIER;
        private List<ExpressionNode>? ArrayIndicies;
        private List<int>? ArraySizes;
        private ExpressionNode expression;

        public AssignmentNode(string iDENTIFIER, List<ExpressionNode>? arrayIndicies, ExpressionNode expression)
        {
            IDENTIFIER = iDENTIFIER;
            ArrayIndicies = arrayIndicies;
            this.expression = expression;
        }

        public string GetIdentifier() {
            return IDENTIFIER;
        }
        
        public List<ExpressionNode>? GetArrayIndices() {
            return ArrayIndicies;
        }
        
        public ExpressionNode? GetExpressionNode() {
            return expression;
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append(indent);

            result.Append(IDENTIFIER);
            if (ArrayIndicies != null) {
                result.Append("[");
                if (ArrayIndicies.Count > 0) {
                    foreach(var exp in ArrayIndicies)
                        result.Append(exp.CodeGen(indentation) + ",");
                    result.Length--;
                }
                result.Append("]");
            }
            result.Append(" = ");
            result.Append(expression.CodeGen(indentation));
            
            return result.ToString();
        }
    }
}
