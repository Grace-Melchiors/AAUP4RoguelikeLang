using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionParamNode : VariableDeclarationNode
    {

        public FunctionParamNode(TypeNode type, string identifier) : base (type, identifier, null)
        {
        }

    }
}
