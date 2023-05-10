using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class ProgramNode : AbstractNode
    {
        private string nameSpace = "MapGen";
        private List<LineNode> lineNodes = new List<LineNode>();
        
        public List<LineNode> retrieveLineNodes() {
            return lineNodes;
        }
        public void AddLineNode (LineNode node) {
            lineNodes.Add(node);
        }

        public string CodeGen(int indentation)
        {
            indentation += 3;
            string result = "using System;\n\n";
            result += "namespace " + nameSpace + "\n";
            result += "{\n";
            result += "\tclass Map {\n";
            result += "\t\tDictionary<string, int[][]> IntLayers = new();\n";
            result += "\t\tDictionary<string, bool[][]> BoolLayers = new();\n";
            result += "\t}\n";
            result += "\n";
            result += "\tclass Program\n";
            result += "\t{\n";
            result += "\t\tstatic void Main(string[] args)\n";
            result += "\t\t{\n";
            foreach (LineNode line in lineNodes) {
                for (int i = 0; i < indentation; i++)
                    result +="\t";
                result += line.CodeGen(indentation + 1) + "\n";
            }
            result += "\t\t}\n";
            result += "\t}\n";
            result += "}\n";
            return result;
        }

    }
}
