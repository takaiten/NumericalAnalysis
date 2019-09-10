using System;

namespace ComMethods
{
    public interface IVector
    {
        int Size { get; }
    }

    class Vector : IVector
    {
        public int Size { get; }
        public double[] Elem { set; get; }

        public Vector()
        {}

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
                    res.Elem[i, j] = Elem[i] * r.Elem[j];

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
            if (a.Size != b.Size)
                return false;

            bool isEqual = true;
            for (int i = 0; i < a.Size; i++)
                isEqual &= Math.Abs(a.Elem[i] - b.Elem[i]) < CONST.EPS;

            return isEqual;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }
    }
}
