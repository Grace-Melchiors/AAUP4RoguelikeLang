using System.Collections;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using Antlr_language.Content;
using Antlr4.Runtime;
using Microsoft.VisualBasic.CompilerServices;

namespace Antlr_language;

public class Tile
{
    
}

public class Map<T>
{

    public List<List<T>> data; 
    public Map(List<List<T>> mapData)
    {
        data = mapData;
    }

    public virtual T GetData(int indexX, int indexY)
    {
        if (indexX <= data.Count)
        {
            if (indexY <= data[indexX].Count)
            {
                return data[indexX][indexY];
            }
        }
        throw new Exception("$Out of bonds exception");
    }
    
    
    
}

public class Region<T> : Map<T>
{
    public Region(List<List<T>> mapData, int xOffSet, int xMaxLength, int yOffSet, int yMaxLength) : base(mapData)
    {
        _xOffSet = xOffSet;
        _xMaxLength = xMaxLength;
        _yOffSet = yOffSet;
        _yMaxLength = yMaxLength;
    }

    private int _xOffSet;
    private int _xMaxLength;
    private int _yOffSet;
    private int _yMaxLength;

    public override T GetData(int indexX, int indexY)
    {
        if (indexX <= _xMaxLength)
        {
            if (indexY <= _yMaxLength)
            {
                return data[indexX+_xOffSet][indexY+_yOffSet];
            }
        }

        throw new Exception("$Out of bonds exception");
    }

}

public class Store
{
    public Dictionary<string, object> variables { get; } = new();
    public Store previous;
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

    public override object? VisitDeclartion(VestaParser.DeclartionContext context)
    {
        var varName = context.IDENTIFIER().GetText();

        var value = Visit(context.expression());

        store.variables[varName] = value;
        
        return null;
    }

    public override object? VisitAssignment(VestaParser.AssignmentContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var value = Visit(context.expression());
        if (!store.variables.ContainsKey(varName))
        {
            Store tempStore = store;
            while (tempStore.previous!=null)
            {
                tempStore = tempStore.previous;
                if (tempStore.variables.ContainsKey(varName))
                {
                    tempStore.variables[varName] = value;
                    return null;
                }
            }

            throw new Exception($"Variable {varName} is not defined");
        }

        store.variables[varName] = value;
        return null;
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
        ArrayList result = new ArrayList();

        var arr = context.expression().ToArray();
        if (arr.Length > 0)
        {
            int count;
            var holder = Visit(arr[0]);
            if (checkForValidArrayEntry(holder))
            {
                result.Add(holder);
                for (count = 1; count < arr.Length; count++)
                {
                    holder = Visit(arr[count]);
                    if(result[count-1].GetType() != holder.GetType())
                    {
                        throw new Exception(
                            $"$Cannot create an array with types {result[count-1].GetType()} and {holder.GetType()}");
                    }
                    result.Add(holder);
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

    public override object? VisitArrayIdentifierExpression(VestaParser.ArrayIdentifierExpressionContext context)
    {
        var varName = context.IDENTIFIER().GetText();
        var numberArr = context.expression().ToArray();
        object? result;
        object? holder;
        if (!Variables.ContainsKey(varName))
        {
            throw new Exception($"Variable {varName} is not defined");
        }

        var myArray = Variables[varName];
        if (!(myArray is ArrayList))
        {
            throw new Exception($"{varName} is not an array, but an {myArray.GetType()}");
        }

        foreach (var newNumber in numberArr)
        {
            holder = Visit(newNumber);
            if (holder is int n && myArray is ArrayList arr)
            {
                myArray = arr[n];
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
        if (left is string || right is string)
        {
            return $"{left}{right}";
        }

        if (left is ArrayList arr1 && right is ArrayList arr2)
        {
            return basicListOperations(arr1, arr2, Add);
        }

        if (left is ArrayList arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Add);
        }

        if (right is ArrayList arr4)
        {
            return arrayApplyToAllRight(arr4, left, Add);
        }
        
        throw new Exception($"cannot add values of types {left?.GetType()} and {right?.GetType()} <3");
    }
    
    private ArrayList arrayApplyToAllRight(ArrayList arr, object? leftside, Func<object?, object?, object?> function)
    {
        ArrayList result = new ArrayList();
        foreach (var var in arr)
        {
            result.Add(function(leftside, var));
        }
        return result;
    }
    
    private ArrayList arrayApplyToAllLeft(ArrayList arr, object? rightside, Func<object?, object?, object?> function)
    {
        ArrayList result = new ArrayList();
        foreach (var var in arr)
        {
            result.Add(function(var, rightside));
        }
        return result;
    }
    private ArrayList basicListOperations(ArrayList arr1, ArrayList arr2, Func<object?, object?, object?> function)
    {
        ArrayList result = new ArrayList();

        int maxLength = Math.Max(arr1.Count, arr2.Count);
        for (int i=0;i < maxLength; i++)
        {
            if (i == arr1.Count)
            {
                for (; i < maxLength; i++)
                {
                    result.Add(arr2[i]);
                }
            }
            else if (i == arr2.Count)
            {
                for (; i < maxLength; i++)
                {
                    result.Add(arr1[i]);
                }
            }
            else
            {
                result.Add(function(arr1[i], arr2[i]));
            }
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

        if (left is ArrayList arr1 && right is ArrayList arr2)
        {
            return basicListOperations(arr1, arr2, Subtract);
        }
        if (left is ArrayList arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Subtract);
        }

        if (right is ArrayList arr4)
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

        if (left is ArrayList arr1 && right is ArrayList arr2)
        {
            return basicListOperations(arr1, arr2, Multiply);
        }
        if (left is ArrayList arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Multiply);
        }

        if (right is ArrayList arr4)
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

        if (left is ArrayList arr1 && right is ArrayList arr2)
        {
            return basicListOperations(arr1, arr2, Divide);
        }
        if (left is ArrayList arr3)
        {
            return arrayApplyToAllLeft(arr3, right, Divide);
        }

        if (right is ArrayList arr4)
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


   