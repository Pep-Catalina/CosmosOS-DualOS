using System;
using System.Text;

namespace DualOS
{
    public static class Calculadora
    {
        public static void Execute(string[] parts)
        {
            Console.WriteLine(ExecuteToString(parts));
        }

        public static string ExecuteToString(string[] parts)
        {
            if (parts.Length < 2)
            {
                return GetUsage();
            }

            string operation = parts[1].ToLower();

            switch (operation)
            {
                case "add":
                    return DoBinaryOperation(parts, "+");

                case "sub":
                    return DoBinaryOperation(parts, "-");

                case "mul":
                    return DoBinaryOperation(parts, "*");

                case "div":
                    return DoBinaryOperation(parts, "/");

                case "mod":
                    return DoBinaryOperation(parts, "%");

                case "sqrt":
                    return DoSqrt(parts);

                default:
                    return "Unknown calc operation.\n" + GetUsage();
            }
        }

        private static string DoBinaryOperation(string[] parts, string op)
        {
            if (parts.Length < 4)
            {
                return "This operation needs 2 numbers.\n" + GetUsage();
            }

            double num1;
            double num2;

            if (!double.TryParse(parts[2], out num1) || !double.TryParse(parts[3], out num2))
            {
                return "Invalid numbers.";
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
                        return "Cannot divide by zero.";
                    }

                    result = num1 / num2;
                    break;

                case "%":
                    if (num2 == 0)
                    {
                        return "Cannot modulo by zero.";
                    }

                    result = num1 % num2;
                    break;
            }

            return "Result: " + result;
        }

        private static string DoSqrt(string[] parts)
        {
            if (parts.Length < 3)
            {
                return "sqrt needs 1 number.\n" + GetUsage();
            }

            double num;

            if (!double.TryParse(parts[2], out num))
            {
                return "Invalid number.";
            }

            if (num < 0)
            {
                return "Cannot calculate square root of a negative number.";
            }

            return "Result: " + Math.Sqrt(num);
        }

        private static string GetUsage()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Calculator usage:");
            sb.AppendLine("  calc add 5 3");
            sb.AppendLine("  calc sub 9 2");
            sb.AppendLine("  calc mul 4 6");
            sb.AppendLine("  calc div 8 2");
            sb.AppendLine("  calc mod 10 3");
            sb.AppendLine("  calc sqrt 25");

            return sb.ToString();
        }
    }
}
