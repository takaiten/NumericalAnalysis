using System;

namespace ComMethods
{
    public class Jacobi : IIteratorSolver
    {
        public double Eps { set; get; }
        public int Iter { set; get; }
        public int MaxIter { set; get; }

        public Vector StartSolver(Matrix A, Vector F)
        {
            double normError, F_Ax;
            Vector res = new Vector(F.Size);
            Vector resNew = new Vector(F.Size);

            for (int i = 0; i < F.Size; i++) 
                res.Elem[i] = 0.0f; // не блочный, а точечный SOR

            do
            {
                normError = 0;
                for (int i = 0; i < F.Size; i++)
                {
                    F_Ax = F.Elem[i];
                    for (int j = 0; j < i; j++) 
                        F_Ax -= A.Elem[i][j] * resNew.Elem[j];
                    for (int j = i + 1; j < F.Size; j++) 
                        F_Ax -= A.Elem[i][j] * res.Elem[j];

                    resNew.Elem[i] = F_Ax / A.Elem[i][i];
                    normError += Math.Pow(res.Elem[i] - resNew.Elem[i], 2);
                }

                res.Copy(resNew);
                normError = Math.Sqrt(normError);
                Iter++;
                Console.WriteLine("Iter {0, -10} {1}", Iter, normError);
            } while (Iter < MaxIter && normError > Eps);

            return res;
        }

        public Jacobi(int maxIter, double eps)
        {
            Eps = eps;
            MaxIter = maxIter;
            Iter = 0;
        }
    }
}