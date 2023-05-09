using System;
using Antlr_language.ast.expression;

namespace Antlr_language.ast.statement
{
    public class DeclarationNode : AbstractNode
    {
        private Enums.Types type;
        private string identifier;
        
        public string CodeGen()
        {
            return "";
        }

    }
}
