using System.Collections;
using System.Data;
using System.Diagnostics.Metrics;
using System.Formats.Asn1;
using System.Linq.Expressions;
using Antlr_language.Content;
using Antlr4.Runtime;
using Microsoft.VisualBasic.CompilerServices;

namespace Antlr_language;

public class ArrDesc
{
    public object? typeBase;
    public Array dimensions;
    public ArrDesc(object? typeBase, Array dimensions)
    {
        this.typeBase = typeBase;
        this.dimensions = dimensions;
    }
}

public class LayerDesc
{
    public string identifier;
    public object value;

    public LayerDesc(string identifier, object value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}

public class Store
{
    public Dictionary<string, object> variables { get; } = new();
    public Store previous;
}

public class Map
{
    public Dictionary<string, object> layers { get; } = new();
    public int d1;
    public int d2;

    public Map(int d1, int d2)
    {
        this.d1 = d1;
        this.d2 = d2;
    }
}




public class VestaVisitor : VestaBaseVisitor<object?>
{
    private Dictionary<string, object> Variables { get; } = new();
    private Store store = new Store();
    
    
    
    public VestaVisitor()
    {
        Variables["Write"] = new Func<object?[], object?>(Write);
        Variables["WriteArray"] = new Func<object?[], object?>(WriteArray);

        store.variables["Write"] = new Func<object?[], object?>(Write);
   

    }

    public override object? VisitBlock(VestaParser.BlockContext context)
    {
        /* New scope */
        Store tempStore = new Store();
        tempStore.previous = store;
        store = tempStore;
        /* Visit */
        var arr = context.line().ToArray();
        for (int i = 0; i < arr.Length; i++)
        {
            Visit(arr[i]);
        }
        /* Return to previous scope */
        store = store.previous;
        return null;
    }

    private object? WriteArrayList(ArrayList arr)
    {
        for (int i=0; i<arr.Count;i++)
        {
            //Adding ',' between elements
            if (i != 0)
            {
                Console.Write(",");
            }
            
            if (arr[i] is ArrayList arr2)
            {
                WriteArrayList(arr2);
            }
            else
            {
                Console.Write(arr[i]);
            }
        }
        return null;
    }
    private object? WriteArray(object?[] args)
    {
        foreach (var array in args)
        {
            if (array is ArrayList arr)
            {
                WriteArrayList(arr);
                Console.WriteLine("");  //Clear line
            }
        }
        return null;
    }
    private object? Write(object?[] args)
    {
        foreach (var arg in args)
        {
            Console.WriteLine((arg));
        }
        
        return null;
    }
    

    public override object? VisitFunctionCall(VestaParser.FunctionCallContext context)
    {
        var name = context.IDENTIFIER().GetText();
        var args = context.expression().Select(e => Visit(e)).ToArray();

        if (!Variables.ContainsKey(name))
        {
            throw new Exception($"Function {name} is not defined");
        }

        if (Variables[name] is Func<object?[], object?> func)
        {
            return func(args);
        }

        throw new Exception($"{name} is not a function");
    }

    object? quickAssignArr(object ass, object value)
    {
        //Check if elements match value
        Type assType = ass.GetType();
        Type valueType = value.GetType();
        if (assType==valueType) //If same type
        {
            if (ass is Array arrAss && value is Array arrVal)   //If arrays
            {
                if (getDepth(arrAss) == getDepth(arrVal)) //If same amount of dimensions
                {
                    if (arrAss.Length == arrVal.Length) //If same length
                    {
                        if (assType.GetElementType() == valueType.GetElementType()) //If last element shares type
                        {
                            return value;
                        }
                        throw new Exception($"To quickAssign both arrays must have the same type");
                    }
                    throw new Exception($"To assign an array it must of the same size");
                }
            }
            else //If same type ready to assign
            {
                return value;
            }
        }
        if (ass is Array arrAss2)
        {
            object[] result = new Object[arrAss2.Length]; 
            for (int i = 0; i < arrAss2.Length; i++)
            {
                result[i] = quickAssignArr(arrAss2.GetValue(i), value);
            }
            return result;
        }
        //If values are not the same type, 
        throw new Exception($"Cannot quickassign values to a type {ass.GetType()}");
    }

