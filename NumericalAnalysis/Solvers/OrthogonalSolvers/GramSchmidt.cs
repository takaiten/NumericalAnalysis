using System;

namespace ComMethods
{
    public class GramSchmidt
    {
        public static void Classic(Matrix A, out Matrix Q, out Matrix R)
        {
            int n = A.Row;
            Q = new Matrix(n, n);
            R = new Matrix(n, n);
            Vector q = new Vector(n);

            for (int j = 0; j < A.Column; j++)
            {
                for (int i = 0; i < j; i++)
                    for (int k = 0; k < A.Row; k++)
                        R.Elem[i, j] += A.Elem[k, j] * Q.Elem[k, i];
                q.CopyColumnFromMatrix(A, j);

                for (int i = 0; i < j; i++)
                    for (int k = 0; k < q.Size; k++)
                        q.Elem[k] -= Q.Elem[k, i] * R.Elem[i, j];

                R.Elem[j, j] = q.Normal();

                if (R.Elem[j, j] < CONST.EPS) return;

                for (int i = 0; i < A.Row; i++) 
                    Q.Elem[i, j] = q.Elem[i] / R.Elem[j, j];
            }
        }

        public static void Modified(Matrix A, out Matrix Q, out Matrix R)
        {
            int n = A.Row;
            Q = new Matrix(n, n);
            R = new Matrix(n, n);
            Vector q = new Vector(n);

            for (int j = 0; j < A.Column; j++)
            {
                q.CopyColumnFromMatrix(A, j);

                for (int i = 0; i < j; i++)
                {
                    for (int k = 0; k < q.Size; k++) 
                        R.Elem[i, j] += q.Elem[k] * Q.Elem[k, i];

                    for (int k = 0; k < q.Size; k++) 
                        q.Elem[k] -= R.Elem[i, j] * Q.Elem[k, i];
                }
                
                R.Elem[j, j] = q.Normal();

                if (R.Elem[j, j] < CONST.EPS) return;
                
                for (int i = 0; i < A.Row; i++) 
                    Q.Elem[i, j] = q.Elem[i] / R.Elem[j, j];
            }
        }
        
        public static Vector StartModifiedSolverQR(Matrix A, Vector F)
        {
            Modified(A, out var Q, out var R);
            Vector y = new Vector(Q.Column);
            Vector x = new Vector(Q.Column);
            
            Q.Transpose();
            y = Q * F;
            Substitution.BackRowSubstitution(R, y, x);

            return x;
        }
        
        public static Vector StartClassicSolverQR(Matrix A, Vector F)
        {
            Classic(A, out var Q, out var R);
            Vector y = new Vector(Q.Column);
            Vector x = new Vector(Q.Column);
            
            Q.Transpose();
            y = Q * F;
            Substitution.BackRowSubstitution(R, y, x);

            return x;
        }
    }
}