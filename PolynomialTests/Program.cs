using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polynomial;
//using Polynomial = Polynomial.Polynomial;


namespace PolynomialTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial.Polynomial pol = new Polynomial.Polynomial(3, 0, 2);
            Polynomial.Polynomial pol2 = new Polynomial.Polynomial(0, 1, 5, 6);
            Console.WriteLine("Calculate pol1 in 2 = " + pol.Calculate(2).ToString());
            Console.WriteLine("pol1 : " + pol.ToString());
            Console.WriteLine("pol2: " + pol2.ToString());
            Polynomial.Polynomial pol3 = pol2 - pol;
            Console.WriteLine("pol1 - pol2 : " + pol3.ToString());
            Polynomial.Polynomial pol4 = pol2 * pol;
            Console.WriteLine("pol1 * pol2 : " + pol4.ToString());
            Polynomial.Polynomial pol5 = pol2 / pol;
            Console.WriteLine("pol1 / pol2 : " + pol5.ToString());
            Polynomial.Polynomial pol6 = pol2 % pol;
            Console.WriteLine("pol1 % pol2 : " + pol6.ToString());
            Console.ReadKey();
        }
    }
}
