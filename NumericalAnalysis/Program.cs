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
                const int size = 500;
                Matrix A = new Matrix(size, size);
                Vector xTrue = new Vector(size);
                
                for (int i = 0; i < size; i++)
                {
                    A.Elem[i, i] = 10;
                    xTrue.Elem[i] = 1;
                    for (int j = i + 1; j < size; j++)
                    {
                        A.Elem[i, j] = 0.01 * (i + j + 1);
                        A.Elem[j, i] = -0.01 * (i + j + 1);
                    }
                }

                Vector F = A * xTrue;
                Vector X = new Vector();
                
                LUDecomposition lu = new LUDecomposition();
                var LU = new Action(() =>
                {
                    X = lu.StartSolver(A, F);
                });
                
                Console.WriteLine("LU Decomposition " + CONST.MeasureTime(LU));
                Console.WriteLine("Error: " + CONST.RelativeError(X, xTrue).ToString("g2"));
                Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F).ToString("g2"));
                
                var GAUSS = new Action(() =>
                {
                    X = Gauss.StartSolver(A, F);
                });
                
                Console.WriteLine("\nGauss " + CONST.MeasureTime(GAUSS));
                Console.WriteLine("Error: " + CONST.RelativeError(X, xTrue).ToString("g2"));
                Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F).ToString("g2"));
                
                var ClassicGS = new Action(() =>
                {
                    X = GramSchmidt.StartClassicSolverQR(A, F);
                });

                Console.WriteLine("\nClassic Gram-Schmidt " + CONST.MeasureTime(ClassicGS));
                Console.WriteLine("Error: " + CONST.RelativeError(X, xTrue).ToString("g2"));
                Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F).ToString("g2"));
                
                var ModifiedGS = new Action(() =>
                {
                    X = GramSchmidt.StartModifiedSolverQR(A, F);
                });

                Console.WriteLine("\nModified Gram-Schmidt " + CONST.MeasureTime(ModifiedGS));
                Console.WriteLine("Error: " + CONST.RelativeError(X, xTrue).ToString("g2"));
                Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F).ToString("g2"));
                
                var GIVENS = new Action(() =>
                {
                    X = Givens.StartSolverQR(A, F);
                });
                
                Console.WriteLine("\nGivens " + CONST.MeasureTime(GIVENS));
                Console.WriteLine("Error: " + CONST.RelativeError(X, xTrue).ToString("g2"));
                Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F).ToString("g2"));
                
                var HOUSEHOLDER = new Action(() =>
                {
                    X = Householder.StartSolverQR(A, F);
                });
                
                Console.WriteLine("\nHouseholder " + CONST.MeasureTime(HOUSEHOLDER));
                Console.WriteLine("Error: " + CONST.RelativeError(X, xTrue).ToString("g2"));
                Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F).ToString("g2"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
