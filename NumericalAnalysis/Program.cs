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
                var T = new Thread(() =>
                {
                    const int size = 20;
                    Matrix A = new Matrix(size, size);
                    Vector xTrue = new Vector(size);
                    for (int i = 0; i < size; i++)
                    {
                        A.Elem[i, i] = 1;
                        xTrue.Elem[i] = 1;
                        for (int j = i + 1; j < size; j++)
                        {
                            A.Elem[i, j] = 0.01 * (i + j + 1);
                            A.Elem[j, i] = 0.02 * (i + j + 1);
                        }
                    }

                    Vector F = A * xTrue;
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
