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
            Vector res = new Vector(F.Size);
            Vector resNew = new Vector(F.Size);

            for (int i = 0; i < F.Size; i++) 
                res.Elem[i] = F.Elem[i] / A.Elem[i][i];

            double normError;

            do
            {
                if (Iter > 1)
                    res.Copy(resNew);
                
                normError = 0.0;
                for (int i = 0; i < F.Size; i++)
                {
                    resNew.Elem[i] = F.Elem[i];
                    for (int j = 0; j < F.Size; j++) 
                        if (i != j)
                            resNew.Elem[i] -= A.Elem[i][j] * res.Elem[j];
                    
                    
                    resNew.Elem[i] /= A.Elem[i][i];
                    normError += Math.Pow(res.Elem[i] - resNew.Elem[i], 2);
                }

                normError = Math.Sqrt(normError);
                Iter++;
                
//                Console.WriteLine($"Iter {Iter, -10} {normError}");
            } while (Iter < MaxIter && normError > Eps);
            
//            Console.WriteLine($"Iter {Iter}");
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