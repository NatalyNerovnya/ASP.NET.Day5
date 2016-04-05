using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polynomial;

namespace PolynomialUnitTests
{
    [TestClass]
    public class PolynomialTests
    {
        #region Private Fields
        private Polynomial.Polynomial pol1 = new Polynomial.Polynomial(0, 3, 0, 4);
        private Polynomial.Polynomial pol2 = new Polynomial.Polynomial(1, 5, 2, 3, 1);
#endregion 


        [TestMethod]
        public void Plus_TwoPolynomial_SumPolynomial()
        {
            Polynomial.Polynomial expected = new Polynomial.Polynomial(1, 8, 2, 7, 1);

            Polynomial.Polynomial result = pol1 + pol2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Plus_NullAndPolynomial_NullArgumentExeprion()
        {
            Polynomial.Polynomial pol2 = null;
            Polynomial.Polynomial expected = new Polynomial.Polynomial(1, 8, 2, 7, 1);

            Polynomial.Polynomial result = pol1 + pol2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Negate_Polynomial_PolynomialOfDifferentSign()
        {
            Polynomial.Polynomial expected = new Polynomial.Polynomial(-1, -5, -2, -3, -1);

            Polynomial.Polynomial result = Polynomial.Polynomial.Negate(pol2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiply_PolynomialAndDouble_Polynomial()
        {
            Polynomial.Polynomial expected = new Polynomial.Polynomial(3, 15, 6, 9, 3);
            double x = 3;

            Polynomial.Polynomial result = pol2 * x;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Divide_TwoPolynomial_Polynomial()
        {
            Polynomial.Polynomial expected = new Polynomial.Polynomial(.75, .25);

            Polynomial.Polynomial result = pol2 / pol1;

            Assert.AreEqual(expected, result);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Divide_TwoPolynomial_ArgumentExeption()
        {
            Polynomial.Polynomial expected = new Polynomial.Polynomial(.75, .25);

            Polynomial.Polynomial result = pol1 / pol2;

            Assert.AreEqual(expected, result);
        }
    }
}
