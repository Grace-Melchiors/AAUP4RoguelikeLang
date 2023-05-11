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
            result += indent + "static class Stdlib {\n";
            result += indent + "\tpublic static void print (int val) {\n";
            result += indent + "\t\tSystem.Console.WriteLine(val);\n";
            result += indent + "\t}\n";

            result += indent + "\tpublic static void print (int[] val) {\n";
            result += indent + "\t\tSystem.Console.Write(\"[\");";
            result += indent + "\t\tfor (int i = 0; i < val.GetLength(0); i++) {";
            result += indent + "\t\t\tSystem.Console.Write(val[i] + \",\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t}\n";

            result += indent + "\tpublic static void print (int[,] val) {\n";
            result += indent + "\t\tSystem.Console.WriteLine(\"[\");";
            result += indent + "\t\tfor (int i = 0; i < val.GetLength(0); i++) {";
            result += indent + "\t\tSystem.Console.Write(\"[\");";
            result += indent + "\t\tfor (int j = 0; j < val.GetLength(1); j++) {";
            result += indent + "\t\t\tSystem.Console.Write(val[j,i] + \",\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t}\n";


            result += indent + "\tpublic static void print (bool val) {\n";
            result += indent + "\t\tSystem.Console.WriteLine(val);\n";
            result += indent + "\t}\n";

            result += indent + "\tpublic static void print (bool[] val) {\n";
            result += indent + "\t\tSystem.Console.Write(\"[\");";
            result += indent + "\t\tfor (int i = 0; i < val.GetLength(0); i++) {";
            result += indent + "\t\t\tSystem.Console.Write(val[i] + \",\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t}\n";

            result += indent + "\tpublic static void print (bool[,] val) {\n";
            result += indent + "\t\tSystem.Console.WriteLine(\"[\");";
            result += indent + "\t\tfor (int i = 0; i < val.GetLength(0); i++) {";
            result += indent + "\t\tSystem.Console.Write(\"[\");";
            result += indent + "\t\tfor (int j = 0; j < val.GetLength(1); j++) {";
            result += indent + "\t\t\tSystem.Console.Write(val[j,i] + \",\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t}\n";
            result += indent + "}\n";

            return result;
        }
    }
}