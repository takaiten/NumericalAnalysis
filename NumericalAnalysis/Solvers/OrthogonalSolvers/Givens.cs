using System;

namespace ComMethods
{
    public class Givens
    {
        public static void Orthogon(Matrix A, out Matrix Q, out Matrix R)
        {
            Q = new Matrix(A);
            R = new Matrix(A.Row, A.Column);
            
            double help1, help2;
            double c = 0, s = 0;
            int n = A.Row;
            
            for (int j = 0; j < n - 1; j++)
            {
                for (int i = j + 1; i < n; i++)
                {
                    if (Math.Abs(A.Elem[i, j]) < CONST.EPS) continue;
                    help1 = Math.Sqrt(Math.Pow(A.Elem[i, j], 2) + Math.Pow(A.Elem[j, j], 2));
                    c = A.Elem[j, j] / help1;
                    s = A.Elem[i, j] / help1;
                    for (int k = j; k < n; k++)
                    {
                        help1 = c * A.Elem[j, k] + s * A.Elem[i, k];
                        help2 = -s * A.Elem[j, k] + c * A.Elem[i, k];
                        
                        R.Elem[j, k] = help1; R.Elem[i, k] = help2;
                        A.Elem[j, k] = help1; A.Elem[i, k] = help2;
                    }

                    for (int k = 0; k < n; k++)
                    {
                        help1 = c * Q.Elem[k, j] + s * Q.Elem[k, i];
                        help2 = -s * Q.Elem[k, j] + c * Q.Elem[k, i];
                        
                        Q.Elem[k, j] = help1; Q.Elem[k, i] = help2;
                    }
                }
            }
        }
    }
}