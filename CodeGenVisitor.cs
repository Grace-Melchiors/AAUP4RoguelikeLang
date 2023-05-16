using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;
using Antlr_language.ast;
using Antlr_language.Content;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Antlr_language
{
    public class CodeGenVisitor : AstBaseVisitorBuilder<string>
    {
        public override string? Visit(ProgramNode context) {
            System.Console.WriteLine("I am in Program");
            return base.Visit(context);
        }
        public override string? Visit(LineNode context) {
            System.Console.WriteLine("I am in Line");
            return base.Visit(context);
        }
        public override string? Visit(ConstantNode context)
        {
            string result;
            if (context.boolean != null) {
                result = context.boolean == true ? "true" : "false";
            } else if (context.integer != null) {
                result = context.integer.ToString() ?? "0";
            } else {
                throw new NotImplementedException();
            }
            System.Console.WriteLine(result);
            return result;
            //return base.Visit(context);
        }
    }
}