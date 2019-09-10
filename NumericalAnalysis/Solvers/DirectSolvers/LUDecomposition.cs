using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    static class LUDecomposition
    {
        public static void Transform(Matrix A, Matrix L, Matrix U)
        {
            for (int i = 0; i < L.Row; i++)
                for (int j = 0; j < L.Column; j++)
                {
                    double sum = 0.0f;

                    for (int k = 0; k < j; k++)
                        sum += L.Elem[i, k] * U.Elem[k, j];

                    L.Elem[i, j] = (A.Elem[i, j] - sum) / U.Elem[j, j];
                }
        }
        
        public static Vector StartSolver(Matrix A, Vector F)
        {
            // TODO:
            // Currently LU is not working correctly. 
            // Think about copying matrices. Is it worth it?
            Matrix L = new Matrix(A.Row, A.Column);
            Matrix U = Gauss.DirectWay(A);
            
            Transform(A, L, U);
            
            var res = new Vector(F.Size);

            Substitution.BackRowSubstitution(U, F, res);
            Substitution.DirectRowSubstitution(L, F, res);

            return res;
        }
    }
}