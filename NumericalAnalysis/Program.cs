using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace ComMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = "./../../../../Systems/System3/";
                Matrix A = new Matrix(path);
                Vector F = new Vector(path);
                
                int mu = 1;
                
                A.MultDiagByConst(mu);
                
                Console.WriteLine($"Disturbance parameter {mu}");
//                Console.WriteLine($"Condition number: {A.CondSquareMatrix():g2}");

                int maxIter = 100000;
                double eps = 1e-8;

                Vector X = new Vector();
                
//                var GAUSS = new Action(() =>
//                {
//                    X = Gauss.StartSolver(A, F);
//                });
//                
//                Console.WriteLine("\nGauss " + CONST.MeasureTime(GAUSS));
//                Console.WriteLine($"Discrepancy: {CONST.RelativeDiscrepancy(A, X, F):g2}");
//
//                Jacobi J = new Jacobi(maxIter, eps);
//                var JACOBI = new Action(() =>
//                {
//                    X = J.StartSolver(A, F);
//                });
//                
//                Console.WriteLine("\nJacobi " + CONST.MeasureTime(JACOBI));
//                Console.WriteLine($"Iterations: {J.Iter}");
//                Console.WriteLine($"Discrepancy: {CONST.RelativeDiscrepancy(A, X, F):g2}");
//                
                SOR S = new SOR(maxIter, eps);
//                var GAUSS_SEIDEL = new Action(() =>
//                {
//                    X = S.StartSolver(A, F, 1);
//                });
//                
//                Console.WriteLine("\nGauss-Seidel " + CONST.MeasureTime(GAUSS_SEIDEL));
//                Console.WriteLine($"Iterations: {S.Iter}");
//                Console.WriteLine($"Discrepancy: {CONST.RelativeDiscrepancy(A, X, F):g2}");

                var RELAXATION = new Action(() =>
                {
                    X = S.StartSolver(A, F, 1.849);
                });
                
                Console.WriteLine("\nSOR " + CONST.MeasureTime(RELAXATION));
                Console.WriteLine($"Iterations: {S.Iter}");
                Console.WriteLine($"Discrepancy: {CONST.RelativeDiscrepancy(A, X, F):g2}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
