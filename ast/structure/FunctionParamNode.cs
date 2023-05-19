using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionParamNode : AbstractNode
    {
        public TypeNode Type {get; private set;}
        public string Identifier {get; private set;}

        public FunctionParamNode(TypeNode type, string identifier)
        {
            Type = type;
            Identifier = identifier;
        }

    }
}
