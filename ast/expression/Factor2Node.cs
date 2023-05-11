using System;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.expression
{
    public class Factor2Node : AbstractExpressionNode
    {
        
        private string? identifier;
        private FunctionCallNode? functionCall;

        public Factor2Node(string? identifier, FunctionCallNode? functionCall)
        {
            this.identifier = identifier;
            this.functionCall = functionCall;
        }

        public override string CodeGen(int indentation)
        {
            if (functionCall != null) {
                return functionCall.CodeGen(indentation);
            } else if (identifier != null) {
                return identifier;
            } else {
                throw new NotImplementedException();
            }
        }
        public override Enums.Types getEvaluationType () {
            throw new NotImplementedException();
        }
        
        public string? GetIdentifier() {
            return identifier;
        }
    }
}
