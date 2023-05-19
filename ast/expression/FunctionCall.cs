using System;
using System.Text;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.expression
{
    public class FunctionCallNode : AbstractNode
    {
        public string? LIBRARY {get; private set;}
        public string IDENTIFIER {get; private set;}
        public List<ExpressionNode>? parameters {get; private set;}


        public FunctionCallNode(string? lIBRARY, string iDENTIFIER, List<ExpressionNode>? parameters)
        {
            LIBRARY = lIBRARY;
            IDENTIFIER = iDENTIFIER;
            this.parameters = parameters;
        }

    }
}
