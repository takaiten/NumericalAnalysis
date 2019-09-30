using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace ComMethods
{
    public static class CONST
    {
        public static readonly double EPS = 1e-8;
        
        public static string MeasureTime(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";

            return ("RunTime: " + elapsedTime);
        }

        public static double RelativeError(Vector a, Vector b)
        {
            double s = 0.0f;
            for (int i = 0; i < a.Size; i++)
                s += Math.Pow(a.Elem[i] - b.Elem[i], 2);
            return Math.Sqrt(s / (b * b));
        }

        public static double RelativeDiscrepancy(Matrix A, Vector x, Vector f)
        {
            var q = A * x;
            for (int i = 0; i < x.Size; i++)
                q.Elem[i] -= f.Elem[i];
            return Math.Sqrt((q * q) / (f * f));
        }
    }
}
