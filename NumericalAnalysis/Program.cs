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
//            var ExcelRW = new Excel_Reader_Writer("Data\\Matrix.xlsx");
            
            Matrix A = new Matrix(5, 6);
            A.Elem[2][2] = 1;
            A.Elem[2][3] = 1;

            A.Elem[3][2] = 1;
            A.Elem[3][3] = 1;

            SVD svd = new SVD();
            svd.SVDExhaustionAlghoritm(A, 1e-5);

            Console.WriteLine("Matrix U:\n");
            for (int i = 0; i < A.Row; i++)
            {
                for (int j = 0; j < svd.Ut.Count; j++)
                    Console.Write(String.Format("{0, -22}", svd.Ut[j][i].ToString("E5")));
                Console.WriteLine();
            }

            Console.WriteLine("\n\nMatrix Sigma:\n");
            foreach (var el in svd.Sigma) Console.WriteLine(el);

            Console.WriteLine("\n\nMatrix V:\n");
            for (int i = 0; i < A.Row; i++)
            {
                for (int j = 0; j < svd.Ut.Count; j++)
                    Console.Write(String.Format("{0, -22}", svd.Vt[j][i].ToString("E5")));
                Console.WriteLine();
            }

            for (int i = 0; i < A.Row; i++)
                for (int j = 0; j < A.Column; j++)
                {
                    A.Elem[i][j] = 0.0;
                    for (int k = 0; k < svd.Sigma.Count; k++)
                        A.Elem[i][j] += svd.Ut[k][i] * svd.Sigma[k] * svd.Vt[k][j];
                }
            Console.WriteLine("\n\nMatrix A = U * Sigma * Vt:\n");
            A.Print();
        }
    }
}
