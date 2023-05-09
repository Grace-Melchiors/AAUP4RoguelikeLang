using System;
using Antlr_language.ast.expression;

namespace Antlr_language.ast.structure
{
    public class BlockNode : AbstractNode
    {
        private List<StatementNode> statementNodes;
        public string CodeGen()
        {
            foreach (StatementNode statement in statementNodes) {
                statement.CodeGen();
            }
        }

    }
}
