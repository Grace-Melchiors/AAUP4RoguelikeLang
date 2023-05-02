using System;
using Antlr_language.ast.expression;

namespace Antlr_language.ast.statement
{
    public class StatementNode : AbstractNode
    {
        private DeclarationNode decl;
        private AssignmentNode assign;
        private FunctionCallNode func;

        public string CodeGen()
        {
            if (decl != null) {
                return decl.CodeGen();
            }
            else if (assign != null) {
                return assign.CodeGen();
            }
            else if (func != null) {
                return func.CodeGen();
            }
            else {
                throw new NotImplementedException();
            }
        }

    }
}
