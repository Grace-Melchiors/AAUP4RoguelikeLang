using Antlr_language.ast.structure;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.expression
{
    public class IndividualLayerNode : AbstractExpressionNode
    {
        private TypeNode type;
        private string IDENTIFIER;
        private ExpressionNode? expression;

        public IndividualLayerNode(TypeNode type, string iDENTIFIER, ExpressionNode? expression)
        {
            this.type = type;
            IDENTIFIER = iDENTIFIER;
            this.expression = expression;
        }

        public override string CodeGen(int indentation)
        {
            throw new NotImplementedException();
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