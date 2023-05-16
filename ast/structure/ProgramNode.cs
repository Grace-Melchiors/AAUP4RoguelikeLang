using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class ProgramNode : AbstractNode
    {
        private string nameSpace = "MapGen";
        public List<LibraryNode> libraryNodes { get; private set;} = new List<LibraryNode>();
        public List<LineNode> lineNodes  { get; private set;} = new List<LineNode>();
        
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
            
            result.AppendLine("\tclass Map {");
            result.AppendLine("\t    public Dictionary<string, MapLayer> layers {get; private set;}");
            result.AppendLine("\t    public int d1Size;");
            result.AppendLine("\t    public int d2Size;");
            result.AppendLine("\t    public Map(int d1Size, int d2Size, params MapLayer[] list)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        layers = new();");
            result.AppendLine("\t        foreach (MapLayer layer in list) {");
            result.AppendLine("\t            layers.Add(layer.Identifier, layer);");
            result.AppendLine("\t            layer.SetInitialValues(d1Size, d2Size);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        this.d1Size = d1Size;");
            result.AppendLine("\t        this.d2Size = d2Size;");
            result.AppendLine("\t    }");
            result.AppendLine("\t}");

            result.AppendLine("\tclass MapLayer {");
            result.AppendLine("\t    public object InitialValue {get; private set; }");
            result.AppendLine("\t    public object[,]? LayerValue {get; private set; }");
            result.AppendLine("\t    public string Identifier {get; private set; }");
            result.AppendLine("\t    public void SetInitialValues (int size1, int size2) {");
            result.AppendLine("\t        LayerValue = new object[size1,size2];");
            result.AppendLine("\t        for (int i = 0; i < size1; i++)");
            result.AppendLine("\t            for (int j = 0; j < size2; j++)");
            result.AppendLine("\t                LayerValue[i,j] = InitialValue;");
            result.AppendLine("\t                ");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public MapLayer (string type, string identifier, object? InitialValue) {");
            result.AppendLine("\t        this.Identifier = identifier;");
            result.AppendLine("\t        if (InitialValue != null) {");
            result.AppendLine("\t            this.InitialValue = InitialValue;");
            result.AppendLine("\t        } else {");
            result.AppendLine("\t            this.InitialValue = type == \"int\" ? 0 : false;");
            result.AppendLine("\t        }");
            result.AppendLine("\t    }");
            result.AppendLine("\t}");



            

            foreach (LibraryNode library in libraryNodes) {
                result.AppendLine(library.CodeGen(indentation - 1));
            }
            result.AppendLine("\tclass Program");
            result.AppendLine("\t{");
            result.AppendLine("\t\tstatic Random rng = new Random();");
            result.AppendLine("\t\tpublic static void seed (int newSeed) {");
            result.AppendLine("\t\t\trng = new Random(newSeed);");
            result.AppendLine("\t\t}");
            result.AppendLine("\t\tstatic void Main(string[] args)");
            result.AppendLine("\t\t{");
            result.AppendLine("\t\t\tRandom rng = new Random();");
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
