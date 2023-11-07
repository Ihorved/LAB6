using System;

public class MathOperations
{
    public static T Add<T>(T a, T b)
    {
        dynamic operand1 = a;
        dynamic operand2 = b;
        return operand1 + operand2;
    }

    public static T Subtract<T>(T a, T b)
    {
        dynamic operand1 = a;
        dynamic operand2 = b;
        return operand1 - operand2;
    }

    public static T Multiply<T>(T a, T b)
    {
        dynamic operand1 = a;
        dynamic operand2 = b;
        return operand1 * operand2;
    }

    public static T[] Add<T>(T[] arr1, T[] arr2)
    {
        if (arr1.Length != arr2.Length)
        {
            throw new ArgumentException("Array lengths must match for addition.");
        }

        T[] result = new T[arr1.Length];
        for (int i = 0; i < arr1.Length; i++)
        {
            result[i] = Add(arr1[i], arr2[i]);
        }
        return result;
    }

    public static T[] Subtract<T>(T[] arr1, T[] arr2)
    {
        if (arr1.Length != arr2.Length)
        {
            throw new ArgumentException("Array lengths must match for subtraction.");
        }

        T[] result = new T[arr1.Length];
        for (int i = 0; i < arr1.Length; i++)
        {
            result[i] = Subtract(arr1[i], arr2[i]);
        }
        return result;
    }
    public static T[] Multiply<T>(T[] arr1, T[] arr2)
    {
        if (arr1.Length != arr2.Length)
        {
            throw new ArgumentException("Array lengths must match for multiplication.");
        }

        T[] result = new T[arr1.Length];
        for (int i = 0; i < arr1.Length; i++)
        {
            result[i] = Multiply(arr1[i], arr2[i]);
        }
        return result;
    }

    public static T[,] Add<T>(T[,] matrix1, T[,] matrix2)
    {
        if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
        {
            throw new ArgumentException("Matrix dimensions must match for addition.");
        }

        int rows = matrix1.GetLength(0);
        int cols = matrix1.GetLength(1);
        T[,] result = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = Add(matrix1[i, j], matrix2[i, j]);
            }
        }

        return result;
    }

    public static T[,] Subtract<T>(T[,] matrix1, T[,] matrix2)
    {
        if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
        {
            throw new ArgumentException("Matrix dimensions must match for subtraction.");
        }

        int rows = matrix1.GetLength(0);
        int cols = matrix1.GetLength(1);
        T[,] result = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = Subtract(matrix1[i, j], matrix2[i, j]);
            }
        }

        return result;
    }

    public static T[,] Multiply<T>(T[,] matrix1, T[,] matrix2)
    {
        if (matrix1.GetLength(1) != matrix2.GetLength(0))
        {
            throw new ArgumentException("Matrix dimensions must match for multiplication.");
        }

        int rows = matrix1.GetLength(0);
        int cols = matrix2.GetLength(1);
        int commonDim = matrix1.GetLength(1);
        T[,] result = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = default(T);
                for (int k = 0; k < commonDim; k++)
                {
                    result[i, j] = Add(result[i, j], Multiply(matrix1[i, k], matrix2[k, j]));
                }
            }
        }

        return result;
    }
}
