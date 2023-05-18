using System;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.expression
{
    abstract public class AbstractExpressionNode : AbstractNode
    {
        public TypeNode? type;
        abstract public string CodeGen(int indentation);
    }
}
