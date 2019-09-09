using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    class LUDecomposition
    {
//        public Matrix SLE { private set; get; }
//        public Matrix U { private set; get; }
//        public Matrix L { private set; get; }
//
//        public LUDecomposition()
//        {
//        }
//
//        public LUDecomposition(Matrix A)
//        {
//            SLE = A;
//        }
//
//        public void UTTransformation()
//        {
//            if (SLE.Column != SLE.Row)
//                throw new Exception("GAUSS: Matrix isn't square");
//
//            U = SLE;
//            
//            for (int k = 0; k < U.Row - 1; k++)
//            {
//                int lead = GetLeadElem(U, k);
//                if (lead != k)
//                    U.SwitchRows(lead, k);
//
//                for (int i = k + 1; i < U.Row; i++)
//                    for (int j = U.Column - 1; j >= 0; j--)
//                        if (Math.Abs(U.Elem[i, k]) > CONST.EPS)
//                        {
//                            U.Elem[i, j] *= U.Elem[k, k] / U.Elem[i, k];
//                            U.Elem[i, j] -= U.Elem[k, j];
//                        }
//            }
//        }

        public static void Transform(Matrix A, Matrix L, Matrix U)
        {
            for (int i = 0; i < L.Row; i++)
                for (int j = 0; j < L.Column; j++)
                {
                    double sum = 0.0f;

                    for (int k = 0; k < j; k++)
                        sum += L.Elem[i, k] * U.Elem[k, j];

                    L.Elem[i, j] = (A.Elem[i, j] - sum) / U.Elem[j, j];
                }
        }
    }
}