using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class TypeNode : AbstractNode
    {
        private Enums.Types Type;
        public bool IsArray => ArraySizes == null ? false : true;
        private List<ExpressionNode>? ArraySizes;

        public TypeNode(Enums.Types type, List<ExpressionNode>? arraySizes)
        {
            Type = type;
            ArraySizes = arraySizes;
        }

        public string CodeGen(int indentation)
        {
            string result = "";
            if (Type == Enums.Types.MAP) {
                result += "Map";
            } else if (Type == Enums.Types.INTEGER) {
                result += "int";
            } else if (Type == Enums.Types.BOOL) {
                result += "bool";
            } else {
                throw new NotImplementedException();
            }
            if (ArraySizes != null) {
                result += "["; 
                foreach (ExpressionNode size in ArraySizes) {
                    result += size.CodeGen(indentation) + ",";
                }
                result = result.Substring(0, result.Length-2);
                result += "]";
            }
            result += " ";
            return result;
        }

    }
}