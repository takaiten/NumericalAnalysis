using System;
using System.Collections.Generic;

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

        public void Transpose()
        {
            double temp;

            for (int i = 0; i < Row; i++)
                for (int j = i + 1; j < Column; j++)
                {
                    temp = Elem[i][j];
                    Elem[i][j] = Elem[j][i];
                    Elem[j][i] = temp;
                }
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
