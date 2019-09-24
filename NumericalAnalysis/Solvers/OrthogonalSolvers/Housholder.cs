using System;

namespace ComMethods
{
    class Housholder
    {
        public static void Orthogon(Matrix A, Matrix Q, Matrix R)   // Q = diag(1 .. 1)
        {
            Vector w = new Vector(A.Row);
            double beta, mu, s;

            for (int i = 0; i < A.Column - 1; i++)
            {
                s = 0.0;

                // ||x||^2 for nullify
                for (int I = i; I < A.Row; I++)
                    s += Math.Pow(A.Elem[I, i], 2);

                if (Math.Abs(s - A.Elem[i, i] * A.Elem[i, i]) > CONST.EPS)
                {
                    beta = A.Elem[i, i] < 0 ? Math.Sqrt(s) : -Math.Sqrt(s);
                    mu = 1.0 / beta / (beta - A.Elem[i, i]);

                    for (int I = 0; I < A.Row; I++)
                    {
                        w.Elem[I] = 0.0;
                        if (I >= i)
                            w.Elem[I] = A.Elem[I, i];
                    }

                    w.Elem[i] -= beta;

                    // A = H * A
                    for (int m = i; m < A.Column; m++)
                    {
                        s = 0;
                        for (int n = i; n < A.Row; n++)
                            s += A.Elem[n, m] * w.Elem[n];

                        s *= mu;
                        for (int n = i; n < A.Row; n++)
                            A.Elem[n, m] -= s * w.Elem[n];
                    }

                    for (int m = 0; m < A.Row; m++)
                    {
                        s = 0;
                        for (int n = 0; n < A.Row; n++)                        
                            s += Q.Elem[m, n] * w.Elem[n];

                        s *= mu;
                        for (int n = i; n < A.Row; n++)
                            Q.Elem[m, n] -= s * w.Elem[n];
                    }

                }
            }

            R.Elem = A.Elem;
        }
    }
}
