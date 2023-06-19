using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class ProgramNode : AbstractNode
    {
        public string nameSpace {get; private set;} = "MapGen";
        public List<LibraryNode> libraryNodes { get; private set;} = new List<LibraryNode>();
        public List<LineNode> lineNodes  { get; private set;} = new List<LineNode>();
        public List<LineNode> FunctionDecls  { get; set;} = new List<LineNode>();
        public List<LineNode> GlobalVariables { get; set;} = new List<LineNode>();
        public List<LineNode> Statements  { get; set;} = new List<LineNode>();
        
        public List<LineNode> retrieveLineNodes() {
            return lineNodes;
        }
        public void AddLineNode (LineNode node) {
            lineNodes.Add(node);
        }
        public void AddLibraryNode (LibraryNode node) {
            libraryNodes.Add(node);
        }
        

    }
}