    int getDepth(Array arr)
    {
        int count = 0;
        object? holderArr = arr;
        while (holderArr is Array recursiveArr)
        {
            holderArr = recursiveArr.GetValue(0);
            count++;
        }
        return count;
    }
    
    
    /* identifierType IDENTIFIER '=' expression;
     identifierType: BASETYPE ('[' expression ']')*;
     BASETYPE: 'int' | 'bool' ;
     */
    public override object? VisitBaseDeclaration(VestaParser.BaseDeclarationContext context)
    {
        var varName = context.IDENTIFIER().GetText();

        var value = Visit(context.expression());
       
        if(store.variables.ContainsKey(varName))  //No overriding current variables
        {
            throw new Exception($"The variable {varName} is already defined");
        }

        var type = Visit(context.identifierType());
        
        
        if (type is ArrDesc arrDesc) //If is an array
        {
            store.variables[varName]=multiDimensionArrGen(arrDesc.dimensions, value, arrDesc.dimensions.Length);
        }
        else if (type is Map map)
        {
            //Map knows the dimensiosn of the array
            //Value has a list of layerDesc with values, and identifers
            if (value is Array arrOfLayers)
            {
                for (int i = 0; i < arrOfLayers.Length; i++)
                {
                    if (arrOfLayers.GetValue(i) is LayerDesc layerDesc)
                    {
                        int[] dimArr = { map.d1, map.d2 };
                        map.layers[layerDesc.identifier] = multiDimensionArrGen(dimArr, value, 2);
                    }
                }
            }
            store.variables[varName] = map;
        }
        else if (type is int n)  //If type is int
        {
            checkType<int>(value);
            store.variables[varName] = value;
        }
        else if (type is bool b) //If type is bool
        {
            checkType<bool>(value);
            store.variables[varName] = value;
        }
        else
        {
            throw new Exception($"Invalid type");
        }

        return null;
    }

    /* mapDeclaration: 'map[' expression ']['expression ']' IDENTIFIER '={' mapLayer (';'mapLayer)*  '}';
       mapLayer: identifierType IDENTIFIER ('=' expression)?; */
    public override object? VisitMapDeclaration(VestaParser.MapDeclarationContext context)
    {
        var mapName = context.IDENTIFIER().GetText();
        if (Visit(context.expression(0)) is not int n1)
        {
            throw new Exception($"Map dimensions must be described with integer");
        }
        if (Visit(context.expression(1)) is not int n2)
        {
            throw new Exception($"Map dimensions must be described with integer");
        }
        Map resultMap = new Map(n1, n2);    
        int[] dimensionsArr = {n1, n2};

        //Need to parse each array element.
        var layerContextArr = context.mapLayer().ToArray();
        for (int i = 0; i < layerContextArr.Length; i++)
        {
            if (Visit(layerContextArr[i]) is not LayerDesc layerDesc)
            { throw new Exception($"Layer not parsed correctly"); }

            resultMap.layers[layerDesc.identifier] = multiDimensionArrGen(dimensionsArr, layerDesc.value, 2);
        }
        store.variables[mapName] = resultMap;
        return null;
    }
    Type getBaseType(object? o)
    {
        while (o is Array arr)
        {
            o = arr.GetValue(0);
        }

        return 0.GetType();
    }
    //Returns layer identifier, and value to assign layer to
    public override object? VisitMapLayer(VestaParser.MapLayerContext context)
    {
        var type = Visit(context.identifierType());
        string identifier = context.IDENTIFIER().GetText();
        var arrValue = context.expression();
        object result=0;
        
        if (context.expression() != null) //If expression exists, I.E. declared with variable
        {
            var value = Visit(arrValue);
            if (type is ArrDesc arrDesc)
            {
                if (getBaseType(value)== arrDesc.typeBase.GetType())
                {
                    result = value;
                }
                else
                {
                    throw new Exception($"Array variable types do not match layer type");
                }
            }
            //Where typebase is set the default of the value
            else if (type is int)
            {
                checkType<int>(value);
                result = value;
            }
            else if (type is bool)
            {
                checkType<bool>(value);
                result = value;
            }
            else { throw new Exception($"Cannot declare layer with type {type.GetType()}"); }
        }
        else //Declared without variable
        {
            if (type is ArrDesc arrDesc)
            {result=  multiDimensionArrGen(arrDesc.dimensions, arrDesc.typeBase, arrDesc.dimensions.Length); }
            //Where typebase is set the default of the value
            else if (type is int) { result = 0; }
            else if (type is bool) { result = false; }
            else { throw new Exception($"Cannot declare layer with type {type.GetType()}"); }
        }

        return new LayerDesc(identifier, result);
    }

