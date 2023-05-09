// See https://aka.ms/new-console-template for more information

using Antlr_language;
using Antlr_language.Content;
using Antlr4.Runtime;
using Antlr_language.ast;

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
builder.Visit((vestaContext));

CSB.OutputResult();

Console.Read();
