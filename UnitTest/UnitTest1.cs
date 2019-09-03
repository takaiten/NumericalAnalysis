using NUnit.Framework;
using ComMethods;

namespace ComMethods.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [TestCase]
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

        [TestCase]
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

        [TestCase]
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

        [TestCase]
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
    }

    [TestFixture]
    public class MatrixTests
    {
        [TestCase]
        public void When()
        {
            Assert.Pass();
        }
    }
}
