using System;
using System.Reflection;
using ComMethods;
using NUnit.Framework;
using ComMethods_Jacobi = ComMethods.Jacobi;

namespace UnitTest
{
    [TestFixture(Ignore = "Outdated")]
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
                trueRes.Elem[i][j] = 3;

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

    [TestFixture(Ignore = "Outdated")]
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
                A.Elem[i][i] = 1;
                B.Elem[i][i] = 1;
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
                A.Elem[i][i] = 1;

            // Assert
            Assert.That(A != B);
        }

        [TestCase(Author = "Marina")]
        public void When_MatrixMult_ByVector()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            Vector v = new Vector(3);
            Vector trueRes = new Vector(3);
            
            A.Elem[0][0] = 2;
            A.Elem[0][1] = 3;
            A.Elem[0][2] = -1;

            A.Elem[1][0] = 1;
            A.Elem[1][1] = -2;
            A.Elem[1][2] = 1;
            
            A.Elem[2][0] = 1;
            A.Elem[2][1] = 0;
            A.Elem[2][2] = 2;

            v.Elem[0] = 4;
            v.Elem[1] = 0;
            v.Elem[2] = -1;

            trueRes.Elem[0] = 9;
            trueRes.Elem[1] = 3;
            trueRes.Elem[2] = 2;

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
                    A.Elem[i][j] = 1;
                    B.Elem[j][i] = 3;
                }

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    trueRes.Elem[i][j] = 9;

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
                    A.Elem[j][i] = 1;
                    if (j != 1)
                        B.Elem[j][i] = 1;
                    else
                        B.Elem[j][i] = 3;
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
                    A.Elem[i][j] = 1;
                    if (j != 1)
                        B.Elem[i][j] = 1;
                    else
                        B.Elem[i][j] = 3;
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
                    A.Elem[j][i] = 1;
                    B.Elem[j][i] = 1;
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
                    A.Elem[i][j] = 1;

            A.Elem[0][size - 1] = 10;
            A.Elem[1][size - 1] = 15;
            A.Elem[2][size - 1] = 16;

            // Act 
            int rowNum = A.RowNumOfMaxColumnElem(size - 1);

            // Assert
            Assert.That(rowNum == 2);
        }
    }

    [TestFixture(Ignore = "Outdated")]
    public class DirectSolvers
    {
        [TestCase(Author = "Oleg")]
        public void When_Solve_SLE()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            A.Elem[0][0] = 2;
            A.Elem[0][1] = 3;
            A.Elem[0][2] = -1;

            A.Elem[1][0] = 1;
            A.Elem[1][1] = -2;
            A.Elem[1][2] = 1;

            A.Elem[2][0] = 1;
            A.Elem[2][1] = 0;
            A.Elem[2][2] = 2;

            Vector F = new Vector(3);
            F.Elem[0] = 9;
            F.Elem[1] = 3;
            F.Elem[2] = 2;

            LUDecomposition lu = new LUDecomposition();


            // Act
            Vector luRes = lu.StartSolver(A, F);
            Vector gaussRes = Gauss.StartSolver(A, F);

            // Assert
            Assert.That(A * luRes == F);
            Assert.That(A * gaussRes == F);
        }
    }

    [TestFixture(Ignore = "Outdated")]
    public class OrthogonalSolvers
    {
        [TestCase(3)]
        [TestCase(10)]
        public void When_Transpose_Q(int size)
        {
            // Arrange
            Random device = new Random();
            Matrix A = new Matrix(size, size);
            Matrix I = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                I.Elem[i][i] = 1;
                for (int j = 0; j < size; j++)
                    A.Elem[i][j] = device.NextDouble();    
            }
            
            Matrix Q1 = new Matrix(A.Row, A.Column);
            Matrix R1 = new Matrix(A.Row, A.Column);
            
            Matrix Q2 = new Matrix(A.Row, A.Column);
            Matrix R2 = new Matrix(A.Row, A.Column);
            
            Matrix Q3 = new Matrix(A.Row, A.Column);
            Matrix R3 = new Matrix(A.Row, A.Column);
            for (int i = 0; i < A.Row; i++) Q3.Elem[i][i] = 1.0;
            
            Matrix Q4 = new Matrix(A.Row, A.Column);
            Matrix R4 = new Matrix(A.Row, A.Column);
            for (int i = 0; i < A.Row; i++) Q4.Elem[i][i] = 1.0;
            
            GramSchmidt.Classic(A, Q1, R1);
            GramSchmidt.Modified(A, Q2, R2);
            Givens.Orthogon(A, Q3, R3);
            Householder.Orthogon(A, Q4, R4);

            // Act
            Matrix QT1 = Q1.Transpose();
            Matrix QT2 = Q2.Transpose();
            Matrix QT3 = Q3.Transpose();
            Matrix QT4 = Q4.Transpose();

            // Assert
            Assert.That(QT1 * Q1 == I);
            Assert.That(QT2 * Q2 == I);
            Assert.That(QT3 * Q3 == I);
            Assert.That(QT4 * Q4 == I);
        }

        [TestCase(Author = "Artem")]
        public void When_Solve_ORT()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            A.Elem[0][0] = 2;
            A.Elem[0][1] = 3;
            A.Elem[0][2] = -1;

            A.Elem[1][0] = 1;
            A.Elem[1][1] = -2;
            A.Elem[1][2] = 1;
        
            A.Elem[2][0] = 1;
            A.Elem[2][1] = 0;
            A.Elem[2][2] = 2;

            Vector F = new Vector(3);
            F.Elem[0] = 9;
            F.Elem[1] = 3;
            F.Elem[2] = 2;


            // Act
            Vector gramSchmidtC = GramSchmidt.StartModifiedSolverQR(A, F);
            Vector gramSchmidtM = GramSchmidt.StartModifiedSolverQR(A, F);
            Vector givens = Givens.StartSolverQR(A, F);
            Vector householder = Householder.StartSolverQR(A, F);

            // Assert
            Assert.That(A * gramSchmidtC == F);
            Assert.That(A * gramSchmidtM == F);
            Assert.That(A * givens == F);
            Assert.That(A * householder == F);
        }
    }

    [TestFixture]
    public class IterationSolvers
    {
        [TestCase(Description = "Homework")]
        public void MFW_HW()
        {
            var A = new Matrix(4, 4);
            A.Elem[0][0] = 5; A.Elem[0][1] = 20; A.Elem[0][2] = 49; A.Elem[0][3] = 4; 
            A.Elem[1][0] = 55; A.Elem[1][1] = 17; A.Elem[1][2] = 12; A.Elem[1][3] = 19; 
            A.Elem[2][0] = 0; A.Elem[2][1] = 11; A.Elem[2][2] = 47; A.Elem[2][3] = 61; 
            A.Elem[3][0] = 48; A.Elem[3][1] = 60; A.Elem[3][2] = 9; A.Elem[3][3] = 70; 
            
            var F = new Vector(4);
            for (int i = 0; i < 4; i++) F.Elem[i] = 1;

            Householder.StartSolverQR(A, F);
            
            Assert.True(true);
        }
    }
}
