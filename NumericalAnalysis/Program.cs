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
                const int size = 500;
                Matrix A = new Matrix(size, size);

                Vector F = new Vector(size);
                Random device = new Random();
                for (int i = 0; i < size; i++)
                {
                    F.Elem[i] = device.NextDouble();
                    for (int j = 0; j < size; j++)
                        A.Elem[i, j] = device.NextDouble();
                }
                
                var T = new Thread(() =>
                {
                    
                    //LUDecomposition lu = new LUDecomposition();
                    //Vector X = lu.StartSolver(A, F);

                    //Vector X = Gauss.StartSolver(A, F);

                    //Vector X = Givens.StartSolverQR(A, F);
                    //Vector X = GramSchmidt.StartSolverQR(A, F);
                    Vector X = Householder.StartSolverQR(A, F);
                    
                });
                // TODO:
                // Complete this part ???
                // And think better idea is to measure time in the unit tests
                // And for some reason Gauss is not working in this test with size bigger than 40
                
                Console.WriteLine(CONST.MeasureTime(T));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
