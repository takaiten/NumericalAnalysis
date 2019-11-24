using System;
using System.Collections.Generic;
using System.Text;

namespace ComMethods
{
    class BiConjugateGrad : IIteratorSolver
    {
        public int MaxIter { set; get; }
        public int Iter { set; get; }
        public double Eps { set; get; }

        public Preconditioner Preconditioner { set; get; }
   
        public BiConjugateGrad(int maxIter, double eps)
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

            // auxiliary vectors
            var r           = new Vector(n);    // ordinary residual
            var r_          = new Vector(n);    // new residual
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
            A.MultMtV(res, vec_help);
            for (int i = 0; i < n; i++)
                vec_help.Elem[i] = F.Elem[i] - vec_help.Elem[i];

            Preconditioner.StartPreconditioner(vec_help, r);

            for (int i = 0; i < n; i++)
            {
                res.Elem[i] = 0.0;
                p.Elem[i] = r_.Elem[i] = p_.Elem[i] = r.Elem[i];
            }

            while (flag && Iter < MaxIter)
            {
                sc1 = r * r_;    // scalar product sc1 = (r; r_)
                A.MultMV(p, vec_help);    // vec_help = A * p
                
                // vec = M^(-1) * A * p
                Preconditioner.StartPreconditioner(vec_help, vec);

                sc2 = vec * p_;           // (M^(-1) * A * p; p_)
                alpha = sc1 / sc2;        // linear search coefficient
                
                // result vector & residual
                for (int i = 0; i < n; i++)
                {
                    res.Elem[i] += alpha * p.Elem[i];
                    r.Elem[i] -= alpha * vec.Elem[i];
                }
                
                // p_ into the preconditioned system
                Preconditioner.StartTrPreconditioner(p_, vec_help);
                
                A.MultMtV(vec_help, vec_);    // vec_ = A_t * M^(-t) * p_
                
                // new residual r_
                for (int i = 0; i < n; i++) 
                    r_.Elem[i] -= alpha * vec_.Elem[i];
                
                sc2 = r * r_;            // (r_new; (r_)_new)
                beta = sc2 / sc1;        // coefficient in choosing a search direction
                rNorm = r.Normal();      // ordinary residual norm

                if (rNorm < Eps) 
                    flag = false;
                
                // new search directions
                if (flag)
                {
                    for (int i = 0; i < n; i++)
                    {
                        p.Elem[i]  = r.Elem[i]  + beta * p.Elem[i];
                        p_.Elem[i] = r_.Elem[i] + beta * p_.Elem[i];
                    }
                }

                Iter++;

//                Console.WriteLine("\n{0, -20} {1, 20:E}", Iter, rNorm);
            }
            Console.WriteLine("Iter: {0, -20} \nNorm: {0, 20:E}", Iter, rNorm);
            
            return res;
        }
    }
}
