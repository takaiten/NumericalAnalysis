using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    class biConjugateGrad : IIteratorSolver
    {
        public int MaxIter { set; get; }
        public int Iter { set; get; }
        public double Eps { set; get; }

        public Preconditioner Preconditioner { set; get; }
   
        public biConjugateGrad(int maxIter, double eps)
        {
            MaxIter = maxIter;
            Eps = eps;
            Iter = 0;
        }

        public Vector StartSolver(CSlRMatrix A, Vector F, Preconditioner.PreconditionerType prec)
        {
            switch (prec)
            {
                case ComMethods.Preconditioner.PreconditionerType.Diagonal:
                    {
                        Preconditioner = new DiagonalPreconditioner(A);
                        break;
                    }
                case ComMethods.Preconditioner.PreconditionerType.ILU:
                    {
                        Preconditioner = new IncompleteLUPreconditioner(A);
                        break;
                    }
            }

            int n = A.Row;
            var res = new Vector(n);

            for (int i = 0; i < n; i++)
                res.Elem[i] = 0.0;

            // auxiliary vectors
            var r           = new Vector(n);
            var r_          = new Vector(n);
            var p           = new Vector(n);
            var p_          = new Vector(n);
            var vec         = new Vector(n);
            var vec_        = new Vector(n);
            var vec_help    = new Vector(n);

            // methods parameters 
            double alpha, beta, sc1, sc2;
            bool flag = true;

            // residual norm
            double rNorm = 0;

            // residual: r = M^-1 * (F - Ax)
            A.MultMtV(res, vec);
            for (int i = 0; i < n; i++)
                r.Elem[i] = F.Elem[i] - vec.Elem[i];

            Preconditioner.StartPreconditioner(r, p);
        }
    }
}
