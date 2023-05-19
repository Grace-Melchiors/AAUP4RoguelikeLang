using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    public abstract class LibraryNode : AbstractNode
    {
        public abstract string LibraryName {get; set;}
        public abstract string Content {get; set;}
    }
}