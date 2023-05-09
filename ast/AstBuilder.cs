using System;


namespace Antlr_language.ast
{
    public class AstBuilder : VestaBaseVisitor<object?>
    {
        public override object? VisitBlock(VestaParser.BlockContext context)
        {

            return null;
        }
    }
}


