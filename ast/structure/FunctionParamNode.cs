using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionParamNode : AbstractNode
    {
        private TypeNode Type;
        private string? Identifier;


        public string CodeGen(int indentation)
        {
            string result = "";
            result += Type.CodeGen(indentation) + " ";
            result += Identifier;
            return result;
            
        }

    }
}
