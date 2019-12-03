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
            var ExcelRW = new Excel_Reader_Writer("Data\\Matrix.xlsx");

            //try
            //{
            //    string path = "..\\..\\..\\..\\CSIR_System\\Systems2\\SPD\\";
            //    var A = new CSlRMatrix(path);
            //    var F = new Vector(A.Row);
            //    var Xtrue = new Vector(A.Row);
            //    for (int i = 0; i < A.Row; i++)
            //    {
            //        Xtrue.Elem[i] = 1.0f;
            //    }
            //    A.MultMV(Xtrue, F);

            //    Vector X = new Vector();

            //    var conjSolver = new ConjugateGrad(600, CONST.EPS);
            //    Console.WriteLine("Conjugate Gradient method:");
            //    var CG = new Action(() =>
            //    {
            //        X = conjSolver.StartSolver(A, F, Preconditioner.PreconditionerType.ILU);
            //    });

            //    Console.WriteLine(CONST.MeasureTime(CG));
            //    Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F));

            //    var biConjSolver = new BiConjugateGrad(600, CONST.EPS);
            //    Console.WriteLine("\nBiConjugate Gradient method:");
            //    var BiCG = new Action(() =>
            //    {
            //        X = biConjSolver.StartSolver(A, F, Preconditioner.PreconditionerType.ILU);
            //    });

            //    Console.WriteLine(CONST.MeasureTime(BiCG));
            //    Console.WriteLine("Discrepancy: " + CONST.RelativeDiscrepancy(A, X, F));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
        }
    }
}
