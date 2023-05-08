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

public class ParamDesc
{
    public object? typeBase;
    public string paramName;

    public ParamDesc(object? typeBase, string paramName)
    {
        this.typeBase = typeBase;
        this.paramName = paramName;
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
        store.variables["Write"] = new Func<object?[], object?>(Write);
        store.variables["WriteArr"] = new Func<object?[], object?>(WriteArray);
        
        store.variables["write"] = new Func<object?[], object?>(Write);
        store.variables["writeArr"] = new Func<object?[], object?>(WriteArray);
    }
    private object? WriteArray(Array arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr.GetValue(i) is Array arr2)
            {
                Console.WriteLine("");
                WriteArray(arr2);
            }
            else
            {
                Console.Write(arr.GetValue(i) +" ");
            }
        }

        Console.WriteLine("");

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

    /* functionDclr: identifierType IDENTIFIER '('(funcParams)?')' funcBody; */
    public override object? VisitFunctionDclr(VestaParser.FunctionDclrContext context)
    {
        var funcName = context.IDENTIFIER().GetText();

        object returnType = Visit(context.identifierType());
        
        //Gather arr of funcParams
        var param = Visit(context.funcParams());
        if (param is not Array paramArr) { throw new Exception($"Could not find parameters");}
        //Parse it as paramDesc
        ParamDesc[] paramDescs = parseArray<ParamDesc>(paramArr);
        
        //Find store of declaration
        Store storeOfDclr = store;
        
        //Need to handle the scope, then execute the funcBody, then reset scope, and return returnstmt
        Func<object?[], object?> newFunction = delegate(object?[] objects)
        {
            object? result;
            //Keep the current store so it can be put back later
            Store currentScope = store;
            //Assign new store, 
            Store tempStore = new Store();
            tempStore.previous = storeOfDclr;
            store = tempStore;
            //Handle that objects addeed match the types, and add them to the store
            if (objects.Length != paramDescs.Length) { throw new Exception($"Invalid amount of arguments");}
            for (int i = 0; i < objects.Length; i++)
            {
                //If array need to check that array have same amount of dimensions and baseType
                if (objects[i] is Array && paramDescs[i].typeBase is ArrDesc)
                {
                    if (getDepth((Array)objects[i]) == ((ArrDesc)(paramDescs[i].typeBase)).dimensions.Length)
                    {
                        if (getBaseType((Array)objects[i]) == ((ArrDesc)(paramDescs[i].typeBase)).typeBase.GetType())
                        {
                            store.variables[paramDescs[i].paramName] = objects[i];
                        }
                        else
                        {
                            throw new Exception($"Input array must have the same baseType");
                        }
                    }
                    else
                    {
                        throw new Exception($"Input array must have the same amount of dimensions");
                    }
                }
                     
                else if (objects[i].GetType() == paramDescs[i].typeBase.GetType())
                {
                    store.variables[paramDescs[i].paramName] = objects[i];
                }

                else
                {
                    throw new Exception($"Paramter {i} of incorrect type");
                }
            }
            //Handle funcBody
            result = Visit(context.funcBody());
            
            //Check if result matches expected value.
            
            
            //Reset scope
            store = currentScope;
            return result;
        };

        storeOfDclr.variables[funcName] = newFunction;
        
        return null;
    }

    T[] parseArray<T> (Array arrayToParse)
    {
        T[] returnArr = new T[arrayToParse.Length];
        for (int i = 0; i < arrayToParse.Length; i++)
        {
            if (arrayToParse.GetValue(i) is T t)
            {
                returnArr[i] = t;
            }
            else
            { throw new Exception($"Array contained element not of type {typeof(T)}");}
        }
        return returnArr;
    }
    
    /* funcParams: parameter (',' parameter)* ; */
    public override object? VisitFuncParams(VestaParser.FuncParamsContext context)
    {
        var paramContextArr = context.parameter().ToArray();
        ParamDesc[] paramsArr = new ParamDesc[paramContextArr.Length];
        for (int i = 0; i < paramContextArr.Length; i++)
        {
            if (Visit(paramContextArr[i]) is ParamDesc param)
            {
                paramsArr[i] = param;
            }
            else { throw new Exception($"Could not parse as a parameter"); }
        }
        return paramsArr;
    }
    /* parameter: identifierType IDENTIFIER; */
    public override object? VisitParameter(VestaParser.ParameterContext context)
    {
        return new ParamDesc(Visit(context.identifierType()), context.IDENTIFIER().GetText());
    }
    /* funcBody: '{' line*  returnStmt '}'; */
    public override object? VisitFuncBody(VestaParser.FuncBodyContext context)
    {
        //Visit all lines
        var lineArr = context.line().ToArray();
        for (int i = 0; i < lineArr.Length; i++)
        {
            Visit(lineArr[i]);
        }
        //Return rtrn stmt
        return Visit(context.returnStmt());
    }

    /* returnStmt:  'return' expression';'; */
    public override object? VisitReturnStmt(VestaParser.ReturnStmtContext context)
    {
        return Visit(context.expression());
    }

    public override object? VisitFunctionCall(VestaParser.FunctionCallContext context)
    {
        var name = context.IDENTIFIER().GetText();
        var args = context.expression().Select(e => Visit(e)).ToArray();

        var relevantStore = getStoreForVar(name);
        
        if (!relevantStore.variables.ContainsKey(name))
        {
            throw new Exception($"Function {name} is not defined");
        }

        if (relevantStore.variables[name] is Func<object?[], object?> func)
        {
            return func(args);
        }

        throw new Exception($"{name} is not a function");
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
     identifierType: TYPE ('[' expression ']')*;
     TYPE: 'int' | 'bool' | 'map' ;
     */
    public override object? VisitDeclaration(VestaParser.DeclarationContext context)
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
            if (arrDesc.typeBase.GetType() == getBaseType(value))
            {
                store.variables[varName] = multiDimensionArrGen(arrDesc.dimensions, value, arrDesc.dimensions.Length);
            }
            else throw new Exception($"Types do not match");
        }
        else if (type is Map map)
        {
            checkType<Map>(value);
            store.variables[varName] = value;
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
    /* | expression'.'IDENTIFIER               #mapGetLayerExpression */
    public override object? VisitMapGetLayerExpression(VestaParser.MapGetLayerExpressionContext context)
    {
        object myMap = Visit(context.expression());
        var layerName = context.IDENTIFIER().GetText();

        if (myMap is Map map)
        {
            if (map.layers.ContainsKey(layerName))
            {
                return map.layers[layerName];
            }

            throw new Exception($"Unknown layer name {layerName}");
        }

        throw new Exception($"Can only get layers of maps, not type {myMap.GetType()}");
    }

    /* mapDeclaration: '[' expression ','expression ']' {' mapLayer (';'mapLayer)*  '}';
       mapLayer: identifierType IDENTIFIER ('=' expression)?; */

    public override object? VisitMapExpression(VestaParser.MapExpressionContext context)
    {
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
        return resultMap;
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
        else if (baseType == "map") { handleMap = true; result = new Map(0,0); }
        else{ throw new Exception($"type {baseType} undefined"); }

        if (handleMap) //If map need to make sure we don't add to array
        {
            if (myArr.Length != 0)
            {
                throw new Exception($"Cannot have arrays with maps");
            }
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

    Store getStoreForVar(string varName)
    {
        if (!store.variables.ContainsKey(varName))
        {
            Store tempStore = store;
            while (tempStore.previous != null)
            {
                tempStore = tempStore.previous;
                if (tempStore.variables.ContainsKey(varName))
                {
                    return tempStore;
                }
            }
            throw new Exception($"Variable {varName} is not defined");
        }
        return store;
    }
    
    public override object? VisitBaseAssignment(VestaParser.BaseAssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression());

        var relevantStore = getStoreForVar(varName);
        var originalValue = relevantStore.variables[varName];
        if (originalValue is Array arr) //if array need array Assign
        {
            if (getBaseType(arr) == getBaseType(value))
            {
                relevantStore.variables[varName] = quickAssignArr(arr, value);
            }
            else throw new Exception($"Types do not match");
        }
        else if(originalValue.GetType()==value.GetType())
        {
            relevantStore.variables[varName] = value;
        }
        else{
            
            throw new Exception(
                $"{value} cannot be assigned to {varName} as it is of type {value.GetType()} not {relevantStore.variables[varName].GetType()}"); }


        return null;
    }
    /* arrayAssignment: IDENTIFER '[' expression (',' expression)* ']' '=' expression ; */
    public override object? VisitArrayAssignment(VestaParser.ArrayAssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var expressionArr = context.expression().ToArray();
        var assignValue = Visit(expressionArr[expressionArr.Length - 1]); //Get last expression
      
        var relevantStore = getStoreForVar(varName);
        
        //generate list of index values
        int[] indexArr = new int[expressionArr.Length - 1];
        for (int i = 0; i < indexArr.Length; i++)
        {
            if (Visit(expressionArr[i]) is int n)
            {
                indexArr[i] = n;
            }
            else
            {
                throw new Exception($"Indexes must be described with integers");
            }
        }
        if (relevantStore.variables[varName] is Array originalVar)
        {
            relevantStore.variables[varName] = assignElementOfArr(indexArr, originalVar, assignValue, 0);
            return null;
        }
        throw new Exception($"$Cannot get element of non array element");
    }

    object insertPrefab(object bigArray, object insertArray, Array indexList, int indexCounter)
    {

        if (bigArray is Array bigArr && insertArray is Array insertArr)
        {
            if (indexList.GetValue(indexCounter) is int indexOffset)
            {
                for (int i = 0; i < insertArr.Length; i++)
                {
                    bigArr.SetValue(insertPrefab(bigArr.GetValue(i+indexOffset), insertArr.GetValue(i), indexList, indexCounter+1), i + indexOffset);
                    //Calls the same algorithm on each of the relevant 
                }
                return bigArr;
            }

            throw new Exception($"Indexes must be parameterized with an integer type");
        }
        else if (bigArray.GetType() == insertArray.GetType()) //If assign time
        {
            return insertArray;
        }

        throw new Exception($"Cannot add prefab with conflicting type");

    }
    
    object assignElementOfArr(Array dimensionsArr, object arr, object value, int dimensionCounter)
    {

        
        if (dimensionCounter == dimensionsArr.Length) //If found right element
        {
            return quickAssignArr(arr, value);  
        }

        if (arr is Array initialArr)
        {
            if (value is Array assignArr) //Check prefab condition. Aka original array depth matches that of value 
            {
                int initialArrDepth = getDepth(initialArr);
                if (initialArrDepth == getDepth(assignArr)) //Must have same amount of dimensions
                {
                    if (initialArrDepth == dimensionsArr.Length - dimensionCounter) //IF a precise location is found
                    {
                        //Check if lengths go over initial arr

                        return insertPrefab(arr, value, dimensionsArr, dimensionCounter);
                        //Assign to arr


                    }

                    throw new Exception($"When assigning prefabs the exact index must be described");
                }
            }
            
            //If not prefab

            if (dimensionsArr.GetValue(dimensionCounter) is int index)
            {
                initialArr.SetValue(
                    assignElementOfArr(dimensionsArr, initialArr.GetValue(index), value, dimensionCounter + 1),
                    index);
                return initialArr;
            }
            throw new Exception($"Indexes must be defined with type int");
        }

        throw new Exception($"Too many index references, tried to find element of type {arr.GetType()}");
    }

    /* mapAssignment:  IDENTIFIER '.' IDENTIFIER ('[' expression (',' expression)* ']') '=' expression;*/
    public override object? VisitMapAssignment(VestaParser.MapAssignmentContext context)
    {
        var varName = context.IDENTIFIER(0).GetText();
        var layerName = context.IDENTIFIER(1).GetText();
        var expressionArr = context.expression().ToArray();
        var assignValue = Visit(expressionArr[expressionArr.Length - 1]); //Get last expression

        var relevantStore = getStoreForVar(varName);

        if (relevantStore.variables[varName] is not Map map)
        {
            throw new Exception($"Cannot get layer of type {relevantStore.variables[varName].GetType()}");
        }

        var layer = map.layers[layerName];

        if (expressionArr.Length  == 1)  //If only valueExpression. Aka no index expressions
        {
            map.layers[layerName] = quickAssignArr(map.layers[layerName], assignValue);
            relevantStore.variables[varName] = map;
            return null;
        }
        //If index:
        //generate list of index values
        int[] indexArr = new int[expressionArr.Length - 1];
        for (int i = 0; i < indexArr.Length; i++)
        {
            if (Visit(expressionArr[i]) is int n)
            {
                indexArr[i] = n;
            }
            else
            {
                throw new Exception($"Indexes must be described with integers");
            }
        }

        if (layer is Array layerArr)
        { //Parse as Array
            map.layers[layerName] = assignElementOfArr(indexArr, layerArr, assignValue, 0);
            relevantStore.variables[varName] = map;
            return null;
        }

        throw new Exception($"Layers must of type array"); //I don't think this could happen but safety measures never hurt





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
            while (tempStore.previous != null)
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

    public override object? VisitNotExpression(VestaParser.NotExpressionContext context)
    {
        if (Visit(context.expression()) is bool b)
        {
            return !b;
        }

        throw new Exception($"Not exceptions must be with expressions of type bool");
    }
/*forLoop: 'for' '(' declaration | assignment ';' expression ';' assignment ')';  */
    public override object? VisitForLoop(VestaParser.ForLoopContext context)
    {
        var dclr = context.declaration();
        var ass = context.assignment().ToArray();
        var expression = context.expression();
        var updateAss = ass[ass.Length - 1]; //Last ass is update
        if (ass.Length == 2) //If initial is assign
        {
            Visit(ass[0]);
        }
        else //IF is dclr
        {
            Visit(dclr);
        }
        
        if(Visit(expression) is bool flag){}
        else
        {
            throw new Exception($"Must be a boolean value for for loops");
        }
        while (flag)
        {
            Visit(context.block());
            Visit(updateAss);
            if (Visit(expression) is bool b)
            {
                flag = b;
            }
            else
            {
                throw new Exception($"Must be a boolean value for for loops");
            }
        }
        return null;
    }

    public override object? VisitNegExpression(VestaParser.NegExpressionContext context)
    {
        if (Visit(context.expression()) is int n)
        {
            return -n;
        }

        throw new Exception($"Not exceptions must be with expressions of type int");
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