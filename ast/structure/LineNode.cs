using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class LineNode : AbstractNode
    {
        public StatementNode? statement {get; private set;}
        public FunctionDeclarationNode? funcDecl {get; private set;}

        public StatementNode GetStatementNode() {
            return statement;

        }
        
        public FunctionDeclarationNode GetFunctionDeclarationNode() {
            return funcDecl;
            
        }

        public bool IsStatement() {
            return statement != null;
        }

        public LineNode(StatementNode? statement, FunctionDeclarationNode? funcDecl)
        {
            this.statement = statement;
            this.funcDecl = funcDecl;
        }

    }
}
