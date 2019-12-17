using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ComMethods
{
    public interface IMatrix
    {
        int Row { get; }
        int Column { get; }
    }

    public class Matrix : IMatrix
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public double[][] Elem { get; set; }

        public Matrix()
        {
        }

        public Matrix(int row, int column)
        {
            Row = row;
            Column = column;
            Elem = new double[row][];
			for (int i = 0; i < row; i++)
				Elem[i] = new double[column];
        }

        public Matrix(Matrix inp)
        {
            Row = inp.Row;
            Column = inp.Column;
            Elem = new double[Row][];
            for (int i = 0; i < Row; i++)
                Elem[i] = new double[Column];
            
            for (int i = 0; i < Row; i++)
            for (int j = 0; j < Column; j++)
                Elem[i][j] = inp.Elem[i][j];
        }

        public Matrix(string path)
        {
            using(var reader = new BinaryReader(File.Open(path + "Size.bin", FileMode.Open)))
            {
                try
                {
                    Row = reader.ReadInt32();
                    Column = Row;
                }
                catch { throw new Exception("Size.bin: file isn't correct"); }
            }

            using (var reader = new BinaryReader(File.Open(path + "Matrix.bin", FileMode.Open)))
            {
                try
                {
                    Elem = new double[Row][];
                    for (int i = 0; i < Row; i++)
                    {
                        Elem[i] = new double[Column];
                        for (int j = 0; j < Column; j++)
                            Elem[i][j] = reader.ReadDouble();
                    }
                }

                catch { throw new Exception("Matrix.bin: file isn't correct"); }
            }
        }

        // Methods 
        public void Print()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                    Console.Write(String.Format("{0, -22}", Elem[i][j].ToString("E5")));
                Console.WriteLine();
            }
        }

        public void MultRowByConst(int row, double c)
        {
            for (int i = 0; i < Column; i++)
                Elem[row][i] *= c;
        }

        public void MultColumnByConst(int column, double c)
        {
            for (int i = 0; i < Row; i++)
                Elem[i][column] *= c;
        }

        public void MultDiagByConst(double c)
        {
            if (Row == Column)
                for (int i = 0; i < Row; i++)
                    Elem[i][i] *= c;
        }
        
        public int RowNumOfMaxColumnElem(int column)
        {
            if (column >= Column || column < 0)
                throw new Exception("MATRIX: Wrong column number");

            double max = 0.0f;
            int rowNum = 0;

            for (int i = 0; i < Row; i++)
                if (Elem[i][column] > max)
                {
                    rowNum = i;
                    max = Elem[i][column];
                }

            return rowNum;
        }

        public void SwitchRows(int r1, int r2)
        {
            if (r1 >= Row || r1 < 0 || r2 >= Row || r2 < 0)
                throw new Exception("MATRIX: Wrong number of row");

            for (int i = 0; i < Column; i++)
            {
                double tmp = Elem[r1][i];
                Elem[r1][i] = Elem[r2][i];
                Elem[r2][i] = tmp;
            }
        }

        public Matrix Copy()
        {
            return new Matrix(this);
        }

        public Matrix Transpose()
        {
            Matrix res = new Matrix(Row, Column);
            
            for (int i = 0; i < Row; i++)
                for (int j = i; j < Column; j++)
                {
                    res.Elem[i][j] = Elem[j][i];
                    res.Elem[j][i] = Elem[i][j];
                }

            return res;
        }

        delegate void ThreadSolver(int number);
        public double CondSquareMatrix()
        {
            if (Row != Column)
                throw new Exception("Row != Column");

            var QRSolver = new QRDecomposition(Transpose(), QRDecomposition.QRAlgorithm.Householder);

            int numberThreads = Environment.ProcessorCount;
            var semaphores = new bool[numberThreads];

            var normaRowA = new double[numberThreads];
            var normaRowA1 = new double[numberThreads];

            var StartSolver = new ThreadSolver(number =>
            {
                var A1 = new Vector(Row);
                double S1, S2;

                int begin = Column / numberThreads * number;
                int end = begin + Column / numberThreads;

                if (number + 1 == numberThreads)
                    end += Column % numberThreads;

                for (int i = begin; i < end; i++)
                {
                    A1.Elem[i] = 1.0;
                    A1 = QRSolver.StartSolver(A1);

                    S1 = 0; S2 = 0;

                    for (int j = 0; j < Row; j++)
                    {
                        S1 += Math.Abs(Elem[i][j]);
                        S2 += Math.Abs(A1.Elem[j]);
                        A1.Elem[j] = 0;
                    }

                    normaRowA[number] = normaRowA[number] < S1 ? S1 : normaRowA[number];
                    normaRowA1[number] = normaRowA1[number] < S2 ? S2 : normaRowA1[number];
                }

                semaphores[number] = true;
            });

            for (int I = 0; I < numberThreads - 1; I++)
            {
                int number = numberThreads - I - 1;
                ThreadPool.QueueUserWorkItem(par => StartSolver(number));
            }

            StartSolver(0);

            while (Array.IndexOf<bool>(semaphores, false) != -1) {}

            for (int i = 1; i < numberThreads; i++)
            {
                normaRowA[0] = normaRowA[0] < normaRowA[i] ? normaRowA[i] : normaRowA[0];
                normaRowA1[0] = normaRowA1[0] < normaRowA1[i] ? normaRowA1[i] : normaRowA1[0];
            }

            return normaRowA[0] * normaRowA1[0];
        }
        
        // Operator overloads
        public static Vector operator *(Matrix M, Vector V) // Mult matrix by a vector 
        {
            if (M.Column != V.Size)
                throw new Exception("MATRIX*VECTOR: Vector size doesn't match matrix dimensions");

            var result = new Vector(M.Row); // Duck typing 

            for (int i = 0; i < M.Row; i++)
            for (int j = 0; j < M.Column; j++)
                result.Elem[i] += M.Elem[i][j] * V.Elem[j];

            return result;
        }

        public static Matrix operator *(Matrix A, Matrix B) // Mult two matrices 
        {
            if (A.Column != B.Row)
                throw new Exception("MATRIX*MATRIX: Matrices dimensions doesn't match");

            var result = new Matrix(A.Row, B.Column);

            for (int i = 0; i < A.Row; i++)
            for (int j = 0; j < B.Column; j++)
            for (int k = 0; k < A.Column; k++)
                result.Elem[i][j] += A.Elem[i][k] * B.Elem[k][j];

            return result;
        }

        public static bool operator ==(Matrix A, Matrix B)
        {
            return A?.Equals(B) ?? object.ReferenceEquals(B, null);
        }

        public static bool operator !=(Matrix A, Matrix B)
        {
            return !(A == B);
        }

        protected bool Equals(Matrix other)
        {
            if (this.Row != other.Row || this.Column != other.Column)
                return false;

            bool isEqual = true;
            for (int i = 0; i < this.Row; i++)
            for (int j = 0; j < this.Column; j++)
                isEqual &= Math.Abs(this.Elem[i][j] - other.Elem[i][j]) < CONST.EPS;

            return isEqual;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix) obj);
        }

        //поиск номера наибольшей строки матрицы по квадрату евклидовой нормы
        public int NumberRowWithMaxNorm(out double maxSqrRowNorm)
        {
            //проверка на наличие элементов в матрице
            if (Row == 0 || Column == 0) throw new Exception("Error in NumberRowWithMaxNorm: Row = Column = 0 ...");
            //номер строки с наибольшей нормой
            int num = 1;
            maxSqrRowNorm = 0.0;

            //в цикле по оставшимся строкам ищём наибольшую 
            for (int row = 0; row < Row; row++)
            {
                //находим квадрат нормы очередной строки
                double sqrRowNorm = 0.0;
                for (int i = 0; i < Column; i++) sqrRowNorm += Math.Pow(Elem[row][i], 2);
                if (sqrRowNorm > maxSqrRowNorm) { maxSqrRowNorm = sqrRowNorm; num = row + 1; }
            }
            return num;
        }

        //поиск номера наибольшего столбца матрицы по квадрату евклидовой нормы
        public int NumberColumnWithMaxNorm(out double maxSqrColumnNorm)
        {
            //проверка на наличие элементов в матрице
            if (Row == 0 || Column == 0) throw new Exception("Error in NumberColumnWithMaxNorm: Row = Column = 0 ...");
            //номер строки с наибольшей нормой
            int num = 1;
            maxSqrColumnNorm = 0.0;

            //в цикле по оставшимся строкам ищём наибольшую 
            for (int column = 0; column < Column; column++)
            {
                //находим квадрат нормы столбца
                double sqrColumnNorm = 0.0;
                for (int i = 0; i < Row; i++) sqrColumnNorm += Math.Pow(Elem[i][column], 2);
                if (sqrColumnNorm > maxSqrColumnNorm) { maxSqrColumnNorm = sqrColumnNorm; num = column + 1; }
            }
            return num;
        }

        public void HessenbergMatrix()
        {
            //Matrix A = new Matrix(AOrig);

            Vector w = new Vector(Row);
            double beta, mu, s;

            for (int i = 0; i < Column - 2; i++)
            {
                s = 0.0;

                // ||x||^2 for nullify
                for (int I = i + 1; I < Row; I++)
                    s += Math.Pow(Elem[I][i], 2);

                if (Math.Abs(s - Elem[i + 1][i] * Elem[i + 1][i]) > CONST.EPS)
                {
                    beta = Elem[i + 1][i] < 0 ? Math.Sqrt(s) : -Math.Sqrt(s);
                    mu = 1.0 / beta / (beta - Elem[i + 1][i]);

                    for (int I = 0; I < Row; I++)
                    {
                        w.Elem[I] = 0.0;
                        if (I >= i + 1)
                            w.Elem[I] = Elem[I][i];
                    }

                    w.Elem[i + 1] -= beta;

                    // A = H * A
                    for (int m = i; m < Column; m++)
                    {
                        s = 0;
                        for (int n = i; n < Row; n++)
                            s += Elem[n][m] * w.Elem[n];

                        s *= mu;
                        for (int n = i; n < Row; n++)
                            Elem[n][m] -= s * w.Elem[n];
                    }

                    // A = H * A * H
                    for (int m = 0; m < Row; m++)
                    {
                        s = 0;
                        for (int n = 0; n < Row; n++)
                            s += Elem[m][n] * w.Elem[n];

                        s *= mu;
                        for (int n = i; n < Row; n++)
                            Elem[m][n] -= s * w.Elem[n];
                    }

                }
            }
        }


        public void ExcludeRowColumn(int n)
        {
            for (int i = n; i < Row - 1; i++)
            {
                Elem[i] = Elem[i + 1];
                for (int j = n; j < Column - 1; j++)
                    Elem[i][j] = Elem[i][j + 1];
            }
            Row--;
            Column--;
        }
    }
}
