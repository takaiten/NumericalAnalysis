using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;

namespace ComMethods
{
    public interface ISparseMatrix
    {
        int Row { set; get; }
        int Column { set; get; }
        void MultMV(Vector X, Vector Y);
        void MultMtV(Vector X, Vector Y);
        void SlauL(Vector X, Vector F);
        void SlauLt(Vector X, Vector F);
        void SlauU(Vector X, Vector F);
        void SlauUt(Vector X, Vector F);
    }
    
    class CSlRMatrix : ISparseMatrix
    {
        //размер матрицы
        public int Row { set; get; }

        public int Column { set; get; }

        //диагональ матрицы
        public double[] di { set; get; }

        //нижний треугольник
        public double[] altr { set; get; }

        //верхний треугольник
        public double[] autr { set; get; }

        //номера строк (столбцов) ненулевых элементов
        public int[] jptr { set; get; }

        //номера строк (столбцов), с которых начинается jptr
        public int[] iptr { set; get; }

        //конструктор по умолчанию
        public CSlRMatrix()
        {
        }

        //конструктор по файлам
        public CSlRMatrix(string path)
        {
            char[] separator = new char[] {' '};

            //размер системы
            using (var reader = new StreamReader(File.Open(path + "Size.txt", FileMode.Open)))
            {
                Row = Convert.ToInt32(reader.ReadLine());
                Column = Row;
                //выделение памяти под массивы di и iptr
                iptr = new int[Column + 1];
                di = new double[Column];
            }

            //диагональ матрицы
            using (var reader = new StreamReader(File.Open(path + "di.txt", FileMode.Open)))
            {
                for (int i = 0; i < Column; i++)
                {
                    di[i] = Convert.ToDouble(
                        reader.ReadLine()?.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0],
                        new CultureInfo("en-US"));
                }
            }

            //массив iptr
            using (var reader = new StreamReader(File.Open(path + "iptr.txt", FileMode.Open)))
            {
                for (int i = 0; i <= Column; i++)
                {
                    iptr[i] = Convert.ToInt32(
                        reader.ReadLine()?.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
            }

            //выделение памяти под массивы jptr, altr, autr
            int size = iptr[Column] - 1;
            jptr = new int[size];
            altr = new double[size];
            autr = new double[size];
            var reader1 = new StreamReader(File.Open(path + "jptr.txt", FileMode.Open));
            var reader2 = new StreamReader(File.Open(path + "altr.txt", FileMode.Open));
            var reader3 = new StreamReader(File.Open(path + "autr.txt", FileMode.Open));
            for (int i = 0; i < size; i++)
            {
                jptr[i] = Convert.ToInt32(
                    reader1.ReadLine()?.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0]);
                altr[i] = Convert.ToDouble(
                    reader2.ReadLine()?.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0],
                        new CultureInfo("en-US"));
                autr[i] = Convert.ToDouble(
                    reader3.ReadLine()?.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0],
                        new CultureInfo("en-US"));
            }

