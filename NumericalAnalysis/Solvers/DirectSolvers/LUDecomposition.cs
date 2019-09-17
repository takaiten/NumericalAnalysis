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

            Substitution.DirectRowSubstitution(A, F, y);
            Substitution.BackRowSubstitution(A, y, x);

            return x;
        }
    }

    class luDecomp
    {
        public Matrix LU { set; get; }

        public luDecomp()
        {
        }

        public luDecomp(Matrix A)
        {
            LU = new Matrix(A);
            Gauss.DirectWay(LU);
            
            for (int i = 0; i < A.Row; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < j; k++)
                        sum += LU.Elem[i, k] * LU.Elem[k, j];
                    
                    LU.Elem[i, j] = (A.Elem[i, j] - sum) / LU.Elem[j, j];
                }

            }
        }

        void DirectWay(Matrix A, Vector F, out Vector res)
        {
            res = new Vector(F);
            for (int i = 0; i < A.Row; i++)
                for (int j = 0; j < i; j++)
                    res.Elem[i] -= A.Elem[i, j] * res.Elem[j];
        }

        public Vector startSolver(Vector F)
        {
//            if (LU == null)
//                throw new Exception("LU: no matrix created");
            
            DirectWay(LU, F, out var res);
            Substitution.BackRowSubstitution(LU, res, res);
            
            return res;
        }
    }
}