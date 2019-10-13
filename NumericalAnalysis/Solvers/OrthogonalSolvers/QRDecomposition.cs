using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ComMethods
{
    /// <summary>
    /// класс семейтсва методов QR-декомпозиции
    /// </summary>
    class QRDecomposition
    {
        //верхняя треугольная матрица
        public Matrix R { set; get; }
        //ортогональная матрица
        public Matrix Q { set; get; }
        //перечисление методов декомпозиции
        public enum QRAlgorithm
        {
            Classic_Gram_Schmidt = 1,
            Modified_Gram_Schmidt,
            Householder,
            Givens
        }

        /// <summary>
        /// реализация QR-декомпозиции
        /// </summary>
        /// <param name="A - исходная матрица"></param>
        /// <param name="Method - метод QR-декомпозиции"></param>
        public QRDecomposition(Matrix A, QRAlgorithm Method)
        {
            R = new Matrix(A.Row, A.Column);
            Q = new Matrix(A.Row, A.Column);
            //начальная инициализация матрицы ортогональных преобразований
            for (int i = 0; i < A.Row; i++) Q.Elem[i][i] = 1.0;

            switch (Method)
            {
                case QRAlgorithm.Classic_Gram_Schmidt:
                    {
                        GramSchmidt.Classic(A, Q, R);
                        break;
                    }
                case QRAlgorithm.Modified_Gram_Schmidt:
                    {
                        GramSchmidt.Modified(A, Q, R);
                        break;
                    }
                case QRAlgorithm.Givens:
                    {
                        Givens.Orthogon(A, Q, R);
                        break;
                    }
                case QRAlgorithm.Householder:
                    {
                        Householder.Orthogon(A, Q, R);
                        break;
                    }
            }
        }

        public Vector StartSolver(Vector F)
        {
            var RES = Q.Transpose() * F;
            Substitution.BackRowSubstitution(R, RES, RES);
            return RES;
        }
    }
}
