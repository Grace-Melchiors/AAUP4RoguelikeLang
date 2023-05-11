// See https://aka.ms/new-console-template for more information
using System;
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
ProgramNode programNode = (ProgramNode) builder.VisitProgram(vestaContext);
string Code = builder.Visit((vestaContext)).CodeGen(0);
CSB.AppendLine(Code);

CSB.OutputResult();

SemanticAnalysis semanticAnalysis = new SemanticAnalysis();
semanticAnalysis.VisitProgram(programNode);

Console.WriteLine("Press enter to continue...");
Console.Read();
