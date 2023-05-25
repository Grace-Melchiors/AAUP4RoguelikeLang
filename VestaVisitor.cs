using System.Collections;
using System.Data;
using System.Diagnostics.Metrics;
using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Xml.Schema;
using Antlr_language.Content;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
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
    public Dictionary<string, object> layers { get; set; } = new();
    public int d1;
    public int d2;

    public Map(int d1, int d2)
    {
        this.d1 = d1;
        this.d2 = d2;
    }
    public Map clone()
    {
        Map returnMap = new Map(d1, d2);
        returnMap.layers = layers.ToDictionary(entry => entry.Key, entry => entry.Value);
        return returnMap;
    }
}

public class MLCGRandom
{

    //private int a= 1664525; //Knuth
    private  int a = 69069; //Marsagli
    private int c = 1;
    private long m = (long)Math.Pow(2,32);
    private long seed = 0;

    public MLCGRandom()
    {
        seed = (long)(DateTime.Now.Ticks % m);
    }
    public MLCGRandom(long seed)
    {
        this.seed = seed;
    }

    long getNext()
    {
        seed = (a * seed + 1) % m;
        return seed;
    }

    public int getRand(int min, int max)
    {
        if (max < min) throw new Exception($"Cannot generate random value where max is more than min");
        return (int)(min + getNext() % (max+1 - min));
    }
}

public class VestaVisitor : VestaBaseVisitor<object?>
{
    private Dictionary<string, object> Variables { get; } = new();
    private Store store = new Store();
    public Dictionary<string, Dictionary<string, Func<object?[], object?>>> libraries { get; } = new();
    
    private MLCGRandom random = new MLCGRandom();

    bool verbose = false;

    public VestaVisitor(bool verbose)
    {
        this.verbose = verbose;
    }

    private object? Seed(object[] args)
    {
        if (args.Length != 1) throw new Exception($"This function only takes 1 parameter");
        if (args.GetValue(0) is not int n) throw new Exception($"Seed must be given with int not {args.GetValue(0).GetType()}");
        random = new MLCGRandom(n);
        return null;
    }
    
    private object? Print(object arg)
    {
        if (verbose) {
            if (arg is Array arr)
            {
                foreach (object? o in arr) Print(o);
                    Console.WriteLine("");
            }
            else Console.Write(arg+" ");
        }

        return null;
    }

    object? RandomFunc(object?[] args)
    {
        return random.getRand((int)args[0], (int)args[1]);
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
    

    object getElementFromIndex(int[] dimensions, object array)
    {
        for(int i=0; i<dimensions.Length; i++)
        {

            if (array is Array arr)
            {
                array = arr.GetValue(dimensions[i]);
            }
            else
            {
                throw new Exception($"Too many indexes.");
            }
        }
        return array;
    }
    /* library: 'using' IDENTIFIER ';'; */
    public override object? VisitLibrary(VestaParser.LibraryContext context)
    {
        Dictionary<string, Func<object?[], object?>> newLib = new();
        var libName = context.IDENTIFIER().GetText();
        switch (libName)
        {
            case "Stdlib":
                newLib["print"] = new Func<object?[], object?>(Print);
                newLib["seed"] = new Func<object?[], object?>(Seed);
                newLib["random"] = new Func<object?[], object?>(RandomFunc);
                break;
            default:
                throw new NotImplementedException();
        }

        libraries[libName] = newLib;
        
        
        return base.VisitLibrary(context);
    }

    /* functionDclr: returnType IDENTIFIER '('(funcParams)?')' funcBody; */
    public override object? VisitFunctionDecl(VestaParser.FunctionDeclContext context)
    {
        var funcName = context.IDENTIFIER().GetText();

        object returnType = Visit(context.returnType());
        
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
            compareReturnResult(result, returnType);
            
            //Reset scope
            store = currentScope;
            if (result is Map mapReturn) return mapReturn.clone();
            return result;
        };

        storeOfDclr.variables[funcName] = newFunction;
        
        return null;
    }