    object? multiDimensionArrGen(Array dimensions, object? assignValue, int dimensionsLength)
    {
        if (assignValue is Array arrAss)
        {
            if (getDepth(arrAss) == dimensionsLength)
            {
                return assignValue;
            }
            //Deal with error case
            if (getDepth(arrAss) > dimensionsLength)
            {
                throw new Exception($"Cannot assign an array with {getDepth(arrAss)} dimensions to an array with {dimensionsLength}");
            }
        }
        if (dimensionsLength == 0)  //if no more arrays, return assignValue
        {
            return assignValue;
        }
        if (dimensions.GetValue(dimensions.Length-dimensionsLength) is int n)   
        {//Calculates current dimension to work on
            object[] result = new object[n];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = multiDimensionArrGen(dimensions, assignValue, dimensionsLength - 1);
            } //Calls itself with -1 dimensions

            return result;
        }
        throw new Exception($"Must describe array with type int");
    }
    
    bool checkType<T>(object? o)    //Throws an error if not correct type
    {
        if (o is T)
        {
            return true;
        }
        throw new Exception($"{o} is of type {o.GetType()}, not {typeof(T)}");
    }
    
    /* identifierType: TYPE ('['expression']')*;
     TYPE: 'int' | 'bool' | 'map'; */
    public override object? VisitIdentifierType(VestaParser.IdentifierTypeContext context)
    {
        var myArr = context.expression().ToArray();
        object? result;
        bool handleMap = false;
        string baseType = context.TYPE().GetText();
        
        if (baseType == "int") { result= 0; }
        else if (baseType == "bool") { result= false; }
        else if (baseType == "map") { handleMap = true; result = 0; }
        else{ throw new Exception($"type {baseType} undefined"); }

        if (handleMap) //If map
        {
            if (myArr.Length == 2)
            {
                if (Visit(myArr[0]) is int n1 && Visit(myArr[1]) is int n2)
                {
                    return new Map(n1, n2);
                }
                throw new Exception($"Index paramters must be of type int when declaring maps");
            }
            throw new Exception($"Maps must have 2 dimensions");
        }
        // If not map
        if (myArr.Length == 0)
        {
            return result;
        }
        //if array, need to know type, and to know length of dimensions. MyArr contains dimensions by needs to know type

        object[] arrResult = new object[myArr.Length];
        for (int i = 0; i < myArr.Length; i++)
        {
            arrResult[i] = Visit(myArr[i]);
        }
        return new ArrDesc(result ,arrResult);
    }

