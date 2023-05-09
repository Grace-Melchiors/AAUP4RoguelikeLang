using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public class ProgramNode : AbstractNode
    {
        private List<LineNode> lineNodes = new List<LineNode>();

        public string CodeGen()
        {
            string result = "";
            foreach (LineNode line in lineNodes) {
                result += line.CodeGen();
            }
            return result;
        }

    }
}
