using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace ComMethods
{
    public class CONST
    {
        public static double EPS = 1e-9;

        public static string MeasureTime(Thread thread)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            thread.Start();
            while (thread.IsAlive)
            {
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

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
