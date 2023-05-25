namespace TestProject1;
using Antlr_language;
using Antlr_language.Content;
using Antlr4.Runtime;
using System.Text;

public class CodeGenerationTest
{
    private MapGeniusParser Setup(string text)
    {
        AntlrInputStream inputStream = new AntlrInputStream(text);
        MapGeniusLexer MapGeniusLexer = new MapGeniusLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(MapGeniusLexer);
        MapGeniusParser MapGeniusParser = new MapGeniusParser(commonTokenStream);
        return MapGeniusParser;
    }
    
    [Test]
    public void TestMismatchedTypes()
    {
        // Build input
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("int i = 5");
        sb.AppendLine("i = true");
        var visitor = new MapGeniusVisitor(true);
        MapGeniusParser parser = Setup(sb.ToString());

        // Assert that type checker throws an Exception
        Assert.Throws<Exception>( delegate
            {
                object content = visitor.Visit(parser.program());
            }
        );

    } 
    
    
    [Test]
    public void TestIdentifierTypeInt()
    {
        MapGeniusParser parser = Setup("int counter = 5;");

        MapGeniusParser.IdentifierTypeContext context = parser.identifierType();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);

        object result = 0;

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestIdentifierTypeBool()
    {
        MapGeniusParser parser = Setup("bool counter = true;");

        MapGeniusParser.IdentifierTypeContext context = parser.identifierType();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);

        object result = false;

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestIdentifierTypeArray()
    {
        MapGeniusParser parser = Setup("int[5,4] counter = 5;");

        MapGeniusParser.IdentifierTypeContext context = parser.identifierType();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] array= {5,4};
        object result = new ArrDesc(0, array);
        
