using System;

namespace Antlr_language.ast.expression
{
    public class FactorNode : AbstractExpressionNode
    {
        
        public ExpressionNode? parenthesizedExpression {get; private set; }
        public ConstantNode? constant {get; private set; }
        public Factor2Node? factor2 {get; private set; }
        public ArrayExpressionNode? arrayExpressionsNode {get; private set; }
        public MapExpressionNode? mapExpression {get; private set; }
        public ArrayAccessNode? arrayAccess {get; private set; }
        public MapAccessNode? mapAccess {get; private set; }

        public Factor2Node? GetFactor2Node() {
            return factor2;
        }

        public FactorNode(ExpressionNode? parenthesizedExpression, ConstantNode? constant, Factor2Node? factor2, ArrayExpressionNode? arrayExpressionsNode, MapExpressionNode? mapExpression, ArrayAccessNode? arrayAccess, MapAccessNode? mapAccess)
        {
            this.parenthesizedExpression = parenthesizedExpression;
            this.constant = constant;
            this.factor2 = factor2;
            this.arrayExpressionsNode = arrayExpressionsNode;
            this.mapExpression = mapExpression;
            this.arrayAccess = arrayAccess;
            this.mapAccess = mapAccess;
        }
        public Enums.Types getEvaluationType () {
            if(constant != null) {
                if(constant.integer != null) {
                    return Enums.Types.INTEGER;
                }
                if(constant.boolean != null) {
                    return Enums.Types.BOOL;
                }
                throw new Exception("Lol neither int or bool xd");
            }
            if(mapExpression != null) {
                return Enums.Types.MAP;
            }
            
            throw new UndefinedTypeException("Error In FactorNode.cs");
            
            // HVAD MED BOOL??? HVBAD ER DET FOR NOGLE FIELDS JEG OPVERHOEVEDT KIGGER PÅ
        }
    }
}
