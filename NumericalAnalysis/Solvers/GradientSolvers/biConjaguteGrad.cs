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

    }
}
