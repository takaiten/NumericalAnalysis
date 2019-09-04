using System;

namespace ComMethods
{
    public interface IVector
    {
        int Size { set; get; }
    }

    class Vector : IVector
    {
        public int Size { set; get; }
        public double[] Elem { set; get; }

        public Vector()
        {
        }

        public Vector(int size)
        {
            Size = size;
            Elem = new double[size];
        }

        // Methods
        public void Print()
        {
            foreach (var e in Elem) 
                Console.WriteLine(e);
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
                isEqual &= Math.Abs(a.Elem[i] - b.Elem[i]) < 1e-9;

            return isEqual;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }
    }
}