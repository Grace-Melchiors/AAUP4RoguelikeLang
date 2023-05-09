using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class LineNode : AbstractNode
    {
        private StatementNode? statement;
        private FunctionDeclarationNode? funcDecl;

        public string CodeGen()
        {
            if (statement != null) {
                return statement.CodeGen();
            } else if (funcDecl != null) {
                return funcDecl.CodeGen();
            }
            else {
                throw new NotImplementedException();
            }
        }

    }
}
