using System;
using System.Collections.Generic;

namespace ComMethods
{
	class Eigenvalues
	{
        double RayleighShift(Matrix A)
        { return A.Elem[A.Row - 1][A.Column - 1]; }

        double WilcoxonShift(Matrix A)
        {
            int N = A.Column - 1;
            double a = A.Elem[A.Row - 2][A.Row - 2],
                b = A.Elem[A.Row - 1][A.Row - 1],
                c = A.Elem[A.Row - 1][A.Row - 2],
                d = A.Elem[A.Row - 2][A.Row - 1];

            double D = Math.Pow(a + b, 2) - 4 * (a * b - c * d);

            if (D < 0)
                throw new Exception("Matrix has a complex eigenvalue");

            return 0.5 * ((a + b) + Math.Sqrt(D));
        }


        void Shift(Matrix A, double shift)
        {
            for (int i = 0; i < A.Row; i++)
                A.Elem[i][i] += shift;
        }

		public List<double> GetEigenvalues(Matrix A, QRDecomposition.QRAlgorithm method)
		{
            var eigenvalues = new List<double>();
            int iter = 0;

            while (A.Row != 1)
            {
                iter++;
                for (int i = A.Row - 1; i > 0; i--)
                    if (Math.Abs(A.Elem[i][i - 1]) < 1e-6)
                    {
                        eigenvalues.Add(A.Elem[i][i]);
                        A.ExcludeRowColumn(i);
                        i = A.Row;
                    }

                if (A.Row == 1)
                    break;

                double shift = WilcoxonShift(A);
                Shift(A, -shift);

                QRDecomposition QR = new QRDecomposition(A, method);
                A = QR.R * QR.Q;
                Shift(A, shift);
            }

            Console.WriteLine("Iter = " + iter.ToString() + "\n");

            eigenvalues.Add(A.Elem[0][0]);
            eigenvalues.Sort();
            eigenvalues.Reverse();

            return eigenvalues;
        }
    }
}
