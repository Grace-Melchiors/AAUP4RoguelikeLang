using System;

namespace Antlr_language.ast.expression
{
    public class ConstantNode : AbstractExpressionNode
    {
        
        public bool? boolean{get; private set;} = null;
        public int? integer{get; private set;} = null;

        public bool? GetBoolean() {
            return boolean;

        }

        public int? GetInteger() {
            return integer;
            
        }

        public ConstantNode(bool? boolean, int? integer)
        {
            this.boolean = boolean;
            this.integer = integer;
        }

        public override string CodeGen(int indentation)
        {
            string result;
            if (boolean != null) {
                result = boolean == true ? "true" : "false";
            } else if (integer != null) {
                result = integer.ToString() ?? "0";
            } else {
                throw new NotImplementedException();
            }
            //System.Console.WriteLine(result);
            return result;
        }
        public override Enums.Types getEvaluationType () {
            if (boolean != null) {
                return Enums.Types.BOOL;
            } else if (integer != null) {
                return Enums.Types.INTEGER;
            } else {
                throw new NotImplementedException();
            }
        }
    }
}
