using System;
using System.ComponentModel.Design;

namespace ComMethods
{
    class conjugateGrad
    {
        public int MaxIter { set; get; }
        public int Iter { set; get; }
        public double Eps { set; get; }

        public Preconditioner Preconditioner { set; get; }

        public conjugateGrad(int maxIter, double eps)
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
            var r = new Vector(n);
            var p = new Vector(n);
            var vec = new Vector(n);
            
            // methods parameters 
            double alpha, beta, sc1, sc2;
            bool flag = true;
            
            // residual norm
            double rNorm = 0;
            
            // residual: r = F - Ax
            A.MultMtV(res, vec);
            for (int i = 0; i < n; i++)
                r.Elem[i] = F.Elem[i] - vec.Elem[i];
            
            // search vector: Mp = r
            Preconditioner.StartPreconditioner(r, p);
            
            // iterative process
            while (flag && Iter < MaxIter)
            {
                sc1 = p * r;       // (M^(-1) * r; r)
                A.MultMtV(p, vec); // vec = A * p
                sc2 = vec * p;     // sc2 = (A * p; p)
                alpha = sc1 / sc2; // linear search coefficient

                // result vector & residual
                for (int i = 0; i < n; i++)
                {
                    res.Elem[i] += alpha * p.Elem[i];
                    r.Elem[i] -= alpha * vec.Elem[i];
                }

                Preconditioner.StartPreconditioner(r, vec); // vec := M^(-1) * r
                sc2 = vec * r;      // (M^(-1) * r; r)
                beta = sc2 / sc1;   // coefficient in choosing a search direction
                rNorm = r.Normal(); // residual norm

                if (rNorm < Eps)
                    flag = false;

                if (flag)
                    for (int i = 0; i < n; i++)
                        p.Elem[i] = vec.Elem[i] + beta * p.Elem[i];
                Iter++;

                Console.WriteLine("\n{0, -20} {1, 20:E}", Iter, rNorm);
            }

            return res;
        }
    }
}