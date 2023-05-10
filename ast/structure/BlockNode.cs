using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class BlockNode : AbstractNode
    {
        private List<StatementNode> statementNodes = new List<StatementNode>();

        public BlockNode(List<StatementNode> statementNodes)
        {
            this.statementNodes = statementNodes;
        }

        public string CodeGen(int indentation)
        {
            string result = "";
            foreach (StatementNode statement in statementNodes) {
                for (int i = 0; i < indentation; i++)
                    result +="\t";
                result += statement.CodeGen(indentation + 1);
            }
            return result;
        }

    }
}
