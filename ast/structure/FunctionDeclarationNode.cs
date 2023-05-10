using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionDeclarationNode : AbstractNode
    {
        private TypeNode Type;
        private string? Identifier;
        private List<FunctionParamNode> funcParams = new List<FunctionParamNode>();
        private BlockNode? body;

        public FunctionDeclarationNode(TypeNode type, string? identifier, List<FunctionParamNode> funcParams, BlockNode? body)
        {
            Type = type;
            Identifier = identifier;
            this.funcParams = funcParams;
            this.body = body;
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            for (int i = 0; i < indentation; i++) {
                indent += "\t";
            }
            string result = indent;
            result += Type.CodeGen(indentation) + " ";
            result += Identifier + " ";
            result += "(";
            foreach (FunctionParamNode param in funcParams) {
                result += param.CodeGen(indentation) + funcParams;
            }
            result += ")\n";
            if (body == null)
                throw new NotImplementedException();
            result += body.CodeGen(indentation);
            return result;
        }

    }
}