            reader1.Close();
            reader2.Close();
            reader3.Close();
        }

        //-------------------------------------------------------------------------------------------------

        //умножение матрицы на вектор A*x = y
        public void MultMV(Vector X, Vector Y)
        {
            for (int i = 0; i < Column; i++) Y.Elem[i] = X.Elem[i] * di[i];
            for (int i = 0; i < Column; i++)
            for (int j = iptr[i] - 1; j < iptr[i + 1] - 1; j++)
            {
                Y.Elem[i] += X.Elem[jptr[j] - 1] * altr[j];
                Y.Elem[jptr[j] - 1] += X.Elem[i] * autr[j];
            }
        }

        //-------------------------------------------------------------------------------------------------

        //умножение транспонированной матрицы на вектор A*x = y
        public void MultMtV(Vector X, Vector Y)
        {
            for (int i = 0; i < Column; i++) Y.Elem[i] = X.Elem[i] * di[i];
            for (int i = 0; i < Column; i++)
            for (int j = iptr[i] - 1; j < iptr[i + 1] - 1; j++)
            {
                Y.Elem[i] += X.Elem[jptr[j] - 1] * autr[j];
                Y.Elem[jptr[j] - 1] += X.Elem[i] * altr[j];
            }
        }

        //-------------------------------------------------------------------------------------------------

        //решение СЛАУ Lx = F с нижней треугольной матрицей
        public void SlauL(Vector X, Vector F)
        {
            for (int i = 0; i < Column; i++)
            {
                X.Elem[i] = F.Elem[i];
                for (int j = iptr[i] - 1; j < iptr[i + 1] - 1; j++)
                    X.Elem[i] -= X.Elem[jptr[j] - 1] * altr[j];
                X.Elem[i] /= di[i];
            }
        }

        //-------------------------------------------------------------------------------------------------

        //решение СЛАУ L_t(x) = F с нижней треугольной транспонированной матрицей
        public void SlauLt(Vector X, Vector F)
        {
            double[] v = new double[Column];
            for (int i = 0; i < Column; i++) v[i] = F.Elem[i];
            for (int i = Column - 1; i >= 0; i--)
            {
                X.Elem[i] = v[i] / di[i];
                for (int j = iptr[i] - 1; j < iptr[i + 1] - 1; j++)
                    v[jptr[j] - 1] -= X.Elem[i] * altr[j];
            }
        }

        //-------------------------------------------------------------------------------------------------

        //решение СЛАУ Ux = F с верхней треугольной матрицей
        public void SlauU(Vector X, Vector F)
        {
            for (int i = 0; i < Column; i++) X.Elem[i] = F.Elem[i];
            for (int i = Column - 1; i >= 0; i--)
            {
                for (int j = iptr[i] - 1; j < iptr[i + 1] - 1; j++)
                {
                    X.Elem[jptr[j] - 1] -= X.Elem[i] * autr[j];
                }
            }
        }

        //-------------------------------------------------------------------------------------------------

        //решение СЛАУ (U_t)x = F с верхней треугольной транспонированной матрицей
        public void SlauUt(Vector X, Vector F)
        {
            for (int i = 0; i < Column; i++)
            {
                X.Elem[i] = F.Elem[i];
                for (int j = iptr[i] - 1; j < iptr[i + 1] - 1; j++)
                    X.Elem[i] -= X.Elem[jptr[j] - 1] * autr[j];
            }
        }

        //-------------------------------------------------------------------------------------------------

        //операция неполного LU-разложения в формате CSlR
        public CSlRMatrix ILU_CSlR()
        {
            var matrix = new CSlRMatrix();
            matrix.Column = Column;
            matrix.Row = Row;
            matrix.iptr = iptr;
            matrix.jptr = jptr;
            int nAutr = autr.Length;
            matrix.autr = new double[nAutr];
            matrix.altr = new double[nAutr];
            matrix.di = new double[Column];

            for (int i = 0; i < nAutr; i++)
            {
                matrix.altr[i] = altr[i];
                matrix.autr[i] = autr[i];
            }

            for (int i = 0; i < Column; i++)
            {
                matrix.di[i] = di[i];
            }

            for (int i = 1; i < Column; i++)
            {
                for (int j = iptr[i] - 1; j < iptr[i + 1] - 1; j++)
                {
                    for (int a = iptr[i] - 1; a < j; a++)
                    {
                        for (int b = iptr[jptr[j] - 1] - 1; b <= iptr[jptr[j]] - 2; b++)
                        {
                            if (jptr[a] == jptr[b])
                            {
                                matrix.altr[j] -= matrix.altr[a] * matrix.autr[b];
                                matrix.autr[j] -= matrix.autr[a] * matrix.altr[b];
                            }
                        }
                    }

                    matrix.autr[j] /= matrix.di[jptr[j] - 1];
                    matrix.di[i] -= matrix.autr[j] * matrix.altr[j];
                }
            }

            return matrix;
        }
    }
}
