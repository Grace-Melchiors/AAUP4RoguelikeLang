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
        public ForNode? forNode;
        public ChanceNode? chance;

        //Not in Grammar
        public StatementsNode? statements;

        public VariableDeclarationNode? GetVarDeclNode() {
            return varDecl;
        }
        
        public AssignmentNode? GetAssignmentNode() {
            return assign;
        }

        public StatementNode(VariableDeclarationNode? varDecl, AssignmentNode? assign, ExpressionNode? expression, ReturnStatementNode? returnStatement, BlockNode? block, IfNode? ifNode, WhileNode? whileNode, ForNode? forNode, ChanceNode? chance, StatementsNode? statements)
        {
            this.varDecl = varDecl;
            this.assign = assign;
            this.expression = expression;
            this.returnStatement = returnStatement;
            this.block = block;
            this.ifNode = ifNode;
            this.whileNode = whileNode;
            this.forNode = forNode;
            this.chance = chance;
            this.statements = statements;
        }

    }
}
