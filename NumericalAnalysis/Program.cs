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
            
            Matrix A = new Matrix(3, 3);
            A.Elem[0][0] = 1;
            A.Elem[0][1] = 2;
            A.Elem[0][2] = 3;

            A.Elem[1][0] = 2;
            A.Elem[1][1] = 2;
            A.Elem[1][2] = 1;

            A.Elem[2][0] = 3;
            A.Elem[2][1] = 1;
            A.Elem[2][2] = 3;

            A.HessenbergMatrix();
            var Pr = new Eigenvalues();
            var eigenvalues = Pr.GetEigenvalues(A, QRDecomposition.QRAlgorithm.Givens);

            foreach (var el in eigenvalues)
                Console.WriteLine(el);
        }
    }
}
