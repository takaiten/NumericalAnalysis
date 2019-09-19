using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ComMethods
{
    class LUDecomposition
    {
        private Matrix LU { set; get; }
        private int _n;
        private double _sum;

        public LUDecomposition()
        {
            _n = 0;
            _sum = 0.0f;
        }

        public LUDecomposition(Matrix A)
        {
            CompactLU(A);
        }

        private void CompactLU(Matrix A)
        {
            _n = A.Row;
            LU = new Matrix(_n, _n);
            
            for (int i = 0; i < _n; i++)
            {
                for (int j = i; j < _n; j++)
                {
                    _sum = 0;
                    for (int k = 0; k < i; k++)
                        _sum += LU.Elem[i, k] * LU.Elem[k, j];
                    LU.Elem[i, j] = A.Elem[i, j] - _sum;
                }

                for (int j = i + 1; j < _n; j++)
                {
                    _sum = 0;
                    for (int k = 0; k < i; k++)
                        _sum += LU.Elem[j, k] * LU.Elem[k, i];
                    LU.Elem[j, i] = (1 / LU.Elem[i, i]) * (A.Elem[j, i] - _sum);
                }
            }
        }

        public Vector StartSolver(Matrix A, Vector F)
        {
            if (LU == null)
                CompactLU(A);

            // LU = L + U - I
            // Find solution of Ly = F
            Vector y = new Vector(_n);
            for (int i = 0; i < _n; i++)
            {
                _sum = 0;
                for (int k = 0; k < i; k++)
                    _sum += LU.Elem[i, k] * y.Elem[k];
                y.Elem[i] = F.Elem[i] - _sum;
            }

            // Find solution of Ux = y
            Vector x = new Vector(_n);
            for (int i = _n - 1; i >= 0; i--)
            {
                _sum = 0;
                for (int k = i + 1; k < _n; k++)
                    _sum += LU.Elem[i, k] * x.Elem[k];
                x.Elem[i] = (1 / LU.Elem[i, i]) * (y.Elem[i] - _sum);
            }

            return x;
        }
    }
}
