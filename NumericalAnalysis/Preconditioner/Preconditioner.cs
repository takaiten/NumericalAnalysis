using System;

namespace ComMethods
{
    public abstract class Preconditioner
    {
        public abstract void StartPreconditioner(Vector x, Vector res);
        public abstract void StartTrPreconditioner(Vector x, Vector res);
        public enum PreconditionerType
        {
            Diagonal = 1,
            ILU // TODO: Create class for LU
        }
    }

    class DiagonalPreconditioner : Preconditioner
    {
        private Vector diag { get; }
        public override void StartPreconditioner(Vector x, Vector res)
        {
            for (int i = 0; i < diag.Size; i++)
            {
                if (diag.Elem[i] < CONST.EPS)
                    throw new Exception("Division by zero");
                res.Elem[i] = x.Elem[i] / diag.Elem[i];
            }
        }
        public override void StartTrPreconditioner(Vector x, Vector res)
        { StartPreconditioner(x, res); }
        
        public DiagonalPreconditioner(CSlRMatrix A)
        {
            diag = new Vector(A.Row);
            for (int i = 0; i < A.Row; i++) 
                diag.Elem[i] = A.di[i];
        }
    }
    class IncompleteLUPreconditioner : Preconditioner
    {
        private CSlRMatrix ILU { get; }
        public override void StartPreconditioner(Vector x, Vector res)
        {
            ILU.SlauL(res, x);
            ILU.SlauU(res, res);
        }

        public override void StartTrPreconditioner(Vector x, Vector res)
        {
            ILU.SlauUt(res, x);
            ILU.SlauLt(res, res);
        }
        public IncompleteLUPreconditioner(CSlRMatrix A)
        {
            ILU = A.ILU_CSlR();
        }
    }
}