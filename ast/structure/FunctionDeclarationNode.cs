using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionDeclarationNode : AbstractNode
    {
        public TypeNode Type {get; private set;}
        public string Identifier {get; private set;}
        public List<FunctionParamNode> funcParams {get; private set;} = new List<FunctionParamNode>();
        public BlockNode? body {get; private set;}

        public FunctionDeclarationNode(TypeNode type, string identifier, List<FunctionParamNode> funcParams, BlockNode? body)
        {
            Type = type;
            Identifier = identifier;
            this.funcParams = funcParams;
            this.body = body;
        }

    }
}
