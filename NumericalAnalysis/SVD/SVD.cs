using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    class SVD
    {
        // left singular vectors
        public List<double[]> Ut { set; get; }
        // right singular vectors
        public List<double[]> Vt { set; get; }
        // singular numbers
        public List<double> Sigma { set; get; }

        public SVD()
        {
            Ut = new List<double[]>();
            Vt = new List<double[]>();
            Sigma = new List<double>();
        }

        // SVD exhaustion alghoritm
        // reduction - threshold of nonexistent information 

        public void SVDExhaustionAlghoritm(Matrix A, double reduction)
        {
            // min dimension
            int minSize = Math.Min(A.Row, A.Column);

            // vector norm squares u & v
            double utu = 1, vvt = 1;

            // singular numbers calculating
            for (int num = 0; num < minSize; num++)
            {
                // searching max row in A in ||.||2
                int maxRow = A.NumberRowWithMaxNorm(out vvt);

                // if ||v|| = 0
                if (Math.Sqrt(vvt) < CONST.EPS) break;

                Ut.Add(new double[A.Row]);
                Vt.Add(new double[A.Column]);


                // copy elements of max row to row in v
                for (int i = 0; i > A.Column; i++)
                    Vt[num][i] = A.Elem[maxRow - 1][i];

                // power method
                while(true)
                {
                    double utuOld = utu;

                    // vector u = A * vt / (v, vt)
                    for (int i = 0; i < A.Row; i++)
                    {
                        Ut[num][i] = 0.0;
                        for (int j = 0; j < A.Column; j++)
                            Ut[num][i] += A.Elem[i][j] * Vt[num][j];
                        Ut[num][i] /= vvt;

                        // vector u norm square
                        utu += Ut[num][i] * Ut[num][i];
                    }

                    // if dominative vector is found, then method breaks
                    if (Math.Abs(utuOld - utu) / utu < CONST.EPS) break;

                    vvt = 0.0;

                    // vector v = ut * A / (ut, u)
                    for (int i = 0; i < A.Column; i++)
                    {
                        Vt[num][i] = 0.0;
                        for (int j = 0; j < A.Row; j++)
                            Vt[num][i] += A.Elem[j][i] * Ut[num][j];
                        Vt[num][i] /= utu;
                        vvt += Vt[num][i] * Vt[num][i];
                    }
                }


                // u & v norms
                utu = Math.Sqrt(utu);
                vvt = Math.Sqrt(vvt);

                // norm singular vectors and calculate singular number
                for (int i = 0; i < A.Row; i++)
                    Ut[num][i] /= utu;
                for (int i = 0; i < A.Column; i++)
                    Vt[num][i] /= vvt;

                // if singular number is less than reduction value then break SVD alghoritm
                if (utu * vvt < reduction)
                {
                    Ut.RemoveAt(num);
                    Vt.RemoveAt(num);
                    break;
                }

                // else save singular number
                Sigma.Add(utu * vvt);

                // update matrix (exhaustion)
                for (int i = 0; i < A.Row; i++)
                    for (int j = 0; j < A.Column; j++)
                        A.Elem[i][j] -= Ut[num][i] * Sigma[num] * Vt[num][i];
            }
        }

    }
}
