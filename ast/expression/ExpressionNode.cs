using System;

namespace Antlr_language.ast.expression
{
    public class ExpressionNode : AbstractExpressionNode
    {
        //public Enums.Types evaluationType;
        
        private Enums.Operators Operator;
        private ExpressionNode? expression1;
        private ExpressionNode? expression2;
        private FactorNode? factor;
        

        public FactorNode GetFactorNode() {
            return factor;
        }
        
        public Tuple<ExpressionNode, ExpressionNode> GetExpressionNodes() {
            return new Tuple<ExpressionNode, ExpressionNode>(expression1, expression2);
        }
        


        public ExpressionNode(Enums.Operators Operator, ExpressionNode? expression1, ExpressionNode? expression2, FactorNode? factor)
        {
            this.Operator = Operator;
            this.expression1 = expression1;
            this.expression2 = expression2;
            this.factor = factor;
        }

        public override string CodeGen(int indentation)
        {
            
            if (factor != null) {
                if (Operator == Enums.Operators.sub || Operator == Enums.Operators.not) {
                    return "(" + Enums.OperatorToString(Operator) + factor.CodeGen(indentation) + ")";
                } else {
                    return factor.CodeGen(indentation);
                }
            } else if (expression1 != null && expression2 != null) {
                return expression1.CodeGen(indentation) + Enums.OperatorToString(Operator) + expression2.CodeGen(indentation);
            } else {
                throw new NotImplementedException();
            }
        }
        public override Enums.Types getEvaluationType () {
            return Enums.Types.MAP;
        }
    }
}
