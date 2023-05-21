using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class AssignmentNode : AbstractNode
    {
        public TypeNode IdentifierType {get; set;}
        public string IDENTIFIER {get; private set;}
        public List<ExpressionNode>? ArrayIndicies {get; private set;}
        public List<int>? ArraySizes {get; private set;}
        public ExpressionNode expression {get; private set;}

        public AssignmentNode(string iDENTIFIER, List<ExpressionNode>? arrayIndicies, ExpressionNode expression)
        {
            IDENTIFIER = iDENTIFIER;
            ArrayIndicies = arrayIndicies;
            this.expression = expression;
        }

        public string GetIdentifier() {
            return IDENTIFIER;
        }
        
        public List<ExpressionNode>? GetArrayIndices() {
            return ArrayIndicies;
        }
        
        public ExpressionNode? GetExpressionNode() {
            return expression;
        }
    }
}
