using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionDeclarationNode : AbstractNode
    {
        private Enums.Types Type;
        private string Identifier;
        private List<FunctionParamNode> funcParams;
        private BlockNode body;


        public string CodeGen()
        {
            string result = "";
            if (Type == Enums.Types.MAP) {
                result += "Map ";
            } else if (Type == Enums.Types.INTEGER) {
                result += "int ";
            } else if (Type == Enums.Types.BOOL) {
                result += "bool ";
            } else {
                throw new NotImplementedException();
            }
            result += Identifier + " ";
            result += "(";
            foreach (FunctionParamNode param in funcParams) {
                result += param.CodeGen() + funcParams;
            }
            result += ")\n";
            result += body.CodeGen();
            return result;
        }

    }
}
