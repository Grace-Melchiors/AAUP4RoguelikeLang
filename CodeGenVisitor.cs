using System;
using System.Text;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;
using Antlr_language.ast;
using Antlr_language.Content;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Antlr_language
{
    public class CodeGenVisitor : AstBaseVisitorBuilder<StringBuilder>
    {
        int Indentation = 2;
        public CodeGenVisitor (int indentation) {
            Indentation = indentation;
        }
        public override StringBuilder Visit(ProgramNode context) {
            
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.AppendLine("using System;");
            result.AppendLine("using System.Collections.Generic;");
            result.AppendLine("namespace " + context.nameSpace);
            result.AppendLine("{");
            
            result.AppendLine("\tpublic class Map {");
            result.AppendLine("\t    public Dictionary<string, MapLayer> layers {get; private set;}");
            result.AppendLine("\t    public int d1Size;");
            result.AppendLine("\t    public int d2Size;");
            result.AppendLine("\t    public Map(int d1Size, int d2Size, params MapLayer[] list)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        layers = new();");
            result.AppendLine("\t        foreach (MapLayer layer in list) {");
            result.AppendLine("\t            layers.Add(layer.Identifier, layer);");
            result.AppendLine("\t            layer.SetInitialValues(d1Size, d2Size);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        this.d1Size = d1Size;");
            result.AppendLine("\t        this.d2Size = d2Size;");
            result.AppendLine("\t    }");
            result.AppendLine("\t}");

            result.AppendLine("\tpublic class MapLayer {");
            result.AppendLine("\t    public object InitialValue {get; private set; }");
            result.AppendLine("\t    public object[,]? LayerValue {get; set; }");
            result.AppendLine("\t    public string Identifier {get; private set; }");
            result.AppendLine("\t    public void SetInitialValues (int size1, int size2) {");
            result.AppendLine("\t        LayerValue = new object[size1,size2];");
            result.AppendLine("\t        for (int i = 0; i < size1; i++)");
            result.AppendLine("\t            for (int j = 0; j < size2; j++)");
            result.AppendLine("\t                LayerValue[i,j] = InitialValue;");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public MapLayer (string type, string identifier, object? InitialValue) {");
            result.AppendLine("\t        this.Identifier = identifier;");
            result.AppendLine("\t        if (InitialValue != null) {");
            result.AppendLine("\t            this.InitialValue = InitialValue;");
            result.AppendLine("\t        } else {");
            result.AppendLine("\t            this.InitialValue = type == \"int\" ? 0 : false;");
            result.AppendLine("\t        }");
            result.AppendLine("\t    }");
            result.AppendLine("\t}");

            result.AppendLine("\tpublic enum ArithmeticOperator");
            result.AppendLine("\t{");
            result.AppendLine("\t    add,");
            result.AppendLine("\t    sub,");
            result.AppendLine("\t    mult,");
            result.AppendLine("\t    div");
            result.AppendLine("\t}");
            result.AppendLine("\tpublic enum RelationalOperator");
            result.AppendLine("\t{");
            result.AppendLine("\t    less,");
            result.AppendLine("\t    leq,");
            result.AppendLine("\t    greater,");
            result.AppendLine("\t    geq,");
            result.AppendLine("\t    eq,");
            result.AppendLine("\t    neq");
            result.AppendLine("\t}");
            result.AppendLine("\tpublic enum BooleanOperator");
            result.AppendLine("\t{");
            result.AppendLine("\t    and,");
            result.AppendLine("\t    or");
            result.AppendLine("\t}");
            result.AppendLine("\tpublic class ArrayCalculator");
            result.AppendLine("\t{");
            result.AppendLine("\t    public static Array ComputeOperation(Array leftArray, int rightValue, ArithmeticOperator op) {");
            result.AppendLine("\t        int[] dimensionSizes = new int[leftArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            dimensionSizes[i] = leftArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array rightArray = Array.CreateInstance(typeof(int), dimensionSizes);");
            result.AppendLine("\t        AssignOperation(rightArray, rightValue);");
            result.AppendLine("\t        return ComputeOperation(leftArray, rightArray, op);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static Array ComputeOperation(int leftValue, Array rightArray, ArithmeticOperator op) {");
            result.AppendLine("\t        int[] dimensionSizes = new int[rightArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < rightArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            dimensionSizes[i] = rightArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array leftArray = Array.CreateInstance(typeof(int), dimensionSizes);");
            result.AppendLine("\t        AssignOperation(leftArray, leftValue);");
            result.AppendLine("\t        return ComputeOperation(leftArray, rightArray, op);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static Array ComputeOperation(Array leftArray, int rightValue, RelationalOperator op) {");
            result.AppendLine("\t        int[] dimensionSizes = new int[leftArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            dimensionSizes[i] = leftArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array rightArray = Array.CreateInstance(typeof(int), dimensionSizes);");
            result.AppendLine("\t        AssignOperation(rightArray, rightValue);");
            result.AppendLine("\t        return ComputeOperation(leftArray, rightArray, op);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static Array ComputeOperation(int leftValue, Array rightArray, RelationalOperator op) {");
            result.AppendLine("\t        int[] dimensionSizes = new int[rightArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < rightArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            dimensionSizes[i] = rightArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array leftArray = Array.CreateInstance(typeof(int), dimensionSizes);");
            result.AppendLine("\t        AssignOperation(leftArray, leftValue);");
            result.AppendLine("\t        return ComputeOperation(leftArray, rightArray, op);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    //Assuming all boolean operations are commutative.");
            result.AppendLine("\t    public static Array ComputeOperation(Array leftArray, bool rightValue, BooleanOperator op) {");
            result.AppendLine("\t        return ComputeOperation(rightValue, leftArray, op);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static Array ComputeOperation(bool leftValue, Array rightArray, BooleanOperator op) {");
            result.AppendLine("\t        int[] dimensionSizes = new int[rightArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < rightArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            dimensionSizes[i] = rightArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array leftArray = Array.CreateInstance(typeof(bool), dimensionSizes);");
            result.AppendLine("\t        AssignOperation(leftArray, leftValue);");
            result.AppendLine("\t        return ComputeOperation(leftArray, rightArray, op);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static Array ComputeOperation(Array leftArray, Array rightArray, ArithmeticOperator op)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (leftArray.Rank != rightArray.Rank)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            throw new ArgumentException(\"Arrays must have the same number of dimensions.\");");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int[] dimensionSizes = new int[leftArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            if (leftArray.GetLength(i) != rightArray.GetLength(i))");
            result.AppendLine("\t            {");
            result.AppendLine("\t                throw new ArgumentException(\"Arrays must have the same length in each dimension.\");");
            result.AppendLine("\t            }");
            result.AppendLine("\t            dimensionSizes[i] = leftArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array result = Array.CreateInstance(leftArray.GetType().GetElementType(), dimensionSizes);");
            result.AppendLine("\t        ComputeOperationRecursive(leftArray, rightArray, result, op);");
            result.AppendLine("\t        return result;");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static Array ComputeOperation(Array leftArray, Array rightArray, RelationalOperator op)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (leftArray.Rank != rightArray.Rank)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            throw new ArgumentException(\"Arrays must have the same number of dimensions.\");");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int[] dimensionSizes = new int[leftArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            if (leftArray.GetLength(i) != rightArray.GetLength(i))");
            result.AppendLine("\t            {");
            result.AppendLine("\t                throw new ArgumentException(\"Arrays must have the same length in each dimension.\");");
            result.AppendLine("\t            }");
            result.AppendLine("\t            dimensionSizes[i] = leftArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array result = Array.CreateInstance(typeof(bool), dimensionSizes);");
            result.AppendLine("\t        ComputeOperationRecursive(leftArray, rightArray, result, op);");
            result.AppendLine("\t        return result;");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static Array ComputeOperation(Array leftArray, Array rightArray, BooleanOperator op)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (leftArray.Rank != rightArray.Rank)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            throw new ArgumentException(\"Arrays must have the same number of dimensions.\");");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int[] dimensionSizes = new int[leftArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            if (leftArray.GetLength(i) != rightArray.GetLength(i))");
            result.AppendLine("\t            {");
            result.AppendLine("\t                throw new ArgumentException(\"Arrays must have the same length in each dimension.\");");
            result.AppendLine("\t            }");
            result.AppendLine("\t            dimensionSizes[i] = leftArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array result = Array.CreateInstance(leftArray.GetType().GetElementType(), dimensionSizes);");
            result.AppendLine("\t        ComputeOperationRecursive(leftArray, rightArray, result, op);");
            result.AppendLine("\t        return result;");
            result.AppendLine("\t    }");
            result.AppendLine("\t    private static void ComputeOperationRecursive(Array leftArray, Array rightArray, Array resultArray, ArithmeticOperator op, int[] indices = null, int dimension = 0)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (indices == null)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices = new int[leftArray.Rank];");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int length = leftArray.GetLength(dimension);");
            result.AppendLine("\t        for (int i = 0; i < length; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices[dimension] = i;");
            result.AppendLine("\t            if (dimension < leftArray.Rank - 1)");
            result.AppendLine("\t            {");
            result.AppendLine("\t                ComputeOperationRecursive(leftArray, rightArray, resultArray, op, indices, dimension + 1);");
            result.AppendLine("\t            }");
            result.AppendLine("\t            else");
            result.AppendLine("\t            {");
            result.AppendLine("\t                object leftValue = leftArray.GetValue(indices);");
            result.AppendLine("\t                object rightValue = rightArray.GetValue(indices);");
            result.AppendLine("\t                int leftNumber = Convert.ToInt32(leftValue);");
            result.AppendLine("\t                int rightNumber = Convert.ToInt32(rightValue);");
            result.AppendLine("\t                int resultValue;");
            result.AppendLine("\t                switch (op)");
            result.AppendLine("\t                {");
            result.AppendLine("\t                    case ArithmeticOperator.add:");
            result.AppendLine("\t                        resultValue = leftNumber + rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case ArithmeticOperator.sub:");
            result.AppendLine("\t                        resultValue = leftNumber - rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case ArithmeticOperator.mult:");
            result.AppendLine("\t                        resultValue = leftNumber * rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case ArithmeticOperator.div:");
            result.AppendLine("\t                        resultValue = leftNumber / rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    default:");
            result.AppendLine("\t                        throw new ArgumentException(\"Invalid operator.\");");
            result.AppendLine("\t                }");
            result.AppendLine("\t                resultArray.SetValue(resultValue, indices);");
            result.AppendLine("\t            }");
            result.AppendLine("\t        }");
            result.AppendLine("\t    }");
            result.AppendLine("\t    private static void ComputeOperationRecursive(Array leftArray, Array rightArray, Array resultArray, RelationalOperator op, int[] indices = null, int dimension = 0)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (indices == null)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices = new int[leftArray.Rank];");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int length = leftArray.GetLength(dimension);");
            result.AppendLine("\t        for (int i = 0; i < length; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices[dimension] = i;");
            result.AppendLine("\t            if (dimension < leftArray.Rank - 1)");
            result.AppendLine("\t            {");
            result.AppendLine("\t                ComputeOperationRecursive(leftArray, rightArray, resultArray, op, indices, dimension + 1);");
            result.AppendLine("\t            }");
            result.AppendLine("\t            else");
            result.AppendLine("\t            {");
            result.AppendLine("\t                object leftValue = leftArray.GetValue(indices);");
            result.AppendLine("\t                object rightValue = rightArray.GetValue(indices);");
            result.AppendLine("\t                int leftNumber = Convert.ToInt32(leftValue);");
            result.AppendLine("\t                int rightNumber = Convert.ToInt32(rightValue);");
            result.AppendLine("\t                bool resultValue;");
            result.AppendLine("\t                switch (op)");
            result.AppendLine("\t                {");
            result.AppendLine("\t                    case RelationalOperator.eq:");
            result.AppendLine("\t                        resultValue = leftNumber == rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case RelationalOperator.geq:");
            result.AppendLine("\t                        resultValue = leftNumber >= rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case RelationalOperator.greater:");
            result.AppendLine("\t                        resultValue = leftNumber > rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case RelationalOperator.leq:");
            result.AppendLine("\t                        resultValue = leftNumber <= rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case RelationalOperator.less:");
            result.AppendLine("\t                        resultValue = leftNumber < rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case RelationalOperator.neq:");
            result.AppendLine("\t                        resultValue = leftNumber != rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    default:");
            result.AppendLine("\t                        throw new ArgumentException(\"Invalid operator.\");");
            result.AppendLine("\t                }");
            result.AppendLine("\t                resultArray.SetValue(resultValue, indices);");
            result.AppendLine("\t            }");
            result.AppendLine("\t        }");
            result.AppendLine("\t    }");
            result.AppendLine("\t    private static void ComputeOperationRecursive(Array leftArray, Array rightArray, Array resultArray, BooleanOperator op, int[] indices = null, int dimension = 0)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (indices == null)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices = new int[leftArray.Rank];");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int length = leftArray.GetLength(dimension);");
            result.AppendLine("\t        for (int i = 0; i < length; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices[dimension] = i;");
            result.AppendLine("\t            if (dimension < leftArray.Rank - 1)");
            result.AppendLine("\t            {");
            result.AppendLine("\t                ComputeOperationRecursive(leftArray, rightArray, resultArray, op, indices, dimension + 1);");
            result.AppendLine("\t            }");
            result.AppendLine("\t            else");
            result.AppendLine("\t            {");
            result.AppendLine("\t                object leftValue = leftArray.GetValue(indices);");
            result.AppendLine("\t                object rightValue = rightArray.GetValue(indices);");
            result.AppendLine("\t                bool leftNumber = Convert.ToBoolean(leftValue);");
            result.AppendLine("\t                bool rightNumber = Convert.ToBoolean(rightValue);");
            result.AppendLine("\t                bool resultValue;");
            result.AppendLine("\t                switch (op)");
            result.AppendLine("\t                {");
            result.AppendLine("\t                    case BooleanOperator.and:");
            result.AppendLine("\t                        resultValue = leftNumber && rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    case BooleanOperator.or:");
            result.AppendLine("\t                        resultValue = leftNumber || rightNumber;");
            result.AppendLine("\t                        break;");
            result.AppendLine("\t                    default:");
            result.AppendLine("\t                        throw new ArgumentException(\"Invalid operator.\");");
            result.AppendLine("\t                }");
            result.AppendLine("\t                resultArray.SetValue(resultValue, indices);");
            result.AppendLine("\t            }");
            result.AppendLine("\t        }");
            result.AppendLine("\t    }");
            result.AppendLine("\t    ");
            result.AppendLine("\t    public static void AssignOperation(Array leftArray, Array rightArray, int[] offset)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (leftArray.Rank < rightArray.Rank)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            throw new ArgumentException(\"rightArray must less or equal amount of dimensions as leftArray.\");");
            result.AppendLine("\t        }");
            result.AppendLine("\t        if (leftArray.Rank != offset.Length)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            throw new ArgumentException(\"Offset must have the same number of entries as dimensions in leftArray.\");");
            result.AppendLine("\t        }");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            if (leftArray.GetLength(i) < rightArray.GetLength(i) + offset[i])");
            result.AppendLine("\t            {");
            result.AppendLine("\t                throw new ArgumentException(\"The inserted array can not be assigned outside leftArray.\");");
            result.AppendLine("\t            }");
            result.AppendLine("\t            if (offset[i] < 0) {");
            result.AppendLine("\t                throw new ArgumentException(\"Offset can not be negative.\");");
            result.AppendLine("\t            }");
            result.AppendLine("\t        }");
            result.AppendLine("\t        AssignOperationRecursive(leftArray, rightArray, offset);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static void AssignOperation(Array leftArray, int rightValue)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        int[] dimensionSizes = new int[leftArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            dimensionSizes[i] = leftArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array rightArray = Array.CreateInstance(rightValue.GetType(), dimensionSizes);");
            result.AppendLine("\t        FillArrayRecursive(rightArray, rightValue);");
            result.AppendLine("\t        AssignOperationRecursive(leftArray, rightArray);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    public static void AssignOperation(Array leftArray, bool rightValue)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        int[] dimensionSizes = new int[leftArray.Rank];");
            result.AppendLine("\t        for (int i = 0; i < leftArray.Rank; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            dimensionSizes[i] = leftArray.GetLength(i);");
            result.AppendLine("\t        }");
            result.AppendLine("\t        Array rightArray = Array.CreateInstance(rightValue.GetType(), dimensionSizes);");
            result.AppendLine("\t        FillArrayRecursive(rightArray, rightValue);");
            result.AppendLine("\t        AssignOperationRecursive(leftArray, rightArray);");
            result.AppendLine("\t    }");
            result.AppendLine("\t    private static void FillArrayRecursive (Array arrToFill, object val, int[] indices = null, int dimension = 0) {");
            result.AppendLine("\t        if (indices == null)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices = new int[arrToFill.Rank];");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int length = arrToFill.GetLength(dimension);");
            result.AppendLine("\t        for (int i = 0; i < length; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices[dimension] = i;");
            result.AppendLine("\t            if (dimension < arrToFill.Rank - 1)");
            result.AppendLine("\t            {");
            result.AppendLine("\t                FillArrayRecursive(arrToFill, val, indices, dimension + 1);");
            result.AppendLine("\t            }");
            result.AppendLine("\t            else");
            result.AppendLine("\t            {");
            result.AppendLine("\t                arrToFill.SetValue(val, indices);");
            result.AppendLine("\t            }");
            result.AppendLine("\t        }");
            result.AppendLine("\t    }");
            result.AppendLine("\t    private static void AssignOperationRecursive(Array leftArray, Array rightArray, int[] offset = null, int[] indices = null, int[] inputIndicies = null , int dimension = 0)");
            result.AppendLine("\t    {");
            result.AppendLine("\t        if (offset == null)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            offset = new int[leftArray.Rank];");
            result.AppendLine("\t        }");
            result.AppendLine("\t        if (indices == null)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices = new int[leftArray.Rank];");
            result.AppendLine("\t        }");
            result.AppendLine("\t        if (inputIndicies == null)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            inputIndicies = new int[rightArray.Rank];");
            result.AppendLine("\t        }");
            result.AppendLine("\t        int length = rightArray.GetLength(dimension);");
            result.AppendLine("\t        for (int i = 0; i < length; i++)");
            result.AppendLine("\t        {");
            result.AppendLine("\t            indices[dimension] = i;");
            result.AppendLine("\t            inputIndicies[dimension] = i;");
            result.AppendLine("\t            if (dimension < rightArray.Rank - 1)");
            result.AppendLine("\t            {");
            result.AppendLine("\t                AssignOperationRecursive(leftArray, rightArray, offset, indices, inputIndicies, dimension + 1);");
            result.AppendLine("\t            }");
            result.AppendLine("\t            else");
            result.AppendLine("\t            {");
            result.AppendLine("\t                object rightValue = rightArray.GetValue(inputIndicies);");
            result.AppendLine("\t                leftArray.SetValue(rightValue, (int[])ComputeOperation(indices, offset, ArithmeticOperator.add));");
            result.AppendLine("\t            }");
            result.AppendLine("\t        }");
            result.AppendLine("\t    }");
            result.AppendLine("\t}");

            result.AppendLine("\tclass Program");
            result.AppendLine("\t{");
            foreach (LibraryNode library in context.libraryNodes) {
                result.Append(Visit(library));
                result.AppendLine();
            }
            result.AppendLine("\t\tpublic static MLCGRandom rng = new MLCGRandom();");

            
            result.AppendLine("\t\tpublic class MLCGRandom");
            result.AppendLine("\t\t{");
            //result.AppendLine("\t\t    //private int a= 1664525; //Knuth");
            result.AppendLine("\t\t    private  int a = 69069; //Marsagli");
            result.AppendLine("\t\t    private int c = 1;");
            result.AppendLine("\t\t    private long m = (long)Math.Pow(2,32);");
            result.AppendLine("\t\t    private long seed = 0;");
            result.AppendLine("\t\t    public MLCGRandom()");
            result.AppendLine("\t\t    { seed = (long)(DateTime.Now.Ticks % m); }");
            result.AppendLine("\t\t    public MLCGRandom(long seed)");
            result.AppendLine("\t\t    { this.seed = seed; }");
            result.AppendLine("\t\t    public long getNext()");
            result.AppendLine("\t\t    {");
            result.AppendLine("\t\t        seed = (a * seed + 1) % m;");
            result.AppendLine("\t\t        return seed;");
            result.AppendLine("\t\t    }");
            result.AppendLine("\t\t    public int getRand(int min, int max)");
            result.AppendLine("\t\t    {");
            result.AppendLine("\t\t        if (max < min) throw new Exception($\"Cannot generate random value where max is more than min\");");
            result.AppendLine("\t\t        return (int)(min + getNext() % (max+1 - min));");
            result.AppendLine("\t\t    }");
            result.AppendLine("\t\t}");
            foreach (LineNode line in context.FunctionDecls) {
                //for (int i = 0; i < indentation; i++)
                    //result +="\t";
                result.Append(Visit(line));
                result.AppendLine();
            }
            result.AppendLine("\t\tstatic void Main(string[] args)");
            result.AppendLine("\t\t{");
            foreach (LineNode line in context.Statements) {
                Indentation++;
                result.Append(Visit(line));
                result.AppendLine();
                Indentation--;
            }
            result.AppendLine("\t\t}");
            result.AppendLine("\t}");
            result.AppendLine("}");
            return result;
        }

        public override StringBuilder Visit(LibraryNode context) {
            StringBuilder sb = new StringBuilder(context.Content);
            return sb;
        }
        public override StringBuilder Visit(LineNode context) {
            if (context.statement != null) {
                return Visit(context.statement);
            } else if (context.funcDecl != null) {
                return Visit(context.funcDecl);
            }
            else {
                throw new NotImplementedException();
            }
        }
        
        
        public override StringBuilder Visit(StatementNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            
            if (context.varDecl != null) {
                result.Append(indent);
                result.Append(Visit(context.varDecl) + ";");
            } else if (context.assign != null) {
                result.Append(indent);
                result.Append(Visit(context.assign) + ";");
            } else if (context.expression != null) {
                result.Append(indent);
                result.Append(Visit(context.expression) + ";");
            } else if (context.returnStatement != null) {
                result.Append(indent);
                result.Append(Visit(context.returnStatement) + ";");
            } else if (context.block != null) {
                result.Append(Visit(context.block));
            } else if (context.ifNode != null) {
                result.Append(Visit(context.ifNode));
            } else if (context.whileNode != null) {
                result.Append(Visit(context.whileNode));
            } else if (context.forNode != null) {
                result.Append(Visit(context.forNode));
            } else if (context.chance != null) {
                result.Append(Visit(context.chance));
            } else if (context.statements != null) {
                result.Append(Visit(context.statements));
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override StringBuilder Visit(VariableDeclarationNode context) {
            StringBuilder result = new StringBuilder();
            result.Append(Visit(context.Type));
            result.Append(" ");
            result.Append(context.Identifier);
            if (context.expression != null) {
                result.Append(" = ");
                result.Append(Visit(context.expression));
            } else {
                if (context.Type.GetNodeType() == Enums.Types.INTEGER) {
                    if (context.Type.IsArray) {
                        result.Append(" = new ");
                        bool tmp = context.Type.WriteArraySizes;
                        context.Type.WriteArraySizes = true;
                        result.Append(Visit(context.Type));
                        context.Type.WriteArraySizes = tmp;
                    } else {
                        result.Append(" = " + Constants.INTEGER_DEFAULT);
                    }
                } else if (context.Type.GetNodeType() == Enums.Types.BOOL) {
                    if (context.Type.IsArray) {
                        result.Append(" = new ");
                        bool tmp = context.Type.WriteArraySizes;
                        context.Type.WriteArraySizes = true;
                        result.Append(Visit(context.Type));
                        context.Type.WriteArraySizes = tmp;
                    } else {
                        result.Append(" = " + Constants.BOOL_DEFAULT);
                    }
                    
                } else {
                    throw new NotImplementedException();
                }
                
            }

            return result;
        }
        public override StringBuilder Visit(AssignmentNode context) {
            StringBuilder result = new StringBuilder();
            if (context.IdentifierType != null) {
                if (context.IdentifierType.DimensionRank != 0) {
                    result.Append("ArrayCalculator.AssignOperation(");
                    result.Append(context.IDENTIFIER);
                    result.Append(", ");
                    result.Append(Visit(context.expression));
                    if (context.expression.type != null && context.expression.type.IsArray) {
                        if (context.ArrayIndicies != null) {
                            result.Append(", new int[]{");
                            if (context.ArrayIndicies.Count > 0) {
                                foreach(var exp in context.ArrayIndicies) {
                                    result.Append(Visit(exp));
                                    result.Append(",");
                                }
                                result.Length--;
                            }
                            result.Append("}");
                        } else {
                            result.Append(", new int[]{");
                            for (int i = 0; i < context.expression.type.DimensionRank; i++) {
                                result.Append(0);
                                result.Append(",");
                            }
                            result.Length--;
                            result.Append("}");
                        }
                    }
                    result.Append(")");
                } else {
                    result.Append(context.IDENTIFIER);
                    if (context.ArrayIndicies != null) {
                        result.Append("[");
                        if (context.ArrayIndicies.Count > 0) {
                            foreach(var exp in context.ArrayIndicies) {
                                result.Append(Visit(exp));
                                result.Append(",");
                            }
                            result.Length--;
                        }
                        result.Append("]");
                    }
                    result.Append(" = ");
                    result.Append(Visit(context.expression));
                }
                
            }
            
            
            return result;
        }
        public override StringBuilder Visit(ReturnStatementNode context) {
            StringBuilder result = new StringBuilder();
            result.Append("return ");
            if (context.expression != null) {
                result.Append(Visit(context.expression));
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override StringBuilder Visit(IfNode context) {
            if (context.expression == null)
                throw new NotImplementedException();
            if (context.block == null)
                throw new NotImplementedException();
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.Append("if (");
            result.Append(Visit(context.expression));
            result.AppendLine(")");
            result.Append(Visit(context.block));

            if (context.elseBlock != null) {
                result.Append(indent);
                result.AppendLine("else ");
                result.Append(Visit(context.elseBlock));
            }
            return result;
        }
        public override StringBuilder Visit(WhileNode context) {
            if (context.expression == null)
                throw new NotImplementedException();
            if (context.block == null)
                throw new NotImplementedException();
            //Maybe string builder here?
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.Append("while (");
            result.Append(Visit(context.expression));
            result.AppendLine(")");
            result.Append(Visit(context.block));
            result.AppendLine();
            return result;
        }
        public override StringBuilder Visit(ForNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.Append("for (");
            result.Append(Visit(context.declaration));
            result.Append(";");
            result.Append(Visit(context.expression));
            result.Append(";");
            result.Append(Visit(context.assignment));
            result.AppendLine(")");
            result.Append(Visit(context.block));
            return result;
        }
        public override StringBuilder Visit(ChanceNode context) {
            StringBuilder result = new StringBuilder();
            string indent = "";
            for (int i = 0; i < Indentation; i++)
                indent +="\t";

            
            result.Append(indent);
            result.AppendLine("{");
            indent +="\t";
            result.Append(indent);
            result.AppendLine("int _sum = 0;");
            for (int i = 0; i < context.weights.Count; i++) {
                var weight = context.weights[i];
                result.Append(indent);
                Indentation++;
                result.Append("_sum += ");
                result.Append(Visit(weight));
                result.AppendLine(";");
                Indentation--;
                result.Append(indent);
                Indentation++;
                result.Append("int _sum"+i+" = ");
                result.Append(Visit(weight));
                result.AppendLine(";");
                Indentation--;
            }
            
            result.Append(indent);
            result.AppendLine("int _chance = rng.getRand(0,_sum);");
            for (int i = 0; i < context.weights.Count; i++) {
                var weight = context.weights[i];
                var block = context.blocks[i];
                result.Append(indent);
                result.AppendLine("if (_chance <_sum"+i+ ")");
                //result.Append(indent);
                Indentation++;
                result.Append(Visit(block));
                Indentation--;
                result.Append(indent);
                result.AppendLine("_chance = _chance<_sum"+i+"?_sum:_chance-_sum"+i+";");
            }
            indent = "";
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.AppendLine("}");

            return result;
        }
        public override StringBuilder Visit(StatementsNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            foreach (StatementNode node in context.statements) {
                result.Append(indent);
                result.Append(Visit(node));
                result.AppendLine();
            }
            return result;
        }
        public override StringBuilder Visit(FunctionDeclarationNode context) {
            StringBuilder result = new StringBuilder();
            string indent = "";
            for (int i = 0; i < Indentation; i++) {
                indent += "\t";
            }
            result.AppendLine();
            result.Append(indent);
            result.Append("public static ");
            result.Append(Visit(context.Type));
            result.Append(" " + context.Identifier + " ");
            result.Append("(");
            if (context.funcParams.Count != 0) {
                foreach (var param in context.funcParams) {
                    if (param != null) {
                        result.Append(Visit(param));
                        result.Append(",");
                    } else {
                        throw new NotImplementedException();
                    }
                }
                result.Length--;
            }
            result.AppendLine(")");
            if (context.body == null)
                throw new NotImplementedException();
            result.Append(Visit(context.body));
            result.AppendLine(indent);
            return result;
        }
        public override StringBuilder Visit(TypeNode context) {
            StringBuilder result = new StringBuilder();
            if (context.Type == Enums.Types.MAP) {
                result.Append("Map");
            } else if (context.Type == Enums.Types.INTEGER) {
                result.Append("int");
            } else if (context.Type == Enums.Types.BOOL) {
                result.Append("bool");
            } else {
                throw new NotImplementedException();
            }
            if (context.DimensionRank != 0) {
                result.Append("[");
                if (context.ArraySizes != null) {
                    foreach (ExpressionNode size in context.ArraySizes) {
                        if (context.WriteArraySizes) {
                            result.Append(Visit(size));
                        }
                        result.Append(",");
                    }
                    result.Length--;
                } else {
                    result.Append(',', context.DimensionRank - 1);
                }
                result.Append("]");
            }
            return result;
        }
        public override StringBuilder Visit(ExpressionNode context) {
            StringBuilder result = new StringBuilder();
            if (context.factor != null) {
                if (context.Operator != null && (context.Operator == Enums.Operators.sub || context.Operator == Enums.Operators.not)) {
                    result.Append("(" + Enums.OperatorToString((Enums.Operators)context.Operator));
                    result.Append(Visit(context.factor));
                    result.Append(")");
                } else {
                    result.Append(Visit(context.factor));
                }
            } else if (context.Operator != null && context.expression1 != null && context.expression2 != null) {
                if ((context.expression1.type?.IsArray ?? false) == false && (context.expression2.type?.IsArray ?? false) == false) {
                    result.Append(Visit(context.expression1));
                    result.Append(Enums.OperatorToString((Enums.Operators)context.Operator));
                    result.Append(Visit(context.expression2));
                } else {
                    if ((Enums.Operators)context.Operator == Enums.Operators.add || (Enums.Operators)context.Operator == Enums.Operators.sub || (Enums.Operators)context.Operator == Enums.Operators.mult || (Enums.Operators)context.Operator == Enums.Operators.div) {
                        result.Append("((int[");
                        result.Append(',', Math.Max(context.expression1.type.DimensionRank, context.expression2.type.DimensionRank) - 1);
                        result.Append("])ArrayCalculator.ComputeOperation(" + Visit(context.expression1) + ", " + Visit(context.expression2) + ", ArithmeticOperator." + ((Enums.Operators)context.Operator).ToString() + "))");
                    } else if ((Enums.Operators)context.Operator == Enums.Operators.greater || (Enums.Operators)context.Operator == Enums.Operators.geq || (Enums.Operators)context.Operator == Enums.Operators.less || (Enums.Operators)context.Operator == Enums.Operators.leq || (Enums.Operators)context.Operator == Enums.Operators.neq || (Enums.Operators)context.Operator == Enums.Operators.eq) {
                        result.Append("((bool[");
                        result.Append(',', Math.Max(context.expression1.type.DimensionRank, context.expression2.type.DimensionRank) - 1);
                        result.Append("])ArrayCalculator.ComputeOperation(" + Visit(context.expression1) + ", " + Visit(context.expression2) + ", RelationalOperator." + ((Enums.Operators)context.Operator).ToString() + "))");
                    } else if ((Enums.Operators)context.Operator == Enums.Operators.and || (Enums.Operators)context.Operator == Enums.Operators.or) {
                        result.Append("((bool[");
                        result.Append(',', Math.Max(context.expression1.type.DimensionRank, context.expression2.type.DimensionRank) - 1);
                        result.Append("])ArrayCalculator.ComputeOperation(" + Visit(context.expression1) + ", " + Visit(context.expression2) + ", BooleanOperator." + ((Enums.Operators)context.Operator).ToString() + "))");
                    } else {
                        throw new Exception("Invalid operator.");
                    }
                }
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override StringBuilder Visit(FactorNode context) {
            StringBuilder result = new StringBuilder();
            if (context.parenthesizedExpression != null) {
                result.Append("(");
                result.Append(Visit(context.parenthesizedExpression));
                result.Append(")");
            } else if (context.constant != null) {
                result.Append(Visit(context.constant));
            } else if (context.factor2 != null) {
                result.Append(Visit(context.factor2));
            } else if (context.arrayExpressionsNode != null) {
                result.Append(Visit(context.arrayExpressionsNode));
            } else if (context.mapExpression != null) {
                result.Append(Visit(context.mapExpression));
            } else if (context.arrayAccess != null) {
                result.Append(Visit(context.arrayAccess));
            } else if (context.mapAccess != null) {
                result.Append(Visit(context.mapAccess));
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override StringBuilder Visit(ConstantNode context)
        {
            StringBuilder result = new StringBuilder();
            if (context.boolean != null) {
                result.Append(context.boolean == true ? "true" : "false");
            } else if (context.integer != null) {
                result.Append(context.integer.ToString() ?? "0");
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override StringBuilder Visit(Factor2Node context) {
            StringBuilder result = new StringBuilder();
            if (context.functionCall != null) {
                result.Append(Visit(context.functionCall));
            } else if (context.identifier != null) {
                result.Append(context.identifier);
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override StringBuilder Visit(FunctionCallNode context) {
            StringBuilder result = new StringBuilder();
            if (context.LIBRARY != null) {
                result.Append(context.LIBRARY);
                result.Append(".");
            }
            result.Append(context.IDENTIFIER);
            result.Append("(");
            if (context.parameters != null && context.parameters.Count > 0) {
                foreach (ExpressionNode param in context.parameters) {
                    result.Append(Visit(param));
                    result.Append(",");
                }
                result.Length--;
            }
            result.Append(")");
            return result;
        }
        public override StringBuilder Visit(ArrayExpressionNode context) {
            StringBuilder result = new StringBuilder();
            if (context.type == null)
                throw new NotImplementedException("Mising type information, Need to write new {type} [size, size]");
            if (context.type == null)
                throw new Exception("Missing type");
            if (context.type.OutMostArrayExpression) {
                result.Append("new ");
                result.Append(Visit(context.type));
                result.Append("[");
                foreach (int size in context.type.ArrayExpressionDimensionSizes) {
                    result.Append(size + ",");
                }
                result.Length--;
                result.Append("]");
            }
            if (context.type.ArrayExpressionDimensionSizes == null)
                throw new Exception("Missing dimensions sizes.");
            
            result.Append("{");
            foreach (var expression in context.expressions) {
                result.Append(Visit(expression));
                result.Append(",");
            }
            result.Length--;
            result.Append("}");

            return result;
        }
        public override StringBuilder Visit(MapExpressionNode context) {
            //Map tempMap = new Map();
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";

            result.Append("new Map(" + Visit(context.arrayDimensions).Replace("[", "").Replace("]","") + ",");
            result.Append(Visit(context.mapLayer));
            result.Append(")");

            return result;
        }
        public override StringBuilder Visit(ArrayDimensionsNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.Append("["); 
            foreach (ExpressionNode size in context.expressions) {
                result.Append(Visit(size));
                result.Append(",");
            }
            result.Length--;
            result.Append("]");

            return result;
        }
        public override StringBuilder Visit(MapLayerNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            
            
            if (context.mapLayer.Count > 0) {
                foreach (var layer in context.mapLayer) {
                    result.Append(Visit(layer));
                    result.Append(",");
                }
                result.Length--;
            }
            return result;
        }
        public override StringBuilder Visit(IndividualLayerNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.Append("new MapLayer (");
            result.Append("\"");
            result.Append(Visit(context.LayerType));
            result.Append("\",");
            result.Append("\"" + context.IDENTIFIER + "\",");
            if (context.Expression != null) {
                result.Append(Visit(context.Expression));
            } else {
                result.Append("null");
            }
            result.Append(")");
            
            return result;
        }

        public override StringBuilder Visit(ArrayAccessNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";

            result.Append(Visit(context.factor2));
            result.Append(Visit(context.indicies));

            return result;
        }
        public override StringBuilder Visit(MapAccessNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            if (context.layerType == null)
                throw new Exception("Missing Layer type.");
            result.Append("(");
            result.Append(Visit(context.layerType));
            result.Append(")");
            result.Append(Visit(context.factor2));
            result.Append(".layers");
            result.Append("[\"" + context.IDENTIFIER + "\"]");
            result.Append(".LayerValue");
            if (context.arrayDimensions != null) {
                result.Append(Visit(context.arrayDimensions));
            }
            return result;
        }
        public override StringBuilder Visit(FunctionParamNode context) {
            StringBuilder result = new StringBuilder();
            result.Append(Visit(context.Type));
            result.Append(" ");
            result.Append(context.Identifier);
            return result;
        }
        public override StringBuilder Visit(BlockNode context) {
            string indent = "";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Indentation; i++)
                indent +="\t";
            result.Append(indent);
            result.AppendLine("{");
            foreach (StatementNode statement in context.statementNodes) {
                //for (int i = 0; i < indentation; i++)
                    //result +="\t";
                Indentation++;
                result.Append(Visit(statement));
                result.AppendLine();
                Indentation--;
            }
            result.Append(indent);
            result.AppendLine("}");
            return result;
        }
    }
}