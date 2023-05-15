using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast;

namespace Antlr_language.ast.structure
{
    public class FunctionDeclarationNode : AbstractNode
    {
        private TypeNode Type;
        private string? Identifier;
        private List<FunctionParamNode> funcParams = new List<FunctionParamNode>();
        private BlockNode? body;

        public FunctionDeclarationNode(TypeNode type, string? identifier, List<FunctionParamNode> funcParams, BlockNode? body)
        {
            Type = type;
            Identifier = identifier;
            this.funcParams = funcParams;
            this.body = body;
        }

        public string CodeGen(int indentation)
        {
            string indent = "";
            for (int i = 0; i < indentation; i++) {
                indent += "\t";
            }
            string result = "\n" + indent;
            result += Type.CodeGen(indentation) + " ";
            result += Identifier + " ";
            //System.Console.WriteLine("Test from: FunctionDeclarationNode");
            result += "(";
            if (funcParams.Count != 0) {
                foreach (FunctionParamNode param in funcParams) {
                    if (param != null) {
                        result += param.CodeGen(indentation) + ",";
                    } else {
                        throw new NotImplementedException();
                    }
                }
                result = result.Substring(0, result.Length-1);
            }
            result += ")\n";
            if (body == null)
                throw new NotImplementedException();
            result += body.CodeGen(indentation);
            result += indent + "\n";
            return result;
        }

    }
}
