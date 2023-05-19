using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class BlockNode : AbstractNode
    {
        public List<StatementNode> statementNodes {get; private set; } = new List<StatementNode>();

        public BlockNode(List<StatementNode> statementNodes)
        {
            this.statementNodes = statementNodes;
        }
        public void AddStatement (StatementNode node) {
            statementNodes.Add(node);
        }
    }
}