        Assert.AreEqual(((ArrDesc)result).dimensions, ((ArrDesc)content).dimensions);
    }
    
    public void TestIdentifierTypeMap()
    {
        MapGeniusParser parser = Setup("map counter = [5,5]{int region};");

        MapGeniusParser.IdentifierTypeContext context = parser.identifierType();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);

        object result = new Map(0, 0);
        
        Assert.AreEqual(result.GetType(), content.GetType());
    }
   [Test]
    public void TestFunctionParameters()
    {
        MapGeniusParser parser = Setup("int region, bool locked");

        MapGeniusParser.FuncParamsContext context = parser.funcParams();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        ParamDesc[] result = {new ParamDesc(0, "region"), new ParamDesc(false, "locked")};

        Assert.AreEqual(((ParamDesc[])result)[0].typeBase, ((ParamDesc[])content)[0].typeBase);
    }
    [Test]
    public void TestParameters()
    {
        MapGeniusParser parser = Setup("int region");

        MapGeniusParser.ParameterContext context = parser.parameter();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        ParamDesc result = new ParamDesc(0, "region");

        Assert.AreEqual(((ParamDesc)result).paramName, ((ParamDesc)content).paramName);
    }
    [Test]
    public void TestFunctionBody()
    {
        MapGeniusParser parser = Setup("{int myVar = 10; return myVar;}");

        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 10;

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestReturnStatement()
    {
        MapGeniusParser parser = Setup("return 10;");

        MapGeniusParser.ReturnStmtContext context = parser.returnStmt();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 10;
        Assert.AreEqual(result, content);
    }

    [Test] //The function Body is used as it returns the desired value
    public void TestDeclaration()
    {

        Console.WriteLine("Hello");
        MapGeniusParser parser = Setup("{int var = 5; return var;}");

        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        Console.WriteLine("Test");
        object content = visitor.Visit(context);
        var result = 5;

        Console.WriteLine(content);
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestDeclarationQuickAssign()
    {
        MapGeniusParser parser = Setup("{int[5] var = 5; return var;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] result = {5,5,5,5,5};
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestMapGetLayer()
    {
        MapGeniusParser parser = Setup("{map myMap = [2,2]{int locked = 2};return myMap.locked;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] resultAssist = { 2, 2 };
        int[][] result = {resultAssist,resultAssist};
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestMapExpression()
    {
        MapGeniusParser parser = Setup("{map myMap = [2,2] {int locked=2}; return myMap;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        Map result = new Map(2,2);
        int[] resultAssist = { 2, 2 };
        int[][] resultAssist2 = {resultAssist,resultAssist};
        result.layers["locked"] = resultAssist2;
        Assert.AreEqual(((Map)result).layers["locked"], ((Map)content).layers["locked"]);
    }
    
    [Test] //The function Body is used as it returns the desired value
    public void TestScope()
    {
        MapGeniusParser parser = Setup("{int var = 5; if(true){var=15;}; return var;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 15;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestBaseAssignment()
    {
        MapGeniusParser parser = Setup("{int myVar = 5; return myVar;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 5;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestArrayAssignment()
    {
        MapGeniusParser parser = Setup("{int[5] var = 0; var = {1,2,3,4,5}; return var[2];}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 3;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestMapAssignment()
    {
        MapGeniusParser parser = Setup("{map myMap = [2,2] {bool walkable}; myMap.walkable[1,1]=true; return myMap.walkable;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        bool[][] result = {new bool[]{false,false}, new bool[]{false, true}};

        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestPrefabInsert()
    {
        MapGeniusParser parser = Setup("{int[5,5] var = 0; int[2,2] prefab = 2; var[2,3]=prefab ; return var;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        object[] result = {new int[]{0,0,0,0,0}, new int[]{0,0,0,0,0}, 
            new int[]{0,0,0,2,2}, new int[]{0,0,0,2,2}, new int[]{0,0,0,0,0}};
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestConstantExpression()
    {
        MapGeniusParser parser = Setup("{return 2;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 2;
        Assert.AreEqual(result, content);
    }

    [Test] //The function Body is used as it returns the desired value
    public void TestIdentifierExpression()
    {
        MapGeniusParser parser = Setup("{bool myVar = true; return myVar;}");

        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = true;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestArrayExpression()
    {
        MapGeniusParser parser = Setup("{int[3] myVar = {1,2,3}; return myVar;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] result = {1,2,3};
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestArrayIdentifierExpression()
    {
        MapGeniusParser parser = Setup("{int[3,3] myVar = 3; return myVar[2,2];}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 3;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestAdditionExpression()
    {
        MapGeniusParser parser = Setup("{return 5+4;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 9;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestArrayAdditionExpression()
    {
        MapGeniusParser parser = Setup(" {int[3] arr= {5,6,7}; return arr-4;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] result = {1,2,3};
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestMultiplicationExpression()
    {
        MapGeniusParser parser = Setup("{return 4*5;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 20;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestArrayMultiplicationExpression()
    {
        MapGeniusParser parser = Setup("{int[3] arr= {20,30,40}; return arr/4;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] result = { 5, 7, 10 };
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestArrayWithArrayAdditionExpression()
    {
        MapGeniusParser parser = Setup("{int[2] arr= {1,2}; int[2] arr2={3,4}; return arr+arr2;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] result = {4,6};
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestArrayWithArrayMultiplicationExpression()
    {
        MapGeniusParser parser = Setup("{int[2] arr= {1,2}; int[2] arr2={3,4}; return arr*arr2;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        int[] result = {3,8};
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestParenthesizedExpression()
    {
        MapGeniusParser parser = Setup("{return (4+5)*6;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 54;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestCompareTrueExpression()
    {
        MapGeniusParser parser = Setup("{return 5>4;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = true;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestCompareFalseExpression()
    {
        MapGeniusParser parser = Setup("{return 5<4;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = false;
        Assert.AreEqual(result, content);
    }

    [Test] //The function Body is used as it returns the desired value
    public void TestCompareArrayExpression()
    {
        MapGeniusParser parser = Setup("{int[9] arr= {1,2,3,4,5,6,7,8,9}; return arr==4;}");

        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        bool[] result = { false, false, false, true, false, false, false, false, false };
        Assert.AreEqual(result, content);
    }

    [Test] //The function Body is used as it returns the desired value
    public void TestNotExpression()
    {
        MapGeniusParser parser = Setup("{ return !true;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = false;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestNegExpression()
    {
        MapGeniusParser parser = Setup(" {return -4;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = -4;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestWhileBlocke()
    {
        MapGeniusParser parser = Setup("{int i=0; while(i<10){i=i+1;} return i;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 10;
        Assert.AreEqual(result, content);
    }
    [Test] //The function Body is used as it returns the desired value
    public void TestForLoop()
    {
        MapGeniusParser parser = Setup("{ int i=0; for(int j=0; i<10; i=i+1){} return i;}");
    
        MapGeniusParser.FuncBodyContext context = parser.funcBody();
        var visitor = new MapGeniusVisitor(true);
        object content = visitor.Visit(context);
        var result = 10;
        Assert.AreEqual(result, content);
    }
}


public class ParserTest
{
    private MapGeniusParser Setup(string text)
    {
        AntlrInputStream inputStream = new AntlrInputStream(text);
        MapGeniusLexer MapGeniusLexer = new MapGeniusLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(MapGeniusLexer);
        MapGeniusParser MapGeniusParser = new MapGeniusParser(commonTokenStream);
        return MapGeniusParser;
    }

    [Test]
    public void TestIdentifierType()
    {
        MapGeniusParser parser = Setup("int counter = 5;");

        MapGeniusParser.IdentifierTypeContext context = parser.identifierType();

        string content = context.GetText();

        Assert.AreEqual("int", content);
    }
    
    

    [Test]
    public void TestIdentifierTypeArray()
    {
        MapGeniusParser parser = Setup("int[10,5] counter = 5;");

        MapGeniusParser.IdentifierTypeContext context = parser.identifierType();
        
        string content = context.GetText();
        string result = "int[10,5]";

        Assert.AreEqual(result,  content);
    }
    
    [Test]
    public void TestIdentifierExpression()
    {
        MapGeniusParser parser = Setup("counter = someVar; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=someVar";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestParenthesizedExpression()
    {
        MapGeniusParser parser = Setup("counter = (10); counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=(10)";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestArrayIdentifierExpression()
    {
        MapGeniusParser parser = Setup("counter = someVar[2,5]; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=someVar[2,5]";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestMapGetLayerExpression()
    {
        MapGeniusParser parser = Setup("counter = someVar.region; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=someVar.region";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestArrayExpression()
    {
        MapGeniusParser parser = Setup("counter = {5,4,3}; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter={5,4,3}";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestMapExpression()
    {
        MapGeniusParser parser = Setup("myMap = [3,3]{int counter, bool walkable=true}; myMap.counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "myMap=[3,3]{intcounter,boolwalkable=true}";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestFunctionCallExpression()
    {
        MapGeniusParser parser = Setup("counter = someFunc(5); counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=someFunc(5)";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestNotExpression()
    {
        MapGeniusParser parser = Setup("walkable = !true; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "walkable=!true";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestNegExpression()
    {
        MapGeniusParser parser = Setup("counter = -someVar; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=-someVar";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestMultiplicationExpression()
    {
        MapGeniusParser parser = Setup("counter = 3*5; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=3*5";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestAdditionExpression()
    {
        MapGeniusParser parser = Setup("counter = 3+5; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "counter=3+5";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestCompareExpression()
    {
        MapGeniusParser parser = Setup("walkable = 3<5; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "walkable=3<5";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestBooleanExpression()
    {
        MapGeniusParser parser = Setup("walkable = true && false; counter = {1,2,3};");

        MapGeniusParser.AssignmentContext context = parser.assignment();

        string content = context.GetText();
        string result = "walkable=true&&false";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestIfBlock()
    {
        MapGeniusParser parser = Setup("if(5==1){int var=20;} Stdlib.print(10);");

        MapGeniusParser.IfStatementContext context = parser.ifStatement();

        string content = context.GetText();
        string result = "if(5==1){intvar=20;}";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestIfElseBlock()
    {
        MapGeniusParser parser = Setup("if(5==1){int var=20;}else{int var=5;} Stdlib.print(10);");

        MapGeniusParser.IfStatementContext context = parser.ifStatement();
        
        string content = context.GetText();
        string result = "if(5==1){intvar=20;}else{intvar=5;}";

        Assert.AreEqual(result,  content);
    }

    [Test]
    public void TestWhileBlock()
    {
        MapGeniusParser parser = Setup("while(var){var = !var;} Stdlib.print(10);");

        MapGeniusParser.WhileStatementContext context = parser.whileStatement();

        string content = context.GetText();
        string result = "while(var){var=!var;}";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestForLoopWithDclr()
    {
        MapGeniusParser parser = Setup("for(int i=5; i<10; i=i+1){write(i);} myVar=10; Stdlib.print(myVar);");

        MapGeniusParser.ForStatementContext context = parser.forStatement();
        
        string content = context.GetText();
        string result = "for(inti=5;i<10;i=i+1){write(i);}";

        Assert.AreEqual(result,  content);
    }
    [Test]
    public void TestForLoopWithAssign()
    {
        MapGeniusParser parser = Setup("for(i=5; i<10; i=i+1){write(i);} myVar=10; Stdlib.print(myVar);");

        MapGeniusParser.ForStatementContext context = parser.forStatement();
        
        string content = context.GetText();
        string result = "for(i=5;i<10;i=i+1){write(i);}";

        Assert.AreEqual(result,  content);
    }
    [Test]
    public void TestChance()
    {
        MapGeniusParser parser = Setup("chance{5:{var=10;}2:{var=100;}} Stdlib.print(var);");

        MapGeniusParser.ChanceContext context = parser.chance();
        
        string content = context.GetText();
        string result = "chance{5:{var=10;}2:{var=100;}}";

        Assert.AreEqual(result,  content);
    }

    [Test]
    public void TestFunctionDclr()
    {
        MapGeniusParser parser = Setup("int double(int number){number = number*2; return number;} Stdlib.print(double(10));");

        MapGeniusParser.FunctionDeclContext context = parser.functionDecl();

        string content = context.GetText();
        string result = "intdouble(intnumber){number=number*2;returnnumber;}";

        Assert.AreEqual(result, content);
    }
    [Test]
    public void TestFunctionCall()
    {
        MapGeniusParser parser = Setup("someFunc(myVar+10); myVar = 10;");

        MapGeniusParser.FunctionCallContext context = parser.functionCall();
        
        string content = context.GetText();
        string result = "someFunc(myVar+10)";

        Assert.AreEqual(result,  content);
    }
    [Test]
    public void TestBaseAssignment()
    {
        MapGeniusParser parser = Setup("myVar = 10; int seed = 808");

        MapGeniusParser.AssignmentContext context = parser.assignment();
        
        string content = context.GetText();
        string result = "myVar=10";

        Assert.AreEqual(result,  content);
    }
    [Test]
    public void TestArrayAssignment()
    {
        MapGeniusParser parser = Setup("myVar[2,5] = 10; int seed = 808");

        MapGeniusParser.AssignmentContext context = parser.assignment();
        
        string content = context.GetText();
        string result = "myVar[2,5]=10";

        Assert.AreEqual(result,  content);
    }
    [Test]
    public void TestMapAssignment()
    {
        MapGeniusParser parser = Setup("myVar.region[2,3] = 10; Stdlib.print(myVar.region);");

        MapGeniusParser.MapAssignmentContext context = parser.mapAssignment();
        
        string content = context.GetText();
        string result = "myVar.region[2,3]=10";

        Assert.AreEqual(result,  content);
    }
}
