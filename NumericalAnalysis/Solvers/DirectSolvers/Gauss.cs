using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    static class Gauss
    {
        public static int GetLeadElemIndex(Matrix A, int j)
        {
            int index = j;
            for (int i = j + 1; i < A.Row; i++)
                if (Math.Abs(A.Elem[i, j]) > Math.Abs(A.Elem[index, j]))
                    index = i;

            if (Math.Abs(A.Elem[index, j]) < CONST.EPS)
                throw new Exception("GAUSS:GetLeadElemIndex: Matrix is singular");

            return index;
        }

        public static void DirectWay(Matrix res, Vector F)
        {
//            double help;
//
//            for (int k = 0; k < A.Row - 1; k++)
//            {
//                int leadElem = GetLeadElem(A, k);
//                if (leadElem != k)
//                {
//                    A.SwitchRows(leadElem, k);
//                    F.SwitchElems(leadElem, k);
//                }
//
//                for (int i = k + 1; i < A.Row; i++)
//                {
//                    help = A.Elem[i, k] / A.Elem[k, k];
//                    A.Elem[i, k] = 0;
//                    for (int j = i + 1; j < A.Column; j++)
//                        A.Elem[i, j] -= help * A.Elem[k, j];
//                    F.Elem[i] -= help * F.Elem[k];
//                }
//            }

            double help;

            for (int row = 0; row < res.Row - 1; row++)
            {
                int leadElem = GetLeadElemIndex(res, row);
                if (leadElem != row)
                {
                    res.SwitchRows(leadElem, row);
                    F.SwitchElems(leadElem, row);
                }

                for (int rowFromDiag = row + 1; rowFromDiag < res.Row; rowFromDiag++)
                {
                    if (Math.Abs(res.Elem[rowFromDiag, row]) < CONST.EPS)
                        throw new Exception("GAUSS:DirectWay: Division by 0");

                    help = res.Elem[row, row] / res.Elem[rowFromDiag, row];
                    for (int column = res.Column - 1; column >= 0; column--)
                        res.Elem[rowFromDiag, column] = res.Elem[rowFromDiag, column] * help - res.Elem[row, column];
                    
                    F.Elem[rowFromDiag] = F.Elem[rowFromDiag] * help - F.Elem[row];
                }
            }
        }

        public static Matrix DirectWay(Matrix A)
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
            Matrix res = A.Copy();
            double help;

            for (int row = 0; row < res.Row - 1; row++)
            {
                int leadElem = GetLeadElemIndex(res, row);
                if (leadElem != row) res.SwitchRows(leadElem, row);

                for (int rowFromDiag = row + 1; rowFromDiag < res.Row; rowFromDiag++)
                {
                    if (Math.Abs(res.Elem[rowFromDiag, row]) < CONST.EPS)
                        throw new Exception("GAUSS:DirectWay: Division by 0");

                    help = res.Elem[row, row] / res.Elem[rowFromDiag, row];
                    for (int column = res.Column - 1; column >= 0; column--)
                        res.Elem[rowFromDiag, column] = res.Elem[rowFromDiag, column] * help - res.Elem[row, column];
                }
            }

            return res;
        }

        public static Vector StartSolver(Matrix A, Vector F)
        {
            DirectWay(A, F);

            var res = new Vector(F.Size);

            Substitution.BackRowSubstitution(A, F, res);

            return res;
        }
    }
}