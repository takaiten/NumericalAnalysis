using System;

namespace ComMethods
{
    class Substitution
    {
        //прямая подстановка по строкам (А - нижняя треугольная матрица)
        public static void DirectRowSubstitution(Matrix A, Vector F, Vector RES)
        {
            //скопируем по значениям вектор F в RES
            RES.Copy(F);

            //проход по строкам
            for (int i = 0; i < F.Size; i++)
            {
                if (Math.Abs(A.Elem[i, i]) < CONST.EPS) throw new Exception("Direct Row Substitution: division by 0...");

                for (int j = 0; j < i; j++)
                {
                    RES.Elem[i] -= A.Elem[i, j] * RES.Elem[j];
                }

                RES.Elem[i] /= A.Elem[i, i];
            }
        }

        //прямая подстановка по столбцам (А - нижняя треугольная матрица)
        public static void DirectColumnSubstitution(Matrix A, Vector F, Vector RES)
        {
            //скопируем вектор F в RES
            RES.Copy(F);

            //проход по столбцам
            for (int j = 0; j < F.Size; j++)
            {
                if (Math.Abs(A.Elem[j, j]) < CONST.EPS) throw new Exception("Direct Column Substitution: division by 0...");

                RES.Elem[j] /= A.Elem[j, j];

                for (int i = j + 1; i < F.Size; i++)
                {
                    RES.Elem[i] -= A.Elem[i, j] * RES.Elem[j];
                }
            }
        }

        //обратная подстановка по строкам (А - верхняя треугольная матрица)
        public static void BackRowSubstitution(Matrix A, Vector F, Vector RES)
        {
            //скопируем вектор F в RES
            RES.Copy(F);

            //начинаем с последней строки, двигаясь вверх
            for (int i = F.Size - 1; i >= 0; i--)
            {
                if (Math.Abs(A.Elem[i, i]) < CONST.EPS) throw new Exception("Back Row Substitution: division by 0... ");

                //двигаемся по столбцам
                for (int j = i + 1; j < F.Size; j++)
                {
                    RES.Elem[i] -= A.Elem[i, j] * RES.Elem[j];
                }

                RES.Elem[i] /= A.Elem[i, i];
            }
        }

        //обратная подстановка по столбцам (А - верхняя треугольная матрица)
        public static void BackColumnSubstitution(Matrix A, Vector F, Vector RES)
        {
            //скопируем вектор F в RES
            RES.Copy(F);

            //начинаем с последнего столбца, сдвигаясь влево
            for (int j = F.Size - 1; j >= 0; j--)
            {
                if (Math.Abs(A.Elem[j, j]) < CONST.EPS) throw new Exception("Back Column Substitution: division by 0...");

                RES.Elem[j] /= A.Elem[j, j];

                //двигаемся по строкам
                for (int i = j - 1; i >= 0; i--)
                {
                    RES.Elem[i] -= A.Elem[i, j] * RES.Elem[j];
                }
            }
        }
    }
}
