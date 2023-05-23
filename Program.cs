// See https://aka.ms/new-console-template for more information
using System;
using System.Text;
using Antlr_language;
using Antlr_language.Content;
using Antlr4.Runtime;
using Antlr_language.ast;
using Antlr_language.ast.structure;

namespace Antlr_language
{
    class Program
    {
        static void Main (string[] args) {
            var filePath = "";
            var outputFileName = "";
            if (args.Length > 0 && args[0] != null) {
                filePath = args[0];
            } else {
                filePath = "Content/input/addition.V";
            }
            if (args.Length > 1 && args[1] != null) {
                outputFileName = args[1];
            } else {
                outputFileName = "Content/output/addition.cs";
            }
            var fileContents = File.ReadAllText(filePath);
            string defaultOutputFolder = "Content/output";
            string defaultOutputFile = "output" + /*DateTime.Now.ToShortDateString() +*/ ".cs";
            CSharpBuilder CSB = new CSharpBuilder(Path.GetDirectoryName(outputFileName) ?? defaultOutputFolder, Path.GetFileName(outputFileName) ?? defaultOutputFile);


            AntlrInputStream inputStream = new AntlrInputStream(fileContents);
            var vestaLexer = new VestaLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(vestaLexer);
            VestaParser vestaParser = new VestaParser(commonTokenStream);
            //vestaParser.AddErrorListener();

            var vestaContext = vestaParser.program();

            //var visitor = new VestaVisitor();
            //visitor.Visit((vestaContext));

            AstBuilder builder = new AstBuilder();
            //ProgramNode programNode = (ProgramNode) builder.VisitProgram(vestaContext);

            AbstractNode AST = builder.Visit((vestaContext));


            ASTDecorator Decorator = new ASTDecorator();
            Decorator.Visit((dynamic)AST);

            CodeGenVisitor CodeGenerator = new CodeGenVisitor(2);

            CSB.AcceptStringBuilder(CodeGenerator.Visit((dynamic)AST));
            CSB.OutputResult();
            //CodeGenVisitor CodeGen = new CodeGenVisitor();
            //CodeGen.Visit((dynamic)AST);



            //SemanticAnalysis semanticAnalysis = new SemanticAnalysis();
            //semanticAnalysis.VisitProgram((ProgramNode) AST);

            Console.WriteLine("Press enter to continue...");
            Console.Read();

        }
    }
}
