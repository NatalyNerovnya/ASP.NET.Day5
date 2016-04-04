using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Polynomial;

namespace PolynomialNUnitTests
{
    [TestFixture]
    public class PolynomialTests
    {

        #region Plus
        public IEnumerable<TestCaseData> TestPlusData
        {
            get
            {
                yield return new TestCaseData(new Polynomial.Polynomial(0, 1), null).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData(new Polynomial.Polynomial(1, 2, 3), new Polynomial.Polynomial(2, 3, 4)).Returns(new Polynomial.Polynomial(3, 5, 7));
                yield return new TestCaseData(new Polynomial.Polynomial(-4, -2, 0), new Polynomial.Polynomial(2, 3, 4, 5)).Returns(new Polynomial.Polynomial(-2, 1, 4, 5));
              
            }
        }

        [Test, TestCaseSource(nameof(TestPlusData))]
        public Polynomial.Polynomial PlusOperator_AddTwoPolynomialsWithYield(Polynomial.Polynomial pol1, Polynomial.Polynomial pol2)
        {
            return pol1 + pol2;
        }

        #endregion

        #region Multiplication

        public IEnumerable<TestCaseData> TestMultiplyData
        {
            get
            {
                yield return new TestCaseData(new Polynomial.Polynomial(0, 1), null).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData(new Polynomial.Polynomial(1, 2), new Polynomial.Polynomial(1)).Returns(new Polynomial.Polynomial(1, 2));
                yield return new TestCaseData(new Polynomial.Polynomial(-4, -2, 0), new Polynomial.Polynomial(2, 3, 4, 5)).Returns(new Polynomial.Polynomial(-8, -16, -22, -28, -10));
                
            }
        }

        [Test, TestCaseSource(nameof(TestMultiplyData))]
        public Polynomial.Polynomial MultiplyOperator_MultiplyPolynomailsWithYield(Polynomial.Polynomial pol1, Polynomial.Polynomial pol2)
        {
            return pol1 * pol2;
        }

        public IEnumerable<TestCaseData> TestMultiplyOnDoubleData
        {
            get
            {
                yield return new TestCaseData(null, 2).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData(new Polynomial.Polynomial(1, 2), 2).Returns(new Polynomial.Polynomial(2, 4));
                yield return new TestCaseData(new Polynomial.Polynomial(-4, -2, 3, 0), -2).Returns(new Polynomial.Polynomial(8, 4, -6));
                
            }
        }

        [Test, TestCaseSource(nameof(TestMultiplyOnDoubleData))]
        public Polynomial.Polynomial MultiplyOperator_MultiplyPolynomailOnDoubleWithYield(Polynomial.Polynomial pol, double x)
        {
            return pol*x;
        }
        #endregion

        #region Equals
        public IEnumerable<TestCaseData> TestEqualsData
        {
            get
            {
                yield return new TestCaseData(new Polynomial.Polynomial(1, 2, 3)).Returns(true);
                yield return new TestCaseData(new Polynomial.Polynomial(1, 2, 3, 0, 0, 0)).Returns(true);
                yield return new TestCaseData(new Polynomial.Polynomial(1, 2)).Returns(false);
                yield return new TestCaseData(null).Returns(false);
            }
        }

        [Test, TestCaseSource(nameof(TestEqualsData))]
        public bool Equals_CompareTwoPolynomialsWithYield(Polynomial.Polynomial pol)
        {
            Polynomial.Polynomial pol1 = new Polynomial.Polynomial(1, 2, 3);
            return pol1.Equals(pol);
        }
        #endregion

        #region ToString
        public IEnumerable<TestCaseData> TestToStringData
        {
            get
            {
                yield return new TestCaseData().Returns("1 + 2 x^1 + 3 x^2 = 0");
            }
        }

        [Test, TestCaseSource(nameof(TestToStringData))]
        public string ToString_ReturnStringWithYield()
        {
            Polynomial.Polynomial pol = new Polynomial.Polynomial(1, 2, 3);
            return pol.ToString();
        }

        #endregion

    }
}
