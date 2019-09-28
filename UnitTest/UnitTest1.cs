using System;
using ComMethods;
using NUnit.Framework;
using ComMethods_Givens = ComMethods.Givens;
using ComMethods_GramSchmidt = ComMethods.GramSchmidt;
using ComMethods_Housholder = ComMethods.Housholder;

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
            Matrix A = new Matrix(3, 3);
            Vector v = new Vector(3);
            Vector trueRes = new Vector(3);
            
            A.Elem[0, 0] = 2;
            A.Elem[0, 1] = 3;
            A.Elem[0, 2] = -1;

            A.Elem[1, 0] = 1;
            A.Elem[1, 1] = -2;
            A.Elem[1, 2] = 1;
            
            A.Elem[2, 0] = 1;
            A.Elem[2, 1] = 0;
            A.Elem[2, 2] = 2;

            v.Elem[0] = 4;
            v.Elem[1] = 0;
            v.Elem[2] = -1;

            trueRes.Elem[0] = 9;
            trueRes.Elem[1] = 3;
            trueRes.Elem[2] = 2;
           
            
//            for (int i = 0; i < 3; i++)
//            {
//                v.Elem[i] = 3;
//                for (int j = 0; j < 2; j++)
//                    A.Elem[j, i] = 1;
//            }
//
//            for (int i = 0; i < 2; i++)
//                trueRes.Elem[i] = 9;

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
        [TestCase(Author = "Oleg")]
        public void When_Solve_SLE()
        {
            // Arrange
            Matrix A = new Matrix(3, 3);
            A.Elem[0, 0] = 2;
            A.Elem[0, 1] = 3;
            A.Elem[0, 2] = -1;

            A.Elem[1, 0] = 1;
            A.Elem[1, 1] = -2;
            A.Elem[1, 2] = 1;

            A.Elem[2, 0] = 1;
            A.Elem[2, 1] = 0;
            A.Elem[2, 2] = 2;

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

    [TestFixture]
    public class OrthogonalSolvers
    {
        [TestCase(1)]
        public void When_Transpose_Q(int size)
        {
            // Arrange
            Random device = new Random();
            Matrix A = new Matrix(size, size);
            Matrix I = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                I.Elem[i, i] = 1;
                for (int j = 0; j < size; j++)
                    A.Elem[i, j] = device.NextDouble();    
            }
            
            ComMethods_GramSchmidt.Classic(A, out var Q1, out var R1);
            ComMethods_GramSchmidt.Modified(A, out var Q2, out var R2);
            ComMethods_Givens.Orthogon(A, out var Q3, out var R3);
            ComMethods_Housholder.Orthogon(A, out var Q4, out var R4);

            Matrix QT1 = new Matrix(Q1);
            Matrix QT2 = new Matrix(Q2);
            Matrix QT3 = new Matrix(Q3);
            Matrix QT4 = new Matrix(Q4);

            // Act
            QT1.Transpose();
            QT2.Transpose();
            QT3.Transpose();
            QT4.Transpose();

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
            A.Elem[0, 0] = 2;
            A.Elem[0, 1] = 3;
            A.Elem[0, 2] = -1;

            A.Elem[1, 0] = 1;
            A.Elem[1, 1] = -2;
            A.Elem[1, 2] = 1;

            A.Elem[2, 0] = 1;
            A.Elem[2, 1] = 0;
            A.Elem[2, 2] = 2;

            Vector F = new Vector(3);
            F.Elem[0] = 9;
            F.Elem[1] = 3;
            F.Elem[2] = 2;


            // Act
            Vector gramSchmidt = ComMethods_GramSchmidt.StartSolverQR(A, F);
            Vector givens = ComMethods_Givens.StartSolverQR(A, F);
            Vector housholder = ComMethods_Housholder.StartSolverQR(A, F);

            // Assert
            Assert.That(A * gramSchmidt == F);
            Assert.That(A * givens == F);
            Assert.That(A * housholder == F);
        }
    }
}
