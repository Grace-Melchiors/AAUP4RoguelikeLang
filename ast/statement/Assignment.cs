using System;
using System.Text;
using Antlr_language.ast.expression;

namespace Antlr_language.ast.statement
{
    public class AssignmentNode : AbstractNode
    {
        private string IDENTIFIER;
        private List<ExpressionNode>? ArrayIndicies;

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append(indent);



            throw new NotImplementedException();
        }

    }
}
