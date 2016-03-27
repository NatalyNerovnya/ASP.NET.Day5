using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    public class Polynomial : IEquatable<Polynomial>
    {
        #region Fields
        private readonly double[] coeff;
        private int dim;
        #endregion

        #region Constructors
        public Polynomial() { }

        public Polynomial(params double[] arr)
        {
            if (arr == null)
                throw new ArgumentNullException();

            dim = arr.Length - 1;
            coeff = new double[dim + 1];
            Array.Copy(arr, coeff, dim + 1);
        }

        #endregion

        #region Properties
        public double this[int i]
        {
            get
            {
                if (i <= dim)
                    return coeff[i];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (i <= dim)
                    coeff[i] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        #endregion

        #region Redefined public methods

        public override string ToString()
        {
            if (this == null)
                throw new ArgumentNullException();
            return ToSrtring("x");
        }

        public string ToSrtring(string variable)
        {
            if (this == null)
                throw new ArgumentNullException();
            if (variable == null)
                variable = "x";

            this.DeleteZeros();
            string result = this[dim + 1].ToString() + " " + variable + "^" + (dim + 1).ToString() + " " ;
           
            for (int i = dim; i >= 0; i++)
            {
                if (this[i] != 0)
                {
                    if (this[i] > 0)
                        result += "+ ";
                    result += this[i].ToString() + variable + "^" + i.ToString() + " ";
                }
            }
            result += "= 0";
            return result;
        }

        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            DeleteZeros();
            other.DeleteZeros();
            if (other.dim != this.dim) return false;

            for (int i = 0; i <= dim; i++)
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

        public override int GetHashCode()
        {
            unchecked
            {
                return ((coeff != null ? coeff.GetHashCode() : 0) * 397) ^ dim;
            }
        }


        #endregion

        #region Operators
        public static bool operator ==(Polynomial pol1, Polynomial pol2)
        {
            if (ReferenceEquals(pol1, pol2)) return true;
            if (ReferenceEquals(pol1, null)) return false;
            return pol1.Equals(pol2);
        }

        public static bool operator !=(Polynomial pol1, Polynomial pol2)
        {
            return !(pol1 == pol2);
        }

        public static Polynomial operator +(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();

            pol1.DeleteZeros();
            pol2.DeleteZeros();
            int d = Math.Min(pol1.dim, pol2.dim);
            var result = pol1.dim >= pol2.dim ? pol1 : pol2;
            for (int i = 0; i <= d; i++)
            {
                checked
                {
                    result[i] = pol1[i] + pol2[i];
                } 
            }
            result.DeleteZeros();
            return result;
        }

        public static Polynomial operator -(Polynomial pol)
        {
            if (pol == null)
                throw new ArgumentNullException();
            return pol * (-1);
        }

        public static Polynomial operator -(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();
            return pol1 + (-pol2);
        }

        public static Polynomial operator *(Polynomial pol, double x)
        {
            if (pol == null)
                throw new ArgumentNullException();
            if (x == Double.NaN || x == Double.NegativeInfinity || x == Double.PositiveInfinity)
                throw new ArgumentNullException();
            
            for (int i = 0; i <= pol.dim; i++)
            {
                checked
                {
                    pol[i] *= x;
                }
            }
            pol.DeleteZeros();
            return pol;

        }

        public static Polynomial operator *(double x, Polynomial pol)
        {
            return pol * x;
        }

        public static Polynomial operator *(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();


            int n = pol1.dim + pol2.dim + 2;
            double[] prod = new double[n];
            for (int i = 0; i <= pol1.dim; i++)
            {
                for (int j = 0; j <= pol2.dim; j++)
                {
                    prod[i + j] += pol1[i] * pol2[j];
                }
            }
            Polynomial result = new Polynomial(prod);
            result.DeleteZeros();
            return result;
        }

        public static Polynomial operator /(Polynomial pol1, Polynomial pol2)
        {
            if (pol1 == null || pol2 == null)
                throw new ArgumentNullException();
            if(pol1.dim < pol2.dim)
                throw new ArgumentException();
            pol1.coeff.Reverse();
            pol2.coeff.Reverse();


            double[] quotient = new double[pol1.dim - pol2.dim + 1];
            double[] remainder = (double[])pol1.coeff.Clone();
            for (int i = 0; i < quotient.Length; i++)
            {
                double coeff = remainder[remainder.Length - i - 1] / pol2.coeff.Last();
                quotient[quotient.Length - i - 1] = coeff;
                for (int j = 0; j <= pol2.dim; j++)
                {
                    remainder[remainder.Length - i - j - 1] -= coeff * pol2[pol2.dim - j];
                }
            }

            Polynomial result = new Polynomial((double[])quotient.Reverse());
            result.DeleteZeros();
            return result;
        }

        public static Polynomial Negate(Polynomial pol)
        {
            return -pol;
        }

        public static Polynomial Add(Polynomial pol1, Polynomial pol2)
        {
            return pol1 + pol2;
        }

        public static Polynomial Subtract(Polynomial pol1, Polynomial pol2)
        {
            return pol1 - pol2;
        }

        public static Polynomial Multiply(Polynomial pol1, Polynomial pol2)
        {
            return pol1 * pol2;
        }

        public static Polynomial Multiply(Polynomial pol, double x)
        {
            return pol * x;
        }

        public static Polynomial Devide(Polynomial pol1, Polynomial pol2)
        {
            return pol1 / pol2;
        }

        public static Polynomial Multiply(double x, Polynomial pol)
        {
            return pol * x;
        }

        #endregion

        #region Private methods
        private void DeleteZeros()
        {
            if(this == null)
                throw new ArgumentNullException();
            if (dim == 0)
                return;
            coeff.Reverse();
            DeleteZerosInTheEnd();
            coeff.Reverse();
            DeleteZerosInTheEnd();

        }

        private void DeleteZerosInTheEnd()
        {
            if (this == null)
                throw new ArgumentNullException();
            if (dim == 0)
                return;
            for (int i = dim - 1; i >= 0; i--)
            {
                if (this[i] == 0)
                    dim--;
            }
        }
        #endregion
    }


}
