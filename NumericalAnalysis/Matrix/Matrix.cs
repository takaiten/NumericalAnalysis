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
        public int Row { get; }
        public int Column { get; }
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

        public Matrix(string Path)
        {
            using(var Reader = new BinaryReader(File.Open(Path + "Size.bin", FileMode.Open)))
            {
                try
                {
                    Row = Reader.ReadInt32();
                    Column = Row;
                }
                catch { throw new Exception("Size.bin: file isn't correct"); }
            }

            using (var Reader = new BinaryReader(File.Open(Path + "Matrix.bin", FileMode.Open)))
            {
                try
                {
                    for (int i = 0; i < Row; i++)
                    {
                        Elem[i] = new double[Column];
                        for (int j = 0; j < Column; j++)
                            Elem[i][j] = Reader.ReadDouble();
                    }
                }

                catch { throw new Exception("Matrix.bin: file isn't correct"); }
            }
        }

        // Methods 
        public void Print()
        {
            for (int i = 0; i < Column; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    Console.Write(Elem[i][j]);
                    Console.Write('\t');
                }

                Console.Write('\n');
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
                for (int j = i + 1; j < Column; j++)
                {
                    res.Elem[i][j] = Elem[j][i];
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

            while (Array.IndexOf<bool>(semaphores, false) != -1);

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
    }
}
