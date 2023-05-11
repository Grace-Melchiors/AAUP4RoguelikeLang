using System;
using System.Text;
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
        public void AddStatement (StatementNode node) {
            statementNodes.Add(node);
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.AppendLine("{");
            foreach (StatementNode statement in statementNodes) {
                //for (int i = 0; i < indentation; i++)
                    //result +="\t";
                result.AppendLine(statement.CodeGen(indentation + 1));
            }
            result.Append(indent);
            result.AppendLine("}");
            return result.ToString();
        }

    }
}