    bool compareReturnResult(object result, object returnType)
    {
        //If array
        if (returnType is ArrDesc arrDesc)
        {
            if (result is not Array arr) throw new Exception($"Expected return type to be an {arrDesc.typeBase.GetType()} array");
            if (arrDesc.dimensions.Length != getDepth(arr)) throw new Exception($"Amount of dimensions does not match in return parameter");
            if (arrDesc.typeBase.GetType()!=getBaseType(arr)) throw new Exception($"Expected return type to be an {arrDesc.typeBase.GetType()} array");

            return true;
        }
        
        //If map
        if (returnType is Map returnMap)
        {
            if (result is not Map resultMap)
                throw new Exception($"Expected return type of map, not {result.GetType()}");

            foreach (var key in returnMap.layers.Keys)
            {
                if (!resultMap.layers.ContainsKey(key)) throw new Exception($"Returned map does not contain key {key}");
                if (!compareType(resultMap.layers[key], returnMap.layers[key]))
                    throw new Exception($"Types of layers do not match");
            }

            return true;
        }
        //If int or bool
        if(result.GetType() != returnType.GetType()) throw new Exception($"Expected returnType {returnType.GetType()}, not {result.GetType()}");
        return true;
    }
    //Only used for mapLayer comparisons
    bool compareType(object mapObject, object returnObject)
    {
        //Get example from first index in map
        for (int i = 0; i < 2; i++)
        {
            if (mapObject is Array arr)
            {
                mapObject = arr.GetValue(0);
            }
            else throw new Exception($"Error layer does not have 2 dimensions");
        }

        if (mapObject.GetType() != returnObject.GetType()) return false;
        if (mapObject is Array arr1 && returnObject is Array arr2)
        {
            return checkDimensionsOfArrays(arr1, arr2);
        }

        return true;
    }

