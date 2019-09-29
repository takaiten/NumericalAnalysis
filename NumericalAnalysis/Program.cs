using System;
using System.Diagnostics;
using System.Threading;

namespace ComMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const int size = 300;
                Matrix A = new Matrix(size, size);

                Vector F = new Vector(size);
                Random device = new Random();
                for (int i = 0; i < size; i++)
                {
                    F.Elem[i] = device.NextDouble();
                    for (int j = 0; j < size; j++)
                        A.Elem[i, j] = device.NextDouble();
                }
                
                LUDecomposition lu = new LUDecomposition();
                var LU = new Thread(() =>
                {
                    Vector X = lu.StartSolver(A, F);
                });
                
                var GAUSS = new Thread(() =>
                {
                    Vector X = Gauss.StartSolver(A, F);
                });
                
                var ClassicGS = new Thread(() =>
                {
                    Vector X = GramSchmidt.StartClassicSolverQR(A, F);
                });

                var ModifiedGS = new Thread(() =>
                {
                    Vector X = GramSchmidt.StartModifiedSolverQR(A, F);
                });

                var GIVENS = new Thread(() =>
                {
                    Vector X = Givens.StartSolverQR(A, F);
                });

                var HOUSEHOLDER = new Thread(() =>
                {
                    Vector X = Householder.StartSolverQR(A, F);
                });
                
                Console.WriteLine("LU Decomposition " + CONST.MeasureTime(LU));
                Console.WriteLine("Gauss " + CONST.MeasureTime(GAUSS));
                Console.WriteLine("Classic Gram-Schmidt " + CONST.MeasureTime(ClassicGS));
                Console.WriteLine("Modified Gram-Schmidt " + CONST.MeasureTime(ModifiedGS));
                Console.WriteLine("Givens " + CONST.MeasureTime(GIVENS));
                Console.WriteLine("Householder " + CONST.MeasureTime(HOUSEHOLDER));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
