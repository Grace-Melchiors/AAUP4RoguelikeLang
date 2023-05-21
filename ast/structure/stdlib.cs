using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class Stdlib : LibraryNode
    {
        public override string LibraryName {get; set;}
        public override string Content { get; set; }
        public Stdlib () {
            LibraryName = "Stdlib";

            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 2; i++)
                indent +="\t";
            result.AppendLine(indent + "static class Stdlib {\n");
            result.AppendLine(indent + "\tpublic static void print (int val) {\n");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(val);\n");
            result.AppendLine(indent + "\t}\n");

            result.AppendLine(indent + "\tpublic static void print (int[] val) {\n");
            result.AppendLine(indent + "\t\tSystem.Console.Write(\"[\");");
            result.AppendLine(indent + "\t\tfor (int i = 0; i < val.GetLength(0); i++) {");
            result.AppendLine(indent + "\t\t\tSystem.Console.Write(val[i] + \",\");");
            result.AppendLine(indent + "\t\t}");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"]\");");
            result.AppendLine(indent + "\t}\n");

            result.AppendLine(indent + "\tpublic static void print (int[,] val) {\n");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"[\");");
            result.AppendLine(indent + "\t\tfor (int i = 0; i < val.GetLength(1); i++) {");
            result.AppendLine(indent + "\t\tSystem.Console.Write(\"[\");");
            result.AppendLine(indent + "\t\tfor (int j = 0; j < val.GetLength(0); j++) {");
            result.AppendLine(indent + "\t\t\tSystem.Console.Write(val[j,i] + \",\");");
            result.AppendLine(indent + "\t\t}");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"]\");");
            result.AppendLine(indent + "\t\t}");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"]\");");
            result.AppendLine(indent + "\t}\n");


            result.AppendLine(indent + "\tpublic static void print (bool val) {\n");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(val);\n");
            result.AppendLine(indent + "\t}\n");

            result.AppendLine(indent + "\tpublic static void print (bool[] val) {\n");
            result.AppendLine(indent + "\t\tSystem.Console.Write(\"[\");");
            result.AppendLine(indent + "\t\tfor (int i = 0; i < val.GetLength(0); i++) {");
            result.AppendLine(indent + "\t\t\tSystem.Console.Write(val[i] + \",\");");
            result.AppendLine(indent + "\t\t}");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"]\");");
            result.AppendLine(indent + "\t}\n");

            result.AppendLine(indent + "\tpublic static void print (bool[,] val) {\n");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"[\");");
            result.AppendLine(indent + "\t\tfor (int i = 0; i < val.GetLength(1); i++) {");
            result.AppendLine(indent + "\t\tSystem.Console.Write(\"[\");");
            result.AppendLine(indent + "\t\tfor (int j = 0; j < val.GetLength(0); j++) {");
            result.AppendLine(indent + "\t\t\tSystem.Console.Write(val[j,i] + \",\");");
            result.AppendLine(indent + "\t\t}");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"]\");");
            result.AppendLine(indent + "\t\t}");
            result.AppendLine(indent + "\t\tSystem.Console.WriteLine(\"]\");");
            result.AppendLine(indent + "\t}\n");

            //Random
            result.AppendLine(indent + "\tpublic static int random () {");
            result.AppendLine(indent + "\t    return (int)(rng.getNext() % (long)int.MaxValue);");
            result.AppendLine(indent + "\t}");
            result.AppendLine(indent + "\tpublic static int random (int min, int max) {");
            result.AppendLine(indent + "\t    return rng.getRand(min, max);");
            result.AppendLine(indent + "\t}");
            result.AppendLine(indent + "\tpublic static void seed (int seed) {");
            result.AppendLine(indent + "\t    rng = new MLCGRandom(seed);");
            result.AppendLine(indent + "\t}");
            

            result.AppendLine(indent + "}\n");
            
            Content = result.ToString();
        }
    }
}