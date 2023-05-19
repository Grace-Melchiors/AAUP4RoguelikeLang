using System;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.expression
{
    public class Factor2Node : AbstractExpressionNode
    {
        
        public string? identifier {get; private set;}
        public FunctionCallNode? functionCall { get; private set; }

        public Factor2Node(string? identifier, FunctionCallNode? functionCall)
        {
            this.identifier = identifier;
            this.functionCall = functionCall;
        }
        
        public string? GetIdentifier() {
            return identifier;
        }
    }
}
