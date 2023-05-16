using System;
using System.Text;
using Antlr_language.ast.structure;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.expression
{
    public class IndividualLayerNode : AbstractExpressionNode
    {
        public TypeNode type {get; private set;}
        public string IDENTIFIER {get; private set;}
        public ExpressionNode? expression {get; private set;}

        public IndividualLayerNode(TypeNode type, string iDENTIFIER, ExpressionNode? expression)
        {
            this.type = type;
            IDENTIFIER = iDENTIFIER;
            this.expression = expression;
        }

        public override string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append("new MapLayer (");
            result.Append("\"" + type.CodeGen(indentation) + "\",");
            result.Append("\"" + IDENTIFIER + "\",");
            result.Append(expression?.CodeGen(indentation) ?? "null");
            result.Append(")");
            
            return result.ToString();
        }
        public Enums.Types GetNodeType () {
            return type.GetNodeType();
        }

        public override Enums.Types getEvaluationType()
        {
            throw new NotImplementedException();
        }
    }
}