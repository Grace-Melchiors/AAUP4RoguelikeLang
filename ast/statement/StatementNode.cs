using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class StatementNode : AbstractNode
    {
        public VariableDeclarationNode? varDecl;
        public AssignmentNode? assign;
        public ExpressionNode? expression;
        public ReturnStatementNode? returnStatement;
        public BlockNode? block;
        public IfNode? ifNode;
        public WhileNode? whileNode;
        public ChanceNode? chance;

        //Not in Grammar
        private StatementsNode? statements;

        public StatementNode(VariableDeclarationNode? varDecl, AssignmentNode? assign, ExpressionNode? expression, ReturnStatementNode? returnStatement, BlockNode? block, IfNode? ifNode, WhileNode? whileNode, ChanceNode? chance, StatementsNode? statements)
        {
            this.varDecl = varDecl;
            this.assign = assign;
            this.expression = expression;
            this.returnStatement = returnStatement;
            this.block = block;
            this.ifNode = ifNode;
            this.whileNode = whileNode;
            this.chance = chance;
            this.statements = statements;
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            if (varDecl != null) {
                result.Append(varDecl.CodeGen(indentation) + ";");
            } else if (assign != null) {
                result.Append(assign.CodeGen(indentation) + ";");
            } else if (expression != null) {
                result.Append(expression.CodeGen(indentation) + ";");
            } else if (returnStatement != null) {
                result.Append(returnStatement.CodeGen(indentation));
            } else if (block != null) {
                result.Append(block.CodeGen(indentation));
            } else if (ifNode != null) {
                result.Append(ifNode.CodeGen(indentation));
            } else if (whileNode != null) {
                result.Append(whileNode.CodeGen(indentation));
            } else if (chance != null) {
                result.Append(chance.CodeGen(indentation));
            } else {
                throw new NotImplementedException();
            }
            return result.ToString();
        }

    }
}
