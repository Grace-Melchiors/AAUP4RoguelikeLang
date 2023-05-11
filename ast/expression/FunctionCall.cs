using System;
using System.Text;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.expression
{
    public class FunctionCallNode : AbstractNode
    {
        private string? LIBRARY;
        private string IDENTIFIER;
        private List<ExpressionNode>? parameters;


        public FunctionCallNode(string? lIBRARY, string iDENTIFIER, List<ExpressionNode>? parameters)
        {
            LIBRARY = lIBRARY;
            IDENTIFIER = iDENTIFIER;
            this.parameters = parameters;
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append(indent);
            if (LIBRARY != null) {
                result.Append(LIBRARY);
                result.Append(".");
            }
            result.Append(IDENTIFIER);
            result.Append("(");
            if (parameters != null && parameters.Count > 0) {
                foreach (ExpressionNode param in parameters)
                    result.Append(param.CodeGen(0) + ",");
                result.Length--;
            }
            result.Append(")");
            return result.ToString();
        }

    }
}
