using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class ForNode : AbstractNode
    {
        public VariableDeclarationNode declaration {get; private set;}
        public ExpressionNode expression {get; private set;}
        public AssignmentNode assignment {get; private set;}
        public BlockNode block {get; private set;}

        public ForNode(VariableDeclarationNode declaration, ExpressionNode expression, AssignmentNode assignment, BlockNode block)
        {
            this.declaration = declaration;
            this.expression = expression;
            this.assignment = assignment;
            this.block = block;
        }

        public string CodeGen(int indentation)
        {

            string result = "for (";
            result += declaration.CodeGen(indentation);
            result += ";";
            result += expression.CodeGen(indentation);
            result += ";";
            result += assignment.CodeGen(indentation);
            result += ")\n";
            result += block.CodeGen(indentation);
            return result;
        }

    }
}
