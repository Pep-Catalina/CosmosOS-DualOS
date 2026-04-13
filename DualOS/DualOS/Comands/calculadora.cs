using System;

namespace DualOS
{
    public static class Calculadora
    {
        public static void Execute(string[] parts)
        {
            if (parts.Length < 2)
            {
                ShowUsage();
                return;
            }

            string operation = parts[1].ToLower();

            switch (operation)
            {
                case "add":
                    DoBinaryOperation(parts, "+");
                    break;

                case "sub":
                    DoBinaryOperation(parts, "-");
                    break;

                case "mul":
                    DoBinaryOperation(parts, "*");
                    break;

                case "div":
                    DoBinaryOperation(parts, "/");
                    break;

                case "mod":
                    DoBinaryOperation(parts, "%");
                    break;

                case "sqrt":
                    DoSqrt(parts);
                    break;

                default:
                    Console.WriteLine("Unknown calc operation.");
                    ShowUsage();
                    break;
            }
        }

        private static void DoBinaryOperation(string[] parts, string op)
        {
            if (parts.Length < 4)
            {
                Console.WriteLine("This operation needs 2 numbers.");
                ShowUsage();
                return;
            }

            if (!double.TryParse(parts[2], out double num1) || !double.TryParse(parts[3], out double num2))
            {
                Console.WriteLine("Invalid numbers.");
                return;
            }

            double result = 0;

            switch (op)
            {
                case "+":
                    result = num1 + num2;
                    break;

                case "-":
                    result = num1 - num2;
                    break;

                case "*":
                    result = num1 * num2;
                    break;

                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("Cannot divide by zero.");
                        return;
                    }
                    result = num1 / num2;
                    break;

                case "%":
                    if (num2 == 0)
                    {
                        Console.WriteLine("Cannot modulo by zero.");
                        return;
                    }
                    result = num1 % num2;
                    break;
            }

            Console.WriteLine("Result: " + result);
        }

        private static void DoSqrt(string[] parts)
        {
            if (parts.Length < 3)
            {
                Console.WriteLine("sqrt needs 1 number.");
                ShowUsage();
                return;
            }

            if (!double.TryParse(parts[2], out double num))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            if (num < 0)
            {
                Console.WriteLine("Cannot calculate square root of a negative number.");
                return;
            }

            double result = Math.Sqrt(num);
            Console.WriteLine("Result: " + result);
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Calculator usage:");
            Console.WriteLine("  calc add 5 3");
            Console.WriteLine("  calc sub 9 2");
            Console.WriteLine("  calc mul 4 6");
            Console.WriteLine("  calc div 8 2");
            Console.WriteLine("  calc mod 10 3");
            Console.WriteLine("  calc sqrt 25");
        }
    }
}