using System;
using System.Collections.Generic;

namespace ComMethods
{
    public interface IMatrix
    {
        int Row { set; get; }
        int Column { set; get; }
    }

    class Matrix : IMatrix
    {
        public int Row { set; get; }
        public int Column { set; get; }
        public double[,] Elem { set; get; }

        public Matrix() { }

        public Matrix(int row, int column)
        {
            Row = row;
            Column = column;
            Elem = new double[row, column];
        }

        public static Vector operator *(Matrix M, Vector V) // Mult matrix by a vector 
        {
            if (M.Column != V.Size)
            {
                throw new Exception("MATRIX*VECTOR: Vector size doesn't match matrix dimensions");
            }
            var result = new Vector(V.Size);      			// Duck typing 
            for (int i = 0; i < M.Row; i++)
            {
                for (int j = 0; j < M.Column; j++)
                {
                    result.Elem[i] += M.Elem[i, j] * V.Elem[j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix A, Matrix B) // Mult two matrices 
        {
            if (A.Column != B.Row)
            {
                throw new Exception("MATRIX*MATRIX: Matrices dimensions doesn't match");
            }
            var result = new Matrix(A.Row, B.Column);
            for (int i = 0; i < A.Row; i++)
            {
                for (int j = 0; j < B.Column; j++)
                {
                    for (int k = 0; k < A.Column; k++)
                    {
                        result.Elem[i, j] += A.Elem[i, k] * B.Elem[k, j];
                    }
                }
            }
            return result;
        }
        public void Print()
        {
            for (int i = 0; i < Column; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    Console.Write(Elem[i, j]);
                    Console.Write('\t');
                }
                Console.Write('\n');
            }
        }
    }
}
