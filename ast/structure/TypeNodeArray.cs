using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class TypeNodeArray : AbstractNode
    {
        public Enums.Types Type {get; private set;}
        public bool IsArray => ArraySizes == null ? false : true;
        public List<ExpressionNode>? ArraySizes {get; private set;}
        
        public Enums.Types GetDataType() {
            return Type;
        }

        public TypeNodeArray(Enums.Types type, List<ExpressionNode>? arraySizes)
        {
            Type = type;
            ArraySizes = arraySizes;
        }


        public Enums.Types GetNodeType () {
            return Type;
        }
        public List<ExpressionNode>? GetArraySizes () {
            return ArraySizes;
        }

        /*public string CodeGen(int indentation)
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
                    result += ",";
                }
                result = result.Substring(0, result.Length-1);
                result += "]";
            }
            return result;
        }
        public string CodeGen(int indentation, bool showSize)
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
                    if (showSize) {
                        result += size.CodeGen(indentation) + ",";
                    } else {
                        result += ",";
                    }
                }
                result = result.Substring(0, result.Length-1);
                result += "]";
            }
            return result;
        }*/

    }
}