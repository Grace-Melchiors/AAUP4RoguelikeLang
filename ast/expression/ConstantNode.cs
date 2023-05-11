using System;

namespace Antlr_language.ast.expression
{
    public class ConstantNode : AbstractExpressionNode
    {
        
        private bool? boolean = null;
        private int? integer = null;

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
            if (boolean != null) {
                return boolean == true ? "true" : "false";
            } else if (integer != null) {
                string? result = integer.ToString() ?? "0";
                if (result == null)
                    throw new ArgumentException();
                return result;
            } else {
                throw new NotImplementedException();
            }
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
