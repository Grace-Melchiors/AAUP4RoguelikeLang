using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class StatementNode : AbstractNode
    {
        private VariableDeclarationNode? varDecl;
        private AssignmentNode? assign;
        private ExpressionNode? expression;
        private ReturnStatementNode? returnStatement;
        private BlockNode? block;
        private IfNode? ifNode;
        private WhileNode? whileNode;
        private ChanceNode? chance;

        public StatementNode(VariableDeclarationNode? varDecl, AssignmentNode? assign, ExpressionNode? expression, ReturnStatementNode? returnStatement, BlockNode? block, IfNode? ifNode, WhileNode? whileNode, ChanceNode? chance)
        {
            this.varDecl = varDecl;
            this.assign = assign;
            this.expression = expression;
            this.returnStatement = returnStatement;
            this.block = block;
            this.ifNode = ifNode;
            this.whileNode = whileNode;
            this.chance = chance;
        }

        public string CodeGen(int indentation)
        {
            if (varDecl != null) {
                return varDecl.CodeGen(indentation) + ";";
            } else if (assign != null) {
                return assign.CodeGen(indentation) + ";";
            } else if (expression != null) {
                return expression.CodeGen(indentation) + ";";
            } else if (block != null) {
                return block.CodeGen(indentation);
            } else if (ifNode != null) {
                return ifNode.CodeGen(indentation);
            } else if (whileNode != null) {
                return whileNode.CodeGen(indentation);
            } else if (chance != null) {
                return chance.CodeGen(indentation);
            } else {
                throw new NotImplementedException();
            }
        }

    }
}
