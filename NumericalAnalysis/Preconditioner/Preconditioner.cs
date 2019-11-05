namespace ComMethods
{
    abstract class Preconditioner
    {
        public abstract void StartPreconditioner(Vector x, Vector res);
        public abstract void StartTvPreconditioner(Vector x, Vector res);
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
            // TODO: Division by zero in diag
            for (int i = 0; i < diag.Size; i++) 
                res.Elem[i] = x.Elem[i] / diag.Elem[i];
        }
        public override void StartTvPreconditioner(Vector x, Vector res)
        { StartPreconditioner(x, res); }
        
        public DiagonalPreconditioner(Matrix A)
        {
            diag = new Vector(A.Row);
            for (int i = 0; i < A.Row; i++) 
                diag.Elem[i] = A.Elem[i][i];
        }
    }
}