    public override object? VisitAssignment(VestaParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression());
        if (!store.variables.ContainsKey(varName))
        {
            Store tempStore = store;
            while (tempStore.previous != null)
            {
                tempStore = tempStore.previous;
                if (tempStore.variables.ContainsKey(varName))
                {
                    if (tempStore.variables[varName] is Array arr) //if array need array Assign
                    {
                        tempStore.variables[varName] = quickAssignArr(arr, value);
                        return null;
                    }

                    if (tempStore.variables[varName].GetType() == value.GetType()) //If not array and types match
                    {
                        tempStore.variables[varName] = value;
                        return null;
                    }

                    throw new Exception(
                        $"{value} cannot be assigned to {varName} as it is of type {value.GetType()} not {tempStore.variables[varName].GetType()}");
                }
            }

            throw new Exception($"Variable {varName} is not defined");
        }
        if(store.variables[varName] is Array arr2)   //if array need array Assign
        {
            store.variables[varName] = quickAssignArr(arr2, value);
            return null;
        }
        if (store.variables[varName].GetType() == value.GetType())
        {
            store.variables[varName] = value;
            return null;
        }
        throw new Exception(
            $"{value} cannot be assigned to {varName} as it is of type {value.GetType()} not {store.variables[varName].GetType()}");
    }


    public override object? VisitConstant(VestaParser.ConstantContext context)
    {
        if (context.INTEGER() is { } i)
        {
            return int.Parse(i.GetText());
        }
        if (context.BOOL() is { } b)
        {
            return (b.GetText() == "true");
        }
        throw new NotImplementedException();
    }


    private bool checkForValidArrayEntry(object? variable)
    {
        return true;
    }
    
    /* '{' (expression',')* '}' */
    public override object? VisitArrayExpression(VestaParser.ArrayExpressionContext context)
    {
        
        var arr = context.expression().ToArray();
        object[] result = new object[arr.Length];
        if (arr.Length > 0)
        {
            int count;
            var holder = Visit(arr[0]);
            if (checkForValidArrayEntry(holder))
            {
                result[0]=holder;
                for (count = 1; count < arr.Length; count++)
                {
                    holder = Visit(arr[count]);
                    if(result[count-1].GetType() != holder.GetType())
                    {
                        throw new Exception(
                            $"$Cannot create an array with types {result[count-1].GetType()} and {holder.GetType()}");
                    }
                    result[count]=(holder);
                }
            }
        }
        return result;
    }
    

    public override object? VisitIdentifierExpression(VestaParser.IdentifierExpressionContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        
        if (!store.variables.ContainsKey(varName))
        {
            Store tempStore = store;
            while (tempStore.previous!=null)
            {
                tempStore = tempStore.previous;
                if (tempStore.variables.ContainsKey(varName))
                {
                    return tempStore.variables[varName];
                }
            }

            throw new Exception($"Variable {varName} is not defined");
        }
        return store.variables[varName];
    }
    /*  expression ('[' expression ']')+      #arrayIdentifierExpression */
    public override object? VisitArrayIdentifierExpression(VestaParser.ArrayIdentifierExpressionContext context)
    {

        var numberArr = context.expression().ToArray();

        var myArray = Visit(numberArr[0]);   //Main array which contains element
        
        object? holder;
        
        if (!(myArray is Array))
        {
            throw new Exception($"{myArray} is not an array, but of type {myArray.GetType()}");
        }
        for(int i=1; i<numberArr.Length; i++)
        {
            holder = Visit(numberArr[i]);   //Visit next number. Arr[5] -> 5
            if (holder is int n && myArray is Array arr)
            {
                myArray = arr.GetValue(n);
            }
            else
            {
                throw new Exception($"Index number must be of type int not {holder.GetType()}");
            }
        }
        return myArray;
    }

    public override object? VisitAdditionExpression(VestaParser.AdditionExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.addOp().GetText();
        return op switch
        {
            "+" => Add(left, right),
            "-" => Subtract(left, right),
            _ => throw new NotImplementedException()
        };
    }

    private object? Add(object? left, object? right)
    {
        if (left is int l && right is int r)
        {
            return l + r;
        }

        if (left is bool lb && right is bool rb)
        {
            return (lb || rb);
        }
        
        if (left is Array arr1 && right is Array arr2)
        {
            return basicListOperations(arr1, arr2, Add);
        }

        if (left is Array arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Add);
        }

        if (right is Array arr4)
        {
            return arrayApplyToAllRight(arr4, left, Add);
        }
        
        throw new Exception($"cannot add values of types {left?.GetType()} and {right?.GetType()} <3");
    }
    
    private Array arrayApplyToAllRight(Array arr, object? leftside, Func<object?, object?, object?> function)
    {
        object[] result = new object[arr.Length];
        for(int i=0; i<arr.Length; i++)
        {
            result[i]=(function(leftside, arr.GetValue(i)));
        }
        return result;
    }
    
    private Array arrayApplyToAllLeft(Array arr, object? rightside, Func<object?, object?, object?> function)
    {
        object[] result = new object[arr.Length];
        for(int i=0; i<arr.Length; i++)
        {
            result[i]=(function(arr.GetValue(i), rightside));
        }
        return result;
    }
    private Array basicListOperations(Array arr1, Array arr2, Func<object?, object?, object?> function)
    {
        object[] result = new object[arr1.Length];

        if (!(arr1.Length == arr2.Length))
        {
            throw new Exception($"The length of arrays that are added must be the same before an operation can be performed");
        }
        
        for (int i=0;i < arr1.Length; i++)
        {
            result[i]=(function(arr1.GetValue(i), arr2.GetValue(i)));
        }

        return result;
    }
    
    
    
    private object? Subtract(object? left, object? right)
    {
        if (left is int l && right is int r)
        {
            return l - r;
        }

        if (left is bool lb && right is bool rb)
        {
            return (lb && rb);
        }

        if (left is Array arr1 && right is Array arr2)
        {
            return basicListOperations(arr1, arr2, Subtract);
        }
        if (left is Array arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Subtract);
        }

        if (right is Array arr4)
        {
            return arrayApplyToAllRight(arr4, left, Subtract);
        }

        throw new Exception($"cannot subtrackt values of types {left?.GetType()} and {right?.GetType()} <3");
    }


    public override object? VisitMultiplicationExpression(VestaParser.MultiplicationExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.multOp().GetText();
        return op switch
        {
            "*" => Multiply(left, right),
            "/" =>Divide(left, right),
            _ => throw new NotImplementedException()
        };
    }

    private object? Multiply(object? left, object? right)
    {
        if (left is int l && right is int r)
        {
            return l * r;
        }

        if (left is Array arr1 && right is Array arr2)
        {
            return basicListOperations(arr1, arr2, Multiply);
        }
        if (left is Array arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Multiply);
        }

        if (right is Array arr4)
        {
            return arrayApplyToAllRight(arr4, left, Multiply);
        }
        
        throw new Exception($"cannot multiply values of types {left?.GetType()} and {right?.GetType()} <3");
    }
    private object? Divide(object? left, object? right)
    {
        if (left is int l && right is int r)
        {
            return l / r;
        }

        if (left is Array arr1 && right is Array arr2)
        {
            return basicListOperations(arr1, arr2, Divide);
        }
        if (left is Array arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Divide);
        }

        if (right is Array arr4)
        {
            return arrayApplyToAllRight(arr4, left, Divide);
        }
        throw new Exception($"cannot divide values of types {left?.GetType()} and {right?.GetType()} <3");
    }

    public override object? VisitRandomExpression(VestaParser.RandomExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));
        if (left is int l && right is int r)
        {
            Random rnd = new Random();
            var result = rnd.Next(l, r+1);
            return result;
        }

        throw new Exception($"Cannot generate values between types");

    }

    public override object? VisitIfBlock(VestaParser.IfBlockContext context)
    {
        if (isTrue(Visit(context.expression())))
        {
            Visit(context.block());
        }

        return null;
    }
    public override object? VisitWhileBlock(VestaParser.WhileBlockContext context)
    {

        while (isTrue(Visit(context.expression())))
        {
            Visit(context.block());
 
        }

        return null;
    }
    
    /* 'chance{' (expression ':' block)+ '}' */
    public override object? VisitChanceBlock(VestaParser.ChanceBlockContext context)
    {
        var arrExpr = context.expression().ToArray();
        int[] arrVal = new int[arrExpr.Length];
        var arrBlck = context.block().ToArray();
        int r = 0, i, counter=0;
        for (i=0; i<arrExpr.Length; i++)
        {
            if (Visit(arrExpr[i]) is int n)
            {
                r += n;
                arrVal[i] = n;
            }
            else { throw new Exception($"Chance table point values must be of type int"); }
        }
        Random rnd = new Random();
        r = rnd.Next(0, r);     //Generates an integer between 0 and sum(v_0...v_n)-1
        i = 0;
        bool flag = false;
        while (!flag)
        {
            if (arrVal[i]+counter > r) { flag = true; Visit(arrBlck[i]); }
            else { counter += arrVal[i]; i++;}
        }
        return null;
    }




    private bool isTrue(object? value)
    {
        if (value is bool b)
            return b;
        throw new Exception("Value is not boolean");
    }

    public override object? VisitParenthesizedExpression(VestaParser.ParenthesizedExpressionContext context)
    {
        return Visit(context.expression());
    }

    public override object? VisitCompareExpression(VestaParser.CompareExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));
        
        var op = context.compareOp().GetText();
        return op switch
        {
            "==" => CompareIntFunction(left, right, (x, y) => x==y ),
            "!=" => CompareIntFunction(left, right, (x, y) => x!=y ),
            "<" => CompareIntFunction(left, right, (x, y) => x<y ),
            ">" => CompareIntFunction(left, right, (x, y) => x>y ),
            "<=" => CompareIntFunction(left, right, (x, y) => x<=y ),
            ">=" => CompareIntFunction(left, right, (x, y) => x>=y ),
            _ => throw new NotImplementedException()
        };
    }

    private object? CompareIntFunction(object? left, object? right, Func<int?,int? , bool> comparer)
    {

        if (left is int l && right is int r)
        {
            return (comparer(l, r));
        }

        throw new Exception($"$Cannot compare between types {left.GetType()} and {right.GetType()} <3");
    }
    private object? CompareFunction(object? left, object? right, Func<object? , object?, bool> comparer)
    {

        if (left is int l && right is int r)
        {
            return (comparer(Convert.ToInt32(l), Convert.ToInt32(r)));
        }

        if (left is string || right is string)
        {
            return (comparer(left.ToString(), right.ToString()));
        }

        throw new Exception($"$Cannot compare between types {left.GetType()} and {right.GetType()}");
    }
}


   