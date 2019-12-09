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
            A.Elem[0][0] = 10;
            A.Elem[0][1] = 1;
            A.Elem[0][2] = 1;

            A.Elem[1][0] = 1;
            A.Elem[1][1] = 1;
            A.Elem[1][2] = -1;
        
            A.Elem[2][0] = 2;
            A.Elem[2][1] = -1;
            A.Elem[2][2] = 0;
          
            var eigenvalues = Eigenvalues.GetEigenvalues(A, 10);
        }
    }
}
