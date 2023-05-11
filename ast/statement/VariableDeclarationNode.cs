using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast;
using Antlr_language.ast.structure;

namespace Antlr_language.ast.statement
{
    public class VariableDeclarationNode : AbstractNode
    {
        private TypeNode Type;
        private string? identifier;
        private ExpressionNode? expression;
        //private List<int>? ArraySizes;
        
        public string GetIdentifier() {
            return identifier;
        }
        public Enums.Types GetDataType() {
            return Type.GetDataType();
        }

        public ExpressionNode GetExpressionNode() {
            return expression;
        }

        //Normal declaration
        public VariableDeclarationNode(TypeNode type, string? identifier, ExpressionNode? expression)
        {
            Type = type;
            this.identifier = identifier;
            this.expression = expression;
            //ArraySizes = null;
        }
        //Array declaration
        //public VariableDeclarationNode(TypeNode type, string? identifier, ExpressionNode? expression, List<int> arraySizes): this(type, identifier, expression) {
        //    ArraySizes = arraySizes;
        //}

        public string CodeGen(int indentation)
        {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.Append(Type.CodeGen(indentation) + " ");
            result.Append(identifier);
            if (expression != null) {
                result.Append(" = " + expression.CodeGen(indentation));
            } else {
                if (Type.GetNodeType() == Enums.Types.INTEGER) {
                    if (Type.IsArray) {
                        result.Append(" = new " + Type.CodeGen(indentation, true));
                    } else {
                        result.Append(" = " + Constants.INTEGER_DEFAULT);
                    }
                } else if (Type.GetNodeType() == Enums.Types.BOOL) {
                    if (Type.IsArray) {
                        result.Append(" = new " + Type.CodeGen(indentation, true));
                    } else {
                        result.Append(" = " + Constants.BOOL_DEFAULT);
                    }
                    
                } else {
                    throw new NotImplementedException();
                }
                
            }

            return result.ToString();
        }

    }

    

}
