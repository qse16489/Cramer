using System;
using System.Numerics;

namespace CramerMethod
{
    public class CramerSolver
    {
        private Complex[,] matrix = new Complex[2, 2];
        private Complex[] constants = new Complex[2];

        public void InputCoefficients()
        {
            Console.WriteLine("Введите коэффициенты матрицы 2x2 и свободные члены:");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write($"Введите a{i + 1}{j + 1}: ");
                    matrix[i, j] = ParseComplex(Console.ReadLine());
                }
                Console.Write($"Введите b{i + 1}: ");
                constants[i] = ParseComplex(Console.ReadLine());
            }
        }

        public void Solve()
        {
            Complex determinant = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            if (Complex.Abs(determinant) < 1e-10)
            {
                Console.WriteLine("Система не имеет единственного решения.");
                return;
            }

            Complex determinantX = constants[0] * matrix[1, 1] - constants[1] * matrix[0, 1];
            Complex determinantY = matrix[0, 0] * constants[1] - matrix[1, 0] * constants[0];

            Complex x = determinantX / determinant;
            Complex y = determinantY / determinant;

            Console.WriteLine($"Решение системы: X = {FormatComplex(x)}, Y = {FormatComplex(y)}");
        }

        private Complex ParseComplex(string input)
        {
            input = input.Replace(" ", "").Replace("j", "i");

            if (double.TryParse(input, out double real))
            {
                return new Complex(real, 0);
            }

            string[] parts = input.Split(new[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2)
            {
                double imaginary = double.Parse(parts[1].TrimEnd('i'));
                if (input.Contains("-"))
                {
                    imaginary = -imaginary;
                }
                return new Complex(double.Parse(parts[0]), imaginary);
            }

            return Complex.Zero;
        }

        private string FormatComplex(Complex number)
        {
            double real = Math.Abs(number.Real) < 1e-10 ? 0 : number.Real;
            double imaginary = Math.Abs(number.Imaginary) < 1e-10 ? 0 : number.Imaginary;

            if (imaginary >= 0)
            {
                return $"{real} + {imaginary}i";
            }
            else
            {
                return $"{real} - {Math.Abs(imaginary)}i";
            }
        }

        static void Main(string[] args)
        {
            CramerSolver solver = new CramerSolver();
            solver.InputCoefficients();
            solver.Solve();
        }
    }
}