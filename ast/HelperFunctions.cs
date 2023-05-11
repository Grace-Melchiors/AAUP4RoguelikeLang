using System;


namespace Antlr_language.ast
{
    public static class HelperFunctions
    {
        public static Array arrayApplyToAllRightWithFunc(Array arr, object? leftside,
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
        

        public static Array arrayApplyToAllLeftWithFunc(Array arr, object? leftside,
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
        public static Array basicListOperationsWithFunc(Array arr1, Array arr2,
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
    }
}


