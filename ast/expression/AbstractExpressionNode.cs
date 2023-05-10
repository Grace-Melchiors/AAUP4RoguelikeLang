using System;

namespace Antlr_language.ast.expression
{
    abstract public class AbstractExpressionNode : AbstractNode
    {
        abstract public Enums.Types getEvaluationType ();
        abstract public string CodeGen(int indentation);
    }
}
