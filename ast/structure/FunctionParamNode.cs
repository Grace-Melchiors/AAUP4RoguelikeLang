using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionParamNode : AbstractNode
    {
        private Enums.Types Type;
        private string? Identifier;


        public string CodeGen(int indentation)
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
            result += Identifier;
            return result;
            
        }

    }
}
