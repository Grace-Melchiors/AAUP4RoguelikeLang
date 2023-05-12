using System;
using System.Text;
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
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.AppendLine("using System;\n");
            result.AppendLine("namespace " + nameSpace);
            result.AppendLine("{");
            
            result.AppendLine("class Map {");
            result.AppendLine("    Dictionary<string, MapLayer> layers;");
            result.AppendLine("    public int d1Size;");
            result.AppendLine("    public int d2Size;");
            result.AppendLine("    public Map(int d1Size, int d2Size, params MapLayer[] list)");
            result.AppendLine("    {");
            result.AppendLine("        layers = new();");
            result.AppendLine("        foreach (MapLayer layer in list) {");
            result.AppendLine("            layers.Add(layer.Identifier, layer);");
            result.AppendLine("            layer.SetInitialValues(d1Size, d2Size);");
            result.AppendLine("        }");
            result.AppendLine("        this.d1Size = d1Size;");
            result.AppendLine("        this.d2Size = d2Size;");
            result.AppendLine("    }");
            result.AppendLine("}");

            result.AppendLine("class MapLayer {");
            result.AppendLine("    public object InitialValue {get; private set; }");
            result.AppendLine("    public object[,]? LayerValue {get; private set; }");
            result.AppendLine("    public string Identifier {get; private set; }");
            result.AppendLine("    public void SetInitialValues (int size1, int size2) {");
            result.AppendLine("        LayerValue = new object[size1,size2];");
            result.AppendLine("        for (int i = 0; i < size1; i++)");
            result.AppendLine("            for (int j = 0; j < size2; j++)");
            result.AppendLine("                LayerValue[i,j] = InitialValue;");
            result.AppendLine("                ");
            result.AppendLine("    }");
            result.AppendLine("    public MapLayer (string type, string identifier, object? InitialValue) {");
            result.AppendLine("        this.Identifier = identifier;");
            result.AppendLine("        if (InitialValue != null) {");
            result.AppendLine("            this.InitialValue = InitialValue;");
            result.AppendLine("        } else {");
            result.AppendLine("            this.InitialValue = type == \"int\" ? 0 : false;");
            result.AppendLine("        }");
            result.AppendLine("    }");
            result.AppendLine("}");



            result.AppendLine("");
            foreach (LibraryNode library in libraryNodes) {
                result.AppendLine(library.CodeGen(indentation - 1));
            }
            result.AppendLine("\tclass Program");
            result.AppendLine("\t{");
            result.AppendLine("\t\tstatic void Main(string[] args)");
            result.AppendLine("\t\t{");
            foreach (LineNode line in lineNodes) {
                //for (int i = 0; i < indentation; i++)
                    //result +="\t";
                result.AppendLine(line.CodeGen(indentation + 1));
            }
            result.AppendLine("\t\t}");
            result.AppendLine("\t}");
            result.AppendLine("}");
            return result.ToString();
        }

    }

    class Map {
        Dictionary<string, MapLayer> layers;
        public int d1Size;
        public int d2Size;
        public Map(int d1Size, int d2Size, params MapLayer[] list)
        {
            layers = new();
            foreach (MapLayer layer in list) {
                layers.Add(layer.Identifier, layer);
                layer.SetInitialValues(d1Size, d2Size);
            }
            this.d1Size = d1Size;
            this.d2Size = d2Size;
        }
    }

    class MapLayer {
        public object InitialValue {get; private set; }
        public object[,]? LayerValue {get; private set; }
        public string Identifier {get; private set; }
        public void SetInitialValues (int size1, int size2) {
            LayerValue = new object[size1,size2];
            for (int i = 0; i < size1; i++)
                for (int j = 0; j < size2; j++)
                    LayerValue[i,j] = InitialValue;
                    
        }
        public MapLayer (string type, string identifier, object? InitialValue) {
            this.Identifier = identifier;
            if (InitialValue != null) {
                this.InitialValue = InitialValue;
            } else {
                this.InitialValue = type == "int" ? 0 : false;
            }
        }
    }
    
    
    /*class IntLayer : MapLayer<int> {
        protected override Dictionary<string, int[][]> layerValue {get;set;}
    }
    class BoolLayer : MapLayer<bool> {
        protected override Dictionary<string, bool[][]> layerValue {get;set;}
    }*/
}
