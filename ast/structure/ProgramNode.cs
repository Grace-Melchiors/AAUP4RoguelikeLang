using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class ProgramNode : AbstractNode
    {
        private string nameSpace = "MapGen";
        private List<LibraryNode> libraryNodes = new List<LibraryNode>();
        private List<LineNode> lineNodes = new List<LineNode>();
        
        public List<LineNode> retrieveLineNodes() {
            return lineNodes;
        }
        public void AddLineNode (LineNode node) {
            lineNodes.Add(node);
        }
        public void AddLibraryNode (LibraryNode node) {
            libraryNodes.Add(node);
        }
        public string CodeGen(int indentation)
        {
            indentation += 2;
            string result = "using System;\n\n";
            result += "namespace " + nameSpace + "\n";
            result += "{\n";
            result += "\tclass Map {\n";
            result += "\t\tDictionary<string, int[][]> IntLayers;\n";
            result += "\t\tDictionary<string, bool[][]> BoolLayers;\n";
            result += "\t\tpublic int d1Size;\n";
            result += "\t\tpublic int d2Size;\n";
            result += "\t\tpublic Map(int d1Size, int d2Size, Dictionary<string, int[][]> IntLayers, Dictionary<string, bool[][]> BoolLayers)\n";
            result += "\t\t{\n";
            result += "\t\t\tthis.IntLayers = IntLayers;";
			result += "\t\t\tthis.BoolLayers = BoolLayers;";
            result += "\t\t\tthis.d1Size = d1Size;\n";
            result += "\t\t\tthis.d2Size = d2Size;\n";
            result += "\t\t}\n";
            result += "\t}\n";
            result += "\n";
            foreach (LibraryNode library in libraryNodes) {
                result += library.CodeGen(indentation - 1) + "\n";
            }
            result += "\tclass Program\n";
            result += "\t{\n";
            result += "\t\tstatic void Main(string[] args)\n";
            result += "\t\t{\n";
            foreach (LineNode line in lineNodes) {
                //for (int i = 0; i < indentation; i++)
                    //result +="\t";
                result += line.CodeGen(indentation + 1) + "\n";
            }
            result += "\t\t}\n";
            result += "\t}\n";
            result += "}\n";
            return result;
        }

    }
}
