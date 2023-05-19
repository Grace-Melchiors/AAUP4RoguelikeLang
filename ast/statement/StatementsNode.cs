using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class StatementsNode : AbstractNode
    {
        public List<StatementNode> statements {get;private set;}

        public StatementsNode(List<StatementNode> statements)
        {
            this.statements = statements;
        }
        public void AddStatement (StatementNode node) {
            statements.Add(node);
        }

    }
}
