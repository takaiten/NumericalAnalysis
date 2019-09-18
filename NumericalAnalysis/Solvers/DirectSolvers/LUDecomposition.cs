using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ComMethods
{
    static class LUDecomposition
    {
        public static void luDecomposition(Matrix A, out Matrix L, out Matrix U)
        {
            if (A.Column != A.Row)
                throw new Exception("luDecomposition: input matrix isn't square");
            int n = A.Column;
            L = new Matrix(n, n);
            U = new Matrix(n, n);

            // Decomposing into Upper and Lower Triangular matrices
            for (int i = 0; i < n; i++)
            {
                // Upper Triangular 
                for (int k = i; k < n; k++)
                {
                    double sum = 0;
                    for (int j = 0; j < i; j++)
                        sum += (L.Elem[i, j] * U.Elem[j, k]);

                    U.Elem[i, k] = A.Elem[i, k] - sum;
                }

                // Lower Triangular 
                for (int k = i; k < n; k++)
                {
                    if (i == k)
                        L.Elem[i, i] = 1; // Diagonal is 1 
                    else
                    {
                        double sum = 0;
                        for (int j = 0; j < i; j++)
                            sum += (L.Elem[k, j] * U.Elem[j, i]);

                        L.Elem[k, i] = (A.Elem[k, i] - sum) / U.Elem[i, i];
                    }
                }
            }
        }

        public static Vector StartSolver(Matrix A, Vector F)
        {
            // TODO:
            Vector x = new Vector(F.Size);
            Vector y = new Vector(F.Size);

            luDecomposition(A, out var L, out var U);

            Substitution.DirectRowSubstitution(L, F, y);
            Substitution.BackRowSubstitution(U, y, x);

            return x;
        }
    }
}