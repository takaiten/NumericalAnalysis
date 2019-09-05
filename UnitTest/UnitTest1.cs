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

        [TestCase(Author = "Oleg")]
        public void When_CalcDotProduct_OfTwoVectors()
        {
            // Arrange
            Vector a = new Vector(3);
            Vector b = new Vector(3);
            for (int i = 0; i < 3; i++)
            {
                a.Elem[i] = 1;
                b.Elem[i] = 5;
            }

            // Act 
            double trueRes = 1 * 5 * 3;
            // Assert
            Assert.That(a * b == trueRes);
        }
        
        [TestCase(Author = "Marina")]
        public void When_ColumnMult_ByRow()
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
            // Act 
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                trueRes.Elem[i, j] = 3;

            // Assert
            Assert.That(a.MultColumnByRow(b) == trueRes);
        }

        [TestCase(Author = "Marina")]
        public void When_RowMult_ByColumn()
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

            // Assert
            Assert.That(a.MultRowByColumn(b) == res);
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

            // Act 
            for (int i = 0; i < 2; i++)
                trueRes.Elem[i] = 9;

            // Assert
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

            // Act 
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    trueRes.Elem[i, j] = 9;

            // Assert
            Assert.That(A * B == trueRes);
        }
    }
}