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
            result += indent + "\t\tfor (int i = 0; i < val.GetLength(1); i++) {";
            result += indent + "\t\tSystem.Console.Write(\"[\");";
            result += indent + "\t\tfor (int j = 0; j < val.GetLength(0); j++) {";
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
            result += indent + "\t\tfor (int i = 0; i < val.GetLength(1); i++) {";
            result += indent + "\t\tSystem.Console.Write(\"[\");";
            result += indent + "\t\tfor (int j = 0; j < val.GetLength(0); j++) {";
            result += indent + "\t\t\tSystem.Console.Write(val[j,i] + \",\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t\t}";
            result += indent + "\t\tSystem.Console.WriteLine(\"]\");";
            result += indent + "\t}\n";
            result += indent + "}\n";

            //Random
            /*private static MLCGRandom rng = new MLCGRandom();
            public static int random () {
                return (int)(rng.getNext() % (long)int.MaxValue);
            }
            public static int random (int min, int max) {
                return rng.getRand(min, max);
            }
            public static void seed (int seed) {
                rng = new MLCGRandom(seed);
            }
            private class MLCGRandom
            {
                //private int a= 1664525; //Knuth
                private  int a = 69069; //Marsagli
                private int c = 1;
                private long m = (long)Math.Pow(2,32);
                private long seed = 0;

                public MLCGRandom()
                { seed = (long)(DateTime.Now.Ticks % m); }
                public MLCGRandom(long seed)
                { this.seed = seed; }

                public long getNext()
                {
                    seed = (a * seed + 1) % m;
                    return seed;
                }
                public int getRand(int min, int max)
                {
                    if (max < min) throw new Exception($"Cannot generate random value where max is more than min");
                    return (int)(min + getNext() % (max+1 - min));
                }
            }*/

            return result;
        }
    }
}