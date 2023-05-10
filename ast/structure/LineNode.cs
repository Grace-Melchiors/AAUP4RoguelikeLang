using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class LineNode : AbstractNode
    {
        private StatementNode? statement;
        private FunctionDeclarationNode? funcDecl;

        public LineNode(StatementNode? statement, FunctionDeclarationNode? funcDecl)
        {
            this.statement = statement;
            this.funcDecl = funcDecl;
        }

        public string CodeGen(int indentation)
        {
            if (statement != null) {
                return statement.CodeGen(indentation);
            } else if (funcDecl != null) {
                return funcDecl.CodeGen(indentation);
            }
            else {
                throw new NotImplementedException();
            }
        }

    }
}
