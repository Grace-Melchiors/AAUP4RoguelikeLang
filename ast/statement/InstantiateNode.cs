using System;
using Antlr_language.ast.expression;

namespace Antlr_language.ast.statement
{
    public class InstantiateNode : AbstractNode
    {
        private Enums.Types type;
        private string? identifier;
        
        public string CodeGen(int indentation)
        {
            return "";
        }

    }
}
