// See https://aka.ms/new-console-template for more information
using System;
using System.Text;
using Antlr_language;
using Antlr_language.Content;
using Antlr4.Runtime;
using Antlr_language.ast;
using Antlr_language.ast.structure;

var fileName = "Content/input/addition.V"; // args[0]
var fileContents = File.ReadAllText(fileName);

CSharpBuilder CSB = new CSharpBuilder();


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


ASTSemanticAnalysisVisitor TypeIdentifier = new ASTSemanticAnalysisVisitor();
TypeIdentifier.Visit((dynamic)AST);

CodeGenVisitor CodeGenerator = new CodeGenVisitor(2);

CSB.AcceptStringBuilder(CodeGenerator.Visit((dynamic)AST));
CSB.OutputResult();
//CodeGenVisitor CodeGen = new CodeGenVisitor();
//CodeGen.Visit((dynamic)AST);



//SemanticAnalysis semanticAnalysis = new SemanticAnalysis();
//semanticAnalysis.VisitProgram((ProgramNode) AST);

Console.WriteLine("Press enter to continue...");
Console.Read();
