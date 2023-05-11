using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class StatementsNode : AbstractNode
    {
        private List<StatementNode> statements;

        public StatementsNode(List<StatementNode> statements)
        {
            this.statements = statements;
        }
        public void AddStatement (StatementNode node) {
            statements.Add(node);
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            
            foreach (StatementNode node in statements) {
                result.Append(node.CodeGen(indentation));
            }


            return result.ToString();
        }

    }
}
