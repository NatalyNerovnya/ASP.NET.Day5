using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    /// <summary>
    /// Class representation of polynomial
    /// </summary>
    public class Polynomial : IEquatable<Polynomial>
    {
        #region Fields
        private readonly double[] coeff = {};
        private int dim;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="arr">Coefficient from the smallest</param>
        public Polynomial(params double[] arr)
        {
            if (arr == null) throw new ArgumentNullException();
            dim = arr.Length;
            coeff = new double[dim];
            Array.Copy(arr, coeff, dim);
        }
        #endregion

        #region Property with parameter
        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="i">Degree of variable</param>
        /// <returns>Coefficient</returns>
        public double this[int i]
        {
            get
            {
                if (i < dim && i >= 0)
                    return coeff[i];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (i < dim && i >= 0)
                    coeff[i] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        #endregion

        #region Public methods

        public double Calculate(double x)
        {
            if(x == Double.NaN || x == Double.PositiveInfinity || x == Double.NegativeInfinity)
                throw new ArgumentException();
            if (ReferenceEquals(this, null))
                throw new ArgumentNullException();

            double result = 0;
            for (int i = 0; i < dim; i++)
            {
                result += this[i] * Math.Pow(x, i);
            }
            return result;
        }
#endregion

        #region Redefinded methods
        /// <summary>
        /// String representation
        /// </summary>
        /// <param name="variable">Variable</param>
        /// <returns>String</returns>
        public string ToString(string variable)
        {
            if (this == null)
                throw new ArgumentNullException();
            if (variable == null)
                variable = "x";

            this.DeleteZerosInTheEnd();
            string result = "";
            if (this[0] != 0)
                result = this[0] + " ";

            for (int i = 1; i < dim; i++)
            {
                if (this[i] != 0)
                {
                    if (this[i] > 0 && !(i == 1 && this[0] == 0))
                        result += "+ ";
                    result += $"{this[i]} {variable}^{i} ";
                }
            }
            result += "= 0";
            return result;
        }

        /// <summary>
        /// String representation
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            if (this == null)
                throw new ArgumentNullException();
            return ToString("x");
        }

        bool IEquatable<Polynomial>.Equals(Polynomial other)
        {
            return Equals(other);
        }

        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            DeleteZerosInTheEnd();
            other.DeleteZerosInTheEnd();
            if (other.dim != this.dim) return false;

            for (int i = 0; i < dim; i++)
                if (this[i].CompareTo(other[i]) != 0)
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Polynomial)obj);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((coeff != null ? coeff.GetHashCode() : 0) * 397) ^ dim;
            }
        }

        #endregion

        #region Operators (symbols)

        /// <summary>
        /// Compare two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second poynomial</param>
        /// <returns>True, if polynomials are equal</returns>
        public static bool operator ==(Polynomial pol1, Polynomial pol2)
        {
            if (ReferenceEquals(pol1, pol2)) return true;
            if (ReferenceEquals(pol1, null)) return false;
            return pol1.Equals(pol2);
        }

        /// <summary>
        /// Compare two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second poynomial</param>
        /// <returns>True, if polynomials aren't equal</returns>
        public static bool operator !=(Polynomial pol1, Polynomial pol2)
        {
            if (ReferenceEquals(pol1, pol2)) return true;
            if (ReferenceEquals(pol1, null)) return false;
            return !pol1.Equals(pol2);
        }

        /// <summary>
        /// Sum two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Sum</returns>
        public static Polynomial operator +(Polynomial pol1, Polynomial pol2)
        {
            return pol1 + pol2;
        }

        /// <summary>
        /// Change sign of polynomial
        /// </summary>
        /// <param name="pol">Polynomial</param>
        /// <returns>Polynomial with other sign</returns>
        public static Polynomial operator -(Polynomial pol)
        {
            return -pol;
        }

        /// <summary>
        /// Substract two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Substraction</returns>
        public static Polynomial operator -(Polynomial pol1, Polynomial pol2)
        {
            return pol1 - pol2;
        }

        /// <summary>
        /// Multiply polynomial and number
        /// </summary>
        /// <param name="pol">Polynomial</param>
        /// <param name="x">Number</param>
        /// <returns>Polynomial * Number</returns>
        public static Polynomial operator *(Polynomial pol, double x)
        {
            return pol * x;
        }

        /// <summary>
        /// Multiply polynomial and number
        /// </summary>
        /// <param name="pol">Polynomial</param>
        /// <param name="x">Number</param>
        /// <returns>Number * Polynomial</returns>
        public static Polynomial operator *(double x, Polynomial pol)
        {
            return pol * x;
        }

        /// <summary>
        /// Multiply two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Product of two polynomials</returns>
        public static Polynomial operator *(Polynomial pol1, Polynomial pol2)
        {
            return pol1 * pol2;
        }

        /// <summary>
        /// Divide two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Quatient of two polynomials</returns>
        public static Polynomial operator /(Polynomial pol1, Polynomial pol2)
        {
            return pol1 / pol2;
        }

        /// <summary>
        /// Divide two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Mod of two polynomials</returns>
        public static Polynomial operator %(Polynomial pol1, Polynomial pol2)
        {
            return pol1 % pol2;
        }
        #endregion

        #region Operators(words)
        /// <summary>
        /// Change sign of polynomial
        /// </summary>
        /// <param name="pol">Polynomial</param>
        /// <returns>Polynomial with other sign</returns>
        public static Polynomial Negate(Polynomial pol)
        {
            if (pol == null)
                throw new ArgumentNullException();
            return pol * (-1);

        }

        /// <summary>
        /// Sum two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Sum</returns>
        public static Polynomial Add(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();

            pol1.DeleteZerosInTheEnd();
            pol2.DeleteZerosInTheEnd();
            int d = Math.Min(pol1.dim, pol2.dim);
            var result = pol1.dim >= pol2.dim ? pol1 : pol2;
            for (int i = 0; i < d; i++)
            {
                checked
                {
                    result[i] = pol1[i] + pol2[i];
                }
            }
            result.DeleteZerosInTheEnd();
            return result;
        }

        /// <summary>
        /// Substract two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Substraction</returns>
        public static Polynomial Subtract(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();
            return pol1 + (-pol2);
        }

        /// <summary>
        /// Multiply two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Product of two polynomials</returns>
        public static Polynomial Multiply(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();


            int n = pol1.dim + pol2.dim;
            double[] prod = new double[n];

            for (int i = 0; i < pol1.dim; i++)
            {
                for (int j = 0; j < pol2.dim; j++)
                {
                    prod[i + j] += pol1[i] * pol2[j];
                }
            }
            Polynomial result = new Polynomial(prod);
            result.DeleteZerosInTheEnd();
            return result;
        }

        /// <summary>
        /// Multiply polynomial and number
        /// </summary>
        /// <param name="pol">Polynomial</param>
        /// <param name="x">Number</param>
        /// <returns>Polynomial * Number</returns>
        public static Polynomial Multiply(Polynomial pol, double x)
        {
            if (pol == null)
                throw new ArgumentNullException();
            var result = new Polynomial(pol.coeff);
            for (int i = 0; i < result.dim; i++)
            {
                checked
                {
                    result[i] *= x;
                }
            }
            result.DeleteZerosInTheEnd();
            return result;

        }

        /// <summary>
        /// Multiply polynomial and number
        /// </summary>
        /// <param name="pol">Polynomial</param>
        /// <param name="x">Number</param>
        /// <returns>Number * Polynomial</returns>
        public static Polynomial Multiply(double x, Polynomial pol)
        {
            return pol * x;
        }

        /// <summary>
        /// Divide two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Quotient of two polynomials</returns>
        public static Polynomial Divide(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();

            return Division(pol1, pol2, true);
        }

        /// <summary>
        /// Divide two polynomials
        /// </summary>
        /// <param name="pol1">First polynomial</param>
        /// <param name="pol2">Second polynomial</param>
        /// <returns>Mod of two polynomials</returns>
        public static Polynomial Mod(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();

            return Division(pol1, pol2, false);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Delete first zero element near the greatest degree of vaariable
        /// </summary>
        private void DeleteZerosInTheEnd()
        {
            for (int i = dim - 1; this[i] == 0; i--)
            {
                dim--;
            }
        }

        /// <summary>
        /// Algorythm of division
        /// </summary>
        /// <param name="pol1">First polynom</param>
        /// <param name="pol2">Second polynom</param>
        /// <param name="flag">True, if you want quotient, false - mod</param>
        /// <returns>Quotient or mod</returns>
        private static Polynomial Division(Polynomial pol1, Polynomial pol2, bool flag)
        {
            if (pol1.dim < pol2.dim)
                throw new ArgumentException();

            double[] quotient = new double[pol1.dim - pol2.dim + 1];
            double[] remainder = (double[])pol1.coeff.Clone();
            for (int i = 0; i < quotient.Length; i++)
            {
                double coeff = remainder[remainder.Length - i - 1] / pol2.coeff.Last();
                quotient[quotient.Length - i - 1] = coeff;
                for (int j = 0; j < pol2.dim; j++)
                {
                    remainder[remainder.Length - i - j - 1] -= coeff * pol2[pol2.dim - j - 1];
                }
            }
            Polynomial result;
            if (flag == true)
                result = new Polynomial(quotient);
            else
                result = new Polynomial(remainder);
            result.DeleteZerosInTheEnd();

            return result;
        }
        #endregion
    }
}

