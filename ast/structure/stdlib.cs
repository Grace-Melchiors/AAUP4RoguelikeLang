using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class Stdlib : LibraryNode
    {
        public override string LibraryName {get; set;}
        public override string CodeGen(int indentation) {
            string indent = "";
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            string result = "";
            result += indent + "static class stdlib {\n";
            result += indent + "\tpublic static void print (string val) {\n";
            result += indent + "\t\tSystem.Console.WriteLine(val);\n";
            result += indent + "\t}\n";
            result += indent + "}\n";

            return result;
        }
    }
}