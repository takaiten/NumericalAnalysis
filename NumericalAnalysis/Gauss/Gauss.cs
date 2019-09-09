using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    public interface IGauss
    { }
    class Gauss
    {
        public Matrix SLE { set; get; }
        public Matrix U { set; get; }
        public Matrix L { set; get; }

        public Gauss()
        { }

        public Gauss(Matrix A)
        {
            SLE = A;
        }

        public void UTTransformation()
        {
            if (SLE.Column != SLE.Row)
                throw new Exception("GAUSS: Matrix isn't square");

            U = SLE;

            for (int k = 0; k < U.Row - 1; k++)
            {
                double max = 0.0f;
                int maxNum = 0;

                for (int i = k; i < U.Row; i++)
                    if (Math.Abs(U.Elem[i, k]) > max)
                    {
                        max = Math.Abs(U.Elem[i, k]);
                        maxNum = i;
                    }

                if (max == 0.0f)
                    throw new Exception("GAUSS: Matrix is singular");

                U.SwitchRows(maxNum, k);

                for (int i = k + 1; i < U.Row; i++)
                    for (int j = U.Column - 1; j >= 0; j--)
                        if (U.Elem[i, k] != 0)
                        {
                            U.Elem[i, j] *= U.Elem[k, k] / U.Elem[i, k];
                            U.Elem[i, j] -= U.Elem[k, j];
                        }
            }
        }

        public void LTTransformation()
        {
            L = new Matrix(U.Row, U.Column);

            for (int i = 0; i < L.Row; i++)
                for (int j = 0; j < L.Column; j++)
                {
                    double sum = 0.0f;

                    for (int k = 0; k < j; k++)
                        sum += L.Elem[i, k] * U.Elem[k, j];

                    L.Elem[i, j] = (SLE.Elem[i, j] - sum) / U.Elem[j, j];
                }
        }
    }
}
