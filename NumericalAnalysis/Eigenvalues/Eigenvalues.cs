using System;
using System.Collections.Generic;

namespace ComMethods
{
	class Eigenvalues
	{
		public static List<double> GetEigenvalues(Matrix A, int IterNum)
		{
			List<double> eigenvalues = new List<double>();
			List<double> diag = new List<double>();
			
			A.HessenbergMatrix();
			
			// only for Hessenberg matrix:
			// if element of the lower diagonal = 0 (A[k + 1][k] == 0)
			// then eigenvalue already found		(A[k][k] is eigenvalue)
			List<int> toExclude = new List<int>();
			for (int j = 0; j < A.Row - 1; j++)
				if (Math.Abs(A.Elem[j + 1][j]) <= CONST.EPS)
				{
					eigenvalues.Add(A.Elem[j][j]);
					toExclude.Add(j);
				}

			// exclude excess rows and columns
			for (int j = 0; j < toExclude.Count; j++)
				A.ExcludeRowColumn(toExclude[j] - j);

			// get diagonal of matrix A
			for (int i = 0; i < A.Row; i++)
				diag.Add(A.Elem[i][i]);
			
			// find eigenvalues
			for (int i = 0; i < IterNum; i++)
			{
				if (A.Row == 0)
					break;
				
				QRDecomposition QR = new QRDecomposition(A, QRDecomposition.QRAlgorithm.Givens);
				A = QR.R * QR.Q;
				
				// if the diagonal element hasn't changed
				// then this element is an eigenvalue
				toExclude.Clear();
				for (int j = 0; j < A.Row; j++)
				{
					if (Math.Abs(A.Elem[j][j] - diag[j]) <= CONST.EPS)
					{
						eigenvalues.Add(A.Elem[j][j]);
						toExclude.Add(j);
					}

					diag[j] = A.Elem[j][j];
				}
				
				// exclude excess rows and columns
				for (int j = 0; j < toExclude.Count; j++)
				{
					A.ExcludeRowColumn(toExclude[j] - j);
					diag.RemoveAt(toExclude[j] - j);
				}
			}
			
			for (int i = 0; i < A.Row; i++)
				eigenvalues.Add(A.Elem[i][i]);

			return eigenvalues;
		}
	}
}