    bool checkDimensionsOfArrays(object? o1, object o2)
    {
        if (o1 is Array arr1 && o2 is Array arr2)
        {
            if (arr1.Length == arr2.Length) return compareReturnResult(arr1.GetValue(0), arr2.GetValue(0));
            throw new Exception($"Lengths of arrays do not match {arr1.Length} and {arr2.Length}");
        }
        if (o1.GetType() == o2.GetType()) return true;
        throw new Exception($"Type of layer did not match {o1.GetType()} and {o2.GetType()}");
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

    public override object? VisitParameterArr(VestaParser.ParameterArrContext context)
    {
        return new object[1 + context.paramaterArrayDenoter().ToArray().Length];
    }

    public override object? VisitParameter(VestaParser.ParameterContext context)
    {
        var parameterName = context.IDENTIFIER().GetText();
        var resultType = Visit(context.parameterType());
        return new ParamDesc(resultType, parameterName);
    }

    /* parameter: (TYPE | COMPLEXTYPE) ('[' (paramaterArrayDenoter)* ']')? IDENTIFIER;
    paramaterArrayDenoter: ','; */
    public override object? VisitParameterType(VestaParser.ParameterTypeContext context)
    {
        object? result;
        string baseType = "";
        if (context.TYPE() is not null) baseType = context.TYPE().GetText();
        else baseType = "map";
            
            
        if (baseType == "int") { result= 0; }
        else if (baseType == "bool") { result= false; }
        else if (baseType == "map") { result = new Map(0, 0);}
        else{ throw new Exception($"type {baseType} undefined"); }

        
        //Test if arrayDimensions doesn't Exist
        
        
        if (context.parameterArr() is null)
        {
            return result;
        }
        
        //if array, need to return arrDesc
        return new ArrDesc(result, (Array)Visit(context.parameterArr()));
    }
    /*verboseComplextype: 'map' mapLayer; */
    public override object? VisitVerboseComplextype(VestaParser.VerboseComplextypeContext context)
    {
        Map resultMap = new Map(1, 1);
        
        //Need to parse each array element.
        var layerArr = (Array)Visit(context.mapLayer());
        for (int i = 0; i < layerArr.Length; i++)
        {
            if (layerArr.GetValue(i) is not LayerDesc layerDesc)
            { throw new Exception($"Layer not parsed correctly"); }

            resultMap.layers[layerDesc.identifier] = layerDesc.value;
        }
        return resultMap;
    }

    /* returnType: TYPE (parameterArr)? | verboseComplextype;
     Must return a type that can be comparable with */

    public override object? VisitReturnType(VestaParser.ReturnTypeContext context)
    {
        if (context.verboseComplextype() is not null) return Visit(context.verboseComplextype());
        object result;
        string baseType = context.TYPE().GetText();
        
            
        if (baseType == "int") { result= 0; }
        else if (baseType == "bool") { result= false; }
        else{ throw new Exception($"type {baseType} undefined"); }
        if (context.parameterArr() is null)
        {
            return result;
        }
        //if array, need to return arrDesc
        return new ArrDesc(result, (Array)Visit(context.parameterArr())); 
       
    }

    /* funcBody: '{' line*  returnStmt '}'; */
    public override object? VisitFuncBody(VestaParser.FuncBodyContext context)
    {
        //Visit all lines
        var lineArr = context.statement().ToArray();
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

        
    /* functionCall: (IDENTIFIER'.')? IDENTIFIER '(' (expression (',' expression)*)? ')'; */

    public override object? VisitFunctionCall(VestaParser.FunctionCallContext context)
    {
        var identifierArr = context.IDENTIFIER().ToArray();
        string name = "";
        if (identifierArr.Length == 2)  //If library call
        {
            var libName = identifierArr[0].GetText();
            var funcName = identifierArr[1].GetText();

            if (!libraries.ContainsKey(libName)) throw new Exception($"Unknown library \"{libName}\"");
            if (!libraries[libName].ContainsKey(funcName))
                throw new Exception($"Unknown function \"{funcName}\" in library {libName}");
            var argsForFunc = context.expression().Select(e => Visit(e)).ToArray();
            return libraries[libName][funcName](argsForFunc);
        }
        else
        {
            name = identifierArr[0].GetText();
        }
        
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
        var arr = context.statement().ToArray();
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
        //Since semantics do not allow this, if assigned to array and not immediaitely inserted, throw error
        if (value is Array valueArr)
            throw new Exception($"Cannot quickAssign an array with another array, must be with a basic type.");
        
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

    /* identifierType IDENTIFIER        #varDeclaration */
    public override object? VisitVarDeclaration(VestaParser.VarDeclarationContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        
        if(store.variables.ContainsKey(varName))  //No overriding current variables
        {
            throw new Exception($"The variable {varName} is already defined");
        }

        var type = Visit(context.identifierType());

        if (type is ArrDesc arrDesc) //If is an array
        {

            store.variables[varName] = multiDimensionArrGen(arrDesc.dimensions, arrDesc.typeBase, arrDesc.dimensions.Length);
  
        }
        else if (type is int n)  //If type is int
        {
            store.variables[varName] = n;
        }
        else if (type is bool b) //If type is bool
        {
            store.variables[varName] = b;
        }
        else
        {
            throw new Exception($"Invalid type");
        }

        return null;
    }
    
    
    
    /* identifierType IDENTIFIER '=' expression;
     identifierType: TYPE ('[' expression ']')*;
     TYPE: 'int' | 'bool' | 'map' ;
     */
    public override object? VisitVarInitialization(VestaParser.VarInitializationContext context)
    {
        var varName = context.assignment().IDENTIFIER().GetText();

        var value = Visit(context.assignment().expression());

        if (store.variables.ContainsKey(varName)) //No overriding current variables
        {
            throw new Exception($"The variable {varName} is already defined");
        }

        var type = Visit(context.allType());


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
            store.variables[varName] = ((Map)value).clone();
        }
        else if (type is int n) //If type is int
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

    /* factor2 '.' IDENTIFIER arrayDimensions			#mapAccess */
    public override object? VisitMapAccess(VestaParser.MapAccessContext context)
    {
        object myMap = Visit(context.factor2());
        var layerName = context.IDENTIFIER().GetText();

        if (myMap is Map map)
        {
            if (map.layers.ContainsKey(layerName))
            {
                if (context.arrayDimensions() is null)
                {   //If no layer indexer thing
                    return map.layers[layerName];
                }
                else
                {
                    return getElementFromIndex(parseArray<int>((Array)Visit(context.arrayDimensions())),map.layers[layerName]);
                }
            }

            throw new Exception($"Unknown layer name {layerName}");
        }

        throw new Exception($"Can only get layers of maps, not type {myMap.GetType()}");
        
    }


    /* arrayDimensions mapLayer 				#mapExpression  '}';
       mapLayer: '{' identifierType IDENTIFIER ('=' expression)?
       (';' identifierType IDENTIFIER ('=' expression)?)* '}' ;
 */

    public override object? VisitMapExpression(VestaParser.MapExpressionContext context)
    {
        var expressionContextArr = context.arrayDimensions().expression().ToArray();
        if (expressionContextArr.Length != 2) throw new Exception($"Must have 2 indexes");
        
        if (Visit(expressionContextArr[0]) is not int n1)
        {
            throw new Exception($"Map dimensions must be described with integer");
        }
        if (Visit(expressionContextArr[1]) is not int n2)
        {
            throw new Exception($"Map dimensions must be described with integer");
        }
        Map resultMap = new Map(n1, n2);    
        int[] dimensionsArr = {n1, n2};

        //Need to parse each array element.
        var layerArr = (Array)Visit(context.mapLayer());
        for (int i = 0; i < layerArr.Length; i++)
        {
            if (layerArr.GetValue(i) is not LayerDesc layerDesc)
            { throw new Exception($"Layer not parsed correctly"); }

            resultMap.layers[layerDesc.identifier] = mapLayerGen(dimensionsArr, layerDesc.value, 2);
        }
        return resultMap;
    }
    Type getBaseType(object? o)
    {
        while (o is Array arr)
        {
            o = arr.GetValue(0);
        }

        return o.GetType();
    }

    public override object? VisitMapLayer(VestaParser.MapLayerContext context)
    {
        return context.individualLayer().Select(e => Visit(e)).ToArray();
    }

    //Returns layer identifier, and value to assign layer to
    /*mapLayer: '{' identifierType IDENTIFIER ('=' expression)?
       (';' identifierType IDENTIFIER ('=' expression)?)* '}' ;*/
    public override object? VisitIndividualLayer(VestaParser.IndividualLayerContext context)
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
                    result = multiDimensionArrGen(arrDesc.dimensions, value, arrDesc.dimensions.Length);
                }
                else
                {
                    throw new Exception($"Array variable types do not match layer type");
                }
            }
            //Where typebase is set the default of the value
            else if (type is int)
            {
                if (getBaseType(value) != type.GetType()) throw new Exception($"Types do not match");
                result = value;
            }
            else if (type is bool)
            {
                if (getBaseType(value) != type.GetType()) throw new Exception($"Types do not match");
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
    object? mapLayerGen(Array dimensions, object? assignValue, int dimensionsLength)
    {
        if (dimensionsLength == 0)  //if no more arrays, return assignValue
        {
            return assignValue;
        }
        if (dimensions.GetValue(dimensions.Length-dimensionsLength) is int n)   
        {//Calculates current dimension to work on
            object[] result = new object[n];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = mapLayerGen(dimensions, assignValue, dimensionsLength - 1);
            } //Calls itself with -1 dimensions

            return result;
        }
        throw new Exception($"Must describe index with type int");
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
            //if (getDepth(arrAss) > dimensionsLength)
            else
            {
                throw new Exception($"Cannot assign an array with {getDepth(arrAss)} dimensions to an array with {dimensionsLength} dimensions");
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
    
    
    public override object? VisitArrayDimensions(VestaParser.ArrayDimensionsContext context)
    {
        //return an int array of dimensions
        return parseArray<int>(context.expression().Select(e=>Visit(e)).ToArray());
    }

    public override object? VisitAllType(VestaParser.AllTypeContext context)
    {
        if (context.COMPLEXTYPE() != null)
        {
            if (context.COMPLEXTYPE().GetText() == "map")
            {
                return new Map(0, 0);
            }
        }

        var identifierType = Visit(context.identifierType());

        if (identifierType != null)
        {
            return identifierType;
        }


        throw new Exception($"Cannot recognize type {context.COMPLEXTYPE().GetText()}");
    }

    /* identifierType: TYPE (arrayDimensions)? ;*;
     TYPE: 'int' | 'bool' ; */
    public override object? VisitIdentifierType(VestaParser.IdentifierTypeContext context)
    {
        
        object? result;
        string baseType = context.TYPE().GetText();
        
        if (baseType == "int") { result= 0; }
        else if (baseType == "bool") { result= false; }
        else{ throw new Exception($"type {baseType} undefined"); }

        
        //Test if arrayDimensions Exist
        var arrayDimensionsContext = context.arrayDimensions()?.expression().ToArray();
        if (arrayDimensionsContext==null)
        {
            return result;
        }
        
        //if array, need to know type, and to know length of dimensions. MyArr contains dimensions by needs to know type
        var myArr = parseArray<int>((Array)Visit(context.arrayDimensions()));
        object[] arrResult = new object[myArr.Length];
        for (int i = 0; i < myArr.Length; i++)
        {
            arrResult[i] = myArr.GetValue(i);
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

    /*assignment: IDENTIFIER (arrayDimensions)? '=' expression | mapAssignment; */
    public override object? VisitAssignment(VestaParser.AssignmentContext context)
    {
        //If mapassignment is not null
        if (context.mapAssignment() is not null) return Visit(context.mapAssignment());
        
        //IF "normal" assignment
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression()); 

        var relevantStore = getStoreForVar(varName);
        var originalValue = relevantStore.variables[varName];
        if (context.arrayDimensions() is not null)//Handle arrayIndexes
        {
            var indexArr = parseArray<int>(context.arrayDimensions().expression().Select(e => Visit(e)).ToArray());

            if (relevantStore.variables[varName] is Array originalVar)
            {
                relevantStore.variables[varName] = assignElementOfArr(indexArr, originalVar, value, 0);
                return null;
            }
            throw new Exception($"$Cannot get element of non array element");
        }
        //Handle not array
        
        //If Maps
        if (originalValue is Map map)
        {
            if (value is Map assignMap)
            {
                relevantStore.variables[varName] = handleAssignMap(map, assignMap);
                return null;
            }

            throw new Exception($"Cannot assign map to non map type");
        }
        
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

    Map handleAssignMap(Map map, Map assignMap)
    {
        if (map.d1 != assignMap.d1 || map.d2 != assignMap.d2)
            throw new Exception($"Dimensions of maps must match to assign");
        
        //If dimensions match start assignning layers
        foreach (string key in assignMap.layers.Keys)
        {
            if (map.layers.ContainsKey(key))
            {
                map.layers[key] = assignMap.layers[key];
            }
            else throw new Exception($"Cannot assign as map is missing {key} layer");
        }
        return map;
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
   /*  mapAssignment: IDENTIFIER ',' IDENTIFIER (arrayDimensions)? '=' expression; */
    /* mapAssignment:  IDENTIFIER '.' IDENTIFIER ('[' expression (',' expression)* ']') '=' expression;*/
    public override object? VisitMapAssignment(VestaParser.MapAssignmentContext context)
    {
        var varName = context.IDENTIFIER(0).GetText();
        var layerName = context.IDENTIFIER(1).GetText();
        var assignValue = Visit(context.expression()); //Get last expression

        var relevantStore = getStoreForVar(varName);

        if (relevantStore.variables[varName] is not Map map)
        {
            throw new Exception($"Cannot get layer of type {relevantStore.variables[varName].GetType()}");
        }

        var layer = map.layers[layerName];

        if (context.arrayDimensions() is null)  //no index expressions
        {
            map.layers[layerName] = quickAssignArr(map.layers[layerName], assignValue);
            relevantStore.variables[varName] = map;
            return null;
        }
        //If index:
        var indexArr= parseArray<int>(context.arrayDimensions().expression().Select(e=>Visit(e)).ToArray());


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
        return true; //Maybe check for not map?
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
    

    public override object? VisitIdentifierAccess(VestaParser.IdentifierAccessContext context)
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

    /*  factor2 arrayDimensions					#arrayAccess */
    public override object? VisitArrayAccess(VestaParser.ArrayAccessContext context)
    {
        var indexArr = parseArray<int>((Array)Visit(context.arrayDimensions()));
        object? myArray = Visit(context.factor2());

        for(int i=0; i<indexArr.Length; i++)
        {
            if (myArray is Array arr)
            {
                myArray = arr.GetValue(indexArr[i]);
            }
            else throw new Exception($"Cannot get element from type {myArray.GetType()}");
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
    private Array arrayApplyToAllRightWithFunc(Array arr, object? leftside,
        Func<object?, object?, Func<object?, object?, object?>, object?> function, 
        Func<object?, object?, object?> mainFunc)
    {
        object[] result = new object[arr.Length];
        for(int i=0; i<arr.Length; i++)
        {
            result[i]=(function(leftside, arr.GetValue(i), mainFunc));
        }
        return result;
    }
    

    private Array arrayApplyToAllLeftWithFunc(Array arr, object? leftside,
        Func<object?, object?, Func<object?, object?, object?>, object?> function, 
        Func<object?, object?, object?> mainFunc)
    {
        object[] result = new object[arr.Length];
        for(int i=0; i<arr.Length; i++)
        {
            result[i]=(function(arr.GetValue(i), leftside, mainFunc));
        }
        return result;
    }
    private Array basicListOperationsWithFunc(Array arr1, Array arr2,
        Func<object?, object?, Func<object?, object?, object?>, object?> function, 
        Func<object?, object?, object?> mainFunc)
    {
        object[] result = new object[arr1.Length];

        if (!(arr1.Length == arr2.Length))
        {
            throw new Exception($"The length of arrays that are added must be the same before an operation can be performed");
        }
        
        for (int i=0;i < arr1.Length; i++)
        {
            result[i]=(function(arr1.GetValue(i), arr2.GetValue(i), mainFunc));
        }

        return result;
    }
    private Array arrayApplyToAllLeft(Array arr, object? leftside, Func<object?, object?, object?> function)
    {
        object[] result = new object[arr.Length];
        for(int i=0; i<arr.Length; i++)
        {
            result[i]=(function(arr.GetValue(i), leftside));
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

    /* ifStatement: 'if' '(' expression')' block ('else' block)?; */
    public override object? VisitIfStatement(VestaParser.IfStatementContext context)
    {
        if (isTrue(Visit(context.expression())))
        {
            Visit(context.block(0));
        }
        else
        {
            Visit(context.block(1));
        }

        return null;
    }
    public override object? VisitWhileStatement(VestaParser.WhileStatementContext context)
    {

        while (isTrue(Visit(context.expression())))
        {
            Visit(context.block());
 
        }

        return null;
    }
    
    /* 'chance{' (expression ':' block)+ '}' */
    public override object? VisitChance(VestaParser.ChanceContext context)
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
        r = random.getRand(0, r-1);     //Generates an integer between 0 and sum(v_0...v_n)-1
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
            "==" => CompareFunction(left, right, (x, y) => (x.Equals(y)) ),
            "!=" => CompareFunction(left, right, (x, y) => !(x.Equals(y)) ),
            "<" => CompareFunction(left, right, (x, y) => (int)x<(int)y ),
            ">" => CompareFunction(left, right, (x, y) => (int)x>(int)y ),
            "<=" => CompareFunction(left, right, (x, y) => (int)x<=(int)y ),
            ">=" => CompareFunction(left, right, (x, y) => (int)x>=(int)y ),
            _ => throw new NotImplementedException()
        };
    }

    public override object? VisitNotExpression(VestaParser.NotExpressionContext context)
    {
        if (Visit(context.factor()) is bool b)
        {
            return !b;
        }

        throw new Exception($"Not exceptions must be with expressions of type bool");
    }
/*forStatement: 'for' '(' varDecl ';'  expression ';' assignment ')' block; */
    public override object? VisitForStatement(VestaParser.ForStatementContext context)
    {
        var ass = context.assignment();
        var expression = context.expression();
        
        //Start by visit dclr
        Visit(context.varDecl());

        if(Visit(expression) is bool flag){}
        else
        {
            throw new Exception($"Must be a boolean value for for loops");
        }
        while (flag)
        {
            Visit(context.block());
            Visit(ass);
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
        if (Visit(context.factor()) is int n)
        {
            return -n;
        }

        throw new Exception($"Not exceptions must be with expressions of type int");
    }

    public override object? VisitBooleanExpression(VestaParser.BooleanExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));
        
        var op = context.boolOp().GetText();
        return op switch
        {
            "&&" => CompareFunction(left, right, (x, y) => (bool)x && (bool)y ),
            "||" => CompareFunction(left, right, (x, y) => (bool)x || (bool)y ),
            _ => throw new NotImplementedException()
        };
    }


    private object? CompareFunction(object? left, object? right, Func<object?, object?, object?> function)
    {
        if (left is Array arr1 && right is Array arr2)
        {
            return basicListOperationsWithFunc(arr1, arr2, CompareFunction, function);
        }
        if (left is Array arr3)
        {
            return arrayApplyToAllLeftWithFunc(arr3, right, CompareFunction, function);
        }

        if (right is Array arr4)
        {
            return arrayApplyToAllRightWithFunc(arr4, left, CompareFunction, function);
        }
        if (left.GetType()==right.GetType())
        {
            return function(left, right);
        }
       
        throw new Exception($"$Cannot compare between types {left.GetType()} and {right.GetType()}");
    }
}
