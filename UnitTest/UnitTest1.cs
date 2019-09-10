using System;
using NUnit.Framework;
using ComMethods;

namespace ComMethods.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [TestCase(Author = "Oleg")]
        public void When_Vectors_AreEqual()
        {
            // Arrange
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            // Act 
            for (int i = 0; i < 3; i++)
            {
                a.Elem[i] = 1;
                b.Elem[i] = 1;
            }

            // Assert
            Assert.That(a == b);
        }

        [TestCase(Author = "Oleg")]
        public void When_Vectors_NotEqual()
        {
            // Arrange
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            // Act 
            for (int i = 0; i < 3; i++)
                b.Elem[i] = 1;

            // Assert
            Assert.That(a != b);
        }

        [TestCase(Author = "Oleg")]
        public void When_VectorMult_ByScalar()
        {
            // Arrange
            Vector v = new Vector(3);
            Vector trueRes = new Vector(3);

            for (int i = 0; i < 3; i++)
            {
                v.Elem[i] = 1;
                trueRes.Elem[i] = 3;
            }

            // Act 
            v *= 3;
            // Assert
            Assert.That(v == trueRes);
        }

        [TestCase(Author = "Marina")]
        public void When_VectorColumnMult_ByRow()
        {
            // Arrange
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            Matrix trueRes = new Matrix(3, 3);
            for (int i = 0; i < 3; i++)
            {
                a.Elem[i] = 1;
                b.Elem[i] = 3;
            }

            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                trueRes.Elem[i, j] = 3;

            // Act and Assert
            Assert.That(a.MultColumnByRow(b) == trueRes);
        }

        [TestCase(Author = "Marina")]
        public void When_VectorRowMult_ByColumn()
        {
            //Arrange
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            double res = 9;
            for (int i = 0; i < 3; i++)
            {
                a.Elem[i] = 1;
                b.Elem[i] = 3;
            }

            // Act and Assert
            Assert.That(a.MultRowByColumn(b) == res);
        }

        [TestCase(Author = "Oleg")]
        public void When_CalcDotProduct_OfTwoVectors()
        {
            // Arrange
            double trueRes = 1 * 5 * 3;
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            for (int i = 0; i < 3; i++)
            {
                a.Elem[i] = 1;
                b.Elem[i] = 5;
            }

            // Act and Assert
            Assert.That(a * b == trueRes);
        }

        [TestCase(Author = "Artem")]
        public void When_Copy_Vector_to_Vector()
        {
            // Arrange
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            for (int i = 0; i < 3; i++)
                b.Elem[i] = 2;

            // Act 
            a.Copy(b);

            // Assert
            Assert.That(a == b);
        }
    }

    [TestFixture]
    public class MatrixTests
    {
        [TestCase(Author = "Marina")]
        public void When_Matrices_AreEqual()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            Matrix B = new Matrix(3, 3);
            // Act 
            for (int i = 0; i < 3; i++)
            {
                A.Elem[i, i] = 1;
                B.Elem[i, i] = 1;
            }

            // Assert
            Assert.That(A == B);
        }

        [TestCase(Author = "Marina")]
        public void When_Matrices_NotEqual()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            Matrix B = new Matrix(3, 3);
            // Act 
            for (int i = 0; i < 3; i++)
                A.Elem[i, i] = 1;

            // Assert
            Assert.That(A != B);
        }

        [TestCase(Author = "Marina")]
        public void When_MatrixMult_ByVector()
        {
            // Arrange
            Matrix A = new Matrix(2, 3);
            Vector v = new Vector(3);
            Vector trueRes = new Vector(2);

            for (int i = 0; i < 3; i++)
            {
                v.Elem[i] = 3;
                for (int j = 0; j < 2; j++)
                    A.Elem[j, i] = 1;
            }

            for (int i = 0; i < 2; i++)
                trueRes.Elem[i] = 9;

            // Act and Assert
            Assert.That(A * v == trueRes);
        }

        [TestCase(Author = "Marina")]
        public void When_MatricesMult()
        {
            // Arrange
            Matrix A = new Matrix(2, 3);
            Matrix B = new Matrix(3, 2);
            Matrix trueRes = new Matrix(2, 2);

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 3; j++)
                {
                    A.Elem[i, j] = 1;
                    B.Elem[j, i] = 3;
                }

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    trueRes.Elem[i, j] = 9;

            // Act and Assert
            Assert.That(A * B == trueRes);
        }

        [TestCase(Author = "Marina")]
        public void When_RowMult_ByConst()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            Matrix B = new Matrix(3, 3);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    A.Elem[j, i] = 1;
                    if (j != 1)
                        B.Elem[j, i] = 1;
                    else
                        B.Elem[j, i] = 3;
                }

            // Act 
            A.MultRowByConst(1, 3);

            // Assert
            Assert.That(A == B);
        }

        [TestCase(Author = "Marina")]
        public void When_ColumnMult_ByConst()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            Matrix B = new Matrix(3, 3);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    A.Elem[i, j] = 1;
                    if (j != 1)
                        B.Elem[i, j] = 1;
                    else
                        B.Elem[i, j] = 3;
                }

            // Act 
            A.MultColumnByConst(1, 3);

            // Assert
            Assert.That(A == B);
        }

        [TestCase(Author = "Marina")]
        public void When_Rows_Switched()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            Matrix B = new Matrix(3, 3);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    A.Elem[j, i] = 1;
                    B.Elem[j, i] = 1;
                }

            B.MultRowByConst(2, 3);
            A.MultRowByConst(0, 3);

            // Act 
            A.SwitchRows(0, 2);

            // Assert
            Assert.That(A == B);
        }

        [TestCase(3, Author = "Oleg")]
        [TestCase(10, Author = "Oleg")]
        public void When_FindRow_of_MaxElem(int size)
        {
            // Arrange
            Matrix A = new Matrix(size, size);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    A.Elem[i, j] = 1;

            A.Elem[0, size - 1] = 10;
            A.Elem[1, size - 1] = 15;
            A.Elem[2, size - 1] = 16;

            // Act 
            int rowNum = A.RowNumOfMaxColumnElem(size - 1);

            // Assert
            Assert.That(rowNum == 2);
        }
    }

    [TestFixture]
    public class DirectSolvers
    {
        [TestCase(Author = "Marina")]
        public void When_MatrixTransformedTo_U()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            Matrix B = new Matrix(3, 3);

            A.Elem[0, 0] = 6;   A.Elem[0, 1] = 2;   A.Elem[0, 2] = 3;
            A.Elem[1, 0] = 2;   A.Elem[1, 1] = 1;   A.Elem[1, 2] = 8;
            A.Elem[2, 0] = 3;   A.Elem[2, 2] = 5;

            B.Elem[0, 0] = 6;   B.Elem[0, 1] = 2;   B.Elem[0, 2] = 3;
            B.Elem[1, 1] = -2;  B.Elem[1, 2] = 7;
            B.Elem[2, 2] = -49;
            
            // Act 
            Matrix U = Gauss.DirectWay(A);
            
            // Assert
            Assert.That(U == B);
        }

        [TestCase(Author = "Marina")]
        public void When_LU_Decomposition()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            A.Elem[0, 0] = 6; A.Elem[0, 1] = 2; A.Elem[0, 2] = 3;
            A.Elem[1, 0] = 2; A.Elem[1, 1] = 1; A.Elem[1, 2] = 8;
            A.Elem[2, 0] = 3; A.Elem[2, 2] = 5;
            
            Matrix L = new Matrix(3, 3);
            Matrix U = Gauss.DirectWay(A);
            
            // Act
            LUDecomposition.Transform(A, L, U);

            // Assert
            Assert.That(L * U == A);
        }
        
        [TestCase(2, Author = "Oleg")]
        [TestCase(10, Author = "Oleg")]
        [TestCase(100, Author = "Oleg")]
        public void When_LU_DecompositionRandom(int size)
        {
            // Arrange
            Random device = new Random();
            
            Matrix A = new Matrix(size, size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    A.Elem[i, j] = device.NextDouble();
            
            Matrix L = new Matrix(size, size);
            Matrix U = Gauss.DirectWay(A);

            // Act
            LUDecomposition.Transform(A, L, U);

            // Assert
            Assert.That(L * U == A);
        }
        
        [TestCase(Author = "Oleg")]
        public void When_GetSolution_Gauss()
        {
            // Arrange
            Matrix A = new Matrix(2, 2);
            A.Elem[0, 0] = 5; A.Elem[0, 1] = 6;
            A.Elem[1, 0] = 10; A.Elem[1, 1] = 13;
            
            Vector F = new Vector(2);
            F.Elem[0] = 1;
            F.Elem[1] = 3;
            
            Vector trueRes = new Vector(2);
            trueRes.Elem[0] = -1;
            trueRes.Elem[1] = 1;

            // Act 
            Vector res = Gauss.StartSolver(A, F);

            // Assert
            Assert.That(res == trueRes);
        }
    }
}
