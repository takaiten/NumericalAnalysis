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
                string path = "C:/Users/TEMP.NSTU.011/Downloads/NumericalAnalysis/CSIR_System/Systems1/SPD/";
                var A = new CSlRMatrix(path);
                var F = new Vector(A.Row);
                var Xtrue = new Vector(A.Row);
                for (int i = 0; i < A.Row; i++)
                {
                    Xtrue.Elem[i] = 1.0f;
                }
                A.MultMV(Xtrue, F);
          
                var conjGradSolver = new conjugateGrad(10000, CONST.EPS);
                conjGradSolver.StartSolver(A, F, Preconditioner.PreconditionerType.Diagonal);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
