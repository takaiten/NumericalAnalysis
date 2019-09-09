using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    class Gauss
    {
        public static int GetLeadElem(Matrix A, int j)
        {
            int index = j;
            for (int i = j + 1; i < A.Row; i++)
                if (Math.Abs(A.Elem[i, j]) > Math.Abs(A.Elem[index, j]))
                    index = i;
            if (Math.Abs(A.Elem[index, j]) < CONST.EPS)
                throw new Exception("GAUSS: Matrix is singular");

            return index;
        }

        public void DirectWay(Matrix A, Vector F)
        {
            double help;

            for (int k = 0; k < A.Row - 1; k++)
            {
                int leadElem = GetLeadElem(A, k);

                if (leadElem != k)
                {
                    A.SwitchRows(leadElem, k);

                    help = F.Elem[k];
                    F.Elem[k] = F.Elem[leadElem];
                    F.Elem[leadElem] = help;
                }

                for (int i = k + 1; i < A.Row; i++)
                {
                    help = A.Elem[i, k] / A.Elem[k, k];
                    A.Elem[i, k] = 0;
                    for (int j = i + 1; j < A.Column; j++)
                        A.Elem[i, j] -= help * A.Elem[k, j];
                    F.Elem[i] -= help * F.Elem[k];
                }
            }
        }

        public static void DirectWay(Matrix A)
        {
//            double help;
//
//            for (int i = 0; i < A.Row - 1; i++)
//            {
//                int leadElem = GetLeadElem(A, i);
//
//                if (leadElem != i)
//                    A.SwitchRows(leadElem, i);
//
//                for (int j = i + 1; j < A.Row; j++)
//                {
//                    help = A.Elem[j, i] / A.Elem[i, i];
//                    A.Elem[j, i] = 0.0f;
//                    for (int k = i + 1; k < A.Column; k++)
//                        A.Elem[j, k] -= help * A.Elem[j, k];
//                }
//            }

            for (int k = 0; k < A.Row - 1; k++)
            {
                int lead = GetLeadElem(A, k);
                if (lead != k)
                    A.SwitchRows(lead, k);

                for (int i = k + 1; i < A.Row; i++)
                for (int j = A.Column - 1; j >= 0; j--)
                    if (Math.Abs(A.Elem[i, k]) > CONST.EPS)
                    {
                        A.Elem[i, j] *= A.Elem[k, k] / A.Elem[i, k];
                        A.Elem[i, j] -= A.Elem[k, j];
                    }
            }
        }

        public Vector StartSolver(Matrix A, Vector F)
        {
            DirectWay(A, F);

            var res = new Vector(F.Size);

            Substitution.BackRowSubstitution(A, F, res);

            return res;
        }
    }
}