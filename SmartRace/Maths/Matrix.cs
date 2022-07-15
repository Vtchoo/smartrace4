using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRace.Maths
{
    public class Matrix
    {
        public static float[,] Mult(float[,] A, float[,] B)
        {
            int linA = A.GetLength(0);
            int colA = A.GetLength(1);
            int linB = B.GetLength(0);
            int colB = B.GetLength(1);

            float[,] AxB = new float[linA, colB];

            if (colA != linB) { return AxB; }

            for (int i = 0; i < linA; i++)
                for (int j = 0; j < colB; j++)
                    for (int k = 0; k < colA; k++)
                        AxB[i, j] += A[i, k] * B[k, j];

            return AxB;
        }

        public static float[,] ToMatrix(float[] array)
        {
            float[,] matrix = new float[array.Length, 1];

            for (int i = 0; i < array.Length; i++)
                matrix[i, 1] = array[i];

            return matrix;
        }
    }
}
