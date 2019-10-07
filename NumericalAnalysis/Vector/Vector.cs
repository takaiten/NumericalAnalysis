using System;
using System.IO;

namespace ComMethods
{
    public interface IVector
    {
        int Size { get; }
    }

    public class Vector : IVector
    {
        public int Size { get; }
        public double[] Elem { get; }

        public Vector()
        {
        }

        public Vector(int size)
        {
            Size = size;
            Elem = new double[size];
        }

        public Vector(Vector inp)
        {
            Size = inp.Size;
            Elem = new double[Size];
            for (int i = 0; i < Size; i++)
                Elem[i] = inp.Elem[i];
        }

        public Vector(string Path)
        {
            using (var Reader = new BinaryReader(File.Open(Path + "Size.bin", FileMode.Open)))
            {
                try
                {
                    Size = Reader.ReadInt32();
                }
                catch { throw new Exception("Size.bin: file isn't correct"); }
            }

            using (var Reader = new BinaryReader(File.Open(Path + "F.bin", FileMode.Open)))
            {
                try
                {
                    for (int i = 0; i < Size; i++)
                    {
                        Elem[i] = new double();
                        Elem[i] = Reader.ReadDouble();

                    }
                }

                catch { throw new Exception("F.bin: file isn't correct"); }
            }
        }

        // Methods
        public void Print()
        {
            foreach (var e in Elem)
                Console.WriteLine(e);
        }

        public Matrix MultColumnByRow(Vector r)
        {
            if (Size != r.Size)
                throw new Exception("VECTOR*VECTOR: Vectors dimensions doesn't match");

            Matrix res = new Matrix(Size, Size);
            for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                res.Elem[i][j] = Elem[i] * r.Elem[j];

            return res;
        }

        public double MultRowByColumn(Vector c)
        {
            if (Size != c.Size)
                throw new Exception("VECTOR*VECTOR: Vectors dimensions doesn't match");

            double res = new double();
            for (int i = 0; i < Size; i++)
                res += Elem[i] * c.Elem[i];

            return res;
        }

        public void Copy(Vector v2)
        {
            if (Size != v2.Size)
                throw new Exception("Copy: Vectors dimensions doesn't match");

            for (int i = 0; i < Size; i++)
                Elem[i] = v2.Elem[i];
        }

        public Vector Copy()
        {
            return new Vector(this);
        }

        public void SwitchElems(int firstIndex, int secondIndex)
        {
            double temp = Elem[firstIndex];
            Elem[firstIndex] = Elem[secondIndex];
            Elem[secondIndex] = temp;
        }

        public void CopyColumnFromMatrix(Matrix A, int column)
        {
            for (int i = 0; i < A.Row; i++) 
                Elem[i] = A.Elem[i][column];
        }
        
        public void CopyRowFromMatrix(Matrix A, int row)
        {
            for (int i = 0; i < A.Column; i++) 
                Elem[i] = A.Elem[row][i];
        }

        public double Normal()
        {
            return Math.Sqrt(this*this);
        }
        
        // Operator overloads
        public static Vector operator *(Vector v, double x) // Mult vector by a scalar
        {
            Vector res = new Vector(v.Size);
            for (int i = 0; i < v.Size; i++)
                res.Elem[i] = v.Elem[i] * x;
            return res;
        }

        public static double operator *(Vector a, Vector b) // Dot product of two vectors
        {
            if (a.Size != b.Size)
                throw new Exception("VECTOR*VECTOR: Vectors dimensions doesn't match");

            double res = 0.0f;
            for (int i = 0; i < a.Size; i++)
                res += a.Elem[i] * b.Elem[i];

            return res;
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return a?.Equals(b) ?? object.ReferenceEquals(b, null);
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        protected bool Equals(Vector other)
        {
            if (this.Size != other.Size)
                return false;

            bool isEqual = true;
            for (int i = 0; i < this.Size; i++)
                isEqual &= Math.Abs(this.Elem[i] - other.Elem[i]) < CONST.EPS;

            return isEqual;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector) obj);
        }
    }
}
